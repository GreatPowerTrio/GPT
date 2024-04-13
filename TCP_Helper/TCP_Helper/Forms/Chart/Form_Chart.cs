using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TCP_Helper.Forms.Chart
{
	public partial class Form_Chart : Form
	{
		private bool ShowAllFlag = true;
		private bool FormHadShown = false; //窗体是否已经展示；窗体显示后方能更新数据

		private readonly DataBuffer buffer = new DataBuffer(); //数据接收缓存

		
		static int LinesNum = 0;
		static int PointsNum = 201; //初始采样点个数
		static int XGridNumber = 10;
		static int YGridNumber = 5;
		private Timer TimerShow = new Timer(); //定时刷新图像

		public List<List<float>> DataValues = new List<List<float>>(); //接收数据存储

		private List<List<float>> Yaxis = new List<List<float>>(); //Y轴队列
		private List<int> Xaxis = new List<int>(); //X轴队列
		private List<Label> LineLabel = new List<Label>(); //线条提示label
		private RectangleF SlidingRect = new RectangleF(0, 0, 0, 0); //当前滑动显示区域
		public List<bool> LineEnable = new List<bool>(); //线条是否显示
		private bool XaxisAuto = true; //X轴自动更新
		private int YaxisAuto = 1; //Y轴自动更新 0无极值点 1极值点保存 2实时极值点
		private bool SpecialUpdate = false; //特殊更新标志位
		private bool ClearFlag = false; //清除标志位
		private RectangleF LastSlidingRect = new RectangleF(0, 0, -1, 0); //保存上一次滑动窗位置
		private List<float> ExtremeValue = new List<float>(); //极值点

		//线条颜色
		private Color x_Color = Color.FromArgb(255, 0, 0);
		private Color y_Color = Color.FromArgb(0, 255, 0);
        private Color wave_color = Color.FromArgb(0, 0, 255);

		public Form_Chart()
		{
			InitializeComponent();
			//双缓存区
			SetStyle(ControlStyles.OptimizedDoubleBuffer
				  | ControlStyles.ResizeRedraw
				  | ControlStyles.Selectable
				  | ControlStyles.AllPaintingInWmPaint
				  | ControlStyles.UserPaint
				  | ControlStyles.SupportsTransparentBackColor, true);
			//设置双缓存区
			MethodInfo methodInfo = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
			methodInfo.Invoke(chart, new object[] { ControlStyles.OptimizedDoubleBuffer, true });

			

			#region 回调函数

			#endregion
			#region Lines区域

			#endregion

			#region 图表初始化
			//线段初始化
			chart.Series.Clear();
			LineEnable.Add(true);
			DataValues.Add(new List<float>());
			Yaxis.Add(new List<float>());
			LineLabel.Add(new Label()
			{
				Parent = chart,
				Visible = false,
				BackColor = Color.Red,
				ForeColor = Color.Blue,
			});


			//标签初始化
			for (int i = 0; i <= PointsNum; i += 2 * (PointsNum / XGridNumber))
			{
				chart.ChartAreas[0].AxisX.CustomLabels.Add(new CustomLabel());
			}

			//部分属性初始化
			
			chart.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
			chart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Microsoft Sans Serif", 8);
			chart.ChartAreas[0].AxisX.LineColor = x_Color;
			chart.ChartAreas[0].AxisX.Minimum = -1;
			chart.ChartAreas[0].AxisX.Interval = 1;
			chart.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Microsoft Sans Serif", 8);
			chart.ChartAreas[0].AxisY.LineColor = y_Color;
			chart.ChartAreas[0].InnerPlotPosition.Height = 95;
			chart.ChartAreas[0].InnerPlotPosition.Width = 100;
			chart.ChartAreas[0].InnerPlotPosition.X = 6;
			chart.ChartAreas[0].InnerPlotPosition.Y = 0;
			SlidingRect.Y = LastSlidingRect.Y = 1.0f;
			SlidingRect.Height = LastSlidingRect.Height = 2.0f;
			#endregion
		}

		public void DataReceived(byte[] data)
		{
			buffer.AddendData(data);
			var newdata = buffer.GetDeleteData(-1);
			DebugBox_Add(newdata);
			ChartLinesShow();
			var datas = Unpack(newdata); //如果传输数据有打包则需要解包
										 //波形数据更新
			foreach (var nums in datas)
			{
				AppendData(nums);

			}
		}

		public void DataHandler(List<object> arg)
		{
			if (buffer.HasChanged())
			{
				///数据处理
				var newdata = buffer.GetDeleteData(-1);
				var datas = Unpack(newdata); //如果传输数据有打包则需要解包

				//波形数据更新
				foreach (var nums in datas)
				{
					AppendData(nums);
					
				}
			}
		}

		private void DebugBox_Add(byte[] data) 
		{
			DebugBox.Invoke(new Action(() =>
			{
				DebugBox.AppendText(BitConverter.ToString(data) + "\r\n");
			}));	
		}
		#region 数据解包
		//帧头  帧尾
		private byte sof = 0x28;
		private byte tail = 0x29;
		private List<byte> databuffer = new List<byte>();
		private byte check_lenth = 0;
		private byte data_type = 0;
		private int data_size = 0;
		private int data_count = 0;
		public List<List<float>> Unpack(byte[] newdata)
		{
			List<List<float>> result = new List<List<float>>();
			databuffer.AddRange(newdata);
			int errorflag = 0; //错误标记位
		start:
			///SOF
			if (check_lenth == 0)
			{
				int sofindex = databuffer.IndexOf(sof);
				if (sofindex != -1)
				{
					databuffer.RemoveRange(0, sofindex);
					check_lenth++;
				}
				else
				{
					errorflag = 1;
					goto error;
				}
			}
			///Type
			if (check_lenth == 1 && databuffer.Count > 1)
			{
				byte temp = databuffer[check_lenth];
				if (temp == 0x0f) data_size = 4;
				else data_size = temp & 0x07;
				if ((temp & 0xf0) == 0 && (temp == 0x0f || data_size == 1 || data_size == 2 || data_size == 4))
				{
					data_type = temp;
					check_lenth++;
				}
				else
				{
					errorflag = 2;
					goto error;
				}
			}
			///Lenth
			if (check_lenth == 2 && databuffer.Count > 2)
			{
				byte temp = databuffer[check_lenth];
				if (temp <= 8)
				{
					data_count = temp;
					check_lenth++;
				}
				else
				{
					errorflag = 3;
					goto error;
				}
			}
			///Tail
			if (check_lenth >= 3 && databuffer.Count > 3 + data_size * data_count)
			{
				byte temp = databuffer[3 + data_size * data_count];
				//校验正确
				if (temp == tail)
				{
					//提取数据字节部分
					List<List<byte>> frames = new List<List<byte>>();
					//高低位置换标志位
					bool Convert = BitConverter.IsLittleEndian ? false : true;
					for (int index = 3; index < 3 + data_size * data_count; index += data_size)
					{
						List<byte> frame = new List<byte>();
						frame.AddRange(databuffer.GetRange(index, data_size));
						if (Convert) frame.Reverse();
						frames.Add(frame);
					}
					//转换成数据
					List<float> dataframe = new List<float>();
					foreach (List<byte> singleframe in frames)
					{
						if (data_type == 0x0f) //浮点型
						{
							dataframe.Add(BitConverter.ToSingle(singleframe.ToArray(), 0));
						}
						else if ((data_type & 0x08) == 0) //无符号型
						{
							if (data_size == 1)
								dataframe.Add(singleframe.First());
							else if (data_size == 2)
								dataframe.Add(BitConverter.ToUInt16(singleframe.ToArray(), 0));
							else
								dataframe.Add(BitConverter.ToUInt32(singleframe.ToArray(), 0));
						}
						else //有符号型
						{
							if (data_size == 1)
							{
								int num = singleframe[0] & 0x7f;
								if ((singleframe[0] & 0x80) != 0) num -= 128;
								dataframe.Add(num);
							}
							else if (data_size == 2)
								dataframe.Add(BitConverter.ToInt16(singleframe.ToArray(), 0));
							else
								dataframe.Add(BitConverter.ToInt32(singleframe.ToArray(), 0));
						}
					}
					result.Add(dataframe);
					///清除该帧数据
					databuffer.RemoveRange(0, 4 + data_size * data_count);
					goto clear;
				}
				else
				{
					errorflag = 4;
					goto error;
				}
			}
			return result;

		///数据传输错误
		error:
			int sof_index = databuffer.IndexOf(sof);
			if (sof_index != -1) databuffer.RemoveRange(0, sof_index + 1);
			else databuffer.Clear();
			///清除标志位 继续解包
			clear:
			check_lenth = 0;
			data_type = 0;
			data_size = data_count = 0;
			if (databuffer.IndexOf(sof) != -1) goto start;
			return result;
		}

		#endregion

		#region 数据添加
		/// <summary>
		/// 数据添加
		/// </summary>
		/// <param name="newdata"></param>
		public void AppendData(List<float> newdata)
		{
			if (!FormHadShown || newdata.Count == 0) return;
			if (LinesNum == 0) LinesNum = newdata.Count; //初次设置线条数
			if (newdata.Count == LinesNum && LinesNum != 0)
			{
				for (int index = 0; index < LinesNum; index++)
				{
					DataValues[index].Add(newdata[index]);
				}
			}
			else //监测到数据个数不一致，则清空数据并重新绘制
			{
				//Clear();
				return;
			}

/*			Action_DataEvent?.Invoke(newdata);*/
		}

		#endregion

		#region 图表绘制

		private void ChartLinesShow()
		{
			if (chart.InvokeRequired)
			{
				Action action = ChartLinesShow;
				chart.Invoke(action);
			/*}
			else
			{*/
				chart.Series.Clear();

				//图表范围更新
				chart.ChartAreas[0].AxisX.Maximum = (Xaxis.Count < PointsNum || ShowAllFlag) && DataValues[0].Count > PointsNum ? Xaxis.Count : PointsNum;
				chart.ChartAreas[0].AxisX.MajorGrid.Interval = chart.ChartAreas[0].AxisX.Maximum / XGridNumber;
				chart.ChartAreas[0].AxisX.MajorTickMark.Interval = chart.ChartAreas[0].AxisX.Maximum / XGridNumber;

				chart.ChartAreas[0].AxisY.Maximum = SlidingRect.Height != 0 ? SlidingRect.Y : SlidingRect.Y + 0.5F;
				chart.ChartAreas[0].AxisY.Minimum = SlidingRect.Height != 0 ? SlidingRect.Y - SlidingRect.Height : SlidingRect.Y - 0.5F;
				chart.ChartAreas[0].AxisY.MajorGrid.Interval = SlidingRect.Height / YGridNumber;
				chart.ChartAreas[0].AxisY.MajorTickMark.Interval = SlidingRect.Height / YGridNumber;

				//横坐标标签更新
				for (int i = 1; i < chart.ChartAreas[0].AxisX.CustomLabels.Count; i++)
				{
					chart.ChartAreas[0].AxisX.CustomLabels[i].FromPosition = 4 * i * chart.ChartAreas[0].AxisX.Maximum / XGridNumber;
					chart.ChartAreas[0].AxisX.CustomLabels[i].Text = ((int)(SlidingRect.X + 2 * i * (DataValues[0].Count > PointsNum ? SlidingRect.Width : PointsNum) / XGridNumber)).ToString();
				}

				//曲线更新
				for (int yindex = 0; yindex < LinesNum; yindex++)
				{
					if (!LineEnable[yindex]) continue;
					Series line = new Series
					{
						//波形样式
						MarkerStyle = ShowAllFlag ? MarkerStyle.None : MarkerStyle.Circle,
						MarkerSize = 3,
						MarkerStep = 1,
						ChartType = SeriesChartType.Line,
						BorderWidth = 2,
						Color = wave_color,
						IsXValueIndexed = true, //硬件加速
					};
					line.Points.DataBindXY(Xaxis.GetRange(0, Yaxis[yindex].Count), Yaxis[yindex]);
					chart.Series.Add(line);
				}
			}
		}

		#endregion

	}
}

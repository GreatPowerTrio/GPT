using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TCP_Helper.Forms.Chart
{
	public partial class Form_Chart : Form
	{
		
		private readonly DataBuffer buffer = new DataBuffer(); //数据接收缓存
        private Queue<double> dataQueue = new Queue<double>(100);

        private int curValue = 0;
        private double[] resultDouble;
        private int num = 1;//每次删除增加几个点
		

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
			this.chart.ChartAreas.Clear();
			ChartArea chartArea1 = new ChartArea("C1");
			this.chart.ChartAreas.Add(chartArea1);
			
			this.chart.Series.Clear();
			Series series1 = new Series("S1");
			series1.ChartArea = "C1";
			this.chart.Series.Add(series1);

			this.chart.ChartAreas[0].AxisY.Minimum = 0;
			this.chart.ChartAreas[0].AxisY.Maximum = 4096;
			this.chart.ChartAreas[0].AxisX.Interval = 10;

			this.chart.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.White;
			this.chart.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;

			this.chart.Titles.Clear();
            this.chart.Titles.Add("S1");
            this.chart.Titles[0].Text = "XXX显示";
            this.chart.Titles[0].ForeColor = Color.RoyalBlue;
            this.chart.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);

            this.chart.Series[0].Color = Color.Red;

            this.chart.Titles[0].Text = string.Format("XXX\"波形\"显示");
            this.chart.Series[0].ChartType = SeriesChartType.Line;
            this.chart.Series[0].Points.Clear();
        }


        #endregion

        private int tick = 0;
        string resultStr = null;
        public void DataReceived(byte[] data)
		{
			buffer.AddendData(data);
			var newdata = buffer.GetDeleteData(-1);
            if (tick < num)
			{
				resultStr += System.Text.Encoding.ASCII.GetString(data) + ",";
				tick++;
			}
			else 
			{
                string[] resultStrArray = resultStr.Split(",".ToCharArray());
                //resultDouble = Array.ConvertAll<string, double>(resultStrArray, s => double.Parse(s));
                resultDouble = Array.ConvertAll<string, double>(resultStrArray, s =>
                {
                    double value;
                    if (double.TryParse(s, out value))
                        return value;
                    else
                        return 0;  // or any other value you want to use for invalid strings
                });
                for (int i = 0; i < resultStrArray.Length; i++)

                    DebugBox.Invoke(new Action(() =>
                    {
                        DebugBox.AppendText(resultStrArray[i] + " ");

                    }));
                // DebugBox_Add(newdata);
                tick = 0;
				resultStr = null;
                this.chart.Invoke((Action)(() =>
                {
                    UpdateQueueValue();
                    this.chart.Series[0].Points.Clear();
                    for (int i = 0; i < dataQueue.Count; i++)
                    {
                        this.chart.Series[0].Points.AddXY((i + 1), dataQueue.ElementAt(i));
                    }
                }));
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
				DebugBox.AppendText("-" + Encoding.ASCII.GetString(data));

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
			
		}

		#endregion

		#region 图表绘制


		#endregion

		private void Chart_Btn_Click(object sender, EventArgs e)
		{
			if (Chart_Btn.Text == "启动")
			{
				this.timer1.Start();
				Chart_Btn.Text = "关闭";
			}
			else 
			{
				this.timer1.Stop();
				Chart_Btn.Text = "启动";
            }
		}

        private void timer1_Tick(object sender, EventArgs e)
        {
            
			/*UpdateQueueValue();
            this.chart.Series[0].Points.Clear();
            for (int i = 0; i < dataQueue.Count; i++)
            {
                this.chart.Series[0].Points.AddXY((i + 1), dataQueue.ElementAt(i));
            }*/
        }

		private void UpdateQueueValue()
		{

			if (dataQueue.Count > 100)
			{
				//先出列
				for (int i = 0; i < num; i++)
				{
					dataQueue.Dequeue();
				}
			}

			/* Random r = new Random();
             for (int i = 0; i < num; i++)
             {
                 dataQueue.Enqueue(r.Next(0, 100));
             }*/
			 
             for (int i = 0; i < num; i++)
             {

				dataQueue.Enqueue(resultDouble[i]);
             }

        }
    }
}

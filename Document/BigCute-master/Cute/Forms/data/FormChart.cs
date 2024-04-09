using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Cute
{
    public partial class FormChart : Form
    {
        private bool FormHadShown = false; //窗体是否已经展示；窗体显示后方能更新数据

        static int LineNumMax = 8;
        static int LinesNum = 0;
        static int PointsNum = 201; //初始采样点个数
        static int XGridNumber = 10;
        static int YGridNumber = 5;
        private Timer TimerShow = new Timer(); //定时刷新图像
        private List<SelectTab.Tab> tabs = new List<SelectTab.Tab>();
        Paraments param = new Paraments(); //配置参数集
        public List<List<float>> DataValues = new List<List<float>>(); //接收数据存储
        private List<List<float>> Yaxis = new List<List<float>>(); //Y轴队列
        private List<int> Xaxis = new List<int>(); //X轴队列
        private RectangleF SlidingRect = new RectangleF(0, 0, 0, 0); //当前滑动显示区域
        public List<Color> LineColors = new List<Color>(); //线条颜色
        private List<Label> LineLabel = new List<Label>(); //线条提示label
        public List<bool> LineEnable = new List<bool>(); //线条是否显示
        private bool XaxisAuto = true; //X轴自动更新
        private int YaxisAuto = 1; //Y轴自动更新 0无极值点 1极值点保存 2实时极值点
        private bool SpecialUpdate = false; //特殊更新标志位
        private bool ClearFlag = false; //清除标志位

        public Action Action_Clear; //此类为了同步主界面传递参数
        public Action Action_Save;
        public Action Action_Add;
        public Action Action_Import;
        public Action<int> TextGoto;
        private event Action<List<float>> Action_DataEvent; //事件函数为了控制后续多开界面
        private event Action Action_TimeStartEvent;
        private event Action Action_TimeStopEvent;
        private event Action Action_ClearEvent;
        public FormChart()
        {
            InitializeComponent();
            //双缓存区
            SetStyle(ControlStyles.OptimizedDoubleBuffer
                  | ControlStyles.ResizeRedraw
                  | ControlStyles.Selectable
                  | ControlStyles.AllPaintingInWmPaint
                  | ControlStyles.UserPaint
                  | ControlStyles.SupportsTransparentBackColor, true);

            #region ToolTips
            Action<Control, string> action = (c, s) =>
            {
                ToolTip tool = new ToolTip
                {
                    AutoPopDelay = 5000,
                    InitialDelay = 500,
                    ReshowDelay = 0,
                    ShowAlways = true
                };
                tool.SetToolTip(c, s);
            };
            action.Invoke(panel_Add, "添加波形页面");
            action.Invoke(panel_Save, "保存数据到文本");
            action.Invoke(panel_Clear, "清空显示及数据");
            action.Invoke(panel_Import, "导入数据");
            #endregion

            //设置双缓存区
            MethodInfo methodInfo = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
            methodInfo.Invoke(chart, new object[] { ControlStyles.OptimizedDoubleBuffer, true });

            //设置图标为主界面图标
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            Panel_Yaixs_Init();

            #region 线条颜色
            LineColors = new List<Color>();
            LineColors.AddRange(param.ChartLines);
            #endregion

            #region 回调函数设置
            Shown += FormChart_Shown;
            TimerShow.Tick += TimerShow_Tick;
            chart.MouseDown += Chart_MouseDown;
            chart.MouseMove += Chart_MouseMove;
            chart.MouseUp += Chart_MouseUp;
            chart.KeyDown += Chart_KeyDown;
            chart.Click += Chart_Click;
            chart.MouseWheel += Chart_MouseWheel;
            chart.GetToolTipText += Chart_GetToolTipText;
            #endregion

            #region Lines区域
            for (int i = LineNumMax - 1; i >= 0; i--)
            {
                Panel panel = new Panel
                {
                    Name = "panel" + i.ToString(),
                    Width = panel_temp.Width,
                    Dock = DockStyle.Left,
                    Parent = panel_lines,
                };
                Label label = new Label
                {
                    Name = "label" + i.ToString(),
                    Text = "Line" + (i + 1).ToString(),
                    ForeColor = param.Chart_LineTitle,
                    Parent = panel,
                    Location = label_temp.Location,
                };
                tabs.Add(new SelectTab.Tab(panel, label));

                label.MouseDown += LabelTab_MouseDown;
                label.Parent.MouseDown += PanelTab_MouseDown;
                label.MouseEnter += LabelTab_MouseEnter;
                label.MouseLeave += LabelTab_MouseLeave;
                label.Parent.MouseEnter += PanelTab_MouseEnter;
                label.Parent.MouseLeave += PanelTab_MouseLeave;
                label.Parent.Paint += PanelTab_Paint;
            }
            label_temp.Dispose();
            panel_temp.Dispose();
            #endregion

            #region 图表初始化
            //线段初始化
            chart.Series.Clear();
            for (int num = 0; num < LineNumMax; num++)
            {
                LineEnable.Add(true);
                DataValues.Add(new List<float>());
                Yaxis.Add(new List<float>());
                LineLabel.Add(new Label()
                {
                    Parent = chart,
                    Visible = false,
                    BackColor = param.Chart_FunctionPre,
                    ForeColor = LineColors[num],
                });
            }

            //标签初始化
            for (int i = 0; i <= PointsNum; i += 2 * (PointsNum / XGridNumber))
            {
                chart.ChartAreas[0].AxisX.CustomLabels.Add(new CustomLabel());
            }

            //部分属性初始化
            label_title.Font = new Font("Microsoft Sans Serif", 8);
            label_title.ForeColor = param.Chart_Title;
            label_title.Text = "0-0 : 0";
            chart.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
            chart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Microsoft Sans Serif", 8);
            chart.ChartAreas[0].AxisX.LineColor = param.Chart_Axes;
            chart.ChartAreas[0].AxisX.Minimum = -1;
            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Microsoft Sans Serif", 8);
            chart.ChartAreas[0].AxisY.LineColor = param.Chart_Axes;
            chart.ChartAreas[0].InnerPlotPosition.Height = 95;
            chart.ChartAreas[0].InnerPlotPosition.Width = 100;
            chart.ChartAreas[0].InnerPlotPosition.X = 6;
            chart.ChartAreas[0].InnerPlotPosition.Y = 0;
            SlidingRect.Y = LastSlidingRect.Y = 1.0f;
            SlidingRect.Height = LastSlidingRect.Height = 2.0f;
            #endregion

            SetTag(this);

            TimerShow.Interval = 100; //刷新速率10fps
        }


        private void FormChart_Shown(object sender, EventArgs e)
        {
            FormHadShown = true;
        }

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
                Clear();
                return;
            }

            Action_DataEvent?.Invoke(newdata);
        }

        public void ImportData(List<float> newdata)
        {
            Action_Clear?.Invoke();
        }
        #endregion

        #region 数据清除
        /// <summary>
        /// 图标数据清零
        /// </summary>
        public void Clear()
        {
            ClearFlag = true;
            Action_ClearEvent?.Invoke();
        }

        private void ChartClear()
        {
            for (int index = 0; index < LinesNum; index++)
            {
                DataValues[index].Clear();
                Yaxis[index].Clear();
            }
            chart.Series.Clear();
            LinesNum = 0;
            SlidingRect = new RectangleF(0, 0, 0, 0);
            LastSlidingRect = new RectangleF(0, 0, -1, 0);
            ClearFlag = false;
            XaxisAuto = true;
            Panel_Xaixs_Init();
            Panel_Yaixs_Init();
            XInterval = YInterval = 1.0f;
            LastXInterval = LastYInterval = 0.1f;
            ExtremeValue.Clear();
        }
        #endregion

        #region UI定时更新
        public void TimerStart()
        {
            if (!IsDisposed)
            {
                if (!TimerShow.Enabled)
                    TimerShow.Start();
                chart.Focus(); //为图表设置输入焦点
            }
            Action_TimeStartEvent?.Invoke();
        }

        public void TimerStop()
        {
            if (TimerShow.Enabled)
                TimerShow.Stop();
            Action_TimeStopEvent?.Invoke();
        }

        private RectangleF LastSlidingRect = new RectangleF(0, 0, -1, 0); //保存上一次滑动窗位置
        private List<float> ExtremeValue = new List<float>(); //极值点
        /// <summary>
        /// 数据定时更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TimerShow_Tick(object sender, EventArgs e)
        {
            ///存在线程未执行完，则此次不更新
            if (!chart.Enabled || !FormHadShown) return;
            chart.Enabled = false; //禁止响应用户指令
            if (ClearFlag) ChartClear(); //图表清零

            ///计算线程
            Task<int> CalculateTask = Task.Run(() =>
            {
                int TotalPoints = DataValues[0].Count; //总点数
                float xinterval = XInterval; //间隔点数
                float yinterval = YInterval;

                ///更新滑动框位置
                ///缩放，以图像中心点为原点进行缩放
                if (xinterval != LastXInterval || yinterval != LastYInterval)
                {
                    int Width = (int)(PointsNum * xinterval);
                    float MidX = SlidingRect.X + 0.5F * SlidingRect.Width;
                    SlidingRect.Width = TotalPoints > Width ? Width : TotalPoints - 1;
                    SlidingRect.X = MidX - 0.5F * SlidingRect.Width;
                    if (SlidingRect.X < 0) SlidingRect.X = 0;
                    if (SlidingRect.Right > TotalPoints) SlidingRect.Width = TotalPoints - SlidingRect.X - 1;
                    if ((SlidingRect.Height > 0.01F && (yinterval - LastYInterval) < 0)
                    || (Math.Abs(SlidingRect.Y) < float.MaxValue / 2
                    && Math.Abs(SlidingRect.Height) < float.MaxValue / 2
                    && Math.Abs(SlidingRect.Bottom) < float.MaxValue / 2
                    && (yinterval - LastYInterval) > 0))
                    {
                        SlidingRect.Y = SlidingRect.Y + ((yinterval - LastYInterval) > 0 ? 0.1F : -0.1F) * SlidingRect.Height / 2;
                        SlidingRect.Height *= (yinterval - LastYInterval) > 0 ? 1.2F : 0.8F;
                    }
                    LastXInterval = xinterval;
                    LastYInterval = yinterval;
                }

                ///鼠标拖动
                if (MoveStep != PointF.Empty)
                {
                    SlidingRect.Y -= MoveStep.Y;
                    SlidingRect.X -= MoveStep.X * (ShowAllFlag ? 1 : (xinterval > 1 ? xinterval : 1));
                    if (SlidingRect.Right > TotalPoints)
                    {
                        SlidingRect.X -= SlidingRect.Right - TotalPoints + 1;
                        if (!XaxisAuto)
                        {
                            XaxisAuto = true;
                            Panel_Xaixs_Init();
                        }
                    }
                    else if (XaxisAuto == true)
                    {
                        XaxisAuto = false;
                        Panel_Xaixs_Init();
                    }
                    if (SlidingRect.X < 0) SlidingRect.X = 0;
                    MoveStep = PointF.Empty;
                }

                ///自动更新滑动框水平位
                if (XaxisAuto)
                {
                    SlidingRect.Width = TotalPoints > (PointsNum - 1) * xinterval ? (PointsNum - 1) * xinterval : TotalPoints - 1; //滑动窗宽度
                    SlidingRect.X = TotalPoints - (SlidingRect.Width + 1); //滑动窗起始位置
                }

                ///滑动窗有更新或标志位设置，则更新图表数据
                if (SlidingRect != LastSlidingRect || SpecialUpdate)
                {
                    ///计算数据范围
                    Xaxis.Clear();
                    for (int index = 0; index <= (int)SlidingRect.Width; index++) Xaxis.Add(index);
                    //最多显示PointsNum个点
                    float interval = (float)Xaxis.Count / PointsNum;
                    if (interval < 1 || ShowAllFlag) interval = 1;
                    for (int yindex = 0; yindex < LinesNum; yindex++)
                    {
                        Yaxis[yindex].Clear(); //清除原有数据
                        if (LineEnable[yindex])
                        {
                            for (float xindex = 0; xindex <= (int)SlidingRect.Width; xindex += interval)
                                Yaxis[yindex].Add(DataValues[yindex][(int)(xindex) + (int)SlidingRect.X]);
                            ExtremeValue.Add(Yaxis[yindex].Min());
                            ExtremeValue.Add(Yaxis[yindex].Max());
                        }
                    }
                    ///自动更新滑动框竖直位置             
                    ///0无极值点 1极值点保存 2实时极值点
                    if (ExtremeValue.Count != 0)
                    {
                        //计算极值点
                        float Max = ExtremeValue.Max();
                        float Min = ExtremeValue.Min();
                        ExtremeValue.Clear();
                        ExtremeValue.AddRange(new float[] { Max, Min });

                        if (YaxisAuto == 0)
                        {

                        }
                        else if (YaxisAuto == 1)
                        {
                            if (SlidingRect.Y < Max)
                            {
                                SlidingRect.Height += Max - SlidingRect.Y;
                                SlidingRect.Y = Max;
                            }
                            if (SlidingRect.Y - SlidingRect.Height > Min)
                                SlidingRect.Height = SlidingRect.Y - Min;
                        }
                        else if (YaxisAuto == 2)
                        {
                            if (Max - Min > 0.000001F)
                            {
                                SlidingRect.Y = Max;
                                SlidingRect.Height = SlidingRect.Y - Min;
                                ExtremeValue.Clear();
                            }
                        }
                    }
                }
                return TotalPoints;
            });

            ///UI更新线程
            CalculateTask.ContinueWith(t =>
            {
                ///标题UI更新
                ChartTitleShow(t.Result);

                ///图表控件UI更新
                if (SlidingRect != LastSlidingRect || SpecialUpdate)
                {
                    ChartLinesShow();
                    SpecialUpdate = false;
                    LastSlidingRect = SlidingRect;
                }

                ///响应用户指令
                chart.Enabled = true;
            });
        }

        /// <summary>
        /// 图表曲线更新
        /// </summary>
        private void ChartLinesShow()
        {
            if (chart.InvokeRequired)
            {
                Action action = ChartLinesShow;
                chart.Invoke(action);
            }
            else
            {
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
                        Color = LineColors[yindex],
                        IsXValueIndexed = true, //硬件加速
                    };
                    line.Points.DataBindXY(Xaxis.GetRange(0, Yaxis[yindex].Count), Yaxis[yindex]);
                    chart.Series.Add(line);
                }
            }
        }

        /// <summary>
        /// 图表标题更新
        /// </summary>
        private void ChartTitleShow(int count)
        {
            if (label_title.InvokeRequired)
            {
                Action<int> action = ChartTitleShow;
                label_title.Invoke(action, count);
            }
            else
            {
                int Min = (int)SlidingRect.X;
                int Max = (int)(SlidingRect.Right > 0 ? SlidingRect.Right : 0);
                label_title.Text = string.Format("{0} - {1}  :  {2} / {3}", Min, Max, DataValues[0].Count > 0 ? Max - Min + 1 : 0, count);
                label_title.Location = new Point((label_title.Parent.Width - label_title.Width) / 2, label_title.Location.Y);
            }
        }

        /// <summary>
        /// ToolTips更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chart_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                int i = e.HitTestResult.PointIndex;
                DataPoint dp = e.HitTestResult.Series.Points[i];
                e.Text = string.Format("X: {0} \r\nY: {1:F3} ", (int)(SlidingRect.X +
                ((SlidingRect.Width > PointsNum && !ShowAllFlag) ? (dp.XValue == 0 ? 0 : dp.XValue + 1) * (SlidingRect.Width / PointsNum) : dp.XValue)), dp.YValues[0]);
            }
        }

        #region 重画边框
        private void panel_title_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            Pen pen = new Pen(param.Chart_NormalBorder, 1);
            e.Graphics.DrawLine(pen, new Point(0, 1), new Point(panel.Width, 1));
            label_title.Location = new Point((label_title.Parent.Width - label_title.Width) / 2, label_title.Location.Y);
        }
        #endregion

        #endregion

        #region lines区域

        ///选择页按下时发生
        private void PanelTab_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Panel panel = sender as Panel;
                int index = Convert.ToInt32(panel.Name.Substring(5));
                LineEnable[index] = !LineEnable[index];
                panel.Invalidate();
            }
        }

        private void LabelTab_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Label label = sender as Label;
                int index = Convert.ToInt32(label.Name.Substring(5));
                LineEnable[index] = !LineEnable[index];
                label.Parent.Invalidate();
            }
        }

        private void PanelTab_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            int index = Convert.ToInt32(panel.Name.Substring(5));
            Pen pen;
            if (LineEnable[index])
                pen = new Pen(LineColors[index], 3);
            else
                pen = new Pen(Color.Transparent, 3);
            Label label = panel.Controls[0] as Label;
            e.Graphics.DrawLine(pen,
                new Point(label.Left, (panel.Height - label.Bottom) / 2 + label.Bottom - 1),
                new Point(panel.Width - label.Left, (panel.Height - label.Bottom) / 2 + label.Bottom - 1));
            if (DataValues[0].Count != 0) SpecialUpdate = true; //设置特殊更新标志位
        }

        //鼠标进入选择页时UI变换
        private void PanelTab_MouseEnter(object sender, EventArgs e)
        {
            SelectedTab_MouseEnter((Label)((sender as Panel).Controls[0]));
        }
        private void PanelTab_MouseLeave(object sender, EventArgs e)
        {
            SelectedTab_MouseLeave((Label)((sender as Panel).Controls[0]));
        }
        private void LabelTab_MouseEnter(object sender, EventArgs e)
        {
            SelectedTab_MouseEnter(sender as Label);
        }
        private void LabelTab_MouseLeave(object sender, EventArgs e)
        {
            SelectedTab_MouseLeave(sender as Label);
        }
        private void SelectedTab_MouseEnter(Label label)
        {
            int index = Convert.ToInt32(label.Name.Substring(5));
            label.ForeColor = LineColors[index];
        }
        private void SelectedTab_MouseLeave(Label label)
        {
            label.ForeColor = param.Chart_LineTitle;
        }
        #endregion

        #region 功能区域

        #region Add
        private void panel_Add_MouseDown(object sender, MouseEventArgs e)
        {
            FormChart formChart = new FormChart();
            formChart.Text = Text == "数据收发--虚拟示波器" ? "数据收发--虚拟示波器" : "参数调节--虚拟示波器";
            formChart.Form_Changed(initSize);
            formChart.FormBorderStyle = FormBorderStyle.FixedSingle;
            formChart.Action_Clear = Action_Clear;
            formChart.Action_Save = Action_Save;
            formChart.TextGoto = TextGoto;
            Action_ClearEvent += formChart.Clear;
            //Action_DataEvent += formChart.AppendData;
            Action_TimeStartEvent += formChart.TimerStart;
            Action_TimeStopEvent += formChart.TimerStop;
            formChart.DataValues = DataValues; //浅表复制 
            formChart.Show();
            formChart.TimerStart();
        }

        private void panel_Add_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = global::Cute.Properties.Resources.新建;
            param.BackColorChange(bitmap, param.Chart_FunctionBack, param.Chart_FunctionPre);
            panel_Add.BackgroundImage = bitmap;
        }

        private void panel_Add_MouseLeave(object sender, EventArgs e)
        {
            panel_Add.BackgroundImage = global::Cute.Properties.Resources.新建;
        }
        #endregion

        #region Save
        private void panel_Save_MouseDown(object sender, MouseEventArgs e)
        {
            Action_Save?.Invoke();
        }

        private void panel_Save_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = global::Cute.Properties.Resources.保存;
            param.BackColorChange(bitmap, param.Chart_FunctionBack, param.Chart_FunctionPre);
            panel_Save.BackgroundImage = bitmap;
        }

        private void panel_Save_MouseLeave(object sender, EventArgs e)
        {
            panel_Save.BackgroundImage = global::Cute.Properties.Resources.保存;
        }
        #endregion

        #region Import
        private void panel_Import_MouseDown(object sender, MouseEventArgs e)
        {
            Action_Import?.Invoke();
        }

        private void panel_Import_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = global::Cute.Properties.Resources.文件夹导入;
            param.BackColorChange(bitmap, param.Chart_FunctionBack, param.Chart_FunctionPre);
            panel_Import.BackgroundImage = bitmap;
        }

        private void panel_Import_MouseLeave(object sender, EventArgs e)
        {
            panel_Import.BackgroundImage = global::Cute.Properties.Resources.文件夹导入;
        }
        #endregion

        #region Clear
        private void panel_Clear_MouseDown(object sender, MouseEventArgs e)
        {
            Action_Clear?.Invoke();
        }

        private void panel_Clear_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = global::Cute.Properties.Resources.清除;
            param.BackColorChange(bitmap, param.Chart_FunctionBack, param.Chart_FunctionPre);
            panel_Clear.BackgroundImage = bitmap;
        }

        private void panel_Clear_MouseLeave(object sender, EventArgs e)
        {
            panel_Clear.BackgroundImage = global::Cute.Properties.Resources.清除;
        }
        #endregion

        #region Xaixs
        private void panel_Xaixs_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap;
            if (XaxisAuto)
                bitmap = global::Cute.Properties.Resources.关闭自动更新;
            else
                bitmap = global::Cute.Properties.Resources.自动更新;
            param.BackColorChange(bitmap, param.Chart_FunctionBack, param.Chart_FunctionPre);
            panel_Xaxis.BackgroundImage = bitmap;
        }

        private void panel_Xaixs_MouseLeave(object sender, EventArgs e)
        {
            if (XaxisAuto)
                panel_Xaxis.BackgroundImage = global::Cute.Properties.Resources.关闭自动更新;
            else
                panel_Xaxis.BackgroundImage = global::Cute.Properties.Resources.自动更新;
        }

        private void panel_Xaixs_MouseDown(object sender, MouseEventArgs e)
        {
            Bitmap bitmap;
            if (XaxisAuto)
            {
                XaxisAuto = false;
                bitmap = global::Cute.Properties.Resources.自动更新;
            }
            else
            {
                XaxisAuto = true;
                bitmap = global::Cute.Properties.Resources.关闭自动更新;
            }
            param.BackColorChange(bitmap, param.Chart_FunctionBack, param.Chart_FunctionPre);
            panel_Xaxis.BackgroundImage = bitmap;
        }

        private void panel_Xaxis_MouseHover(object sender, EventArgs e)
        {
            ToolTip tool = new ToolTip
            {
                AutoPopDelay = 5000,
                InitialDelay = 500,
                ReshowDelay = 0,
                ShowAlways = true
            };
            string txt = XaxisAuto ? "关闭自动更新" : "开启自动更新";
            tool.SetToolTip(sender as Panel, txt);
        }

        private void Panel_Xaixs_Init()
        {
            if (panel_Xaxis.InvokeRequired)
            {
                Action action = Panel_Xaixs_Init;
                panel_Xaxis.Invoke(action);
            }
            else
            {
                panel_Xaixs_MouseLeave(null, null);
            }
        }
        #endregion

        #region Yaixs

        private void Panel_Yaixs_Init()
        {
            panel_YFree_MouseLeave(null, null);
            panel_YAuto_MouseLeave(null, null);
            panel_YLock_MouseLeave(null, null);
        }

        #region Free
        private void panel_YFree_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.y轴自由;
            if (YaxisAuto != 0)
                param.BackColorChange(bitmap, param.Chart_FunctionBack, param.Chart_FunctionPre);
            else
                param.BackColorChange(bitmap,
                    new Color[] { param.Chart_FunctionBack, param.Chart_Word },
                    new Color[] { param.Chart_FunctionPre, param.Chart_FunctionChoose });
            panel_YFree.BackgroundImage = bitmap;
        }
        private void panel_YFree_MouseLeave(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.y轴自由;
            if (YaxisAuto == 0) param.BackColorChange(bitmap, param.Chart_Word, param.Chart_FunctionChoose);
            panel_YFree.BackgroundImage = bitmap;
        }

        private void panel_YFree_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && YaxisAuto != 0)
            {
                int temp = YaxisAuto; YaxisAuto = 0;
                if (temp == 1) panel_YAuto_MouseLeave(null, null);
                else if (temp == 2) panel_YLock_MouseLeave(null, null);
                panel_YFree_MouseEnter(null, null);
                if (Yaxis[0].Count != 0) SpecialUpdate = true; //设置特殊更新标志位
            }
        }
        #endregion

        #region Auto

        private void panel_YAuto_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && YaxisAuto != 1)
            {
                int temp = YaxisAuto; YaxisAuto = 1;
                if (temp == 0) panel_YFree_MouseLeave(null, null);
                else if (temp == 2) panel_YLock_MouseLeave(null, null);
                panel_YAuto_MouseEnter(null, null);
                if (Yaxis[0].Count != 0) SpecialUpdate = true; //设置特殊更新标志位
            }
        }

        private void panel_YAuto_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.y轴自适应;
            if (YaxisAuto != 1)
                param.BackColorChange(bitmap, param.Chart_FunctionBack, param.Chart_FunctionPre);
            else
                param.BackColorChange(bitmap,
                    new Color[] { param.Chart_FunctionBack, param.Chart_Word },
                    new Color[] { param.Chart_FunctionPre, param.Chart_FunctionChoose });
            panel_YAuto.BackgroundImage = bitmap;
        }

        private void panel_YAuto_MouseLeave(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.y轴自适应;
            if (YaxisAuto == 1) param.BackColorChange(bitmap, param.Chart_Word, param.Chart_FunctionChoose);
            panel_YAuto.BackgroundImage = bitmap;
        }
        #endregion

        #region Lock
        private void panel_YLock_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && YaxisAuto != 2)
            {
                int temp = YaxisAuto; YaxisAuto = 2;
                if (temp == 0) panel_YFree_MouseLeave(null, null);
                else if (temp == 1) panel_YAuto_MouseLeave(null, null);
                panel_YLock_MouseEnter(null, null);
                if (Yaxis[0].Count != 0) SpecialUpdate = true; //设置特殊更新标志位
            }
        }

        private void panel_YLock_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.y轴固定;
            if (YaxisAuto != 2)
                param.BackColorChange(bitmap, param.Chart_FunctionBack, param.Chart_FunctionPre);
            else
                param.BackColorChange(bitmap,
                    new Color[] { param.Chart_FunctionBack, param.Chart_Word },
                    new Color[] { param.Chart_FunctionPre, param.Chart_FunctionChoose });
            panel_YLock.BackgroundImage = bitmap;
        }

        private void panel_YLock_MouseLeave(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.y轴固定;
            if (YaxisAuto == 2) param.BackColorChange(bitmap, param.Chart_Word, param.Chart_FunctionChoose);
            panel_YLock.BackgroundImage = bitmap;
        }
        #endregion 

        #endregion


        #endregion

        #region 控件大小首次显示缩放
        Size initSize;
        private void SetTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag += con.Font.Size + ";"
                    + (float)con.Left / con.Parent.Width + ";"
                    + (float)con.Top / con.Parent.Height + ";"
                    + (float)con.Width / con.Parent.Width + ";"
                    + (float)con.Height / con.Parent.Height;
                if (con.Controls.Count > 0)
                {
                    SetTag(con);
                }
            }
        }

        private void SetControls(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                con.Left = Convert.ToInt32(Convert.ToSingle(mytag[1]) * con.Parent.Width);
                con.Top = Convert.ToInt32(Convert.ToSingle(mytag[2]) * con.Parent.Height);
                con.Width = Convert.ToInt32(Convert.ToSingle(mytag[3]) * con.Parent.Width);
                con.Height = Convert.ToInt32(Convert.ToSingle(mytag[4]) * con.Parent.Height);
                if (con.Controls.Count > 0)
                {
                    SetControls(con);
                }
            }
        }

        /// <summary>
        /// 第一次显示窗体时控件位置调整
        /// </summary>
        public void Form_Changed(Size size)
        {
            initSize = this.Size = size;
            SetControls(this);
        }

        private void FormChart_SizeChanged(object sender, EventArgs e)
        {
            ReLocation(panel_funcbottom);
            ReLocation(panel_lines);
        }

        private void ReLocation(Control con)
        {
            string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
            con.Width = Convert.ToInt32(Convert.ToSingle(mytag[3]) * con.Parent.Width);
        }
        #endregion

        #region 鼠标事件
        Point LPoint = Point.Empty;
        PointF MoveStep = PointF.Empty;
        private void Chart_MouseDown(object sender, MouseEventArgs e)
        {
            if (DataValues[0].Count <= PointsNum) return;
            int MinX = (int)chart.ChartAreas[0].AxisX.ValueToPixelPosition(chart.ChartAreas[0].AxisX.Minimum);
            int MaxY = (int)chart.ChartAreas[0].AxisY.ValueToPixelPosition(chart.ChartAreas[0].AxisY.Minimum);
            int MaxX = (int)chart.ChartAreas[0].AxisX.ValueToPixelPosition(chart.ChartAreas[0].AxisX.Maximum);
            int MinY = (int)chart.ChartAreas[0].AxisY.ValueToPixelPosition(chart.ChartAreas[0].AxisY.Maximum);
            ///鼠标在表格内部时触发
            if (e.X >= MinX && e.X <= MaxX && e.Y >= MinY && e.Y <= MaxY)
            {
                if (e.Button == MouseButtons.Left)
                {
                    LPoint = e.Location;
                    double value = chart.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                    int index = (int)(SlidingRect.X + ((SlidingRect.Width > PointsNum && !ShowAllFlag) ? (value == 0 ? 0 : value + 1) * (SlidingRect.Width / PointsNum) : value));
                    if (index + 2 < DataValues[0].Count) index += 2;
                    TextGoto?.Invoke(index);
                }
            }
        }

        private void Chart_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && LPoint != Point.Empty && DataValues[0].Count > PointsNum)
            {
                int MinX = (int)chart.ChartAreas[0].AxisX.ValueToPixelPosition(chart.ChartAreas[0].AxisX.Minimum);
                int MaxY = (int)chart.ChartAreas[0].AxisY.ValueToPixelPosition(chart.ChartAreas[0].AxisY.Minimum);
                int MaxX = (int)chart.ChartAreas[0].AxisX.ValueToPixelPosition(chart.ChartAreas[0].AxisX.Maximum);
                int MinY = (int)chart.ChartAreas[0].AxisY.ValueToPixelPosition(chart.ChartAreas[0].AxisY.Maximum);
                Point MousePoint = new Point(e.X, e.Y);
                if (MousePoint.X < MinX) MousePoint.X = MinX;
                else if (MousePoint.X > MaxX) MousePoint.X = MaxX;
                if (MousePoint.Y < MinY) MousePoint.Y = MinY;
                else if (MousePoint.Y > MaxY) MousePoint.Y = MaxY;
                MoveStep.X += (float)(chart.ChartAreas[0].AxisX.PixelPositionToValue(MousePoint.X) - chart.ChartAreas[0].AxisX.PixelPositionToValue(LPoint.X));
                MoveStep.Y += (float)(chart.ChartAreas[0].AxisY.PixelPositionToValue(MousePoint.Y) - chart.ChartAreas[0].AxisY.PixelPositionToValue(LPoint.Y));
                LPoint = MousePoint;
            }
        }

        private void Chart_MouseUp(object sender, MouseEventArgs e)
        {
            //重新记录位置
            if (e.Button == MouseButtons.Left)
            {
                LPoint = Point.Empty;
            }
        }

        ///鼠标缩放
        private float XInterval = 1.0f;
        private float YInterval = 1.0f;
        private float LastXInterval = 1.0f;
        private float LastYInterval = 1.0f;
        private void Chart_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None)
            {
                if (YaxisAuto == 1)
                {
                    YaxisAuto = 0; //设置Y轴缩放模式
                    Panel_Yaixs_Init();
                }
                if (DataValues[0].Count > 0)
                {
                    XInterval *= e.Delta < 0 ? 1.2F : 0.9F;
                    YInterval *= e.Delta < 0 ? 1.2F : 0.9F;
                    //限幅
                    if (YInterval > 10000.0F) YInterval = 10000.0F;
                    else if (YInterval < 0.1F) YInterval = 0.1F; //总共200个点，即最少显示20个点
                    if (XInterval > 10000.0F) XInterval = 10000.0F; //最多显示200万个数据点
                    else if (XInterval < 0.1F) XInterval = 0.1F; //总共200个点，即最少显示20个点
                    if (ShowAllFlag && XInterval > 50.0F) //最多显示1万个数据点
                    {
                        XInterval = 50.0F;
                    }
                }
            }
        }


        private void Chart_Click(object sender, EventArgs e)
        {
            if (!chart.Focused) chart.Focus();
        }

        #endregion

        #region 按键事件

        private void Chart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {

            }
        }


        #endregion

        #region 右键事件
        private bool ShowAllFlag = false;
        private void 全部显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAllFlag = ShowAllFlag ? false : true;
            //if(XaxisAuto)
            //{
            //    XaxisAuto = false;
            //    panel_Xaixs_MouseLeave(null, null);
            //}
            if (ShowAllFlag) //最多显示1万个数据点
            {
                if(XInterval > 50.0F) XInterval = 50.0F;
                全部显示ToolStripMenuItem.Text = "间隔显示";
            }
            else
            {
                全部显示ToolStripMenuItem.Text = "全部显示";
            }
            SpecialUpdate = true;
        }
        #endregion

        #region 窗体关闭
        private void FormChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TimerShow.Enabled) TimerShow.Stop();
        }

        #endregion


    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Runtime.InteropServices;

namespace Cute
{
     partial class FormBasic : System.Windows.Forms.Form
    {
        private readonly DataBuffer buffer = new DataBuffer(); //数据接收缓存
        private readonly DataSave dataSave = new DataSave(); //数据存储
        Paraments param = new Paraments();
        /// <summary>
        /// 发送数据
        /// </summary>
        public Func<byte[], bool> SendByteBuffer;
        public Func<string, bool> SendStrBuffer;
        public Action ClearSendBytes;
        /// <summary>
        /// 编码格式
        /// </summary>
        private int TextEncoding = 0;
        /// <summary>
        /// 已发送字节数
        /// </summary>
        private int HadSendBytes = 0;
        /// <summary>
        /// 接收速率
        /// </summary>
        private int RecByteSpd = 0;
        /// <summary>
        /// 1ms最大发送长度
        /// </summary>
        private float MaxLenth_1Ms = 1;
        private AccurateTimer timer_send = new AccurateTimer(1);
        /// <summary>
        /// 发送内容追加
        /// </summary>
        private string SendTextAdd = string.Empty;
        public FormBasic()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; //允许线程访问控件
            SetTag(this);
            timer_send.Tick += Timer_send_Tick;
            comboBox_Add.SelectedIndex = 0;
            //#region 颜色重置
            //splitContainer.BackColor = param.Desktop_FunctionBack;
            //groupBox_Send.BackColor = groupBox_Rec.BackColor = param.Basic_Back;
            //param.ColorSetNormalModule(this, param.Basic_Font);
            //param.ColorSetButton(this, param.Basic_ButtonBack, param.Basic_ButtonFont);
            //textBox_dt.BackColor = textBox_Rec.BackColor = textBox_Send.BackColor = param.Basic_TextBack;
            //textBox_dt.ForeColor = textBox_Rec.ForeColor = textBox_Send.ForeColor = param.Basic_TextFore;
            //#endregion
        }


        #region 控件大小随窗体大小等比例缩放
        private void SetTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                //颜色配置
                if(con as Button != null)
                {
                    Button button = con as Button;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 2;
                    button.FlatAppearance.BorderColor = param.Basic_NormalBorder;
                    button.ForeColor = param.Basic_Font;
                    button.FlatAppearance.MouseOverBackColor = param.Desktop_FunctionPre;
                    button.FlatAppearance.MouseDownBackColor = param.C_Blue_2;
                }
                else if(con as ComboBox != null)
                {
                    ComboBox box = con as ComboBox;
                    box.FlatStyle = FlatStyle.Flat;
                    box.BackColor = param.Basic_Back;
                    box.ForeColor = param.Basic_Font;
                }

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
            this.Size = size;
            string[] mytag = splitContainer.Tag.ToString().Split(new char[] { ';' });
            splitContainer.Width = Convert.ToInt32(Convert.ToSingle(mytag[3]) * splitContainer.Parent.Width);
            splitContainer.Height = Convert.ToInt32(Convert.ToSingle(mytag[4]) * splitContainer.Parent.Height);
            float ratio = splitContainer.Height / 931;
            splitContainer.Panel1MinSize = (int)(splitContainer.Panel2MinSize * ratio);
            splitContainer.Panel2MinSize = (int)(splitContainer.Panel2MinSize * ratio);
            splitContainer.SplitterDistance = 549 * splitContainer.Height / 931;
            SetControls(groupBox_Send);
            SetControls(groupBox_Rec);
            Hide();
        }
        #endregion

        #region 数据处理
        /// <summary>
        /// 接收新数据
        /// </summary>
        /// <param name="data"></param>
        public void DataReceived(byte[] data)
        {
            buffer.AddendData(data);
        }

        /// <summary>
        /// 数据源关闭
        /// </summary>
        public void SourseClose()
        {
            RecByteSpd = 0;
            RecMsgTitleShow();
        }

        private int LastShowTime = 0;
        /// <summary>
        /// 接收数据处理
        /// </summary>
        public void DataHandler(List<object> arg)
        {
            TextEncoding = (int)arg.ElementAt(0);
            RecByteSpd = (int)arg.ElementAt(1);
            HadSendBytes = (int)arg.ElementAt(2);
            MaxLenth_1Ms = (float)arg.ElementAt(3);

            //接收数据显示
            if (buffer.HasChanged())
            {
                //16进制显示
                if (checkBox_RecHex.Checked)
                {
                    RecMsgShow(new Common().Data_ToHex(buffer.GetNewData(-1)));
                }
                //10进制显示
                else
                {
                    switch (TextEncoding)
                    {
                        case 0:
                            RecMsgShow(System.Text.Encoding.ASCII.GetString(buffer.GetNewData(-1)));
                            break;
                        case 1:
                            RecMsgShow(System.Text.Encoding.UTF8.GetString(buffer.GetNewData(-1)));
                            break;
                        case 2:
                            RecMsgShow(System.Text.Encoding.Unicode.GetString(buffer.GetNewData(-1)));
                            break;
                    }
                }
            }

            //10fps显示标题栏
            int NowTime = System.Environment.TickCount;
            if (NowTime - LastShowTime > 100)
            {
                RecMsgTitleShow();
                SendMsgTitleShow();
                LastShowTime = NowTime;
            }
        }

        /// <summary>
        /// 进制转换时接收区文本变换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_RecHex_CheckedChanged(object sender, EventArgs e)
        {
            if (System.Environment.TickCount - LastShowTime < 200) return; //串口开启时不进行转换
            textBox_Rec.Text = "格式转换中，请稍后...";
            var result = new StringBuilder();
            //16进制显示
            if (checkBox_RecHex.Checked)
            {
                result.Append(new Common().Data_ToHex(buffer.GetData(0, buffer.DataLenth())));
            }
            //10进制显示
            else
            {
                byte[] data = buffer.GetData(0, buffer.DataLenth());
                switch (TextEncoding)
                {
                    case 0:
                        result.Append(System.Text.Encoding.ASCII.GetString(data));
                        break;
                    case 1:
                        result.Append(System.Text.Encoding.UTF8.GetString(data));
                        break;
                    case 2:
                        result.Append(System.Text.Encoding.Unicode.GetString(data));
                        break;
                }
            }
            textBox_Rec.Text = string.Empty;
            RecMsgShow(result.ToString());
        }

        /// <summary>
        /// 串口发送字符串
        /// </summary>
        private bool SerialSendData(string data)
        {
            bool SendStatus = true;
            if (checkBox_HexSend.Checked)
            {
                byte[] send = new Common().Hex_ToHex(data);
                if (send != null)
                {
                    SendStatus = (bool)(SendByteBuffer?.Invoke(send));
                }
            }
            else
            {
                SendStatus = (bool)(SendStrBuffer?.Invoke(data));
            }

            if (checkBox_AutoClear.Checked)  //发送完自动清空
                textBox_Send.Clear();
            return SendStatus;
        }

        #region 定时发送

        /// <summary>
        /// 开关定时发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_TimerSend_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_TimerSend.Checked)
            {
                string SendText = textBox_Send.Text + SendTextAdd;
                int dt = ReCalculateTime(SendText, textBox_dt.Text);
                timer_send.Interval = dt;
                textBox_dt.Text = Convert.ToString(dt);
                if (!timer_send.running)
                    timer_send.Start();
            }
            else if (timer_send.running)
            {
                timer_send.Stop();
            }
        }

        /// <summary>
        /// 定时发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_send_Tick(object sender)
        {
            if (textBox_Send.Text != string.Empty && !SerialSendData(textBox_Send.Text + SendTextAdd))
            {
                if (timer_send.running)
                {
                    checkBox_TimerSend.Checked = false;
                    timer_send.Stop(); //关闭该定时器及线程，该语句后内容都将不会执行
                }
            }
        }

        /// <summary>
        /// 更改定时发送时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_dt_TextChanged(object sender, EventArgs e)
        {
            int dt = ReCalculateTime(textBox_Send.Text, textBox_dt.Text);
            if(dt == 0)
            {
                checkBox_TimerSend.Checked = false;
                if(timer_send.running) timer_send.Stop();
            }
            else
            {
                timer_send.Interval = dt;
                textBox_dt.Text = Convert.ToString(dt);
            }
        }
        /// <summary>
        /// 发送区改变 更改定时发送时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Send_TextChanged(object sender, EventArgs e)
        {
            textBox_Send.Font = new Font("宋体", 12, FontStyle.Regular, GraphicsUnit.Point);
            if (checkBox_TimerSend.Checked)
            {
                try
                {
                    int dt = ReCalculateTime(textBox_Send.Text, textBox_dt.Text);
                    if (dt == 0 && timer_send.running)
                    {
                        checkBox_TimerSend.Checked = false;
                        timer_send.Stop();
                    }
                    else if (Convert.ToInt32(textBox_dt.Text) < dt)
                    {
                        timer_send.Interval = dt;
                        textBox_dt.Text = Convert.ToString(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "定时发送");
                }

            }
        }

        /// <summary>
        /// 根据设置间隔重新计算最大理论间隔时间
        /// </summary>
        public int ReCalculateTime(string Text, string setTimeMs)
        {
            float dtmin = 0;

            if (MaxLenth_1Ms != 0)
            {
                switch (TextEncoding)
                {
                    case 0:
                        dtmin = (float)System.Text.Encoding.ASCII.GetByteCount(Text) / MaxLenth_1Ms; //计算最小间隔时间 ms
                        break;
                    case 1:
                        dtmin = (float)System.Text.Encoding.UTF8.GetByteCount(Text) / MaxLenth_1Ms; //计算最小间隔时间 ms
                        break;
                    case 2:
                        dtmin = (float)System.Text.Encoding.Unicode.GetByteCount(Text) / MaxLenth_1Ms; //计算最小间隔时间 ms
                        break;
                }
            }
            dtmin = dtmin >= 1 ? dtmin : 1;
            try
            {
                int dt = 0;
                if (setTimeMs.Length > 0) dt = Convert.ToInt32(setTimeMs);
                else return 0;
                if (dt < dtmin) dt = (int)dt + 1; //向上取整
                return dt;
            }
            catch
            {
                if (setTimeMs.Length != 0)
                    MessageBox.Show("请输入整型数字");
            }

            return (int)dtmin;
        }
        #endregion

        #endregion

        #region 文本显示
        /// <summary>
        /// 接收区安全显示
        /// </summary>
        /// <param name="str"></param>
        public void RecMsgShow(string str)
        {
            if (textBox_Rec.InvokeRequired)
            {
                Action<string> action = RecMsgShow;
                textBox_Rec.Invoke(action, str);
            }
            else
            {
                textBox_Rec.AppendText(str);
                textBox_Rec.SelectionStart = textBox_Rec.TextLength;
                textBox_Rec.ScrollToCaret();
            }
        }

        /// <summary>
        /// 接收区标题栏显示
        /// </summary>
        private void RecMsgTitleShow()
        {
            if (label_RecTitle.InvokeRequired)
            {
                Action action = RecMsgTitleShow;
                label_RecTitle.Invoke(action);
            }
            else
            {
                label_RecTitle.Text = string.Format("共接收 {0} 字节，速度 {1} 字节/秒", buffer.DataLenth(), RecByteSpd);
            }
        }

        /// <summary>
        /// 发送区标题栏显示
        /// </summary>
        private void SendMsgTitleShow()
        {
            if (label_SendTitle.InvokeRequired)
            {
                Action action = SendMsgTitleShow;
                label_SendTitle.Invoke(action);
            }
            else
            {
                label_SendTitle.Text = string.Format("共发送 {0} 字节", HadSendBytes);
            }
        }
        #endregion

        #region 鼠标事件

        private void comboBox_Add_DropDownClosed(object sender, EventArgs e)
        {
            switch (comboBox_Add.SelectedIndex)
            {
                case 0:
                    SendTextAdd = string.Empty;break;
                case 1:
                    SendTextAdd = "\r";break;
                case 2:
                    SendTextAdd = "\n"; break;
                case 3:
                    SendTextAdd = "\r\n"; break;
                case 4:
                    SendTextAdd = "\n\r"; break;
                case 5:
                    SendTextAdd = "\r\n\r\n"; break;
            }
        }

        /// <summary>
        /// 访问超链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Send_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }


        /// <summary>
        /// 点击单次发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Send_Click(object sender, EventArgs e)
        {
            string SendText = textBox_Send.Text + SendTextAdd;
            if (SerialSendData(SendText))
            {
                //保存发送内容
                SendTextHistory textHistory = new SendTextHistory();
                textHistory.AppEndText(CuteMode.Basic, SendText);
                HistoryTime = string.Empty;
                HistoryText = null;
            }
        }

        /// <summary>
        /// 清空接收区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ClearRec_Click(object sender, EventArgs e)
        {
            buffer.ClearData();
            textBox_Rec.Clear();
            RecByteSpd = 0;
            RecMsgTitleShow();
        }

        /// <summary>
        /// 清空发送区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ClearSend_Click(object sender, EventArgs e)
        {
            textBox_Send.Clear();
            ClearSendBytes?.Invoke();
            HadSendBytes = 0;
            SendMsgTitleShow();
        }

        #endregion

        #region 发送框历史内容回溯

        /// <summary>
        /// 历史发送内容回溯
        /// </summary>
        string HistoryTime = string.Empty;
        string HistoryText = null;
        private void textBox_Send_KeyDown(object sender, KeyEventArgs e)
        {
            //调出历史值
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                SendTextHistory textHistory = new SendTextHistory();
                string[] str = textHistory.FindText(CuteMode.Basic, HistoryTime, e.KeyCode);
                if (str != null)
                {
                    if (str[0] == string.Empty)
                    {
                        if (HistoryText != null)
                        {
                            textBox_Send.Text = HistoryText;
                            e.Handled = true; //消除按键提示音
                        }
                            HistoryTime = string.Empty;
                        HistoryText = null;
                    }
                    else
                    {
                        e.Handled = true; //消除按键提示音
                        if (HistoryText == null) HistoryText = textBox_Send.Text;
                        HistoryTime = str[0]; //历史发送时间
                        //textBox_Send.Text = "[" + str[0] + "]\r\n" + str[1];
                        textBox_Send.Text = str[1];
                    }
                }
                textBox_Send.Select(textBox_Send.TextLength, 0); //设置光标到末尾
            }
        }
        #endregion

        #region 文本存储

        private void button_Save_Click(object sender, EventArgs e)
        {
            string path = dataSave.choose_path("文本文档(*.txt)|*.txt");
            if (path != string.Empty)
            {
                Task saveTask = Task.Run(() => 
                {
                    SetCursor(Cursors.WaitCursor);
                    dataSave.Run(path, buffer.GetData(0, buffer.DataLenth()), checkBox_RecHex.Checked);
                    SetCursor(Cursors.Arrow);
                });
            }
        }

        private void SetCursor(Cursor cursor)
        {
            if(this.InvokeRequired)
            {
                Action<Cursor> action = SetCursor;
                this.Invoke(action, cursor);
            }
            else
            {
                Cursor = cursor;
            }
        }
        #endregion

        #region 重画事件
        /// <summary>
        /// 接收区、发送区重画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupBox_Paint(object sender, PaintEventArgs e)
        {
            Paraments param = new Paraments();
            GroupBox groupBox = sender as GroupBox;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;  //抗锯齿
            e.Graphics.Clear(groupBox.BackColor);

            //重画边框
            e.Graphics.DrawLine(new Pen(param.Basic_NormalBorder, 2), new Point(0, 1), new Point(groupBox.Width, 1));
            e.Graphics.DrawLine(new Pen(param.Basic_NormalBorder, 2), new Point(0, groupBox.Height - 2), new Point(groupBox.Width, groupBox.Height - 2));

            //画标题框
            GraphicsPath wordGp = new GraphicsPath();
            Rectangle wordRect = new Rectangle(new Point((int)(panel_Rec.Left * 0.4F), (int)groupBox.Font.Size), new Size(panel_Rec.Left, (int)groupBox.Font.Size));
            wordGp.AddRectangle(wordRect);
            wordGp.CloseAllFigures();
            e.Graphics.FillPath(new HatchBrush(HatchStyle.Percent10, param.Basic_NormalTitle, groupBox.BackColor), wordGp);

            //写标题
            e.Graphics.DrawString(groupBox.Text, groupBox.Font, new SolidBrush(param.Basic_NormalTitle), new PointF(0, groupBox.Font.Size / 2));
        }
        #endregion

        #region 窗体关闭

        public void FunctionClose()
        {
            if (timer_send.running) timer_send.Stop();
        }

        #endregion


    }
}

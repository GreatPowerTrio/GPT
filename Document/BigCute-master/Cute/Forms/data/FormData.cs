using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace Cute
{
    public partial class FormData : Form
    {
        public FormChart _FormChart = new FormChart();
        private Paraments param = new Paraments();
        private readonly DataBuffer buffer = new DataBuffer(); //数据接收缓存
        private List<List<float>> Data = new List<List<float>>(); //解析后数据按帧存放
        private readonly DataSave dataSave = new DataSave(); //数据存储
        int HadShowTextEndRow = 0; //文本框显示末尾
        int SetShowTextEndRow = 0;
        /// <summary>
        /// 发送数据
        /// </summary>
        public Func<byte[], bool> SendByteBuffer;
        public Func<string, bool> SendStrBuffer;

        private int HadSendFrame = 0; //已发送帧数
        private int LastShowTime = 0;

        public FormData()
        {
            InitializeComponent();
            SetTag(this);
            checkBox_Update.Checked = true;
            textBox_Rec.MouseWheel += TextBox_Rec_MouseWheel;
            //#region 颜色重置
            //splitContainer.BackColor = param.Desktop_FunctionBack;
            //groupBox_Send.BackColor = groupBox_Rec.BackColor = param.Data_Back;
            //param.ColorSetNormalModule(this, param.Data_Font);
            //param.ColorSetButton(this, param.Data_ButtonBack, param.Data_ButtonFont);
            //textBox_Rec.BackColor = textBox_Send.BackColor = param.Data_TextBack;
            //textBox_Rec.ForeColor = textBox_Send.ForeColor = param.Data_TextFore;
            //#endregion
        }


        #region 控件大小随窗体大小等比例缩放
        private void SetTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                //颜色配置
                if (con as Button != null)
                {
                    Button button = con as Button;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 2;
                    button.FlatAppearance.BorderColor = param.Data_Border;
                    button.ForeColor = param.Data_Font;
                    button.FlatAppearance.MouseOverBackColor = param.Desktop_FunctionPre;
                    button.FlatAppearance.MouseDownBackColor = param.C_Blue_2;
                }
                else if (con as ComboBox != null)
                {
                    ComboBox box = con as ComboBox;
                    box.FlatStyle = FlatStyle.Flat;
                    box.BackColor = param.Data_Back;
                    box.ForeColor = param.Data_Font;
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
            SetControls(splitContainer.Panel1);
            SetControls(splitContainer.Panel2);
            Hide();
            _FormChart.TopLevel = false;
            _FormChart.Parent = panel_up;
            _FormChart.Anchor = (AnchorStyles)15;
            _FormChart.Size = panel_up.Size;
            _FormChart.Action_Clear = ClearRec;
            _FormChart.Action_Save = DataSave;
            _FormChart.Action_Import = DataImport;
            _FormChart.TextGoto = TextBoxRec_Goto;
            _FormChart.Text = "数据收发--虚拟示波器";
            _FormChart.Form_Changed(panel_up.Size);
        }

        private void FormData_Shown(object sender, EventArgs e)
        {
            _FormChart.Show();
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
        /// 数据处理
        /// </summary>
        /// <param name="arg"></param>
        public void DataHandler(List<object> arg)
        {
            if (buffer.HasChanged())
            {
                ///数据处理
                var newdata = buffer.GetDeleteData(-1);
                var datas = Unpack(newdata);

                //波形数据更新
                foreach (var nums in datas)
                {
                    _FormChart.AppendData(nums);
                    Data.Add(nums);
                }
                ///设置文本框显示终止行
                if(checkBox_Update.Checked) SetShowTextEndRow = Data.Count;
            }

            //20fps更新文本区
            int NowTime = System.Environment.TickCount;
            if (NowTime - LastShowTime > 50)
            {
                ///标题更新
                RecMsgTitleShow();
                ///文本更新
                if (HadShowTextEndRow != SetShowTextEndRow)
                    RecMsgUpdate();

                LastShowTime = NowTime;
            }
        }
        #endregion

        #region 协议解包

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
            if(check_lenth == 0)
            {
                int sofindex = databuffer.IndexOf(sof);
                if(sofindex != -1)
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
            if(check_lenth == 1 && databuffer.Count > 1)
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
            if(check_lenth == 2 && databuffer.Count > 2)
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
            if(check_lenth >= 3 && databuffer.Count > 3 + data_size * data_count)
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
                    foreach(List<byte> singleframe in frames)
                    {
                        if (data_type == 0x0f) //浮点型
                        {
                            dataframe.Add(BitConverter.ToSingle(singleframe.ToArray(), 0));
                        }
                        else if((data_type & 0x08) == 0) //无符号型
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

        #region 文本、标题栏显示
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
                //自动显示最底端
                textBox_Rec.SelectionStart = textBox_Rec.TextLength;
                textBox_Rec.ScrollToCaret();
            }
        }

        /// <summary>
        /// 文本框刷新
        /// </summary>
        /// <param name="endindex"><=0则显示最新</param>
        private void RecMsgUpdate()
        {
            if (textBox_Rec.InvokeRequired)
            {
                Action action = RecMsgUpdate;
                textBox_Rec.Invoke(action);
            }
            else
            {
                int startindex, end;
                end = SetShowTextEndRow > Data.Count ? Data.Count : SetShowTextEndRow;
                int rows = (int)((textBox_Rec.Height) / (textBox_Rec.Font.Height + textBox_Rec.Font.Size));
                startindex = end - rows;
                if (startindex < 0)
                {
                    if (Data.Count > rows) end = rows;
                    else end = Data.Count;
                    startindex = 0;
                }
                var result = new StringBuilder();
                for(int i = startindex; i < end; i++)
                {
                    result.Append(string.Format("[{0}]", i));
                    foreach (var num in Data[i])
                    {
                        result.Append(string.Format(" {0:N7}", num));
                    }
                    result.Append("\r\n");
                }
                textBox_Rec.Text = result.ToString();
                textBox_Rec.SelectionStart = textBox_Rec.TextLength;
                textBox_Rec.ScrollToCaret();
                HadShowTextEndRow = end;
            }
        }
        int ff;
        /// <summary>
        /// 鼠标点击设置文本显示行
        /// </summary>
        /// <param name="EndRow"></param>
        private void TextBoxRec_Goto(int EndRow)
        {
            checkBox_Update.Checked = false;
            SetShowTextEndRow = EndRow;
            int NowTime = System.Environment.TickCount;
            if (NowTime - LastShowTime > 100)
            {
                ///标题更新
                RecMsgTitleShow();
                ///文本更新
                RecMsgUpdate();
            }
        }

        /// <summary>
        /// 关闭串口时鼠标滑动显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Rec_MouseWheel(object sender, MouseEventArgs e)
        {
            checkBox_Update.Checked = false;
            int err = (int)(textBox_Rec.Height / (textBox_Rec.Font.Height + textBox_Rec.Font.Size) / 2 + 1);
            SetShowTextEndRow += e.Delta > 0 ? -err : err;
            int NowTime = System.Environment.TickCount;
            if (NowTime - LastShowTime > 100)
            {
                ///标题更新
                RecMsgTitleShow();
                ///文本更新
                RecMsgUpdate();
            }
        }

        /// <summary>
        /// 大小改变时更新文本框内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Rec_SizeChanged(object sender, EventArgs e)
        {
            RecMsgUpdate();
        }

        /// <summary>
        /// 接收区标题显示
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
                label_RecTitle.Text = string.Format("共接收 {0} 数据帧", Data.Count);
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
                label_SendTitle.Text = string.Format("共发送 {0} 数据帧", HadSendFrame);
            }
        }

        private void textBox_Send_TextChanged(object sender, EventArgs e)
        {
            textBox_Send.Font = new Font("宋体", 12, FontStyle.Regular, GraphicsUnit.Point);
        }

        #endregion

        #region 鼠标事件

        /// <summary>
        /// 点击单次发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Send_Click(object sender, EventArgs e)
        {
            bool SendStatus = true;
            if (textBox_Send.TextLength == 0) return;
            if (checkBox_HexSend.Checked)
            {
                byte[] send = new Common().Hex_ToHex(textBox_Send.Text);
                if (send != null)
                {
                    SendStatus = (bool)(SendByteBuffer?.Invoke(send));
                }
            }
            else
            {
                SendStatus = (bool)(SendStrBuffer?.Invoke(textBox_Send.Text));
            }

            //发送成功，则保存发送数据
            if (SendStatus)
            {
                HadSendFrame++;
                SendMsgTitleShow();
                SendTextHistory textHistory = new SendTextHistory();
                textHistory.AppEndText(CuteMode.Data, textBox_Send.Text);
                HistoryTime = string.Empty;
                HistoryText = null;
            }
        }

        /// <summary>
        /// 清空发送区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ClearSend_Click(object sender, EventArgs e)
        {
            textBox_Send.Clear();
            HadSendFrame = 0;
            SendMsgTitleShow();
        }

        /// <summary>
        /// 清空接收区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ClearRec_Click(object sender, EventArgs e)
        {
            ClearRec();
        }

        private void ClearRec()
        {
            buffer.ClearData();
            Data.Clear();
            SetShowTextEndRow = HadShowTextEndRow = 0;
            _FormChart.Clear();
            textBox_Rec.Clear();
            RecMsgTitleShow();
        }

        /// <summary>
        /// 保存数据到txt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Save_Click(object sender, EventArgs e)
        {
            DataSave();
        }

        private void DataSave()
        {
            string path = dataSave.choose_path("文本文档(*.txt)|*.txt");
            if(path != string.Empty)
            {
                Task saveTask = Task.Run(() =>
                {
                    SetCursor(Cursors.WaitCursor);
                    dataSave.Run(path, Data);
                    SetCursor(Cursors.Arrow);
                });
            }
        }

        private void DataImport()
        {
            using (OpenFileDialog file = new OpenFileDialog()
            {
                Title = "path",
                Filter = "CSV文件 (*.csv)|*.csv|文本文档(*.txt)|*.txt",
            })
            {
                if (file.ShowDialog() == DialogResult.OK)
                {
                    string path = Path.GetFullPath(file.FileName);
                    file.InitialDirectory = Path.GetDirectoryName(path);
                    file.FileName = Path.GetFileName(path);
                    using (StreamReader sr = new StreamReader(path))
                    {
                        try
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                string[] rowData = line.Split(new char[] { ' ', ',', '，', '\t' }, StringSplitOptions.RemoveEmptyEntries); ; // 使用逗号分隔符分割每行数据
                                List<float> newdata = new List<float>();
                                foreach (string str in rowData)
                                {
                                    newdata.Add(float.Parse(str));
                                }
                                _FormChart.AppendData(newdata);
                                Data.Add(newdata);
                            }
                            ///设置文本框显示终止行
                            if (checkBox_Update.Checked) SetShowTextEndRow = Data.Count;
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("所有单元格需为浮点型或整型且每列个数相等", "文件格式错误");
                        }
                    }
                }
            }
        }

        private void SetCursor(Cursor cursor)
        {
            if (this.InvokeRequired)
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

        #region 按键事件
        string HistoryTime = string.Empty;
        string HistoryText = null;
        private void textBox_Send_KeyDown(object sender, KeyEventArgs e)
        {
            //调出历史值
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                SendTextHistory textHistory = new SendTextHistory();
                string[] str = textHistory.FindText(CuteMode.Data, HistoryTime, e.KeyCode);
                if(str != null)
                {
                    if(str[0] == string.Empty)
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
                        HistoryTime = str[0];
                        textBox_Send.Text = "[" + str[0] + "]\r\n" + str[1];
                    }
                }
                textBox_Send.Select(textBox_Send.TextLength, 0); //设置光标到末尾
            }
        }

        #endregion

        #region 重画事件        
        
        private void panel_up_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            //重画边框
            e.Graphics.DrawLine(new Pen(param.Data_Border, 2), new Point(0, 1), new Point(panel.Width, 1));
            e.Graphics.DrawLine(new Pen(param.Data_Border, 2), new Point(0, panel.Height - 1), new Point(panel.Width, panel.Height - 1));
        }

        private void groupBox_Paint(object sender, PaintEventArgs e)
        {
            GroupBox groupBox = sender as GroupBox;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;  //抗锯齿
            e.Graphics.Clear(groupBox.BackColor);

            //重画边框
            if (groupBox.Name.Contains("Send"))
            {
                e.Graphics.DrawLine(new Pen(param.Data_Border, 1), new Point(0, 0), new Point(groupBox.Width, 0));
                e.Graphics.DrawLine(new Pen(param.Data_Border, 2), new Point(0, groupBox.Height - 1), new Point(groupBox.Width, groupBox.Height - 1));
            }
            else
            {
                e.Graphics.DrawLine(new Pen(param.Data_Border, 2), new Point(0, 1), new Point(groupBox.Width, 1));
            }
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


    }
}

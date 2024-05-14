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

namespace Cute
{
     partial class FormSerialComBaud : System.Windows.Forms.Form
    {
        public Paraments param = new Paraments();
        public bool isOpen { get; set; }
        public bool Running { get; set; }

        public Func<int> GetEncoding; //获取编码格式
        public Func<List<string>> GetConfiguration; //获取串口配置信息
        public event Action SerialClose;
        /// <summary>
        /// 发送队列
        /// </summary>
        private readonly DataBuffer SendBuffer = new DataBuffer();
        /// <summary>
        /// 发送字节数
        /// </summary>
        private int SendBytes { get; set; }
        /// <summary>
        /// 接收数据回调
        /// </summary>
        public event Action<byte[]> DataReceived;
        /// <summary>
        /// 数据处理
        /// </summary>
        public event Action<List<object>> DataHandler;
        /// <summary>
        /// 接收字节速率
        /// </summary>
        private int RecByteSpeed { get; set; }
        private int RecByteTemp { get; set; }

        public FormSerialComBaud()
        {
            InitializeComponent();
            SetTag(this);
            //BackColor = param.Desktop_FunctionBack;
            //label_com.ForeColor = label_baudrate.ForeColor = param.Desktop_Font;
            //comboBox_baudrate.ForeColor = comboBox_com.ForeColor = button_openSerial.ForeColor = param.Desktop_ModuleFont;
            //comboBox_baudrate.BackColor = comboBox_com.BackColor = button_openSerial.BackColor = param.Desktop_ModuleBack;
        }
        private void FormSerialComBaud_Load(object sender, EventArgs e)
        {
            ComUpdate();
            if (comboBox_com.Items.Count != 0) comboBox_com.SelectedIndex = 0;
            comboBox_baudrate.Text = "115200";
        }


        /// <summary>
        /// 下拉列表后串口号更新一次
        /// </summary>
        private void comboBox_com_DropDown(object sender, EventArgs e)
        {
            ComUpdate();
        }

        private void button_openSerial_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (Cursor == Cursors.WaitCursor) return;
            Cursor = Cursors.WaitCursor;

            if (button.Text == "打开串口")
            {
                button.Text = "开启中";
                string com = comboBox_com.Text;
                if (com == null)
                {
                    //串口出错
                    SerialError();
                    return;
                }

                Task open = Task.Run(() =>
                {
                    try
                    {
                        List<string> configs = GetConfiguration.Invoke();
                        SerialPort serialPort = new SerialPort();
                        serialPort.DataReceived += serialPort_DataReceived;
                        serialPort.PortName = com;
                        serialPort.BaudRate = Convert.ToInt32(comboBox_baudrate.Text);
                        serialPort.Parity = (Parity)System.Enum.Parse(typeof(Parity), configs[0]);
                        serialPort.DataBits = Convert.ToInt32(configs[1]);
                        serialPort.StopBits = (StopBits)System.Enum.Parse(typeof(StopBits), configs[2]);
                        serialPort.Open();

                        //串口正常开启
                        if (serialPort.IsOpen)
                        {
                            //串口刚打开执行函数
                            SerialOpening();

                            //开启1s定时器 更新接收速率
                            System.Threading.Timer timer_1s = null;
                            timer_1s = new System.Threading.Timer(timer =>
                            {
                                if (isOpen)
                                {
                                    RecByteSpeed = RecByteTemp;
                                    RecByteTemp = 0;
                                }
                                else
                                {
                                    timer_1s.Dispose();
                                }
                            }, timer_1s, 0, 1000);

                            //开启新线程 数据发送
                            Task SerialSendTask = Task.Run(() =>
                            {
                                int SleepTime = 1;
                                while (isOpen)
                                {
                                    try
                                    {
                                        if (SendBuffer.DataLenth() > 0)
                                        {
                                            byte[] data = SendBuffer.GetDeleteData(-1);
                                            serialPort.Write(data, 0, data.Length);
                                            SendBytes += data.Length;
                                            SleepTime = 1;
                                        }
                                        else
                                        {
                                            System.Threading.Thread.Sleep(SleepTime); //节省CPU资源
                                            SleepTime = SleepTime == 100 ? SleepTime : SleepTime++;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        isOpen = false;
                                        MessageBox.Show(ex.Message, "串口已断开");
                                    }
                                }
                            });

                            //计算1ms最大发送长度
                            float maxLenth = (float)serialPort.BaudRate / (serialPort.DataBits + Convert.ToInt32(serialPort.StopBits) + 1) / 1000;
                            Task DataHandlerTask = Task.Run(() =>
                            {
                                //死循环等待串口关闭
                                while (isOpen)
                                {
                                    int encode = GetEncoding.Invoke();
                                    DataHandler?.Invoke(new List<object>
                                    {
                                        encode,
                                        RecByteSpeed,
                                        SendBytes,
                                        maxLenth,
                                    });
                                    System.Threading.Thread.Sleep(50);
                                }
                            });

                            //等待发送线程发送完毕
                            while (!SerialSendTask.IsCompleted || !DataHandlerTask.IsCompleted)
                            {
                                System.Threading.Thread.Sleep(50); //节省CPU
                            }

                            serialPort.Close();

                            //串口关闭后
                            SerialClosed();
                            Running = false;
                            isOpen = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        //串口出错
                        SerialError();
                        isOpen = false;
                        MessageBox.Show(ex.Message, "打开串口");
                    }
                });
            }
            else if(button.Text == "关闭串口")
            {
                isOpen = false;
            }
        }

        #region 串口事件
        /// <summary>
        /// 串口刚打开时操作
        /// </summary>
        public void SerialOpening()
        {
            Cursor = Cursors.Arrow;
            button_openSerial.Text = "关闭串口";
            isOpen = true;
            Running = true;
        }

        /// <summary>
        /// 串口出错
        /// </summary>
        public void SerialError()
        {
            Cursor = Cursors.Arrow;
            button_openSerial.Text = "打开串口";
        }

        /// <summary>
        /// 串口关闭后
        /// </summary>
        public void SerialClosed()
        {
            Cursor = Cursors.Arrow;
            button_openSerial.Text = "打开串口";
            SerialClose?.Invoke();
        }

        /// <summary>
        /// 串口接收回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = sender as SerialPort;
            byte[] newdata = new byte[serialPort.BytesToRead];
            if(serialPort.IsOpen)
            {
                int lenth = serialPort.Read(newdata, 0, newdata.Length);
                RecByteTemp += lenth;
                DataReceived?.BeginInvoke(newdata, null, null);
            }
        }

        /// <summary>
        /// 串口号更新
        /// </summary>
        private void ComUpdate()
        {
            string[] SrcNames = SerialPort.GetPortNames(); //添加所有可用串口
             List<string> PortNames = new List<string>();
            foreach (string str in SrcNames)
            {
                if (!PortNames.Contains(str)) PortNames.Add(str);
            }

            if (PortNames.Count == comboBox_com.Items.Count)
            {
                foreach (object obj in comboBox_com.Items)
                {
                    if (!PortNames.Contains((string)obj))
                    {
                        comboBox_com.Items.Clear();
                        comboBox_com.Items.AddRange(PortNames.ToArray());
                        break;
                    }
                }
            }
            else
            {
                string com = comboBox_com.Text;
                comboBox_com.Items.Clear();
                comboBox_com.Items.AddRange(PortNames.ToArray());
                if (comboBox_com.Items.Contains(com))
                    comboBox_com.Text = com;
            }
        }
        #endregion

        #region 发送数据添加

        /// <summary>
        /// 清除发送数据个数记录
        /// </summary>
        public void ClearSendBytes()
        {
            SendBytes = 0;
        }

        /// <summary>
        /// 添加数据到发送队列
        /// </summary>
        /// <param name="buff"></param>
        public bool AddSendBuffer(string buff)
        {
            if (isOpen)
            {
                int encode = GetEncoding.Invoke();
                switch (encode)
                {
                    case 0:
                        SendBuffer.AddendData(Encoding.ASCII.GetBytes(buff));
                        break;
                    case 1:
                        SendBuffer.AddendData(Encoding.UTF8.GetBytes(buff));
                        break;
                    case 2:
                        SendBuffer.AddendData(Encoding.Unicode.GetBytes(buff));
                        break;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 添加数据到发送队列
        /// </summary>
        /// <param name="buff"></param>
        public bool AddSendBuffer(byte[] buff)
        {
            if (isOpen)
            {
                SendBuffer.AddendData(buff);
                return true;
            }
            return false;
        }

        #endregion

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
                    button.BackColor = param.Desktop_MenuBack;
                    button.ForeColor = param.Data_Font;
                    button.FlatAppearance.MouseOverBackColor = param.Desktop_FunctionPre;
                    button.FlatAppearance.MouseDownBackColor = param.C_Blue_2;
                }
                else if (con as ComboBox != null)
                {
                    ComboBox box = con as ComboBox;
                    box.FlatStyle = FlatStyle.Flat;
                    box.BackColor = param.Desktop_MenuBack;
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

        private void SetControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                con.Left = Convert.ToInt32(Convert.ToSingle(mytag[1]) * con.Parent.Width);
                con.Top = Convert.ToInt32(Convert.ToSingle(mytag[2]) * con.Parent.Height);
                con.Width = Convert.ToInt32(Convert.ToSingle(mytag[3]) * con.Parent.Width);
                con.Height = Convert.ToInt32(Convert.ToSingle(mytag[4]) * con.Parent.Height);
                //con.Font = new Font(con.Font.Name, con.Font.Size * (newx > newy ? newy : newx), con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    SetControls(newx, newy, con);
                }
            }
        }

        /// <summary>
        /// 第一次显示窗体时控件位置调整
        /// </summary>
        public void Form_Changed(float newx, float newy)
        {
            Size = new Size((int)(Width * newx), (int)(Height * newy));
            SetControls(newx, newy, this);
            Show();
        }
        #endregion

        #region 关闭界面
        public void FunctionClose()
        {
            isOpen = false;
        }
        #endregion


    }
}

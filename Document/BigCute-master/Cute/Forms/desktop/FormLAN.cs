using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
namespace Cute
{
    public partial class FormLAN : Form
    {
        public Func<int> GetEncoding; //获取编码格式
        public Func<string> GetLocalIP; //获取本地IP
        public bool IsOpen = false; //局域网是否已经开启
        public bool Running { get; set; } //是否正在运行 只能内部设置，供外部读取标志位
        public string LAN_Mode = string.Empty;  //通信方式
        Paraments param = new Paraments();
        /// <summary>
        /// 发送字节数
        /// </summary>
        private int SendBytes { get; set; }
        /// <summary>
        /// 接收字节速率
        /// </summary>
        private int RecByteSpeed { get; set; }
        private int RecByteTemp { get; set; }
        /// <summary>
        /// 接收数据回调
        /// </summary>
        public event Action<byte[]> DataReceived;
        /// <summary>
        /// 数据处理
        /// </summary>
        public event Action<List<object>> DataHandler;
        /// <summary>
        /// 发送队列
        /// </summary>
        private readonly DataBuffer SendBuffer = new DataBuffer();
        /// <summary>
        /// 临时客户端信息
        /// </summary>
        private Hashtable clientTable = new Hashtable(30);

        public FormLAN()
        {
            InitializeComponent();
            SetTag(this);
            Running = false;
            //BackColor = param.Desktop_FunctionBack;
            //label_baudrate.ForeColor = label_RemoteIP.ForeColor = param.Desktop_Font;
            //textBox_Port.ForeColor = comboBox_RemoteIP.ForeColor = button_Start.ForeColor = param.Desktop_ModuleFont;
            //textBox_Port.BackColor = comboBox_RemoteIP.BackColor = button_Start.BackColor = param.Desktop_ModuleBack;
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
        public void Form_Changed(float newx, float newy)
        {
            Size = new Size((int)(Width * newx), (int)(Height * newy));
            SetControls(this);
            Hide();
        }

        #endregion

        #region comboBox远程端点更改
        private void ComboBox_RemoteIP_Add(EndPoint ep)
        {
            if (comboBox_RemoteIP.InvokeRequired)
            {
                Action<EndPoint> action = ComboBox_RemoteIP_Add;
                comboBox_RemoteIP.Invoke(action, ep);
            }
            else
            {
                comboBox_RemoteIP.Items.Add(ep);
            }
        }

        private void ComboBox_RemoteIP_Delete(EndPoint ep)
        {
            if (comboBox_RemoteIP.InvokeRequired)
            {
                Action<EndPoint> action = ComboBox_RemoteIP_Delete;
                comboBox_RemoteIP.Invoke(action, ep);
            }
            else
            {
                if (comboBox_RemoteIP.Items.Contains(ep))
                    comboBox_RemoteIP.Items.Remove(ep);
                if (comboBox_RemoteIP.Text == ep.ToString()) 
                    comboBox_RemoteIP.Text = "";
            }
        }
        #endregion

        #region 开启定时器 接收速率更新
        private void RecSpeedTimerStart()
        {
            System.Threading.Timer timer_1s = null;
            timer_1s = new System.Threading.Timer(timer =>
            {
                if (IsOpen)
                {
                    RecByteSpeed = RecByteTemp;
                    RecByteTemp = 0;
                }
                else
                {
                    timer_1s.Dispose();
                }
            }, timer_1s, 0, 1000);
        }
        #endregion

        #region TCP UDP 通信

        private void button_Start_Click(object sender, EventArgs e)
        {
            if (button_Start.Text == "断开连接")
            {
                IsOpen = false;
            }
            else if(button_Start.Text == "打开连接")
            {
                if (LAN_Mode == "TCP Server")
                {
                    TCPServerOpen();
                }
                else if (LAN_Mode == "TCP Client")
                {
                    TCPClientConnect();
                }
                else if (LAN_Mode == "UDP")
                {
                    UDPClientConnect();
                }
            }
        }

        #region TCP Server
        private Socket TCPServer;
        public void TCPServerOpen()
        {
            int Port = 0;
            try
            {
                Port = int.Parse(textBox_Port.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Port输入错误");
                return;
            }

            button_Start.Text = "连接中";
            Task task = Task.Run(() =>
            {
                try
                {
                    //初始化SOCKET实例
                    TCPServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //允许SOCKET被绑定在已使用的地址上。
                    TCPServer.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    //绑定到指定IP，Port口上
                    string LocalAddress = GetLocalIP.Invoke();
                    TCPServer.Bind(new IPEndPoint(LocalAddress == "Any" ? IPAddress.Any : IPAddress.Parse(LocalAddress), Port));
                    //监听
                    TCPServer.Listen(30);
                    //开始接受连接，异步。
                    TCPServer.BeginAccept(new AsyncCallback(OnConnectRequest), TCPServer);
                    //服务器已开启
                    button_Start.Text = "断开连接";
                    IsOpen = true;
                    Running = true;

                    //开启1s定时器 更新接收速率
                    RecSpeedTimerStart();

                    //开启数据处理线程
                    Task DataHandlerTask = Task.Run(() =>
                    {
                        while (IsOpen)
                        {
                            DataHandler?.Invoke(new List<object>
                            {
                                GetEncoding.Invoke(),
                                RecByteSpeed,
                                SendBytes,
                                (float)0.0F, //无上限
                            });
                            System.Threading.Thread.Sleep(50); //20fps
                        }
                    });

                    int SleepTime = 1;
                    while (IsOpen)
                    {
                        if (SendBuffer.DataLenth() > 0 && comboBox_RemoteIP.Items.Count > 0)
                        {
                            byte[] data = SendBuffer.GetDeleteData(-1);
                            if (comboBox_RemoteIP.SelectedItem != null)
                            {
                                TCP_Server_Send(data, (EndPoint)comboBox_RemoteIP.SelectedItem);
                            }
                            else
                            {
                                for (int i = 0; i < comboBox_RemoteIP.Items.Count; i++)
                                {
                                    if (i < comboBox_RemoteIP.Items.Count)
                                    {
                                        EndPoint ep = (EndPoint)comboBox_RemoteIP.Items[i];
                                        TCP_Server_Send(data, (EndPoint)ep);
                                    }
                                }
                            }
                            SendBytes += data.Length;
                            SleepTime = 1;
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(SleepTime); //节省CPU资源
                            SleepTime = SleepTime == 100 ? SleepTime : SleepTime++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "开启服务器失败");
                }
                IsOpen = false;
                TCPServer.Close();
                TCPServer.Dispose();
                button_Start.Text = "打开连接";
                Running = false;
            });
        }

        private void OnConnectRequest(IAsyncResult ar)
        {
            try
            {
                if (!IsOpen) return;
                ///初始化一个SOCKET，用于其它客户端的连接
                Socket client = TCPServer.EndAccept(ar);
                EndPoint remote = client.RemoteEndPoint;
                ///设置超时时间2s
                client.ReceiveTimeout = 2000;
                ///将客户端点添加到哈希表中
                clientTable.Add(remote, client);
                ///添加到combox组件
                ComboBox_RemoteIP_Add(remote);
                ///等待新的客户端连接
                TCPServer.BeginAccept(new AsyncCallback(OnConnectRequest), TCPServer);
                
                int errflag = 0;
                ///服务器未关闭
                ///连接正常
                while (IsOpen && !(client.Poll(1000, SelectMode.SelectRead) && client.Available == 0))
                {
                    try
                    {
                        byte[] newdata = new byte[1024];
                        int lenth = client.Receive(newdata);
                        DataReceived.Invoke(newdata.Take(lenth).ToArray());
                        RecByteTemp += lenth;
                    }
                    catch { }
                }
                clientTable.Remove(remote);
                ComboBox_RemoteIP_Delete(remote);
                client.Close();
                client.Dispose();
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message, "客户端异常");
                //重新开始异步连接
                //TCPServer.BeginAccept(new AsyncCallback(OnConnectRequest), TCPServer);
            }
        }

        /// <summary>
        /// 发送
        /// </summary>
        private void TCP_Server_Send(byte[] data, EndPoint ep)
        {
            try
            {
                if (ep != null && clientTable.ContainsKey(ep))
                {
                    Socket socket = (Socket)clientTable[ep];
                    socket.SendTo(data, ep);
                }
            }
            catch { }
        }
        #endregion

        #region TCP Client

        Socket TCPClient;
        private void TCPClientConnect()
        {
            int Port = 0;
            try
            {
                Port = int.Parse(textBox_Port.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Port输入错误");
                return;
            }

            IPAddress address;
            try
            {
                address = IPAddress.Parse(comboBox_RemoteIP.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RemoteAddress输入错误");
                return;
            }

            button_Start.Text = "连接中";
            Task connect = Task.Run(() =>
            {
                TCPClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    TCPClient.Connect(new IPEndPoint(address, Port));
                    button_Start.Text = "断开连接";
                    IsOpen = true;
                    Running = true;
                    //开启1s定时器 更新接收速率
                    RecSpeedTimerStart();
                    //开启数据处理线程
                    Task DataHandlerTask = Task.Run(() =>
                    {
                        while (IsOpen)
                        {
                            DataHandler?.Invoke(new List<object>
                            {
                                GetEncoding.Invoke(),
                                RecByteSpeed,
                                SendBytes,
                                (float)0.0F, //无上限
                            });
                            System.Threading.Thread.Sleep(50); //20fps
                        }
                    });
                    //开启发送线程
                    Task SendTask = Task.Run(() =>
                    {
                        int SleepTime = 1;
                        while (IsOpen)
                        {
                            try
                            {
                                if (SendBuffer.DataLenth() > 0)
                                {
                                    byte[] data = SendBuffer.GetDeleteData(-1);
                                    TCPClient.Send(data);
                                    SendBytes += data.Length;
                                    SleepTime = 1;
                                }
                                else
                                {
                                    System.Threading.Thread.Sleep(SleepTime); //节省CPU资源
                                    SleepTime = SleepTime == 100 ? SleepTime : SleepTime++;
                                }
                            }
                            catch { IsOpen = false; }
                        }
                    });

                    TCP_ClientListenning();
                }
                catch (SocketException ex)
                {
                    //System.Threading.Thread.Sleep(1000); //1s后重新访问
                    //TCPServerConnect();  //一直访问直至连接
                    MessageBox.Show(ex.Message, "连接服务器失败");
                }
                IsOpen = false;
                TCPClient.Dispose();
                button_Start.Text = "打开连接";
                Running = false;
            });
        }

        private void TCP_ClientListenning()
        {
            TCPClient.ReceiveTimeout = 2000; //设置超时时间2s

            ///客户端打开
            ///连接正常
            while (IsOpen && !(TCPClient.Poll(1000, SelectMode.SelectRead) && TCPClient.Available == 0))
            {
                try
                {
                    byte[] newdata = new byte[1024];
                    int lenth = TCPClient.Receive(newdata, 1024, SocketFlags.None);
                    DataReceived.Invoke(newdata.Take(lenth).ToArray());
                    RecByteTemp += lenth;
                }
                catch { }
            }
            if (IsOpen) MessageBox.Show(string.Format("与服务器 {0} 的连接已断开", TCPClient.RemoteEndPoint), "连接服务器异常");
        }

        #endregion

        #region UDP

        private void UDPClientConnect()
        {
            int Port = 0;
            try
            {
                Port = int.Parse(textBox_Port.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Port输入错误");
                return;
            }

            IPAddress address;
            try
            {
                address = IPAddress.Parse(comboBox_RemoteIP.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RemoteAddress输入错误");
                return;
            }

            button_Start.Text = "连接中";
            Task task = Task.Run(() =>
            {
                try
                {
                    UdpClient udpClient = new UdpClient(Port); 
                    button_Start.Text = "断开连接";
                    IsOpen = true;
                    Running = true;
                    //开启1s定时器 更新接收速率
                    RecSpeedTimerStart();
                    //开启接收线程
                    Task RecTask = Task.Run(() =>
                    {
                        while (IsOpen)
                        {
                            try
                            {
                                IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
                                byte[] newdata = udpClient.Receive(ref ep);
                                DataReceived.Invoke(newdata);
                                RecByteTemp += newdata.Length;
                            }
                            catch { }
                        }
                    });

                    //开启数据处理线程
                    Task DataHandlerTask = Task.Run(() =>
                    {
                        while (IsOpen)
                        {
                            DataHandler?.Invoke(new List<object>
                            {
                                GetEncoding.Invoke(),
                                RecByteSpeed,
                                SendBytes,
                                (float)0.0F, //无上限
                            });
                            System.Threading.Thread.Sleep(50); //20fps
                        }
                    });

                    int SleepTime = 1;
                    //发送循环
                    while (IsOpen)
                    {
                        try
                        {
                            if (SendBuffer.DataLenth() > 0)
                            {
                                try
                                {
                                    Port = int.Parse(textBox_Port.Text); 
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Port输入错误");
                                    continue;
                                }
                                byte[] data = SendBuffer.GetDeleteData(-1);
                                udpClient.Send(data, data.Length, new IPEndPoint(address, Port));
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
                            MessageBox.Show(ex.Message, "Udp Send");
                        }
                    }
                    udpClient.Close();
                }
                catch(SocketException ex)
                {
                    MessageBox.Show(ex.Message, "开启UDP失败");
                }
                IsOpen = false;
                button_Start.Text = "打开连接";
                Running = false;
            });

        }
        #endregion

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
            if (IsOpen)
            {
                switch (GetEncoding.Invoke())
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
            if (IsOpen)
            {
                SendBuffer.AddendData(buff);
                return true;
            }
            return false;
        }

        #endregion

        #region 窗口关闭
        public void FunctionClose()
        {
            IsOpen = false;
        }
        #endregion
    }
}

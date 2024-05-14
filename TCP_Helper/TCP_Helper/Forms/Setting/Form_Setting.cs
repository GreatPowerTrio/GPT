using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace TCP_Helper.Forms.Setting
{
	public partial class Form_Setting : Form
	{
		public string LAN_Mode = string.Empty;  //通信方式
		/*public Func<int> GetEncoding; //获取编码格式  跨窗口委托时使用 Func函数委托 */
		
		public bool IsOpen = false; //局域网是否已经开启
		public bool Running { get; set; } //是否正在运行 只能内部设置，供外部读取标志位
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
		public Form_Setting()
		{
			InitializeComponent();
			Running = false;
			TXComboBox.Items.Add("TCP Server");
			TXComboBox.Items.Add("TCP Client");
			TXComboBox.Items.Add("UDP");
		}
		#region comboBox远程端点更改
		private void ComboBox_RemoteIP_Add(EndPoint ep)
		{
			if (RemoteIPComboBox.InvokeRequired)
			{
				Action<EndPoint> action = ComboBox_RemoteIP_Add;
				RemoteIPComboBox.Invoke(action, ep);
			}
			else
			{
				RemoteIPComboBox.Items.Add(ep);
			}
		}

		private void ComboBox_RemoteIP_Delete(EndPoint ep)
		{
			if (RemoteIPComboBox.InvokeRequired)
			{
				Action<EndPoint> action = ComboBox_RemoteIP_Delete;
				RemoteIPComboBox.Invoke(action, ep);
			}
			else
			{
				if (RemoteIPComboBox.Items.Contains(ep))
					RemoteIPComboBox.Items.Remove(ep);
				if (RemoteIPComboBox.Text == ep.ToString())
					RemoteIPComboBox.Text = "";
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
		private void BtnConnect_Click(object sender, EventArgs e)
		{
			if (BtnConnect.Text == "断开连接")
			{
				IsOpen = false;
			}
			else if (BtnConnect.Text == "开始连接")
			{
				Console.WriteLine(TXComboBox.Text);
				if (TXComboBox.Text == "TCP Server")
				{
					TCPServerOpen();
				}
				else if (TXComboBox.Text == "TCP Client")
				{
					TCPClientConnect();
				}
				else if (TXComboBox.Text == "UDP")
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
				Port = int.Parse(PortBox.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Port输入错误");
				return;
			}
			BtnConnect.Text = "连接中";
			Task task = Task.Run(() =>
			{
				try
				{
					//初始化SOCKET实例
					TCPServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					//允许SOCKET被绑定在已使用的地址上。
					TCPServer.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
					//绑定到指定IP，Port口上
					string LocalAddress = ThisIPComboBox.Text;
					TCPServer.Bind(new IPEndPoint(LocalAddress == "Any" ? IPAddress.Any : IPAddress.Parse(LocalAddress), Port));
					//监听
					TCPServer.Listen(30);
					//开始接受连接，异步。
					TCPServer.BeginAccept(new AsyncCallback(OnConnectRequest), TCPServer);
					//服务器已开启
					this.Invoke((MethodInvoker)delegate
					{
						BtnConnect.Text = "断开连接";
						BtnConnect.FillColor = Color.Red;
					});
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
								GetEncoding(),
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
						if (SendBuffer.DataLenth() > 0 && RemoteIPComboBox.Items.Count > 0)
						{
							byte[] data = SendBuffer.GetDeleteData(-1);
							if (RemoteIPComboBox.SelectedItem != null)
							{
								TCP_Server_Send(data, (EndPoint)RemoteIPComboBox.SelectedItem);
							}
							else
							{
								for (int i = 0; i < RemoteIPComboBox.Items.Count; i++)
								{
									if (i < RemoteIPComboBox.Items.Count)
									{
										EndPoint ep = (EndPoint)RemoteIPComboBox.Items[i];
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
				this.Invoke((MethodInvoker)delegate {
					BtnConnect.Text = "开始连接";
					BtnConnect.FillColor = Color.Blue;
				});
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
				Port = int.Parse(PortBox.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Port输入错误");
				return;
			}

			IPAddress address;
			try
			{
				address = IPAddress.Parse(RemoteIPComboBox.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "RemoteAddress输入错误");
				return;
			}

			BtnConnect.Text = "连接中";
			Task connect = Task.Run(() =>
			{
				TCPClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				try
				{
					TCPClient.Connect(new IPEndPoint(address, Port));
					this.Invoke((MethodInvoker)delegate
					{
						BtnConnect.Text = "断开连接";
						BtnConnect.FillColor = Color.Red;
					});
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
								GetEncoding(),	 //加东西
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
				this.Invoke((MethodInvoker)delegate {
					BtnConnect.Text = "开始连接";
					BtnConnect.FillColor = Color.Blue;
				});
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
				Port = int.Parse(PortBox.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Port输入错误");
				return;
			}

			IPAddress address;
			try
			{
				address = IPAddress.Parse(RemoteIPComboBox.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "RemoteAddress输入错误");
				return;
			}

			BtnConnect.Text = "连接中";
			Task task = Task.Run(() =>
			{
				try
				{
					UdpClient udpClient = new UdpClient(Port);
					this.Invoke((MethodInvoker)delegate
					{
						BtnConnect.Text = "断开连接";
						BtnConnect.FillColor = Color.Red;
					});
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
								GetEncoding(),
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
									Port = int.Parse(PortBox.Text);
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
				catch (SocketException ex)
				{
					MessageBox.Show(ex.Message, "开启UDP失败");
				}
				IsOpen = false;
				this.Invoke((MethodInvoker)delegate {
					BtnConnect.Text = "开始连接";
					BtnConnect.FillColor = Color.Blue;
				});
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
				switch (GetEncoding())
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


		#region 本地IP地址获取
		private void ThisIPComboBox_DropDown(object sender, EventArgs e)
		{
			try
			{
				UIComboBox box = sender as UIComboBox; //获取当前下拉框
				string text = box.Text; 
				box.Items.Clear();
				box.Items.Add("Any");
				IPAddress[] IPs = Dns.GetHostEntry(Dns.GetHostName()).AddressList; //获取本机所有IP
				foreach (IPAddress ip in IPs)
				{
					if (ip.AddressFamily.ToString().Equals("InterNetwork")) box.Items.Add(ip.ToString()); //填充下拉框加入本机IP
				}
				box.SelectedItem = text;
			}
			catch (SocketException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		#region 编码格式设置


		public int GetEncoding()
		{
			if (EncodingBox.InvokeRequired)
			{
				return (int)EncodingBox.Invoke(new Func<int>(() => GetEncoding()));
			}
			else
			{
				try
				{
					return EncodingBox.SelectedIndex != -1 ? EncodingBox.SelectedIndex : 1;
				}
				catch
				{
					return 1;
				}
			}
		}

		#endregion

	}
}

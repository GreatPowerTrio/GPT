using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project
{
	public partial class Form1 : Form
	{

		private string ServerIP; //IP
		private int port;   //端口
		private bool isExit = false;
		private TcpClient client;
		NetworkStream stream;


		public Form1()
		{
			InitializeComponent();
			/*			lst_State.HorizontalScrollbar = true;
						btn_Stop.Enabled = false;*/
			SetServerIPAndPort();

		}


		/// <summary>
		/// 根据当前程序目录的文本文件‘ServerIPAndPort.txt’内容来设定IP和端口
		/// 格式：127.0.0.1:8885
		/// </summary>
		private void SetServerIPAndPort()
		{
			try
			{
				FileStream fs = new FileStream("ServerIPAndPort.txt", FileMode.Open);
				StreamReader sr = new StreamReader(fs);
				string IPAndPort = sr.ReadLine();
				ServerIP = IPAndPort.Split(':')[0]; //设定IP
				port = int.Parse(IPAndPort.Split(':')[1]); //设定端口
				sr.Close();
				fs.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("配置IP与端口失败，错误原因：" + ex.Message);
				//Application.Exit();
			}
		}


		/// <summary>
		/// 处理服务器信息
		/// </summary>
		private void ReceiveData()
		{
			// 接收数据
			byte[] data = new byte[256];
			int bytes = stream.Read(data, 0, data.Length);
			textBox2.Text = Encoding.ASCII.GetString(data, 0, bytes);
		}

		/// <summary>
		/// 向服务端发送消息
		/// </summary>
		/// <param name="message"></param>
		private void SendMessage(string message)
		{
			try {
				byte[] data = Encoding.ASCII.GetBytes(textBox1.Text);
				stream.Write(data, 0, data.Length);
			}
			catch (Exception ex) { MessageBox.Show("发送失败，错误原因：" + ex.Message); }
		}


		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				client = new TcpClient(ServerIP, port);
				stream = client.GetStream();
				Thread threadReceive = new Thread(new ThreadStart(ReceiveData));
				threadReceive.IsBackground = true;
				threadReceive.Start();
			}
			catch 
			{
				MessageBox.Show("连接失败");
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try { SendMessage(textBox1.Text); }
			catch (Exception ex) { MessageBox.Show("发送失败，错误原因：" + ex.Message); }
			
			
		}

		private void button3_Click(object sender, EventArgs e)
		{
			try { ReceiveData(); }
			catch (Exception ex) { MessageBox.Show("接收失败，错误原因：" + ex.Message); }
		}
	}
}


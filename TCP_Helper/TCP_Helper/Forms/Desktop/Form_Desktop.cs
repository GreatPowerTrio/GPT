using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCP_Helper.Forms.Chart;
using TCP_Helper.Forms.Home;
using TCP_Helper.Forms.Setting;

namespace TCP_Helper
{
	public partial class Form_Desktop : MetroForm
	{
		//创建子窗体
		Form_Home _Form_Home = new Form_Home();
		Form_Chart _Form_Chart = new Form_Chart();
		Form_Setting _Form_Setting = new Form_Setting();

		//当前子窗体标志位
		private bool is_Home_Open = true;
		private bool is_Chart_Open = false;
		private bool is_Setting_Open = false;
		



		public Form_Desktop()
		{
			InitializeComponent();
			OpenForm(_Form_Home);

			#region 窗体属性设置


			#endregion

			#region 回调函数设置
			_Form_Setting.DataReceived += DataReceived;
			_Form_Setting.DataHandler += DataHandler;
			#endregion
		}
		#region 子窗体切换
		private void OpenForm(Form frm)
		{
			frm.TopLevel = false;
			frm.TopMost = false;
			this.Form_Panel.Controls.Clear();
			this.Form_Panel.Controls.Add(frm);
			frm.Show();
		}

		private void is_Form_Open(Form frm)
		{
			if (frm == _Form_Home)
			{
				is_Home_Open = true;
				is_Chart_Open = false;
				is_Setting_Open = false;
			}
			else if (frm == _Form_Chart)
			{
				is_Home_Open = false;
				is_Chart_Open = true;
				is_Setting_Open = false;
			}
			else if (frm == _Form_Setting)
			{
				is_Home_Open = false;
				is_Chart_Open = false;
				is_Setting_Open = true;
			}
		}

		private void BtnHome_Click(object sender, EventArgs e)
		{
			OpenForm(_Form_Home);
			is_Form_Open(_Form_Home);
		}

		private void BtnChart_Click(object sender, EventArgs e)
		{
			OpenForm(_Form_Chart);
			is_Form_Open(_Form_Chart);
		}

		private void BtnSetting_Click(object sender, EventArgs e)
		{
			OpenForm(_Form_Setting);
			is_Form_Open(_Form_Setting);
		}
		#endregion

		#region 数据整体调控
		/// <summary>
		/// 数据接收分配
		/// </summary>
		/// <param name="buff"></param>
		private void DataReceived(byte[] buff)
		{
			
			_Form_Chart.DataReceived(buff);
			/*if(is_Chart_Open)
			{
				_Form_Chart.DataReceived(buff);
			}*/
		}

		private void DataHandler(List<object> arg)
		{
			_Form_Chart.DataHandler(arg);
/*			if (is_Chart_Open)
			{
				_Form_Chart.DataHandler(arg);
			}*/
		}
		#endregion


	}
}

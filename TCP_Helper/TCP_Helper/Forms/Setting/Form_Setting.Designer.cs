namespace TCP_Helper.Forms.Setting
{
	partial class Form_Setting
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.RemoteIP_Label = new Sunny.UI.UILabel();
			this.Port_Label = new Sunny.UI.UILabel();
			this.ThisIP_Label = new Sunny.UI.UILabel();
			this.RemoteIPComboBox = new Sunny.UI.UIComboBox();
			this.ThisIPComboBox = new Sunny.UI.UIComboBox();
			this.BtnConnect = new Sunny.UI.UIButton();
			this.PortBox = new Sunny.UI.UITextBox();
			this.uiLabel1 = new Sunny.UI.UILabel();
			this.TXComboBox = new Sunny.UI.UIComboBox();
			this.uiLabel2 = new Sunny.UI.UILabel();
			this.EncodingBox = new Sunny.UI.UIComboBox();
			this.DebugTB = new Sunny.UI.UITextBox();
			this.SuspendLayout();
			// 
			// RemoteIP_Label
			// 
			this.RemoteIP_Label.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.RemoteIP_Label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
			this.RemoteIP_Label.Location = new System.Drawing.Point(291, 175);
			this.RemoteIP_Label.Name = "RemoteIP_Label";
			this.RemoteIP_Label.Size = new System.Drawing.Size(100, 23);
			this.RemoteIP_Label.TabIndex = 0;
			this.RemoteIP_Label.Text = "远程IP";
			this.RemoteIP_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Port_Label
			// 
			this.Port_Label.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Port_Label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
			this.Port_Label.Location = new System.Drawing.Point(291, 214);
			this.Port_Label.Name = "Port_Label";
			this.Port_Label.Size = new System.Drawing.Size(100, 23);
			this.Port_Label.TabIndex = 1;
			this.Port_Label.Text = "端口";
			this.Port_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ThisIP_Label
			// 
			this.ThisIP_Label.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ThisIP_Label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
			this.ThisIP_Label.Location = new System.Drawing.Point(290, 253);
			this.ThisIP_Label.Name = "ThisIP_Label";
			this.ThisIP_Label.Size = new System.Drawing.Size(100, 23);
			this.ThisIP_Label.TabIndex = 2;
			this.ThisIP_Label.Text = "本机IP";
			this.ThisIP_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// RemoteIPComboBox
			// 
			this.RemoteIPComboBox.DataSource = null;
			this.RemoteIPComboBox.FillColor = System.Drawing.Color.White;
			this.RemoteIPComboBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.RemoteIPComboBox.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.RemoteIPComboBox.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
			this.RemoteIPComboBox.Location = new System.Drawing.Point(397, 172);
			this.RemoteIPComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.RemoteIPComboBox.MinimumSize = new System.Drawing.Size(63, 0);
			this.RemoteIPComboBox.Name = "RemoteIPComboBox";
			this.RemoteIPComboBox.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
			this.RemoteIPComboBox.Size = new System.Drawing.Size(150, 29);
			this.RemoteIPComboBox.SymbolSize = 24;
			this.RemoteIPComboBox.TabIndex = 3;
			this.RemoteIPComboBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.RemoteIPComboBox.Watermark = "";
			// 
			// ThisIPComboBox
			// 
			this.ThisIPComboBox.DataSource = null;
			this.ThisIPComboBox.FillColor = System.Drawing.Color.White;
			this.ThisIPComboBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ThisIPComboBox.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.ThisIPComboBox.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
			this.ThisIPComboBox.Location = new System.Drawing.Point(397, 250);
			this.ThisIPComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ThisIPComboBox.MinimumSize = new System.Drawing.Size(63, 0);
			this.ThisIPComboBox.Name = "ThisIPComboBox";
			this.ThisIPComboBox.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
			this.ThisIPComboBox.Size = new System.Drawing.Size(150, 29);
			this.ThisIPComboBox.SymbolSize = 24;
			this.ThisIPComboBox.TabIndex = 4;
			this.ThisIPComboBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.ThisIPComboBox.Watermark = "";
			this.ThisIPComboBox.DropDown += new System.EventHandler(this.ThisIPComboBox_DropDown);
			// 
			// BtnConnect
			// 
			this.BtnConnect.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BtnConnect.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnConnect.Location = new System.Drawing.Point(293, 317);
			this.BtnConnect.MinimumSize = new System.Drawing.Size(1, 1);
			this.BtnConnect.Name = "BtnConnect";
			this.BtnConnect.Size = new System.Drawing.Size(254, 45);
			this.BtnConnect.TabIndex = 5;
			this.BtnConnect.Text = "开始连接";
			this.BtnConnect.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
			// 
			// PortBox
			// 
			this.PortBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.PortBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.PortBox.Location = new System.Drawing.Point(397, 211);
			this.PortBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.PortBox.MinimumSize = new System.Drawing.Size(1, 16);
			this.PortBox.Name = "PortBox";
			this.PortBox.Padding = new System.Windows.Forms.Padding(5);
			this.PortBox.ShowText = false;
			this.PortBox.Size = new System.Drawing.Size(150, 29);
			this.PortBox.TabIndex = 1;
			this.PortBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.PortBox.Watermark = "";
			// 
			// uiLabel1
			// 
			this.uiLabel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
			this.uiLabel1.Location = new System.Drawing.Point(291, 135);
			this.uiLabel1.Name = "uiLabel1";
			this.uiLabel1.Size = new System.Drawing.Size(100, 23);
			this.uiLabel1.TabIndex = 6;
			this.uiLabel1.Text = "通信协议";
			this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TXComboBox
			// 
			this.TXComboBox.DataSource = null;
			this.TXComboBox.FillColor = System.Drawing.Color.White;
			this.TXComboBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.TXComboBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.TXComboBox.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.TXComboBox.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
			this.TXComboBox.Location = new System.Drawing.Point(398, 133);
			this.TXComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.TXComboBox.MinimumSize = new System.Drawing.Size(63, 0);
			this.TXComboBox.Name = "TXComboBox";
			this.TXComboBox.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
			this.TXComboBox.Size = new System.Drawing.Size(150, 29);
			this.TXComboBox.SymbolSize = 24;
			this.TXComboBox.TabIndex = 4;
			this.TXComboBox.Tag = "";
			this.TXComboBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.TXComboBox.Watermark = "";
			// 
			// uiLabel2
			// 
			this.uiLabel2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
			this.uiLabel2.Location = new System.Drawing.Point(291, 96);
			this.uiLabel2.Name = "uiLabel2";
			this.uiLabel2.Size = new System.Drawing.Size(100, 23);
			this.uiLabel2.TabIndex = 7;
			this.uiLabel2.Text = "编码方式";
			this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// EncodingBox
			// 
			this.EncodingBox.DataSource = null;
			this.EncodingBox.FillColor = System.Drawing.Color.White;
			this.EncodingBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.EncodingBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.EncodingBox.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
			this.EncodingBox.Items.AddRange(new object[] {
            "ASCII",
            "UTF8",
            "Unicode"});
			this.EncodingBox.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
			this.EncodingBox.Location = new System.Drawing.Point(398, 94);
			this.EncodingBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.EncodingBox.MinimumSize = new System.Drawing.Size(63, 0);
			this.EncodingBox.Name = "EncodingBox";
			this.EncodingBox.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
			this.EncodingBox.Size = new System.Drawing.Size(150, 29);
			this.EncodingBox.SymbolSize = 24;
			this.EncodingBox.TabIndex = 5;
			this.EncodingBox.Tag = "";
			this.EncodingBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.EncodingBox.Watermark = "";
			// 
			// DebugTB
			// 
			this.DebugTB.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.DebugTB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.DebugTB.Location = new System.Drawing.Point(143, 396);
			this.DebugTB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.DebugTB.MinimumSize = new System.Drawing.Size(1, 16);
			this.DebugTB.Name = "DebugTB";
			this.DebugTB.Padding = new System.Windows.Forms.Padding(5);
			this.DebugTB.ShowText = false;
			this.DebugTB.Size = new System.Drawing.Size(602, 50);
			this.DebugTB.TabIndex = 1;
			this.DebugTB.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.DebugTB.Watermark = "";
			// 
			// Form_Setting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Snow;
			this.ClientSize = new System.Drawing.Size(910, 580);
			this.Controls.Add(this.DebugTB);
			this.Controls.Add(this.EncodingBox);
			this.Controls.Add(this.uiLabel2);
			this.Controls.Add(this.TXComboBox);
			this.Controls.Add(this.uiLabel1);
			this.Controls.Add(this.PortBox);
			this.Controls.Add(this.BtnConnect);
			this.Controls.Add(this.ThisIPComboBox);
			this.Controls.Add(this.RemoteIPComboBox);
			this.Controls.Add(this.ThisIP_Label);
			this.Controls.Add(this.Port_Label);
			this.Controls.Add(this.RemoteIP_Label);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Form_Setting";
			this.Text = "Form_Setting";
			this.ResumeLayout(false);

		}

		#endregion

		private Sunny.UI.UILabel RemoteIP_Label;
		private Sunny.UI.UILabel Port_Label;
		private Sunny.UI.UILabel ThisIP_Label;
		private Sunny.UI.UIComboBox RemoteIPComboBox;
		private Sunny.UI.UIComboBox ThisIPComboBox;
		private Sunny.UI.UIButton BtnConnect;
		private Sunny.UI.UITextBox PortBox;
		private Sunny.UI.UILabel uiLabel1;
		private Sunny.UI.UIComboBox TXComboBox;
		private Sunny.UI.UILabel uiLabel2;
		private Sunny.UI.UIComboBox EncodingBox;
		private Sunny.UI.UITextBox DebugTB;
	}
}
namespace TCP_Helper
{
	partial class Form_Desktop
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.Select_Panel = new MetroFramework.Controls.MetroPanel();
			this.BtnMoreFunction = new System.Windows.Forms.Button();
			this.BtnSetting = new System.Windows.Forms.Button();
			this.BtnChart = new System.Windows.Forms.Button();
			this.BtnHome = new System.Windows.Forms.Button();
			this.Form_Panel = new System.Windows.Forms.Panel();
			this.Select_Panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// Select_Panel
			// 
			this.Select_Panel.BackColor = System.Drawing.Color.Yellow;
			this.Select_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Select_Panel.Controls.Add(this.BtnMoreFunction);
			this.Select_Panel.Controls.Add(this.BtnSetting);
			this.Select_Panel.Controls.Add(this.BtnChart);
			this.Select_Panel.Controls.Add(this.BtnHome);
			this.Select_Panel.HorizontalScrollbarBarColor = true;
			this.Select_Panel.HorizontalScrollbarHighlightOnWheel = false;
			this.Select_Panel.HorizontalScrollbarSize = 10;
			this.Select_Panel.Location = new System.Drawing.Point(0, 53);
			this.Select_Panel.Name = "Select_Panel";
			this.Select_Panel.Size = new System.Drawing.Size(200, 580);
			this.Select_Panel.TabIndex = 0;
			this.Select_Panel.VerticalScrollbarBarColor = true;
			this.Select_Panel.VerticalScrollbarHighlightOnWheel = false;
			this.Select_Panel.VerticalScrollbarSize = 10;
			// 
			// BtnMoreFunction
			// 
			this.BtnMoreFunction.BackColor = System.Drawing.Color.White;
			this.BtnMoreFunction.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnMoreFunction.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnMoreFunction.Location = new System.Drawing.Point(0, 237);
			this.BtnMoreFunction.Name = "BtnMoreFunction";
			this.BtnMoreFunction.Size = new System.Drawing.Size(200, 80);
			this.BtnMoreFunction.TabIndex = 5;
			this.BtnMoreFunction.Text = "更多功能";
			this.BtnMoreFunction.UseVisualStyleBackColor = false;
			// 
			// BtnSetting
			// 
			this.BtnSetting.BackColor = System.Drawing.Color.White;
			this.BtnSetting.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnSetting.Location = new System.Drawing.Point(0, 158);
			this.BtnSetting.Name = "BtnSetting";
			this.BtnSetting.Size = new System.Drawing.Size(200, 80);
			this.BtnSetting.TabIndex = 4;
			this.BtnSetting.Text = "设置";
			this.BtnSetting.UseVisualStyleBackColor = false;
			this.BtnSetting.Click += new System.EventHandler(this.BtnSetting_Click);
			// 
			// BtnChart
			// 
			this.BtnChart.BackColor = System.Drawing.Color.White;
			this.BtnChart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnChart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnChart.Location = new System.Drawing.Point(0, 79);
			this.BtnChart.Name = "BtnChart";
			this.BtnChart.Size = new System.Drawing.Size(200, 80);
			this.BtnChart.TabIndex = 3;
			this.BtnChart.Text = "波形";
			this.BtnChart.UseVisualStyleBackColor = false;
			this.BtnChart.Click += new System.EventHandler(this.BtnChart_Click);
			// 
			// BtnHome
			// 
			this.BtnHome.BackColor = System.Drawing.Color.White;
			this.BtnHome.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.BtnHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnHome.Location = new System.Drawing.Point(0, 0);
			this.BtnHome.Name = "BtnHome";
			this.BtnHome.Size = new System.Drawing.Size(200, 80);
			this.BtnHome.TabIndex = 2;
			this.BtnHome.Text = "主页";
			this.BtnHome.UseVisualStyleBackColor = false;
			this.BtnHome.Click += new System.EventHandler(this.BtnHome_Click);
			// 
			// Form_Panel
			// 
			this.Form_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Form_Panel.Location = new System.Drawing.Point(206, 53);
			this.Form_Panel.Name = "Form_Panel";
			this.Form_Panel.Size = new System.Drawing.Size(910, 580);
			this.Form_Panel.TabIndex = 1;
			// 
			// Form_Desktop
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1139, 632);
			this.Controls.Add(this.Form_Panel);
			this.Controls.Add(this.Select_Panel);
			this.Name = "Form_Desktop";
			this.Text = "橘皮提";
			this.Select_Panel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private MetroFramework.Controls.MetroPanel Select_Panel;
		private System.Windows.Forms.Button BtnMoreFunction;
		private System.Windows.Forms.Button BtnSetting;
		private System.Windows.Forms.Button BtnChart;
		private System.Windows.Forms.Button BtnHome;
		private System.Windows.Forms.Panel Form_Panel;
	}
}


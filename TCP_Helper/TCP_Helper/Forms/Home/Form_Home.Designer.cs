namespace TCP_Helper.Forms.Home
{
	partial class Form_Home
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
			this.uiTextBox1 = new Sunny.UI.UITextBox();
			this.SuspendLayout();
			// 
			// uiTextBox1
			// 
			this.uiTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.uiTextBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.uiTextBox1.Location = new System.Drawing.Point(220, 166);
			this.uiTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.uiTextBox1.MinimumSize = new System.Drawing.Size(1, 16);
			this.uiTextBox1.Name = "uiTextBox1";
			this.uiTextBox1.Padding = new System.Windows.Forms.Padding(5);
			this.uiTextBox1.ShowText = false;
			this.uiTextBox1.Size = new System.Drawing.Size(328, 208);
			this.uiTextBox1.TabIndex = 1;
			this.uiTextBox1.Text = "我们是GPT";
			this.uiTextBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.uiTextBox1.Watermark = "";
			// 
			// Form_Home
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Snow;
			this.ClientSize = new System.Drawing.Size(910, 580);
			this.Controls.Add(this.uiTextBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Form_Home";
			this.Text = "Form_Home";
			this.ResumeLayout(false);

		}

		#endregion

		private Sunny.UI.UITextBox uiTextBox1;
	}
}
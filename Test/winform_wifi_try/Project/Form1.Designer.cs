namespace Project
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			button1 = new Button();
			textBox1 = new TextBox();
			textBox2 = new TextBox();
			button2 = new Button();
			button3 = new Button();
			label1 = new Label();
			label2 = new Label();
			SuspendLayout();
			// 
			// button1
			// 
			button1.Location = new Point(260, 282);
			button1.Name = "button1";
			button1.Size = new Size(75, 23);
			button1.TabIndex = 0;
			button1.Text = "连接";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// textBox1
			// 
			textBox1.Location = new Point(330, 189);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(100, 23);
			textBox1.TabIndex = 1;
			// 
			// textBox2
			// 
			textBox2.Location = new Point(330, 228);
			textBox2.Name = "textBox2";
			textBox2.Size = new Size(100, 23);
			textBox2.TabIndex = 2;
			// 
			// button2
			// 
			button2.Location = new Point(341, 282);
			button2.Name = "button2";
			button2.Size = new Size(75, 23);
			button2.TabIndex = 3;
			button2.Text = "发送";
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// button3
			// 
			button3.Location = new Point(422, 282);
			button3.Name = "button3";
			button3.Size = new Size(75, 23);
			button3.TabIndex = 4;
			button3.Text = "接收";
			button3.UseVisualStyleBackColor = true;
			button3.Click += button3_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(281, 192);
			label1.Name = "label1";
			label1.Size = new Size(32, 17);
			label1.TabIndex = 5;
			label1.Text = "发送";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(281, 231);
			label2.Name = "label2";
			label2.Size = new Size(32, 17);
			label2.TabIndex = 6;
			label2.Text = "接收";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 17F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(button3);
			Controls.Add(button2);
			Controls.Add(textBox2);
			Controls.Add(textBox1);
			Controls.Add(button1);
			Name = "Form1";
			Text = "Form1";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button button1;
		private TextBox textBox1;
		private TextBox textBox2;
		private Button button2;
		private Button button3;
		private Label label1;
		private Label label2;
	}
}

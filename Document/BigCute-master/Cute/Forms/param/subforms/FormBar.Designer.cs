
namespace Cute.param
{
    partial class FormBar
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
            this.button_WaveShow = new System.Windows.Forms.Button();
            this.button_Send = new System.Windows.Forms.Button();
            this.textBox_Value = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_Min = new System.Windows.Forms.TextBox();
            this.textBox_Max = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_OK = new System.Windows.Forms.Label();
            this.label_Name = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // button_WaveShow
            // 
            this.button_WaveShow.Font = new System.Drawing.Font("宋体", 12F);
            this.button_WaveShow.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_WaveShow.Location = new System.Drawing.Point(13, 685);
            this.button_WaveShow.Margin = new System.Windows.Forms.Padding(4);
            this.button_WaveShow.Name = "button_WaveShow";
            this.button_WaveShow.Size = new System.Drawing.Size(120, 35);
            this.button_WaveShow.TabIndex = 6;
            this.button_WaveShow.Text = "波形显示";
            this.button_WaveShow.UseVisualStyleBackColor = true;
            // 
            // button_Send
            // 
            this.button_Send.Font = new System.Drawing.Font("宋体", 12F);
            this.button_Send.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Send.Location = new System.Drawing.Point(192, 685);
            this.button_Send.Margin = new System.Windows.Forms.Padding(4);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(120, 35);
            this.button_Send.TabIndex = 7;
            this.button_Send.Text = "发送参数";
            this.button_Send.UseVisualStyleBackColor = true;
            // 
            // textBox_Value
            // 
            this.textBox_Value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.textBox_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.textBox_Value.Location = new System.Drawing.Point(114, 302);
            this.textBox_Value.Name = "textBox_Value";
            this.textBox_Value.Size = new System.Drawing.Size(109, 35);
            this.textBox_Value.TabIndex = 2;
            this.textBox_Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.textBox_Value.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBox_Min);
            this.panel1.Controls.Add(this.textBox_Max);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.trackBar);
            this.panel1.Controls.Add(this.textBox_Value);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 610);
            this.panel1.TabIndex = 48;
            // 
            // textBox_Min
            // 
            this.textBox_Min.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.textBox_Min.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Min.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.textBox_Min.Location = new System.Drawing.Point(114, 555);
            this.textBox_Min.Name = "textBox_Min";
            this.textBox_Min.Size = new System.Drawing.Size(109, 35);
            this.textBox_Min.TabIndex = 3;
            this.textBox_Min.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.textBox_Min.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // textBox_Max
            // 
            this.textBox_Max.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.textBox_Max.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Max.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.textBox_Max.Location = new System.Drawing.Point(114, 50);
            this.textBox_Max.Name = "textBox_Max";
            this.textBox_Max.Size = new System.Drawing.Size(109, 35);
            this.textBox_Max.TabIndex = 1;
            this.textBox_Max.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.textBox_Max.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 24);
            this.label4.TabIndex = 9;
            this.label4.Text = "Max";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 556);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "Min";
            // 
            // trackBar
            // 
            this.trackBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.trackBar.LargeChange = 1;
            this.trackBar.Location = new System.Drawing.Point(229, 24);
            this.trackBar.Maximum = 0;
            this.trackBar.Name = "trackBar";
            this.trackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar.Size = new System.Drawing.Size(69, 584);
            this.trackBar.TabIndex = 7;
            this.trackBar.TabStop = false;
            this.trackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 305);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Value";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "滑动调参";
            // 
            // label_OK
            // 
            this.label_OK.AutoSize = true;
            this.label_OK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label_OK.Location = new System.Drawing.Point(151, 690);
            this.label_OK.Name = "label_OK";
            this.label_OK.Size = new System.Drawing.Size(34, 24);
            this.label_OK.TabIndex = 53;
            this.label_OK.Text = "OK";
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Font = new System.Drawing.Font("宋体", 15F);
            this.label_Name.Location = new System.Drawing.Point(105, 9);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(73, 30);
            this.label_Name.TabIndex = 47;
            this.label_Name.Text = "Name";
            // 
            // FormBar
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(325, 740);
            this.Controls.Add(this.button_WaveShow);
            this.Controls.Add(this.button_Send);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label_OK);
            this.Controls.Add(this.label_Name);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormBar";
            this.Text = "FormBar";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormBar_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_WaveShow;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.TextBox textBox_Value;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_OK;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.TextBox textBox_Max;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.TextBox textBox_Min;
    }
}
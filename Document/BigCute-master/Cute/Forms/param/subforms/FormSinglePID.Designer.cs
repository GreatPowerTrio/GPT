
namespace Cute.param
{
    partial class FormSinglePID
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
            this.textBox_MaxIOut = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_WaveShow = new System.Windows.Forms.Button();
            this.textBox_Kp = new System.Windows.Forms.TextBox();
            this.textBox_Ki = new System.Windows.Forms.TextBox();
            this.textBox_Kd = new System.Windows.Forms.TextBox();
            this.textBox_MaxOut = new System.Windows.Forms.TextBox();
            this.button_Send = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_Name = new System.Windows.Forms.Label();
            this.label_OK = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_MaxIOut
            // 
            this.textBox_MaxIOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.textBox_MaxIOut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_MaxIOut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.textBox_MaxIOut.Location = new System.Drawing.Point(115, 195);
            this.textBox_MaxIOut.Name = "textBox_MaxIOut";
            this.textBox_MaxIOut.Size = new System.Drawing.Size(168, 35);
            this.textBox_MaxIOut.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 248);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 24);
            this.label6.TabIndex = 5;
            this.label6.Text = "MaxOut";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 24);
            this.label5.TabIndex = 4;
            this.label5.Text = "MaxIOut";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "Kd";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ki";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Kp";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "单环PID";
            // 
            // button_WaveShow
            // 
            this.button_WaveShow.Font = new System.Drawing.Font("宋体", 12F);
            this.button_WaveShow.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_WaveShow.Location = new System.Drawing.Point(13, 375);
            this.button_WaveShow.Margin = new System.Windows.Forms.Padding(4);
            this.button_WaveShow.Name = "button_WaveShow";
            this.button_WaveShow.Size = new System.Drawing.Size(120, 35);
            this.button_WaveShow.TabIndex = 51;
            this.button_WaveShow.Text = "波形显示";
            this.button_WaveShow.UseVisualStyleBackColor = true;
            // 
            // textBox_Kp
            // 
            this.textBox_Kp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.textBox_Kp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Kp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.textBox_Kp.Location = new System.Drawing.Point(115, 45);
            this.textBox_Kp.Name = "textBox_Kp";
            this.textBox_Kp.Size = new System.Drawing.Size(168, 35);
            this.textBox_Kp.TabIndex = 6;
            // 
            // textBox_Ki
            // 
            this.textBox_Ki.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.textBox_Ki.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Ki.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.textBox_Ki.Location = new System.Drawing.Point(115, 95);
            this.textBox_Ki.Name = "textBox_Ki";
            this.textBox_Ki.Size = new System.Drawing.Size(168, 35);
            this.textBox_Ki.TabIndex = 7;
            // 
            // textBox_Kd
            // 
            this.textBox_Kd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.textBox_Kd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Kd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.textBox_Kd.Location = new System.Drawing.Point(115, 145);
            this.textBox_Kd.Name = "textBox_Kd";
            this.textBox_Kd.Size = new System.Drawing.Size(168, 35);
            this.textBox_Kd.TabIndex = 8;
            // 
            // textBox_MaxOut
            // 
            this.textBox_MaxOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.textBox_MaxOut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_MaxOut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.textBox_MaxOut.Location = new System.Drawing.Point(115, 245);
            this.textBox_MaxOut.Name = "textBox_MaxOut";
            this.textBox_MaxOut.Size = new System.Drawing.Size(168, 35);
            this.textBox_MaxOut.TabIndex = 10;
            // 
            // button_Send
            // 
            this.button_Send.Font = new System.Drawing.Font("宋体", 12F);
            this.button_Send.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Send.Location = new System.Drawing.Point(192, 375);
            this.button_Send.Margin = new System.Windows.Forms.Padding(4);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(120, 35);
            this.button_Send.TabIndex = 52;
            this.button_Send.Text = "发送参数";
            this.button_Send.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBox_Kp);
            this.panel1.Controls.Add(this.textBox_Ki);
            this.panel1.Controls.Add(this.textBox_Kd);
            this.panel1.Controls.Add(this.textBox_MaxOut);
            this.panel1.Controls.Add(this.textBox_MaxIOut);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 300);
            this.panel1.TabIndex = 47;
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Font = new System.Drawing.Font("宋体", 15F);
            this.label_Name.Location = new System.Drawing.Point(123, 2);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(73, 30);
            this.label_Name.TabIndex = 46;
            this.label_Name.Text = "Name";
            // 
            // label_OK
            // 
            this.label_OK.AutoSize = true;
            this.label_OK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label_OK.Location = new System.Drawing.Point(151, 380);
            this.label_OK.Name = "label_OK";
            this.label_OK.Size = new System.Drawing.Size(34, 24);
            this.label_OK.TabIndex = 53;
            this.label_OK.Text = "OK";
            // 
            // FormSinglePID
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(325, 442);
            this.Controls.Add(this.label_OK);
            this.Controls.Add(this.button_WaveShow);
            this.Controls.Add(this.button_Send);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label_Name);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormSinglePID";
            this.Text = "FormSinglePID";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormSinglePID_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox_MaxIOut;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_WaveShow;
        private System.Windows.Forms.TextBox textBox_Kp;
        private System.Windows.Forms.TextBox textBox_Ki;
        private System.Windows.Forms.TextBox textBox_Kd;
        private System.Windows.Forms.TextBox textBox_MaxOut;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.Label label_OK;
    }
}
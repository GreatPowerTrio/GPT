
namespace Cute
{
    partial class FormLAN
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
            this.button_Start = new System.Windows.Forms.Button();
            this.label_baudrate = new System.Windows.Forms.Label();
            this.label_RemoteIP = new System.Windows.Forms.Label();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.comboBox_RemoteIP = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button_Start
            // 
            this.button_Start.Font = new System.Drawing.Font("宋体", 12F);
            this.button_Start.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Start.Location = new System.Drawing.Point(583, 5);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(125, 40);
            this.button_Start.TabIndex = 3;
            this.button_Start.Text = "打开连接";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // label_baudrate
            // 
            this.label_baudrate.AutoSize = true;
            this.label_baudrate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_baudrate.ForeColor = System.Drawing.Color.Gainsboro;
            this.label_baudrate.Location = new System.Drawing.Point(382, 13);
            this.label_baudrate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_baudrate.Name = "label_baudrate";
            this.label_baudrate.Size = new System.Drawing.Size(58, 24);
            this.label_baudrate.TabIndex = 23;
            this.label_baudrate.Text = "Port";
            // 
            // label_RemoteIP
            // 
            this.label_RemoteIP.AutoSize = true;
            this.label_RemoteIP.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_RemoteIP.ForeColor = System.Drawing.Color.Gainsboro;
            this.label_RemoteIP.Location = new System.Drawing.Point(10, 13);
            this.label_RemoteIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_RemoteIP.Name = "label_RemoteIP";
            this.label_RemoteIP.Size = new System.Drawing.Size(106, 24);
            this.label_RemoteIP.TabIndex = 21;
            this.label_RemoteIP.Text = "RemoteIP";
            // 
            // textBox_Port
            // 
            this.textBox_Port.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.textBox_Port.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Port.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.textBox_Port.Location = new System.Drawing.Point(447, 10);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(120, 35);
            this.textBox_Port.TabIndex = 2;
            // 
            // comboBox_RemoteIP
            // 
            this.comboBox_RemoteIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.comboBox_RemoteIP.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_RemoteIP.FormattingEnabled = true;
            this.comboBox_RemoteIP.IntegralHeight = false;
            this.comboBox_RemoteIP.Location = new System.Drawing.Point(124, 10);
            this.comboBox_RemoteIP.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_RemoteIP.Name = "comboBox_RemoteIP";
            this.comboBox_RemoteIP.Size = new System.Drawing.Size(250, 32);
            this.comboBox_RemoteIP.TabIndex = 1;
            // 
            // FormLAN
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(720, 50);
            this.Controls.Add(this.comboBox_RemoteIP);
            this.Controls.Add(this.textBox_Port);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.label_baudrate);
            this.Controls.Add(this.label_RemoteIP);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormLAN";
            this.Text = "FormLAN";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Label label_baudrate;
        private System.Windows.Forms.Label label_RemoteIP;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.ComboBox comboBox_RemoteIP;
    }
}
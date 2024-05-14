
namespace Cute.param
{
    partial class FormOneParam
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
            this.textBox_value = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_Name = new System.Windows.Forms.Label();
            this.label_OK = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_WaveShow
            // 
            this.button_WaveShow.Font = new System.Drawing.Font("宋体", 12F);
            this.button_WaveShow.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_WaveShow.Location = new System.Drawing.Point(13, 200);
            this.button_WaveShow.Margin = new System.Windows.Forms.Padding(4);
            this.button_WaveShow.Name = "button_WaveShow";
            this.button_WaveShow.Size = new System.Drawing.Size(120, 35);
            this.button_WaveShow.TabIndex = 4;
            this.button_WaveShow.Text = "波形显示";
            this.button_WaveShow.UseVisualStyleBackColor = true;
            // 
            // button_Send
            // 
            this.button_Send.Font = new System.Drawing.Font("宋体", 12F);
            this.button_Send.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Send.Location = new System.Drawing.Point(192, 200);
            this.button_Send.Margin = new System.Windows.Forms.Padding(4);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(120, 35);
            this.button_Send.TabIndex = 5;
            this.button_Send.Text = "发送参数";
            this.button_Send.UseVisualStyleBackColor = true;
            // 
            // textBox_value
            // 
            this.textBox_value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.textBox_value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.textBox_value.Location = new System.Drawing.Point(115, 45);
            this.textBox_value.Name = "textBox_value";
            this.textBox_value.Size = new System.Drawing.Size(168, 35);
            this.textBox_value.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBox_value);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 125);
            this.panel1.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
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
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "单参数";
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Font = new System.Drawing.Font("宋体", 15F);
            this.label_Name.Location = new System.Drawing.Point(109, 2);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(73, 30);
            this.label_Name.TabIndex = 38;
            this.label_Name.Text = "Name";
            // 
            // label_OK
            // 
            this.label_OK.AutoSize = true;
            this.label_OK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label_OK.Location = new System.Drawing.Point(151, 205);
            this.label_OK.Name = "label_OK";
            this.label_OK.Size = new System.Drawing.Size(34, 24);
            this.label_OK.TabIndex = 46;
            this.label_OK.Text = "OK";
            // 
            // FormOneParam
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(325, 265);
            this.Controls.Add(this.label_OK);
            this.Controls.Add(this.button_WaveShow);
            this.Controls.Add(this.button_Send);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label_Name);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormOneParam";
            this.Text = "FormOneParam";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormOneParam_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_WaveShow;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.TextBox textBox_value;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_OK;
    }
}
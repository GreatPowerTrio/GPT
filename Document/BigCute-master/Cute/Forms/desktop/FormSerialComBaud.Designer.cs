
namespace Cute
{
    partial class FormSerialComBaud
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
            if(this != null)
                base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button_openSerial = new System.Windows.Forms.Button();
            this.label_baudrate = new System.Windows.Forms.Label();
            this.comboBox_baudrate = new System.Windows.Forms.ComboBox();
            this.label_com = new System.Windows.Forms.Label();
            this.comboBox_com = new System.Windows.Forms.ComboBox();
            this.desktopBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.desktopBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.desktopBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.desktopBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_openSerial
            // 
            this.button_openSerial.Font = new System.Drawing.Font("宋体", 12F);
            this.button_openSerial.Location = new System.Drawing.Point(456, 6);
            this.button_openSerial.Name = "button_openSerial";
            this.button_openSerial.Size = new System.Drawing.Size(125, 40);
            this.button_openSerial.TabIndex = 20;
            this.button_openSerial.Text = "打开串口";
            this.button_openSerial.UseVisualStyleBackColor = true;
            this.button_openSerial.Click += new System.EventHandler(this.button_openSerial_Click);
            // 
            // label_baudrate
            // 
            this.label_baudrate.AutoSize = true;
            this.label_baudrate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_baudrate.ForeColor = System.Drawing.Color.Gainsboro;
            this.label_baudrate.Location = new System.Drawing.Point(214, 13);
            this.label_baudrate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_baudrate.Name = "label_baudrate";
            this.label_baudrate.Size = new System.Drawing.Size(82, 24);
            this.label_baudrate.TabIndex = 18;
            this.label_baudrate.Text = "波特率";
            // 
            // comboBox_baudrate
            // 
            this.comboBox_baudrate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.comboBox_baudrate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_baudrate.FormattingEnabled = true;
            this.comboBox_baudrate.IntegralHeight = false;
            this.comboBox_baudrate.Items.AddRange(new object[] {
            "9600",
            "14400",
            "19200",
            "38400",
            "115200",
            "230400",
            "460800"});
            this.comboBox_baudrate.Location = new System.Drawing.Point(304, 9);
            this.comboBox_baudrate.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_baudrate.Name = "comboBox_baudrate";
            this.comboBox_baudrate.Size = new System.Drawing.Size(122, 32);
            this.comboBox_baudrate.TabIndex = 19;
            // 
            // label_com
            // 
            this.label_com.AutoSize = true;
            this.label_com.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_com.ForeColor = System.Drawing.Color.Gainsboro;
            this.label_com.Location = new System.Drawing.Point(10, 13);
            this.label_com.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_com.Name = "label_com";
            this.label_com.Size = new System.Drawing.Size(82, 24);
            this.label_com.TabIndex = 16;
            this.label_com.Text = "端口号";
            // 
            // comboBox_com
            // 
            this.comboBox_com.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.comboBox_com.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox_com.ForeColor = System.Drawing.SystemColors.InfoText;
            this.comboBox_com.FormattingEnabled = true;
            this.comboBox_com.IntegralHeight = false;
            this.comboBox_com.Location = new System.Drawing.Point(100, 9);
            this.comboBox_com.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_com.Name = "comboBox_com";
            this.comboBox_com.Size = new System.Drawing.Size(100, 32);
            this.comboBox_com.Sorted = true;
            this.comboBox_com.TabIndex = 17;
            this.comboBox_com.DropDown += new System.EventHandler(this.comboBox_com_DropDown);
            // 
            // desktopBindingSource
            // 
            this.desktopBindingSource.DataSource = typeof(Cute.Desktop);
            // 
            // desktopBindingSource1
            // 
            this.desktopBindingSource1.DataSource = typeof(Cute.Desktop);
            // 
            // FormSerialComBaud
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(591, 50);
            this.Controls.Add(this.button_openSerial);
            this.Controls.Add(this.label_baudrate);
            this.Controls.Add(this.comboBox_baudrate);
            this.Controls.Add(this.label_com);
            this.Controls.Add(this.comboBox_com);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormSerialComBaud";
            this.Text = "FormSerialSimple";
            this.Load += new System.EventHandler(this.FormSerialComBaud_Load);
            ((System.ComponentModel.ISupportInitialize)(this.desktopBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.desktopBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource desktopBindingSource;
        private System.Windows.Forms.BindingSource desktopBindingSource1;
        private System.Windows.Forms.Button button_openSerial;
        private System.Windows.Forms.Label label_baudrate;
        private System.Windows.Forms.ComboBox comboBox_baudrate;
        private System.Windows.Forms.Label label_com;
        private System.Windows.Forms.ComboBox comboBox_com;
    }
}
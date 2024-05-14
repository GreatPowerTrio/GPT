
namespace Cute
{
    partial class FormBasic
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.groupBox_Rec = new System.Windows.Forms.GroupBox();
            this.panel_Rec = new System.Windows.Forms.Panel();
            this.textBox_Rec = new System.Windows.Forms.RichTextBox();
            this.label_RecTitle = new System.Windows.Forms.Label();
            this.button_Save = new System.Windows.Forms.Button();
            this.checkBox_RecHex = new System.Windows.Forms.CheckBox();
            this.button_ClearRec = new System.Windows.Forms.Button();
            this.groupBox_Send = new System.Windows.Forms.GroupBox();
            this.comboBox_Add = new System.Windows.Forms.ComboBox();
            this.panel_Send = new System.Windows.Forms.Panel();
            this.textBox_Send = new System.Windows.Forms.RichTextBox();
            this.label_SendTitle = new System.Windows.Forms.Label();
            this.button_Send = new System.Windows.Forms.Button();
            this.button_ClearSend = new System.Windows.Forms.Button();
            this.checkBox_HexSend = new System.Windows.Forms.CheckBox();
            this.checkBox_AutoClear = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_dt = new System.Windows.Forms.TextBox();
            this.checkBox_TimerSend = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.groupBox_Rec.SuspendLayout();
            this.panel_Rec.SuspendLayout();
            this.groupBox_Send.SuspendLayout();
            this.panel_Send.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.splitContainer.Panel1.Controls.Add(this.groupBox_Rec);
            this.splitContainer.Panel1MinSize = 270;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.splitContainer.Panel2.Controls.Add(this.groupBox_Send);
            this.splitContainer.Panel2MinSize = 360;
            this.splitContainer.Size = new System.Drawing.Size(1373, 931);
            this.splitContainer.SplitterDistance = 549;
            this.splitContainer.SplitterWidth = 5;
            this.splitContainer.TabIndex = 0;
            // 
            // groupBox_Rec
            // 
            this.groupBox_Rec.Controls.Add(this.panel_Rec);
            this.groupBox_Rec.Controls.Add(this.label_RecTitle);
            this.groupBox_Rec.Controls.Add(this.button_Save);
            this.groupBox_Rec.Controls.Add(this.checkBox_RecHex);
            this.groupBox_Rec.Controls.Add(this.button_ClearRec);
            this.groupBox_Rec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Rec.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox_Rec.ForeColor = System.Drawing.Color.Gainsboro;
            this.groupBox_Rec.Location = new System.Drawing.Point(0, 0);
            this.groupBox_Rec.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.groupBox_Rec.Name = "groupBox_Rec";
            this.groupBox_Rec.Size = new System.Drawing.Size(1373, 549);
            this.groupBox_Rec.TabIndex = 0;
            this.groupBox_Rec.TabStop = false;
            this.groupBox_Rec.Text = "接收区";
            this.groupBox_Rec.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // panel_Rec
            // 
            this.panel_Rec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Rec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Rec.Controls.Add(this.textBox_Rec);
            this.panel_Rec.Location = new System.Drawing.Point(200, 35);
            this.panel_Rec.Name = "panel_Rec";
            this.panel_Rec.Size = new System.Drawing.Size(1171, 508);
            this.panel_Rec.TabIndex = 28;
            // 
            // textBox_Rec
            // 
            this.textBox_Rec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.textBox_Rec.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_Rec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Rec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.textBox_Rec.Location = new System.Drawing.Point(0, 0);
            this.textBox_Rec.Name = "textBox_Rec";
            this.textBox_Rec.ReadOnly = true;
            this.textBox_Rec.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.textBox_Rec.Size = new System.Drawing.Size(1169, 506);
            this.textBox_Rec.TabIndex = 0;
            this.textBox_Rec.Text = "";
            this.textBox_Rec.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.textBox_Send_LinkClicked);
            // 
            // label_RecTitle
            // 
            this.label_RecTitle.AutoSize = true;
            this.label_RecTitle.Font = new System.Drawing.Font("宋体", 12F);
            this.label_RecTitle.ForeColor = System.Drawing.Color.Gainsboro;
            this.label_RecTitle.Location = new System.Drawing.Point(196, 9);
            this.label_RecTitle.Name = "label_RecTitle";
            this.label_RecTitle.Size = new System.Drawing.Size(358, 24);
            this.label_RecTitle.TabIndex = 34;
            this.label_RecTitle.Text = "共接收 0 字节，速度 0 字节/秒";
            // 
            // button_Save
            // 
            this.button_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Save.FlatAppearance.BorderSize = 0;
            this.button_Save.Font = new System.Drawing.Font("宋体", 12F);
            this.button_Save.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Save.Location = new System.Drawing.Point(25, 427);
            this.button_Save.Margin = new System.Windows.Forms.Padding(4);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(150, 45);
            this.button_Save.TabIndex = 33;
            this.button_Save.Text = "保存接收";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // checkBox_RecHex
            // 
            this.checkBox_RecHex.AutoSize = true;
            this.checkBox_RecHex.Font = new System.Drawing.Font("宋体", 11F);
            this.checkBox_RecHex.ForeColor = System.Drawing.Color.Gainsboro;
            this.checkBox_RecHex.Location = new System.Drawing.Point(25, 45);
            this.checkBox_RecHex.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_RecHex.Name = "checkBox_RecHex";
            this.checkBox_RecHex.Size = new System.Drawing.Size(146, 26);
            this.checkBox_RecHex.TabIndex = 31;
            this.checkBox_RecHex.Text = "16进制显示";
            this.checkBox_RecHex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_RecHex.UseVisualStyleBackColor = true;
            this.checkBox_RecHex.CheckedChanged += new System.EventHandler(this.checkBox_RecHex_CheckedChanged);
            // 
            // button_ClearRec
            // 
            this.button_ClearRec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_ClearRec.FlatAppearance.BorderSize = 0;
            this.button_ClearRec.Font = new System.Drawing.Font("宋体", 12F);
            this.button_ClearRec.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_ClearRec.Location = new System.Drawing.Point(25, 487);
            this.button_ClearRec.Name = "button_ClearRec";
            this.button_ClearRec.Size = new System.Drawing.Size(150, 45);
            this.button_ClearRec.TabIndex = 30;
            this.button_ClearRec.Text = "清空接收";
            this.button_ClearRec.UseVisualStyleBackColor = true;
            this.button_ClearRec.Click += new System.EventHandler(this.button_ClearRec_Click);
            // 
            // groupBox_Send
            // 
            this.groupBox_Send.Controls.Add(this.comboBox_Add);
            this.groupBox_Send.Controls.Add(this.panel_Send);
            this.groupBox_Send.Controls.Add(this.label_SendTitle);
            this.groupBox_Send.Controls.Add(this.button_Send);
            this.groupBox_Send.Controls.Add(this.button_ClearSend);
            this.groupBox_Send.Controls.Add(this.checkBox_HexSend);
            this.groupBox_Send.Controls.Add(this.checkBox_AutoClear);
            this.groupBox_Send.Controls.Add(this.label1);
            this.groupBox_Send.Controls.Add(this.textBox_dt);
            this.groupBox_Send.Controls.Add(this.checkBox_TimerSend);
            this.groupBox_Send.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_Send.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox_Send.ForeColor = System.Drawing.Color.Gainsboro;
            this.groupBox_Send.Location = new System.Drawing.Point(0, 0);
            this.groupBox_Send.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.groupBox_Send.Name = "groupBox_Send";
            this.groupBox_Send.Size = new System.Drawing.Size(1373, 377);
            this.groupBox_Send.TabIndex = 10;
            this.groupBox_Send.TabStop = false;
            this.groupBox_Send.Text = "发送区";
            this.groupBox_Send.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // comboBox_Add
            // 
            this.comboBox_Add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.comboBox_Add.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Add.Font = new System.Drawing.Font("宋体", 11F);
            this.comboBox_Add.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.comboBox_Add.FormattingEnabled = true;
            this.comboBox_Add.Items.AddRange(new object[] {
            "无追加",
            "\\r",
            "\\n",
            "\\r\\n",
            "\\n\\r",
            "\\r\\n\\r\\n"});
            this.comboBox_Add.Location = new System.Drawing.Point(25, 189);
            this.comboBox_Add.Name = "comboBox_Add";
            this.comboBox_Add.Size = new System.Drawing.Size(150, 30);
            this.comboBox_Add.TabIndex = 29;
            this.comboBox_Add.DropDownClosed += new System.EventHandler(this.comboBox_Add_DropDownClosed);
            // 
            // panel_Send
            // 
            this.panel_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Send.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Send.Controls.Add(this.textBox_Send);
            this.panel_Send.Location = new System.Drawing.Point(200, 35);
            this.panel_Send.Name = "panel_Send";
            this.panel_Send.Size = new System.Drawing.Size(1171, 336);
            this.panel_Send.TabIndex = 1;
            // 
            // textBox_Send
            // 
            this.textBox_Send.AcceptsTab = true;
            this.textBox_Send.AutoWordSelection = true;
            this.textBox_Send.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.textBox_Send.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_Send.BulletIndent = 2;
            this.textBox_Send.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Send.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.textBox_Send.Location = new System.Drawing.Point(0, 0);
            this.textBox_Send.Name = "textBox_Send";
            this.textBox_Send.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.textBox_Send.Size = new System.Drawing.Size(1169, 334);
            this.textBox_Send.TabIndex = 0;
            this.textBox_Send.Text = "";
            this.textBox_Send.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.textBox_Send_LinkClicked);
            this.textBox_Send.TextChanged += new System.EventHandler(this.textBox_Send_TextChanged);
            this.textBox_Send.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Send_KeyDown);
            // 
            // label_SendTitle
            // 
            this.label_SendTitle.AutoSize = true;
            this.label_SendTitle.Font = new System.Drawing.Font("宋体", 12F);
            this.label_SendTitle.Location = new System.Drawing.Point(196, 9);
            this.label_SendTitle.Name = "label_SendTitle";
            this.label_SendTitle.Size = new System.Drawing.Size(166, 24);
            this.label_SendTitle.TabIndex = 27;
            this.label_SendTitle.Text = "共发送 0 字节";
            // 
            // button_Send
            // 
            this.button_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Send.FlatAppearance.BorderSize = 0;
            this.button_Send.Font = new System.Drawing.Font("宋体", 12F);
            this.button_Send.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Send.Location = new System.Drawing.Point(25, 255);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(150, 45);
            this.button_Send.TabIndex = 26;
            this.button_Send.Text = "单次发送";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.button_Send_Click);
            // 
            // button_ClearSend
            // 
            this.button_ClearSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_ClearSend.FlatAppearance.BorderSize = 0;
            this.button_ClearSend.Font = new System.Drawing.Font("宋体", 12F);
            this.button_ClearSend.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_ClearSend.Location = new System.Drawing.Point(25, 315);
            this.button_ClearSend.Name = "button_ClearSend";
            this.button_ClearSend.Size = new System.Drawing.Size(150, 45);
            this.button_ClearSend.TabIndex = 25;
            this.button_ClearSend.Text = "清空发送";
            this.button_ClearSend.UseVisualStyleBackColor = true;
            this.button_ClearSend.Click += new System.EventHandler(this.button_ClearSend_Click);
            // 
            // checkBox_HexSend
            // 
            this.checkBox_HexSend.AutoSize = true;
            this.checkBox_HexSend.Font = new System.Drawing.Font("宋体", 11F);
            this.checkBox_HexSend.Location = new System.Drawing.Point(25, 79);
            this.checkBox_HexSend.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_HexSend.Name = "checkBox_HexSend";
            this.checkBox_HexSend.Size = new System.Drawing.Size(146, 26);
            this.checkBox_HexSend.TabIndex = 24;
            this.checkBox_HexSend.Text = "16进制发送";
            this.checkBox_HexSend.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_HexSend.UseVisualStyleBackColor = true;
            // 
            // checkBox_AutoClear
            // 
            this.checkBox_AutoClear.AutoSize = true;
            this.checkBox_AutoClear.Font = new System.Drawing.Font("宋体", 11F);
            this.checkBox_AutoClear.Location = new System.Drawing.Point(25, 45);
            this.checkBox_AutoClear.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_AutoClear.Name = "checkBox_AutoClear";
            this.checkBox_AutoClear.Size = new System.Drawing.Size(146, 26);
            this.checkBox_AutoClear.TabIndex = 23;
            this.checkBox_AutoClear.Text = "发送完清空";
            this.checkBox_AutoClear.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F);
            this.label1.Location = new System.Drawing.Point(106, 149);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 22);
            this.label1.TabIndex = 22;
            this.label1.Text = "ms";
            // 
            // textBox_dt
            // 
            this.textBox_dt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.textBox_dt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_dt.Font = new System.Drawing.Font("宋体", 11F);
            this.textBox_dt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.textBox_dt.Location = new System.Drawing.Point(25, 147);
            this.textBox_dt.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_dt.Name = "textBox_dt";
            this.textBox_dt.Size = new System.Drawing.Size(73, 33);
            this.textBox_dt.TabIndex = 21;
            this.textBox_dt.TextChanged += new System.EventHandler(this.textBox_dt_TextChanged);
            // 
            // checkBox_TimerSend
            // 
            this.checkBox_TimerSend.AutoSize = true;
            this.checkBox_TimerSend.Font = new System.Drawing.Font("宋体", 11F);
            this.checkBox_TimerSend.Location = new System.Drawing.Point(25, 113);
            this.checkBox_TimerSend.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_TimerSend.Name = "checkBox_TimerSend";
            this.checkBox_TimerSend.Size = new System.Drawing.Size(124, 26);
            this.checkBox_TimerSend.TabIndex = 20;
            this.checkBox_TimerSend.Text = "定时发送";
            this.checkBox_TimerSend.UseVisualStyleBackColor = true;
            this.checkBox_TimerSend.CheckedChanged += new System.EventHandler(this.checkBox_TimerSend_CheckedChanged);
            // 
            // FormBasic
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1373, 931);
            this.Controls.Add(this.splitContainer);
            this.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormBasic";
            this.Text = "FormBasic";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.groupBox_Rec.ResumeLayout(false);
            this.groupBox_Rec.PerformLayout();
            this.panel_Rec.ResumeLayout(false);
            this.groupBox_Send.ResumeLayout(false);
            this.groupBox_Send.PerformLayout();
            this.panel_Send.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox groupBox_Send;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.Button button_ClearSend;
        private System.Windows.Forms.CheckBox checkBox_HexSend;
        private System.Windows.Forms.CheckBox checkBox_AutoClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_dt;
        private System.Windows.Forms.CheckBox checkBox_TimerSend;
        private System.Windows.Forms.Label label_SendTitle;
        private System.Windows.Forms.GroupBox groupBox_Rec;
        private System.Windows.Forms.Label label_RecTitle;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.CheckBox checkBox_RecHex;
        private System.Windows.Forms.Button button_ClearRec;
        private System.Windows.Forms.Panel panel_Rec;
        private System.Windows.Forms.RichTextBox textBox_Rec;
        private System.Windows.Forms.Panel panel_Send;
        private System.Windows.Forms.RichTextBox textBox_Send;
        private System.Windows.Forms.ComboBox comboBox_Add;
    }
}
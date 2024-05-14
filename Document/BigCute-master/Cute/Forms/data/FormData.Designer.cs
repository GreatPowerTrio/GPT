
namespace Cute
{
    partial class FormData
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
            this.panel_up = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox_Send = new System.Windows.Forms.GroupBox();
            this.panel_Send = new System.Windows.Forms.Panel();
            this.textBox_Send = new System.Windows.Forms.RichTextBox();
            this.label_SendTitle = new System.Windows.Forms.Label();
            this.button_Send = new System.Windows.Forms.Button();
            this.button_ClearSend = new System.Windows.Forms.Button();
            this.checkBox_HexSend = new System.Windows.Forms.CheckBox();
            this.groupBox_Rec = new System.Windows.Forms.GroupBox();
            this.checkBox_Update = new System.Windows.Forms.CheckBox();
            this.panel_Rec = new System.Windows.Forms.Panel();
            this.textBox_Rec = new System.Windows.Forms.RichTextBox();
            this.label_RecTitle = new System.Windows.Forms.Label();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_ClearRec = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.groupBox_Send.SuspendLayout();
            this.panel_Send.SuspendLayout();
            this.groupBox_Rec.SuspendLayout();
            this.panel_Rec.SuspendLayout();
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
            this.splitContainer.Panel1.Controls.Add(this.panel_up);
            this.splitContainer.Panel1.Controls.Add(this.panel2);
            this.splitContainer.Panel1.Controls.Add(this.panel1);
            this.splitContainer.Panel1MinSize = 270;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.splitContainer.Panel2.Controls.Add(this.groupBox_Send);
            this.splitContainer.Panel2.Controls.Add(this.groupBox_Rec);
            this.splitContainer.Panel2MinSize = 355;
            this.splitContainer.Size = new System.Drawing.Size(1373, 931);
            this.splitContainer.SplitterDistance = 549;
            this.splitContainer.SplitterWidth = 5;
            this.splitContainer.TabIndex = 1;
            // 
            // panel_up
            // 
            this.panel_up.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_up.Location = new System.Drawing.Point(0, 2);
            this.panel_up.Name = "panel_up";
            this.panel_up.Size = new System.Drawing.Size(1373, 545);
            this.panel_up.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(95)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 547);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1373, 2);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(95)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1373, 2);
            this.panel1.TabIndex = 0;
            // 
            // groupBox_Send
            // 
            this.groupBox_Send.Controls.Add(this.panel_Send);
            this.groupBox_Send.Controls.Add(this.label_SendTitle);
            this.groupBox_Send.Controls.Add(this.button_Send);
            this.groupBox_Send.Controls.Add(this.button_ClearSend);
            this.groupBox_Send.Controls.Add(this.checkBox_HexSend);
            this.groupBox_Send.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox_Send.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox_Send.ForeColor = System.Drawing.Color.Gainsboro;
            this.groupBox_Send.Location = new System.Drawing.Point(0, 192);
            this.groupBox_Send.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.groupBox_Send.Name = "groupBox_Send";
            this.groupBox_Send.Size = new System.Drawing.Size(1373, 185);
            this.groupBox_Send.TabIndex = 11;
            this.groupBox_Send.TabStop = false;
            this.groupBox_Send.Text = "发送区";
            this.groupBox_Send.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // panel_Send
            // 
            this.panel_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Send.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Send.Controls.Add(this.textBox_Send);
            this.panel_Send.Location = new System.Drawing.Point(202, 36);
            this.panel_Send.Name = "panel_Send";
            this.panel_Send.Size = new System.Drawing.Size(1170, 143);
            this.panel_Send.TabIndex = 28;
            // 
            // textBox_Send
            // 
            this.textBox_Send.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.textBox_Send.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_Send.DetectUrls = false;
            this.textBox_Send.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Send.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.textBox_Send.Location = new System.Drawing.Point(0, 0);
            this.textBox_Send.Name = "textBox_Send";
            this.textBox_Send.Size = new System.Drawing.Size(1168, 141);
            this.textBox_Send.TabIndex = 28;
            this.textBox_Send.Text = "";
            this.textBox_Send.TextChanged += new System.EventHandler(this.textBox_Send_TextChanged);
            this.textBox_Send.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Send_KeyDown);
            // 
            // label_SendTitle
            // 
            this.label_SendTitle.AutoSize = true;
            this.label_SendTitle.Font = new System.Drawing.Font("宋体", 12F);
            this.label_SendTitle.Location = new System.Drawing.Point(196, 9);
            this.label_SendTitle.Name = "label_SendTitle";
            this.label_SendTitle.Size = new System.Drawing.Size(190, 24);
            this.label_SendTitle.TabIndex = 27;
            this.label_SendTitle.Text = "共发送 0 数据帧";
            // 
            // button_Send
            // 
            this.button_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Send.Font = new System.Drawing.Font("宋体", 12F);
            this.button_Send.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Send.Location = new System.Drawing.Point(27, 84);
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
            this.button_ClearSend.Font = new System.Drawing.Font("宋体", 12F);
            this.button_ClearSend.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_ClearSend.Location = new System.Drawing.Point(27, 134);
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
            this.checkBox_HexSend.Location = new System.Drawing.Point(27, 38);
            this.checkBox_HexSend.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_HexSend.Name = "checkBox_HexSend";
            this.checkBox_HexSend.Size = new System.Drawing.Size(146, 26);
            this.checkBox_HexSend.TabIndex = 24;
            this.checkBox_HexSend.Text = "16进制发送";
            this.checkBox_HexSend.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_HexSend.UseVisualStyleBackColor = true;
            // 
            // groupBox_Rec
            // 
            this.groupBox_Rec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Rec.Controls.Add(this.checkBox_Update);
            this.groupBox_Rec.Controls.Add(this.panel_Rec);
            this.groupBox_Rec.Controls.Add(this.label_RecTitle);
            this.groupBox_Rec.Controls.Add(this.button_Save);
            this.groupBox_Rec.Controls.Add(this.button_ClearRec);
            this.groupBox_Rec.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox_Rec.ForeColor = System.Drawing.Color.Gainsboro;
            this.groupBox_Rec.Location = new System.Drawing.Point(0, 0);
            this.groupBox_Rec.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.groupBox_Rec.Name = "groupBox_Rec";
            this.groupBox_Rec.Size = new System.Drawing.Size(1373, 192);
            this.groupBox_Rec.TabIndex = 1;
            this.groupBox_Rec.TabStop = false;
            this.groupBox_Rec.Text = "接收区";
            this.groupBox_Rec.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox_Paint);
            // 
            // checkBox_Update
            // 
            this.checkBox_Update.AutoSize = true;
            this.checkBox_Update.Font = new System.Drawing.Font("宋体", 11F);
            this.checkBox_Update.ForeColor = System.Drawing.Color.Gainsboro;
            this.checkBox_Update.Location = new System.Drawing.Point(27, 36);
            this.checkBox_Update.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_Update.Name = "checkBox_Update";
            this.checkBox_Update.Size = new System.Drawing.Size(124, 26);
            this.checkBox_Update.TabIndex = 35;
            this.checkBox_Update.Text = "同步最新";
            this.checkBox_Update.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_Update.UseVisualStyleBackColor = true;
            // 
            // panel_Rec
            // 
            this.panel_Rec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Rec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Rec.Controls.Add(this.textBox_Rec);
            this.panel_Rec.Location = new System.Drawing.Point(202, 34);
            this.panel_Rec.Name = "panel_Rec";
            this.panel_Rec.Size = new System.Drawing.Size(1170, 153);
            this.panel_Rec.TabIndex = 0;
            // 
            // textBox_Rec
            // 
            this.textBox_Rec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.textBox_Rec.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_Rec.DetectUrls = false;
            this.textBox_Rec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Rec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.textBox_Rec.Location = new System.Drawing.Point(0, 0);
            this.textBox_Rec.Name = "textBox_Rec";
            this.textBox_Rec.ReadOnly = true;
            this.textBox_Rec.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.textBox_Rec.Size = new System.Drawing.Size(1168, 151);
            this.textBox_Rec.TabIndex = 0;
            this.textBox_Rec.Text = "";
            this.textBox_Rec.SizeChanged += new System.EventHandler(this.textBox_Rec_SizeChanged);
            // 
            // label_RecTitle
            // 
            this.label_RecTitle.AutoSize = true;
            this.label_RecTitle.Font = new System.Drawing.Font("宋体", 12F);
            this.label_RecTitle.ForeColor = System.Drawing.Color.Gainsboro;
            this.label_RecTitle.Location = new System.Drawing.Point(196, 9);
            this.label_RecTitle.Name = "label_RecTitle";
            this.label_RecTitle.Size = new System.Drawing.Size(190, 24);
            this.label_RecTitle.TabIndex = 34;
            this.label_RecTitle.Text = "共接收 0 数据帧";
            // 
            // button_Save
            // 
            this.button_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Save.Font = new System.Drawing.Font("宋体", 12F);
            this.button_Save.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Save.Location = new System.Drawing.Point(25, 92);
            this.button_Save.Margin = new System.Windows.Forms.Padding(4);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(150, 45);
            this.button_Save.TabIndex = 33;
            this.button_Save.Text = "保存接收";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_ClearRec
            // 
            this.button_ClearRec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_ClearRec.Font = new System.Drawing.Font("宋体", 12F);
            this.button_ClearRec.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_ClearRec.Location = new System.Drawing.Point(25, 142);
            this.button_ClearRec.Name = "button_ClearRec";
            this.button_ClearRec.Size = new System.Drawing.Size(150, 45);
            this.button_ClearRec.TabIndex = 30;
            this.button_ClearRec.Text = "清空接收";
            this.button_ClearRec.UseVisualStyleBackColor = true;
            this.button_ClearRec.Click += new System.EventHandler(this.button_ClearRec_Click);
            // 
            // FormData
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1373, 931);
            this.Controls.Add(this.splitContainer);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormData";
            this.Text = "FormData";
            this.Shown += new System.EventHandler(this.FormData_Shown);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.groupBox_Send.ResumeLayout(false);
            this.groupBox_Send.PerformLayout();
            this.panel_Send.ResumeLayout(false);
            this.groupBox_Rec.ResumeLayout(false);
            this.groupBox_Rec.PerformLayout();
            this.panel_Rec.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox groupBox_Rec;
        private System.Windows.Forms.Label label_RecTitle;
        private System.Windows.Forms.GroupBox groupBox_Send;
        private System.Windows.Forms.Label label_SendTitle;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.Button button_ClearSend;
        private System.Windows.Forms.CheckBox checkBox_HexSend;
        private System.Windows.Forms.RichTextBox textBox_Send;
        private System.Windows.Forms.RichTextBox textBox_Rec;
        private System.Windows.Forms.Panel panel_Rec;
        private System.Windows.Forms.Panel panel_Send;
        private System.Windows.Forms.Panel panel_up;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox_Update;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_ClearRec;
    }
}
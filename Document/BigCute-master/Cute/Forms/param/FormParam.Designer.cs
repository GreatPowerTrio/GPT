
namespace Cute
{
    partial class FormParam
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
            this.panel_UpBack = new System.Windows.Forms.Panel();
            this.checkedListBox_Tab = new System.Windows.Forms.CheckedListBox();
            this.panel_Up2 = new System.Windows.Forms.Panel();
            this.panel_Func = new System.Windows.Forms.Panel();
            this.panel_Func1 = new System.Windows.Forms.Panel();
            this.panel_Load = new System.Windows.Forms.Panel();
            this.panel_Add = new System.Windows.Forms.Panel();
            this.panel_Interval = new System.Windows.Forms.Panel();
            this.panel_Up1 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_contain = new System.Windows.Forms.Panel();
            this.panel_Bottom = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panel_UpBack.SuspendLayout();
            this.panel_Func.SuspendLayout();
            this.panel_Func1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.splitContainer.Panel1.Controls.Add(this.panel_UpBack);
            this.splitContainer.Panel1MinSize = 120;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.splitContainer.Panel2.Controls.Add(this.panel1);
            this.splitContainer.Panel2.Controls.Add(this.panel_contain);
            this.splitContainer.Panel2.Controls.Add(this.panel_Bottom);
            this.splitContainer.Panel2.Font = new System.Drawing.Font("宋体", 12F);
            this.splitContainer.Panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.splitContainer.Panel2MinSize = 300;
            this.splitContainer.Size = new System.Drawing.Size(1373, 931);
            this.splitContainer.SplitterDistance = 180;
            this.splitContainer.SplitterWidth = 5;
            this.splitContainer.TabIndex = 3;
            this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer_SplitterMoved);
            // 
            // panel_UpBack
            // 
            this.panel_UpBack.Controls.Add(this.checkedListBox_Tab);
            this.panel_UpBack.Controls.Add(this.panel_Up2);
            this.panel_UpBack.Controls.Add(this.panel_Func);
            this.panel_UpBack.Controls.Add(this.panel_Up1);
            this.panel_UpBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_UpBack.Location = new System.Drawing.Point(0, 0);
            this.panel_UpBack.Name = "panel_UpBack";
            this.panel_UpBack.Size = new System.Drawing.Size(1373, 180);
            this.panel_UpBack.TabIndex = 0;
            this.panel_UpBack.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_UpBack_Paint);
            // 
            // checkedListBox_Tab
            // 
            this.checkedListBox_Tab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox_Tab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.checkedListBox_Tab.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox_Tab.ColumnWidth = 300;
            this.checkedListBox_Tab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkedListBox_Tab.Font = new System.Drawing.Font("宋体", 12F);
            this.checkedListBox_Tab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.checkedListBox_Tab.FormattingEnabled = true;
            this.checkedListBox_Tab.Location = new System.Drawing.Point(12, 53);
            this.checkedListBox_Tab.MultiColumn = true;
            this.checkedListBox_Tab.Name = "checkedListBox_Tab";
            this.checkedListBox_Tab.Size = new System.Drawing.Size(1349, 128);
            this.checkedListBox_Tab.TabIndex = 3;
            this.checkedListBox_Tab.ThreeDCheckBoxes = true;
            this.checkedListBox_Tab.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_Tab_ItemCheck);
            this.checkedListBox_Tab.KeyDown += new System.Windows.Forms.KeyEventHandler(this.checkedListBox_Tab_KeyDown);
            this.checkedListBox_Tab.Leave += new System.EventHandler(this.checkedListBox_Tab_Leave);
            // 
            // panel_Up2
            // 
            this.panel_Up2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(95)))));
            this.panel_Up2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Up2.Location = new System.Drawing.Point(0, 52);
            this.panel_Up2.Name = "panel_Up2";
            this.panel_Up2.Size = new System.Drawing.Size(1373, 1);
            this.panel_Up2.TabIndex = 7;
            // 
            // panel_Func
            // 
            this.panel_Func.Controls.Add(this.panel_Func1);
            this.panel_Func.Controls.Add(this.panel_Interval);
            this.panel_Func.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Func.Location = new System.Drawing.Point(0, 2);
            this.panel_Func.Name = "panel_Func";
            this.panel_Func.Size = new System.Drawing.Size(1373, 50);
            this.panel_Func.TabIndex = 6;
            // 
            // panel_Func1
            // 
            this.panel_Func1.Controls.Add(this.panel_Load);
            this.panel_Func1.Controls.Add(this.panel_Add);
            this.panel_Func1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Func1.Location = new System.Drawing.Point(12, 0);
            this.panel_Func1.Name = "panel_Func1";
            this.panel_Func1.Size = new System.Drawing.Size(236, 50);
            this.panel_Func1.TabIndex = 8;
            // 
            // panel_Load
            // 
            this.panel_Load.BackgroundImage = global::Cute.Properties.Resources.导入;
            this.panel_Load.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_Load.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Load.Location = new System.Drawing.Point(50, 0);
            this.panel_Load.Name = "panel_Load";
            this.panel_Load.Size = new System.Drawing.Size(50, 50);
            this.panel_Load.TabIndex = 0;
            this.panel_Load.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Load_MouseDown);
            this.panel_Load.MouseEnter += new System.EventHandler(this.panel_Load_MouseEnter);
            this.panel_Load.MouseLeave += new System.EventHandler(this.panel_Load_MouseLeave);
            // 
            // panel_Add
            // 
            this.panel_Add.BackgroundImage = global::Cute.Properties.Resources.新建;
            this.panel_Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_Add.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Add.Location = new System.Drawing.Point(0, 0);
            this.panel_Add.Name = "panel_Add";
            this.panel_Add.Size = new System.Drawing.Size(50, 50);
            this.panel_Add.TabIndex = 3;
            this.panel_Add.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Add_MouseDown);
            this.panel_Add.MouseEnter += new System.EventHandler(this.panel_Add_MouseEnter);
            this.panel_Add.MouseLeave += new System.EventHandler(this.panel_Add_MouseLeave);
            // 
            // panel_Interval
            // 
            this.panel_Interval.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Interval.Location = new System.Drawing.Point(0, 0);
            this.panel_Interval.Name = "panel_Interval";
            this.panel_Interval.Size = new System.Drawing.Size(12, 50);
            this.panel_Interval.TabIndex = 1;
            // 
            // panel_Up1
            // 
            this.panel_Up1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(95)))));
            this.panel_Up1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Up1.Location = new System.Drawing.Point(0, 0);
            this.panel_Up1.Name = "panel_Up1";
            this.panel_Up1.Size = new System.Drawing.Size(1373, 2);
            this.panel_Up1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(95)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 744);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1373, 2);
            this.panel1.TabIndex = 0;
            // 
            // panel_contain
            // 
            this.panel_contain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_contain.Location = new System.Drawing.Point(12, 3);
            this.panel_contain.Name = "panel_contain";
            this.panel_contain.Size = new System.Drawing.Size(1349, 739);
            this.panel_contain.TabIndex = 1;
            // 
            // panel_Bottom
            // 
            this.panel_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(95)))));
            this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Bottom.Location = new System.Drawing.Point(0, 0);
            this.panel_Bottom.Name = "panel_Bottom";
            this.panel_Bottom.Size = new System.Drawing.Size(1373, 2);
            this.panel_Bottom.TabIndex = 0;
            // 
            // FormParam
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1373, 931);
            this.Controls.Add(this.splitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormParam";
            this.Text = "FormParam";
            this.Load += new System.EventHandler(this.FormParam_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panel_UpBack.ResumeLayout(false);
            this.panel_Func.ResumeLayout(false);
            this.panel_Func1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.CheckedListBox checkedListBox_Tab;
        private System.Windows.Forms.Panel panel_UpBack;
        private System.Windows.Forms.Panel panel_Up2;
        private System.Windows.Forms.Panel panel_Func;
        private System.Windows.Forms.Panel panel_Interval;
        private System.Windows.Forms.Panel panel_Up1;
        private System.Windows.Forms.Panel panel_Func1;
        private System.Windows.Forms.Panel panel_Add;
        private System.Windows.Forms.Panel panel_Bottom;
        private System.Windows.Forms.Panel panel_contain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel_Load;
    }
}
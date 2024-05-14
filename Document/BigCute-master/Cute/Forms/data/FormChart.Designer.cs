
namespace Cute
{
    partial class FormChart
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel_up = new System.Windows.Forms.Panel();
            this.panel_YLock = new System.Windows.Forms.Panel();
            this.panel_YAuto = new System.Windows.Forms.Panel();
            this.panel_axisbottom = new System.Windows.Forms.Panel();
            this.panel_YFree = new System.Windows.Forms.Panel();
            this.panel_Xaxis = new System.Windows.Forms.Panel();
            this.panel_funcbottom = new System.Windows.Forms.Panel();
            this.panel_Clear = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel_Save = new System.Windows.Forms.Panel();
            this.panel_Add = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_lines = new System.Windows.Forms.Panel();
            this.panel_temp = new System.Windows.Forms.Panel();
            this.label_temp = new System.Windows.Forms.Label();
            this.panel_title = new System.Windows.Forms.Panel();
            this.label_title = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.全部显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_Import = new System.Windows.Forms.Panel();
            this.panel_up.SuspendLayout();
            this.panel_axisbottom.SuspendLayout();
            this.panel_funcbottom.SuspendLayout();
            this.panel_lines.SuspendLayout();
            this.panel_temp.SuspendLayout();
            this.panel_title.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_up
            // 
            this.panel_up.Controls.Add(this.panel_YLock);
            this.panel_up.Controls.Add(this.panel_YAuto);
            this.panel_up.Controls.Add(this.panel_axisbottom);
            this.panel_up.Controls.Add(this.panel_funcbottom);
            this.panel_up.Controls.Add(this.panel_lines);
            this.panel_up.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_up.Location = new System.Drawing.Point(0, 0);
            this.panel_up.Name = "panel_up";
            this.panel_up.Size = new System.Drawing.Size(1369, 50);
            this.panel_up.TabIndex = 1;
            // 
            // panel_YLock
            // 
            this.panel_YLock.BackgroundImage = global::Cute.Properties.Resources.y轴固定;
            this.panel_YLock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_YLock.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_YLock.Location = new System.Drawing.Point(1179, 0);
            this.panel_YLock.Name = "panel_YLock";
            this.panel_YLock.Size = new System.Drawing.Size(50, 50);
            this.panel_YLock.TabIndex = 4;
            this.panel_YLock.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_YLock_MouseDown);
            this.panel_YLock.MouseEnter += new System.EventHandler(this.panel_YLock_MouseEnter);
            this.panel_YLock.MouseLeave += new System.EventHandler(this.panel_YLock_MouseLeave);
            // 
            // panel_YAuto
            // 
            this.panel_YAuto.BackgroundImage = global::Cute.Properties.Resources.y轴自适应;
            this.panel_YAuto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_YAuto.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_YAuto.Location = new System.Drawing.Point(1129, 0);
            this.panel_YAuto.Name = "panel_YAuto";
            this.panel_YAuto.Size = new System.Drawing.Size(50, 50);
            this.panel_YAuto.TabIndex = 6;
            this.panel_YAuto.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_YAuto_MouseDown);
            this.panel_YAuto.MouseEnter += new System.EventHandler(this.panel_YAuto_MouseEnter);
            this.panel_YAuto.MouseLeave += new System.EventHandler(this.panel_YAuto_MouseLeave);
            // 
            // panel_axisbottom
            // 
            this.panel_axisbottom.Controls.Add(this.panel_YFree);
            this.panel_axisbottom.Controls.Add(this.panel_Xaxis);
            this.panel_axisbottom.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_axisbottom.Location = new System.Drawing.Point(1029, 0);
            this.panel_axisbottom.Name = "panel_axisbottom";
            this.panel_axisbottom.Size = new System.Drawing.Size(100, 50);
            this.panel_axisbottom.TabIndex = 6;
            // 
            // panel_YFree
            // 
            this.panel_YFree.BackgroundImage = global::Cute.Properties.Resources.y轴自由;
            this.panel_YFree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_YFree.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_YFree.Location = new System.Drawing.Point(50, 0);
            this.panel_YFree.Name = "panel_YFree";
            this.panel_YFree.Size = new System.Drawing.Size(50, 50);
            this.panel_YFree.TabIndex = 6;
            this.panel_YFree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_YFree_MouseDown);
            this.panel_YFree.MouseEnter += new System.EventHandler(this.panel_YFree_MouseEnter);
            this.panel_YFree.MouseLeave += new System.EventHandler(this.panel_YFree_MouseLeave);
            // 
            // panel_Xaxis
            // 
            this.panel_Xaxis.BackgroundImage = global::Cute.Properties.Resources.关闭自动更新;
            this.panel_Xaxis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_Xaxis.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Xaxis.Location = new System.Drawing.Point(0, 0);
            this.panel_Xaxis.Name = "panel_Xaxis";
            this.panel_Xaxis.Size = new System.Drawing.Size(50, 50);
            this.panel_Xaxis.TabIndex = 6;
            this.panel_Xaxis.Tag = "";
            this.panel_Xaxis.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Xaixs_MouseDown);
            this.panel_Xaxis.MouseEnter += new System.EventHandler(this.panel_Xaixs_MouseEnter);
            this.panel_Xaxis.MouseLeave += new System.EventHandler(this.panel_Xaixs_MouseLeave);
            this.panel_Xaxis.MouseHover += new System.EventHandler(this.panel_Xaxis_MouseHover);
            // 
            // panel_funcbottom
            // 
            this.panel_funcbottom.Controls.Add(this.panel_Clear);
            this.panel_funcbottom.Controls.Add(this.panel_Import);
            this.panel_funcbottom.Controls.Add(this.panel6);
            this.panel_funcbottom.Controls.Add(this.panel_Save);
            this.panel_funcbottom.Controls.Add(this.panel_Add);
            this.panel_funcbottom.Controls.Add(this.panel2);
            this.panel_funcbottom.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_funcbottom.Location = new System.Drawing.Point(760, 0);
            this.panel_funcbottom.Name = "panel_funcbottom";
            this.panel_funcbottom.Size = new System.Drawing.Size(269, 50);
            this.panel_funcbottom.TabIndex = 6;
            // 
            // panel_Clear
            // 
            this.panel_Clear.BackgroundImage = global::Cute.Properties.Resources.清除;
            this.panel_Clear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_Clear.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Clear.Location = new System.Drawing.Point(153, 0);
            this.panel_Clear.Name = "panel_Clear";
            this.panel_Clear.Size = new System.Drawing.Size(50, 50);
            this.panel_Clear.TabIndex = 9;
            this.panel_Clear.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Clear_MouseDown);
            this.panel_Clear.MouseEnter += new System.EventHandler(this.panel_Clear_MouseEnter);
            this.panel_Clear.MouseLeave += new System.EventHandler(this.panel_Clear_MouseLeave);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(95)))));
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(266, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(3, 50);
            this.panel6.TabIndex = 8;
            // 
            // panel_Save
            // 
            this.panel_Save.BackgroundImage = global::Cute.Properties.Resources.保存;
            this.panel_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_Save.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Save.Location = new System.Drawing.Point(53, 0);
            this.panel_Save.Name = "panel_Save";
            this.panel_Save.Size = new System.Drawing.Size(50, 50);
            this.panel_Save.TabIndex = 6;
            this.panel_Save.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Save_MouseDown);
            this.panel_Save.MouseEnter += new System.EventHandler(this.panel_Save_MouseEnter);
            this.panel_Save.MouseLeave += new System.EventHandler(this.panel_Save_MouseLeave);
            // 
            // panel_Add
            // 
            this.panel_Add.BackgroundImage = global::Cute.Properties.Resources.新建;
            this.panel_Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_Add.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Add.Location = new System.Drawing.Point(3, 0);
            this.panel_Add.Name = "panel_Add";
            this.panel_Add.Size = new System.Drawing.Size(50, 50);
            this.panel_Add.TabIndex = 7;
            this.panel_Add.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Add_MouseDown);
            this.panel_Add.MouseEnter += new System.EventHandler(this.panel_Add_MouseEnter);
            this.panel_Add.MouseLeave += new System.EventHandler(this.panel_Add_MouseLeave);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(95)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(3, 50);
            this.panel2.TabIndex = 6;
            // 
            // panel_lines
            // 
            this.panel_lines.AutoScrollMargin = new System.Drawing.Size(5, 5);
            this.panel_lines.Controls.Add(this.panel_temp);
            this.panel_lines.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_lines.Location = new System.Drawing.Point(0, 0);
            this.panel_lines.Name = "panel_lines";
            this.panel_lines.Size = new System.Drawing.Size(760, 50);
            this.panel_lines.TabIndex = 1;
            // 
            // panel_temp
            // 
            this.panel_temp.Controls.Add(this.label_temp);
            this.panel_temp.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_temp.Location = new System.Drawing.Point(0, 0);
            this.panel_temp.Name = "panel_temp";
            this.panel_temp.Size = new System.Drawing.Size(95, 50);
            this.panel_temp.TabIndex = 4;
            // 
            // label_temp
            // 
            this.label_temp.AutoSize = true;
            this.label_temp.Font = new System.Drawing.Font("宋体", 12F);
            this.label_temp.ForeColor = System.Drawing.Color.Gainsboro;
            this.label_temp.Location = new System.Drawing.Point(13, 14);
            this.label_temp.Name = "label_temp";
            this.label_temp.Size = new System.Drawing.Size(59, 20);
            this.label_temp.TabIndex = 0;
            this.label_temp.Text = "Line1";
            // 
            // panel_title
            // 
            this.panel_title.Controls.Add(this.label_title);
            this.panel_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_title.Location = new System.Drawing.Point(0, 50);
            this.panel_title.Name = "panel_title";
            this.panel_title.Size = new System.Drawing.Size(1369, 38);
            this.panel_title.TabIndex = 3;
            this.panel_title.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_title_Paint);
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label_title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.label_title.Location = new System.Drawing.Point(648, 15);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(85, 18);
            this.label_title.TabIndex = 3;
            this.label_title.Text = "0 - 0  :  0 / 0";
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.chart.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.Center;
            this.chart.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.chart.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(95)))));
            this.chart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chart.BorderlineWidth = 0;
            this.chart.BorderSkin.BackColor = System.Drawing.Color.Transparent;
            this.chart.BorderSkin.PageColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            chartArea1.AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal;
            chartArea1.Area3DStyle.LightStyle = System.Windows.Forms.DataVisualization.Charting.LightStyle.Realistic;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            chartArea1.AxisX.LabelStyle.Format = "N3";
            chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(158)))));
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(105)))));
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(158)))));
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(158)))));
            chartArea1.AxisX2.LineColor = System.Drawing.Color.Bisque;
            chartArea1.AxisX2.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(158)))));
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            chartArea1.AxisY.LabelStyle.Format = "N3";
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(158)))));
            chartArea1.AxisY.MajorGrid.Interval = 0D;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(105)))));
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(158)))));
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(158)))));
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            chartArea1.BorderColor = System.Drawing.Color.Empty;
            chartArea1.CursorX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            chartArea1.CursorY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            chartArea1.Name = "Area1";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.ContextMenuStrip = this.contextMenuStrip;
            this.chart.Location = new System.Drawing.Point(0, 88);
            this.chart.Name = "chart";
            series1.ChartArea = "Area1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Series1";
            series1.ToolTip = "#VALX,#VAL";
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(1369, 457);
            this.chart.TabIndex = 5;
            this.chart.Text = "chart";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全部显示ToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(149, 32);
            // 
            // 全部显示ToolStripMenuItem
            // 
            this.全部显示ToolStripMenuItem.Name = "全部显示ToolStripMenuItem";
            this.全部显示ToolStripMenuItem.Size = new System.Drawing.Size(148, 28);
            this.全部显示ToolStripMenuItem.Text = "全部显示";
            this.全部显示ToolStripMenuItem.Click += new System.EventHandler(this.全部显示ToolStripMenuItem_Click);
            // 
            // panel_Import
            // 
            this.panel_Import.BackgroundImage = global::Cute.Properties.Resources.文件夹导入;
            this.panel_Import.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_Import.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Import.Location = new System.Drawing.Point(103, 0);
            this.panel_Import.Name = "panel_Import";
            this.panel_Import.Size = new System.Drawing.Size(50, 50);
            this.panel_Import.TabIndex = 6;
            this.panel_Import.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Import_MouseDown);
            this.panel_Import.MouseEnter += new System.EventHandler(this.panel_Import_MouseEnter);
            this.panel_Import.MouseLeave += new System.EventHandler(this.panel_Import_MouseLeave);
            // 
            // FormChart
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1369, 545);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.panel_title);
            this.Controls.Add(this.panel_up);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormChart";
            this.Text = "FormChart";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormChart_FormClosing);
            this.SizeChanged += new System.EventHandler(this.FormChart_SizeChanged);
            this.panel_up.ResumeLayout(false);
            this.panel_axisbottom.ResumeLayout(false);
            this.panel_funcbottom.ResumeLayout(false);
            this.panel_lines.ResumeLayout(false);
            this.panel_temp.ResumeLayout(false);
            this.panel_temp.PerformLayout();
            this.panel_title.ResumeLayout(false);
            this.panel_title.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_up;
        private System.Windows.Forms.Panel panel_lines;
        private System.Windows.Forms.Panel panel_title;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Panel panel_temp;
        private System.Windows.Forms.Label label_temp;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Panel panel_Xaxis;
        private System.Windows.Forms.Panel panel_YFree;
        private System.Windows.Forms.Panel panel_funcbottom;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel_Save;
        private System.Windows.Forms.Panel panel_Add;
        private System.Windows.Forms.Panel panel_axisbottom;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel_Clear;
        private System.Windows.Forms.Panel panel_YLock;
        private System.Windows.Forms.Panel panel_YAuto;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 全部显示ToolStripMenuItem;
        private System.Windows.Forms.Panel panel_Import;
    }
}
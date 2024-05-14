namespace TCP_Helper.Forms.Chart
{
	partial class Form_Chart
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
			this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.DebugBox = new Sunny.UI.UITextBox();
			this.Chart_Btn = new Sunny.UI.UIButton();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
			this.SuspendLayout();
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
			this.chart.Location = new System.Drawing.Point(3, 94);
			this.chart.Name = "chart";
			series1.ChartArea = "Area1";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
			series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
			series1.Name = "Series1";
			series1.ToolTip = "#VALX,#VAL";
			this.chart.Series.Add(series1);
			this.chart.Size = new System.Drawing.Size(895, 365);
			this.chart.TabIndex = 6;
			this.chart.Text = "chart";
			// 
			// DebugBox
			// 
			this.DebugBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.DebugBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.DebugBox.Location = new System.Drawing.Point(3, 57);
			this.DebugBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.DebugBox.MinimumSize = new System.Drawing.Size(1, 16);
			this.DebugBox.Name = "DebugBox";
			this.DebugBox.Padding = new System.Windows.Forms.Padding(5);
			this.DebugBox.ShowText = false;
			this.DebugBox.Size = new System.Drawing.Size(894, 29);
			this.DebugBox.TabIndex = 7;
			this.DebugBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.DebugBox.Watermark = "";
			// 
			// Chart_Btn
			// 
			this.Chart_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Chart_Btn.FillColorGradient = true;
			this.Chart_Btn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Chart_Btn.Location = new System.Drawing.Point(352, 491);
			this.Chart_Btn.MinimumSize = new System.Drawing.Size(1, 1);
			this.Chart_Btn.Name = "Chart_Btn";
			this.Chart_Btn.Size = new System.Drawing.Size(188, 48);
			this.Chart_Btn.TabIndex = 9;
			this.Chart_Btn.Text = "启动";
			this.Chart_Btn.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Chart_Btn.Click += new System.EventHandler(this.Chart_Btn_Click);
			// 
			// timer1
			// 
			this.timer1.Interval = 2;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// Form_Chart
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Snow;
			this.ClientSize = new System.Drawing.Size(910, 580);
			this.Controls.Add(this.Chart_Btn);
			this.Controls.Add(this.DebugBox);
			this.Controls.Add(this.chart);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Form_Chart";
			this.Text = "Form_Chart";
			((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.DataVisualization.Charting.Chart chart;
		private Sunny.UI.UITextBox DebugBox;
        private Sunny.UI.UIButton Chart_Btn;
        private System.Windows.Forms.Timer timer1;
    }
}
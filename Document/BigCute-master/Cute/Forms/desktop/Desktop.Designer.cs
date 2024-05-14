
namespace Cute
{
    partial class Desktop
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Desktop));
            this.panel_formLeft = new System.Windows.Forms.Panel();
            this.TabPanel = new System.Windows.Forms.Panel();
            this.图像接收 = new System.Windows.Forms.Panel();
            this.参数调节 = new System.Windows.Forms.Panel();
            this.数据接收 = new System.Windows.Forms.Panel();
            this.基础收发 = new System.Windows.Forms.Panel();
            this.panel_tabHighLight = new System.Windows.Forms.Panel();
            this.通信配置 = new System.Windows.Forms.Panel();
            this.panel_TabUp = new System.Windows.Forms.Panel();
            this.panel_formImage = new System.Windows.Forms.Panel();
            this.panel_formUp = new System.Windows.Forms.Panel();
            this.pictureBox_formMinSize = new System.Windows.Forms.PictureBox();
            this.pictureBox_formMaxSize = new System.Windows.Forms.PictureBox();
            this.pictureBox_formClose = new System.Windows.Forms.PictureBox();
            this.panel_Debug = new System.Windows.Forms.Panel();
            this.panel_Bottom = new System.Windows.Forms.Panel();
            this.panel_formLeft.SuspendLayout();
            this.TabPanel.SuspendLayout();
            this.基础收发.SuspendLayout();
            this.panel_formUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_formMinSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_formMaxSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_formClose)).BeginInit();
            this.panel_Debug.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_formLeft
            // 
            this.panel_formLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel_formLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panel_formLeft.Controls.Add(this.TabPanel);
            this.panel_formLeft.Controls.Add(this.panel_formImage);
            this.panel_formLeft.Location = new System.Drawing.Point(1, 1);
            this.panel_formLeft.Name = "panel_formLeft";
            this.panel_formLeft.Size = new System.Drawing.Size(200, 997);
            this.panel_formLeft.TabIndex = 1;
            // 
            // TabPanel
            // 
            this.TabPanel.Controls.Add(this.图像接收);
            this.TabPanel.Controls.Add(this.参数调节);
            this.TabPanel.Controls.Add(this.数据接收);
            this.TabPanel.Controls.Add(this.基础收发);
            this.TabPanel.Controls.Add(this.通信配置);
            this.TabPanel.Controls.Add(this.panel_TabUp);
            this.TabPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TabPanel.Location = new System.Drawing.Point(0, 120);
            this.TabPanel.Name = "TabPanel";
            this.TabPanel.Size = new System.Drawing.Size(200, 403);
            this.TabPanel.TabIndex = 7;
            // 
            // 图像接收
            // 
            this.图像接收.BackgroundImage = global::Cute.Properties.Resources.图像接收;
            this.图像接收.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.图像接收.Dock = System.Windows.Forms.DockStyle.Top;
            this.图像接收.Location = new System.Drawing.Point(0, 302);
            this.图像接收.Name = "图像接收";
            this.图像接收.Size = new System.Drawing.Size(200, 75);
            this.图像接收.TabIndex = 5;
            // 
            // 参数调节
            // 
            this.参数调节.BackgroundImage = global::Cute.Properties.Resources.参数调节;
            this.参数调节.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.参数调节.Dock = System.Windows.Forms.DockStyle.Top;
            this.参数调节.Location = new System.Drawing.Point(0, 227);
            this.参数调节.Name = "参数调节";
            this.参数调节.Size = new System.Drawing.Size(200, 75);
            this.参数调节.TabIndex = 4;
            // 
            // 数据接收
            // 
            this.数据接收.BackgroundImage = global::Cute.Properties.Resources.数据接收;
            this.数据接收.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.数据接收.Dock = System.Windows.Forms.DockStyle.Top;
            this.数据接收.Location = new System.Drawing.Point(0, 152);
            this.数据接收.Name = "数据接收";
            this.数据接收.Size = new System.Drawing.Size(200, 75);
            this.数据接收.TabIndex = 3;
            // 
            // 基础收发
            // 
            this.基础收发.BackgroundImage = global::Cute.Properties.Resources.基础收发;
            this.基础收发.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.基础收发.Controls.Add(this.panel_tabHighLight);
            this.基础收发.Dock = System.Windows.Forms.DockStyle.Top;
            this.基础收发.Location = new System.Drawing.Point(0, 77);
            this.基础收发.Name = "基础收发";
            this.基础收发.Size = new System.Drawing.Size(200, 75);
            this.基础收发.TabIndex = 2;
            // 
            // panel_tabHighLight
            // 
            this.panel_tabHighLight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(165)))), ((int)(((byte)(205)))));
            this.panel_tabHighLight.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_tabHighLight.Location = new System.Drawing.Point(0, 0);
            this.panel_tabHighLight.Name = "panel_tabHighLight";
            this.panel_tabHighLight.Size = new System.Drawing.Size(4, 75);
            this.panel_tabHighLight.TabIndex = 2;
            // 
            // 通信配置
            // 
            this.通信配置.BackgroundImage = global::Cute.Properties.Resources.通信配置;
            this.通信配置.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.通信配置.Dock = System.Windows.Forms.DockStyle.Top;
            this.通信配置.Location = new System.Drawing.Point(0, 2);
            this.通信配置.Name = "通信配置";
            this.通信配置.Size = new System.Drawing.Size(200, 75);
            this.通信配置.TabIndex = 1;
            // 
            // panel_TabUp
            // 
            this.panel_TabUp.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_TabUp.Location = new System.Drawing.Point(0, 0);
            this.panel_TabUp.Name = "panel_TabUp";
            this.panel_TabUp.Size = new System.Drawing.Size(200, 2);
            this.panel_TabUp.TabIndex = 0;
            // 
            // panel_formImage
            // 
            this.panel_formImage.BackgroundImage = global::Cute.Properties.Resources.logo;
            this.panel_formImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_formImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_formImage.Location = new System.Drawing.Point(0, 0);
            this.panel_formImage.Name = "panel_formImage";
            this.panel_formImage.Size = new System.Drawing.Size(200, 120);
            this.panel_formImage.TabIndex = 4;
            // 
            // panel_formUp
            // 
            this.panel_formUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_formUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panel_formUp.Controls.Add(this.pictureBox_formMinSize);
            this.panel_formUp.Controls.Add(this.pictureBox_formMaxSize);
            this.panel_formUp.Controls.Add(this.pictureBox_formClose);
            this.panel_formUp.Location = new System.Drawing.Point(1424, 1);
            this.panel_formUp.Name = "panel_formUp";
            this.panel_formUp.Size = new System.Drawing.Size(150, 50);
            this.panel_formUp.TabIndex = 0;
            // 
            // pictureBox_formMinSize
            // 
            this.pictureBox_formMinSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox_formMinSize.Image = global::Cute.Properties.Resources.最小化;
            this.pictureBox_formMinSize.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_formMinSize.Name = "pictureBox_formMinSize";
            this.pictureBox_formMinSize.Size = new System.Drawing.Size(50, 50);
            this.pictureBox_formMinSize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_formMinSize.TabIndex = 4;
            this.pictureBox_formMinSize.TabStop = false;
            // 
            // pictureBox_formMaxSize
            // 
            this.pictureBox_formMaxSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox_formMaxSize.Image = global::Cute.Properties.Resources.放大;
            this.pictureBox_formMaxSize.Location = new System.Drawing.Point(50, 0);
            this.pictureBox_formMaxSize.Name = "pictureBox_formMaxSize";
            this.pictureBox_formMaxSize.Size = new System.Drawing.Size(50, 50);
            this.pictureBox_formMaxSize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_formMaxSize.TabIndex = 5;
            this.pictureBox_formMaxSize.TabStop = false;
            // 
            // pictureBox_formClose
            // 
            this.pictureBox_formClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox_formClose.Image = global::Cute.Properties.Resources.删除;
            this.pictureBox_formClose.Location = new System.Drawing.Point(100, 0);
            this.pictureBox_formClose.Name = "pictureBox_formClose";
            this.pictureBox_formClose.Size = new System.Drawing.Size(50, 50);
            this.pictureBox_formClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_formClose.TabIndex = 6;
            this.pictureBox_formClose.TabStop = false;
            // 
            // panel_Debug
            // 
            this.panel_Debug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Debug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panel_Debug.Controls.Add(this.panel_Bottom);
            this.panel_Debug.Location = new System.Drawing.Point(201, 52);
            this.panel_Debug.Name = "panel_Debug";
            this.panel_Debug.Size = new System.Drawing.Size(1373, 946);
            this.panel_Debug.TabIndex = 3;
            // 
            // panel_Bottom
            // 
            this.panel_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Bottom.Location = new System.Drawing.Point(0, 931);
            this.panel_Bottom.Name = "panel_Bottom";
            this.panel_Bottom.Size = new System.Drawing.Size(1373, 15);
            this.panel_Bottom.TabIndex = 0;
            this.panel_Bottom.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Bottom_Paint);
            // 
            // Desktop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1591, 1000);
            this.Controls.Add(this.panel_Debug);
            this.Controls.Add(this.panel_formLeft);
            this.Controls.Add(this.panel_formUp);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Desktop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cute";
            this.SizeChanged += new System.EventHandler(this.Form_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Desktop_Paint);
            this.panel_formLeft.ResumeLayout(false);
            this.TabPanel.ResumeLayout(false);
            this.基础收发.ResumeLayout(false);
            this.panel_formUp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_formMinSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_formMaxSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_formClose)).EndInit();
            this.panel_Debug.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_formImage;
        private System.Windows.Forms.Panel panel_formLeft;
        private System.Windows.Forms.Panel panel_tabHighLight;
        private System.Windows.Forms.Panel panel_formUp;
        private System.Windows.Forms.Panel panel_Debug;
        private System.Windows.Forms.PictureBox pictureBox_formClose;
        private System.Windows.Forms.PictureBox pictureBox_formMaxSize;
        private System.Windows.Forms.Panel panel_Bottom;
        private System.Windows.Forms.PictureBox pictureBox_formMinSize;
        private System.Windows.Forms.Panel TabPanel;
        private System.Windows.Forms.Panel 图像接收;
        private System.Windows.Forms.Panel 参数调节;
        private System.Windows.Forms.Panel 数据接收;
        private System.Windows.Forms.Panel 基础收发;
        private System.Windows.Forms.Panel 通信配置;
        private System.Windows.Forms.Panel panel_TabUp;
    }
}


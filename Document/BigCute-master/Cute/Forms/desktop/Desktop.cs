using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cute
{
    public partial class Desktop : System.Windows.Forms.Form
    {
        SelectTab _SelectTabs = new SelectTab();
        Paraments _CuteParam = new Paraments(true);

        FormLAN _FormLAN = new FormLAN();
        FormComConfig _FormComConfig = new FormComConfig();
        FormBasic _FormBasic = new FormBasic();
        FormData _FormData = new FormData();
        FormParam _FormParam = new FormParam();
        FormPic _FormPic = new FormPic();
        FormSerialComBaud _FormSerialComBaud = new FormSerialComBaud();

        public Desktop()
        {
            InitializeComponent();

            #region 窗体属性设置            
            _FormSerialComBaud.TopLevel = false;
            _FormSerialComBaud.Parent = this;
            _FormSerialComBaud.Location = new Point(panel_formImage.Right, 1);

            _FormLAN.TopLevel = false;
            _FormLAN.Parent = this;
            _FormLAN.Location = new Point(panel_formImage.Right, 1);

            _FormComConfig.TopLevel = false;
            _FormComConfig.Parent = panel_Debug;
            _FormComConfig.Anchor = (AnchorStyles)15;

            _FormBasic.TopLevel = false;
            _FormBasic.Parent = panel_Debug;
            _FormBasic.Anchor = (AnchorStyles)15;

            _FormData.TopLevel = false;
            _FormData.Parent = panel_Debug;
            _FormData.Anchor = (AnchorStyles)15;

            _FormParam.TopLevel = false;
            _FormParam.Parent = panel_Debug;
            _FormParam.Anchor = (AnchorStyles)15;

            _FormPic.TopLevel = false;
            _FormPic.Parent = panel_Debug;
            _FormPic.Anchor = (AnchorStyles)15;
            #endregion

            #region 回调函数设置
            MouseDoubleClick += FormMaxSize;
            panel_formImage.MouseDown += FormMouseDown;
            panel_formImage.MouseMove += FormMouseMove;
            panel_formLeft.MouseDown += FormMouseDown;
            panel_formLeft.MouseMove += FormMouseMove;
            panel_formUp.MouseDown += FormMouseDown;
            panel_formUp.MouseMove += FormMouseMove;
            panel_Debug.MouseDown += FormMouseDown;
            panel_Debug.MouseMove += FormMouseMove;
            pictureBox_formMaxSize.MouseClick += FormMaxSize;
            pictureBox_formMaxSize.MouseEnter += pictureBox_formMaxSize_MouseEnter;
            pictureBox_formMaxSize.MouseLeave += pictureBox_formMaxSize_MouseLeave;
            pictureBox_formMinSize.MouseClick += FormMinSize;
            pictureBox_formMinSize.MouseEnter += pictureBox_formMinSize_MouseEnter;
            pictureBox_formMinSize.MouseLeave += pictureBox_formMinSize_MouseLeave;
            pictureBox_formClose.MouseClick += FormClose;
            pictureBox_formClose.MouseEnter += pictureBox_formClose_MouseEnter;
            pictureBox_formClose.MouseLeave += pictureBox_formClose_MouseLeave;
            _SelectTabs.SelectedIndexChanging += SelectedIndexChanging;
            _SelectTabs.SelectedIndexChanged += SelectedIndexChanged;
            _FormLAN.GetLocalIP = _FormComConfig.GetLocalIP;
            _FormLAN.GetEncoding = _FormComConfig.GetEncoding;
            _FormSerialComBaud.GetEncoding = _FormComConfig.GetEncoding;
            _FormSerialComBaud.GetConfiguration = _FormComConfig.GetConfiguration;
            _FormSerialComBaud.DataReceived += DataReceived;
            _FormSerialComBaud.DataHandler += DataHandler;
            _FormLAN.DataReceived += DataReceived;
            _FormLAN.DataHandler += DataHandler;
            _FormComConfig.ModeChanged = FormHeaderChanged;
            _FormSerialComBaud.SerialClose += _FormBasic.SourseClose;

            _FormBasic.SendByteBuffer = _FormSerialComBaud.AddSendBuffer;
            _FormBasic.SendStrBuffer = _FormSerialComBaud.AddSendBuffer;
            _FormBasic.ClearSendBytes = _FormSerialComBaud.ClearSendBytes;
            _FormData.SendByteBuffer = _FormSerialComBaud.AddSendBuffer;
            _FormData.SendStrBuffer = _FormSerialComBaud.AddSendBuffer;
            _FormParam.SendByteBuffer = _FormSerialComBaud.AddSendBuffer;
            #endregion

            #region 不同DPI适配
            this.MinimumSize = new Size(Width, Height);
            ///右上角大小配置
            panel_formUp.Size = new Size( 3 * (panel_Debug.Top - 2), panel_Debug.Top - 2);
            pictureBox_formMaxSize.Width = pictureBox_formClose.Width = pictureBox_formMinSize.Width = panel_Debug.Top - 2;
            panel_formUp.Location = new Point(panel_Debug.Right - panel_formUp.Width, 1);
            Size size = new Size(panel_Debug.Width, panel_Debug.Height - 15);
            _FormComConfig.Form_Changed(size);
            _FormBasic.Form_Changed(size);
            _FormData.Form_Changed(size);
            _FormParam.Form_Changed(size);
            _FormPic.Form_Changed(size);
            _FormSerialComBaud.Form_Changed((float)Width / 1580, (float)Height / 1000);
            _FormLAN.Form_Changed((float)Width / 1580, (float)Height / 1000);
            panel_tabHighLight.BackColor = _CuteParam.Desktop_Border;
            ////页面选择初始化
            PanelFormLeft_Set(TabPanel, 基础收发);
            #endregion
        }

        #region 页面选择栏初始化

        /// <summary>
        /// 页面选择栏初始化
        /// </summary>
        /// <param name="cons">页面panel容器</param>
        /// <param name="page">初始页面</param>
        private void PanelFormLeft_Set(Control cons, Panel page)
        {
            foreach (Control con in cons.Controls)
            {
                if (con as Panel != null && con.Name != "panel_tabHighLight" && con.Name != "panel_TabUp")
                {
                    Panel panel = con as Panel;
                    ///按panel名新建页
                    _SelectTabs.Add(panel);
                    ///添加按下回调函数
                    panel.MouseEnter += PanelTab_MouseEnter;
                    panel.MouseLeave += PanelTab_MouseLeave;
                    panel.MouseDown += PanelTab_SelectedIndexChanging;
                }
            }
            Shown += new EventHandler((obj, arg) => { _SelectTabs.SelectPanel(page); });
        }


        #endregion

        #region page页切换

        /// <summary>
        /// Page页切换前发生
        /// </summary>
        /// <param name="newIndex">切换后选择项下标</param>
        private void SelectedIndexChanging(int newIndex)
        {
            if(_SelectTabs.SelectedIndex != -1)
            {
                Panel panel = _SelectTabs.GetTab(_SelectTabs.SelectedIndex).panel;
                panel.BackgroundImage = global::Cute.Properties.Resources.FindBitmap(panel.Name);

                //隐藏窗体
                if (panel == 通信配置)
                {
                    _FormComConfig.Hide();
                }
                else if(panel == 基础收发)
                {
                    _FormBasic.Hide();
                }
                else if(panel == 数据接收)
                {
                    _FormData.Hide();
                    _FormData._FormChart.TimerStop();
                }
                else if(panel == 参数调节)
                {
                    _FormParam.Hide();
                    _FormParam.TimeStop();
                }
                else if(panel == 图像接收)
                {
                    _FormPic.Hide();
                }
            }
        }

        ///Page页切换后发生
        private void SelectedIndexChanged()
        {
            if (_SelectTabs.SelectedIndex != -1)
            {
                Panel panel = _SelectTabs.GetTab(_SelectTabs.SelectedIndex).panel;
                panel_tabHighLight.Parent = panel;
                Bitmap bitmap = global::Cute.Properties.Resources.FindBitmap(panel.Name);
                _CuteParam.ThreColorChange(bitmap, _CuteParam.Desktop_MenuChooseBack, _CuteParam.Desktop_MenuChooseWord);
                panel.BackgroundImage = bitmap;
                this.Text = panel.Name;
                //显示窗体
                if (panel == 通信配置)
                {
                    _FormComConfig.Show();
                }
                else if(panel == 基础收发)
                {
                    _FormBasic.Show();
                }
                else if(panel == 数据接收)
                {
                    _FormData.Show();
                    _FormData._FormChart.TimerStart();
                }
                else if(panel == 参数调节)
                {
                    _FormParam.Show();
                    _FormParam.TimeStart();
                }
                else if(panel == 图像接收)
                {
                    _FormPic.Show();
                }
            }
            panel_formLeft.Focus(); //切换时将焦点转移到无用位置
        }

        ///选择页按下时发生
        private void PanelTab_SelectedIndexChanging(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _SelectTabs.SelectPanel(sender as Panel);
            }
        }

            //鼠标进入选择页时发生
         private void PanelTab_MouseEnter(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            if (_SelectTabs.GetTab(_SelectTabs.SelectedIndex).panel != panel)
            {
                Bitmap bitmap = global::Cute.Properties.Resources.FindBitmap(panel.Name);
                _CuteParam.ThreColorChange(bitmap, _CuteParam.Desktop_MenuPreBack, _CuteParam.Desktop_MenuChooseWord);
                panel.BackgroundImage = bitmap;
            }
        }

        private void PanelTab_MouseLeave(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            if (_SelectTabs.GetTab(_SelectTabs.SelectedIndex).panel != panel)
            {
                panel.BackgroundImage = global::Cute.Properties.Resources.FindBitmap(panel.Name);
            }
        }

        #endregion

        #region 窗口拖动、窗口最大化、窗口最小化、窗口关闭
        /// <summary>
        /// 窗口拖动、放大缩小
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    if (WindowState == FormWindowState.Maximized) break;
                    Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X > 32767) vPoint.X -= 65535;
                    if (vPoint.Y > 32767) vPoint.Y -= 65535;
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)(13);
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)(16);
                        else m.Result = (IntPtr)(10);
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)(14);
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)(17);
                        else m.Result = (IntPtr)(11);
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)(12);
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)(15);
                    break;
                case 0x0201://鼠标左键按下的消息 用于实现拖动窗口功能
                    m.Msg = 0x00A1;//更改消息为非客户区按下鼠标
                    m.LParam = IntPtr.Zero;//默认值
                    m.WParam = new IntPtr(2);//鼠标放在标题栏内
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        /// <summary>
        /// 第二种方法移动窗体
        /// </summary>
        Point npoint = new Point();
        private void FormMouseDown(object sender, MouseEventArgs e)
        {
            npoint = new Point(e.X, e.Y);
        }

        private void FormMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Cursor == Cursors.Arrow)
            {
                Location = new Point(Location.X + e.X - npoint.X, Location.Y + e.Y - npoint.Y);
            }
        }

        private void FormMinSize(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        private void FormMaxSize(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (WindowState == FormWindowState.Normal)
                {
                    Rectangle area = Screen.FromControl(this).WorkingArea;
                    MaximizedBounds = new Rectangle(new Point(0, 0), area.Size);
                    WindowState = FormWindowState.Maximized;
                }
                else
                {
                    WindowState = FormWindowState.Normal;
                }
            }
        }
        private void Form_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                pictureBox_formMaxSize.Image = Properties.Resources.缩小;
            }
            else
            {
                pictureBox_formMaxSize.Image = Properties.Resources.放大;
            }
            //强制重绘所有控件
            this.Refresh();
        }

        private void FormClose(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Task task = Task.Run(() =>
                {
                    FunctionClose();
                });
                task.ContinueWith(t =>
                {
                    _FormComConfig.Dispose();
                    _FormLAN.Dispose();
                    _FormBasic.Dispose();
                    _FormData.Dispose();
                    _FormParam.Dispose();
                    _FormPic.Dispose();
                    this.Close();
                });
            }
        }

        private void FunctionClose()
        {
            _FormBasic.FunctionClose();
            _FormData._FormChart.TimerStop();
            _FormParam.FunctionClose();
            _FormSerialComBaud.FunctionClose();
            _FormLAN.FunctionClose();
            while (_FormSerialComBaud.Running || _FormLAN.Running) ; //等待串口、局域网关闭
            System.Threading.Thread.Sleep(50);
        }

        /// <summary>
        /// 点击任务栏图标实现窗体的显示切换
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000;// | 0x80000 | 0x40000;
                return cp;
            }
        }


        #region 右上角按键颜色改变

        private void pictureBox_formMinSize_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.最小化;
            _CuteParam.BackColorChange(bitmap, _CuteParam.Desktop_FunctionBack, _CuteParam.Desktop_FunctionPre);
            pictureBox_formMinSize.Image = bitmap;
        }

        private void pictureBox_formMinSize_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_formMinSize.Image = Properties.Resources.最小化;
        }

        private void pictureBox_formMaxSize_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap;
            if (WindowState == FormWindowState.Maximized)
            {
                bitmap = Properties.Resources.缩小;
            }
            else
            {
                bitmap = Properties.Resources.放大;
            }
            _CuteParam.BackColorChange(bitmap, _CuteParam.Desktop_FunctionBack, _CuteParam.Desktop_FunctionPre);
            pictureBox_formMaxSize.Image = bitmap;
        }

        private void pictureBox_formMaxSize_MouseLeave(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                pictureBox_formMaxSize.Image = Properties.Resources.缩小;
            }
            else
            {
                pictureBox_formMaxSize.Image = Properties.Resources.放大;
            }
        }
        private void pictureBox_formClose_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.删除;
            _CuteParam.BackColorChange(bitmap, _CuteParam.Desktop_FunctionBack, _CuteParam.Desktop_FunctionPre);
            pictureBox_formClose.Image = bitmap;
        }

        private void pictureBox_formClose_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_formClose.Image = Properties.Resources.删除;
        }
        #endregion

        #endregion

        #region 数据整体调控
        /// <summary>
        /// 数据接收分配
        /// </summary>
        /// <param name="buff"></param>
        private void DataReceived(byte[] buff)
        {
            if(panel_tabHighLight.Parent == 基础收发)
            {
                _FormBasic.DataReceived(buff);
            }
            else if(panel_tabHighLight.Parent == 数据接收)
            {
                _FormData.DataReceived(buff);
            }
            else if(panel_tabHighLight.Parent == 参数调节)
            {
                _FormParam.DataReceived(buff);
            }
            else if(panel_tabHighLight.Parent == 图像接收)
            {
                _FormPic.DataReceived(buff);
            }
        }

        private void DataHandler(List<object> arg)
        {
            if (panel_tabHighLight.Parent == 基础收发)
            {
                _FormBasic.DataHandler(arg);
            }
            else if (panel_tabHighLight.Parent == 数据接收)
            {
                _FormData.DataHandler(arg);
            }
            else if(panel_tabHighLight.Parent == 参数调节)
            {
                _FormParam.DataHandler(arg);
            }
            else if (panel_tabHighLight.Parent == 图像接收)
            {
                _FormPic.DataHandler(arg);
            }
        }

        bool LastComModeIsSerial = true;
        private void FormHeaderChanged(string msg)
        {
            if(msg == "Serial")
            {
                if(_FormLAN.IsOpen)
                {
                    MessageBox.Show("请先关闭局域网并重新选择通信方式", "Error");
                }
                else
                {
                    if(!LastComModeIsSerial)
                    {
                        _FormBasic.SendByteBuffer = _FormSerialComBaud.AddSendBuffer;
                        _FormBasic.SendStrBuffer = _FormSerialComBaud.AddSendBuffer;
                        _FormBasic.ClearSendBytes = _FormSerialComBaud.ClearSendBytes;
                        _FormData.SendByteBuffer = _FormSerialComBaud.AddSendBuffer;
                        _FormData.SendStrBuffer = _FormSerialComBaud.AddSendBuffer;
                        _FormParam.SendByteBuffer = _FormSerialComBaud.AddSendBuffer;
                        _FormLAN.Hide();
                        _FormSerialComBaud.Show();
                    }
                    LastComModeIsSerial = true;
                }
            }
            else
            {
                if (_FormSerialComBaud.isOpen)
                {
                    MessageBox.Show("请先关闭串口并重新选择通信方式", "Error");
                }
                else
                {
                    if(LastComModeIsSerial)
                    {
                        _FormBasic.SendByteBuffer = _FormLAN.AddSendBuffer;
                        _FormBasic.SendStrBuffer = _FormLAN.AddSendBuffer;
                        _FormBasic.ClearSendBytes = _FormLAN.ClearSendBytes;
                        _FormData.SendByteBuffer = _FormLAN.AddSendBuffer;
                        _FormData.SendStrBuffer = _FormLAN.AddSendBuffer;
                        _FormParam.SendByteBuffer = _FormLAN.AddSendBuffer;
                        _FormSerialComBaud.Hide();
                        _FormLAN.Show();
                    }
                    _FormLAN.LAN_Mode = msg;
                    LastComModeIsSerial = false;
                }
            }
        }

        #endregion

        #region 边框重绘
        /// <summary>
        /// 画边框线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Desktop_Paint(object sender, PaintEventArgs e)
        {
            if(WindowState == FormWindowState.Normal)
            {
                e.Graphics.DrawRectangle(new Pen(_CuteParam.Desktop_Border, 1), new Rectangle(new Point(0, 0), new Size(Width - 1, Height - 1)));
                e.Graphics.DrawLine(new Pen(_CuteParam.Desktop_Border, 1), new Point(0, Height - 2), new Point(Width - 1, Height - 2));
            }
        }
        private void panel_Bottom_Paint(object sender, PaintEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                e.Graphics.DrawLine(new Pen(_CuteParam.Desktop_Border, 1), new Point(0, panel_Bottom.Height), new Point(panel_Bottom.Width - 1, panel_Bottom.Height));
        }
        #endregion


    }
}

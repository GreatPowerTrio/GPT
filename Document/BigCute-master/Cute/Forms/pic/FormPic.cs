using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace Cute
{
    public partial class FormPic : System.Windows.Forms.Form
    {
        Paraments param = new Paraments(); //软件参数
        private readonly DataBuffer buffer = new DataBuffer(); //数据接收缓存
        List<Bitmap> _Bitmaps = new List<Bitmap>(); //图像缓存
        System.Windows.Forms.Timer timer = new Timer(); //计时器
        Cute.pic.ImageUnpack imageUnpack = new pic.ImageUnpack(); //解包类
        Cute.pic.Algorithm _Algorithm = new pic.Algorithm();
        List<PictureBox> _PicBoxes = new List<PictureBox>(); //图像框列表
        List<Label> _PicLabels = new List<Label>(); //标签列表
        public FormPic()
        {
            InitializeComponent();
            SetTag(this);

            #region 回调函数设置
            panel_Ave.MouseDown += Panel_Ave_MouseDown;
            panel_Ave.MouseEnter += Panel_Ave_MouseEnter; 
            panel_Ave.MouseLeave += Panel_Ave_MouseLeave;
            panel_Otsu.MouseDown += Panel_Otsu_MouseDown;
            panel_Otsu.MouseEnter += Panel_Otsu_MouseEnter;
            panel_Otsu.MouseLeave += Panel_Otsu_MouseLeave;
            panel_Wallner.MouseDown += Panel_Wallner_MouseDown;
            panel_Wallner.MouseEnter += Panel_Wallner_MouseEnter;
            panel_Wallner.MouseLeave += Panel_Wallner_MouseLeave;
            panel_SelfBinary.MouseDown += Panel_SelfBinary_MouseDown;
            panel_SelfBinary.MouseEnter += Panel_SelfBinary_MouseEnter;
            panel_SelfBinary.MouseLeave += Panel_SelfBinary_MouseLeave;
            pictureBox_MasterRight.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_MasterLeft.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_MasterLeft.Paint += pictureBox_Master_Paint;
            pictureBox_MasterLeft.MouseDown += pictureBox_Master_MouseDown;
            pictureBox_MasterLeft.MouseMove += pictureBox_Master_MouseMove;
            pictureBox_MasterLeft.MouseUp += PictureBox_Master_MouseUp;
            pictureBox_MasterRight.Paint += pictureBox_Master_Paint;
            pictureBox_MasterRight.MouseDown += pictureBox_Master_MouseDown;
            pictureBox_MasterRight.MouseMove += pictureBox_Master_MouseMove;
            pictureBox_MasterRight.MouseUp += PictureBox_Master_MouseUp;
            _PicBoxes.Add(pictureBox_Record1);
            _PicBoxes.Add(pictureBox_Record2);
            _PicBoxes.Add(pictureBox_Record3);
            _PicLabels.Add(label_Record1);
            _PicLabels.Add(label_Record2);
            _PicLabels.Add(label_Record3);
            for (int index = 0; index < _PicBoxes.Count; index++)
                _PicBoxes[index].MouseDown += LBox_MouseDown;
            trackBar.ValueChanged += TrackBar_ValueChanged;
            trackBar.KeyDown += TrackBar_KeyDown;
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            _Algorithm.ShowTextEnd = ShowText;


            #endregion

            #region ToolTips
            Action<Control, string> action = (c, s) =>
            {
                ToolTip tool = new ToolTip
                {
                    AutoPopDelay = 5000,
                    InitialDelay = 500,
                    ReshowDelay = 0,
                    ShowAlways = true
                };
                tool.SetToolTip(c, s);
            };
            action.Invoke(panel_Build, "编译");
            action.Invoke(panel_picload, "导入图片");
            action.Invoke(panel_picsave, "保存图片");
            action.Invoke(panel_picclear, "清空所有");
            action.Invoke(panel_Origin, "原始显示");
            action.Invoke(panel_Zoom, "缩放显示");
            action.Invoke(panel_Ave, "均值法二值化");
            action.Invoke(panel_Otsu, "大津法二值化");
            action.Invoke(panel_Wallner, "Wallner二值化");
            action.Invoke(panel_SelfBinary, "自定义预处理");
            action.Invoke(panel_Detector1, "边界识别");
            action.Invoke(panel_Detector2, "自定义识别");
            #endregion

            #region 部分控件初始化            
            Bitmap bitmap0 = Properties.Resources.Zoom;
            param.BackColorChange(bitmap0, param.Pic_Word, param.Pic_FunctionChoose);
            panel_Zoom.BackgroundImage = bitmap0;
            Clear();
            #endregion

        }

        #region 控件大小随窗体大小变化而变化
        private void SetTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Font.Size + ";"
                    + (float)con.Left / con.Parent.Width + ";"
                    + (float)con.Top / con.Parent.Height + ";"
                    + (float)con.Width / con.Parent.Width + ";"
                    + (float)con.Height / con.Parent.Height;
                if (con.Controls.Count > 0)
                {
                    SetTag(con);
                }
            }
        }

        private void SetControls(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                con.Left = Convert.ToInt32(Convert.ToSingle(mytag[1]) * con.Parent.Width);
                con.Top = Convert.ToInt32(Convert.ToSingle(mytag[2]) * con.Parent.Height);
                con.Width = Convert.ToInt32(Convert.ToSingle(mytag[3]) * con.Parent.Width);
                con.Height = Convert.ToInt32(Convert.ToSingle(mytag[4]) * con.Parent.Height);
                if (con.Controls.Count > 0 && !con.Name.Contains("panel_Master"))
                {
                    SetControls(con);
                }
            }
        }

        /// <summary>
        /// 第一次显示窗体时控件位置调整
        /// </summary>
        public void Form_Changed(Size size)
        {
            this.Size = size;
            SetControls(this);
            SetControls(panel_master);
            SetControls(panel_Record);
            Hide();
        }

        /// <summary>
        /// 大小更改时布局保持不变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitContainer_SizeChanged(object sender, EventArgs e)
        {
            SetControls(panel_master);
            SetControls(panel_Record);
            ReLocation(panel_funcleft);
            ReLocation(panel_Binaryzation);
            ReLocation(panel1);
        }

        private void trackBar_SizeChanged(object sender, EventArgs e)
        {
            Label_TotalNumShow(_Bitmaps.Count.ToString());
        }

        private void splitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SetControls(panel_master);
            SetControls(panel_Record);
            ReLocation(panel_funcleft);
            ReLocation(panel_Binaryzation);
            ReLocation(panel1);
        }

        private void splitContainer_Slave_SplitterMoved(object sender, SplitterEventArgs e)
        {
            SetControls(panel_Record);
        }

        private void ReLocation(Control con)
        {
            string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
            con.Width = Convert.ToInt32(Convert.ToSingle(mytag[3]) * con.Parent.Width);
        }
        #endregion

        #region 数据处理

        /// <summary>
        /// 接收新数据
        /// </summary>
        /// <param name="data"></param>
        public void DataReceived(byte[] data)
        {
            buffer.AddendData(data);
        }

        /// <summary>
        /// 数据处理
        /// </summary>
        /// <param name="arg"></param>
        public void DataHandler(List<object> arg)
        {
            if (buffer.HasChanged())
            {
                var newdata = buffer.GetDeleteData(-1);
                var res = imageUnpack.Unpack(newdata);
                if(res.Count > 0)
                {
                    _Bitmaps.AddRange(res);
                    TotalNumChanged();
                }
            }
        }
        #endregion

        #region 文本显示

        private void ShowText(string text)
        {
            if (richTextBox.InvokeRequired)
            {
                Action<string> action = ShowText;
                richTextBox.Invoke(action, text);
            }
            else
            {
                richTextBox.AppendText(text);
            }
            //自动显示最底端
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.ScrollToCaret();
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            richTextBox.Font = new Font("宋体", 12, FontStyle.Regular, GraphicsUnit.Point);
        }
        #endregion

        #region 清空接收

        private void Clear()
        {
            _Bitmaps.Clear();
            imageUnpack.Number = 0;
            LBoxRange = Size.Empty;
            TotalNumChanged();
            LBox_Update(0);
            MBox_Update(0);
            LastFocusIndex = -1;
            LBoxRange = Size.Empty;
            richTextBox.Clear();
        }

        #endregion

        #region 功能区

        #region 图片批量操作

        #region 图片导入
        private void panel_picload_MouseDown(object sender, MouseEventArgs e)
        {
            using(var openfile = new OpenFileDialog()
            {
                Title = "选择图片",
                Filter = "图片|* .jpg;* .png;* .bmp",
                Multiselect = true
            })
            {
                if (openfile.ShowDialog() == DialogResult.OK)
                {
                    Task task = Task.Run(() =>
                    {
                        try
                        {
                            foreach (string file in openfile.FileNames)
                            {
                                Bitmap map = new Bitmap(file)
                                {
                                    Tag = Path.GetFileNameWithoutExtension(file),
                                };
                                _Bitmaps.Add(map);
                            }
                            TotalNumChanged();
                            MessageBox.Show("导入完成", "Tip");
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message, "导入失败");
                        }
                    });  
                }
            }
        }

        private void panel_picload_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.文件夹导入;
            param.BackColorChange(bitmap, param.Pic_FunctionBack, param.Pic_FunctionPre);
            panel_picload.BackgroundImage = bitmap;
        }

        private void panel_picload_MouseLeave(object sender, EventArgs e)
        {
            panel_picload.BackgroundImage = Properties.Resources.文件夹导入;
        }
        #endregion

        #region 图片保存
        private void panel_picsave_MouseDown(object sender, MouseEventArgs e)
        {
            using (var folder = new FolderBrowserDialog()
            {
                RootFolder = Environment.SpecialFolder.Desktop,
                Description = "请选择文件夹"
            })
            {
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    Task task = Task.Run(() =>
                    {
                        try
                        {
                            foreach (Bitmap map in _Bitmaps)
                            {
                                map.Save(folder.SelectedPath + @"\" + map.Tag + ".png", ImageFormat.Png);
                            }
                            MessageBox.Show("保存完成", "Tip");
                        }
                        catch
                        {
                            MessageBox.Show("保存失败", "Tip");
                        }
                    });
                }
            }
        }

        private void panel_picsave_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.保存;
            param.BackColorChange(bitmap, param.Pic_FunctionBack, param.Pic_FunctionPre);
            panel_picsave.BackgroundImage = bitmap;
        }

        private void panel_picsave_MouseLeave(object sender, EventArgs e)
        {
            panel_picsave.BackgroundImage = Properties.Resources.保存;
        }

        #endregion

        #region 图片清空
        private void panel_picclear_MouseDown(object sender, MouseEventArgs e)
        {
            Clear();
        }

        private void panel_picclear_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.清除;
            param.BackColorChange(bitmap, param.Pic_FunctionBack, param.Pic_FunctionPre);
            panel_picclear.BackgroundImage = bitmap;
        }

        private void panel_picclear_MouseLeave(object sender, EventArgs e)
        {
            panel_picclear.BackgroundImage = Properties.Resources.清除;
        }
        #endregion

        #endregion

        #region 编译
        private void panel_Build_MouseDown(object sender, MouseEventArgs e)
        {
            _Algorithm.Build();
            TrackBar_ValueChanged(null, null);
        }

        private void panel_Build_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.编译;
            param.BackColorChange(bitmap, param.Pic_FunctionBack, param.Pic_FunctionPre);
            panel_Build.BackgroundImage = bitmap;
        }

        private void panel_Build_MouseLeave(object sender, EventArgs e)
        {
            panel_Build.BackgroundImage = Properties.Resources.编译;
        }
        #endregion

        #region 显示模式

        int PicShowFlag = 1; //图像显示模式

        #region 原始
        private void panel_Origin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && PicShowFlag != 0)
            {
                int temp = PicShowFlag; PicShowFlag = 0;
                if (temp == 1) panel_Zoom_MouseLeave(null, null);
                panel_Origin_MouseEnter(null, null);
                TrackBar_ValueChanged(null, null);
            }
        }

        private void panel_Origin_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.Origin;
            if (PicShowFlag != 0)
                param.BackColorChange(bitmap, param.Pic_FunctionBack, param.Pic_FunctionPre);
            else
                param.BackColorChange(bitmap,
                    new Color[] { param.Pic_FunctionBack, param.Pic_Word },
                    new Color[] { param.Pic_FunctionPre, param.Pic_FunctionChoose });
            panel_Origin.BackgroundImage = bitmap;
        }

        private void panel_Origin_MouseLeave(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.Origin;
            if (PicShowFlag == 0) param.BackColorChange(bitmap, param.Pic_Word, param.Pic_FunctionChoose);
            panel_Origin.BackgroundImage = bitmap;
        }
        #endregion

        #region 放大
        private void panel_Zoom_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && PicShowFlag != 1)
            {
                int temp = PicShowFlag; PicShowFlag = 1;
                if (temp == 0) panel_Origin_MouseLeave(null, null);
                panel_Zoom_MouseEnter(null, null);
                TrackBar_ValueChanged(null, null);
            }
        }

        private void panel_Zoom_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.Zoom;
            if (PicShowFlag != 1)
                param.BackColorChange(bitmap, param.Pic_FunctionBack, param.Pic_FunctionPre);
            else
                param.BackColorChange(bitmap,
                    new Color[] { param.Pic_FunctionBack, param.Pic_Word },
                    new Color[] { param.Pic_FunctionPre, param.Pic_FunctionChoose });
            panel_Zoom.BackgroundImage = bitmap;
        }

        private void panel_Zoom_MouseLeave(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.Zoom;
            if (PicShowFlag == 1) param.BackColorChange(bitmap, param.Pic_Word, param.Pic_FunctionChoose);
            panel_Zoom.BackgroundImage = bitmap;
        }
        #endregion

        #endregion

        #region 二值化

        int BinaryFlag = -1; //二值化模式

        #region AVE
        private void Panel_Ave_MouseLeave(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.Ave;
            if (BinaryFlag == 0) param.BackColorChange(bitmap, param.Pic_Word, param.Pic_FunctionChoose);
            panel_Ave.BackgroundImage = bitmap;
        }

        private void Panel_Ave_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int temp = BinaryFlag;
                if (BinaryFlag == 0) BinaryFlag = -1;
                else BinaryFlag = 0;
                if (temp == 1) Panel_Otsu_MouseLeave(null, null);
                else if(temp == 2) Panel_Wallner_MouseLeave(null, null);
                else if(temp == 3) Panel_SelfBinary_MouseLeave(null, null);
                Panel_Ave_MouseEnter(null, null);
                TrackBar_ValueChanged(null, null);
            }
        }

        private void Panel_Ave_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.Ave;
            if (BinaryFlag != 0)
                param.BackColorChange(bitmap, param.Pic_FunctionBack, param.Pic_FunctionPre);
            else
                param.BackColorChange(bitmap,
                    new Color[] { param.Pic_FunctionBack, param.Pic_Word },
                    new Color[] { param.Pic_FunctionPre, param.Pic_FunctionChoose });
            panel_Ave.BackgroundImage = bitmap;
        }

        #endregion

        #region OTSU
        private void Panel_Otsu_MouseLeave(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.Otsu;
            if (BinaryFlag == 1) param.BackColorChange(bitmap, param.Pic_Word, param.Pic_FunctionChoose);
            panel_Otsu.BackgroundImage = bitmap;
        }

        private void Panel_Otsu_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.Otsu;
            if (BinaryFlag != 1)
                param.BackColorChange(bitmap, param.Pic_FunctionBack, param.Pic_FunctionPre);
            else
                param.BackColorChange(bitmap,
                    new Color[] { param.Pic_FunctionBack, param.Pic_Word },
                    new Color[] { param.Pic_FunctionPre, param.Pic_FunctionChoose });
            panel_Otsu.BackgroundImage = bitmap;
        }

        private void Panel_Otsu_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int temp = BinaryFlag;
                if (BinaryFlag == 1) BinaryFlag = -1;
                else BinaryFlag = 1;
                if (temp == 0) Panel_Ave_MouseLeave(null, null);
                else if (temp == 2) Panel_Wallner_MouseLeave(null, null);
                else if (temp == 3) Panel_SelfBinary_MouseLeave(null, null);
                Panel_Otsu_MouseEnter(null, null);
                TrackBar_ValueChanged(null, null);
            }
        }
        #endregion

        #region wallner自适应二值化
        private void Panel_Wallner_MouseLeave(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.Wallner;
            if (BinaryFlag == 2) param.BackColorChange(bitmap, param.Pic_Word, param.Pic_FunctionChoose);
            panel_Wallner.BackgroundImage = bitmap;
        }

        private void Panel_Wallner_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.Wallner;
            if (BinaryFlag != 2)
                param.BackColorChange(bitmap, param.Pic_FunctionBack, param.Pic_FunctionPre);
            else
                param.BackColorChange(bitmap,
                    new Color[] { param.Pic_FunctionBack, param.Pic_Word },
                    new Color[] { param.Pic_FunctionPre, param.Pic_FunctionChoose });
            panel_Wallner.BackgroundImage = bitmap;
        }

        private void Panel_Wallner_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int temp = BinaryFlag;
                if (BinaryFlag == 2) BinaryFlag = -1;
                else BinaryFlag = 2;
                if (temp == 0) Panel_Ave_MouseLeave(null, null);
                else if (temp == 1) Panel_Otsu_MouseLeave(null, null);
                else if (temp == 3) Panel_SelfBinary_MouseLeave(null, null);
                Panel_Wallner_MouseEnter(null, null);
                TrackBar_ValueChanged(null, null);
            }
        }
        #endregion

        #region 自定义方法
        private void Panel_SelfBinary_MouseLeave(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.self;
            if (BinaryFlag == 3) param.BackColorChange(bitmap, param.Pic_Word, param.Pic_FunctionChoose);
            panel_SelfBinary.BackgroundImage = bitmap;
        }

        private void Panel_SelfBinary_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.self;
            if (BinaryFlag != 3)
                param.BackColorChange(bitmap, param.Pic_FunctionBack, param.Pic_FunctionPre);
            else
                param.BackColorChange(bitmap,
                    new Color[] { param.Pic_FunctionBack, param.Pic_Word },
                    new Color[] { param.Pic_FunctionPre, param.Pic_FunctionChoose });
            panel_SelfBinary.BackgroundImage = bitmap;
        }

        private void Panel_SelfBinary_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int temp = BinaryFlag;
                if (BinaryFlag == 3) BinaryFlag = -1;
                else BinaryFlag = 3;
                if (temp == 0) Panel_Ave_MouseLeave(null, null);
                else if (temp == 1) Panel_Otsu_MouseLeave(null, null);
                else if (temp == 2) Panel_Wallner_MouseLeave(null, null);
                Panel_SelfBinary_MouseEnter(null, null);
                TrackBar_ValueChanged(null, null);
            }
        }
        #endregion

        #endregion

        #region 识别处理

        int DetectorFlag = -1; //识别模式

        private void panel_Detector1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int temp = DetectorFlag;
                if (DetectorFlag == 0) DetectorFlag = -1;
                else DetectorFlag = 0;
                if (temp == 1) panel_Detector2_MouseLeave(null, null);
                panel_Detector1_MouseEnter(null, null);
                TrackBar_ValueChanged(null, null);
            }
        }

        private void panel_Detector1_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.detector1;
            if (DetectorFlag != 0)
                param.BackColorChange(bitmap, param.Pic_FunctionBack, param.Pic_FunctionPre);
            else
                param.BackColorChange(bitmap,
                    new Color[] { param.Pic_FunctionBack, param.Pic_Word },
                    new Color[] { param.Pic_FunctionPre, param.Pic_FunctionChoose });
            panel_Detector1.BackgroundImage = bitmap;
        }

        private void panel_Detector1_MouseLeave(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.detector1;
            if (DetectorFlag == 0) param.BackColorChange(bitmap, param.Pic_Word, param.Pic_FunctionChoose);
            panel_Detector1.BackgroundImage = bitmap;
        }

        private void panel_Detector2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int temp = DetectorFlag;
                if (DetectorFlag == 1) DetectorFlag = -1;
                else DetectorFlag = 1;
                if (temp == 0) panel_Detector1_MouseLeave(null, null);
                panel_Detector2_MouseEnter(null, null);
                TrackBar_ValueChanged(null, null);
            }
        }

        private void panel_Detector2_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.detector2;
            if (DetectorFlag != 1)
                param.BackColorChange(bitmap, param.Pic_FunctionBack, param.Pic_FunctionPre);
            else
                param.BackColorChange(bitmap,
                    new Color[] { param.Pic_FunctionBack, param.Pic_Word },
                    new Color[] { param.Pic_FunctionPre, param.Pic_FunctionChoose });
            panel_Detector2.BackgroundImage = bitmap;
        }

        private void panel_Detector2_MouseLeave(object sender, EventArgs e)
        {
            Bitmap bitmap = Properties.Resources.detector2;
            if (DetectorFlag == 1) param.BackColorChange(bitmap, param.Pic_Word, param.Pic_FunctionChoose);
            panel_Detector2.BackgroundImage = bitmap;
        }

        #endregion

        #endregion

        #region 定时翻阅图片
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (trackBar.Value < trackBar.Maximum) trackBar.Value++;
        }

        private void checkBox_timer_CheckedChanged(object sender, EventArgs e)
        {
            //if(checkBox_timer.Checked)
            //{
            //    try
            //    {
            //        int interval = 1000;
            //        if (textBox_dt.Text != string.Empty)
            //        {
            //            interval = (int)float.Parse(textBox_dt.Text);
            //            if (interval <= 0) interval = 1;
            //        }
            //        textBox_dt.Text = Convert.ToString(interval);
            //        timer.Interval = interval;
            //        if(!timer.Enabled) timer.Start();
            //    }
            //    catch(Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "间隔时间输入");
            //        checkBox_timer.Checked = false;
            //    }
            //}
            //else
            //{
            //    if(timer.Enabled) timer.Stop();
            //}
        }

        #endregion

        #region 滑动条值更改
        /// <summary>
        /// 滑动条自动随值的更改而更改
        /// </summary>
        /// <param name="num"></param>
        
        private void TotalNumChanged()
        {
            int lastvalue = trackBar.Value;
            int index = _Bitmaps.Count == 0 ? 0 : _Bitmaps.Count - 1;
            if (trackBar.Value == trackBar.Maximum)
            {
                if (index > trackBar.Maximum) trackBar.Value = trackBar.Maximum = index;
                else trackBar.Maximum = trackBar.Value = index;
            }
            else
            {
                if (index < trackBar.Value)
                {
                    trackBar.Value = index;
                }
                trackBar.Maximum = index;
            }
            if (lastvalue == trackBar.Value || _Bitmaps.Count == 1) TrackBar_ValueChanged(null, null);

            Label_TotalNumShow(_Bitmaps.Count.ToString());
        }

        bool Updateflag = true; //允许图像更新标志位
        Size LBoxRange = Size.Empty; //起始点，长度
        int LastFocusIndex = -1;
        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (Updateflag == false) return ; //若上次图像未更新完成 则此次不更新
            Updateflag = false;

            int index = trackBar.Value;
            LBoxRange.Height = _Bitmaps.Count > 3 ? 3 : _Bitmaps.Count;
            if (index < LBoxRange.Width)
            {
                LBoxRange.Width = index;
            }
            else if (index > LBoxRange.Width + LBoxRange.Height - 1)
            {
                LBoxRange.Width = index - LBoxRange.Height + 1;
            }
            else if (LBoxRange.Height + LBoxRange.Width > _Bitmaps.Count)
            {
                LBoxRange.Width = _Bitmaps.Count - LBoxRange.Height;
            }
            LBox_Update(index);
            MBox_Update(index);

            Updateflag = true;
        }

        private void Label_TotalNumShow(string text)
        {
            if(label_totalNum.InvokeRequired)
            {
                Action<string> action = Label_TotalNumShow;
                label_totalNum.Invoke(action, text);
            }
            else
            {
                label_totalNum.Text = "Total:" + text;
                label_totalNum.Location = new Point(label_totalNum.Parent.Width - label_totalNum.Width - 12, label_totalNum.Top);
            }
        }
        #endregion

        #region 小图框部分更新

        #region UI部分
        /// <summary>
        /// 小图框更新
        /// </summary>
        private void LBox_Update(int index)
        {
            //图片标签更新
            for (int i = 2; i >= 0; i--)
            {
                if (LBoxRange.Height > i)
                {
                    var image = _Bitmaps[LBoxRange.Width + i];
                    LBox_ImageUpdate(_PicBoxes[i], image);
                    LBox_LabelUpdate(_PicLabels[i], string.Format("{0} ; {1}*{2} ", (string)image.Tag, image.Width, image.Height));
                }
                else
                {
                    LBox_ImageUpdate(_PicBoxes[i], null);
                    LBox_LabelUpdate(_PicLabels[i], string.Format("NULL"));
                }
            }

            //焦点更新
            if (_Bitmaps.Count != 0)
            {
                int NowFocusIndex = index - LBoxRange.Width;
                if(NowFocusIndex != LastFocusIndex)
                {
                    if (LastFocusIndex != -1) LBox_FocusUpdate(_PicBoxes[LastFocusIndex], false);
                    LBox_FocusUpdate(_PicBoxes[NowFocusIndex], true);
                }
                LastFocusIndex = NowFocusIndex;
            }
            else
            {
                if (LastFocusIndex != -1) LBox_FocusUpdate(_PicBoxes[LastFocusIndex], false);
                LastFocusIndex = -1;
            }
        }


        /// <summary>
        /// 图片更新
        /// </summary>
        /// <param name="box"></param>
        /// <param name="bitmap"></param>
        private void LBox_ImageUpdate(PictureBox box, Bitmap bitmap)
        {
            if(box.InvokeRequired)
            {
                Action<PictureBox, Bitmap> action = LBox_ImageUpdate;
                box.Invoke(action, new object[] { box, bitmap });
            }
            else
            {
                box.BackgroundImage = bitmap;
            }
        }

        /// <summary>
        /// 焦点更新
        /// </summary>
        /// <param name="box"></param>
        /// <param name="focus"></param>
        private void LBox_FocusUpdate(PictureBox box, bool focus)
        {
            if (box.InvokeRequired)
            {
                Action<PictureBox, bool> action = LBox_FocusUpdate;
                box.Invoke(action, new object[] { box, focus });
            }
            else
            {
                if (focus) box.BorderStyle = BorderStyle.Fixed3D;
                else box.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        /// <summary>
        /// 标签更新
        /// </summary>
        /// <param name="text"></param>
        private void LBox_LabelUpdate(Label label, string text)
        {
            if(label.InvokeRequired)
            {
                Action<Label, string> action = LBox_LabelUpdate;
                label.Invoke(action, new object[] { label, text });
            }
            else
            {
                label.Text = text;
            }
        }
        #endregion

        #region 鼠标按键操作

        private void LBox_MouseDown(object sender, MouseEventArgs e)
        {
            trackBar.Focus();
            if(_Bitmaps.Count > 0)
            {
                PictureBox box = sender as PictureBox;
                int value = Convert.ToInt32(box.Name.Last() - '0') - 1 + LBoxRange.Width;
                if (value >= trackBar.Minimum && value <= trackBar.Maximum)
                {
                    trackBar.Value = value;
                }
            }
        }

        private void TrackBar_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if (_Bitmaps.Count > 0)
                {
                    if (_Bitmaps.Count == 1) Clear();
                    else
                    {
                        _Bitmaps.RemoveAt(trackBar.Value);
                        LBoxRange.Height--;
                        TotalNumChanged();
                    }
                }
            }
        }

        #endregion

        #endregion

        #region 大图框操作

        #region UI更新
        /// <summary>
        /// 图片更新
        /// </summary>
        /// <param name="box"></param>
        /// <param name="bitmap"></param>
        private void MBox_Update(int index)
        {
            if(_Bitmaps.Count > 0)
            {
                Bitmap bitmap = _Bitmaps[index];
                Bitmap res;
                if (BinaryFlag != -1) res = _Algorithm.ImagePreMethod[BinaryFlag].Invoke(bitmap);
                else res = bitmap;
                if (DetectorFlag != -1) res = _Algorithm.ImageDetector[DetectorFlag].Invoke(res);
                Master_ImageUpdate(pictureBox_MasterLeft, bitmap);
                Master_ImageUpdate(pictureBox_MasterRight, res);
            }
            else
            {
                Master_ImageUpdate(pictureBox_MasterLeft, null); 
                Master_ImageUpdate(pictureBox_MasterRight, null);
            }
        }

        /// <summary>
        /// 图片更新
        /// </summary>
        /// <param name="box"></param>
        /// <param name="bitmap"></param>
        private void Master_ImageUpdate(PictureBox box, Bitmap bitmap)
        {
            if (box.InvokeRequired)
            {
                Action<PictureBox, Bitmap> action = Master_ImageUpdate;
                box.Invoke(action, new object[] { box, bitmap });
            }
            else
            {
                //设置box属性
                if (bitmap == null) box.BorderStyle = BorderStyle.None;
                else
                {
                    if (box.BorderStyle == BorderStyle.None) box.BorderStyle = BorderStyle.FixedSingle;
                    if(PicShowFlag == 0) //原始大小显示
                        box.Size = bitmap.Size;
                    else //缩放模式
                    {
                        float RatioX = (float)bitmap.Width / box.Parent.Width;
                        float RatioY = (float)bitmap.Height / box.Parent.Height;
                        float Ratio = RatioX < RatioY ? RatioY : RatioX;
                        box.Size = new Size((int)(bitmap.Width / Ratio), (int)(bitmap.Height / Ratio));
                    }
                    box.Location = new Point((box.Parent.Width - box.Width) / 2, (box.Parent.Height - box.Height) / 2);
                }
                box.Image = bitmap;
            }
        }

        /// <summary>
        /// 图片更新
        /// </summary>
        /// <param name="box"></param>
        /// <param name="bitmap"></param>
        private void OnlyImageUpdate(PictureBox box, Bitmap bitmap)
        {
            if (box.InvokeRequired)
            {
                Action<PictureBox, Bitmap> action = OnlyImageUpdate;
                box.Invoke(action, new object[] { box, bitmap });
            }
            else
            {
                box.Image = bitmap;
            }
        }
        #endregion

        #region 键鼠操作
        Point LPoint = Point.Empty;
        Point RPoint = Point.Empty;
        private void pictureBox_Master_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox box = sender as PictureBox;
                if (box.Name.Contains("Left"))
                    LPoint = e.Location;
                else
                    RPoint = e.Location;
                box.Invalidate();
            }
        }

        private void pictureBox_Master_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                PictureBox box = sender as PictureBox;
                if (box.Name.Contains("Left"))
                    LPoint = e.Location;
                else
                    RPoint = e.Location;
                box.Invalidate();
            }
        }

        private void PictureBox_Master_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox box = sender as PictureBox;
                LPoint = RPoint = Point.Empty;
                box.Invalidate();
            }
            LPoint = RPoint = Point.Empty;
        }

        private void pictureBox_Master_Paint(object sender, PaintEventArgs e)
        {
            PictureBox box = sender as PictureBox;
            //重新加载图像
            if (box.Image != null)
            {
                e.Graphics.DrawImage(box.Image, new RectangleF(new PointF(0, 0), box.Size));

                if ((LPoint != Point.Empty && box.Name.Contains("Left")) || (RPoint != Point.Empty && box.Name.Contains("Right")))
                {
                    Point point = LPoint == Point.Empty ? RPoint : LPoint;

                    float ratioX = (float)point.X / box.Width;
                    float ratioY = (float)point.Y / box.Height;
                    if (ratioX >= 0 && ratioX < 1 && ratioY >= 0 && ratioY < 1)
                    {
                        int X = (int)(ratioX * box.Image.Width);
                        int Y = (int)(ratioY * box.Image.Height);
                        StringFormat format = new StringFormat();
                        if (ratioX > 0.5) format.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                        if (ratioY > 0.5) format.LineAlignment = StringAlignment.Far;
                        string text = string.Format("  X : {0}\r\n  Y : {1}\r\n", X, Y);
                        if (_Bitmaps.Count > trackBar.Value)
                        {
                            var image = _Bitmaps[trackBar.Value];
                            var mode = image.PixelFormat;
                            if (mode == PixelFormat.Format8bppIndexed)
                                text = string.Format("{0}  V : {1}\r\n", text, 
                                    (((Bitmap)box.Image).GetPixel(X, Y).R + ((Bitmap)box.Image).GetPixel(X, Y).G + ((Bitmap)box.Image).GetPixel(X, Y).B) / 3);
                            else if (mode == PixelFormat.Format24bppRgb 
                                || mode == PixelFormat.Format16bppRgb565 
                                || mode == PixelFormat.Format32bppArgb
                                || mode == PixelFormat.Format32bppRgb)
                                text = string.Format("{0}  R : {1}\r\n  G : {2}\r\n  B : {3}\r\n", text,
                                    ((Bitmap)box.Image).GetPixel(X, Y).R,
                                    ((Bitmap)box.Image).GetPixel(X, Y).G,
                                    ((Bitmap)box.Image).GetPixel(X, Y).B);
                        }                    
                        //绘制光标线
                        Pen LinePen = new Pen(param.Pic_Cursor, 2);
                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        e.Graphics.DrawLine(LinePen, new Point(0, point.Y), new Point(Width - 1, point.Y));
                        e.Graphics.DrawLine(LinePen, new Point(point.X, 0), new Point(point.X, Height - 1));

                        //绘制tooltip
                        e.Graphics.DrawString(text, new Font(FontFamily.GenericSansSerif, 15, FontStyle.Regular),
                            Brushes.BlueViolet, point, format);
                    }
                }
            }
            else
                e.Graphics.Clear(box.BackColor);
        }

        #endregion

        #endregion

        #region 右键菜单栏
        private void Menu_Copy_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
        }

        private void Menu_Paste_Click(object sender, EventArgs e)
        {
            richTextBox.Paste();
        }

        private void Menu_Clear_Click(object sender, EventArgs e)
        {
            richTextBox.Clear();
        }
        #endregion

        #region 窗体关闭

        private void FormPic_SizeChanged(object sender, EventArgs e)
        {
            TotalNumChanged();
        }





        #endregion

    }
}

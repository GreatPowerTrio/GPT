using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.IO;
using System.Drawing.Drawing2D;
using System.Net;
using System.Net.Sockets;

namespace Cute
{

    partial class FormComConfig : System.Windows.Forms.Form
    {
        /// <summary>
        /// 全局模式更改
        /// </summary>
        public Action<string> ModeChanged;

        public FormComConfig()
        {
            InitializeComponent(); 
            SetTag(this);
        }

        private void FormSerial_Load(object sender, EventArgs e)
        {
            //载入参数，随窗体大小改变而变
            comboBox_databit.Text = "8";
            comboBox_paritybit.Items.Clear();
            comboBox_stopbit.Items.Clear();
            comboBox_paritybit.Items.AddRange(System.Enum.GetNames(typeof(Parity)));
            comboBox_stopbit.Items.AddRange(System.Enum.GetNames(typeof(StopBits)));
            comboBox_paritybit.SelectedItem = "None";
            comboBox_stopbit.SelectedItem = "One";
            comboBox_encoding.SelectedItem = "UTF8";
            ///位转换
            comboBox_type.SelectedItem = "float";
            comboBox_bittype.SelectedItem = "低字节在前";
            comboBox_Mode.SelectedItem = "Serial";
            comboBox_LocalIP.SelectedItem = "Any";
            SizeChanged += FormComConfig_SizeChanged;
        }

        #region 控件大小随窗体大小等比例缩放
        private void SetTag(Control cons)
        {
            Paraments param = new Paraments();
            foreach (Control con in cons.Controls)
            {
                //颜色配置
                if (con as Button != null)
                {
                    Button button = con as Button;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 2;
                    button.FlatAppearance.BorderColor = param.Data_Border;
                    button.BackColor = param.Basic_Back;
                    button.ForeColor = param.Data_Font;
                    button.FlatAppearance.MouseOverBackColor = param.Desktop_FunctionPre;
                    button.FlatAppearance.MouseDownBackColor = param.C_Blue_2;
                }
                else if (con as ComboBox != null)
                {
                    ComboBox box = con as ComboBox;
                    box.FlatStyle = FlatStyle.Flat;
                    box.BackColor = param.Data_Back;
                    box.ForeColor = param.Data_Font;
                }

                con.Tag += con.Font.Size + ";"
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

        private void SetControls(Control cons, bool FontSize)
        {
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ';' }); 
                if (FontSize) con.Font = new Font("宋体", Convert.ToSingle(mytag[0]) * ((float)Width / srcSize.Width + (float)Height / srcSize.Height) / 2.0F);
                con.Left = Convert.ToInt32(Convert.ToSingle(mytag[1]) * con.Parent.Width);
                con.Top = Convert.ToInt32(Convert.ToSingle(mytag[2]) * con.Parent.Height);
                con.Width = Convert.ToInt32(Convert.ToSingle(mytag[3]) * con.Parent.Width);
                con.Height = Convert.ToInt32(Convert.ToSingle(mytag[4]) * con.Parent.Height); 
                if (con.Controls.Count > 0)
                {
                    SetControls(con, FontSize);
                }
            }
        }

        Size srcSize = Size.Empty;
        /// <summary>
        /// 第一次显示窗体时控件位置调整
        /// </summary>
        public void Form_Changed(Size size)
        {
            if (srcSize == Size.Empty)
            {
                Size = srcSize = size;
                SetControls(this, false);
                Show();
                Hide();
            }
            else
            {
                SetControls(this, true);
            }
        }



        private void FormComConfig_SizeChanged(object sender, EventArgs e)
        {
            if (Width == 0 || Height == 0) return;
            Form_Changed(Size);
        }

        private void FormComConfig_Shown(object sender, EventArgs e)
        {
            FormComConfig_SizeChanged(null, null);
            comboBox_LocalIP.Select(comboBox_LocalIP.Text.Length, 0); //删除该下拉列表的焦点
        }

        #endregion

        #region 全局模式调控

        private void comboBox_Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModeChanged.Invoke(comboBox_Mode.Text);
        }

        public int GetEncoding()
        {
            return comboBox_encoding.SelectedIndex != -1 ? comboBox_encoding.SelectedIndex : 1;
        }
        #endregion

        #region 串口相关
        public List<string> GetConfiguration()
        {
            return new List<string>() { 
                comboBox_paritybit.Text == "" ? "None" : comboBox_paritybit.Text, 
                comboBox_databit.Text == "" ? "8" : comboBox_databit.Text,
                comboBox_stopbit.Text == "" ?  "One" : comboBox_stopbit.Text };
        }

        #endregion

        #region LAN相关

        ///更新本地IP
        private void comboBox_LocalIP_DropDown(object sender, EventArgs e)
        {
            try
            {
                ComboBox box = sender as ComboBox;
                string text = box.Text;
                box.Items.Clear();
                box.Items.Add("Any");
                IPAddress[] IPs = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
                foreach(IPAddress ip in IPs)
                {
                    if (ip.AddressFamily.ToString().Equals("InterNetwork")) box.Items.Add(ip.ToString());
                }
                box.SelectedItem = text;
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string GetLocalIP()
        {
            return comboBox_LocalIP.Text;
        }

        #endregion

        #region 字节提取
        private void button_TypeConvert_Click(object sender, EventArgs e)
        {
            List<byte> result = new List<byte>();
            string[] data = textBox1.Text.Split(new char[] { ' ', ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                foreach (string str in data)
                {
                    List<byte> temp = new List<byte>();
                    switch (comboBox_type.Text)
                    {
                        case "Int16":
                            temp.AddRange(BitConverter.GetBytes(Int16.Parse(str)));
                            break;
                        case "UInt16":
                            temp.AddRange(BitConverter.GetBytes(UInt16.Parse(str)));
                            break;
                        case "Int32":
                            temp.AddRange(BitConverter.GetBytes(Int32.Parse(str)));
                            break;
                        case "UInt32":
                            temp.AddRange(BitConverter.GetBytes(UInt32.Parse(str)));
                            break;
                        case "float":
                            temp.AddRange(BitConverter.GetBytes(float.Parse(str)));
                            break;
                    }
                    bool Convert = true; //转换标志位
                    if ((BitConverter.IsLittleEndian && comboBox_bittype.Text == "低字节在前")
                        || (!(BitConverter.IsLittleEndian) && comboBox_bittype.Text == "高字节在前"))
                        Convert = false;
                    if (Convert) temp.Reverse();
                    result.AddRange(temp);
                }
                textBox2.Text = new Common().Data_ToHex(result.ToArray());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "字节提取");
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button_TypeConvert_Click(null, null);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region 位图转换

        private void button_PicConvert_Click(object sender, EventArgs e)
        {
            string srcpath = textBox_PicSrc.Text;
            string dstpath = textBox_PicDst.Text;
            try
            {
                if (!Directory.Exists(srcpath)) throw new Exception();
                if (!Directory.Exists(dstpath)) Directory.CreateDirectory(dstpath);
                string[] files = Directory.GetFiles(srcpath);
                Task task = Task.Run(() =>
                {
                    ///设置颜色模板
                    System.Drawing.Imaging.ColorPalette grayPal;
                    Bitmap templateMap = new Bitmap(2, 2, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                    grayPal = templateMap.Palette;
                    for (int i = 0; i <= 255; i++)
                        grayPal.Entries[i] = Color.FromArgb(i, i, i);

                    foreach (string file in files)
                    {
                        string type = Path.GetExtension(file).ToLower();
                        if (type == ".jpg" || type == ".png" || type == ".bmp")
                        {
                            Bitmap map = new Bitmap(file);
                            Bitmap res = new Bitmap(map.Width, map.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                            bool Saveflag = false;
                            System.Drawing.Imaging.BitmapData srcData =
                                map.LockBits(new Rectangle(0, 0, map.Width, map.Height),
                                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                                map.PixelFormat);
                            System.Drawing.Imaging.BitmapData dstData =
                                res.LockBits(new Rectangle(0, 0, res.Width, res.Height),
                                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                                res.PixelFormat);
                            unsafe
                            {

                                if (map.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
                                {
                                    byte* src = (byte*)srcData.Scan0;
                                    byte* dst = (byte*)dstData.Scan0;
                                    for (int i = 0; i < res.Height; i++)
                                    {
                                        for (int j = 0; j < res.Width; j++)
                                        {
                                            *dst = (byte)((*(src + 0) + *(src + 1) + *(src + 2)) / 3);
                                            dst++;
                                            src += 3;
                                        }
                                        dst += dstData.Stride - dstData.Width;
                                        src += srcData.Stride - srcData.Width * 3;
                                    }
                                    Saveflag = true;
                                }
                                else if (map.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                                {
                                    byte* src = (byte*)srcData.Scan0;
                                    byte* dst = (byte*)dstData.Scan0;
                                    for (int i = 0; i < res.Height; i++)
                                    {
                                        for (int j = 0; j < res.Width; j++)
                                        {
                                            *dst = (byte)((*(src + 1) + *(src + 2) + *(src + 3)) / 3);
                                            dst++;
                                            src += 4;
                                        }
                                        dst += dstData.Stride - dstData.Width;
                                        src += srcData.Stride - srcData.Width * 4;
                                    }
                                    Saveflag = true;
                                }
                            }
                            map.UnlockBits(srcData);
                            res.UnlockBits(dstData);
                            if (Saveflag) map = res;
                            //存储
                            if (map.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                            {
                                map.Palette = grayPal; 
                                map.Save(dstpath + @"\" + Path.GetFileNameWithoutExtension(file) + ".png", System.Drawing.Imaging.ImageFormat.Png);
                            }
                        }
                    }

                    MessageBox.Show("转换完成", "位图转换");
                });
            }
            catch (Exception ex)
            { 
                MessageBox.Show(" 路径输入错误\r\n 示例: "+ @"C:\myfolder ", "error");
            }
        }

        #endregion

        #region 重绘事件
        private void groupBox_func_Paint(object sender, PaintEventArgs e)
        {
            Paraments param = new Paraments();
            GroupBox groupBox = sender as GroupBox;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;  //抗锯齿
            e.Graphics.Clear(groupBox.BackColor);

            //重画边框
            e.Graphics.DrawLine(new Pen(param.Basic_NormalBorder, 2), new Point(0, 1), new Point(groupBox.Width, 1));
            e.Graphics.DrawLine(new Pen(param.Basic_NormalBorder, 2), new Point(0, groupBox.Height - 1), new Point(groupBox.Width, groupBox.Height - 1));

            //画标题框
            int WordLenth = (int)(groupBox.Text.Length * groupBox.Font.Size * 2.0f);
            //GraphicsPath wordGp = new GraphicsPath();
            //Rectangle wordRect1 = new Rectangle(new Point(0, (int)groupBox.Font.Size), new Size((groupBox.Width - WordLenth) / 2, (int)groupBox.Font.Size));
            //wordGp.AddRectangle(wordRect1);
            //wordGp.CloseAllFigures();
            //e.Graphics.FillPath(new HatchBrush(HatchStyle.Percent20, param.Basic_NormalTitle, groupBox.BackColor), wordGp);
            //Rectangle wordRect2 = new Rectangle(new Point((groupBox.Width - WordLenth) / 2 + WordLenth, (int)groupBox.Font.Size), new Size((groupBox.Width - WordLenth) / 2, (int)groupBox.Font.Size));
            //wordGp.AddRectangle(wordRect2);
            //wordGp.CloseAllFigures();
            //e.Graphics.FillPath(new HatchBrush(HatchStyle.Percent20, param.Basic_NormalTitle, groupBox.BackColor), wordGp);
            //写标题
            e.Graphics.DrawString(groupBox.Text, groupBox.Font, new SolidBrush(param.Basic_NormalTitle),
                new PointF((groupBox.Width - WordLenth) / 2, groupBox.Font.Size * 0.5f));
        }

        private void groupBox_Comset_Paint(object sender, PaintEventArgs e)
        {
            groupBox_func_Paint(sender, e);
        }

        #endregion

    }
}

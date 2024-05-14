using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Cute
{
    //软件的四个模块
    enum CuteMode
    {
        Basic,
        Data,
        Param,
        Pic,
        None,
    }

    class Paraments
    {        
        public Paraments()
        {
            //LoadXml();
        }

        public Paraments(bool Rand)
        {
            ///边框设置随机颜色
            Random random = new Random((int)(System.DateTime.Now.Ticks & 0xffffffffL));
            int R = random.Next(255);
            int G = random.Next(255);
            int B = random.Next(255);
            B = (R + G > 400) ? R + G - 400 : B;
            B = (B > 255) ? 255 : B;
            Desktop_Border = Color.FromArgb(R, G, B);
        }

        public Color C_Blue = Color.FromArgb(122, 195, 255);
        public Color C_Blue_2 = Color.FromArgb(62, 95, 145);
        #region Desktop
        /// <summary>
        /// 左侧选择界面
        /// </summary>
        public Color Desktop_MenuChooseBack = Color.FromArgb(30, 30, 30);
        public Color Desktop_MenuChooseWord = Color.FromArgb(122, 195, 255);
        public Color Desktop_MenuPreBack = Color.FromArgb(62, 62, 64);
        public Color Desktop_MenuBack = Color.FromArgb(45, 45, 48);
        public Color Desktop_MenuTip = Color.FromArgb(87, 116, 48);
        public Color Desktop_Font = Color.FromArgb(122, 195, 255);// Color.FromArgb(235, 235, 235);
        public Color Desktop_Word = Color.FromArgb(235, 235, 238);
        public Color Desktop_ModuleFont = Color.FromArgb(5, 5, 5);
        public Color Desktop_ModuleBack = Color.FromArgb(180, 180, 185);
        /// <summary>
        /// 右上角功能区
        /// </summary>
        public Color Desktop_FunctionBack = Color.FromArgb(45, 45, 48);
        public Color Desktop_FunctionPre = Color.FromArgb(62, 62, 64);
        /// <summary>
        /// 桌面外框
        /// </summary>
        public Color Desktop_Border = Color.FromArgb(51, 94, 168);
        #endregion

        #region Basic
        /// <summary>
        /// 普通边框颜色
        /// </summary>
        public Color Basic_NormalBorder = Color.FromArgb(90, 90, 95);
        /// <summary>
        /// 普通标题颜色
        /// </summary>
        public Color Basic_NormalTitle = Color.FromArgb(180, 180, 185);
        public Color Basic_Back = Color.FromArgb(30, 30, 30);
        public Color Basic_Font = Color.FromArgb(235, 235, 235);
        public Color Basic_ButtonBack = Color.FromArgb(180, 180, 185);
        public Color Basic_ButtonFont = Color.FromArgb(30, 30, 30);
        public Color Basic_TextBack = Color.FromArgb(30, 30, 30);
        public Color Basic_TextFore = Color.FromArgb(235, 235, 238);
        #endregion

        #region Data
        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color Data_Border = Color.FromArgb(90, 90, 95);
        /// <summary>
        /// 标题颜色
        /// </summary>
        public Color Data_Title = Color.FromArgb(180, 180, 185);
        public Color Data_Back = Color.FromArgb(30, 30, 30);
        public Color Data_Font = Color.FromArgb(235, 235, 235);
        public Color Data_ButtonBack = Color.FromArgb(180, 180, 185);
        public Color Data_ButtonFont = Color.FromArgb(30, 30, 30);
        public Color Data_TextBack = Color.FromArgb(30, 30, 30);
        public Color Data_TextFore = Color.FromArgb(235, 235, 238);
        #endregion

        #region Chart
        /// <summary>
        /// 功能区背景色
        /// </summary>
        public Color Chart_FunctionBack = Color.FromArgb(30, 30, 30);
        /// <summary>
        /// 功能区预选色
        /// </summary>
        public Color Chart_FunctionPre = Color.FromArgb(62, 62, 64);
        /// <summary>
        /// 功能区选择后颜色
        /// </summary>
        public Color Chart_FunctionChoose = Color.FromArgb(122, 195, 255);
        /// <summary>
        /// 坐标轴颜色
        /// </summary>
        public Color Chart_Axes = Color.FromArgb(150, 150, 158);
        /// <summary>
        /// 字体颜色
        /// </summary>
        public Color Chart_Word = Color.FromArgb(235, 235, 238);
        /// <summary>
        /// 光标颜色
        /// </summary>
        public Color Chart_Cursor = Color.FromArgb(235, 235, 238);
        /// <summary>
        /// 普通边框颜色
        /// </summary>
        public Color Chart_NormalBorder = Color.FromArgb(90, 90, 95);
        public Color Chart_Title = Color.FromArgb(238, 235, 235);
        public Color Chart_LineTitle = Color.FromArgb(235, 235, 238);
        public Color[] ChartLines = new Color[8] {
                Color.FromArgb(71, 147, 211),
                Color.FromArgb(227, 112, 42),
                Color.FromArgb(183, 50, 238),
                Color.FromArgb(44, 243, 77),
                Color.FromArgb(79, 18, 235),
                Color.FromArgb(210, 24, 24),
                Color.FromArgb(230, 228, 20),
                Color.FromArgb(30, 109, 150) };
        #endregion

        #region Param
        public class SParam
        {
            public Color Title = Color.FromArgb(180, 180, 185);
            public Color Back = Color.FromArgb(30, 30, 30);
            public Color Fore = Color.FromArgb(235, 235, 238);
            public Color TextFore = Color.FromArgb(235, 235, 238);
            public Color TextBack = Color.FromArgb(30, 30, 30);
            public Color ButtonFore = Color.FromArgb(5, 5, 5);
            public Color ButtonBack = Color.FromArgb(180, 180, 185);
            public Color Check = Color.FromArgb(0, 192, 0);
            public void Set(XmlDocument xml, XmlElement master)
            {
                XmlElement element1 = xml.CreateElement("标题色");
                element1.SetAttribute("RGB", "180, 180, 185");
                master.AppendChild(element1);
                XmlElement element2 = xml.CreateElement("背景色");
                element2.SetAttribute("RGB", "30,30,30");
                master.AppendChild(element2);
                XmlElement element3 = xml.CreateElement("字体色");
                element3.SetAttribute("RGB", "235, 235, 238");
                master.AppendChild(element3);
                XmlElement element4 = xml.CreateElement("文本框背景色");
                element4.SetAttribute("RGB", "30,30,30");
                master.AppendChild(element4);
                XmlElement element5 = xml.CreateElement("文本框字体色");
                element5.SetAttribute("RGB", "235, 235, 238");
                master.AppendChild(element5);
                XmlElement element6 = xml.CreateElement("按键背景色");
                element6.SetAttribute("RGB", "180, 180, 185");
                master.AppendChild(element6);
                XmlElement element7 = xml.CreateElement("按键字体色");
                element7.SetAttribute("RGB", "5, 5, 5");
                master.AppendChild(element7);
                XmlElement element8 = xml.CreateElement("OK色");
                element8.SetAttribute("RGB", "0, 192, 0");
                master.AppendChild(element8);
            }

            public void Read(XmlNode node)
            {
                ColorTrans(ref Title, node["标题色"].GetAttribute("RGB"));
                ColorTrans(ref Back, node["背景色"].GetAttribute("RGB"));
                ColorTrans(ref Fore, node["字体色"].GetAttribute("RGB"));
                ColorTrans(ref TextFore, node["文本框字体色"].GetAttribute("RGB"));
                ColorTrans(ref TextBack, node["文本框背景色"].GetAttribute("RGB"));
                ColorTrans(ref ButtonFore, node["按键字体色"].GetAttribute("RGB"));
                ColorTrans(ref ButtonBack, node["按键背景色"].GetAttribute("RGB"));
                ColorTrans(ref Check, node["OK色"].GetAttribute("RGB"));
            }
        }

        /// <summary>
        /// 普通边框
        /// </summary>
        public Color Param_NormalBorder = Color.FromArgb(90, 90, 95);
        public Color Param_TitleBack = Color.FromArgb(30, 30, 30);
        public Color Param_PageBack = Color.FromArgb(30, 30, 30);
        public SParam Param_PageBar = new SParam();
        public SParam Param_PageCascade = new SParam();
        public SParam Param_PageSinge = new SParam();
        public SParam Param_PageOne = new SParam();
        #endregion

        #region Pic
        /// <summary>
        /// 功能区背景色
        /// </summary>
        public Color Pic_FunctionBack = Color.FromArgb(30, 30, 30);
        /// <summary>
        /// 功能区预选色
        /// </summary>
        public Color Pic_FunctionPre = Color.FromArgb(62, 62, 64);
        /// <summary>
        /// 功能区选择后颜色
        /// </summary>
        public Color Pic_FunctionChoose = Color.FromArgb(122, 195, 255);
        /// <summary>
        /// 图片光标颜色
        /// </summary>
        public Color Pic_Cursor = Color.FromArgb(80, 220, 48);
        /// <summary>
        /// 图片字体颜色
        /// </summary>
        public Color Pic_Word = Color.FromArgb(235, 235, 238);
        #endregion


        #region 设置图片颜色

        /// <summary>
        /// 设置图片背景色
        /// </summary>
        /// <param name="src">图片</param>
        /// <param name="colorSrc">当前背景色</param>
        /// <param name="colorDst">更改背景色</param>
        public void BackColorChange(Bitmap src, Color colorSrc, Color colorDst)
        {
            System.Drawing.Imaging.BitmapData data = src.LockBits(new Rectangle(0, 0, src.Width, src.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* ptr = (byte*)(data.Scan0); 
                for (int i = 0; i < data.Height; i++)
                {
                    for (int j = 0; j < data.Width; j++)
                    {
                        // write the logic implementation here
                        if (colorSrc == Color.FromArgb(*(ptr + 2), *(ptr + 1), *(ptr + 0)))
                        {
                            *(ptr + 2) = colorDst.R;
                            *(ptr + 1) = colorDst.G;
                            *(ptr + 0) = colorDst.B;
                        }
                        ptr += 3;
                    }
                    ptr += data.Stride - data.Width * 3;
                }
            }
            src.UnlockBits(data);
        }

        /// <summary>
        /// 设置背景颜色 一一对应
        /// </summary>
        /// <param name="src"></param>
        /// <param name="colorSrc"></param>
        /// <param name="colorDst"></param>
        public void BackColorChange(Bitmap src, Color[] colorSrc, Color[] colorDst)
        {
            if (colorSrc.Length != colorDst.Length) return;
            System.Drawing.Imaging.BitmapData data = src.LockBits(new Rectangle(0, 0, src.Width, src.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* ptr = (byte*)(data.Scan0);
                for (int i = 0; i < data.Height; i++)
                {
                    for (int j = 0; j < data.Width; j++)
                    {
                        for(int k = 0; k < colorSrc.Length; k++)
                        {
                            // write the logic implementation here
                            if (colorSrc[k] == Color.FromArgb(*(ptr + 2), *(ptr + 1), *(ptr + 0)))
                            {
                                *(ptr + 2) = colorDst[k].R;
                                *(ptr + 1) = colorDst[k].G;
                                *(ptr + 0) = colorDst[k].B;
                            }
                        }
                        ptr += 3;
                    }
                    ptr += data.Stride - data.Width * 3;
                }
            }
            src.UnlockBits(data);
        }

        public void ThreColorChange(Bitmap src, Color Dark, Color Light)
        {
            System.Drawing.Imaging.BitmapData data = src.LockBits(new Rectangle(0, 0, src.Width, src.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* ptr = (byte*)(data.Scan0);
                for (int i = 0; i < data.Height; i++)
                {
                    for (int j = 0; j < data.Width; j++)
                    {
                        if (*(ptr + 2) > 128 && *(ptr + 1) > 128 && *(ptr + 0) > 128)
                        {
                            *(ptr + 2) = Light.R;
                            *(ptr + 1) = Light.G;
                            *(ptr + 0) = Light.B;
                        }
                        else
                        {
                            *(ptr + 2) = Dark.R;
                            *(ptr + 1) = Dark.G;
                            *(ptr + 0) = Dark.B;
                        }
                        ptr += 3;
                    }
                    ptr += data.Stride - data.Width * 3;
                }
            }
            src.UnlockBits(data);
        }
        #endregion

        #region 设置控件颜色

        public void ColorSetNormalModule(Control cons, Color color)
        {
            foreach(Control con in cons.Controls)
            {
                if(con as Label != null || con as CheckBox != null)
                {
                    con.ForeColor = color;
                }
                if(con.Controls.Count > 0)
                {
                    ColorSetNormalModule(con, color);
                }
            }
        }

        public void ColorSetButton(Control cons, Color back, Color fore)
        {
            foreach (Control con in cons.Controls)
            {
                if (con as Button != null)
                {
                    con.ForeColor = fore;
                    con.BackColor = back;
                }
                if (con.Controls.Count > 0)
                {
                    ColorSetButton(con, back, fore);
                }
            }
        }

        public void ColorSetText(Control cons, Color back, Color fore)
        {
            foreach (Control con in cons.Controls)
            {
                if (con as TextBox != null)
                {
                    con.ForeColor = fore;
                    con.BackColor = back;
                }
                if (con.Controls.Count > 0)
                {
                    ColorSetText(con, back, fore);
                }
            }
        }
        #endregion

        #region 颜色参数配置
        private XmlDocument xml = new XmlDocument();
        string path = "./Colors.xml";

        /// <summary>
        /// 新建xml文件
        /// </summary>
        public void CreateXml()
        {
            try
            {
                XmlDeclaration xmlDeclaration = xml.CreateXmlDeclaration("1.0", "utf-8", null);
                xml.InsertBefore(xmlDeclaration, xml.DocumentElement);

                XmlElement master = xml.CreateElement("颜色配置");
                xml.AppendChild(master);
                #region 桌面
                XmlElement element1 = xml.CreateElement("桌面");
                master.AppendChild(element1);
                XmlElement element13 = xml.CreateElement("背景色");
                element13.SetAttribute("RGB", "45,45,48");
                element1.AppendChild(element13);
                XmlElement element14 = xml.CreateElement("预选色");
                element14.SetAttribute("RGB", "62,62,64");
                element1.AppendChild(element14);
                XmlElement element15 = xml.CreateElement("字体色");
                element15.SetAttribute("RGB", "235,235,235");
                element1.AppendChild(element15);
                XmlElement element16 = xml.CreateElement("组件字体色");
                element16.SetAttribute("RGB", "5,5,5");
                element1.AppendChild(element16);
                XmlElement element17 = xml.CreateElement("组件背景色");
                element17.SetAttribute("RGB", "235,235,235");
                element1.AppendChild(element17);

                XmlElement element11 = xml.CreateElement("左侧菜单栏");
                element1.AppendChild(element11);
                XmlElement element111 = xml.CreateElement("背景色");
                element111.SetAttribute("RGB", "45,45,48");
                element11.AppendChild(element111);
                XmlElement element112 = xml.CreateElement("预选色");
                element112.SetAttribute("RGB", "62,62,64");
                element11.AppendChild(element112);
                XmlElement element113 = xml.CreateElement("选择色");
                element113.SetAttribute("RGB", "30,30,30");
                element11.AppendChild(element113);
                XmlElement element114 = xml.CreateElement("标记框色");
                element114.SetAttribute("RGB", "87,116,48");
                element11.AppendChild(element114);
                #endregion
                #region 基础收发
                XmlElement element2 = xml.CreateElement("基础收发");
                master.AppendChild(element2);
                XmlElement element21 = xml.CreateElement("背景色");
                element21.SetAttribute("RGB", "30,30,30");
                element2.AppendChild(element21);
                XmlElement element22 = xml.CreateElement("字体色");
                element22.SetAttribute("RGB", "235,235,235");
                element2.AppendChild(element22);
                XmlElement element23 = xml.CreateElement("按键背景色");
                element23.SetAttribute("RGB", "180,180,185");
                element2.AppendChild(element23);
                XmlElement element24 = xml.CreateElement("按键字体色");
                element24.SetAttribute("RGB", "5,5,5");
                element2.AppendChild(element24);
                XmlElement element25 = xml.CreateElement("文本框背景色");
                element25.SetAttribute("RGB", "30,30,30");
                element2.AppendChild(element25);
                XmlElement element26 = xml.CreateElement("文本框字体色");
                element26.SetAttribute("RGB", "235,235,238");
                element2.AppendChild(element26);
                XmlElement element27 = xml.CreateElement("边框色");
                element27.SetAttribute("RGB", "90,90,95");
                element2.AppendChild(element27);
                XmlElement element28 = xml.CreateElement("标题字体色");
                element28.SetAttribute("RGB", "180,180,185");
                element2.AppendChild(element28);
                #endregion
                #region 数据收发
                XmlElement element3 = xml.CreateElement("数据收发");
                master.AppendChild(element3);
                XmlElement element31 = xml.CreateElement("背景色");
                element31.SetAttribute("RGB", "30,30,30");
                element3.AppendChild(element31);
                XmlElement element32 = xml.CreateElement("字体色");
                element32.SetAttribute("RGB", "235,235,235");
                element3.AppendChild(element32);
                XmlElement element33 = xml.CreateElement("按键背景色");
                element33.SetAttribute("RGB", "180,180,185");
                element3.AppendChild(element33);
                XmlElement element34 = xml.CreateElement("按键字体色");
                element34.SetAttribute("RGB", "5,5,5");
                element3.AppendChild(element34);
                XmlElement element35 = xml.CreateElement("文本框背景色");
                element35.SetAttribute("RGB", "30,30,30");
                element3.AppendChild(element35);
                XmlElement element36 = xml.CreateElement("文本框字体色");
                element36.SetAttribute("RGB", "235,235,238");
                element3.AppendChild(element36);
                XmlElement element37 = xml.CreateElement("边框色");
                element37.SetAttribute("RGB", "90,90,95");
                element3.AppendChild(element37);
                XmlElement element38 = xml.CreateElement("标题字体色");
                element38.SetAttribute("RGB", "180,180,185");
                element3.AppendChild(element38);
                XmlElement element39 = xml.CreateElement("波形控件");
                element3.AppendChild(element39);
                XmlElement element391 = xml.CreateElement("标题字体色");
                element391.SetAttribute("RGB", "235,235,238");
                element39.AppendChild(element391);
                XmlElement element393 = xml.CreateElement("波形线标题色");
                element393.SetAttribute("RGB", "235,235,238");
                element39.AppendChild(element393);
                XmlElement element392 = xml.CreateElement("波形线颜色");
                element39.AppendChild(element392);
                XmlElement element3921 = xml.CreateElement("Line1");
                element3921.SetAttribute("RGB", "71, 147, 211");
                element392.AppendChild(element3921);
                XmlElement element3922 = xml.CreateElement("Line2");
                element3922.SetAttribute("RGB", "227, 112, 42");
                element392.AppendChild(element3922);
                XmlElement element3923 = xml.CreateElement("Line3");
                element3923.SetAttribute("RGB", "183, 50, 238");
                element392.AppendChild(element3923);
                XmlElement element3924 = xml.CreateElement("Line4");
                element3924.SetAttribute("RGB", "44, 243, 77");
                element392.AppendChild(element3924);
                XmlElement element3925 = xml.CreateElement("Line5");
                element3925.SetAttribute("RGB", "79, 18, 235");
                element392.AppendChild(element3925);
                XmlElement element3926 = xml.CreateElement("Line6");
                element3926.SetAttribute("RGB", "210, 24, 24");
                element392.AppendChild(element3926);
                XmlElement element3927 = xml.CreateElement("Line7");
                element3927.SetAttribute("RGB", "230, 228, 20");
                element392.AppendChild(element3927);
                XmlElement element3928 = xml.CreateElement("Line8");
                element3928.SetAttribute("RGB", "30, 109, 150");
                element392.AppendChild(element3928);
                #endregion
                #region 参数调节
                XmlElement element4 = xml.CreateElement("参数调节");
                master.AppendChild(element4);
                XmlElement element42 = xml.CreateElement("标题栏背景色");
                element42.SetAttribute("RGB", "30,30,30");
                element4.AppendChild(element42);
                XmlElement element43 = xml.CreateElement("参数页栏背景色");
                element43.SetAttribute("RGB", "30,30,30");
                element4.AppendChild(element43);
                XmlElement element48 = xml.CreateElement("边框色");
                element48.SetAttribute("RGB", "90, 90, 95");
                element4.AppendChild(element48);
                XmlElement element44 = xml.CreateElement("滑动调参");
                element4.AppendChild(element44);
                Param_PageBar.Set(xml, element44);
                XmlElement element45 = xml.CreateElement("单参数");
                element4.AppendChild(element45);
                Param_PageOne.Set(xml, element45);
                XmlElement element46 = xml.CreateElement("单级PID");
                element4.AppendChild(element46);
                Param_PageSinge.Set(xml, element46);
                XmlElement element47 = xml.CreateElement("串级PID");
                element4.AppendChild(element47);
                Param_PageCascade.Set(xml, element47);
                #endregion
                #region 图片接收
                XmlElement element5 = xml.CreateElement("图片接收");
                master.AppendChild(element5);
                #endregion

                xml.Save(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Xml创建失败");
            }
        }

        public void LoadXml()
        {
            try
            {
                if (!File.Exists(path)) CreateXml();
                xml.Load(path);
                XmlNode node = xml.SelectSingleNode("/颜色配置");
                XmlNode DesktopNode = node["桌面"]; 
                ColorTrans(ref Desktop_FunctionBack, DesktopNode["背景色"].GetAttribute("RGB"));
                ColorTrans(ref Desktop_FunctionPre, DesktopNode["预选色"].GetAttribute("RGB"));
                ColorTrans(ref Desktop_Font, DesktopNode["字体色"].GetAttribute("RGB"));
                ColorTrans(ref Desktop_ModuleFont, DesktopNode["组件字体色"].GetAttribute("RGB"));
                ColorTrans(ref Desktop_ModuleBack, DesktopNode["组件背景色"].GetAttribute("RGB"));
                ColorTrans(ref Desktop_MenuTip, DesktopNode["左侧菜单栏"]["标记框色"].GetAttribute("RGB"));
                XmlNode BasicNode = node["基础收发"];
                ColorTrans(ref Basic_Back, BasicNode["背景色"].GetAttribute("RGB"));
                ColorTrans(ref Basic_Font, BasicNode["字体色"].GetAttribute("RGB"));
                ColorTrans(ref Basic_ButtonBack, BasicNode["按键背景色"].GetAttribute("RGB"));
                ColorTrans(ref Basic_ButtonFont, BasicNode["按键字体色"].GetAttribute("RGB"));
                ColorTrans(ref Basic_TextBack, BasicNode["文本框背景色"].GetAttribute("RGB"));
                ColorTrans(ref Basic_TextFore, BasicNode["文本框字体色"].GetAttribute("RGB"));
                ColorTrans(ref Basic_NormalBorder, BasicNode["边框色"].GetAttribute("RGB"));
                ColorTrans(ref Basic_NormalTitle, BasicNode["标题字体色"].GetAttribute("RGB"));
                XmlNode DataNode = node["数据收发"];
                ColorTrans(ref Data_Back, DataNode["背景色"].GetAttribute("RGB"));
                ColorTrans(ref Data_Font, DataNode["字体色"].GetAttribute("RGB"));
                ColorTrans(ref Data_ButtonBack, DataNode["按键背景色"].GetAttribute("RGB"));
                ColorTrans(ref Data_ButtonFont, DataNode["按键字体色"].GetAttribute("RGB"));
                ColorTrans(ref Data_TextBack, DataNode["文本框背景色"].GetAttribute("RGB"));
                ColorTrans(ref Data_TextFore, DataNode["文本框字体色"].GetAttribute("RGB"));
                ColorTrans(ref Data_Border, DataNode["边框色"].GetAttribute("RGB"));
                ColorTrans(ref Data_Title, DataNode["标题字体色"].GetAttribute("RGB"));
                ColorTrans(ref Chart_LineTitle, DataNode["波形控件"]["波形线标题色"].GetAttribute("RGB"));
                ColorTrans(ref Chart_Title, DataNode["波形控件"]["标题字体色"].GetAttribute("RGB"));
                for (int i = 0; i < 8; i++)
                    ColorTrans(ref ChartLines[i], DataNode["波形控件"]["波形线颜色"]["Line"+(i+1).ToString()].GetAttribute("RGB"));
                XmlNode ParamNode = node["参数调节"];
                ColorTrans(ref Param_TitleBack, ParamNode["标题栏背景色"].GetAttribute("RGB"));
                ColorTrans(ref Param_PageBack, ParamNode["参数页栏背景色"].GetAttribute("RGB"));
                ColorTrans(ref Param_NormalBorder, ParamNode["边框色"].GetAttribute("RGB"));
                Param_PageBar.Read(ParamNode["滑动调参"]);
                Param_PageOne.Read(ParamNode["单参数"]);
                Param_PageSinge.Read(ParamNode["单级PID"]);
                Param_PageCascade.Read(ParamNode["串级PID"]);
            }
            catch(NullReferenceException ex)
            {
                MessageBox.Show(ex.Message, "Xml格式错误，请重新配置或删除该xml文件");
            }
        }

        static void ColorTrans(ref Color color, string txt)
        {
            try
            {
                string[] str = txt.Split(new char[] { ' ', ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
                if (str.Length == 3)
                {
                    Color res = Color.FromArgb(int.Parse(str[0]), int.Parse(str[1]), int.Parse(str[2]));
                    color = res;
                }
            }
            catch { }
        }
        #endregion
    }

    #region 历史数据保存

    /// <summary>
    /// 发送历史文本保存
    /// </summary>
    class SendTextHistory
    {
        private XmlDocument xml = new XmlDocument();
        string path = "./SendHistory.xml";

        /// <summary>
        /// 新建xml文件
        /// </summary>
        public void CreateXml()
        {
            try
            {
                XmlDeclaration xmlDeclaration = xml.CreateXmlDeclaration("1.0", "utf-8", null);
                xml.InsertBefore(xmlDeclaration, xml.DocumentElement);

                XmlElement master = xml.CreateElement("历史记录");
                xml.AppendChild(master);

                XmlElement element1 = xml.CreateElement("保存帧上限");
                element1.InnerText = "50";
                master.AppendChild(element1);

                XmlElement element2 = xml.CreateElement("基础收发历史数据帧");
                element2.SetAttribute("已保存帧数", "0");
                master.AppendChild(element2);

                XmlElement element3 = xml.CreateElement("数据收发历史数据帧");
                element3.SetAttribute("已保存帧数", "0");
                master.AppendChild(element3);

                xml.Save(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Xml创建失败");
            }
        }

        /// <summary>
        /// xml文件末添加数据
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="text"></param>
        public void AppEndText(CuteMode mode, string text)
        {               
            try
            {
                //文件不存在则创建xml文件
                if (!File.Exists(path)) CreateXml();
                xml.Load(path);
                XmlNode node = xml.SelectSingleNode("/历史记录");
                int HistoryNumberMax = int.Parse(((XmlElement)node["保存帧上限"]).InnerText);
                XmlNode frames = null;
                if (mode == CuteMode.Basic)
                    frames = node["基础收发历史数据帧"];
                else if (mode == CuteMode.Data)
                    frames = node["数据收发历史数据帧"];
                //新建节点
                XmlElement element = xml.CreateElement("数据帧");
                string time = System.DateTime.Now.ToString() + "." + System.DateTime.Now.Millisecond;
                element.SetAttribute("创建时间", time);
                element.SetAttribute("文本长度", text.Length.ToString());
                element.InnerText = text;
                frames.AppendChild(element);
                //超出上限则删除首个子节点
                while (frames.ChildNodes.Count > HistoryNumberMax)
                {
                    frames.RemoveChild(frames.FirstChild);
                }
                ((XmlElement)frames).SetAttribute("已保存帧数", frames.ChildNodes.Count.ToString());
                xml.Save(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Xml存储");
            }
        }

        /// <summary>
        /// 寻找指定记录
        /// </summary>
        /// <param name="mode">页面</param>
        /// <param name="time">发送内容时间</param>
        /// <param name="key">按下按键</param>
        /// <returns></returns>
        public string[] FindText(CuteMode mode, string time, Keys key)
        {
            if (!File.Exists(path)) CreateXml();
            xml.Load(path);
            XmlNode node = xml.SelectSingleNode("/历史记录");
            XmlNode frames = null;
            if (mode == CuteMode.Basic)
                frames = node["基础收发历史数据帧"];
            else if (mode == CuteMode.Data)
                frames = node["数据收发历史数据帧"];

            if(frames.HasChildNodes)
            {
                int Count = frames.ChildNodes.Count;
                int index = 0;
                for (; index < Count; index++)
                {
                    if (((XmlElement)(frames.ChildNodes[index])).GetAttribute("创建时间") == time)
                        break;
                }
                if (key == Keys.Up && index > 0) index -= 1;
                else if (key == Keys.Down) index += 1;
                string Time = string.Empty; //对应时间
                string Text = string.Empty; //对应文本
                if (index < Count)
                {
                    Time = ((XmlElement)frames.ChildNodes[index]).GetAttribute("创建时间");
                    Text = frames.ChildNodes[index].InnerText;
                }
                return new string[] { Time, Text };
            }
            return null;
        }
    }
    #endregion
}

using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace Cute.pic
{
    class Algorithm
    {
        public List<Func<Bitmap, Bitmap>> ImagePreMethod = new List<Func<Bitmap, Bitmap>>();
        public List<Func<Bitmap, Bitmap>> ImageDetector = new List<Func<Bitmap, Bitmap>>();
        private ColorPalette colorPalette; //颜色映射表
        public Action<string> ShowTextEnd;

        public Algorithm()
        {
            ///初始化
            ImagePreMethod.Add(new Func<Bitmap, Bitmap>(AVE_Method));
            ImagePreMethod.Add(new Func<Bitmap, Bitmap>(OTSU_Method));
            ImagePreMethod.Add(new Func<Bitmap, Bitmap>(Wallner_Method));
            ImagePreMethod.Add(new Func<Bitmap, Bitmap>(Self_Method));
            ImageDetector.Add(new Func<Bitmap, Bitmap>(Detecting_Method1));
            ImageDetector.Add(new Func<Bitmap, Bitmap>(Detecting_Method2));

            #region 设置灰度值映射表
            Bitmap templateMap = new Bitmap(1, 1, PixelFormat.Format8bppIndexed);
            colorPalette = templateMap.Palette;
            for (int i = 0; i < 256; i++)
                colorPalette.Entries[i] = Color.FromArgb(i, i, i);
            colorPalette.Entries[1] = Color.Red;
            colorPalette.Entries[2] = Color.Green;
            colorPalette.Entries[3] = Color.Blue;
            colorPalette.Entries[4] = Color.Orange;
            colorPalette.Entries[5] = Color.Gold;
            colorPalette.Entries[6] = Color.BlueViolet;
            colorPalette.Entries[7] = Color.GreenYellow;
            colorPalette.Entries[8] = Color.Beige;
            colorPalette.Entries[9] = Color.CadetBlue;
            colorPalette.Entries[10] = Color.DeepPink;
            #endregion
            //预编译
            Build();
         }

        #region 图片预处理

        #region 均值法二值化

        Bitmap AVE_Method(Bitmap image)
        {
            ///复制图片
            Bitmap usemap = (Bitmap)image.Clone();
            System.Drawing.Imaging.BitmapData dstData =
                usemap.LockBits(new Rectangle(0, 0, usemap.Width, usemap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                usemap.PixelFormat);

            unsafe
            {
                if (usemap.PixelFormat == PixelFormat.Format8bppIndexed) //灰度图片
                {
                    ///用户图像处理
                    int lenth = usemap.Width * usemap.Height;
                    byte* value = (byte*)dstData.Scan0; //获取图像首地址
                    ///均值法求阈值
                    Int64 sum = 0;
                    for (int i = 0; i < lenth; i++)
                    {
                        sum += value[i];
                    }
                    byte ave = (byte)(sum / lenth);
                    ///二值化
                    for (int i = 0; i < lenth; i++)
                    {
                        if (value[i] >= ave) value[i] = 255;
                        else value[i] = 0;
                    }
                }
            }

            usemap.UnlockBits(dstData);
            return usemap;
        }

        #endregion

        #region 大津法二值化

        Bitmap OTSU_Method(Bitmap image)
        {
            ///复制图片
            Bitmap usemap = (Bitmap)image.Clone();
            System.Drawing.Imaging.BitmapData dstData =
                usemap.LockBits(new Rectangle(0, 0, usemap.Width, usemap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                usemap.PixelFormat);

            unsafe
            {
                if (usemap.PixelFormat == PixelFormat.Format8bppIndexed) //灰度图片
                {
                    ///用户图像处理
                    int lenth = usemap.Width * usemap.Height;
                    byte* value = (byte*)dstData.Scan0;  //获取图像首地址
                    ///大津法求阈值
                    byte threshold = get_ostu_value((IntPtr)value, usemap.Width, usemap.Height);
                    ///二值化
                    for (int i = 0; i < lenth; i++)
                    {
                        if (value[i] >= threshold) value[i] = 255;
                        else value[i] = 0;
                    }
                }
            }

            usemap.UnlockBits(dstData);
            return usemap;
        }

        byte get_ostu_value(IntPtr image, int W, int H)
        {
            unsafe
            {
                byte* grayImage = (byte*)image;
                UInt32 Amount = 0;
                UInt32 PixelBack = 0;
                UInt32 PixelIntegralBack = 0;
                UInt32 PixelIntegral = 0;
                UInt32 PixelIntegralFore = 0;
                UInt32 PixelFore = 0;
                float OmegaBack, OmegaFore, MicroBack, MicroFore, SigmaB, Sigma; // 类间方差;
                Int16 MinValue, MaxValue;
                byte Threshold = 0;
                UInt16[] HistoGram = new UInt16[256];
                for (int i = 0; i < W * H; ++i) HistoGram[*(grayImage + i)]++; //统计灰度级中每个像素在整幅图像中的个数
                for (MinValue = 0; MinValue < 256 && HistoGram[MinValue] == 0; MinValue++) ;         //获取最小灰度的值
                for (MaxValue = 255; MaxValue > MinValue && HistoGram[MinValue] == 0; MaxValue--) ; //获取最大灰度的值
                if (MaxValue == MinValue) return (byte)MaxValue;         //图像中只有一个颜色
                if (MinValue + 1 == MaxValue) return (byte)MinValue;        //图像中只有二个颜色
                for (Int16 j = MinValue; j <= MaxValue; j++) Amount += HistoGram[j];        //像素总数
                PixelIntegral = 0;
                for (Int16 j = MinValue; j <= MaxValue; j++) PixelIntegral += (UInt32)(HistoGram[j] * j); //灰度值总数
                SigmaB = -1;
                for (Int16 j = MinValue; j < MaxValue; j++)
                {
                    PixelBack = PixelBack + HistoGram[j];    //前景像素点数
                    PixelFore = Amount - PixelBack;         //背景像素点数
                    OmegaBack = (float)PixelBack / Amount; //前景像素百分比
                    OmegaFore = (float)PixelFore / Amount; //背景像素百分比
                    PixelIntegralBack += (UInt32)(HistoGram[j] * j);  //前景灰度值
                    PixelIntegralFore = PixelIntegral - PixelIntegralBack; //背景灰度值
                    MicroBack = (float)PixelIntegralBack / PixelBack;   //前景灰度百分比
                    MicroFore = (float)PixelIntegralFore / PixelFore;   //背景灰度百分比
                    Sigma = OmegaBack * OmegaFore * (MicroBack - MicroFore) * (MicroBack - MicroFore); //计算类间方差
                    if (Sigma > SigmaB)                    //遍历最大的类间方差g //找出最大类间方差以及对应的阈值
                    {
                        SigmaB = Sigma;
                        Threshold = (byte)j;
                    }
                }
                return Threshold;                        //返回最佳阈值;
            }
        }
        #endregion

        #region Wallner自适应法二值化

        Bitmap Wallner_Method(Bitmap image)
        {
            ///复制图片
            Bitmap usemap = (Bitmap)image.Clone();
            System.Drawing.Imaging.BitmapData srcData =
                image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                image.PixelFormat);
            System.Drawing.Imaging.BitmapData dstData =
                usemap.LockBits(new Rectangle(0, 0, usemap.Width, usemap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                usemap.PixelFormat);
            unsafe
            {
                if (usemap.PixelFormat == PixelFormat.Format8bppIndexed) //灰度图片
                {
                    ///用户图像处理
                    int lenth = usemap.Width * usemap.Height;
                    byte* src = (byte*)srcData.Scan0;
                    byte* dst = (byte*)dstData.Scan0;
                    /*
                    * pn = 当前点的灰度值
                    * s = 选取周围像素点的个数 : 图片宽度/n （n = 8时效果最好）
                    * threshold = (fs(n) / s) * (1 - t /100) 每点自适应的阈值
                    * t = 比例阈值
                    * 公式：g(n) = g(n-1) * (1-1/s) + p(n) 改进后的像素点值 代替fs(n)
                    * */
                    int wallner_width = usemap.Width;
                    int wallner_height = usemap.Height;
                    int t = 15;
                    int s = wallner_width / 8;
                    int S = 9;
                    int power2S = 1 << S;//乘性因子，防止浮点型计算
                    int factor = power2S * (100 - t) / (100 * s);
                    int gn = 127 * s;
                    int q = power2S - power2S / s;
                    int pn, hn;
                    int[] lastrow_gn = new int[wallner_width];
                    for (int i = 0; i < wallner_width; i++) lastrow_gn[i] = gn;
                    for (int i = 0; i < wallner_height; i++)
                    {
                        for (int j = 0; j < wallner_width; j++)//从左向右遍历
                        {
                            pn = *(src + i * wallner_width + j);
                            gn = ((gn * q) >> S) + pn;
                            hn = (gn + lastrow_gn[j]) >> 1;
                            lastrow_gn[j] = gn;
                            *(dst + i * wallner_width + j) = (byte)(pn < ((hn * factor) >> S) ? 0 : 255);
                        }
                        i++;
                        if (i == wallner_height) break;

                        for (int j = wallner_width - 1; j >= 0; j--)//从右向左遍历
                        {
                            pn = *(src + i * wallner_width + j);
                            gn = ((gn * q) >> S) + pn;
                            hn = (gn + lastrow_gn[j]) >> 1;
                            lastrow_gn[j] = gn;
                            *(dst + i * wallner_width + j) = (byte)(pn < (hn * factor) >> S ? 0 : 255);
                        }
                    }
                }
            }
            image.UnlockBits(srcData);
            usemap.UnlockBits(dstData);
            return usemap;
        }

        #endregion

        #region 自定义方法

        Bitmap Self_Method(Bitmap image)
        {
            ///复制图片
            Bitmap usemap = (Bitmap)image.Clone();
            System.Drawing.Imaging.BitmapData dstData =
                usemap.LockBits(new Rectangle(0, 0, usemap.Width, usemap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                usemap.PixelFormat);

            if (ClassDetector != null)
            {
                MethodInfo method = ClassDetector.GetType().GetMethod("PreProcessing");
                if (method != null)
                {
                    ShowTextEnd?.BeginInvoke((string)method.Invoke(ClassDetector, new object[] { dstData.Scan0, usemap.Width, usemap.Height }), null, null);
                }
            }

            usemap.UnlockBits(dstData);
            return usemap;
        }

        #endregion

        #endregion

        #region 逻辑处理

        #region Method1

        Bitmap Detecting_Method1(Bitmap image)
        {
            ///复制图片
            Bitmap usemap = (Bitmap)image.Clone();
            System.Drawing.Imaging.BitmapData dstData =
                usemap.LockBits(new Rectangle(0, 0, usemap.Width, usemap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                usemap.PixelFormat);

            unsafe
            {
                if (usemap.PixelFormat == PixelFormat.Format8bppIndexed) //灰度图片
                {
                    ///用户图像处理
                    byte* value = (byte*)dstData.Scan0; //获取图像首地址

                    int[] Left_Line = new int[usemap.Height]; //左边界
                    int[] Right_Line = new int[usemap.Height]; //右边界
                    int[] Mid_Line = new int[usemap.Height + 1]; //中心线

                    Mid_Line[usemap.Height] = usemap.Width / 2; //初始化第一个中点

                    ///寻边线，中线
                    for(int i = usemap.Height - 1; i >= 0; i--)
                    {
                        ///寻左右边界点，每次都以上一行中点为起始点
                        int[] border = Search_Border((IntPtr)(value + i * usemap.Width), Mid_Line[i + 1], Mid_Line[i + 1], usemap.Width);
                        Left_Line[i] = border[0];
                        Right_Line[i] = border[1];
                        Mid_Line[i] = (Left_Line[i] + Right_Line[i]) / 2;
                    }

                    ///标识边线，中线
                    for (int i = usemap.Height - 1; i >= 0; i--)
                    {
                        if ((Math.Abs(Mid_Line[i] - usemap.Width / 2) > (usemap.Width / 2 - usemap.Width / 10))
                            || (i < usemap.Height - 1 && Math.Abs(Mid_Line[i] - Mid_Line[i + 1]) > usemap.Width / 3)) break;
                        *(value + i * usemap.Width + Left_Line[i]) = 1;
                        *(value + i * usemap.Width + Right_Line[i]) = 2;
                        *(value + i * usemap.Width + Mid_Line[i]) = 3;
                    }
                }
            }

            usemap.UnlockBits(dstData);
            usemap.Palette = colorPalette;
            return usemap;
        }


        /// <summary>
        /// 寻边界点
        /// </summary>
        /// <param name="ImageRow">图像行首地址</param>
        /// <param name="LPoint">寻左边界起始点</param>
        /// <param name="RPoint">寻右边界起始点</param>
        /// <param name="Width">图像宽度</param>
        /// <returns>左右边界点</returns>
        int[] Search_Border(IntPtr ImageRow, int LPoint, int RPoint, int Width)
        {
            unsafe
            {
                int[] result = new int[2]; //边界点数组
                byte* value = (byte*)ImageRow; //获取图像行首地址
                int j;

                ///左边界
                for (j = LPoint; j > 0; j--)//从LPoint处向左找左边界
                {
                    if(*(value + j + 1) != 0 && *(value + j) != 0 && *(value + j - 1) == 0)//两白一黑检测左边界
                    {
                        result[0] = j;
                        break;
                    }
                }
                if (j == 0) //未找寻到边界点
                {
                    result[0] = 0;
                }

                ///右边界
                for (j = RPoint; j < Width - 1; j++)//从RPoint处向右找右边界
                {
                    if(*(value + j - 1) != 0 && *(value + j) != 0 && *(value + j + 1) == 0) //两白一黑检测右边界
                    {
                        result[1] = j;
                        break;
                    }
                }
                if (j == Width - 1) //未找寻到边界点
                {
                    result[1] = Width - 1;
                }
                return result;
            }
        }
        #endregion

        #region Method2

        object ClassDetector;
        Bitmap Detecting_Method2(Bitmap image)
        {
            ///复制图片
            Bitmap usemap = (Bitmap)image.Clone();
            System.Drawing.Imaging.BitmapData dstData =
                usemap.LockBits(new Rectangle(0, 0, usemap.Width, usemap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                usemap.PixelFormat);

            if (ClassDetector != null)
            {
                MethodInfo method = ClassDetector.GetType().GetMethod("Detecting");
                if (method != null)
                {
                    ShowTextEnd?.BeginInvoke((string)method.Invoke(ClassDetector, new object[] { dstData.Scan0, usemap.Width, usemap.Height }), null, null);
                }
            }
            usemap.UnlockBits(dstData);
            usemap.Palette = colorPalette;
            return usemap;
        }

        #endregion

        #endregion

        #region 代码编译

        /// <summary>
        /// 编译路径
        /// </summary>

        /// <summary>
        /// 编译
        /// </summary>
        public void Build()
        {
            if (!File.Exists("ImageDetecting.cs")) return;
            CSharpCodeProvider complier = new CSharpCodeProvider();
            CompilerParameters paras = new CompilerParameters();
            paras.ReferencedAssemblies.Add("System.dll");
            paras.ReferencedAssemblies.Add("System.IO.dll");
            paras.GenerateExecutable = false;
            paras.GenerateInMemory = true;
            paras.CompilerOptions = "/unsafe";
            paras.OutputAssembly = "ImageDetecting.dll";

            var result = complier.CompileAssemblyFromFile(paras, "ImageDetecting.cs");
            if (result.Errors.HasErrors)
            {
                var strs = new StringBuilder();
                foreach (CompilerError error in result.Errors)
                {
                    if (error.IsWarning) strs.Append(" warning ");
                    else strs.Append(" error ");
                    if (error.Line != 0) strs.Append("行" + error.Line);
                    strs.Append(" ：").Append(error.ErrorText).Append("\r\n");
                }
                ShowTextEnd?.Invoke(strs.Append("\r\n---编译失败---\r\n").ToString());
            }
            else
            {
                //类实例化
                ClassDetector = result.CompiledAssembly.CreateInstance("Cute.Pic.ImageDetector");
                ShowTextEnd?.Invoke("\r\n---编译成功---\r\n");
            }
        }
        #endregion
    }
}

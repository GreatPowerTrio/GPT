using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;

namespace Cute.pic
{
    class ImageUnpack
    {
        //帧头  帧尾
        private byte sof = 0x28;
        private byte headercheck = 0x2D;
        private byte tail = 0x29;
        private List<byte> databuffer = new List<byte>();
        private byte check_lenth = 0;
        private PixelFormat mode = PixelFormat.Undefined;
        private int width = 0;
        private int height = 0;
        public int Number = 0;
        bool Remind = true;
        ColorPalette grayPal; //灰度调色板
        public ImageUnpack()
        {
            Bitmap gray = new Bitmap(2, 2, PixelFormat.Format8bppIndexed);
            grayPal = gray.Palette;
            for (Int32 i = 0; i < 256; i++)
                grayPal.Entries[i] = Color.FromArgb(i, i, i);
            gray.Dispose();
        }

        public List<Bitmap> Unpack(byte[] newdata)
        {
            List<Bitmap> result = new List<Bitmap>();
            databuffer.AddRange(newdata);
            int errorflag = 0; //错误标记位
        start:
            ///SOF
            if (check_lenth == 0)
            {
                int sofindex = databuffer.IndexOf(sof);
                if (sofindex != -1)
                {
                    databuffer.RemoveRange(0, sofindex);
                    check_lenth++;
                }
                else
                {
                    errorflag = 1;
                    goto error;
                }
            }
            ///mode
            if (check_lenth == 1 && databuffer.Count > 1)
            {
                byte temp = databuffer[check_lenth];
                if (temp >= 0x21 && temp <= 0x24)
                {
                    if (temp == 0x21) mode = PixelFormat.Format1bppIndexed;
                    else if (temp == 0x22) mode = PixelFormat.Format8bppIndexed;
                    else if (temp == 0x23) mode = PixelFormat.Format16bppRgb565;
                    else if (temp == 0x24) mode = PixelFormat.Format24bppRgb;
                    check_lenth++;
                }
                else
                {
                    errorflag = 2;
                    goto error;
                }
            }
            ///Width
            if (check_lenth == 2 && databuffer.Count > 3)
            {
                byte[] W = databuffer.GetRange(check_lenth, 2).ToArray();
                if (!BitConverter.IsLittleEndian) W.Reverse();
                width = BitConverter.ToUInt16(W, 0);
                check_lenth += 2;
            }
            ///Height
            if (check_lenth == 4 && databuffer.Count > 5)
            {
                byte[] H = databuffer.GetRange(check_lenth, 2).ToArray();
                if (!BitConverter.IsLittleEndian) H.Reverse();
                height = BitConverter.ToUInt16(H, 0);
                check_lenth += 2;
            }
            if(check_lenth == 6 && databuffer.Count > 6)
            {
                byte check = databuffer[check_lenth];
                if(check == 0x2D)
                {
                    //善意的提醒
                    if (Remind && width > 1000 && height > 1000)
                    {
                        var res = MessageBox.Show(string.Format("检测到帧头属性异常。width:{0} height:{1} \r\n是否忽略该类提醒", width, height), "异常", MessageBoxButtons.YesNo);
                        if (res == DialogResult.Yes)
                        {
                            Remind = false;
                        }
                    }
                    check_lenth++;
                }
                else
                {
                    errorflag = 3;
                    goto error;
                }
            }
            ///Tail
            if (check_lenth >= 7)
            {
                if(mode == PixelFormat.Format1bppIndexed)
                {
                    int Lenth = width * height / 8;
                    int remainder = width * height % 8;
                    if (remainder != 0) Lenth += 1;
                    if (databuffer.Count > 7 + Lenth)
                    {
                        byte temp = databuffer[7 + Lenth];
                        //校验正确
                        if (temp == tail)
                        {
                            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
                            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                ImageLockMode.WriteOnly,
                                bitmap.PixelFormat);
                            unsafe
                            {
                                byte* ptr = (byte*)bmpData.Scan0;
                                for (int i = 0; i < height; i++)
                                {
                                    for (int j = 0; j < width; j++)
                                    {
                                        int value = databuffer[7 + (i * width + j) / 8] & (1 << (i * width + j) % 8);
                                        *ptr++ = (byte)(value > 0 ? 255 : 0);
                                    }
                                    ptr += bmpData.Stride - bmpData.Width;
                                }
                            }
                            bitmap.UnlockBits(bmpData);
                            bitmap.Palette = grayPal;
                            bitmap.Tag = (Number++).ToString();
                            result.Add(bitmap);
                            ///清除该帧数据
                            databuffer.RemoveRange(0, 7 + Lenth + 1);
                            goto clear;
                        }
                        else
                        {
                            errorflag = 41;
                            goto error;
                        }
                    }
                }
                else if(mode == PixelFormat.Format8bppIndexed)
                {
                    if(databuffer.Count > 7 + width * height)
                    {
                        byte temp = databuffer[7 + width * height];
                        //校验正确
                        if (temp == tail)
                        {
                            Bitmap bitmap = new Bitmap(width, height, mode);
                            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), 
                                ImageLockMode.WriteOnly, 
                                bitmap.PixelFormat);
                            unsafe
                            {
                                byte* ptr = (byte*)bmpData.Scan0;
                                for (int i = 0; i < height; i++)
                                {
                                    for (int j = 0; j < width; j++)
                                    {
                                        *ptr++ = databuffer[7 + i * width + j];
                                    }
                                    ptr += bmpData.Stride - bmpData.Width;
                                }
                            }
                            bitmap.UnlockBits(bmpData);
                            bitmap.Palette = grayPal;
                            bitmap.Tag = (Number++).ToString();
                            result.Add(bitmap);
                            ///清除该帧数据
                            databuffer.RemoveRange(0, 7 + width * height + 1);
                            goto clear;
                        }
                        else
                        {
                            errorflag = 42;
                            goto error;
                        }
                    }
                }
                else if(mode == PixelFormat.Format16bppRgb565)
                {
                    int Lenth = width * height * 2;
                    if (databuffer.Count > 7 + Lenth)
                    {
                        byte temp = databuffer[7 + Lenth];
                        //校验正确
                        if (temp == tail)
                        {
                            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format16bppRgb565);
                            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                ImageLockMode.WriteOnly,
                                bitmap.PixelFormat);
                            unsafe
                            {
                                byte* ptr = (byte*)bmpData.Scan0;
                                for (int i = 0; i < bitmap.Height; i++)
                                {
                                    for (int j = 0; j < bitmap.Width; j++)
                                    {
                                        int index = 7 + (i * bitmap.Width + j) * 2;
                                        * ptr++ = databuffer[index + 1];
                                        *ptr++ = databuffer[index];
                                    }
                                    ptr += bmpData.Stride - bitmap.Width * 2;
                                }
                            }
                            bitmap.UnlockBits(bmpData);
                            bitmap.Tag = (Number++).ToString();
                            result.Add(bitmap);
                            ///清除该帧数据
                            databuffer.RemoveRange(0, 7 + Lenth + 1);
                            goto clear;
                        }
                        else
                        {
                            errorflag = 43;
                            goto error;
                        }
                    }
                }
                else if(mode == PixelFormat.Format24bppRgb)
                {
                    int Lenth = width * height * 3;
                    if (databuffer.Count > 7 + Lenth)
                    {
                        byte temp = databuffer[7 + Lenth];
                        //校验正确
                        if (temp == tail)
                        {
                            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                ImageLockMode.WriteOnly,
                                bitmap.PixelFormat);
                            unsafe
                            {
                                byte* ptr = (byte*)bmpData.Scan0;
                                for (int i = 0; i < bitmap.Height; i++)
                                {
                                    for (int j = 0; j < bitmap.Width; j++)
                                    {
                                        int index = 7 + (i * bitmap.Width + j) * 3;
                                        *ptr++ = databuffer[index + 2];
                                        *ptr++ = databuffer[index + 1];
                                        *ptr++ = databuffer[index];
                                    }
                                    ptr += bmpData.Stride - bitmap.Width * 3;
                                }
                            }
                            bitmap.UnlockBits(bmpData);
                            bitmap.Tag = (Number++).ToString();
                            result.Add(bitmap);
                            ///清除该帧数据
                            databuffer.RemoveRange(0, 7 + Lenth + 1);
                            goto clear;
                        }
                        else
                        {
                            errorflag = 43;
                            goto error;
                        }
                    }
                }
            }
            return result;

        ///数据传输错误
        error:
            //Console.WriteLine(string.Format("errflag: {0}", errorflag));
            int sof_index = databuffer.IndexOf(sof);
            if (sof_index != -1) databuffer.RemoveRange(0, sof_index + 1);
            else databuffer.Clear();
            ///清除标志位 继续解包
            clear:
            check_lenth = 0;
            mode = PixelFormat.Undefined;
            width = height = 0;
            if (databuffer.IndexOf(sof) != -1) goto start;
            return result;
        }
    }
}

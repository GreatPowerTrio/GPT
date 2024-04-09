using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cute.param
{
    class FrameMsg
    {
        public string Name = string.Empty;
        public List<float> data = new List<float>();
    }

    class FrameUnPack
    {
        //帧头  帧尾
        private byte sof = 0x28;
        private byte tail = 0x29;
        private List<byte> databuffer = new List<byte>();

        public CuteMode Mode = CuteMode.None;
        private byte check_lenth = 0;
        private byte Id = 0; //标识符
        private int data_size = 0; //数据大小
        private int data_count = 0; //数据个数
        private int name_lenth = 0; //名字长度

        public List<FrameMsg> Unpack(byte[] newdata)
        {
            List<FrameMsg> result = new List<FrameMsg>();
            databuffer.AddRange(newdata);
            int errorflag = 0; //错误标记位

        CheckHeader:
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
            ///Type
            if (check_lenth == 1 && databuffer.Count > 1)
            {
                Id = databuffer[check_lenth];

                if (Id == 0x10)
                {
                    Mode = CuteMode.Param;
                    data_size = 4;
                }
                else if (Id == 0x0f)
                {
                    Mode = CuteMode.Data;
                    data_size = 4;
                }
                else if ((Id & 0xf0) == 0)
                {
                    data_size = Id & 0x07;
                    if (data_size == 1 || data_size == 2 || data_size == 4)
                    {
                        Mode = CuteMode.Data;
                    }
                }

                if (Mode != CuteMode.None)
                {
                    check_lenth++;
                }
                else
                {
                    errorflag = 2;
                    goto error;
                }
            }
            ///Lenth
            if (check_lenth == 2 && databuffer.Count > 2)
            {
                byte temp = databuffer[check_lenth];
                if (Mode == CuteMode.Data)
                {
                    if (temp <= 8)
                    {
                        data_count = temp;
                        check_lenth++;
                        goto CheckData;
                    }
                    else
                    {
                        errorflag = 3;
                        goto error;
                    }
                }
                else if (Mode == CuteMode.Param)
                {
                    name_lenth = databuffer[check_lenth];
                    if (name_lenth != 0)
                    {
                        name_lenth = temp;
                        check_lenth++;
                    }
                    else
                    {
                        errorflag = 31;
                        goto error;
                    }
                }
                else
                {
                    errorflag = 32;
                    goto error;
                }
            }
            //Lenth
            if(Mode == CuteMode.Param)
            {
                if (check_lenth == 3 && databuffer.Count > 3)
                {
                    data_count = databuffer[check_lenth]; 
                    check_lenth++;
                }
            }

        CheckData:

            if (Mode == CuteMode.Data)
            {
                ///Tail
                int Lenth = 3 + data_size * data_count;
                if (check_lenth >= 3 && databuffer.Count > Lenth)
                {
                    byte temp = databuffer[Lenth];
                    //校验正确
                    if (temp == tail)
                    {
                        //提取数据字节部分
                        List<List<byte>> frames = new List<List<byte>>();
                        //高低位置换标志位
                        bool Convert = BitConverter.IsLittleEndian ? false : true;
                        for (int index = 3; index < Lenth; index += data_size)
                        {
                            List<byte> frame = new List<byte>();
                            frame.AddRange(databuffer.GetRange(index, data_size));
                            if (Convert) frame.Reverse();
                            frames.Add(frame);
                        }
                        //转换成数据
                        FrameMsg frameMsg = new FrameMsg();
                        foreach (List<byte> singleframe in frames)
                        {
                            if (Id == 0x0f) //浮点型
                            {
                                frameMsg.data.Add(BitConverter.ToSingle(singleframe.ToArray(), 0));
                            }
                            else if ((Id & 0x08) == 0) //无符号型
                            {
                                if (data_size == 1)
                                    frameMsg.data.Add(singleframe.First());
                                else if (data_size == 2)
                                    frameMsg.data.Add(BitConverter.ToUInt16(singleframe.ToArray(), 0));
                                else
                                    frameMsg.data.Add(BitConverter.ToUInt32(singleframe.ToArray(), 0));
                            }
                            else //有符号型
                            {
                                if (data_size == 1)
                                {
                                    int num = singleframe[0] & 0x7f;
                                    if ((singleframe[0] & 0x80) != 0) num -= 128;
                                    frameMsg.data.Add(num);
                                }
                                else if (data_size == 2)
                                    frameMsg.data.Add(BitConverter.ToInt16(singleframe.ToArray(), 0));
                                else
                                    frameMsg.data.Add(BitConverter.ToInt32(singleframe.ToArray(), 0));
                            }
                        }
                        result.Add(frameMsg);
                        ///清除该帧数据
                        databuffer.RemoveRange(0, Lenth + 1);
                        goto clear;
                    }
                    else
                    {
                        errorflag = 4;
                        goto error;
                    }
                }
            }
            else if (Mode == CuteMode.Param)
            {
                //校验名字
                int Lenth = 4 + name_lenth + data_size * data_count;
                if (check_lenth >= 4 && databuffer.Count > Lenth)
                {
                    byte temp = databuffer[Lenth];
                    //帧尾校验正确
                    if (temp == tail)
                    {
                        FrameMsg frameMsg = new FrameMsg();
                        frameMsg.Name = Encoding.ASCII.GetString(databuffer.GetRange(4, name_lenth).ToArray());

                        //提取数据字节部分
                        List<List<byte>> Datas = new List<List<byte>>();
                        //高低位置换标志位
                        bool Convert = BitConverter.IsLittleEndian ? false : true;
                        for (int index = 4 + name_lenth; index < Lenth; index += data_size)
                        {
                            List<byte> data = new List<byte>();
                            data.AddRange(databuffer.GetRange(index, data_size));
                            if (Convert) data.Reverse();
                            Datas.Add(data);
                        }

                        foreach (List<byte> data in Datas)
                        {
                            frameMsg.data.Add(BitConverter.ToSingle(data.ToArray(), 0));
                        }
                        result.Add(frameMsg);
                        ///清除该帧数据
                        databuffer.RemoveRange(0, Lenth + 1);
                        goto clear;
                    }
                    else
                    {
                        errorflag = 5;
                        goto error;
                    }
                }
            }
            else
            {
                errorflag = 6;
                goto error;
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
            Mode = CuteMode.None;
            check_lenth = 0;
            Id = 0;
            data_size = data_count = name_lenth = 0;
            if (databuffer.IndexOf(sof) != -1) goto CheckHeader;
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace TCP_Helper
{
    class DataBuffer
    {
        /// <summary>
        /// 变量初始化
        /// </summary>
        private List<byte> data = new List<byte>(); //数据汇总


        /// <summary>
        /// 已经读取过的数据长度
        /// </summary>
        public int hadReadLenth { get; set; } 

        /// <summary>
        /// 数据是否改变
        /// </summary>
        /// <returns></returns>
        public bool HasChanged()
        {
            if (hadReadLenth != DataLenth()) return true;
            else return false;
        }

        /// <summary>
        /// 清除所有数据
        /// </summary>
        public void ClearData()
        {
            Monitor.Enter(data);
            data.Clear();
            hadReadLenth = 0;
            Monitor.Exit(data);
        }

        /// <summary>
        /// 在数据末尾添加新数据
        /// </summary>
        /// <param name="data"></param>
        public void AddendData(byte[] newdata)
        {
            Monitor.Enter(data);
            data.AddRange(newdata);
            Monitor.Exit(data);
        }

        /// <summary>
        /// 在数据末尾添加新数据
        /// </summary>
        /// <param name="data"></param>
        public void AddendData(byte[] newdata, int lenth)
        {
            if (newdata.Length == 0) return;
            Monitor.Enter(data);
            for (int i = 0; i < lenth; i++) data.Add(newdata[i]);
            Monitor.Exit(data);
        }


        /// <summary>
        /// 从指定位置读取指定长度，如果有的话
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="Lenth"></param>
        /// <returns></returns>

        public byte[] GetData(int startIndex, int Lenth)
        {
            List<byte> outdata = new List<byte>();
            if (startIndex < 0) startIndex = 0;
            Monitor.Enter(data);
            if (DataLenth() >= startIndex + Lenth)
            {
                outdata = data.GetRange(startIndex, Lenth);
                hadReadLenth = startIndex + Lenth;
            }
            else if (DataLenth() > startIndex)
            {
                outdata = data.GetRange(startIndex, DataLenth() - startIndex);
                hadReadLenth = DataLenth();
            }   
            Monitor.Exit(data);
            return outdata.ToArray();
        }

        /// <summary>
        /// 从上次读取的位置继续读取
        /// </summary>
        /// <param name="Lenth">小于0则返回所有新数据，大于等于零则按min{Lenth, 剩余长度}读取</param>
        /// <returns></returns>
        public byte[] GetNewData(int Lenth)
        {
            List<byte> outdata = new List<byte>();
            Monitor.Enter(data);
            int remainLenth = DataLenth() - hadReadLenth;
            if(remainLenth > 0)
            {
                int readLenth = remainLenth;
                if(Lenth >= 0) readLenth = Lenth > remainLenth ? remainLenth : Lenth;
                outdata = data.GetRange(hadReadLenth, readLenth);
                hadReadLenth += readLenth;
            }
            Monitor.Exit(data);
            return outdata.ToArray();
        }

        /// <summary>
        /// 从头获取并删除数据 
        /// </summary>
        /// <param name="Lenth">-1时为获取全部数据</param>
        /// <returns></returns>
        public byte[] GetDeleteData(int Lenth)
        {
            List<byte> outdata = new List<byte>();
            Monitor.Enter(data);
            if (DataLenth() > 0)
            {
                int readLenth = DataLenth();
                if (Lenth >= 0) readLenth = Lenth > DataLenth() ? DataLenth() : Lenth;
                outdata = data.GetRange(0, readLenth);
                DeleteData(0, outdata.Count);
            }
            Monitor.Exit(data);
            return outdata.ToArray();
        }

        /// <summary>
        /// 删除指定数据
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="Lenth"></param>
        public void DeleteData(int startIndex, int Lenth)
        {
            if (startIndex < 0) startIndex = 0;
            Monitor.Enter(data);
            if(DataLenth() > startIndex + Lenth)
            {
                data.RemoveRange(startIndex, Lenth);
                hadReadLenth -= Lenth;
                if (hadReadLenth < 0) hadReadLenth = 0;
            }
            else if (DataLenth() > startIndex)
            {
                data.Clear();
                hadReadLenth = 0;
            }
            Monitor.Exit(data);
        }

        /// <summary>
        /// 获取总接收数据长度
        /// </summary>
        /// <returns></returns>
        public int DataLenth()
        {
            return data.Count();
        }

    }
}

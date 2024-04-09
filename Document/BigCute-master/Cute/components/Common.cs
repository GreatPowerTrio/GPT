using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cute
{
    class Common
    {

        public Common()
        {

        }

        /// <summary>
        /// byte[] 转16进制
        /// </summary>
        public string Data_ToHex(byte[] data)
        {
            string newdata = string.Empty;
            foreach (byte word in data)
            {
                newdata += " " + Convert.ToString(word, 16);
            }
            return newdata;
        }


        /// <summary>
        /// 输入16进制转实际16进制，通过 ' '或','或'，' 分隔
        /// 如：输入 10 实际发送 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Hex_ToHex(string data)
        {
            try
            {
                //分割字符串，并去掉空字符
                string[] hexs = data.Split(new char[] { ' ', ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
                List<byte> Bytes = new List<byte>();
                //逐个字符变为16进制字节数据并去掉0x,0X
                foreach (string hex in hexs)
                {
                    Bytes.Add(Convert.ToByte(hex, 16));
                }
                return Bytes.ToArray();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n请用','或'，'或' '隔开每个字节", "Hex格式错误");
            }
            return null;
        }

        public int Limit_int(int number, int max, int min)
        {
            if (number > max) return max;
            else if (number < min) return min;
            else return number;
        }

    }
}

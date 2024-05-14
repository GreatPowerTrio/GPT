using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using Cute.param;

namespace Cute
{
    public partial class DataSave
    {
        /// <summary>
        /// 获取路径
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string choose_path(string filter)
        {
            using(SaveFileDialog file = new SaveFileDialog() 
            { 
                Title = "path",
                Filter = filter,
            })
            {
                if(file.ShowDialog() == DialogResult.OK)
                {
                    string path = Path.GetFullPath(file.FileName);
                    file.InitialDirectory = Path.GetDirectoryName(path);
                    file.FileName = Path.GetFileName(path);
                    return path;
                }
                return string.Empty;
            }
        }

        #region 数据串存储
   
        public void Run(string path, byte[] data, bool Hex)
        {
            using (StreamWriter StreamWriter = new StreamWriter(path, false))//覆盖该文件，重新写
            {
                if (Hex) data = Encoding.UTF8.GetBytes(new Common().Data_ToHex(data)); //16进制显示
                StreamWriter.Write(Encoding.UTF8.GetString(data));
            }
        }
        #endregion

        #region 数据帧存储

        public void Run(string path, List<List<float>> data)
        {
            using (StreamWriter StreamWriter = new StreamWriter(path, false))//覆盖该文件，重新写
            {
                int count = data.Count;
                for(int i = 0; i < count;i++)
                {
                    foreach (float num in data[i])
                    {
                        StreamWriter.Write(String.Format("{0:N7} ", num));
                    }
                    StreamWriter.Write(String.Format("\r\n"));
                }
            }
        }
        #endregion
    }
}

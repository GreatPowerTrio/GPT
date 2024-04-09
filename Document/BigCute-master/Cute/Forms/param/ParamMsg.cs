using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cute.param
{
    /// <summary>
    /// 数据类
    /// </summary>
    class ParamMsg
    {
        public string Name;
        public string Type;

        public System.Windows.Forms.Form Form = null;
        public Action<List<string>> ParamSet;
        public Func<List<string>> ParamGet;
        public Action<string> Send;
        public Action<string> WaveShow;
        public Action<bool> LabelOKEnable;
        public Action<float, float> FormChanged;
        public bool hasShowed = false; //第一次显示标志位
        /// <summary>
        ///   模式  |  单参数   |  单级PID               |  串级PID                               
        ///   下标  |    0      |  0.1.2.3.4             |  0.1.2.3.4.5.6.7.8.9                                  
        ///   意义  |   value   |  P.I.D.maxIout.maxout  |  内环P.I.D.maxIout.maxout 外环P.I.D.maxIout.maxout
        /// </summary>
        public List<float> Value = new List<float>();

        /// <summary>
        /// 参数初始化，信息保存
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public ParamMsg(string name, string type)
        {
            Name = name;
            Type = type;
            if (type == "单参数")
            {
                Value.Add(0);
                FormOneParam form = new FormOneParam();
                ParamSet = form.ParamSet;
                ParamGet = form.ParamGet;
                LabelOKEnable = form.OKShowEnalbe;
                FormChanged = form.Form_Changed;
                form.Send += n => { Send?.Invoke(n); };
                form.WaveShow += n => { WaveShow?.Invoke(n); };
                Form = form;
            }
            else if (type == "单级PID")
            {
                Value.AddRange(new List<float>() { 0, 0, 0, 0, 0 });
                FormSinglePID form = new FormSinglePID();
                ParamSet = form.ParamSet;
                ParamGet = form.ParamGet;
                LabelOKEnable = form.OKShowEnalbe;
                FormChanged = form.Form_Changed;
                form.Send += n => { Send?.Invoke(n); };
                form.WaveShow += n => { WaveShow?.Invoke(n); };
                Form = form;
            }
            else if(type == "串级PID")
            {
                Value.AddRange(new List<float>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
                FormCascadePID form = new FormCascadePID();
                ParamSet = form.ParamSet;
                ParamGet = form.ParamGet;
                LabelOKEnable = form.OKShowEnalbe;
                FormChanged = form.Form_Changed;
                form.Send += n => { Send?.Invoke(n); };
                form.WaveShow += n => { WaveShow?.Invoke(n); };
                Form = form;
            }
            else if(type == "滑动调参")
            {
                Value.AddRange(new List<float>() { 0, 0, 0, 0 });
                FormBar form = new FormBar();
                ParamSet = form.ParamSet;
                ParamGet = form.ParamGet;
                LabelOKEnable = form.OKShowEnalbe;
                FormChanged = form.Form_Changed;
                form.Send += n => { Send?.Invoke(n); };
                form.WaveShow += n => { WaveShow?.Invoke(n); };
                Form = form;
            }
        }

        /// <summary>
        /// 文本转数字
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<float> ParamTextToNum(List<string> text)
        {
            List<float> res = new List<float>();
            foreach (string str in text)
            {
                try
                {
                    res.Add(float.Parse(str));
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "输入格式错误");
                    return null;
                }
            }
            return res;
        }

        /// <summary>
        /// 数字转文本
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public List<string> ParamNumToText(List<float> nums)
        {
            List<string> res = new List<string>();
            foreach (float num in nums)
            {
                res.Add(num.ToString());
            }
            return res;
        }
    }
}

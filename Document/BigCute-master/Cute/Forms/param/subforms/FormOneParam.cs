using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cute.param
{
    public partial class FormOneParam : Form
    {
        public event Action<string> Send;
        public event Action<string> WaveShow;

        private Paraments param = new Paraments();
        public FormOneParam()
        {
            InitializeComponent();

            button_Send.Click += Button_Send_Click;
            button_WaveShow.Click += Button_WaveShow_Click;
            textBox_value.TextChanged += TextBox_TextChanged;
            textBox_value.GotFocus += TextBox_GotFocus;
            textBox_value.MouseUp += TextBox_MouseUp;
            SetTag(this);
            //BackColor = param.Param_PageOne.Back;
            //param.ColorSetNormalModule(this, param.Param_PageOne.Fore);
            //param.ColorSetButton(this, param.Param_PageOne.ButtonBack, param.Param_PageOne.ButtonFore);
            //param.ColorSetText(this, param.Param_PageOne.TextBack, param.Param_PageOne.TextFore);
            //label_Name.ForeColor = param.Param_PageOne.Title;
            //label_OK.ForeColor = param.Param_PageOne.Check;
        }

        #region 获取焦点时文本全选
        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            box.SelectAll();
            Flag = true;
        }

        bool Flag = true;
        private void TextBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Flag == true)
            {
                TextBox box = sender as TextBox;
                box.SelectAll();
            }
            Flag = false;
        }
        #endregion

        #region 控件大小随DPI而变
        private void SetTag(Control cons)
        {
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

        private void SetControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                con.Left = Convert.ToInt32(Convert.ToSingle(mytag[1]) * con.Parent.Width);
                con.Top = Convert.ToInt32(Convert.ToSingle(mytag[2]) * con.Parent.Height);
                con.Width = Convert.ToInt32(Convert.ToSingle(mytag[3]) * con.Parent.Width);
                con.Height = Convert.ToInt32(Convert.ToSingle(mytag[4]) * con.Parent.Height);
                if (con.Controls.Count > 0)
                {
                    SetControls(newx, newy, con);
                }
            }
        }

        /// <summary>
        /// 第一次显示窗体时控件位置调整
        /// </summary>
        public void Form_Changed(float newx, float newy)
        {
            this.Size = new Size((int)(Size.Width * newx), (int)(Size.Height * newy));
            SetControls(newx, newy, this);
        }
        #endregion

        #region 按键事件
        private void Button_WaveShow_Click(object sender, EventArgs e)
        {
            WaveShow?.Invoke(label_Name.Text);
        }

        private void Button_Send_Click(object sender, EventArgs e)
        {
            Send?.Invoke(label_Name.Text);
        }

        #endregion

        #region OK标签显示

        public void OKShowEnalbe(bool enable)
        {
            if(label_OK.InvokeRequired)
            {
                Action<bool> action = OKShowEnalbe;
                label_OK.Invoke(action, enable);
            }
            else
            {
                if (enable) label_OK.Visible = true;
                else label_OK.Visible = false;
            }
        }


        /// <summary>
        /// 文本改变时关闭OK显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            label_OK.Visible = false;
        }

        #endregion

        #region 参数处理
        /// <summary>
        /// 提取参数
        /// </summary>
        /// <returns></returns>
        public List<string> ParamGet()
        {
            List<string> param = new List<string>();
            param.Add(textBox_value.Text);
            return param;
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="param"></param>
        public void ParamSet(List<string> param)
        {
            if(param != null)
            {
                int index = 0;
                while(index <= param.Count)
                {
                    if (index == 0) TextBox_Set(textBox_value, param[index]); 
                    if (++index == 1) return;
                }
            }
        }

        private void TextBox_Set(object sender, string text)
        {
            TextBox box = sender as TextBox;
            if(box.InvokeRequired)
            {
                Action<object, string> action = TextBox_Set;
                box.Invoke(action, new object[] { sender, text });
            }
            else
            {
                box.Text = text;
            }
        }
        #endregion

        private void FormOneParam_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(param.Param_NormalBorder, 1),
                new Rectangle(new Point(0, 0), new Size(Width - 1, Height - 1)));
        }
    }
}

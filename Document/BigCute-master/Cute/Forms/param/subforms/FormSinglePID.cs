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
    public partial class FormSinglePID : Form
    {
        public event Action<string> Send;
        public event Action<string> WaveShow;
        private Paraments param = new Paraments();
        public FormSinglePID()
        {
            InitializeComponent();

            //label_Name.MouseDown += FormSinglePID_MouseDown;
            //label_Name.MouseMove += FormSinglePID_MouseMove;

            button_Send.Click += Button_Send_Click;
            button_WaveShow.Click += Button_WaveShow_Click;
            textBox_Kp.TextChanged += TextBox_TextChanged;
            textBox_Kp.GotFocus += TextBox_GotFocus;
            textBox_Kp.MouseUp += TextBox_MouseUp;
            textBox_Ki.TextChanged += TextBox_TextChanged;
            textBox_Ki.GotFocus += TextBox_GotFocus;
            textBox_Ki.MouseUp += TextBox_MouseUp;
            textBox_Kd.TextChanged += TextBox_TextChanged;
            textBox_Kd.GotFocus += TextBox_GotFocus;
            textBox_Kd.MouseUp += TextBox_MouseUp;
            textBox_MaxIOut.TextChanged += TextBox_TextChanged;
            textBox_MaxIOut.GotFocus += TextBox_GotFocus;
            textBox_MaxIOut.MouseUp += TextBox_MouseUp;
            textBox_MaxOut.TextChanged += TextBox_TextChanged;
            textBox_MaxOut.GotFocus += TextBox_GotFocus;
            textBox_MaxOut.MouseUp += TextBox_MouseUp;
            SetTag(this);
            //BackColor = param.Param_PageSinge.Back;
            //param.ColorSetNormalModule(this, param.Param_PageSinge.Fore);
            //param.ColorSetButton(this, param.Param_PageSinge.ButtonBack, param.Param_PageSinge.ButtonFore);
            //param.ColorSetText(this, param.Param_PageSinge.TextBack, param.Param_PageSinge.TextFore);
            //label_Name.ForeColor = param.Param_PageSinge.Title;
            //label_OK.ForeColor = param.Param_PageSinge.Check;
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

        #region OK标签
        public void OKShowEnalbe(bool enable)
        {
            if (label_OK.InvokeRequired)
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

        #region 数据处理
        /// <summary>
        /// 提取文本
        /// </summary>
        /// <returns></returns>
        public List<string> ParamGet()
        {
            List<string> param = new List<string>();
            param.Add(textBox_Kp.Text);
            param.Add(textBox_Ki.Text);
            param.Add(textBox_Kd.Text);
            param.Add(textBox_MaxIOut.Text);
            param.Add(textBox_MaxOut.Text);
            return param;
        }

        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="param"></param>
        public void ParamSet(List<string> param)
        {
            if (param != null)
            {
                int index = 0;
                while (index <= param.Count)
                {
                    if (index == 0) TextBox_Set(textBox_Kp, param[index]);
                    else if (index == 1) TextBox_Set(textBox_Ki, param[index]);
                    else if (index == 2) TextBox_Set(textBox_Kd, param[index]);
                    else if (index == 3) TextBox_Set(textBox_MaxIOut, param[index]);
                    else if (index == 4) TextBox_Set(textBox_MaxOut, param[index]);
                    if (++index == 5) return;
                }
            }
        }
        private void TextBox_Set(object sender, string text)
        {
            TextBox box = sender as TextBox;
            if (box.InvokeRequired)
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

        #region 鼠标移动窗体
        Point npoint = new Point();
        private void FormSinglePID_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Dock != DockStyle.None) Dock = DockStyle.None;
                Point EndPoint = new Point(Location.X + e.X - npoint.X, Location.Y + e.Y - npoint.Y);
                if (EndPoint.X < 0) EndPoint.X = 0;
                else if (EndPoint.X >= Parent.Width - Width) EndPoint.X = Parent.Width - Width;
                if (EndPoint.Y < 0) EndPoint.Y = 0;
                else if (EndPoint.Y >= Parent.Height - Height) EndPoint.Y = Parent.Height - Height;
                Location = EndPoint;
            }
        }

        private void FormSinglePID_MouseDown(object sender, MouseEventArgs e)
        {
            npoint = new Point(e.X, e.Y);
        }

        private void FormSinglePID_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(param.Param_NormalBorder, 1),
                new Rectangle(new Point(0, 0), new Size(Width - 1, Height - 1)));
        }
        #endregion
    }
}

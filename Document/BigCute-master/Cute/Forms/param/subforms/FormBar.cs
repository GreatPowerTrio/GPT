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
    public partial class FormBar : Form
    {
        public event Action<string> Send;
        public event Action<string> WaveShow;
        private Paraments param = new Paraments();
        public FormBar()
        {
            InitializeComponent();
            button_Send.Click += Button_Send_Click;
            button_WaveShow.Click += Button_WaveShow_Click;

            textBox_Max.TextChanged += TextBox_TextChanged;
            textBox_Value.TextChanged += TextBox_TextChanged;
            textBox_Min.TextChanged += TextBox_TextChanged;
            textBox_Max.GotFocus += TextBox_GotFocus;
            textBox_Max.MouseUp += TextBox_MouseUp;
            textBox_Value.GotFocus += TextBox_GotFocus;
            textBox_Value.MouseUp += TextBox_MouseUp;
            textBox_Min.GotFocus += TextBox_GotFocus;
            textBox_Min.MouseUp += TextBox_MouseUp;
            trackBar.ValueChanged += TrackBar_ValueChanged;
            SetTag(this);
            //BackColor = param.Param_PageBar.Back;
            //param.ColorSetNormalModule(this, param.Param_PageBar.Fore);
            //param.ColorSetButton(this, param.Param_PageBar.ButtonBack, param.Param_PageBar.ButtonFore);
            //param.ColorSetText(this, param.Param_PageBar.TextBack, param.Param_PageBar.TextFore);
            //label_Name.ForeColor = param.Param_PageBar.Title;
            //label_OK.ForeColor = param.Param_PageBar.Check;
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
            if(e.Button == MouseButtons.Left && Flag == true)
            {
                TextBox box = sender as TextBox;
                box.SelectAll();
            }
            Flag = false;
        }
        #endregion

        #region 滑动条改变
        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            TextBoxChange();
            Send?.Invoke(label_Name.Text);
        }

        private void TextBoxChange()
        {
            if(textBox_Value.InvokeRequired)
            {
                Action action = TextBoxChange;
                textBox_Value.Invoke(action);
            }
            else
            {
                textBox_Value.Text = trackBar.Value.ToString();
            }
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
            try
            {
                float value = float.Parse(textBox_Value.Text);
                if (value == trackBar.Value)
                    Send?.Invoke(label_Name.Text);
                else
                    trackBar.Value = (int)value;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region OK标签显示
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

        #region 参数处理
        /// <summary>
        /// 提取参数
        /// </summary>
        /// <returns></returns>
        public List<string> ParamGet()
        {
            List<string> param = new List<string>();
            param.Add(textBox_Max.Text);
            param.Add(textBox_Value.Text);
            param.Add(textBox_Min.Text);
            return param;
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="param"></param>
        public void ParamSet(List<string> param)
        {
            if (param != null)
            {
                int index = 0;
                while (index <= param.Count)
                {
                    if (index == 0)
                    {
                        TextBox_Set(textBox_Max, param[index]);
                        TrackBarValueChanged(textBox_Max);
                    }
                    else if (index == 1)
                    {
                        TextBox_Set(textBox_Value, param[index]);
                        TrackBarValueChanged(textBox_Value);
                    }
                    else if (index == 2) 
                    {
                        TextBox_Set(textBox_Min, param[index]);
                        TrackBarValueChanged(textBox_Min);
                    }
                    if (++index == 3) return;

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

        #region 按键事件

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            TrackBarValueChanged(sender as TextBox);
        }

        private void TrackBarValueChanged(TextBox box)
        {
            try
            {
                if (box.Name.Contains("Max"))
                {
                    float max = float.Parse(box.Text);
                    if (max >= trackBar.Value) trackBar.Maximum = (int)max;
                    else if (max < trackBar.Minimum)
                    {
                        MessageBox.Show("最大值不能小于最小值");
                    }
                    else
                    {
                        trackBar.Maximum = trackBar.Value = (int)max;
                    }
                }
                else if(box.Name.Contains("Min"))
                {
                    float min = float.Parse(box.Text);
                    if (min <= trackBar.Value) trackBar.Minimum = (int)min;
                    else if (min > trackBar.Maximum)
                    {
                        MessageBox.Show("最小值不能大于最大值");
                    }
                    else
                    {
                        trackBar.Minimum = trackBar.Value = (int)min;
                    }
                }
                else
                {
                    float value = float.Parse(box.Text);
                    if (value <= trackBar.Maximum && value >= trackBar.Minimum) trackBar.Value = (int)value;
                    else
                    {
                        MessageBox.Show("Value值超限");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        //消除按键提示音
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }

        #endregion

        private void FormBar_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(param.Param_NormalBorder, 1), new Rectangle(new Point(0, 0), new Size(Width - 1, Height - 1)));
        }

    }
}

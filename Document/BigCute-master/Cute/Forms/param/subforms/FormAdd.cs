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
    public partial class FormAdd : System.Windows.Forms.Form
    {
        public Action<string, string> Add;

        public FormAdd()
        {
            InitializeComponent();
        }

        private void FormAdd_Load(object sender, EventArgs e)
        {
            comboBox_Type.SelectedIndex = 0;
            TopMost = true;
        }

        private void button_Enter_Click(object sender, EventArgs e)
        {
            string Name = textBox_Name.Text.Trim();
            if (Name.Length > 0)
            {
                Add?.Invoke(Name, comboBox_Type.Text);
                textBox_Name.Text = string.Empty;
            }
            else
            {
                System.Media.SystemSounds.Exclamation.Play();
                MessageBox.Show("命名错误","error");
            }
        }

        private void textBox_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button_Enter_Click(null, null);
            }
        }

        private void textBox_Name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == System.Convert.ToChar(13))
            {
                e.Handled = true;
            }
        }
    }
}

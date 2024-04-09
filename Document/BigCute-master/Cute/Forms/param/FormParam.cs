using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections;
using Cute.param;

namespace Cute
{

    public partial class FormParam : System.Windows.Forms.Form
    {
        Paraments param = new Paraments();
        FormAdd _FormAdd = null;
        FormChart _FormChart = null;
        Hashtable _ParamTable = new Hashtable();
        private readonly DataBuffer buffer = new DataBuffer(); //数据接收缓存
        private List<List<float>> Data = new List<List<float>>(); //解析后数据按帧存放
        private Cute.param.FrameUnPack frameUnPack = new param.FrameUnPack();
        private readonly DataSave dataSave = new DataSave(); //数据存储

        private bool ParamLoadEnable = false; //参数导入标志位
        private string WaveShowEnable = string.Empty; //波形显示标志位
        private string WaveShowLastEnable = string.Empty;
        /// <summary>
        /// 发送数据
        /// </summary>
        public Func<byte[], bool> SendByteBuffer;

        public FormParam()
        {
            InitializeComponent();
            SetTag(this);

            #region ToolTips
            Action<Control, string> action = (c, s) =>
            {
                ToolTip tool = new ToolTip
                {
                    AutoPopDelay = 5000,
                    InitialDelay = 500,
                    ReshowDelay = 0,
                    ShowAlways = true
                };
                tool.SetToolTip(c, s);
            };
            action.Invoke(panel_Add, "添加参数");
            action.Invoke(panel_Load, "下位机导入参数");
            #endregion
            //#region 颜色配置
            //splitContainer.BackColor = param.Desktop_FunctionBack;
            //panel_Up1.BackColor = panel_Up2.BackColor = param.Param_NormalBorder;
            //checkedListBox_Tab.BackColor = panel_UpBack.BackColor = param.Param_TitleBack;
            //panel_Func.BackColor = Color.FromArgb(30, 30, 30);
            //splitContainer.Panel2.BackColor = panel_contain.BackColor = param.Param_PageBack;
            //#endregion
        }


        private void FormParam_Load(object sender, EventArgs e)
        {
            ///内部命令添加
            _ParamTable.Add("LOAD_START", new ParamMsg("LOAD_START", "CMD"));
        }

        #region 控件大小随窗体大小等比例缩放
        private void SetTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
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

        private void SetControls(Control cons)
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
                    SetControls(con);
                }
            }
        }

        private Size OriginSize;
        /// <summary>
        /// 第一次显示窗体时控件位置调整
        /// </summary>
        public void Form_Changed(Size size)
        {
            OriginSize = this.Size = size;
            SetControls(this);
            Hide();
        }
        #endregion

        #region 重绘事件
        /// <summary>
        /// 分隔栏改变时重绘所有子控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            splitContainer.Refresh();
        }

        private void panel_UpBack_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel; 
            e.Graphics.Clear(panel.BackColor);
            //重画边框
            e.Graphics.DrawLine(new Pen(param.Param_NormalBorder, 2), new Point(0, panel.Height - 1), new Point(panel.Width, panel.Height - 1));
        }

        #endregion

        #region 数据处理

        /// <summary>
        /// 接收新数据
        /// </summary>
        /// <param name="data"></param>
        public void DataReceived(byte[] data)
        {
            buffer.AddendData(data);
        }

        /// <summary>
        /// 数据处理
        /// </summary>
        /// <param name="arg"></param>
        public void DataHandler(List<object> arg)
        {
            if (buffer.HasChanged())
            {
                var newdata = buffer.GetDeleteData(-1);
                //Console.Write(Encoding.ASCII.GetString(newdata) + " ");
                //Console.WriteLine(new Common().Data_ToHex(newdata));
                var res = frameUnPack.Unpack(newdata);
                foreach (var frame in res)
                {
                    if (frame.Name == string.Empty) //波形显示
                    {
                        if (!string.IsNullOrEmpty(WaveShowEnable))
                        {
                            Data.Add(frame.data);
                            _FormChart.AppendData(frame.data);
                        }
                    }
                    else if (_ParamTable.Contains(frame.Name))
                    {
                        if (frame.Name == "LOAD_START")
                        {
                            MessageBox.Show("参数导入完成", "参数导入");
                            ParamLoadEnable = false;
                        }
                        else if (!ParamLoadEnable)
                        {
                            var msg = (ParamMsg)_ParamTable[frame.Name];
                            msg.LabelOKEnable?.Invoke(msg.ParamGet().SequenceEqual(msg.ParamNumToText(frame.data)));
                        }
                    }
                    else if (ParamLoadEnable) ParamLoad(frame);
                }
            }

            ///波形页面关闭后发送结束帧
            if (!string.IsNullOrEmpty(WaveShowEnable))
            {
                if (_FormChart == null || _FormChart.IsDisposed)
                {
                    WaveShowSend(WaveShowEnable);
                    WaveShowEnable = string.Empty;
                }
            }
        }

        /// <summary>
        /// 参数导入添加页
        /// </summary>
        /// <param name="msg"></param>
        private void ParamLoad(param.FrameMsg msg)
        {
            string type;
            if (msg.data.Count == 1) type = "单参数";
            else if (msg.data.Count == 5) type = "单级PID";
            else if (msg.data.Count == 3) type = "滑动调参";
            else if (msg.data.Count == 10) type = "串级PID";
            else return;
            ParamAdd(msg.Name, type);
            var Param = (ParamMsg)_ParamTable[msg.Name];
            Param.Value = msg.data;
            Param.ParamSet(Param.ParamNumToText(Param.Value));
            WaveShowLastEnable = string.Empty;
        }

        #endregion

        #region 波形显示
        private void WaveShow(string name)
        {
            if (_ParamTable.Contains(name))
            {
                if(_FormChart == null || _FormChart.IsDisposed)
                {
                    _FormChart = new FormChart();
                    float newx = (float)OriginSize.Width / 1373;
                    float newy = (float)OriginSize.Height / 931;
                    _FormChart.Form_Changed(new Size((int)(_FormChart.Width * newx), (int)(_FormChart.Height * newy)));
                    _FormChart.FormBorderStyle = FormBorderStyle.FixedSingle;
                    _FormChart.Action_Clear = ClearRec;
                    _FormChart.Action_Save = DataSave;
                    _FormChart.Show();
                    _FormChart.TimerStart();
                }
                _FormChart.Text = name + "波形";

                WaveShowSend(name);
                
                if (WaveShowEnable == name)
                {
                    WaveShowEnable = string.Empty;
                }
                else
                {
                    WaveShowEnable = name;
                    WaveShowLastEnable = WaveShowEnable;
                }
            }
        }

        private void WaveShowSend(string name)
        {
            var msg = (ParamMsg)_ParamTable[name];
            List<byte> frame = new List<byte>();
            frame.Add(0x28);
            frame.Add(0x10);
            frame.Add((byte)msg.Name.Length);
            frame.Add(0);
            frame.AddRange(Encoding.ASCII.GetBytes(msg.Name));
            frame.Add(0x29);
            SendByteBuffer?.Invoke(frame.ToArray());
        }

        private void ClearRec()
        {
            buffer.ClearData();
            Data.Clear();
            _FormChart.Clear();
        }

        private void DataSave()
        {
            string path = dataSave.choose_path("文本文档(*.txt)|*.txt");
            if (path != string.Empty)
            {
                Task saveTask = Task.Run(() =>
                {
                    SetCursor(Cursors.WaitCursor);
                    dataSave.Run(path, Data);
                    SetCursor(Cursors.Arrow);
                });
            }
        }

        private void SetCursor(Cursor cursor)
        {
            if (this.InvokeRequired)
            {
                Action<Cursor> action = SetCursor;
                this.Invoke(action, cursor);
            }
            else
            {
                Cursor = cursor;
            }
        }

        public void TimeStart()
        {
            if(_FormChart != null && !_FormChart.IsDisposed)
            {
                _FormChart.TimerStart();
            }
        }

        public void TimeStop()
        {
            if (_FormChart != null && !_FormChart.IsDisposed)
            {
                _FormChart.TimerStop();
            }
        }
        #endregion

        #region 参数页添加
        private bool ParamAdd(string name, string type)
        {
            if (_ParamTable.Contains(name))
            {
                return false;
            }
            else
            {
                ParamMsg paramMsg = new ParamMsg(name, type); //新建数据集
                paramMsg.Send = ParamSend;
                paramMsg.WaveShow = WaveShow;
                paramMsg.FormChanged((float)OriginSize.Width / 1373, (float)OriginSize.Height / 931);
                _ParamTable.Add(name, paramMsg);
                checkedListBox_Tab.Items.Add(name);
                return true;
            }
        }
        #endregion

        #region 数据发送
        private void ParamSend(string name)
        {
            if (_ParamTable.Contains(name))
            {
                var msg = (ParamMsg)_ParamTable[name];
                List<byte> frame = new List<byte>();
                frame.Add(0x28);
                frame.Add(0x10);
                frame.Add((byte)msg.Name.Length);
                List<float> datas = msg.ParamTextToNum(msg.Type != "CMD" ? msg.ParamGet() : new List<string>());
                if (datas == null) return;
                frame.Add((byte)datas.Count);
                frame.AddRange(Encoding.ASCII.GetBytes(msg.Name));
                foreach(float data in datas)
                {
                    frame.AddRange(BitConverter.GetBytes(data));
                }
                frame.Add(0x29);
                SendByteBuffer?.Invoke(frame.ToArray());
            }
        }
        #endregion

        #region 条目勾选变化

        private void checkedListBox_Tab_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckedListBox box = sender as CheckedListBox;
            var msg = (ParamMsg)_ParamTable[box.Items[e.Index].ToString()];
            Form form = (Form)msg.Form;
            if (e.CurrentValue != CheckState.Checked)
            {
                form.TopLevel = false;
                form.Parent = panel_contain;
                form.Dock = DockStyle.Left;
                if(!msg.hasShowed) //首次显示调整大小
                {
                    msg.hasShowed = true;
                    msg.FormChanged.Invoke((float)OriginSize.Width / 1373, (float)OriginSize.Height / 931);
                }
                form.Show();

                Label label_Name = (Label)form.Controls["label_Name"];
                label_Name.Text = msg.Name;
                label_Name.Location = new Point((label_Name.Parent.Width - label_Name.Width) / 2, label_Name.Top);

                Label label_OK = (Label)form.Controls["label_OK"];
                label_OK.Visible = false;

                msg.ParamSet(msg.ParamNumToText(msg.Value));
            }
            else
            {
                form.Parent = null; //为了显示参数卡保证靠左
                form.Dock = DockStyle.None;
                form.Hide();
            }
        }

        /// <summary>
        /// 控件失去焦点时清除高亮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBox_Tab_Leave(object sender, EventArgs e)
        {
            checkedListBox_Tab.ClearSelected();
        }

        /// <summary>
        /// Delete键 删除高亮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBox_Tab_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if (checkedListBox_Tab.SelectedIndex != -1)
                {
                    var msg = (ParamMsg)_ParamTable[checkedListBox_Tab.SelectedItem];
                    if (msg.Form != null && !msg.Form.IsDisposed)
                        msg.Form.Close();
                    _ParamTable.Remove(checkedListBox_Tab.SelectedItem);
                    checkedListBox_Tab.Items.RemoveAt(checkedListBox_Tab.SelectedIndex);
                }
            }
        }
        #endregion

        #region 功能区

        #region 添加
        private void panel_Add_MouseDown(object sender, MouseEventArgs e)
        {
            ///新建窗体
            if (_FormAdd == null || _FormAdd.IsDisposed)
            {
                _FormAdd = new FormAdd();
                ///按键按下时添加相关信息
                _FormAdd.Add = (name, type) =>
                {
                    if(!ParamAdd(name, type))
                    {
                        System.Media.SystemSounds.Exclamation.Play();
                        MessageBox.Show("名字重复", "error");
                    }
                };
                _FormAdd.Show();
            }
        }

        private void panel_Add_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = global::Cute.Properties.Resources.新建;
            param.BackColorChange(bitmap, param.Chart_FunctionBack, param.Chart_FunctionPre);
            panel_Add.BackgroundImage = bitmap;
        }

        private void panel_Add_MouseLeave(object sender, EventArgs e)
        {
            panel_Add.BackgroundImage = global::Cute.Properties.Resources.新建;
        }
        #endregion

        #region 下位机导入
        private void panel_Load_MouseDown(object sender, MouseEventArgs e)
        {
            ParamLoadEnable = true;
            ParamSend("LOAD_START");
        }

        private void panel_Load_MouseEnter(object sender, EventArgs e)
        {
            Bitmap bitmap = global::Cute.Properties.Resources.导入;
            param.BackColorChange(bitmap, param.Chart_FunctionBack, param.Chart_FunctionPre);
            panel_Load.BackgroundImage = bitmap;
        }

        private void panel_Load_MouseLeave(object sender, EventArgs e)
        {
            panel_Load.BackgroundImage = global::Cute.Properties.Resources.导入;
        }

        #endregion

        #endregion

        #region 窗口关闭
        public void FunctionClose()
        {
            foreach (DictionaryEntry param in _ParamTable)
            {
                ParamMsg msg = param.Value as ParamMsg;
                if (msg.Form != null && !msg.Form.IsDisposed)
                    msg.Form.Dispose();
            }
            if (_FormAdd != null && !_FormAdd.IsDisposed)
            {
                _FormAdd.Dispose();
            }
        }

        #endregion


    }
}

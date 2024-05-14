using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP_Helper
{
    /// <summary>
    /// 选择页面内容
    /// </summary>
    class SelectTab
    {
        public event Action<int> SelectedIndexChanging;
        public event Action SelectedIndexChanged;
        public class Tab
        {
            public Panel panel;
            public Label label;
            
            public Tab(Panel pan)
            {
                panel = pan;
            }

            public Tab(Panel pan, Label lab)
            {
                panel = pan;
                label = lab;
            }
        }


        public List<Tab> Tabs = new List<Tab>(); //选择页面选项管理
        private int Index = -1;
        public int SelectedIndex
        {
            get
            {
                return Index;
            }
            set
            {
                if(value != Index)
                {
                    SelectedIndexChanging?.Invoke(value);
                    Index = value;
                    SelectedIndexChanged?.Invoke();
                }
            }
        }

        /// <summary>
        /// 添加tab页
        /// </summary>
        /// <param name="pan"></param>
        /// <param name="lab"></param>
        public void Add(Panel pan, Label lab)
        {
            foreach(Tab tab in Tabs)
            {
                if (tab.panel == pan) return;
            }
            Tabs.Add(new Tab(pan, lab));
        }
        public void Add(Panel pan)
        {
            foreach (Tab tab in Tabs)
            {
                if (tab.panel == pan) return;
            }
            Tabs.Add(new Tab(pan));
        }

        /// <summary>
        /// 根据下标获取tab页
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Tab GetTab(int index)
        {
            if (index >= 0 && index < Tabs.Count)
                return Tabs[index];
            else
                return null;
        }

        /// <summary>
        /// 根据panel设置当前选择
        /// </summary>
        /// <param name="pan"></param>
        public void SelectPanel(Panel pan)
        {
            for (int i = 0; i < Tabs.Count; i++)
            {
                if (Tabs[i].panel.Name == pan.Name)
                {
                    SelectedIndex = i;
                }
            }
        }
        /// <summary>
        /// 根据label设置当前选择
        /// </summary>
        /// <param name="lab"></param>
        public void SelectLabel(Label lab)
        {
            for (int i = 0; i < Tabs.Count; i++)
            {
                if (Tabs[i].label.Name == lab.Name)
                {
                    SelectedIndex = i;
                }
            }
        }
    }
}

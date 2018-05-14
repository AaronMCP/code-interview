using System;
using System.Collections.Generic;
using System.Text;
using Telerik.WinControls.UI;
using Telerik.WinControls.Primitives;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Drawing;
using System.Collections;

namespace Hys.CommonControls
{
    public class CSComboBoxEx : CSComboBox
    {

        private bool isUpDownKeyPressed = false;
        public CSComboBoxEx()
        {
            this.ComboBoxElement.ArrowButton.MouseDown += new MouseEventHandler(ArrowButton_MouseDown);
            this.ComboBoxElement.KeyDown += new KeyEventHandler(ComboBoxElement_KeyDown);
            this.ComboBoxElement.TextBoxElement.KeyPress += new KeyPressEventHandler(TextBoxElement_KeyPress);
            this.ComboBoxElement.TextBoxElement.TextBoxItem.ReadOnly = false;
        }

        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadComboBox"; }
        }

        public override System.Drawing.Color BackColor
        {
            get
            {
                return (this.ComboBoxElement.TextBoxElement.Children[0] as RadTextBoxItem).BackColor;
            }
            set
            {
                (this.ComboBoxElement.TextBoxElement.Children[0] as RadTextBoxItem).BackColor = value;
                (this.ComboBoxElement.Children[0] as FillPrimitive).BackColor = value;
            }
        }

        public bool ReadOnly
        {
            get;
            set;
        }

        public void ClearItems()
        {
            this.m_list.Clear();
            this.Items.Clear();
        }

        public int Add(string text)
        {

            if (!m_list.Contains(text))
            {
                m_list.Add(text);
            }

            foreach (RadComboBoxItem item in this.Items)
            {
                if (item.Text == text)
                {
                    return 0;
                }
            }
            RadComboBoxItem cbxItem = new RadComboBoxItem(text);
            return this.Items.Add(cbxItem);

        }

        public int Add(string text, string szName)
        {
            RadComboBoxItem cbxItem = new RadComboBoxItem(text);
            cbxItem.Name = szName;

            return this.Items.Add(cbxItem);
        }

        public bool Contains(string text)
        {
            RadItemCollection.RadItemEnumerator iterator = this.Items.GetEnumerator();
            while (iterator.MoveNext())
            {
                if ((iterator.Current as RadComboBoxItem).Text == text)
                {
                    return true;
                }
            }

            return false;
        }

        public int FindString(string text)
        {
            int i = 0;
            RadItemCollection.RadItemEnumerator iterator = this.Items.GetEnumerator();
            while (iterator.MoveNext())
            {
                if ((iterator.Current as RadComboBoxItem).Text.ToUpper() == text.ToUpper())
                {
                    return i;
                }

                ++i;
            }

            return -1;
        }

        public void Insert(int idx, string text)
        {
            base.Items.Insert(idx, new RadComboBoxItem(text));
        }

        /// <summary>
        /// If we do not use data-binding, SelectedValue will be NULL always.
        /// So we implement this property.
        /// </summary>
        public string SelectedName
        {
            get
            {
                if (SelectedIndex >= 0 && SelectedIndex < Items.Count)
                {
                    return Items[SelectedIndex].Name;
                }

                return string.Empty;
            }
            set
            {
                RadItemCollection.RadItemEnumerator iterator = this.Items.GetEnumerator();
                while (iterator.MoveNext())
                {
                    if ((iterator.Current as RadComboBoxItem).Name.ToUpper() == value.ToUpper())
                    {
                        SelectedItem = iterator.Current;
                        return;
                    }
                }
            }
        }

        public new string SelectedText
        {
            get
            {
                if (SelectedIndex >= 0 && SelectedIndex < Items.Count)
                {
                    return Items[SelectedIndex].Text;
                }

                return string.Empty;
            }
            set
            {
                SelectedItem = null;

                RadItemCollection.RadItemEnumerator iterator = this.Items.GetEnumerator();
                while (iterator.MoveNext())
                {
                    if ((iterator.Current as RadComboBoxItem).Text.ToUpper() == value.ToUpper())
                    {
                        SelectedItem = iterator.Current;
                        return;
                    }
                }
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            // RadComboBox dose not implement the OnSelectedValueChanged Event,
            // so we do it mannually.
            OnSelectedValueChanged(e);

            base.OnSelectedIndexChanged(e);
        }

        private void ArrowButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Items.Count > 0)
            {
                int maxWidth = this.Width;
                foreach (RadItem item in this.Items)
                {
                    Graphics g = this.CreateGraphics();
                    SizeF sf = g.MeasureString(item.Text, item.Font);
                    if ((int)sf.Width > maxWidth)
                    {
                        maxWidth = (int)sf.Width;
                    }
                }
                this.DropDownWidth = maxWidth;
            }
        }

        private void ComboBoxElement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.Items.Count > 0 && IsDroppedDown)
                {
                    this.Text = this.SelectedText;
                }
            }
        }

        private void TextBoxElement_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            Keys pressedKey = (Keys)e.KeyChar;
            if (pressedKey == Keys.Escape)
            {
                this.SelectedIndex = -1;
                this.Text = string.Empty;
            }
            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key = (keyData & Keys.KeyCode);
            if ((key == Keys.Down || key == Keys.Up) && this.FocusedElement != null)
            {
                isUpDownKeyPressed = true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        //定义数组
        private ArrayList m_list = new ArrayList();
        

        //当控件成为窗体的活动控件时
        protected override void OnEnter(EventArgs e)
        {
            
            //触发父类事件
            base.OnEnter(e);
        }

        //当控件不再是活动控件时
        protected override void OnLeave(EventArgs e)
        {            
            if (this.ReadOnly && (!m_list.Contains(this.Text)))
            {
                this.Text = "";
            }

            //触发父类事件
            base.OnLeave(e);
        }

        

        //当文本更改时
        protected override void OnTextChanged(EventArgs e)
        {
            if (isUpDownKeyPressed)
            {
                isUpDownKeyPressed = false;
                return;
            }

            bool bSelected = false;
            string strCurText = this.Text;
            //如何ComboBox的集合中有值就将ComboBox中的项删除
            List<string> listCur = new List<string>();
            this.Items.Clear();
            if (this.Text.Trim().Length == 0 || m_list.Contains(strCurText))
            {
                bSelected = true;
                foreach (string strText in m_list)
                {
                    listCur.Add(strText);

                }
            }
            else
            {

                foreach (object o in this.m_list)
                {
                    if (GetChineseSpell(o.ToString()).ToLower().Contains(this.Text.ToLower()))
                    {
                        listCur.Add(o.ToString());
                        //this.Add(o.ToString());
                    }
                }


            }
            foreach (string strText in listCur)
            {
                this.Add(strText);
            }

            if (bSelected)
            {
                this.Text = strCurText;
                this.SelectedItem = strCurText;
                this.Select(this.Text.Length, 0);
            }          
            
            base.OnTextChanged(e);
        }

       

        //传入字符串获得各个汉字的首字母
        static private string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += getSpell(strText.Substring(i, 1));
            }
            return myStr;
        }

        //传入汉字获取首字母的方法(一个字符)
        static private string getSpell(string cnChar)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "*";
            }
            else
                return cnChar;
        }
    }
}

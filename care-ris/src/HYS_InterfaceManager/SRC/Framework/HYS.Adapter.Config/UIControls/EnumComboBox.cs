using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace HYS.Adapter.Config.UIControls
{
    public partial class EnumComboBox : ComboBox
    {
        #region Private Field
        private Type m_type;
        private List<object> m_list;
        #endregion

        #region Constructor
        public EnumComboBox()
        {
            InitializeComponent();
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Sorted = false;
            m_list = new List<object>();
        }
        #endregion

        #region Public Property
        public Type TheType
        {
            get
            {
                return m_type;
            }
            set
            {
                if (value == null)
                {
                    return;
                }
                if (value.IsEnum)
                {
                    m_type = value;
                    this.Items.Clear();
                    System.Reflection.FieldInfo[] fields = m_type.GetFields();
                    foreach (System.Reflection.FieldInfo fileInfo in fields)
                    {
                        if (fileInfo.FieldType.IsEnum)
                        {
                            object obj = m_type.InvokeMember(fileInfo.Name, BindingFlags.GetField, null, null, null);
                            m_list.Add(obj);
                            this.Items.Add(obj.ToString());
                        }
                    }
                    this.SelectedIndex = 0;
                }

            }
        }

        public object CurrentValue
        {
            get
            {
                if (this.SelectedIndex == -1)
                {
                    return -1;
                }
                else
                {
                    return m_list[this.SelectedIndex];
                }
            }
        }
        #endregion

        #region Override method OnPaint()
        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }
        #endregion
    }
}

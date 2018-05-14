using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;

namespace HYS.Adapter.Config.Controls
{
    public partial class CombinationControl : UserControl, IConfigControl
    {
        public CombinationControl()
        {
            InitializeComponent();
        }

        #region IConfigControl Members

        private IConfigUI ui;

        public bool LoadConfig()
        {
            string filename = Application.StartupPath + "\\OutboundDBInstall.exe";
            if (File.Exists(filename))
            {
                Assembly asm = AssemblyHelper.LoadAssembly(filename);
                if (asm == null)
                {
                    MessageBox.Show(this, AssemblyHelper.LastError.ToString(), "Load assembly error.");
                    return true;
                }

                Type type = null;
                Type[] tlist = asm.GetTypes();
                if (tlist != null)
                {
                    foreach (Type t in tlist)
                    {
                        Type it = t.GetInterface("IConfigUI");
                        if (it != null)
                        {
                            type = t;
                            break;
                        }
                    }
                }

                if (type != null)
                {
                    try
                    {
                        ui = Activator.CreateInstance(type) as IConfigUI;
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(this, err.ToString(), "Create instance error.");
                        return true;
                    }
                }

                try
                {
                    if (ui != null)
                    {
                        Control ctrl = ui.GetControl();
                        ctrl.Dock = DockStyle.Fill;
                        this.Controls.Add(ctrl);

                        ui.LoadConfig();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(this, e.ToString(), "IConfigUI GetControl error.");
                    return true;
                }
            }
            return true;
        }

        public bool SaveConfig()
        {
            if (ui != null) return ui.SaveConfig();
            return true;
        }

        #endregion
    }
}

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HYS.FileAdapter.Common
{
    public class FileUIFunction
    {
        static public bool LoadItemFromEnum(System.Windows.Forms.ComboBox cb, Type t)
        {
            //Type t = AEnum.GetType();
            FieldInfo[] fis = t.GetFields();
            foreach (FieldInfo fi in fis)
            {
                cb.Items.Add(fi.Name.ToString());
            }

            return true;
        }
    }
}

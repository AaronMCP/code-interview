using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HYS.IM.Common.XDS
{
    [Obsolete("Please use class Common.ServiceHelper.DocumentHelper instead", true)]
    public class MimeTypeHelper
    {
        public static string GetMimeTypeFromWindowsRegistry(string filename)
        {
            System.Security.Permissions.RegistryPermission regPerm = new System.Security.Permissions.RegistryPermission(System.Security.Permissions.RegistryPermissionAccess.Read, "\\HKEY_CLASSES_ROOT");
            Microsoft.Win32.RegistryKey classesRoot = Microsoft.Win32.Registry.ClassesRoot;
            FileInfo fi = new FileInfo(filename);
            string extension = fi.Extension.ToUpper();
            Microsoft.Win32.RegistryKey typeKey = classesRoot.OpenSubKey(@"MIME\Database\Content Type");

            foreach (string keyname in typeKey.GetSubKeyNames())
            {
                Microsoft.Win32.RegistryKey currentKey = classesRoot.OpenSubKey(@"MIME\Database\Content Type\" + keyname);
                string currentExtension = (string)currentKey.GetValue("Extension", null);
                if (!string.IsNullOrEmpty(currentExtension) && currentExtension.ToUpper() == extension)
                    return keyname;
            }
            return string.Empty;
        }
    }
}

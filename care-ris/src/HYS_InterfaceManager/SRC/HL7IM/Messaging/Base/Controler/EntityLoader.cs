using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Messaging.Base.Config;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Controler
{
    public static class EntityLoader
    {
        private static Exception _lastError;
        public static Exception LastError
        {
            get
            {
                return _lastError;
            }
        }
        public static string LastErrorInfor
        {
            get
            {
                if (_lastError == null) return "";
                return _lastError.ToString();
            }
        }

        private static Dictionary<string, Assembly> AssemblyList = new Dictionary<string, Assembly>();
        internal static void InitializeAssemblyList(Assembly asm)
        {
            if (asm == null) return;
            string fnameKey = asm.FullName;
            fnameKey = fnameKey.Substring(0, fnameKey.IndexOf(','));
            AssemblyList.Add(fnameKey, asm);
        }
        internal static string BaseDirectory = Application.StartupPath;
        public static Assembly LoadAssembly(string filename)
        {
            try
            {
                _lastError = null;
                string fname = ConfigHelper.GetFullPath(BaseDirectory, filename);

                string fnameKey = Path.GetFileNameWithoutExtension(fname);
                if (AssemblyList.ContainsKey(fnameKey)) return AssemblyList[fnameKey];
                
                LoadingAssemblyFileName = fname;
                LoadingAssemblyFileNames.Add(fname);

                Assembly a = Assembly.LoadFile(fname);
                AssemblyList.Add(fnameKey, a);
                return a;
            }
            catch (Exception err)
            {
                _lastError = err;
                return null;
            }
        }
        public static string LoadingAssemblyFileName = "";
        public static List<string> LoadingAssemblyFileNames = new List<string>();

        public static T GetEntryAttribute<T>(Type t) where T : EntryAttribute
        {
            if (t == null) return null;
            object[] ol = t.GetCustomAttributes(typeof(T), false);
            if (ol == null || ol.Length < 1) return null;
            return (T)ol[0];
        }

        public static Type FindEntryType(string filename, string typename)
        {
            Assembly asm = LoadAssembly(filename);
            return FindEntryType(asm, typename);
        }
        public static Type FindEntryType(Assembly asm, string typename)
        {
            if (asm == null) return null;
            return asm.GetType(typename, false);
        }
        public static Type[] FindEntryType<T>(string filename) where T : EntryAttribute
        {
            Assembly asm = LoadAssembly(filename);
            if (asm == null) return null;
            return FindEntryType<T>(asm);
        }
        public static Type[] FindEntryType<T>(Assembly asm) where T : EntryAttribute
        {
            if (asm == null) return null;

            List<Type> list = new List<Type>();

            Type[] tlist = asm.GetTypes();
            if (tlist != null)
            {
                foreach (Type t in tlist)
                {
                    T a = GetEntryAttribute<T>(t);
                    if (a != null) list.Add(t);
                }
            }

            return list.ToArray();
        }

        public static T CreateEntry<T>(Type t) where T: IEntry
        {
            try
            {
                _lastError = null;
                if (t == null) return default(T);
                return (T)Activator.CreateInstance(t);
            }
            catch (Exception err)
            {
                _lastError = err;
                return default(T);
            }
        }
        public static T CreateEntry<T>(string typename) where T: IEntry
        {
            try
            {
                _lastError = null;
                Type t = Type.GetType(typename);
                return CreateEntry<T>(t);
            }
            catch (Exception err)
            {
                _lastError = err;
                return default(T);
            }
        }

        //public static EntityConfigImpl LoadEntityConfig(string cfgFileName)
        //{
        //    try
        //    {
        //        _lastError = null;
        //        string fname = ConfigHelper.GetFullPath(cfgFileName);

        //        string xmlstring = "";
        //        using (StreamReader sr = File.OpenText(fname))
        //        {
        //            xmlstring = sr.ReadToEnd();
        //        }

        //        EntityConfigImpl cfg = XObjectManager.CreateObject<EntityConfigImpl>(xmlstring);
        //        if (cfg == null) _lastError = XObjectManager.LastError;
        //        return cfg;
        //    }
        //    catch (Exception err)
        //    {
        //        _lastError = err;
        //        return null;
        //    }
        //}

        public static void PrepareControl(Control ctrl, Control parentCtrl)
        {
            if (ctrl != null) ctrl.Dock = DockStyle.Fill;

            Form frm = ctrl as Form;
            if (frm == null) return;

            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.TopLevel = false;
            frm.Visible = true;
            frm.Parent = parentCtrl;
        }
    }
}

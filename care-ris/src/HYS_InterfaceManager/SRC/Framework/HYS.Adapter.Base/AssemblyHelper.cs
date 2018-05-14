using System;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using HYS.Common.Objects.Config;

namespace HYS.Adapter.Base
{
    public class AssemblyHelper
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

        public static Assembly LoadAssembly(string filename)
        {
            try
            {
                _lastError = null;
                string fname = ConfigHelper.GetFullPath(filename);
                return Assembly.LoadFile(fname);
            }
            catch (Exception err)
            {
                _lastError = err;
                return null;
            }
        }
        public static A GetApdaterEntryAttribute<A>(Type adapterType) where A : AdapterEntryAttributeBase
        {
            if (adapterType == null) return null;
            object[] o = adapterType.GetCustomAttributes(typeof(A), false);
            if (o == null || o.Length < 1) return null;
            return (A) o[0];
        }
        public static Type FindAdapter<A>(string filename) where A : AdapterEntryAttributeBase
        {
            Assembly asm = LoadAssembly(filename);
            if (asm == null) return null;
            return FindAdapter<A>(asm);
        }
        public static Type FindAdapter<A>(Assembly asm) where A : AdapterEntryAttributeBase
        {
            if (asm == null) return null;

            Type[] tlist = asm.GetTypes();
            if (tlist != null)
            {
                foreach (Type t in tlist)
                {
                    A a = GetApdaterEntryAttribute<A>(t);
                    if (a != null)
                    {
                        return t;
                    }
                }
            }

            return null;
        }

        public static T CreateAdapter<T>(Type adapterType) where T : IAdapterBase
        {
            try
            {
                _lastError = null;
                if (adapterType == null) return default(T);
                return (T) Activator.CreateInstance(adapterType);
            }
            catch (Exception err)
            {
                _lastError = err;
                return default(T);
            }
        }
        
        //public static IAdapter CreateAdapter(Type adapterType)
        //{
        //    try
        //    {
        //        _lastError = null;
        //        if (adapterType == null) return null;
        //        return Activator.CreateInstance(adapterType) as IAdapter;
        //    }
        //    catch (Exception err)
        //    {
        //        _lastError = err;
        //        return null;
        //    }
        //}
        //public static IInBoundAdapter CreateInBoundAdapter(Type adapterType)
        //{
        //    try
        //    {
        //        _lastError = null;
        //        if (adapterType == null) return null;
        //        return Activator.CreateInstance(adapterType) as IInBoundAdapter;
        //    }
        //    catch (Exception err)
        //    {
        //        _lastError = err;
        //        return null;
        //    }
        //}
        //public static IOutBoundAdapter CreateOutBoundAdapter(Type adapterType)
        //{
        //    try
        //    {
        //        _lastError = null;
        //        if (adapterType == null) return null;
        //        return Activator.CreateInstance(adapterType) as IOutBoundAdapter;
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

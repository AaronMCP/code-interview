using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Hys.CommonControls
{
    public static class Extensions
    {
        #region Get / Set record value

        public static T GetValue<T>(this DataRow source, string columnName, T defaultValue = default(T))
        {
            return ProcessDataValue(source[columnName], defaultValue);
        }

        public static T GetValue<T>(this IDataRecord source, string columnName, T defaultValue = default(T))
        {
            return ProcessDataValue(source[columnName], defaultValue);
        }

        static T ProcessDataValue<T>(object value, T defaultValue = default(T))
        {
            return value == DBNull.Value || value == null ? defaultValue : (T)Convert.ChangeType(value, GetUnderlyingType(typeof(T)));
        }

        static Type GetUnderlyingType(Type type)
        {
            if (type.IsGenericType && typeof(Nullable<>) == type.GetGenericTypeDefinition())
                type = new NullableConverter(type).UnderlyingType;
            return type;
        }

        public static void SetValue(this DataRow source, string columnName, object value)
        {
            source[columnName] = value ?? DBNull.Value;
        }

        #endregion

        public static void Dispose(this IList source)
        {
            for (var i = 0; i < source.Count; i++)
            {
                var item = source[i] as IDisposable;
                if (item != null) item.Dispose();
            }
            source.Clear();
        }

        public static void Dispose2(this DataSet source)
        {
            source.Clear();
            for (var i = 0; i < source.Tables.Count; i++)
            {
                source.Tables[i].Dispose();
            }
            source.Tables.Clear();
            source.Dispose();
        }

        public static void FixText(this RichTextBox source)
        {
            var text = source.Text;
            var fonts = new[]
            {
                "宋体", "明體"
            };
            string font;
            if (string.IsNullOrEmpty(text)
                || string.IsNullOrEmpty(font = Array.Find(fonts, text.StartsWith)))
                return;

            var isReadOnly = source.ReadOnly;
            if (isReadOnly) source.ReadOnly = false;
            source.Select(0, font.Length);
            source.SelectedText = "";
            if (isReadOnly) source.ReadOnly = true;
        }
    }

    public class Memory
    {
        [DllImport("kernel32.dll")]
        static extern bool SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

        [DllImport("psapi.dll")]
        static extern bool EmptyWorkingSet(IntPtr hProcess);

        static DateTime? lasTime;

        static bool CheckInterval(int? interval)
        {
            if (interval == null)
                return true;

            var now = DateTime.Now;
            if (lasTime == null || now.Subtract(lasTime.Value).TotalMinutes >= interval.Value)
            {
                lasTime = now;
                return true;
            }

            return false;
        }

        public static void GCCollect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public static void SetProcessWorkingSetSize()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                using (var process = Process.GetCurrentProcess())
                    SetProcessWorkingSetSize(process.Handle, -1, -1);
        }

        public static void EmptyWorkingSet()
        {
            using (var process = Process.GetCurrentProcess())
                EmptyWorkingSet(process.Handle);
        }

        public static void Flush(int? interval = null)
        {
            GCCollect();
            if (CheckInterval(interval))
                SetProcessWorkingSetSize();
        }

        public static void Flush2(int? interval = null)
        {
            GCCollect();
            if (CheckInterval(interval))
                EmptyWorkingSet();
        }
    }
}

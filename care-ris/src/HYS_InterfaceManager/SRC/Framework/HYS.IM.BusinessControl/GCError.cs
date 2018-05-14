using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.BusinessControl
{
    public class GCError
    {
        private static Exception _lastError;
        private static string _errorString;

        internal static void ClearLastError()
        {
            _lastError = null;
            _errorString = "";
        }
        internal static void SetLastError(Exception e)
        {
            _lastError = e;
            _errorString = (e != null) ? e.Message : "";
        }
        internal static void SetLastError(string err)
        {
            _errorString = err;
        }

        public static string LastErrorInfor
        {
            get
            {
                //if (_errorString != null)
                //{
                //    return _errorString;
                //}
                //else
                //{
                //    if (_lastError == null) return "";
                //    return _lastError.ToString();
                //}
                return _errorString;
            }
        }
        public static Exception LastError
        {
            get { return _lastError; }
        }
    }
}

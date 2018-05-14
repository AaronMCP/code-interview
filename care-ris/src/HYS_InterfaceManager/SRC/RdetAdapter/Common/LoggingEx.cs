using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;

namespace HYS.RdetAdapter.Common
{

    /// <summary>
    /// Encapsulate HYS.Adapter.base.logging
    /// Add output level control
    /// </summary>
    //public class LoggingEx : Logging
    //{
        
    //    LogType _LogLevel = LogType.Debug;

    //    public LoggingEx()
    //    {

    //    }

    //    public LoggingEx(LogType lt, string FileName):base(FileName)
    //    {
    //        _LogLevel = lt;            
    //    }
 
    //    public LogType LogLevel
    //    {
    //        get { return _LogLevel; }
    //        set { _LogLevel = value; }
    //    }


    //    #region New Write
            
    //    new public void Write(string msg)
    //    {
    //        Write(LogType.Debug, msg, false);
    //    }
    //    new public void Write(LogType type, string msg)
    //    {
    //        Write(type, msg, false);
    //    }
    //    new public void Write(string msg, bool timeMark)
    //    {
    //        Write(LogType.Debug, msg, timeMark);
    //    }
    //    new public void Write(LogType type, string msg, bool timeMark)
    //    {
    //        switch(_LogLevel)
    //        {
    //            case LogType.Debug:
    //                base.Write(type, msg, timeMark);
    //                break;
    //            case LogType.Warning:
    //            {
    //                switch (type)
    //                {
    //                    case LogType.Warning:
    //                    case LogType.Error:
    //                        base.Write(type, msg, timeMark);
    //                        break;
    //                }
    //                break;  
    //            }
    //            case LogType.Error:
    //            {
    //                switch (type)
    //                {
    //                    case LogType.Error:
    //                        base.Write(type, msg, timeMark);
    //                        break;
    //                }
    //                break;
    //            }
    //        }

    //    }

    //    #endregion



        
    //}


    public class PackageLog
    {
        static public void WritePkgLog(object pkg, Logging _log)
        {
            if (pkg.GetType().IsSubclassOf(typeof(CmdReqBase)) || pkg.GetType() == typeof(CmdReqBase)) //or cast to CmdReqBase
            {
                CmdReqBase req = (CmdReqBase)pkg;                
                _log.Write(LogType.Debug, "Command=" + req.Command);
                for(int i=0;i< req.GetParamCount();i++)
                {
                    _log.Write(LogType.Debug,  req.GetParamName(i) + "=" + req.GetParamValue(i));
                }
                _log.Write(LogType.Debug, "DONE");
            }
            if (pkg.GetType() == typeof(CmdRespBase))
            {
                CmdRespBase resp = (CmdRespBase)pkg;                
                _log.Write(LogType.Debug, "ErrorCode=" + resp.ErrorCode);
                for (int i = 0; i < resp.GetParamCount(); i++)
                {
                    _log.Write(LogType.Debug, resp.GetParamName(i) + "=" + resp.GetParamValue(i));
                }
                _log.Write(LogType.Debug, "DONE");
            }
        }
    }



    public class StarFile
    {
        static public string BackupFile(string sFileName)
        {
            try
            {
                if (!File.Exists(sFileName))
                    return "";

                for (int i = 0; i < 100; i++)
                {
                    string sBakFileName = sFileName + ".bak" + i.ToString();
                    if (File.Exists(sBakFileName))
                        continue;
                    else
                        File.Copy(sFileName, sBakFileName);
                    return sBakFileName;
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace LogServer
{
    
    public interface ILogManager
    {
        //Methods for each type of severity
        string GetLogHomeDir();
        string GetLogReserveDay();
        void Debug(long lModule, string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo);
        void Info(long lModule, string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo);
        void Warn(long lModule, string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo);
        void Error(long lModule, string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo);
        void Fatal(long lModule, string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo);
        void SendLog(long lModule, string szModuleInstanceName, long lSeverity, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo);
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CareRIS.Log
{
    
    public interface ILogManager
    {
        //Methods for each type of severity      
        void Debug(string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo);
        void Info(string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo);
        void Warn(string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo);
        void Error(string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo);
        void Fatal(string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo);
        void SendLog(string szModuleInstanceName, long lSeverity, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo);
        
    }
}

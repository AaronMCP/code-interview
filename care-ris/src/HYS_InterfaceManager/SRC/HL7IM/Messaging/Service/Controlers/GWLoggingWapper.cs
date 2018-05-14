using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWLog = HYS.Common.Objects.Logging;
using HYS.IM.Common.Logging;

namespace HYS.IM.Messaging.Service.Controlers
{
    internal class GWLoggingWapper : GWLog.ILogging
    {
        private LogControler _log = null;
        public GWLoggingWapper(LogControler log)
        {
            _log = log;
        }

        #region ILogging Members

        public void Write(string msg)
        {
            _log.Write(msg);
        }

        public void Write(GWLog.LogType type, string msg)
        {
            switch (type)
            {
                case GWLog.LogType.Debug:
                    _log.Write(LogType.Debug, msg);
                    break;
                case GWLog.LogType.Info:
                    _log.Write(LogType.Information, msg);
                    break;
                case GWLog.LogType.Warning:
                    _log.Write(LogType.Warning, msg);
                    break;
                case GWLog.LogType.Error:
                    _log.Write(LogType.Error, msg);
                    break;
                default:
                    _log.Write(msg);
                    break;
            }
        }

        public void Write(Exception err)
        {
            _log.Write(err);
        }

        #endregion
    }
}

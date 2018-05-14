using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace Hys.Platform.CrossCutting.LogContract
{
    public class Log4netImpl:ICommonLog
    {
        private readonly object objLock = new object();

        static Log4netImpl()
        {
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase),
                "log4net.config");
            if (fileName.StartsWith(@"file:\"))
            {
                fileName = fileName.Substring(@"file:\".Length);
            }
            XmlConfigurator.ConfigureAndWatch(new FileInfo(fileName));
        }

        private static readonly ILog logger = LogManager.GetLogger("CommonLog");

        public void Log(LogLevel logLevel, object message)
        {
            lock (objLock)
            {
                switch (logLevel)
                {
                    case LogLevel.Debug:
                        logger.Debug(message);
                        break;
                    case LogLevel.Trace:
                        logger.Debug(message);
                        break;
                    case LogLevel.Info:
                        logger.Info(message);
                        break;
                    case LogLevel.Error:
                        logger.Error(message);
                        break;
                    case LogLevel.Fatal:
                        logger.Fatal(message);
                        break;
                    default:
                        logger.Info(message);
                        break;
                }
            }
        }
    }
}

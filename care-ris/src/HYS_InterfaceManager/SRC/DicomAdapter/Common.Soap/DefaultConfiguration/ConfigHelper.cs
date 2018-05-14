using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using HYS.Common.Objects.Logging;

namespace HYS.Common.Soap.DefaultConfiguration
{
    public class ConfigHelper
    {
        public static void WriteConfigToFolder(string configFileName, string targetFolderPath, bool overWrite, ILogging log)
        {
            try
            {
                string targetFileName = Path.Combine(targetFolderPath, configFileName);

                if (!overWrite)
                {
                    if (File.Exists(targetFileName)) return;
                }

                Assembly asm = Assembly.GetExecutingAssembly();
                string resourceName = string.Format("HYS.Common.Soap.DefaultConfiguration.{0}", configFileName);
                using (Stream xmlStream = asm.GetManifestResourceStream(resourceName))
                {
                    using (Stream file = File.OpenWrite(targetFileName))
                    {
                        CopyStream(xmlStream, file);
                    }
                }
            }
            catch (Exception err)
            {
                log.Write(err);
            }
        }
        public static void WriteConfigToFile(string configFileName, string targetFilePath, bool overWrite, ILogging log)
        {
            try
            {
                if (!overWrite)
                {
                    if (File.Exists(targetFilePath)) return;
                }

                Assembly asm = Assembly.GetExecutingAssembly();
                string resourceName = string.Format("HYS.Common.Soap.DefaultConfiguration.{0}", configFileName);
                using (Stream xmlStream = asm.GetManifestResourceStream(resourceName))
                {
                    using (Stream file = File.OpenWrite(targetFilePath))
                    {
                        CopyStream(xmlStream, file);
                    }
                }
            }
            catch (Exception err)
            {
                log.Write(err);
            }
        }
        public static string GetConfig(string configFileName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string resourceName = string.Format("HYS.Common.Soap.DefaultConfiguration.{0}", configFileName);
            using (Stream xmlStream = asm.GetManifestResourceStream(resourceName))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    CopyStream(xmlStream, ms);
                    return Encoding.UTF8.GetString(ms.GetBuffer());
                }
            }
        }

        private static void CopyStream(Stream input, Stream output)
        {
            int len;
            byte[] buffer = new byte[1024];
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        } 
    }
}

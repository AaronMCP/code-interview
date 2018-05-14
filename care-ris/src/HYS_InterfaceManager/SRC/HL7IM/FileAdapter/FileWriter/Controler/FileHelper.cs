using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HYS.IM.MessageDevices.FileAdpater.FileWriter.Controler
{
    public static class FileHelper
    {
        public static void MoveFile(string sourceFile, string distFile)
        {
            string distDirectory = Path.GetDirectoryName(distFile);
            if (!Directory.Exists(distDirectory))
            {
                Directory.CreateDirectory(distDirectory);
            }

            if (File.Exists(distFile))
            {
                File.Delete(distFile);
            }

            File.Move(sourceFile, distFile);
        }

        internal static void WriteFile(string filePath, string sendData,string strCodePageName)
        {
            string strDir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(strDir))
            {
                Directory.CreateDirectory(strDir);
            }
            File.WriteAllText(filePath, sendData, Encoding.GetEncoding(strCodePageName));
        }
    }
}

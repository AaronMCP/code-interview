using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Windows.Lib;

namespace HYS.FileAdapter.Common
{
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


        /// <summary>
        /// return files, exclude folder
        /// </summary>
        /// <param name="sFileToken"></param>
        /// <param name="sFolder"></param>
        /// <returns></returns>
        static public string[] FindFiles(string sFileToken, string sFolder)
        {
            FindData fd = new FindData();
            IntPtr handle = Kernel32.FindFirstFile(sFileToken, fd);

            System.Collections.ArrayList alFiles = new System.Collections.ArrayList();

            while (handle != null)
            {
                if ((fd.fileAttributes & (int)FileAttributes.Archive) == (int)FileAttributes.Archive)
                {
                    alFiles.Add(fd.fileName);
                }

                if (Kernel32.FindNextFile(handle, fd) == 0)
                    break;
            }

            string[] files = new string[alFiles.Count];
            for (int i = 0; i < alFiles.Count; i++)
                files[i] = Convert.ToString(alFiles[i]);
            return files;
        }

        static public string[] FindFolders(string sFolderToken, string sFolder)
        {
            FindData fd = new FindData();
            IntPtr handle = Kernel32.FindFirstFile(sFolderToken, fd);

            System.Collections.ArrayList alFiles = new System.Collections.ArrayList();

            while (handle != null)
            {
                if ((fd.fileAttributes & (int)FileAttributes.Directory) == (int)FileAttributes.Directory)
                {
                    alFiles.Add(fd.fileName);
                }

                if (Kernel32.FindNextFile(handle, fd) == 0)
                    break;
            }

            string[] files = new string[alFiles.Count];
            for (int i = 0; i < alFiles.Count; i++)
                files[i] = Convert.ToString(alFiles[i]);
            return files;
        }

    }


}

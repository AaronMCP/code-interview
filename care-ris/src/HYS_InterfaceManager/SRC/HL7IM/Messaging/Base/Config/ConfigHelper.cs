using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HYS.IM.Messaging.Base.Config
{
    public static class ConfigHelper
    {
        public static string GetFullPath(string path)
        {
            if (Path.IsPathRooted(path)) return path;
            //return Application.StartupPath + "\\" + path;
            return Path.Combine(Application.StartupPath, path);
        }

        public static string GetRelativePath(string path)
        {
            string directFilePath = path;
            if (!Path.IsPathRooted(directFilePath)) return directFilePath;

            // AppPath = C:\A\B
            string fromPath = Application.StartupPath.ToLowerInvariant();

            string toPath = Path.GetDirectoryName(directFilePath);
            string toFile = Path.GetFileName(directFilePath);

            if (toPath.ToLowerInvariant().Contains(fromPath))   // directFilePath = C:\A\B\D\f.exe or C:\A\B\f.exe
            {
                string opath = toPath.Substring(fromPath.Length);
                if (opath.StartsWith("\\")) opath = opath.TrimStart('\\');
                else if (opath.StartsWith("/")) opath = opath.TrimStart('/');
                opath = Path.Combine(opath, toFile);
                return opath;
            }
            else // directFilePath = C:\A\D\f.exe or C:\A\f.exe or D:\A\f.exe
            {
                char[] fList = fromPath.Replace('/', '\\').ToCharArray();
                char[] tList = toPath.ToLowerInvariant().Replace('/', '\\').ToCharArray();

                int flen = fList.Length;
                int tlen = tList.Length;
                int len = flen > tlen ? flen : tlen;

                int i = 0;
                for (; i < len; i++)
                {
                    char cf = i < flen ? fList[i] : '\0';
                    char ct = i < tlen ? tList[i] : '\0';
                    if (cf != ct)
                    {
                        for (; i > 0; i--)
                        {
                            int pre = i - 1;
                            char c = fList[pre];
                            if (c == '\\') break;
                        }
                        break;
                    }
                }

                if (i < 1) return directFilePath;   // D:\A\f.exe

                StringBuilder sb = new StringBuilder();

                if (i < flen)
                {
                    string diffFromPath = fromPath.Substring(i).TrimStart('\\');
                    string[] pathDepth = diffFromPath.Split('\\');
                    for (int j = 0; j < pathDepth.Length; j++) sb.Append("..\\");
                }

                if (i < tlen)
                {
                    string diffToPath = toPath.Substring(i);
                    sb.Append(diffToPath);
                }

                string opath = sb.ToString();
                opath = Path.Combine(opath, toFile);
                return opath;
            }
        }

        public static string EnsurePathSlash(string path)
        {
            if (path == null || path.Length < 1 || path.EndsWith("\\")) return path;
            return path + "\\";
        }

        /// <summary>
        /// Transform relative path to rooted path.
        /// </summary>
        /// <param name="rootedFromPath">Should be a rooted path.</param>
        /// <param name="relativePath">Could be a rooted path or relative path.</param>
        /// <returns>Return the rooted path corresponding to the path in the parameter relativePath.</returns>
        public static string GetFullPath(string rootedFrom, string relativePath)
        {
            if (Path.IsPathRooted(relativePath)) return relativePath;
            //return Application.StartupPath + "\\" + path;
            return Path.Combine(rootedFrom, relativePath);
        }

        /// <summary>
        /// Transform rooted path to relative path.
        /// </summary>
        /// <param name="rootedFrom">Should be a rooted path.</param>
        /// <param name="rootedPath">Could be a rooted path or relative path.</param>
        /// <returns>Return the relative path corresponding to the path in the parameter rootedPath.</returns>
        public static string GetRelativePath(string rootedFrom, string rootedPath)
        {
            string directFilePath = rootedPath;
            if (!Path.IsPathRooted(directFilePath)) return directFilePath;

            // AppPath = C:\A\B
            string fromPath = (rootedFrom != null && rootedFrom.Length > 0)
                ? rootedFrom.ToLowerInvariant() : Application.StartupPath.ToLowerInvariant();
            
            // 20100329
            fromPath = DismissDotDotInThePath(fromPath);

            string toPath = Path.GetDirectoryName(directFilePath);
            string toFile = Path.GetFileName(directFilePath);

            if (toPath.ToLowerInvariant().Contains(fromPath))   // directFilePath = C:\A\B\D\f.exe or C:\A\B\f.exe
            {
                string opath = toPath.Substring(fromPath.Length);
                if (opath.StartsWith("\\")) opath = opath.TrimStart('\\');
                else if (opath.StartsWith("/")) opath = opath.TrimStart('/');
                opath = Path.Combine(opath, toFile);
                return opath;
            }
            else // directFilePath = C:\A\D\f.exe or C:\A\f.exe or D:\A\f.exe
            {
                char[] fList = fromPath.Replace('/', '\\').ToCharArray();
                char[] tList = toPath.ToLowerInvariant().Replace('/', '\\').ToCharArray();

                int flen = fList.Length;
                int tlen = tList.Length;
                int len = flen > tlen ? flen : tlen;

                int i = 0;
                for (; i < len; i++)
                {
                    char cf = i < flen ? fList[i] : '\0';
                    char ct = i < tlen ? tList[i] : '\0';
                    if (cf != ct)
                    {
                        for (; i > 0; i--)
                        {
                            int pre = i - 1;
                            char c = fList[pre];
                            if (c == '\\') break;
                        }
                        break;
                    }
                }

                if (i < 1) return directFilePath;   // D:\A\f.exe

                StringBuilder sb = new StringBuilder();

                if (i < flen)
                {
                    string diffFromPath = fromPath.Substring(i).TrimStart('\\');
                    string[] pathDepth = diffFromPath.Split('\\');
                    for (int j = 0; j < pathDepth.Length; j++) sb.Append("..\\");
                }

                if (i < tlen)
                {
                    string diffToPath = toPath.Substring(i);
                    sb.Append(diffToPath);
                }

                string opath = sb.ToString();
                opath = Path.Combine(opath, toFile);
                return opath;
            }
        }

        /// <summary>
        /// Process (dismiss) all ".." in the path
        /// </summary>
        /// <param name="rootedPathContainingDotDot">Should be rooted path contain or not contain the ".."</param>
        /// <returns></returns>
        public static string DismissDotDotInThePath(string rootedPathContainingDotDot)
        {
            string path = rootedPathContainingDotDot;
            path = path.Replace('/', '\\');

            string[] folders = path.Split('\\');
            List<FolderNeeded> dicFolder = new List<FolderNeeded>();

            int upCount = 0;
            for (int i = folders.Length - 1; i >= 0; i--)
            {
                string f = folders[i];
                if (f == null || f.Length < 1) continue;
                if (f == "..")
                {
                    dicFolder.Add(new FolderNeeded(i, false));
                    upCount++;
                }
                else
                {
                    if (upCount > 0)
                    {
                        dicFolder.Add(new FolderNeeded(i, false));
                        upCount--;
                    }
                    else
                    {
                        dicFolder.Add(new FolderNeeded(i, true));
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            for (int i = dicFolder.Count - 1; i >= 0; i--)
            {
                FolderNeeded fn = dicFolder[i];
                if (!fn.Needed) continue;
                sb.Append(folders[fn.Index]).Append('\\');
            }

            path = sb.ToString().TrimEnd('\\');
            return path;
        }
        private struct FolderNeeded
        {
            public readonly int Index;
            public readonly bool Needed;
            public FolderNeeded(int i, bool n)
            {
                Index = i;
                Needed = n;
            }
        }
    }
}

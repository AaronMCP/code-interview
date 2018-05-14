using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace HYS.FileAdapter.Common
{
    public class IniFile2
    {
        public string Path;
        public Encoding Encoder;

        private class BufferWrapper
        {
            public BufferWrapper(byte[] b, int l)
            {
                Buffer = b;
                Length = l;
            }
            public const int FixLength = 1024;
            public readonly byte[] Buffer;
            public readonly int Length;
        }
        public static Encoding GetEncoder(string codePageName)
        {
            if (codePageName == null || codePageName.Length < 1)
            {
                return Encoding.Default;
            }
            else
            {
                return Encoding.GetEncoding(codePageName);
            }
        }
        public static string ReadFile(string fname, Encoding encoder)
        {
            if (!File.Exists(fname)) return null;
            using (FileStream fs = File.OpenRead(fname))
            {
                //StringBuilder sb = new StringBuilder();
                //byte[] b = new byte[1024];
                //while (fs.Read(b, 0, b.Length) > 0)
                //{
                //    string str = encoder.GetString(b);
                //    sb.Append(str);
                //}
                //return sb.ToString().TrimEnd('\0');

                bool isEOF = false;
                List<BufferWrapper> buffers = new List<BufferWrapper>();
                while (!isEOF)
                {
                    byte[] b = new byte[BufferWrapper.FixLength];
                    int numofBytes = fs.Read(b, 0, b.Length);
                    if (numofBytes > 0)
                    {
                        BufferWrapper bw = new BufferWrapper(b, numofBytes);
                        buffers.Add(bw);
                    }
                    else
                    {
                        isEOF = true;
                    }
                }
                
                int clength = 0;
                byte[] totalBuffer = new byte[buffers.Count * BufferWrapper.FixLength];
                foreach (BufferWrapper bw in buffers)
                {
                    bw.Buffer.CopyTo(totalBuffer, clength);
                    clength += bw.Buffer.Length;
                }

                string str = encoder.GetString(totalBuffer);
                return str.TrimEnd('\0').TrimStart();        // trim utf-8 header !!!  20071218
            }
        }
        public static bool WriteFile(string fname, string content, Encoding encoder)
        {
            if (content == null) return false;

            using (FileStream fs = File.Open(fname, FileMode.Create))
            {
                byte[] blist = encoder.GetBytes(content);
                fs.Write(blist, 0, blist.Length);
            }

            return true;
        }

        public IniFile2(string INIPath, string codePageName)
        {
            Path = INIPath;
            Encoder = GetEncoder(codePageName);
        }
        public IniFile2(string INIPath, int codePage)
        {
            Path = INIPath;
            Encoder = Encoding.GetEncoding(codePage);
        }
        public IniFile2(string INIPath)
            : this(INIPath, null)
        {
        }

        private string _content;
        private string ReadContent()
        {
            return ReadFile(Path, Encoder);
        }
        private bool WriteContent()
        {
            return WriteFile(Path, _content, Encoder);
        }
        public string Content
        {
            get
            {
                if (_content == null) _content = ReadContent();
                return _content;
            }
        }

        private static string _SectionStart = "[";
        private static string _SectionEnd = "]\r\n";
        private static string _KeyEnd = "=";
        private static string _ValueEnd = "\r\n";

        private Dictionary<string, Dictionary<string, string>> _dom;
        private Dictionary<string, Dictionary<string, string>> LoadDOM()
        {
            string content = Content;
            if (content == null) return null;
            content = "\r\n" + content;

            Dictionary<string, string> sectList = new Dictionary<string, string>();
            string[] strList = content.Split(new string[] { "\r\n" + _SectionStart }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in strList)
            {
                int index = str.IndexOf(_SectionEnd);
                if (index < 0) continue;

                string sectionName = str.Substring(0, index).Trim();
                string sectionContent = "";

                index += _SectionEnd.Length;
                if (index < str.Length) sectionContent = str.Substring(index, str.Length - index);

                sectList[sectionName] = sectionContent;
            }

            Dictionary<string, Dictionary<string, string>> dom = new Dictionary<string, Dictionary<string, string>>();
            foreach (KeyValuePair<string, string> sect in sectList)
            {
                string sectName = sect.Key;
                string[] pairList = sect.Value.Split(new string[] { _ValueEnd }, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, string> sectDOM = new Dictionary<string, string>();
                dom[sectName] = sectDOM;
                foreach (string pair in pairList)
                {
                    int index = pair.IndexOf(_KeyEnd);
                    if (index < 0) continue;

                    string key = pair.Substring(0, index).Trim();
                    string value = "";

                    index += _KeyEnd.Length;
                    if (index < pair.Length) value = pair.Substring(index, pair.Length - index);

                    sectDOM[key] = value;
                }
            }

            return dom;   
        }
        public Dictionary<string, Dictionary<string, string>> DOM
        {
            get
            {
                if (_dom == null) _dom = LoadDOM();
                return _dom;
            }
        }

        public string ReadValue(string Section, string Key)
        {
            return ReadValue(Section, Key, "");
        }
        public string ReadValue(string Section, string Key, string Default)
        {
            Dictionary<string, Dictionary<string, string>> dom = DOM;
            if (dom != null && dom.ContainsKey(Section))
            {
                Dictionary<string, string> sect = dom[Section];
                if (sect != null && sect.ContainsKey(Key))
                {
                    return sect[Key];
                }
            }
            return Default;
        }
        public void WriteValue(string Section, string Key, string Value)
        {
            WriteValue(Section, Key, Value, true);
        }
        public void WriteValue(string Section, string Key, string Value, bool flush)
        {
            if (DOM == null) _dom = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, Dictionary<string, string>> dom = DOM;
            if (dom == null) return;

            Dictionary<string, string> sect = null;
            if (dom.ContainsKey(Section)) sect = dom[Section];
            if (sect == null)
            {
                sect = new Dictionary<string, string>();
                dom[Section] = sect;
            }
            sect[Key] = Value;

            if(flush) Flush();
        }

        public bool Flush()
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<string, Dictionary<string, string>> dom = DOM;
            if (dom != null)
            {
                foreach (KeyValuePair<string, Dictionary<string, string>> section in dom)
                {
                    sb.Append(_SectionStart);
                    sb.Append(section.Key);
                    sb.Append(_SectionEnd);
                    foreach (KeyValuePair<string, string> pair in section.Value)
                    {
                        sb.Append(pair.Key);
                        sb.Append(_KeyEnd);
                        sb.Append(pair.Value);
                        sb.Append(_ValueEnd);
                    }
                }
            }
            _content = sb.ToString();
            return WriteContent();
        }
    }
}

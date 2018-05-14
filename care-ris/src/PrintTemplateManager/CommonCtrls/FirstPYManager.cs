using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Hys.CommonControls
{
    public class FirstPYManager
    {
        //private static System.Xml.XmlDocument _xmlDocPY = null;
        private static Dictionary<string, string> _dicC2E = new Dictionary<string, string>();
        //private static readonly FirstPYManager _me = new FirstPYManager();
        private static bool _bChanged = false;
        private static string _inValidChars = "'\"\\][";
        private static string PYfileDir = "";
        private static string PYfilepath = "";

        private static Dictionary<string, string> _dicPy = new Dictionary<string, string>();

        class Nested
        {
            static Nested()
            {
            }
            internal static readonly FirstPYManager pymanger = new FirstPYManager();
        }

        public static string GetCurrentWorkPath()
        {
            string str = System.Windows.Forms.Application.ExecutablePath;
            int pos = str.LastIndexOf('\\');
            if (pos > 0)
            {
                str = str.Substring(0, pos);
            }

            return str;
        }

        private FirstPYManager()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("You can't get the instance of mine!");

                string systemRoot = System.IO.Directory.GetDirectoryRoot(Environment.SystemDirectory);
                string subSystem = System.Configuration.ConfigurationSettings.AppSettings["SubSystem"];
                PYfileDir = systemRoot + "Carestream\\" + subSystem + "\\Data";
                PYfilepath = PYfileDir + "\\firstPY.tmp";

                string path = GetCurrentWorkPath() + "\\Pingyin.dic";
                System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.All));
                _dicPy = (Dictionary<string, string>)bf.Deserialize(fs);
                fs.Close();

                if (_dicC2E != null)
                {

                    if (System.IO.File.Exists(PYfilepath))
                    {
                        System.IO.FileStream fsTmp = new System.IO.FileStream(PYfilepath, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                        _dicC2E = (Dictionary<string, string>)bf.Deserialize(fsTmp);
                        fsTmp.Close();
                        //System.Diagnostics.Debug.WriteLine("Read tmp PY 1 at " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FirstPYManager Error=" + ex.Message);
            }
        }

        //~FirstPYManager()
        //{
        //    Save2TmpFile();
        //}

        public static FirstPYManager Instance
        {
            get
            {
                return Nested.pymanger;
            }
        }

        public string GetPYFirstCharacter(string src)
        {
            if (_dicC2E == null || src == null || src == string.Empty)
                return src;

            string key = src.Trim().ToUpper();
            string ret = src;

            if (_dicC2E.ContainsKey(key))
            {
                ret = _dicC2E[key];
            }
            else
            {
                string firstC = MakeFirstPY(src);

                if (firstC == string.Empty)
                {
                    return ret;
                }
                lock (_dicC2E)
                {
                    if (!_dicC2E.ContainsKey(key))
                    {
                        _dicC2E.Add(key, firstC);
                    }
                }

                _bChanged = true;

                ret = firstC;
            }

            return ret;
        }

        private string MakeFirstPY(string chineseWord)
        {
            try
            {
                if (_dicPy == null)
                    return "";

                //System.Xml.XmlNode root = _xmlDocPY.DocumentElement;

                string c = "";

                for (int i = 0; i < chineseWord.Length; i++)
                {
                    string pp = System.Convert.ToString(chineseWord[i]);
                    //Defect EK_HI00077490 Foman 2008-10-9
                    if (_inValidChars.Contains(pp))
                        return chineseWord;

                    //System.Xml.XmlNode node = root.SelectSingleNode("descendant::hz[Character='" + pp.Trim() + "']");
                    if (_dicPy.ContainsKey(pp.Trim()))
                    {
                        c += _dicPy[pp][0];
                    }
                    else
                    {
                        c += pp;
                    }
                }

                return c.Trim();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, "Error on MakeFirstPY=" + ex.Message);
            }

            return "";
        }

        public void Save2TmpFile()
        {
            if (!_bChanged)
                return;

            _bChanged = false;

            //string tmpfilepath = TreeCombo.GetCurrentWorkPath() + "\\firstPY.tmp";

            System.Diagnostics.Debug.WriteLine("Write tmp PY 0 at " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            if (!System.IO.Directory.Exists(PYfileDir))
            {
                System.IO.Directory.CreateDirectory(PYfileDir);
            }
            System.IO.FileStream fs = new System.IO.FileStream(PYfilepath, System.IO.FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.All));
            bf.Serialize(fs, _dicC2E);
            fs.Close();

            System.Diagnostics.Debug.WriteLine("Write tmp PY 1 at " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Hys.CrossCutting.Common.Utils
{
    public class PinyinUtil
    {
        private static Dictionary<string, string> _chineseCharacterToPinyin = new Dictionary<string, string>();

        // Chinese, Japanese, Korean
        private static Regex ChineseCharacterRegex = new Regex("^[\u2E80-\u9FFF]+$");

        private static bool IsChineseCharacter(string character)
        {
            return ChineseCharacterRegex.IsMatch(character);
        }

        public static void Initialize()
        {
            string strFile = HttpContext.Current.Server.MapPath("~/Configurations/PinYin/pingyin.xml");
            if (!File.Exists(strFile))
            {
                throw new FileNotFoundException("pinyin.xml");
            }
            var xDoc = XDocument.Load(strFile);
            _chineseCharacterToPinyin = xDoc.Descendants("hz").ToDictionary(e => e.Element("Character").Value, e => e.Element("Spell").Value);
        }

        public static string ToPinyin(string strChinese, bool bUpperFirstLetter, int nSeparatePolicy, string strSeparator)
        {
            if (string.IsNullOrEmpty(strChinese.Trim()))
            {
                return strChinese;
            }

            string strPinyin = string.Empty;
            strChinese = strChinese.Trim();
            bool bFirst = false;

            foreach (char sz in strChinese)
            {
                if (IsChineseCharacter(sz.ToString()))//HZ
                {
                    string pinyinValue = string.Empty;
                    if (_chineseCharacterToPinyin.TryGetValue(sz.ToString(), out pinyinValue))
                    {
                        var polyphones = pinyinValue.Split("|".ToCharArray());
                        var strLetter = (polyphones.Length == 1 ? polyphones[0] : polyphones[1]);
                        if (bUpperFirstLetter)
                        {
                            string strFirstLetter = strLetter.Substring(0, 1);
                            string strLast = strLetter.Remove(0, 1);
                            strLetter = strFirstLetter.ToUpper() + strLast;
                        }
                        //the surname
                        if (strPinyin.Length == 0)
                        {
                            strPinyin = strLetter;
                        }
                        else
                        {
                            if (nSeparatePolicy == 1)
                            {
                                strPinyin += strSeparator;
                            }
                            else
                            {
                                if (!bFirst)
                                {
                                    strPinyin += strSeparator;
                                    bFirst = true;
                                }
                            }
                            strPinyin += strLetter;
                        }
                    }
                }
                else
                {
                    strPinyin += sz;
                }
            }

            return strPinyin;
        }
    }
}

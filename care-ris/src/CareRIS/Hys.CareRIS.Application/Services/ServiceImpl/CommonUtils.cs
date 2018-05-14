using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.XPath;

namespace Csh.Hcis.GC.RisPro.Application.Services.ServiceImpl
{
    public class CommonUtils
    {
        private static XPathDocument m_xpathDoc;
        private static XPathNavigator m_xpathNav;

        public static bool IsChinese(string strWord)
        {
            string strRex = @"[\u4e00-\u9fa5]";
            return System.Text.RegularExpressions.Regex.IsMatch(strWord, strRex);
        }


        public static bool SimplifiedToEnglish(string strChinese, ref string strEnglish, bool bUpperFirstLetter, int nSeparatePolicy, string strSeparator)
        {

            try
            {
                if (m_xpathDoc == null || m_xpathNav == null)
                {
                    string strFile = HttpContext.Current.Server.MapPath("~/Configurations/PinYin/pingyin.xml");
                    if (!File.Exists(strFile))
                    {
                        //throw new FileNotFoundException("拼音文件不存在");
                        return false;
                    }
                    m_xpathDoc = new XPathDocument(strFile);
                    m_xpathNav = m_xpathDoc.CreateNavigator();
                }

                bool bFirst = false;
                string strTemp = "";
                strEnglish.Remove(0, strEnglish.Length);
                strChinese = strChinese.Trim();
                if (strChinese.Length == 0)
                {
                    return true;
                }
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("^[\u2E80-\u9FFF]+$");
                foreach (char sz in strChinese)
                {
                    if (reg.IsMatch(sz.ToString()))//HZ
                    {
                        string strHz = string.Format("/catalog/hz[Character='{0}']/Spell", sz);
                        // Compile a standard XPath expression
                        XPathExpression expr;
                        //if (m_xpathNav == null)
                        //{
                        //    OpenPingyinFile();
                        //}
                        if (m_xpathNav == null)
                        {
                            return false;
                        }
                        expr = m_xpathNav.Compile(strHz);
                        XPathNodeIterator iterator = m_xpathNav.Select(expr);
                        if (iterator.MoveNext())
                        {
                            XPathNavigator nav2 = iterator.Current.Clone();
                            string[] polyphones = null;
                            polyphones = nav2.Value.Split("|".ToCharArray());
                            var strLetter = (polyphones.Length == 1 ? polyphones[0] : polyphones[1]);
                            if (bUpperFirstLetter)
                            {
                                string strFirstLetter = strLetter.Substring(0, 1);
                                string strLast = strLetter.Remove(0, 1);
                                strLetter = strFirstLetter.ToUpper() + strLast;
                            }
                            //the surname
                            if (strTemp.Length == 0)
                            {
                                strTemp = strLetter;
                            }
                            else
                            {
                                if (nSeparatePolicy == 1)
                                {
                                    strTemp += strSeparator;
                                }
                                else
                                {
                                    if (!bFirst)
                                    {
                                        strTemp += strSeparator;
                                        bFirst = true;
                                    }
                                }
                                strTemp += strLetter;
                            }
                        }
                        else
                        {
                            strHz = sz.ToString();
                            if (bUpperFirstLetter)
                            {
                                string strFirstLetter = strHz.Substring(0, 1);
                                string strLast = strHz.Remove(0, 1);
                                strHz = strFirstLetter.ToUpper() + strLast;
                            }
                            if (strTemp.Length == 0)
                            {
                                strTemp += strHz;
                            }
                            else
                            {
                                if (nSeparatePolicy == 1)
                                {
                                    strTemp += strSeparator;
                                }
                                else
                                {
                                    if (!bFirst)
                                    {
                                        strTemp += strSeparator;
                                        bFirst = true;
                                    }
                                }
                                strTemp += strHz;
                            }
                        }
                    }
                    else
                    {
                        strTemp += sz;
                    }
                }
                    strEnglish = strTemp;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.DAO.Oam;
using Server.DAO.Oam.Impl;
using CommonGlobalSettings;
using Common.ActionResult.Framework;
using System.Xml;
using System.IO;
using Server.DAO.Oam;


namespace Server.Business.Oam.Impl
{
    public class LoginSettingsServiceImpl : ILoginSettings
    {
        private ILoginSettingsDAO LoginDAO = DataBasePool.Instance.GetDBProvider();
        private ILoginSettingsDAO LoginDAOForLock = DataBasePool.Instance.GetDBProvider();

        public void GetLoginInfo(LoginSettingsActionResult result)
        {

            List<string> myList = new List<string>();
            string[] arr = new string[] { };
            string site = "";
            result.useLoginSettings = LoginDAO.FullShowLoginBackground(ref site);
            lock (LoginDAOForLock)
            {
                XmlDocument root = loadXml(site);
                XmlNode pictures = root.SelectSingleNode("//pictures");
                if (pictures.Attributes["previous"] == null)
                {
                    XmlAttribute attribute = root.CreateAttribute("previous");
                    pictures.Attributes.Append(attribute);
                }
                if (pictures.Attributes["current"] == null)
                {
                    XmlAttribute attribute = root.CreateAttribute("current");
                    pictures.Attributes.Append(attribute);
                }
                if (pictures.Attributes["next"] == null)
                {
                    XmlAttribute attribute = root.CreateAttribute("next");
                    pictures.Attributes.Append(attribute);
                }

                XmlNode myNode = root.SelectSingleNode("//updateDateTime");
                DateTime updateDate = (myNode == null) ? DateTime.Now.Date : Convert.ToDateTime(myNode.InnerText);
                myNode = root.SelectSingleNode("//regularNum");
                int regularNum = (myNode == null) ? 1 : Convert.ToInt32(myNode.InnerText);

                myNode = root.SelectSingleNode("//title");
                if (myNode != null)
                {
                    result.Model.title = myNode.InnerText;
                }
                myNode = root.SelectSingleNode("//font");
                if (myNode != null)
                {
                    result.Model.font = myNode.InnerText;
                }
                myNode = root.SelectSingleNode("//fontStyle");
                if (myNode != null)
                {
                    result.Model.fontStyle = Convert.ToInt32(myNode.InnerText);
                }
                myNode = root.SelectSingleNode("//color");
                if (myNode != null)
                {
                    result.Model.color = myNode.InnerText;
                }
                myNode = root.SelectSingleNode("//logo");
                if (myNode != null)
                {
                    result.Model.logo = myNode.InnerText;
                }

                bool IsRandom = false;
                myNode = root.SelectSingleNode("//random");
                if (myNode != null)
                {
                    if (myNode.InnerText == "1")
                    {
                        IsRandom = true;
                    }
                }

                myNode = root.SelectSingleNode("//isRegular");
                if (myNode != null && myNode.InnerText == "0")
                {
                    myNode = root.SelectSingleNode("//IsNew");
                    if (myNode != null)
                    {
                        if (myNode.InnerText == "1")
                        {
                            pictures.Attributes["previous"].Value = pictures.Attributes["current"].Value;
                            pictures.Attributes["current"].Value = pictures.Attributes["default"].Value;
                            myNode.InnerText = "0";
                            LoginDAOForLock.WriteXML(site, root.InnerXml);
                            //root.Save(AppDomain.CurrentDomain.BaseDirectory + "Login_UI_Setting.xml");
                        }
                    }

                    myList.Add(pictures.Attributes["previous"].Value);
                    myList.Add(pictures.Attributes["default"].Value);
                    myList.Add(pictures.Attributes["default"].Value);
                }
                else
                {
                    myNode = root.SelectSingleNode("//regularUnit");
                    if (myNode != null)
                    {
                        string regularUnit = myNode.InnerText;
                        int startPosition = 1;
                        for (int i = 0; i < pictures.ChildNodes.Count; i++)
                        {
                            if (pictures.ChildNodes[i].InnerText == pictures.Attributes["default"].Value)
                            {
                                startPosition = i + 1;
                                break;
                            }
                        }

                        myNode = root.SelectSingleNode("//IsNew");
                        if (myNode != null)
                        {
                            Int32 times = GetTimes(regularNum, regularUnit, updateDate);

                            //lock (pictures)
                            //{
                            if (myNode.InnerText == "1" || pictures.Attributes["times"].Value.ToString()!= times.ToString())
                            {
                                if (pictures.ChildNodes.Count == 0)
                                {
                                    pictures.Attributes["previous"].Value = pictures.Attributes["current"].Value;
                                    pictures.Attributes["current"].Value = "";
                                    pictures.Attributes["next"].Value = "";
                                }
                                else
                                {
                                    if (IsRandom)
                                    {
                                        Random rm = new Random();
                                        pictures.Attributes["previous"].Value = pictures.Attributes["current"].Value;
                                        if (string.IsNullOrWhiteSpace(pictures.Attributes["next"].Value))
                                        {
                                            pictures.Attributes["current"].Value = pictures.ChildNodes[rm.Next(0, (pictures.ChildNodes.Count - 1))].InnerText;
                                        }
                                        else
                                        {
                                            pictures.Attributes["current"].Value = pictures.Attributes["next"].Value;
                                        }
                                        pictures.Attributes["next"].Value = pictures.ChildNodes[rm.Next(0, (pictures.ChildNodes.Count - 1))].InnerText;
                                    }
                                    else
                                    {
                                        XmlNode currentNode = GetCurrentPicture(times, startPosition, pictures);
                                        pictures.Attributes["previous"].Value = pictures.Attributes["current"].Value;
                                        pictures.Attributes["current"].Value = currentNode.InnerText;
                                        if (currentNode.NextSibling == null)
                                        {
                                            pictures.Attributes["next"].Value = pictures.FirstChild.InnerText;
                                        }
                                        else
                                        {
                                            pictures.Attributes["next"].Value = currentNode.NextSibling.InnerText;
                                        }
                                    }
                                }
                                myNode.InnerText = "0";
                                pictures.Attributes["times"].Value = times.ToString();
                                LoginDAOForLock.WriteXML(site, root.InnerXml);
                                //root.Save(AppDomain.CurrentDomain.BaseDirectory + "Login_UI_Setting.xml");
                            }
                            //}


                            myList.Add(pictures.Attributes["previous"].Value);
                            myList.Add(pictures.Attributes["current"].Value);
                            myList.Add(pictures.Attributes["next"].Value);

                        }
                    }
                }
                if (myList.Count == 0)
                {
                    myList.Add("");
                    myList.Add("");
                    myList.Add("");
                }

                result.ArrData = myList.ToArray();
            }
        }

        public void WriteXML(LoginSettingsActionResult result, string site)
        {

            XmlDocument root = loadXml(site);
            XmlElement element;
            XmlNode myNode;
            XmlNode pictures = root.SelectSingleNode("//pictures");
            if (pictures != null)
            {
                element = pictures as XmlElement;
                element.Attributes["default"].Value = result.Model.defaultPicture;
            }
            foreach (XmlNode node in pictures.ChildNodes)
            {
                pictures.RemoveChild(node);

            }
            while (pictures.ChildNodes.Count > 0)
            {
                pictures.RemoveChild(pictures.ChildNodes[0]);
            }
            foreach (string picture in result.Model.pictures)
            {
                element = root.CreateElement("picture");
                element.InnerText = picture;
                pictures.AppendChild(element);
            }

            myNode = root.SelectSingleNode("//title");
            if (myNode != null)
            {
                element = myNode as XmlElement;
                element.InnerText = result.Model.title;
            }
            myNode = root.SelectSingleNode("//font");
            if (myNode != null)
            {
                element = myNode as XmlElement;
                element.InnerText = result.Model.font;
            }
            myNode = root.SelectSingleNode("//fontStyle");
            if (myNode != null)
            {
                element = myNode as XmlElement;
                element.InnerText = result.Model.fontStyle.ToString();
            }
            myNode = root.SelectSingleNode("//color");
            if (myNode != null)
            {
                element = myNode as XmlElement;
                element.InnerText = result.Model.color;
            }
            myNode = root.SelectSingleNode("//logo");
            if (myNode != null)
            {
                element = myNode as XmlElement;
                element.InnerText = result.Model.logo;
            }
            myNode = root.SelectSingleNode("//isRegular");
            if (myNode != null)
            {
                element = myNode as XmlElement;
                element.InnerText = result.Model.isRegular;
            }
            myNode = root.SelectSingleNode("//regularNum");
            if (myNode != null)
            {
                element = myNode as XmlElement;
                element.InnerText = result.Model.regularNum;
            }
            myNode = root.SelectSingleNode("//regularUnit");
            if (myNode != null)
            {
                element = myNode as XmlElement;
                element.InnerText = result.Model.regularUnit;
            }
            myNode = root.SelectSingleNode("//random");
            if (myNode != null)
            {
                element = myNode as XmlElement;
                element.InnerText = result.Model.random;
            }
            myNode = root.SelectSingleNode("//updateDateTime");
            if (myNode != null)
            {
                element = myNode as XmlElement;
                element.InnerText = DateTime.Now.Date.ToShortDateString();
            }
            myNode = root.SelectSingleNode("//IsNew");
            if (myNode != null)
            {
                element = myNode as XmlElement;
                element.InnerText = "1";
            }
            lock (LoginDAOForLock)
            {
                LoginDAOForLock.WriteXML(site, root.InnerXml);
                //root.Save(AppDomain.CurrentDomain.BaseDirectory + "Login_UI_Setting.xml");
            }
        }

        public void ReadXML(LoginSettingsActionResult result, string site)
        {
            XmlDocument root = loadXml(site,true);  
            XmlNode myNode;
            XmlNode pictures = root.SelectSingleNode("//pictures");
            if (pictures != null)
            {                
                result.Model.defaultPicture = pictures.Attributes["default"].Value;
            }
            List<string> mylist = new List<string>();
            foreach (XmlNode node in pictures.ChildNodes)
            {
                mylist.Add(node.InnerText);
            }
            result.Model.pictures = mylist.ToArray();            

            myNode = root.SelectSingleNode("//title");
            if (myNode != null)
            {                
                result.Model.title = myNode.InnerText;
            }
            myNode = root.SelectSingleNode("//font");
            if (myNode != null)
            {
                result.Model.font = myNode.InnerText;
            }
            myNode = root.SelectSingleNode("//fontStyle");
            if (myNode != null)
            {
                result.Model.fontStyle = Convert.ToInt32(myNode.InnerText);
            }
            myNode = root.SelectSingleNode("//color");
            if (myNode != null)
            {
                result.Model.color = myNode.InnerText;               
            }
            myNode = root.SelectSingleNode("//logo");
            if (myNode != null)
            {
                result.Model.logo = myNode.InnerText;                
            }
            myNode = root.SelectSingleNode("//isRegular");
            if (myNode != null)
            {
                result.Model.isRegular = myNode.InnerText;                
            }
            myNode = root.SelectSingleNode("//regularNum");
            if (myNode != null)
            {
                result.Model.regularNum = myNode.InnerText;                 
            }
            myNode = root.SelectSingleNode("//regularUnit");
            if (myNode != null)
            {
                result.Model.regularUnit = myNode.InnerText;                 
            }
            myNode = root.SelectSingleNode("//random");
            if (myNode != null)
            {
                result.Model.random = myNode.InnerText;
            }          
        }

        private XmlDocument loadXml(string site,bool bReadOnly =false)
        {
            XmlDocument doc = new XmlDocument();
            //string Path = AppDomain.CurrentDomain.BaseDirectory + "Login_UI_Setting.xml";
            string xml;
            if (bReadOnly)
            {
                 xml = LoginDAO.GetXML(site);
            }
            else
            {
                xml = LoginDAOForLock.GetXML(site);                
            }
            doc.Load(new StringReader(xml));
            return doc;
        }

        private XmlNode GetCurrentPicture(Int32 times, Int32 startPosition, XmlNode pictures)
        {
            //bool temp=false;
            //Int32 times = GetTimes(Num, Unit, updateDate, ref temp);
            Int32 numNd;
            Int32 i=1;

            numNd = times % pictures.ChildNodes.Count == 0 ? pictures.ChildNodes.Count : times % pictures.ChildNodes.Count;

            if (startPosition + numNd - 1 <= pictures.ChildNodes.Count)
            {
                i = startPosition + numNd - 1;
            }
            else
            {
                i = startPosition + numNd - 1 - pictures.ChildNodes.Count;
            }   
            return pictures.ChildNodes[i - 1];
        }

        /// <summary>
        /// this method will return times to change the background picture from updateDate to today.
        /// </summary>
        /// <param name="Num">num+unit will decide the period to change the piture</param>
        /// <param name="Unit">unit will be days/week/month/year</param>
        /// <param name="updateDate">start date to caculating the days</param>
        /// <param name="IsFirstForNewOne">indicate if it is time to change background picture</param>
        /// <returns></returns>
        private Int32 GetTimes(Int32 Num, string Unit, DateTime updateDate)
        {
            TimeSpan tp = DateTime.Now.Date - updateDate;
            Int32 times=1;
           
            switch (Unit)
            {
                case "days"://day

                    if (DateTime.Now.Date >= updateDate)
                    {
                        Int32 days;
                        days = tp.Days + 1;

                        if (days % Num == 0)
                        { times = days / Num; }
                        else
                        { times = days / Num + 1; }
                    }
                   
                    break;

                case "week"://week 
                    if (DateTime.Now.Date >= updateDate)
                    {
                        Int32 weeks;//weeks = (totaldays - daysoffirstweeks - daysoflastweeks)/7 + 2; daysoffirstweeks = 7th - NthOfWeek4UpdateDate +1; daysoflastweeks = NthOfWeek4Today

                        if (!LoginDAOForLock.isWeekBeginFromMonday())
                        {
                            //sunday is the first day of week 
                            weeks = ((tp.Days + 1) - (7 - (Convert.ToInt32(updateDate.DayOfWeek)+1) + 1) - (Convert.ToInt32(DateTime.Now.Date.DayOfWeek) + 1)) / 7 + 2;

                            if (weeks % Num == 0)
                            { times = weeks / Num; }
                            else
                            { times = weeks / Num + 1; }
                        }
                        else
                        {
                            //monday is the first day of week   
                            weeks = ((tp.Days + 1) - (7 - (Convert.ToInt32(updateDate.DayOfWeek) == 0 ? 7 : Convert.ToInt32(updateDate.DayOfWeek)) + 1) - (Convert.ToInt32(DateTime.Now.Date.DayOfWeek) == 0 ? 7 : Convert.ToInt32(DateTime.Now.Date.DayOfWeek))) / 7 + 2;

                            if (weeks % Num == 0)
                            { times = weeks / Num; }
                            else
                            { times = weeks / Num + 1; }
                        }
                        
                    }

                    break;
                case "month"://month

                    if (DateTime.Now.Date >= updateDate)
                    {
                        Int32 months;
                        months = (DateTime.Now.Date.Month + (DateTime.Now.Date.Year - updateDate.Year) * 12) - updateDate.Month + 1;

                        if (months % Num == 0)
                        { times = months / Num; }
                        else
                        { times = months / Num + 1; }

                    }

                    break;
                case "year"://year

                    if (DateTime.Now.Date >= updateDate)
                    {
                        Int32 years;
                        years = DateTime.Now.Date.Year - updateDate.Year + 1;

                        if (years % Num == 0)
                        { times = years / Num; }
                        else
                        { times = years / Num + 1; }
                    }
                    break;
            }
            if (times <= 0)
            {
                times = 1;
            }
            return times;
        }
    }
}

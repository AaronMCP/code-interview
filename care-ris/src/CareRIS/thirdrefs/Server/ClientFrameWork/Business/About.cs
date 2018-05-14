using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.ActionResult.Framework;
using System.Xml;
using Server.DAO.ClientFramework;
using System.IO;

namespace Server.Business.ClientFramework
{
    public class About
    {
        public About()
        { }

        public void GetAboutInfo(ArrayActionResult result)
        {
            List<string> myList=new List<string>();
            string[] arr=new string[]{};
            
            //using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "about.txt",System.Text.Encoding.GetEncoding("gb2312")))
            //{
            //    string line;
            //    while ((line = sr.ReadLine()) != null)
            //    {
            //        line.Trim();
            //        if(line != string.Empty)myList.Add(line);
            //    }
            //}


            XmlDocument doc = new XmlDocument();
            string aboutPath = AppDomain.CurrentDomain.BaseDirectory + "about.xml";

            doc.Load(aboutPath);
            XmlNodeList nodeList = doc.SelectNodes("//hospital");
            foreach (XmlNode node in nodeList) myList.Add(node.InnerText);             
            result.ArrData = myList.ToArray();       
            
           
        }
    }
}

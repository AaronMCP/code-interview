using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hys.PrintTemplateManager
{
    static class Context
    {
        public static IDbConnection DbContext = new SqlConnection(ConfigurationManager.ConnectionStrings["RisConn"].ToString());
        public static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Dictionary<string, string> LangDic;

        static Context()
        {
            XDocument xdoc = XDocument.Load("lang.xml");
            var ns = xdoc.Root.Name.Namespace;
            var pairs = from pair in xdoc.Descendants(ns + "StringRes")
                        select new KeyValuePair<string, string>(
                            pair.Element(ns + "Name").Value,
                            pair.Element(ns + "Value").Value);
            LangDic = pairs.ToDictionary(k => k.Key, v => v.Value);
        }
    }
}

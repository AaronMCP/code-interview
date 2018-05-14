using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

namespace ASPNetWSAppTest
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://www.carestreamhealth.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PIXService : System.Web.Services.WebService
    {

        [WebMethod]
        public int MessageCom(string RequestMessage, out string ReturnMessage)
        {
            ReturnMessage = "Function MessageCom got the request message of: " + RequestMessage;
            return 0;
        }

        //[WebMethod]
        //public string MessageCom2(string RequestMessage)
        //{
        //    return "Function MessageCom2 got the request message of: " + RequestMessage;
        //}
    }
}

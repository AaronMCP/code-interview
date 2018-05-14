using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Common.HL7v2.Encoding
{
    public class HL7MessageTemplates
    {
        public const string SuccessResponse =
            @"MSH|^~\&|<%MSH-5%>|<%MSH-6%>|<%MSH-3%>|<%MSH-4%>|20091209175006||ACK|10|P|2.3" +
            "\r\n" + @"MSA|AA|<%MSH-10%>|";
        public const string ErrorResponse =
            @"MSH|^~\&|<%MSH-5%>|<%MSH-6%>|<%MSH-3%>|<%MSH-4%>|20091209175006||ACK|10|P|2.3" +
            "\r\n" + @"MSA|AE|<%MSH-10%>|Cannot process this HL7 message in current application configuraiton.|";
        public const string Q23QueryResult =
            @"MSH|^~\&|<%MSH-5%>|<%MSH-6%>|<%MSH-3%>|<%MSH-4%>|20050801073008-0600||RSP^K23^RSP_K23|1|P|2.5|" +
            "\r\n" + @"MSA|AA|<%MSH-10%>|" +
            "\r\n" + @"QAK|111069|OK|Q23^Get Corresponding IDs^HL70471|1|" +
            "\r\n" + @"QPD|Q23^Get Corresponding IDs^HL70471|111069|112234^^^METRO HOSPITAL|^^^WEST CLINIC~^^^SOUTH LAB|" +
            "\r\n" + @"PID|||56321A^^^WEST CLINIC~66532^^^SOUTH LAB||~^^^^^^S|";
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using Common.Consts;
using System.Globalization;

namespace CommonGlobalSettings
{
    public class BookingResponse
    {
        protected DateTime? _bookingBeginDt;
        protected DateTime? _bookingEndDt;

        [DataField("BookingBeginDt")]
        public DateTime? BookingBeginDt
        {
            get { return _bookingBeginDt; }
            set { _bookingBeginDt = value; }
        }

        [DataField("BookingEndDt")]
        public DateTime? BookingEndDt
        {
            get { return _bookingEndDt; }
            set { _bookingEndDt = value; }
        }

        public XElement SerializeXML()
        {
            XElement element = new XElement("BookingResponse",
                new XElement("BOOKINGBEGINDT", this.BookingBeginDt.HasValue ? this.BookingBeginDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
               new XElement("BOOKINGENDDT", this.BookingEndDt.HasValue ? this.BookingEndDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""));

            return element;
        }

        public void DeSerializeXML(XElement element)
        {
            if (element == null) return;

            var e = (from node in element.Descendants("BOOKINGBEGINDT")
                     select node).SingleOrDefault();
            if (string.IsNullOrEmpty(e.Value))
                this.BookingBeginDt = null;
            else
                this.BookingBeginDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = (from node in element.Descendants("BOOKINGENDDT")
                 select node).SingleOrDefault();
            if (string.IsNullOrEmpty(e.Value))
                this.BookingEndDt = null;
            else
                this.BookingEndDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

        }
    }
}

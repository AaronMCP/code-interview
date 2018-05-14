using System;
using System.Collections.Generic;
using System.Text;

namespace Server.DAO.Templates
{
    public interface IDBProvider : IReportTemplateDAO, IReportTemplateDirectoryDAO, IPrintTemplateDAO, IPhraseTemplatetDAO, IExaminTemplateDAO, IEmergencyTemplateDao, IBookingNoticeTemplateDAO
    {

    }
}

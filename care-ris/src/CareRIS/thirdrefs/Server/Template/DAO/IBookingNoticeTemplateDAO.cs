using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonGlobalSettings;

namespace Server.DAO.Templates
{
    public interface IBookingNoticeTemplateDAO
    {
        bool AddBookingNoticeTemplate(BookingNoticeModel model);
        bool ModifyBookingNoticeTemplate(BookingNoticeModel model);
        bool DeleteBookingNoticeTemplate(BookingNoticeModel model);
        DataSet GetAllBookingNoticeTemplate();
        DataSet GetOneBookingNoticeTemplate(BookingNoticeModel model);
        DataSet GetBookingNoticeTemplates(BookingNoticeModel model);
    }
}

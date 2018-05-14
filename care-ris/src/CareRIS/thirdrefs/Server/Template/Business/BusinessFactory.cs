using System;
using System.Collections.Generic;
using System.Text;
using Server.DAO.Templates;
using System.Collections;
using Server.Business.Templates.Impl;
namespace Server.Business.Templates
{
    public sealed class BusinessFactory
    {
        private static BusinessFactory instance = new BusinessFactory();
        private Hashtable flyWeightPool = new Hashtable();

        private BusinessFactory()
        {
            
        }

        public static BusinessFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public IEmergencyTemplateService GetEmergencyTemplateService()
        {
            IEmergencyTemplateService eyTemplateService = flyWeightPool["EmergencyTemplateService"] as IEmergencyTemplateService;

            if (eyTemplateService == null)
            {
                eyTemplateService = new EmergencyTemplateServiceImpl();
                flyWeightPool.Add("EmergencyTemplateService", eyTemplateService);
            }

            return eyTemplateService;
        }



        public IReportTemplateService GetReportTemplateService()
        {
            IReportTemplateService reportTemplateService = flyWeightPool["ReportTemplateService"] as IReportTemplateService;

            if (reportTemplateService == null)
            {
                reportTemplateService = new ReportTemplateServiceImpl();
                flyWeightPool.Add("ReportTemplateService", reportTemplateService);
            }

            return reportTemplateService;
        }
        public IReportTemplateDirectoryService GetReportTemplateDirectoryService()
        {
            IReportTemplateDirectoryService reportTemplateDirectoryService = flyWeightPool["ReportTemplateDirectoryService"] as IReportTemplateDirectoryService;

            if (reportTemplateDirectoryService == null)
            {
                reportTemplateDirectoryService = new ReportTemplateServiceImpl();
                flyWeightPool.Add("ReportTemplateDirectoryService", reportTemplateDirectoryService);
            }

            return reportTemplateDirectoryService;
        }
        public IPrintTemplateService GetPrintTemplateService()
        {
            IPrintTemplateService printTemplateService = flyWeightPool["PrintTemplateService"] as IPrintTemplateService;

            if (printTemplateService == null)
            {
                printTemplateService = new PrintTemplateServiceImpl();
                flyWeightPool.Add("PrintTemplateService", printTemplateService);
            }

            return printTemplateService;
        }

        public IPhraseTemplateService GetPhraseTemplateService()
        {
            IPhraseTemplateService phraseTemplateService = flyWeightPool["PhraseTemplateService"] as IPhraseTemplateService;

            if (phraseTemplateService == null)
            {
                phraseTemplateService = new PhraseTemplateServiceImpl();
                flyWeightPool.Add("PhraseTemplateService", phraseTemplateService);
            }

            return phraseTemplateService;
        }

        public IExaminTemplateService GetExaminTemplateService()
        {
            IExaminTemplateService examinTemplateService = flyWeightPool["ExaminTemplateService"] as IExaminTemplateService;

            if (examinTemplateService == null)
            {
                examinTemplateService = new ExaminTemplateServiceImpl();
                flyWeightPool.Add("ExaminTemplateService", examinTemplateService);
            }

            return examinTemplateService;
        }

        public IBookingNoticeTemplateService GetBookingNoticeTemplateService()
        {
            IBookingNoticeTemplateService bookingNoticeTemplateService = flyWeightPool["BookingNoticeTemplateService"] as IBookingNoticeTemplateService;

            if (bookingNoticeTemplateService == null)
            {
                bookingNoticeTemplateService = new BookingNoticeTemplateServiceImpl();
                flyWeightPool.Add("BookingNoticeTemplateService", bookingNoticeTemplateService);
            }

            return bookingNoticeTemplateService;
        }
       

    }
}

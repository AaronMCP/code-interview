using Hys.CareAgent.Upload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;

namespace Hys.CareAgent.Common
{
    [Serializable()]
    public class Dam
    {
        public string ID { get; set; }
        public string WebApiHostUrl { get; set; }
    }

    public class DamDetail
    {
        public string ID { get; set; }
        public string IP { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string WebApiurl { get; set; }
        public string WebApiHostUrl { get; set; }
    }

    public class DAMInfoDto
    {
        public string UniqueID { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public string WebApiUrl { get; set; }
        public string Description { get; set; }
        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }

    public class EMRFileItemDto
    {
        public string UniqueID { get; set; }
        public string ParentID { get; set; }
        public int ItemType { get; set; }
        public int FileType { get; set; }
        public Int64 FileSize { get; set; }
        public string DestFilePath { get; set; }
        public string DicomPrefix { get; set; }
        public int FileStatus { get; set; }
        public string FileName { get; set; }
        public string SrcFilePath { get; set; }
        public string SrcInfo { get; set; }
        public string CreatorID { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UploadedTime { get; set; }
        public int Progress { get; set; }
        public string Description { get; set; }
        public string ExtraConfigID { get; set; }
        public int Visible { get; set; }
        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }

    public class RegEMRFileItemDto
    {
        public string UniqueID { get; set; }
        public int ItemType { get; set; }
        public string DicomPrefix { get; set; }
        public string SrcFilePath { get; set; }
        public Int64 FileSize { get; set; }
        public string SrcInfo { get; set; }
        public string FileName { get; set; }
        public string CreatorID { get; set; }
        public string Description { get; set; }
        public string ParentID { get; set; }
    }

    public enum ItemType
    {
        File = 0,
        Folder = 1
    }

    public enum FileType
    {
        JPG = 0,
        PDF = 1,
        DOC = 2,
        TXT = 3,
        WAV = 4,
        AVI = 5,
        DICOM = 6,
        Unknown = 20
    }

    public enum UploadFileType
    {
        NonDICOM = 0,
        DICOM = 1
    }

    public class DCMDto
    {
        public StudyDto StudyDto { get; set; }
        public SeriesDto SeriesDto { get; set; }
        public ImageDto ImageDto { get; set; }
        public EMRFileItemDto EMRFileItemDto { get; set; }
    }

    public class UploadFileTaskWorkDto
    {
        public string UniqueID { get; set; }
        public int Status { get; set; }
        public string FileID { get; set; }
        public int FileType { get; set; }
        public string DicomPrefix { get; set; }
        public DateTime StartTime { get; set; }
        public int RetryNumber { get; set; }
        public Int64 TransferedSize { get; set; }
        public string SrcFilePath { get; set; }
        public string DestFilePath { get; set; }
    }

    public class UploadingProgressDto
    {
        public string UniqueID { get; set; }
        public Int64 TransferedSize { get; set; }
        public int Progress { get; set; }
    }

    public class UploadTask
    {
        public string UniqueID { get; set; }
        public UploadFileTaskWorkDto UploadFileTaskWorkDto { get; set; }
        public DamDetail Dam { get; set; }
    }

    public class ConfigurationDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public static class DAMCommon
    {
        private static List<DamDetail> _commonDamDetails = new List<DamDetail>();
        private static List<Dam> _commonDams = new List<Dam>();
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");

        public static List<DamDetail> CommonDamDetails
        {
            get
            {
                return _commonDamDetails;
            }
            set
            {
                _commonDamDetails = value;
            }
        }

        public static List<Dam> CommonDams
        {
            get
            {
                return _commonDams;
            }
            set
            {
                _commonDams = value;
            }
        }

        public static List<Dam> GetDamConfig()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "DamConfig.xml";
            XmlSerializer xmlser = new XmlSerializer(typeof(List<Dam>));
            using (StreamReader srdr = new StreamReader(filePath))
            {
                List<Dam> dams = (List<Dam>)xmlser.Deserialize(srdr);
                return dams;
            }
        }

        public static void SetDams(List<Dam> dams)
        {
            bool isChange = false;
            List<Dam> newDams = new List<Dam>();
            System.Threading.Monitor.Enter(DAMCommon.CommonDams);
            foreach (Dam dam in dams)
            {
                if (DAMCommon.CommonDams.Where(d => d.ID == dam.ID).FirstOrDefault() == null)
                {
                    DAMCommon.CommonDams.Add(dam);
                    newDams.Add(dam);
                    isChange = true;
                }
            }
            System.Threading.Monitor.Exit(DAMCommon.CommonDams);

            if (isChange)
            {
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "DamConfig.xml";
                XmlSerializer serialiser = new XmlSerializer(typeof(List<Dam>));
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    serialiser.Serialize(writer, DAMCommon.CommonDams);
                }
            }


            //add new
            foreach (Dam dam in newDams)
            {
                DAMCommon.SetDamDetail(dam);
            }
        }

        public static void SetDamDetail(Dam dam)
        {
            try
            {
                if (DAMCommon.CommonDamDetails.Where(d => d.ID == dam.ID).FirstOrDefault() == null)
                {
                    using (var client = new HttpClient())
                    {
                        var parameters = "/api/v1/consultation/configuration/userdambyid/" + dam.ID;
                        HttpResponseMessage response = client.GetAsync(dam.WebApiHostUrl + parameters).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsAsync<DAMInfoDto>().Result;
                            if (result != null)
                            {
                                //damwebapi
                                DamDetail damDetail = new DamDetail();
                                damDetail.ID = dam.ID;
                                damDetail.WebApiHostUrl = dam.WebApiHostUrl;
                                damDetail.WebApiurl = result.WebApiUrl;
                                var parameters2 = "/api/v1/configuration/configurationlist";
                                HttpResponseMessage response2 = client.GetAsync(result.WebApiUrl + parameters2).Result;
                                if (response2.IsSuccessStatusCode)
                                {

                                    var result2 = response2.Content.ReadAsAsync<List<ConfigurationDto>>().Result;
                                    if (result2 != null)
                                    {
                                        damDetail.IP = result2.Where(r => r.Name.Equals("FTPIP", StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Value;
                                        damDetail.User = result2.Where(r => r.Name.Equals("FTPUser", StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Value;
                                        damDetail.Port = result2.Where(r => r.Name.Equals("FtpPort", StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Value;
                                        damDetail.Password = result2.Where(r => r.Name.Equals("FTPPassword", StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Value;

                                        System.Threading.Monitor.Enter(DAMCommon.CommonDamDetails);
                                        if (DAMCommon.CommonDamDetails.Where(d => d.ID == dam.ID).FirstOrDefault() == null)
                                        {
                                            DAMCommon.CommonDamDetails.Add(damDetail);
                                        }
                                        System.Threading.Monitor.Exit(DAMCommon.CommonDamDetails);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("SetDamDetail error:" + ex.ToString());
            }

        }

    }
}

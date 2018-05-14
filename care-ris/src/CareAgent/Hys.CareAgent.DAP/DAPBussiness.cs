using kdt_managed;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hys.CareAgent.Common;
using Hys.CareAgent.DAP.Entity;
using DateTime = System.DateTime;

namespace Hys.CareAgent.DAP
{
    public class DAPBussiness
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");


        public DAPBussiness()
        {
            
        }

        /// <summary>
        /// Save DICOM data and file
        /// </summary>
        public bool SaveDICOMInfo(MElementList dataset, ts_t baseTsn, string implClassUid, string versionName,string rootPath)
        {
            var dicom = new DICOMInfoDto();
            DICOMUtility.BuildDICOMInfo(dataset, ref dicom);
            var path = rootPath+ dicom.AccessionNo + Convert.ToString(Path.DirectorySeparatorChar);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var filePath = path + dicom.SOPInstanceUID + ".dcm";
            var result1 = SaveDICOMFile(dataset, baseTsn, implClassUid, versionName, filePath);
            var result2 = false;
            if (result1)
            {
                 result2 = SaveDICOMData(dicom, filePath);
            }
           return result1&&result2;
        }

        /// <summary>
        /// Save DICOM data in SQLCompact
        /// </summary>
        public bool SaveDICOMData(DICOMInfoDto dicom,string filePath)
        {
            try
            {
                var study = new Study();
                var series = new Series();
                var image = new Image();
                // Study
                study.StudyInstanceUID = dicom.StudyInstanceUID;
                study.PatientID = dicom.PatientID;
                study.PatientName = dicom.PatientName;
                study.PatientDOB = dicom.PatientDOB;
                study.PatientAge = dicom.PatientAge;
                study.PatientSex = dicom.PatientSex;
                study.AccessionNo = dicom.AccessionNo;
                study.BodyPart = dicom.BodyPart;
                study.Modality = dicom.Modality;
                study.ExamCode = string.Empty;
                study.StudyDate = dicom.StudyDate;
                study.StudyTime = dicom.StudyTime;
                study.StudyDescription = dicom.StudyDescription;
                study.ReferPhysician = dicom.ReferPhysician;
                study.ReceiveTime = DateTime.Now.AddTicks(-(DateTime.Now.Ticks % TimeSpan.TicksPerSecond));
                // Series
                series.SeriesInstanceUID = dicom.SeriesInstanceUID;
                series.StudyInstanceUID = dicom.StudyInstanceUID;
                series.BodyPart = dicom.BodyPart;
                series.Modality = dicom.Modality;
                //Image
                image.SOPInstanceUID = dicom.SOPInstanceUID;
                image.SeriesInstanceUID = dicom.SeriesInstanceUID;
                image.FilePath = filePath;
                image.CreateTime = DateTime.Now;
                // Retrieve the ConnectionString from App.config 
                string connectString = ConfigurationManager.ConnectionStrings["DAPContext"].ToString();
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectString);
                // Retrieve the DataSource property.    
                string dbPath = builder.DataSource.Substring(0,builder.DataSource.LastIndexOf('\\'));
                if (!Directory.Exists(dbPath))
                {
                    Directory.CreateDirectory(dbPath);
                }
                using (var db = new DAPContext())
                {
                    var studyToUpdate = db.Studies.FirstOrDefault(s => s.StudyInstanceUID.Equals(study.StudyInstanceUID));
                    var seriesToUpdate = db.Series.FirstOrDefault(s => s.SeriesInstanceUID.Equals(series.SeriesInstanceUID));
                    var imageToUpdate = db.Images.FirstOrDefault(i => i.SOPInstanceUID.Equals(image.SOPInstanceUID));
                    if (studyToUpdate != null)
                    {
                        db.Entry(studyToUpdate).CurrentValues.SetValues(study);
                    }
                    else
                    {
                        db.Studies.Add(study);
                    }

                    if (seriesToUpdate != null)
                    {
                        db.Entry(seriesToUpdate).CurrentValues.SetValues(seriesToUpdate);
                    }
                    else
                    {
                        db.Series.Add(series);
                    }
                    if (imageToUpdate != null)
                    {
                        db.Entry(imageToUpdate).CurrentValues.SetValues(imageToUpdate);
                    }
                    else
                    {
                        db.Images.Add(image);
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return false;
            }
        }

        /// <summary>
        /// Search DICOM data for list in RisPro
        /// </summary>
        public SearchResult SearchDICOMData(string patientName, string patientId, string accessionNo, int pageSize, int pageIndex)
        {
            using (var db = new DAPContext())
            {
                var query = db.Studies.AsQueryable();
                var lastTime = db.Studies.Max(p => p.ReceiveTime);
                if (!string.IsNullOrEmpty(patientName))
                {
                    query = query.Where(p => p.PatientName.Contains(patientName));
                }
                if (!string.IsNullOrEmpty(patientId))
                {
                    query = query.Where(p => p.PatientID.Contains(patientId));
                }
                if (!string.IsNullOrEmpty(accessionNo))
                {
                    query = query.Where(p => p.AccessionNo.Contains(accessionNo));
                }
                int count = query.Count();
                var queryVal = query.OrderByDescending(p => p.ReceiveTime).ToList().Skip((pageIndex - 1) * pageSize).Take(pageSize);
                var path = ConfigurationManager.AppSettings["DICOMFilePath"];
                var result = new SearchResult()
                {
                    Result = queryVal.ToList(),
                    Count = count,
                    DICOMPath = path,
                    LastTime = lastTime.HasValue?lastTime.Value.ToString("yyyy-MM-dd HH:mm:ss"):string.Empty
                };
                return result;
            }
        }

        public bool CheckDICOMData(string time)
        {
            using (var db = new DAPContext())
            {
                DateTime timeVal;
                if (DateTime.TryParse(time, out timeVal))
                {
                    return db.Studies.Any(p => p.ReceiveTime.Value > timeVal);
                }
                else
                {
                    return db.Studies.Any();
                }
            }
        }

        /// <summary>
        /// Search DICOM Image file path list
        /// </summary>
        public List<string> SearchDICOMFile(string studyInstanceUid)
        {
            using (var db = new DAPContext())
            {
                var query = from s in db.Studies
                    join c in db.Series on s.StudyInstanceUID equals c.StudyInstanceUID
                    join m in db.Images on c.SeriesInstanceUID equals m.SeriesInstanceUID
                    where s.StudyInstanceUID == studyInstanceUid
                    orderby c.SeriesNo, m.ImageNo  
                    select m.FilePath;
                return query.ToList();
            }
        }

        /// <summary>
        /// Save DICOM file
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="baseTsn"></param>
        /// <param name="implClassUid"></param>
        /// <param name="versionName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool SaveDICOMFile(MElementList dataset, ts_t baseTsn, string implClassUid, string versionName,  string filePath)
        {
            try
            {
                // add the following group 2 element for writing pt10 files
                string str_tsn = "1.2.840.10008.1.2";
                switch (baseTsn)
                {
                    case ts_t.ImplicitVRLittleEndian: str_tsn = "1.2.840.10008.1.2"; break;
                    case ts_t.ExplicitVRBigEndian: str_tsn = "1.2.840.10008.1.2.2"; break;
                    case ts_t.ExplicitVRLittleEndian: str_tsn = "1.2.840.10008.1.2.1"; break;
                    case ts_t.JPEGLosslessNonHierarchical_14: str_tsn = "1.2.840.10008.1.2.4.57"; break;
                    case ts_t.JPEGLosslessNonHierarchicalFirstOrderPrediction: str_tsn = "1.2.840.10008.1.2.4.70"; break;
                    default: str_tsn = "1.2.840.10008.1.2"; break;
                }
                MElement tsnUid = new MElement(tag_t.kTransferSyntaxUID, vr_t.UI);
                UID tsn = new UID(str_tsn);
                tsnUid.set_uid(0, tsn);
                dataset.addElement(tsnUid);

                MElement implUid = new MElement(tag_t.kImplementationClassUID, vr_t.UI);
                UID uid = new UID(implClassUid);
                implUid.set_uid(0, uid);
                dataset.addElement(implUid);

                MElement implVer = new MElement(tag_t.kImplementationVersionName, vr_t.SH);
                string version = versionName;
                implVer.set_string(0, version);
                dataset.addElement(implVer);

                // Note: in the current version,saving file will use a large size of memory,this is a limitation
                // of the managed class in KDT,in the later version,if the limitation is resolved,we will use
                // the MDecoder to save files,not the MEncoder.wirte_pt10_file
                if (MEncoder.write_pt10_file
                    (
                    filePath,
                    dataset,
                    true, // item length is explicit
                    false // do not check group 2 items
                    ))
                {
                    _logger.Info("DAPStoreService.SaveFile(): " + "save file:" + filePath + "OK.");
                    return true;
                }
                else
                {
                    _logger.Info("DAPStoreService.SaveFile(): " + "save file:" + filePath + "failed.");
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.Info("DAPStoreService.SaveFile(): " + "error occurred while trying to write part-10 file " + e.Message);
                return false;
            }
        }

        public Utilities.StatusCode DeleteDICOM(string accessionNo,string studyInstanceuId)
        {
            //load xml, read dam
            if (!RegisterFileTask.LoadDamInfo())
            {
                DeleteDICOMFile(accessionNo);
                   return Utilities.StatusCode.Success;
            }
            try
            {
                    //get folder or dcm, unreg
                    using (var client = new HttpClient())
                    {
                        var parameters = String.Format("/api/v1/registration/isprocessingdicom/{0}", studyInstanceuId);
                        HttpResponseMessage response = client.GetAsync(DAMCommon.CommonDamDetails.FirstOrDefault().WebApiurl + parameters).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            bool result = false;
                            bool.TryParse(response.Content.ReadAsStringAsync().Result, out result);
                            if (!result)
                            {
                                DeleteDICOMFile(accessionNo);
                                return Utilities.StatusCode.Success;
                            }
                        }
                         return Utilities.StatusCode.AccessDenied;
                    }
            }
            catch (Exception ex)
            {
                _logger.Error("Delete DICOM File error:" + ex.ToString());
                return Utilities.StatusCode.Failed;
            }
        }

        private void DeleteDICOMFile(string accessionNo)
        {
            var path = Path.Combine(ConfigurationManager.AppSettings["DICOMFilePath"], accessionNo);
            DleteFile(path);
            using (var db = new DAPContext())
            {
                var existStudy = db.Studies.FirstOrDefault(p => p.AccessionNo.Equals(accessionNo));
                if (existStudy != null)
                {
                    db.Studies.Remove(existStudy);
                    var existSeries = db.Series.Where(p => p.StudyInstanceUID.Equals(existStudy.StudyInstanceUID));
                    db.Series.RemoveRange(existSeries);
                    var seriesUids = existSeries.Select(p => p.SeriesInstanceUID);
                    var existImages = db.Images.Where(p => seriesUids.Contains(p.SeriesInstanceUID));
                    db.Images.RemoveRange(existImages);
                    db.SaveChanges();
                }
            }
        }

        private void DleteFile(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo downloadedMessageInfo = new DirectoryInfo(path);
                foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                {
                    dir.Delete(true);
                }
                Directory.Delete(path);
            }
        }
    }
}

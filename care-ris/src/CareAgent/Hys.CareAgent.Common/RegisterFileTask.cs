using Hys.CareAgent.Upload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Hys.CareAgent.Common
{
    public static class RegisterFileTask
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");

        public static void RegisterERMFile()
        {
            //load xml, read dam
            if (!LoadDamInfo())
            {
                return;
            }

            string scrInfo = Utilities.GetProcessID();
            _logger.Debug("scrInfo:" + scrInfo);
            try
            {
                foreach (DamDetail dam in DAMCommon.CommonDamDetails)
                {
                    //get folder or dcm, unreg
                    using (var client = new HttpClient())
                    {
                        var parameters = String.Format("/api/v1/registration/unregitems/{0}", scrInfo);
                        HttpResponseMessage response = client.GetAsync(dam.WebApiurl + parameters).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsAsync<List<EMRFileItemDto>>().Result;
                            RegisterEMRItems(dam, client, result);
                        }

                        SetItemSize(scrInfo, dam, client, ref parameters, ref response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("RegisterERMFile error:" + ex.ToString());
            }
        }

        private static void SetItemSize(string scrInfo, DamDetail dam, HttpClient client, ref string parameters, ref HttpResponseMessage response)
        {
            List<RegEMRFileItemDto> regEMRFileItemNoSizeList = new List<RegEMRFileItemDto>();
            parameters = String.Format("/api/v1/registration/nosizeitems/{0}", scrInfo);
            response = client.GetAsync(dam.WebApiurl + parameters).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<List<EMRFileItemDto>>().Result;
                foreach (EMRFileItemDto emrFileItemDto in result)
                {
                    if (File.Exists(emrFileItemDto.SrcFilePath))
                    {
                        try
                        {
                            RegEMRFileItemDto regEMRFileItemDto = new Common.RegEMRFileItemDto();
                            regEMRFileItemDto.UniqueID = emrFileItemDto.UniqueID;
                            FileInfo fileInfo = new FileInfo(emrFileItemDto.SrcFilePath);
                            regEMRFileItemDto.FileSize = fileInfo.Length;
                            regEMRFileItemNoSizeList.Add(regEMRFileItemDto);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }

            if (regEMRFileItemNoSizeList.Count > 0)
            {
                HttpResponseMessage response3 = client.PostAsync<List<RegEMRFileItemDto>>(dam.WebApiurl + "/api/v1/registration/updateditemsize", regEMRFileItemNoSizeList, new JsonMediaTypeFormatter()).Result;
            }
        }

        private static void RegisterEMRItems(DamDetail dam, HttpClient client, List<EMRFileItemDto> result)
        {
            foreach (EMRFileItemDto emrFileItemDto in result)
            {
                if (emrFileItemDto.SrcFilePath != "")
                {
                    //folder
                    if (emrFileItemDto.ItemType == (int)ItemType.Folder)
                    {
                        //get all files
                        if (!RegisterFolder(dam, client, emrFileItemDto))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        //dcm file
                        List<string> files = new List<string> { emrFileItemDto.SrcFilePath };
                        RegisterDCMEMRFileItems(emrFileItemDto, files, dam);
                    }
                }
            }
        }

        private static bool RegisterFolder(DamDetail dam, HttpClient client, EMRFileItemDto emrFileItemDto)
        {
            string[] filePaths = GetFiles(emrFileItemDto);
            if (filePaths == null || filePaths.Length == 0)
            {
                var parametersFail = String.Format("/api/v1/registration/setstatus?id={0}&status={1}&description={2}", emrFileItemDto.UniqueID, 10, "SendFTPError004");
                HttpResponseMessage responseFail = client.GetAsync(dam.WebApiurl + parametersFail).Result;
                return false;
            }

            List<RegEMRFileItemDto> regEMRFileItemList = new List<RegEMRFileItemDto>();
            List<string> files = new List<string>();
            foreach (string file in filePaths)
            {
                if (!ProcessEMRItemByType(emrFileItemDto, regEMRFileItemList, files, file))
                {
                    return false;
                }
            }

            //Non dcm
            if (regEMRFileItemList.Count > 0)
            {
                HttpResponseMessage response3 = client.PostAsync<List<RegEMRFileItemDto>>(dam.WebApiurl + "/api/v1/registration/newitemlistfolder", regEMRFileItemList, new JsonMediaTypeFormatter()).Result;
            }

            //dcm
            if (files.Count > 0)
            {
                RegisterDCMEMRFileItems(emrFileItemDto, files, dam);
            }

            return true;
        }

        private static bool ProcessEMRItemByType(EMRFileItemDto emrFileItemDto, List<RegEMRFileItemDto> regEMRFileItemList, List<string> files, string file)
        {
            int type = GetFileType(file);
            //non dcm
            if (type != (int)FileType.Unknown && type != (int)FileType.DICOM)
            {
                RegEMRFileItemDto regEMRFileItemDto = new RegEMRFileItemDto();
                regEMRFileItemDto.UniqueID = Guid.NewGuid().ToString();
                regEMRFileItemDto.ParentID = emrFileItemDto.ParentID;
                regEMRFileItemDto.DicomPrefix = emrFileItemDto.DicomPrefix;
                regEMRFileItemDto.CreatorID = emrFileItemDto.CreatorID;
                regEMRFileItemDto.Description = emrFileItemDto.Description;
                regEMRFileItemDto.ItemType = 0;
                regEMRFileItemDto.SrcFilePath = file;
                regEMRFileItemDto.SrcInfo = emrFileItemDto.SrcInfo;
                try
                {
                FileInfo fileInfo = new FileInfo(file);
                regEMRFileItemDto.FileSize = fileInfo.Length;
                }
                catch
                {
                    return false;
                }

                regEMRFileItemList.Add(regEMRFileItemDto);
            }
            else
            {
                files.Add(file);
            }

            return true;
        }

        private static string[] GetFiles(EMRFileItemDto emrFileItemDto)
        {
            string[] filePaths = null;
            try
            {
                filePaths = Directory.GetFiles(emrFileItemDto.SrcFilePath, "*.*",
                     SearchOption.AllDirectories);
            }
            catch (Exception ex)
            {
                _logger.Error("RegisterERMFile error:" + ex.ToString());
            }

            return filePaths;
        }

        public static bool LoadDamInfo()
        {
            List<Dam> dams = DAMCommon.GetDamConfig();
            foreach (Dam dam in dams)
            {
                DAMCommon.SetDamDetail(dam);
            }

            if (DAMCommon.CommonDamDetails.Count == 0)
            {
                return false;
            }

            return true;
        }

        private static void RegisterDCMEMRFileItems(EMRFileItemDto emrFileItemDto, List<string> files, DamDetail dam)
        {
            List<DCMInfo> dcmInfoList = new List<DCMInfo>();
            try
            {
                DCMParser.GetDcmInfo(files, out dcmInfoList);
            }
            catch (Exception ex)
            {
                _logger.Error("DCMParser error:" + ex.ToString());
            }
            if (dcmInfoList.Count > 0)
            {
                //dcm list
                List<DCMDto> dcmList = new List<DCMDto>();
                foreach (DCMInfo dcmInfo in dcmInfoList)
                {
                    DCMDto dcmDto = new DCMDto();
                    dcmDto.StudyDto = dcmInfo.StudyDto;
                    dcmDto.StudyDto.AccessionNo = emrFileItemDto.DicomPrefix + dcmDto.StudyDto.AccessionNo;
                    dcmDto.StudyDto.PatientID = emrFileItemDto.DicomPrefix + dcmDto.StudyDto.PatientID;

                    dcmDto.SeriesDto = dcmInfo.SeriesDto;

                    dcmDto.ImageDto = dcmInfo.ImageDto;
                    dcmDto.ImageDto.ExtraConfigID = emrFileItemDto.ExtraConfigID;
                    dcmDto.ImageDto.SrcInfo = emrFileItemDto.SrcInfo;
                    dcmDto.ImageDto.CreatorID = emrFileItemDto.CreatorID;
                    dcmDto.ImageDto.CreateTime = emrFileItemDto.CreateTime;
                    dcmDto.ImageDto.ObjectFile = System.DateTime.Now.ToString("yyyy-MM-dd") + "\\" + dcmDto.StudyDto.AccessionNo + "\\" + dcmDto.ImageDto.UniqueID + ".dcm";
                    dcmDto.EMRFileItemDto = CreateDCMEMRFileItem(emrFileItemDto, dcmInfo.StudyDto);
                    dcmList.Add(dcmDto);
                }
                if (dcmList.Count > 0)
                {
                    try
                    {
                        using (var client = new HttpClient())
                        {
                            var parameters = "/api/v1/registration/dcmitems";
                            HttpResponseMessage response = client.PostAsync<List<DCMDto>>(dam.WebApiurl + parameters, dcmList, new JsonMediaTypeFormatter()).Result;
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.Error("dcmitems error:" + ex.ToString());
                    }
                }
            }
        }


        private static EMRFileItemDto CreateDCMEMRFileItem(EMRFileItemDto emrFileItemDto, StudyDto studyDto)
        {
            EMRFileItemDto newItemDto = new EMRFileItemDto();
            newItemDto.UniqueID = Guid.NewGuid().ToString();
            newItemDto.ParentID = emrFileItemDto.ParentID;
            newItemDto.ItemType = 0;
            newItemDto.FileType = (int)FileType.DICOM;
            newItemDto.FileSize = 0;
            newItemDto.DestFilePath = studyDto.UniqueID;
            newItemDto.DicomPrefix = emrFileItemDto.DicomPrefix;
            newItemDto.FileStatus = 1;
            newItemDto.FileName = studyDto.AccessionNo;
            newItemDto.SrcFilePath = emrFileItemDto.SrcFilePath;
            newItemDto.SrcInfo = emrFileItemDto.SrcInfo;
            newItemDto.CreatorID = emrFileItemDto.CreatorID;
            newItemDto.CreateTime = emrFileItemDto.CreateTime;
            newItemDto.UploadedTime = emrFileItemDto.UploadedTime;
            newItemDto.Progress = emrFileItemDto.Progress;
            newItemDto.Description = emrFileItemDto.Description;
            newItemDto.ExtraConfigID = emrFileItemDto.ExtraConfigID;
            newItemDto.Visible = emrFileItemDto.Visible;
            newItemDto.LastEditUser = emrFileItemDto.LastEditUser;
            newItemDto.LastEditTime = emrFileItemDto.LastEditTime;

            return newItemDto;
        }

        private static int GetFileType(string srcFilePath)
        {
            FileType typeRet;
            String strSuffix = srcFilePath.Substring(srcFilePath.LastIndexOf(".") + 1);
            switch (strSuffix.ToUpper())
            {
                case "TXT":
                    typeRet = FileType.TXT;
                    break;
                case "DOC":
                case "DOCX":
                    typeRet = FileType.DOC;
                    break;
                case "PDF":
                    typeRet = FileType.PDF;
                    break;
                case "MP3":
                case "WAV":
                    typeRet = FileType.WAV;
                    break;
                case "AVI":
                    typeRet = FileType.AVI;
                    break;
                case "JPEG":
                case "JPG":
                case "GIF":
                case "PNG":
                case "BMP":
                    typeRet = FileType.JPG;
                    break;
                case "DCM":
                    typeRet = FileType.DICOM;
                    break;

                default:
                    typeRet = FileType.Unknown;
                    break;
            }
            return (int)typeRet;
        }
    }
}

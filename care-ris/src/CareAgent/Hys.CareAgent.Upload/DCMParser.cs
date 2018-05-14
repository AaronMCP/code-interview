using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Collections;
using kdt_managed;
using System.Configuration;
using System.Text.RegularExpressions;


namespace Hys.CareAgent.Upload
{

    public static class DCMParser
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");

        public static bool ModifyAccNoAndPatientID(string filePath, string tempFilePath, string prefix, out string message)
        {
            Boolean ret = false;
            message = "";
            //String strAccPrefix = @"-" + m_infoDAP.HospitalID.ToString();
            String strAccPrefix = prefix;
            try
            {
                MElementList elmlist = new MElementList();
                if (!MDecoder.read_pt10_file(filePath, ref elmlist, null, -1))
                {
                    _logger.Error("ImageSubmitService.ModifyAccNoAndPatientID(): " + "read dicom file " + filePath + " error.");
                    message = "SendFTPError002";
                    return ret;
                }

                //Modify AccessionNumber
                String accNoOrg = "";
                String patIDOrg = "";
                GetDICOMInfo(elmlist, ref accNoOrg, ref patIDOrg);

                #region process Num
                //PID，AccNo为空时拒绝接受该图像
                if (patIDOrg.Length == 0 || accNoOrg.Length == 0)
                {
                    IDEmptyProcess(patIDOrg);

                    elmlist.Dispose();
                    message = "SendFTPError003";
                    return false;
                }

                #endregion

                int PrefixLength = 32 - strAccPrefix.Length;
                if (accNoOrg.Length > PrefixLength)
                    accNoOrg = accNoOrg.Remove(PrefixLength);

                patIDOrg = AddPrefix(strAccPrefix, elmlist, accNoOrg, patIDOrg, PrefixLength);

                //judge TransferSyntax
                SetTSN(elmlist);

                if (MEncoder.write_pt10_file
                      (
                          tempFilePath,
                          elmlist,
                          true, // item length is explicit
                          false // do not check group 2 items
                      ))
                {
                    elmlist.Dispose();
                    _logger.Debug("ImageSubmitService.ModifyAccNoAndPatientID(): " + "save dicom file " + tempFilePath + " ok.");
                    return true;
                }
                else
                {
                    elmlist.Dispose();
                    _logger.Error("ImageSubmitService.ModifyAccNoAndPatientID(): " + "save dicom file " + tempFilePath + " error.");
                    message = "SendFTPError002";
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.Error("ImageSubmitService.ModifyAccNoAndPatientID(): " + "pop up an exception--" + e.Message);
            }
            return ret;
        }

        private static void IDEmptyProcess(String patIDOrg)
        {
            if (patIDOrg.Length == 0)
            {
                _logger.Error("PatientID is empty");
            }
            else
            {
                _logger.Error("ACCNO is empty");
            }
        }

        private static string AddPrefix(String strAccPrefix, MElementList elmlist, String accNoOrg, String patIDOrg, int PrefixLength)
        {
            AddPrefixForAccNo(strAccPrefix, elmlist, accNoOrg);
            //Modify PatientID
            if (patIDOrg.Length > PrefixLength)
                patIDOrg = patIDOrg.Remove(PrefixLength);
            using (MElementRef element = elmlist.get_Element(tag_t.kPatientID))
            {
                string patIDNew = strAccPrefix + patIDOrg;

                //delete special char
                string patIDNew_temp = "";
                Regex re = new Regex(@"[a-zA-Z0-9-_]+", RegexOptions.None);
                MatchCollection collection = re.Matches(patIDNew);
                foreach (Match ma in collection)
                {
                    patIDNew_temp += ma.ToString();
                }
                patIDNew = patIDNew_temp;

                if (element == null || (element != null && element.value_count <= 0))
                {
                    MElement ele = new MElement(tag_t.kPatientID, vr_t.LO);
                    ele.set_string(0, patIDNew);
                    elmlist.addElement(ele);
                }
                else
                    element.set_string(0, patIDNew);
            }
            return patIDOrg;
        }

        private static void AddPrefixForAccNo(String strAccPrefix, MElementList elmlist, String accNoOrg)
        {
            using (MElementRef element = elmlist.get_Element(tag_t.kAccessionNumber))
            {
                string accNoNew = strAccPrefix + accNoOrg;

                //delete special char
                string accNoNew_temp = "";
                Regex re = new Regex(@"[a-zA-Z0-9-_]+", RegexOptions.None);
                MatchCollection collection = re.Matches(accNoNew);
                foreach (Match ma in collection)
                {
                    accNoNew_temp += ma.ToString();
                }
                accNoNew = accNoNew_temp;

                if (element == null || (element != null && element.value_count <= 0))
                {
                    MElement ele = new MElement(tag_t.kAccessionNumber, vr_t.LO);
                    ele.set_string(0, accNoNew);
                    elmlist.addElement(ele);
                }
                else
                    element.set_string(0, accNoNew);
            }
        }

        private static void SetTSN(MElementList elmlist)
        {
            string str_tsn = "";
            using (MElementRef element = elmlist.get_Element(tag_t.kTransferSyntaxUID))
            {
                if (element != null && element.value_count > 0)
                {
                    str_tsn = element.get_string(0);
                    if (str_tsn == "")
                    {
                        str_tsn = "1.2.840.10008.1.2";
                        kdt_managed.UID tsn = new kdt_managed.UID(str_tsn);
                        element.set_uid(0, tsn);
                    }
                }

            }

            if (str_tsn == "")
            {
                MElement tsnUid = new MElement(tag_t.kTransferSyntaxUID, vr_t.UI);
                kdt_managed.UID tsn = new kdt_managed.UID("1.2.840.10008.1.2");
                tsnUid.set_uid(0, tsn);
                elmlist.addElement(tsnUid);
            }
        }

        private static void GetDICOMInfo(MElementList elmlist, ref String accNoOrg, ref String patIDOrg)
        {
            using (MElementRef element = elmlist.get_Element(tag_t.kAccessionNumber))
            {
                if (element != null && element.value_count > 0)
                    accNoOrg = element.get_string(0);
            }
            using (MElementRef element = elmlist.get_Element(tag_t.kPatientID))
            {
                if (element != null && element.value_count > 0)
                    patIDOrg = element.get_string(0);
            }
            
        }


        public static void GetDcmInfo(List<string> PrcFileList, out List<DCMInfo> dcmInfoList)
        {
            dcmInfoList = new List<DCMInfo>();
            try
            {
                foreach (String prcFilePath in PrcFileList)
                {
                    FileInfo file = new FileInfo(prcFilePath);

                    MElementList elmlist = new MElementList();
                    using (DicomHeader dicomHeader = new DicomHeader())
                    {
                        if (!MDecoder.read_pt10_file(prcFilePath, ref elmlist, null, -1))
                        {
                            _logger.Error("Archiver.ProcessDcmFile(): " + "read dicom file " + prcFilePath + " error.");
                            continue;
                        }
                        bool bRet = dicomHeader.BuildDicomInfo(elmlist);
                        elmlist.Dispose();
                        if (bRet)
                        {
                            DCMInfo dcmInfo = dicomHeader.GetDCMInfo();
                            dcmInfo.ImageDto.SrcFilePath = prcFilePath;
                            dcmInfo.ImageDto.FileSize = file.Length;
                            dcmInfoList.Add(dcmInfo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Archiver.ProcessDcmFile(): " + "archive file failed " +
                    ex.Message);
            }
        }
    }

}

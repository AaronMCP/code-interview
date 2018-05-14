using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Dicom;
using HYS.Common.Dicom.Net;
using HYS.Common.Objects.Rule;
using HYS.Common.Soap;

namespace HYS.DicomAdapter.StorageServer.Objects
{
    public class StorageServerConfig : XObject
    {
        private bool _dumpDicomData;
        public bool DumpDicomData
        {
            get { return _dumpDicomData; }
            set { _dumpDicomData = value; }
        }

        private bool _saveFileToDirectory;
        public bool SaveFileToDirectory
        {
            get { return _saveFileToDirectory; }
            set { _saveFileToDirectory = value; }
        }

        private string _fileInboxDirectory = "";
        public string FileInboxDirectory
        {
            get { return _fileInboxDirectory; }
            set { _fileInboxDirectory = value; }
        }

        private string _gwDataDBConnection = "";
        public string GWDataDBConnection
        {
            get { return _gwDataDBConnection; }
            set { _gwDataDBConnection = value; }
        }

        private SCPConfig _scpConfig = new SCPConfig();
        public SCPConfig SCPConfig
        {
            get { return _scpConfig; }
            set { _scpConfig = value; }
        }

        private bool _SOAPEnable = false;
        public bool SOAPEnable
        {
            get { return _SOAPEnable; }
            set { _SOAPEnable = value; }
        }

        private SOAPClientExConfig _SOAPClientSetting = new SOAPClientExConfig("http://localhost:2489/Service/Service.asmx?wsdl" ,"http://www.HaoYiShenghealth.com/MessageCom" , true, true);
        public SOAPClientExConfig SOAPClientSetting
        {
            get { return _SOAPClientSetting; }
            set { _SOAPClientSetting = value; }
        }

        private string _XSLTFileToTransformSOAPtoDICOM = "XSLT\\StdResponseToDicom.xsl";
        public string XSLTFileToTransformSOAPtoDICOM
        {
            get { return _XSLTFileToTransformSOAPtoDICOM; }
            set { _XSLTFileToTransformSOAPtoDICOM = value; }
        }

        private string _XSLTFileToTransformDICOMtoSOAP = "XSLT\\DicomToStdRequest";
        public string XSLTFileToTransformDICOMtoSOAP
        {
            get { return _XSLTFileToTransformDICOMtoSOAP; }
            set { _XSLTFileToTransformDICOMtoSOAP = value; }
        }

        private XCollection<StorageServiceUID> _storageServiceUIDs = new XCollection<StorageServiceUID>();
        public XCollection<StorageServiceUID> StorageServiceUIDs
        {
            get { return _storageServiceUIDs; }
            set { _storageServiceUIDs = value; }
        }

        private InboundRule<QueryCriteriaItem, StorageItem> _storageRule = new InboundRule<QueryCriteriaItem, StorageItem>();
        public InboundRule<QueryCriteriaItem, StorageItem> StorageRule
        {
            get { return _storageRule; }
            set { _storageRule = value; }
        }

        private XCollection<PrivateTag> _privateTagList = new XCollection<PrivateTag>();
        public XCollection<PrivateTag> PrivateTagList
        {
            get { return _privateTagList; }
            set { _privateTagList = value; }
        }
        public void UpdatePrivateTagList()
        {
            Dictionary<string, PrivateTag> dic = new Dictionary<string, PrivateTag>();

            foreach (StorageItem qcItem in StorageRule.QueryResult.MappingList)
            {
                if (qcItem.DPath.Type != DPathType.Normal) continue;
                int tag = qcItem.DPath.GetTag();
                if (PrivateTagHelper.IsPrivateTag(tag))
                {
                    string strTag = DHelper.Int2HexString(tag);
                    if (!dic.ContainsKey(strTag))
                        dic.Add(strTag, new PrivateTag(strTag, qcItem.DPath.VR));
                }
            }

            // do not keep manually added item, refresh the whole private tag list

            //if (PrivateTagList != null)
            //{
            //    foreach (PrivateTag tag in PrivateTagList)
            //    {
            //        string strTag = tag.Tag;
            //        if (!dic.ContainsKey(strTag))
            //        {
            //            dic.Add(strTag, tag);
            //        }
            //    }
            //}

            XCollection<PrivateTag> xlist = new XCollection<PrivateTag>();
            foreach (KeyValuePair<string, PrivateTag> p in dic) xlist.Add(p.Value);
            PrivateTagList = xlist;
        }
    }
}

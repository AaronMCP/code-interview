using HYS.Common.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HYS.Common.Dicom.Net
{
    public class StorageServiceUID : XObject
    {
        private bool _enable;
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }
        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _uid = "";
        public string UID
        {
            get { return _uid; }
            set { _uid = value; }
        }

        public StorageServiceUID()
        {
        }
        internal StorageServiceUID(string name, string uid)
        {
            _uid = uid;
            _name = name;
        }

        public override string ToString()
        {
            return Name + "(" + UID + ")";
        }

        private static StorageServiceUID[] _uidList;
        private static StorageServiceUID[] _GetStorageServiceUIDs2()
        {
            List<StorageServiceUID> uidlist = new List<StorageServiceUID>();
            uidlist.Add(new StorageServiceUID("CRImage", "1.2.840.10008.5.1.4.1.1.1"));
            uidlist.Add(new StorageServiceUID("CTImage", "1.2.840.10008.5.1.4.1.1.2"));
            uidlist.Add(new StorageServiceUID("Curve", "1.2.840.10008.5.1.4.1.1.9"));
            uidlist.Add(new StorageServiceUID("DXProcessing", "1.2.840.10008.5.1.4.1.1.1.1.1"));
            uidlist.Add(new StorageServiceUID("EnhancedCTImage", "1.2.840.10008.5.1.4.1.1.2.1"));
            uidlist.Add(new StorageServiceUID("EnhancedMRImage", "1.2.840.10008.5.1.4.1.1.4.1"));
            uidlist.Add(new StorageServiceUID("GrayscalePresentationState", "1.2.840.10008.5.1.4.1.1.11.1"));
            uidlist.Add(new StorageServiceUID("HCColor", "1.2.840.10008.5.1.1.30"));
            uidlist.Add(new StorageServiceUID("HCGrayscale", "1.2.840.10008.5.1.1.29"));
            uidlist.Add(new StorageServiceUID("IOPresentation", "1.2.840.10008.5.1.4.1.1.1.3"));
            uidlist.Add(new StorageServiceUID("IOProcessing", "1.2.840.10008.5.1.4.1.1.1.3.1"));
            uidlist.Add(new StorageServiceUID("MGPresentation", "1.2.840.10008.5.1.4.1.1.1.2"));
            uidlist.Add(new StorageServiceUID("MGProcessing", "1.2.840.10008.5.1.4.1.1.1.2.1"));
            uidlist.Add(new StorageServiceUID("ModLUT", "1.2.840.10008.5.1.4.1.1.10"));
            uidlist.Add(new StorageServiceUID("MRImage", "1.2.840.10008.5.1.4.1.1.4"));
            uidlist.Add(new StorageServiceUID("NM93Image", "1.2.840.10008.5.1.4.1.1.5"));
            uidlist.Add(new StorageServiceUID("NMImage", "1.2.840.10008.5.1.4.1.1.20"));
            uidlist.Add(new StorageServiceUID("OPImage16", "1.2.840.10008.5.1.4.1.1.77.1.5.2"));
            uidlist.Add(new StorageServiceUID("OPImage8", "1.2.840.10008.5.1.4.1.1.77.1.5.1"));
            uidlist.Add(new StorageServiceUID("Overlay", "1.2.840.10008.5.1.4.1.1.8"));
            uidlist.Add(new StorageServiceUID("RTBeamsTreatment", "1.2.840.10008.5.1.4.1.1.481.4"));
            uidlist.Add(new StorageServiceUID("RTBrachyTreatment", "1.2.840.10008.5.1.4.1.1.481.6"));
            uidlist.Add(new StorageServiceUID("RTDose", "1.2.840.10008.5.1.4.1.1.481.2"));
            uidlist.Add(new StorageServiceUID("RTImage", "1.2.840.10008.5.1.4.1.1.481.1"));
            uidlist.Add(new StorageServiceUID("RTPlan", "1.2.840.10008.5.1.4.1.1.481.5"));
            uidlist.Add(new StorageServiceUID("RTStructureSet", "1.2.840.10008.5.1.4.1.1.481.3"));
            uidlist.Add(new StorageServiceUID("RTTreatmentSummary", "1.2.840.10008.5.1.4.1.1.481.7"));
            uidlist.Add(new StorageServiceUID("SCImage", "1.2.840.10008.5.1.4.1.1.7"));
            uidlist.Add(new StorageServiceUID("SCMFGB", "1.2.840.10008.5.1.4.1.1.7.2"));
            uidlist.Add(new StorageServiceUID("SCMFGW", "1.2.840.10008.5.1.4.1.1.7.3"));
            uidlist.Add(new StorageServiceUID("SCMFSB", "1.2.840.10008.5.1.4.1.1.7.1"));
            uidlist.Add(new StorageServiceUID("SCMFTC", "1.2.840.10008.5.1.4.1.1.7.4"));
            uidlist.Add(new StorageServiceUID("SRChestCAD", "1.2.840.10008.5.1.4.1.1.88.65"));
            uidlist.Add(new StorageServiceUID("SRComprehensive", "1.2.840.10008.5.1.4.1.1.88.33"));
            uidlist.Add(new StorageServiceUID("SREnhanced", "1.2.840.10008.5.1.4.1.1.88.22"));
            uidlist.Add(new StorageServiceUID("SRKeyObject", "1.2.840.10008.5.1.4.1.1.88.59"));
            uidlist.Add(new StorageServiceUID("SRMammoCAD", "1.2.840.10008.5.1.4.1.1.88.50"));
            uidlist.Add(new StorageServiceUID("SRText", "1.2.840.10008.5.1.4.1.1.88.11"));
            uidlist.Add(new StorageServiceUID("StoredPrint", "1.2.840.10008.5.1.1.27"));
            uidlist.Add(new StorageServiceUID("StudyContent", "1.2.840.10008.1.9"));
            uidlist.Add(new StorageServiceUID("US93Image", "1.2.840.10008.5.1.4.1.1.6"));
            uidlist.Add(new StorageServiceUID("USImage", "1.2.840.10008.5.1.4.1.1.6.1"));
            uidlist.Add(new StorageServiceUID("USMF93Image", "1.2.840.10008.5.1.4.1.1.3"));
            uidlist.Add(new StorageServiceUID("USMFImage", "1.2.840.10008.5.1.4.1.1.3.1"));
            uidlist.Add(new StorageServiceUID("VOILUT", "1.2.840.10008.5.1.4.1.1.11"));
            uidlist.Add(new StorageServiceUID("XABIImage", "1.2.840.10008.5.1.4.1.1.12.3"));
            uidlist.Add(new StorageServiceUID("XAImage", "1.2.840.10008.5.1.4.1.1.12.1"));
            uidlist.Add(new StorageServiceUID("XRFImage", "1.2.840.10008.5.1.4.1.1.12.2"));
            uidlist.Add(new StorageServiceUID("PETImage", "1.2.840.10008.5.1.4.1.1.128"));

            return uidlist.ToArray();
        }

        public static StorageServiceUID[] GetStorageServiceUIDs()
        {
            if (_uidList == null) _uidList = _GetStorageServiceUIDs2();
            return _uidList;
        }
        public static StorageServiceUID GetStorageServiceUID(string uid)
        {
            return GetStorageServiceUID("", uid);
        }
        public static StorageServiceUID GetStorageServiceUID(string name, string uid)
        {
            if (uid == null || uid.Length < 1) return null;
            StorageServiceUID[] list = GetStorageServiceUIDs();
            foreach (StorageServiceUID u in list) if (u.UID == uid) return u;
            return new StorageServiceUID(name, uid);
        }

    }
}

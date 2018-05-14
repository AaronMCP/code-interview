using HYS.Common.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYS.Common.Dicom.Net
{
    public class SCUConfig : XObject, IConfig
    {
        public SCUConfig()
        {
        }

        //private string _implementationClassUID = "1.2.3.10080";
        private string _implementationClassUID = "1.2.840.113564.3.1.64";
        public string ImplementationClassUID
        {
            get { return _implementationClassUID; }
            set { _implementationClassUID = value; }
        }

        private string _implementationVersion = "2.0";
        public string ImplementationVersion
        {
            get { return _implementationVersion; }
            set { _implementationVersion = value; }
        }

        private int _maxPduLength = 128;    //kb
        public int MaxPduLength
        {
            get { return _maxPduLength; }
            set { _maxPduLength = value; }
        }

        private int _associationTimeOut = 30000;        //ms    30s    0.5min
        public int AssociationTimeOut
        {
            get { return _associationTimeOut; }
            set { _associationTimeOut = value; }
        }

        private int _sessionTimeOut = 60000;          //ms    60s    1min
        public int SessionTimeOut
        {
            get { return _sessionTimeOut; }
            set { _sessionTimeOut = value; }
        }

        private string _aeTitle = "EKC_SCU";
        public string AETitle
        {
            get { return _aeTitle; }
            set { _aeTitle = value; }
        }

        private bool _implicitVRLittleEndian = true;
        public bool ImplicitVRLittleEndian
        {
            get { return _implicitVRLittleEndian; }
            set { _implicitVRLittleEndian = value; }
        }

        private bool _explicitVRLittleEndian = true;
        public bool ExplicitVRLittleEndian
        {
            get { return _explicitVRLittleEndian; }
            set { _explicitVRLittleEndian = value; }
        }

        private bool _explicitVRBigEndian = true;
        public bool ExplicitVRBigEndian
        {
            get { return _explicitVRBigEndian; }
            set { _explicitVRBigEndian = value; }
        }

        private bool _JPEGBaseline = false;
        public bool JPEGBaseline
        {
            get { return _JPEGBaseline; }
            set { _JPEGBaseline = value; }
        }

        private bool _JPEGExtended_4 = false;
        public bool JPEGExtended_4
        {
            get { return _JPEGExtended_4; }
            set { _JPEGExtended_4 = value; }
        }

        private bool _JPEGLosslessNonHierarchical_14 = false;
        public bool JPEGLosslessNonHierarchical_14
        {
            get { return _JPEGLosslessNonHierarchical_14; }
            set { _JPEGLosslessNonHierarchical_14 = value; }
        }

        private bool _JPEGLosslessNonHierarchicalFirstOrderPrediction = false;
        public bool JPEGLosslessNonHierarchicalFirstOrderPrediction
        {
            get { return _JPEGLosslessNonHierarchicalFirstOrderPrediction; }
            set { _JPEGLosslessNonHierarchicalFirstOrderPrediction = value; }
        }

        private bool _JPEGLSLossless = false;
        public bool JPEGLSLossless
        {
            get { return _JPEGLSLossless; }
            set { _JPEGLSLossless = value; }
        }

        private bool _JPEGLSLossy = false;
        public bool JPEGLSLossy
        {
            get { return _JPEGLSLossy; }
            set { _JPEGLSLossy = value; }
        }

        private bool _JPEG2000LosslessOnly = false;
        public bool JPEG2000LosslessOnly
        {
            get { return _JPEG2000LosslessOnly; }
            set { _JPEG2000LosslessOnly = value; }
        }

        private bool _JPEG2000 = false;
        public bool JPEG2000
        {
            get { return _JPEG2000; }
            set { _JPEG2000 = value; }
        }

        private bool _RLELossless = false;
        public bool RLELossless
        {
            get { return _RLELossless; }
            set { _RLELossless = value; }
        }
    }
}

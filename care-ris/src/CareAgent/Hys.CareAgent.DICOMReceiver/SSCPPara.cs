using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareAgent.DICOMReceiver
{
    public class SSCPPara
    {
        private String _AETitle;
        private UInt16 _Port;
        private String _StoragePath;
        private Int32 _TimeOut;

        public String AETitle
        {
            get
            {
                return _AETitle;
            }
            set
            {
                _AETitle = value;
            }
        }

        public UInt16 Port
        {
            get
            {
                return _Port;
            }
            set
            {
                _Port = value;
            }
        }

        public String StoragePath
        {
            get
            {
                return _StoragePath;
            }
            set
            {
                _StoragePath = value;
            }

        }

        public Int32 TimeOut
        {
            get
            {
                return _TimeOut;
            }
            set
            {
                _TimeOut = value;
            }

        }

        public SSCPPara(String aeTitle, UInt16 port, String storagePath, Int32 timeOut)
        {
            _AETitle = aeTitle;
            _Port = port;
            _StoragePath = storagePath;
            _TimeOut = timeOut;
        }
    }
}

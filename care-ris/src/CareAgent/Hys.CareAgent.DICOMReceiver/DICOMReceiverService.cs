using kdt_managed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareAgent.DICOMReceiver
{
    public class DICOMReceiverService
    {
        private SSCPPara sscpPatameter = null;
        MDTAeServer m_SSCPServer = null;
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");

        public DICOMReceiverService(String aeTitle, UInt16 port, String storagePath, Int32 timeOut)
        {
            sscpPatameter = new SSCPPara(aeTitle, port, storagePath, timeOut);
        }

        public void Start()
        {
            try
            {
                UInt16 port = sscpPatameter.Port;
                m_SSCPServer = new MDTAeServer();

                if (m_SSCPServer.listen(port, new DAPListenerReactor(sscpPatameter)))
                {
                    _logger.Info("UploadService.StartSSCPService(): " + "listening on port " + port.ToString());
                }
                else
                {
                    _logger.Info("UploadService.StartSSCPService(): " + "listening failed on port " + port.ToString() + ", please check if the port is used by another instance or application.");
                    return;
                }
            }
            catch (System.Exception except)
            {
                _logger.Error("UploadService.StartSSCPService(): " + "pop up an exception on start sscp service " + except.Message);
                return;
            }
        }
    }
}

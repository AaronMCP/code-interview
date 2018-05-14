using kdt_managed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Hys.CareAgent.DICOMReceiver
{
    internal class DAPAcseObserver : IDTAcseObserver
    {
        private AutoResetEvent m_sync = null;
        private SSCPPara m_theStore = null;
        private MDTAe m_theAe = null;
        private String m_reason = "";
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");

        // indicate if the whole case operation is success or not
        // it will determine if the assembled case will be submitted or not
        public bool m_OK = false;

        public DAPAcseObserver(AutoResetEvent sync, SSCPPara theStore, MDTAe theAe)
        {
            m_sync = sync;
            m_theStore = theStore;
            m_theAe = theAe;
        }

        /// <summary>
        /// This method is invoked when the A-ABORT is sent
        /// </summary>
        /// <param name="source">SCU or SCP</param>
        /// <param name="reason">why this occur,see kdt_managed.IDTAcseObserver.rej_reason_t</param>
        public void notifyIAborted(byte source, byte reason)
        {
            _logger.Info("DAPAcseObserver.notifyIAborted(): " + "I aborted the association for " + reason.ToString() + ".");
        }

        /// <summary>
        /// This method is invoked when the connection is down by the peer
        /// </summary>
        public void notifyConnectionDrop()
        {
            _logger.Info("DAPAcseObserver.notifyConnectionDrop(): " + "the connection is dropped.");
        }

        /// <summary>
        /// This method is invoked when the A-RELEASE_RQ is received
        /// </summary>
        public void notifyReleaseRequested()
        {
            _logger.Info("DAPAcseObserver.notifyReleaseRequested(): " + "a release request is received.");

        }

        /// <summary>
        /// This method is invoked when the A-ABORT is received
        /// </summary>
        /// <param name="source">SCU or SCP</param>
        /// <param name="reason">why this occur,see kdt_managed.IDTAcseObserver.rej_reason_t</param>
        public void notifyPeerAborted(byte source, byte reason)
        {
            m_OK = false;
            _logger.Info("DAPAcseObserver.notifyPeerAborted(): " + "peer aborted the association for " + reason.ToString() + ".");
        }

        /// <summary>
        /// This method is invoked when the association state is changed to UP
        /// </summary>
        public void notifyAssociationUp()
        {
            m_OK = true;
            _logger.Info("DAPAcseObserver.notifyAssociationUp(): " + "association is up.");
        }

        /// <summary>
        /// This method is invoked just after the DICOM protocol negotiation
        /// </summary>
        /// <param name="result">permanent of transient</param>
        /// <param name="source">SCU or SCP</param>
        /// <param name="reason">why this occur,see kdt_managed.IDTAcseObserver.rej_reason_t</param>
        /// <returns>if returns false,the association will be rejected</returns>
        public bool postNegotiate(ref kdt_managed.IDTAcseObserver.rej_result_t result, ref kdt_managed.IDTAcseObserver.rej_source_t source, ref kdt_managed.IDTAcseObserver.rej_reason_t reason)
        {
            return true;
        }

        /// <summary>
        /// This method is invoked just before the DICOM protocol negotiation
        /// </summary>
        /// <param name="result0">permanent of transient</param>
        /// <param name="source">SCU or SCP</param>
        /// <param name="reason">why this occur,see kdt_managed.IDTAcseObserver.rej_reason_t</param>
        /// <returns>if returns false,the association will be rejected</returns>
        public bool preNegotiate(ref kdt_managed.IDTAcseObserver.rej_result_t result0, ref kdt_managed.IDTAcseObserver.rej_source_t source, ref kdt_managed.IDTAcseObserver.rej_reason_t reason)
        {

            // check the if the called AE is the name of SSCPPara
            string calledAE = m_theStore.AETitle;
            if (calledAE == null)
            {
                calledAE = "DAPClient";
            }

            string callingAE = "*";

            string ca = m_theAe.getCalledAE().Trim();

            if (m_theAe.getCalledAE().Trim() == calledAE || calledAE == "*")
            {
                if (m_theAe.getCallingAE().Trim() == callingAE || callingAE == "*")
                {
                    return true;
                }
                else
                {
                    reason = kdt_managed.IDTAcseObserver.rej_reason_t.CALLING_AE_TITLE_NOT_RECOGNIZED;
                    _logger.Info("DAPAcseObserver.preNegotiate(): " + "Calling AE " + m_theAe.getCallingAE().Trim() + " is not recognized.");
                }
            }
            else
            {
                reason = kdt_managed.IDTAcseObserver.rej_reason_t.CALLED_AE_TITLE_NOT_RECOGNIZED;
                _logger.Info("DAPAcseObserver.preNegotiate(): " + "Called AE " + m_theAe.getCalledAE().Trim() + " is not recognized.");
            }
            result0 = kdt_managed.IDTAcseObserver.rej_result_t.PERMANENT;
            source = kdt_managed.IDTAcseObserver.rej_source_t.UL_SERVICE_PROVIDER_PRESENTATION;

            return false;
        }

        /// <summary>
        /// This method is invoked when the A-RELEASE-RP is received
        /// </summary>
        public void notifyReleaseConfirmed()
        {
            _logger.Info("DAPAcseObserver.notifyReleaseConfirmed(): " + "release is confirmed.");
        }

        /// <summary>
        /// This method is invoked when the A-ASSOCIATE_RJ is received
        /// </summary>
        /// <param name="result">permanent of transient</param>
        /// <param name="source">SCU or SCP</param>
        /// <param name="reason">why this occur,see kdt_managed.IDTAcseObserver.rej_reason_t</param>
        public void notifyRejected(byte result, byte source, byte reason)
        {
            _logger.Info("DAPAcseObserver.notifyRejected(): " + "association is rejected.");
        }

        /// <summary>
        /// This method is invoked when the TLS connection needs to authenticate the peer
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns>if authenticate successfully,returns true,or returns false</returns>
        public bool authenticate(byte[] certificate)
        {
            return true;
        }

        /// <summary>
        /// This method is invoked when the association state is changed to DOWN
        /// </summary>
        public void notifyAssociationDown()
        {
            _logger.Info("DAPAcseObserver.notifyAssociationDown(): " + "association is down.");
            if (m_sync != null)
            {
                m_sync.Set();
            }
        }

        /// <summary>
        /// This method is invoked when the A-ASSOCIATE-RJ is sent.
        /// </summary>
        /// <param name="result">permanent of transient</param>
        /// <param name="source">SCU or SCP</param>
        /// <param name="reason">why this occur,see kdt_managed.IDTAcseObserver.rej_reason_t</param>
        public void notifyIRejected(byte result, byte source, byte reason)
        {
            _logger.Info("DAPAcseObserver.notifyIRejected(): " + "I rejected the association for " + reason.ToString());
            m_OK = false;
            m_reason = "Association rejected";

        }
        /// <summary>
        /// This method is invoked when the A-RELEASE-RP is sent.
        /// </summary>
        public void notifyIReleased()
        {
            _logger.Info("DAPAcseObserver.notifyIReleased(): " + "I released the association.");
        }

        public string getImplClassUid()
        {
            string implUid = (string)m_theAe.getImplUID();

            return implUid;
        }

        public string getImplVersionName()
        {
            return m_theAe.getImplVersion();
        }
    }
}

using kdt_managed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hys.CareAgent.DICOMReceiver
{
    public class DAPListenerReactor : IDTListenerReactor
    {
        private SSCPPara m_StorePara;
        private AutoResetEvent m_sync = null;
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");

        public DAPListenerReactor(SSCPPara theStore)
        {
            m_StorePara = theStore;
            m_sync = new AutoResetEvent(false);
        }

        public void newClientIndication(MDTAe ae)
        {
            _logger.Info("DAPListenerReactor.newClientIndication(): " + "a new connection request received for " + ae.getCallingAE());
            // implementation uid
            const String impl_uid = "1.2.840.113564.12.1.1";
            // implement version name, the major and minor version number should be the same with that in the assembly
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            String impl_version = "SSM " + asm.GetName(false).Version.Major.ToString() + "." + asm.GetName(false).Version.Minor.ToString();
            // max pdu length
            const UInt32 pdu_length = 256 * 1024;
            // asynchorous window size
            const UInt16 asyn_window = 1;

            // start to do the association negotiation
            try
            {
                kdt_managed.UID implUid = new kdt_managed.UID(impl_uid);

                if (!ae.init
                    (
                      implUid,
                      impl_version,
                      pdu_length,
                      asyn_window,
                      null,
                      null,
                      null
                    ))
                {
                    _logger.Info("DAPListenerReactor.newClientIndication(): " + "initialize failed.");
                    return;
                }

                DAPAcseObserver currentObserver = new DAPAcseObserver(m_sync, m_StorePara, ae);
                DAPEchoService echo_service = new DAPEchoService(m_StorePara);
                ae.addService(echo_service);

                // supported transfer syntax for every SOP class
                AddSupportedStoreServices(ref ae, currentObserver, m_sync, m_StorePara);

                if (!ae.associate(currentObserver, null, 0))
                {
                    _logger.Info("DAPListenerReactor.newClientIndication(): " + "associate failed.");
                }
                m_sync.WaitOne(-1, false);
            }
            catch (System.Exception except)
            {
                _logger.Info("DAPListenerReactor.newClientIndication(): " + "pop up an exception--" + except.Message);
            }
        }

        void AddSupportedStoreServices(ref MDTAe ae, DAPAcseObserver observer, AutoResetEvent sync, SSCPPara theStore)
        {
            ts_t[] tsn = null;
            // confirm the transfer syntax(es) that will be used
            tsn = new ts_t[5];
            tsn[0] = ts_t.ImplicitVRLittleEndian;
            tsn[1] = ts_t.ExplicitVRLittleEndian;
            tsn[2] = ts_t.ExplicitVRBigEndian;
            tsn[3] = ts_t.JPEGLosslessNonHierarchical_14;
            tsn[4] = ts_t.JPEGLosslessNonHierarchicalFirstOrderPrediction;

            // confirm abstract syntax(es) that will be used
            DAPStoreService store_service = null;
            // add all the service
            store_service = new DAPStoreService(MDTStoreService.kMGPresentation, tsn, sync, theStore);
            store_service.setObserver(observer);
            ae.addService(store_service);

            store_service = new DAPStoreService(MDTStoreService.kMGProcessing, tsn, sync, theStore);
            store_service.setObserver(observer);
            ae.addService(store_service);

            store_service = new DAPStoreService(MDTStoreService.kDXPresentation, tsn, sync, theStore);
            store_service.setObserver(observer);
            ae.addService(store_service);

            store_service = new DAPStoreService(MDTStoreService.kDXProcessing, tsn, sync, theStore);
            store_service.setObserver(observer);
            ae.addService(store_service);

            store_service = new DAPStoreService(MDTStoreService.kCRImage, tsn, sync, theStore);
            store_service.setObserver(observer);
            ae.addService(store_service);

            store_service = new DAPStoreService(MDTStoreService.kCTImage, tsn, sync, theStore);
            store_service.setObserver(observer);
            ae.addService(store_service);

            store_service = new DAPStoreService(MDTStoreService.kMRImage, tsn, sync, theStore);
            store_service.setObserver(observer);
            ae.addService(store_service);

            store_service = new DAPStoreService(MDTStoreService.kNMImage, tsn, sync, theStore);
            store_service.setObserver(observer);
            ae.addService(store_service);

            store_service = new DAPStoreService(MDTStoreService.kUSImage, tsn, sync, theStore);
            store_service.setObserver(observer);
            ae.addService(store_service);

            store_service = new DAPStoreService(MDTStoreService.kXAImage, tsn, sync, theStore);
            store_service.setObserver(observer);
            ae.addService(store_service);

            store_service = new DAPStoreService(MDTStoreService.kSCImage, tsn, sync, theStore);
            store_service.setObserver(observer);
            ae.addService(store_service);

            store_service = new DAPStoreService(MDTStoreService.kXRFImage, tsn, sync, theStore);
            store_service.setObserver(observer);
            ae.addService(store_service);

            store_service = new DAPStoreService(MDTStoreService.kUSMFImage, tsn, sync, theStore);
            store_service.setObserver(observer);
            ae.addService(store_service);

        }

    }
}

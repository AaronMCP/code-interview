using kdt_managed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Hys.CareAgent.DAP;

namespace Hys.CareAgent.DICOMReceiver
{
    internal class DAPStoreService : MDTStoreService
    {
        private SSCPPara m_theStore;
        private DAPAcseObserver m_theObserver = null;
        private String m_fileName = "test.dcm";
        private AutoResetEvent m_sync = null;
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");

        public DAPStoreService(kdt_managed.UID asn, ts_t[] tsn, AutoResetEvent sync, SSCPPara theStore)
            : base(asn, MDTService.role_t.SCP, tsn)
        {
            m_theStore = theStore;
            m_sync = sync;
            m_fileName = m_theStore.StoragePath;
        }

        /// <summary>
        /// returns the pointer to this object itself,used by KDT 
        /// </summary>
        /// <returns></returns>
        public override MDTService clone()
        {
            return new DAPStoreService(this.asn, this.tsn, this.m_sync, m_theStore);
        }

        /// <summary>
        /// used by SCU
        /// </summary>
        /// <param name="con">confirm information from SCP</param>
        public override void confirmation(MElementList con)
        {
        }

        /// <summary>
        /// indicate receiving over of an image
        /// </summary>
        /// <param name="ind">all the information about the received image</param>
        /// <param name="status">status that returned to the SCU</param>
        public override void indication(MElementList ind, MElementList status)
        {
            using (MElementRef eref = ind.get_Element(tag_t.kSOPInstanceUID))
            {
                if (eref != null)
                {
                    m_fileName = eref.get_string(0);
                }
            }

            MElementList pt10file = ind.getDataset();
            // save image and update case information
            if (!SaveFile(pt10file))
            {
                try
                {
                    using (MElementRef statusRef = status.get_Element(tag_t.kStatus))
                    {
                        // notify the client that image processing failed and cancel the whole association
                        statusRef.set_ushort(0, (ushort) MDTService.status_t.PROCESSING_FAILURE);
                        statusRef.set_ushort(1, (ushort) MDTService.status_t.CANCEL);
                    }

                    // file saving failed,the case should not be submitted
                    m_theObserver.m_OK = false;
                }
                catch (Exception e)
                {
                    m_theObserver.m_OK = false;
                    _logger.Info("DAPStoreService.indication(): " + "set response status error " + e.Message);
                }
            }

            // release the memory used by MElementList
            pt10file.Dispose();
            ind.Dispose();

            GC.Collect();
            GC.WaitForPendingFinalizers();

        }

        public void setObserver(DAPAcseObserver theObserver)
        {
            m_theObserver = theObserver;
        }

        private bool SaveFile(MElementList dataset)
        {
            string rootPath = m_theStore.StoragePath + Convert.ToString(System.IO.Path.DirectorySeparatorChar);
            DAPBussiness bussiness = new DAPBussiness();
            var result=bussiness.SaveDICOMInfo(dataset, base.getNegotiatedTsn(), m_theObserver.getImplClassUid(),
                m_theObserver.getImplVersionName(), rootPath);
            return result;
        }
    }
}

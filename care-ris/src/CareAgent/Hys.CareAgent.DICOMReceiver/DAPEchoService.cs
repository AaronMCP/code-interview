using kdt_managed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Hys.CareAgent.DICOMReceiver
{
    internal class DAPEchoService : MDTEchoService
    {
        private SSCPPara m_theStore;
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");

        public DAPEchoService(SSCPPara theStore)
            : base(MDTService.role_t.SCP)
        {
            m_theStore = theStore;
        }

        /// <summary>
        /// returns the pointer to this object itself,used by KDT
        /// </summary>
        /// <returns></returns>
        override public MDTService clone()
        {
            return new DAPEchoService(m_theStore);
        }

        /// <summary>
        /// used in SCU,not SCP
        /// </summary>
        override public void confirmation()
        {
        }

        /// <summary>
        /// indicate an echo request coming,do nothing
        /// </summary>
        override public void indication()
        {
            _logger.Info("DAPEchoService.indication(): " + "an echo request comming.");
        }
    }
}

using Hys.CareAgent.DAP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Hys.CareAgent.WcfService
{
    public partial class FormDcmInfo : Form
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");
        private readonly string studyInstanceUID = "";
        public FormDcmInfo(string strStudyInstanceUID)
        {
            InitializeComponent();
            studyInstanceUID = strStudyInstanceUID;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            try
            {
                List<string> listImage = new List<string>();
                DAPBussiness bussiness = new DAPBussiness();
                listImage = bussiness.SearchDICOMFile(studyInstanceUID);
                m_dcmcontViewer.OpenDicomImages(listImage);
            }
            catch (Exception ex)
            {
                _logger.Error("FormDcmInfo pops up exception during being launched: " +
                    ex.ToString());
            }
        }
    }
}
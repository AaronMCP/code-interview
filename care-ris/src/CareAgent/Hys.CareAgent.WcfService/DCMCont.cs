using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Hys.CareAgent.WcfService
{
    public partial class DCMCont : UserControl
    {
        private string emrKeyName;
        private int iActivePage = 1;
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");
        public DCMCont()
        {
            InitializeComponent();
        }

        public string EMRKeyName
        {
            get { return emrKeyName; }
            set { emrKeyName = value; }
        }

        private List<string> Files = new List<string>();

        public void OpenDicomImages(List<string> lstFile)
        {
            try
            {
                Files = lstFile;
                this.Enabled = false;
                //foreach (string filePath in lstFile)
                //{
                //    DicomViewer.OpenDicomImage(filePath, 0);
                //}

                if (Files.Count == 0 || Files.Count == 1)
                {
                    plPage.Visible = false;

                }

                if (Files.Count > 0)
                {
                    DicomViewer.OpenDicomImage(Files[0], 0);
                }

                if (Files.Count > 1)
                {
                    m_btnFirstPage.Enabled = false;
                    m_btnLastPage.Enabled = true;
                    m_btnNextPage.Enabled = true;
                    m_btnPrePage.Enabled = false;
                    m_textboxActivePage.Text = "1";
                    m_textboxActivePage.Enabled = true;
                    m_textboxTotalPages.Text = Files.Count.ToString();
                    m_textboxTotalPages.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("OpenDicomImages, exception:" +
                   ex.ToString());
            }

            this.Enabled = true;
        }

        public void CloseControl()
        {
            try
            {
                DicomViewer.CloseAllImages();
                DicomViewer.Dispose();
            }
            catch (Exception ex)
            {
                _logger.Error("DICOM CloseControl, exception:" +
                   ex.ToString());
            }
        }

        private void Paging()
        {
            if (iActivePage == 1)
            {
                m_btnFirstPage.Enabled = false;
                m_btnLastPage.Enabled = true;
                m_btnNextPage.Enabled = true;
                m_btnPrePage.Enabled = false;
            }
            else if (iActivePage > 1 && iActivePage < Files.Count)
            {
                m_btnFirstPage.Enabled = true;
                m_btnLastPage.Enabled = true;
                m_btnNextPage.Enabled = true;
                m_btnPrePage.Enabled = true;
            }
            else if (iActivePage == Files.Count)
            {
                m_btnFirstPage.Enabled = true;
                m_btnLastPage.Enabled = false;
                m_btnNextPage.Enabled = false;
                m_btnPrePage.Enabled = true;
            }
            m_textboxActivePage.Text = iActivePage.ToString();
            DicomViewer.CloseAllImages();
            DicomViewer.OpenDicomImage(Files[iActivePage - 1], 0);
            
        }

        private void m_btnFirstPage_Click(object sender, EventArgs e)
        {
            iActivePage = 1;
            Paging();
        }

        private void m_btnPrePage_Click(object sender, EventArgs e)
        {
            iActivePage--;
            Paging();
        }

        private void m_btnNextPage_Click(object sender, EventArgs e)
        {
            iActivePage++;
            Paging();
        }

        private void m_btnLastPage_Click(object sender, EventArgs e)
        {
            iActivePage = Files.Count;
            Paging();
        }

        private void m_textboxActivePage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') ||
                e.KeyChar == 8 || e.KeyChar == 46 || e.KeyChar == 3 || e.KeyChar == 22))
            {
                e.Handled = true;
            }
        }

        protected override bool ProcessDialogKey(System.Windows.Forms.Keys aKey)
        {
            if (aKey == Keys.Enter)
            {
                if (m_textboxActivePage.Focused)
                {
                    int iInputPage = 0;
                    try
                    {
                        iInputPage = Convert.ToInt32(m_textboxActivePage.Text);
                    }
                    catch (Exception ex)
                    {
                        iActivePage = 1;
                    }

                    if (iInputPage < 1 || iInputPage > Files.Count)
                    {
                        iActivePage = 1;
                    }
                    else
                    {
                        iActivePage = iInputPage;
                    }

                    Paging();
                    return true;
                }

            }
            base.ProcessDialogKey(aKey);
            return false;
        }

        private void DCMCont_Resize(object sender, EventArgs e)
        {
            m_panelPageControl.Left = (Width - m_panelPageControl.Width) / 2;
        }
    }
}

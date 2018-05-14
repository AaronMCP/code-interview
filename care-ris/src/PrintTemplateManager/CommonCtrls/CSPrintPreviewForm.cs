using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.C1Report;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace Hys.CommonControls
{
    public partial class CSPrintPreviewForm : CSForm
    {
        private C1Report _rpt = null;
        private int _numOfCopies = 1;
        private string _printerName;

        public RadToolStrip ToolStrip
        {
            get
            {
                return radToolStrip1;
            }
        }

        public C1Report C1Report
        {
            get
            {
                return _rpt;
            }
        }

        public Func<string, string, string, string> MultiLanTranslator { get; set; }

        public Func<string, string, string> GetUserProfileByName { get; set; }

        public Action<string, string,string> SetUserProfileNoRole { get; set; }

        public bool RememberSizeLocation { get; set; }

        public event EventHandler PrintCompleted;

        public CSPrintPreviewForm(C1Report rpt, string printerName)
        {
            InitializeComponent();
            _rpt = rpt;
            _printerName = printerName;
        }

        protected override void OnClosed(EventArgs e)
        {
            _rpt.EndReport -= new EventHandler(_rpt_EndReport);
            base.OnClosed(e);
        }

        private void _rpt_EndReport(object sender, EventArgs e)
        {
            _ppcPreview_StartPageChanged(null, null);
        }

        private void _ppcPreview_StartPageChanged(object sender, EventArgs e)
        {
            string pageNum = "N.O {0}";
            if (MultiLanTranslator != null)
            {
                pageNum = MultiLanTranslator("PageNumber", "", "N.O {0}");
            }
            tsLblPageIndex.Text = string.Format(pageNum, (_ppcPreview.StartPage + 1) + "/" + _rpt.Page);
        }

        private void tsCBXNumberofCopy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RadItem selectedItem = tsCBXNumberofCopy.SelectedItem as RadItem;
                if (selectedItem != null)
                    _numOfCopies = System.Convert.ToInt32(selectedItem.Text);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }
        }

        private void tsMenu50_Click(object sender, EventArgs e)
        {
            _ppcPreview.Zoom = 0.5;
        }

        private void tsMenu75_Click(object sender, EventArgs e)
        {
            _ppcPreview.Zoom = 0.75;
        }

        private void tsMenu100_Click(object sender, EventArgs e)
        {
            _ppcPreview.Zoom = 1;
        }

        private void tsMenu200_Click(object sender, EventArgs e)
        {
            _ppcPreview.Zoom = 2;
        }

        private void tsMenu400_Click(object sender, EventArgs e)
        {
            _ppcPreview.Zoom = 4;
        }

        private void tsBtnNextPage_Click(object sender, EventArgs e)
        {
            _ppcPreview.StartPage++;
        }

        private void tsBtnPrevPage_Click(object sender, EventArgs e)
        {
            if (_ppcPreview.StartPage > 0)
                _ppcPreview.StartPage--;
        }

        private void tsBtnPrintSet_Click(object sender, EventArgs e)
        {
            tsBtnPrintSet.Enabled = false;

            try
            {
                using (PrintDialog dlg = new PrintDialog())
                {
                    dlg.Document = _rpt.Document;
                    if (dlg.ShowDialog() == DialogResult.OK && _rpt != null && _rpt.Document != null)
                    {
                        _printerName = dlg.PrinterSettings.PrinterName;
                        _rpt.Document.PrinterSettings.Copies = dlg.PrinterSettings.Copies;
                        _rpt.Document.PrinterSettings.PrinterName = _printerName;
                        Print();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            tsBtnPrintSet.Enabled = true;
        }

        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            tsBtnPrint.Enabled = false;

            try
            {
                Print();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            tsBtnPrint.Enabled = true;
        }

        private void Print()
        {
            if (_numOfCopies < 1) _numOfCopies = 1;
            if (_numOfCopies > 9) _numOfCopies = 9;

            lock (_rpt)
            {
                for (int i = 0; i < _numOfCopies; i++)
                {
                    _rpt.Document.Print();
                }
            }

            if (PrintCompleted != null)
            {
                PrintCompleted(this, new EventArgs());
            }
        }

        private void OnSelectPreview()
        {
            try
            {
                if (_rpt.Document.PrinterSettings.IsValid)
                {
                    _ppcPreview.Document = _rpt.Document;
                    _ppcPreview.InvalidatePreview();
                }
                else
                {
                    throw new Exception("Invalid printer setting!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CSPrintPreviewForm_Load(object sender, EventArgs e)
        {
            setWindowSizeLocation();
            _rpt.EndReport += new EventHandler(_rpt_EndReport);
            multilanTranslate();
            OnSelectPreview();
        }

        private void setWindowSizeLocation()
        {
            try
            {
                if (RememberSizeLocation &&  this.Tag != null)
                {
                    string sizeLocation = GetUserProfileByName(this.Tag + "SizeLocation", "0200");
                    if (!string.IsNullOrEmpty(sizeLocation))
                    {
                        string[] arr = sizeLocation.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        this.Size = new Size(Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]));
                        this.Location = new Point(Convert.ToInt32(arr[2]), Convert.ToInt32(arr[3]));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void saveWindowSizeLocation()
        {
            try
            {
                if (RememberSizeLocation && this.Tag != null)
                {
                    string sizeLocation = string.Format("{0};{1};{2};{3}", this.Size.Width, this.Size.Height, this.Location.X, this.Location.Y);
                    SetUserProfileNoRole(this.Tag +"SizeLocation", "0200", sizeLocation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void multilanTranslate()
        {
            if (MultiLanTranslator != null)
            {
                this.Text = MultiLanTranslator(this.Text, "", this.Text);
                foreach (RadItem item in radToolStripItem1.Items)
                {
                    item.Text = MultiLanTranslator(item.Text, "" , item.Text);
                    item.UpdateLayout();
                }
            }
        }

        private void CSPrintPreviewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveWindowSizeLocation();
        }
    }
}

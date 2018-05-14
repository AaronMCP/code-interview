using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace HYS.IM.UIControl
{
    public class SliderPanel : Panel 
    {
        public ArrayList Pages = new ArrayList();

        private int _currentPageIndex;
        public int CurrentPageIndex
        {
            get { return _currentPageIndex; }
        }
        public Control CurrentPage
        {
            get
            {
                if (_currentPageIndex < 0 ||
                    _currentPageIndex >= Controls.Count) return null;
                return Controls[_currentPageIndex];
            }
        }

        private void NotifyPageRefresh()
        {
            if (OnPageRefresh != null) OnPageRefresh(this, EventArgs.Empty);
        }
        public event EventHandler OnPageRefresh;

        public void RefreshPage()
        {
            foreach (Control ctl in Controls)
            {
                ctl.Visible = false;
            }

            if (_currentPageIndex < 0 ||
                _currentPageIndex >= Controls.Count) return;

            Control sctl = Controls[_currentPageIndex] as Control;
            if (sctl == null) return;

            sctl.Dock = DockStyle.Fill;
            sctl.Visible = true;

            NotifyPageRefresh();
        }
        public void AddPage(IPage page)
        {
            Pages.Add(page);
            Controls.Add(page.GetControl());

            page.MoveNext += new PageEventHandler(page_MoveNext);
            page.MovePrev += new PageEventHandler(page_MovePrev);
        }
        public void RemovePage(IPage page)
        {
            if (Pages.Contains(page)) Pages.Remove(page);
            if (Controls.Contains(page.GetControl())) Controls.Remove(page.GetControl());

            page.MoveNext -= new PageEventHandler(page_MoveNext);
            page.MovePrev -= new PageEventHandler(page_MovePrev);
        }
        public void GotoPage(IPage page)
        {
            if (!Controls.Contains(page.GetControl())) return;
            int index = Controls.IndexOf(page.GetControl());
            if (_currentPageIndex == index) return;
            _currentPageIndex = index;
            RefreshPage();
        }
        public void NextPage()
        {
            if (_currentPageIndex < 0 ||
                _currentPageIndex >= Controls.Count - 1) return;

            _currentPageIndex++;
            RefreshPage();
        }
        public void PrevPage()
        {
            if (_currentPageIndex <= 0 ||
                _currentPageIndex >= Controls.Count) return;

            _currentPageIndex--;
            RefreshPage();
        }

        private void page_MovePrev(IPage me)
        {
        }
        private void page_MoveNext(IPage me)
        {
        }
    }
}

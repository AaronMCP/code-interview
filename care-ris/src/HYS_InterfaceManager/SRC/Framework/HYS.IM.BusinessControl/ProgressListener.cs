using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.BusinessControl
{
    public class ProgressListener
    {
        private Label _progTitle;
        private ProgressBar _progBar;
        public ProgressListener(Label title, ProgressBar bar)
        {
            _progBar = bar;
            _progTitle = title;
            if (_progBar == null||
                _progTitle ==null ) throw new ArgumentNullException();
        }

        private IProgress _progUnit;
        public void AttachProgress(IProgress unit)
        {
            _progUnit = unit;
            if (_progUnit == null) throw new ArgumentNullException();

            _progUnit.OnStart += new ProgressStartEventHandler(_progUnit_OnStart);
            _progUnit.OnGoing += new ProgressGoingEventHandler(_progUnit_OnGoing);
            _progUnit.OnComplete += new ProgressCompleteEventHandler(_progUnit_OnComplete);
        }

        private void _progUnit_OnStart(int max, int min, int val, string title)
        {
            if (!_progBar.Visible) return;

            _progTitle.Text = title;
            _progBar.Maximum = max;
            _progBar.Minimum = min;
            if (_progBar.Maximum >= val &&
                _progBar.Minimum <= val)
                _progBar.Value = val;

            Application.DoEvents();
            //System.Threading.Thread.Sleep(500);
        }
        private void _progUnit_OnComplete(bool succeed, string message)
        {
            if (!_progBar.Visible) return;

            if (succeed)
            {
                _progBar.Value = _progBar.Maximum;
            }

            Application.DoEvents();
            //System.Threading.Thread.Sleep(500);
        }
        private void _progUnit_OnGoing(int val, string caption)
        {
            if (!_progBar.Visible) return;

            _progTitle.Text = caption;
            if (_progBar.Maximum >= val &&
                _progBar.Minimum <= val)
                _progBar.Value = val;

            Application.DoEvents();
            //System.Threading.Thread.Sleep(500);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Adapter.Composer.Forms
{
    public class CodeStatusMgt
    {
        private CodeStatus _status = InitCode.Instance;
        public CodeStatus Status
        {
            get { return _status; }
            set 
            {
                if (_status == value) return;
                if (OnChange != null) OnChange(_status, value);
                _status = value;
            }
        }

        public event CodeStatusChangeEventHandler OnChange;

        public void GB_BIG5()
        {
            Status.GB_BIG5(this);
        }
        public void GB_GBK()
        {
            Status.GB_GBK(this);
        }
        public void GBK_BIG5()
        {
            Status.GBK_BIG5(this);
        }
        public void GBK_GB()
        {
            Status.GBK_GB(this);
        }
        public void BIG5_GBK()
        {
            Status.BIG5_GBK(this);
        }
        public void BIG5_GB()
        {
            Status.BIG5_GB(this);
        }
        public void Reset()
        {
            Status.Reset(this);
        }
    }

    public delegate void CodeStatusChangeEventHandler(CodeStatus from, CodeStatus to);
}

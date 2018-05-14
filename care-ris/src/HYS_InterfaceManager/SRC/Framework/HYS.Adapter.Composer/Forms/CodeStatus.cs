using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HYS.Adapter.Composer.Forms
{
    public abstract class CodeStatus
    {
        public virtual void GB_BIG5(CodeStatusMgt mgt)
        {
        }
        public virtual void GB_GBK(CodeStatusMgt mgt)
        {
        }
        public virtual void GBK_BIG5(CodeStatusMgt mgt)
        {
        }
        public virtual void GBK_GB(CodeStatusMgt mgt)
        {
        }
        public virtual void BIG5_GBK(CodeStatusMgt mgt)
        {
        }
        public virtual void BIG5_GB(CodeStatusMgt mgt)
        {
        }
        public virtual void Reset(CodeStatusMgt mgt)
        {
        }
    }

    public class InitCode : CodeStatus
    {
        private static InitCode _code;
        public static InitCode Instance
        {
            get
            {
                if (_code == null) _code = new InitCode();
                return _code;
            }
        }

        public override void GB_BIG5(CodeStatusMgt mgt)
        {
            mgt.Status = BIG5Code.Instance;
        }
        public override void GB_GBK(CodeStatusMgt mgt)
        {
            mgt.Status = GBKCode.Instance;
        }
        public override void GBK_BIG5(CodeStatusMgt mgt)
        {
            mgt.Status = BIG5Code.Instance;
        }
        public override void GBK_GB(CodeStatusMgt mgt)
        {
            mgt.Status = GBCode.Instance;
        }
        public override void BIG5_GBK(CodeStatusMgt mgt)
        {
            mgt.Status = GBKCode.Instance;
        }
        public override void BIG5_GB(CodeStatusMgt mgt)
        {
            mgt.Status = GBCode.Instance;
        }
    }

    public class GBCode : CodeStatus
    {
        private static GBCode _code;
        public static GBCode Instance
        {
            get
            {
                if (_code == null) _code = new GBCode();
                return _code;
            }
        }

        public override void GB_BIG5(CodeStatusMgt mgt)
        {
            mgt.Status = BIG5Code.Instance;
        }
        public override void GB_GBK(CodeStatusMgt mgt)
        {
            mgt.Status = GBKCode.Instance;
        }
        public override void Reset(CodeStatusMgt mgt)
        {
            mgt.Status = InitCode.Instance;
        }
    }

    public class GBKCode : CodeStatus
    {
        private static GBKCode _code;
        public static GBKCode Instance
        {
            get
            {
                if (_code == null) _code = new GBKCode();
                return _code;
            }
        }

        public override void GBK_BIG5(CodeStatusMgt mgt)
        {
            mgt.Status = BIG5Code.Instance;
        }
        public override void GBK_GB(CodeStatusMgt mgt)
        {
            mgt.Status = GBCode.Instance;
        }
        public override void Reset(CodeStatusMgt mgt)
        {
            mgt.Status = InitCode.Instance;
        }
    }

    public class BIG5Code : CodeStatus
    {
        private static BIG5Code _code;
        public static BIG5Code Instance
        {
            get
            {
                if (_code == null) _code = new BIG5Code();
                return _code;
            }
        }

        public override void BIG5_GBK(CodeStatusMgt mgt)
        {
            mgt.Status = GBKCode.Instance;
        }
        public override void BIG5_GB(CodeStatusMgt mgt)
        {
            mgt.Status = GBCode.Instance;
        }
        public override void Reset(CodeStatusMgt mgt)
        {
            mgt.Status = InitCode.Instance;
        }
    }
}

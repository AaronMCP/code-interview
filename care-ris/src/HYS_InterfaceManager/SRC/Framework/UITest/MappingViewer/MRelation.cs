using System;
using System.Collections.Generic;
using System.Text;

namespace UITest.MappingViewer
{
    public class MRelation : IRelation
    {
        private MRelationType _type = MRelationType.None;
        private List<IItem> _sourceItems = new List<IItem>();
        private List<IItem> _targetItems = new List<IItem>();

        public MRelation()
        {
        }
        public MRelation(IItem source, IItem target)
        {
            _type = MRelationType.OneToOne;
            _sourceItems.Add(source);
            _targetItems.Add(target);
        }
        public MRelation(IItem[] sList, IItem target)
        {
            _type = MRelationType.MultiToOne;
            foreach (IItem s in sList) _sourceItems.Add(s);
            _targetItems.Add(target);
        }
        public MRelation(IItem source, IItem[] tList)
        {
            _type = MRelationType.OneToMulti;
            _sourceItems.Add(source);
            foreach (IItem t in tList) _targetItems.Add(t);
        }

        #region IMRelation Members

        public MRelationType Type
        {
            get 
            { 
                return _type; 
            }
            set 
            { 
                _type = value; 
            }
        }

        public List<IItem> Sources
        {
            get
            {
                return _sourceItems;
            }
            set
            {
                _sourceItems = value;
            }
        }

        public List<IItem> Targets
        {
            get
            {
                return _targetItems;
            }
            set
            {
                _targetItems = value;
            }
        }

        #endregion
    }
}

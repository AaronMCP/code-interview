using System;
using System.Collections.Generic;
using System.Text;

namespace UITest.MappingViewer
{
    public class MListItem : IItem 
    {
        //private IMItem _target;
        //private IMItem _source;
        private IControler _controler;

        private string _displayText;
        public MListItem(string displayText)
        {
            _displayText = displayText;
        }

        public override string ToString()
        {
            return _displayText;
        }

        #region IMListItem Members

        //public IMItem MapTarget
        //{
        //    get
        //    {
        //        return _target;
        //    }
        //    set
        //    {
        //        _target = value;
        //    }
        //}

        //public IMItem MapSource
        //{
        //    get
        //    {
        //        return _source;
        //    }
        //    set
        //    {
        //        _source = value;
        //    }
        //}

        public IControler Controler
        {
            get
            {
                return _controler;
            }
            set
            {
                _controler = value;
            }
        }

        #endregion
    }
}

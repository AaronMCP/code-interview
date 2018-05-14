using System;
using System.Collections.Generic;
using HYS.Common.Objects.Device;

namespace HYS.Adapter.Base
{
    public abstract class AdapterEntryAttributeBase : Attribute
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private DirectionType _direction;
        public DirectionType Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}

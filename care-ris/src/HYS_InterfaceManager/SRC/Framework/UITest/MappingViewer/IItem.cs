using System;
using System.Collections.Generic;
using System.Text;

namespace UITest.MappingViewer
{
    public interface IItem
    {
        string ToString();
        IControler Controler { get; set; }
    }
}

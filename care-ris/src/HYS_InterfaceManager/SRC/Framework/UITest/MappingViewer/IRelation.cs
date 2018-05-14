using System;
using System.Collections.Generic;
using System.Text;

namespace UITest.MappingViewer
{
    public interface IRelation
    {
        MRelationType Type { get; set;}
        List<IItem> Sources { get; set; }
        List<IItem> Targets { get; set; }
    }
}

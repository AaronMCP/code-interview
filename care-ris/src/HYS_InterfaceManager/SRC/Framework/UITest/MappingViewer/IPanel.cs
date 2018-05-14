using System;
using System.Collections.Generic;
using System.Text;

namespace UITest.MappingViewer
{
    public interface IPanel
    {
        void Redraw();

        void AddList(IControler list);
        void RemoveList(IControler list);
        void ClearList();
    }
}

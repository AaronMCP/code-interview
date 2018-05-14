using System;
using System.Collections.Generic;
using System.Text;
using HYS.IM.Controler;
using HYS.IM.BusinessEntity;

namespace HYS.IM.Forms
{
    public interface IView
    {
        void HideGroup();
        void RefreshView();
        void GroupByType();
        void BrowseFolder();
        void GroupByDirection();
        event EventHandler SelectionChange;
        IDevice SelectedItem { get;}
    }
}

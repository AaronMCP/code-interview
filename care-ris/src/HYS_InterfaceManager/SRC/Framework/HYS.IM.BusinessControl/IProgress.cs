using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.BusinessControl
{
    public interface IProgress
    {
        event ProgressStartEventHandler OnStart;
        event ProgressGoingEventHandler OnGoing;
        event ProgressCompleteEventHandler OnComplete;
    }

    public delegate void ProgressStartEventHandler(int max, int min, int val, string title);
    public delegate void ProgressCompleteEventHandler(bool succeed, string message);
    public delegate void ProgressGoingEventHandler(int val, string caption);
}

#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Hys.Platform.Application
{
    public class DisposableServiceBase : IDisposable
    {
        public IList<IDisposable> DisposableObjectList { get; private set; }

        public DisposableServiceBase()
        {
            DisposableObjectList = new List<IDisposable>();
        }

        protected void AddDisposableObject(object obj)
        {
            var disposable = obj as IDisposable;
            if (disposable != null)
            {
                if (!DisposableObjectList.Contains(obj))
                    DisposableObjectList.Add(disposable);
            }
        }

        public void Dispose()
        {
            foreach (var disposable in DisposableObjectList)
            {
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
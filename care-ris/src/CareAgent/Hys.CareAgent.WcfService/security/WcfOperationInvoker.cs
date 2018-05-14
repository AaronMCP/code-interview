using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Hys.CareAgent.WcfService.security
{
    public class WcfOperationInvoker : IOperationInvoker
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");
        IOperationInvoker _baseInvoker;
        string _operationName;

        public WcfOperationInvoker(IOperationInvoker baseInvoker, DispatchOperation operation)
        {
            _baseInvoker = baseInvoker;
            _operationName = operation.Name;
        }

        public object[] AllocateInputs()
        {
            return this._baseInvoker.AllocateInputs();
        }

        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            try
            {
                return _baseInvoker.Invoke(instance, inputs, out outputs);
            }
            catch (Exception ex)
            {
                outputs = new object[] { };
                _logger.Info(_operationName);
                _logger.Error(ex);
                return null;
            }
        }

        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            return this._baseInvoker.InvokeBegin(instance, inputs, callback, state);
        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            return this._baseInvoker.InvokeEnd(instance, out outputs, result);
        }

        public bool IsSynchronous
        {
            get { return this._baseInvoker.IsSynchronous; }
        }
    }
}

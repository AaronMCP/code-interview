using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.MessageDevices.MessagePipe.Base.Channel;
using HYS.Common.Logging;
using HYS.MessageDevices.MessagePipe.Base;
using HYS.MessageDevices.MessagePipe.Processors;
using HYS.MessageDevices.MessagePipe.Base.Processor;
using HYS.Common.Xml;

namespace HYS.MessageDevices.MessagePipe.Channels.Common
{
    public class ProcessingPipeLine
    {
        private ILog _log;
        private ChannelInitializationParameter _param;
        private List<ProcessorControler> _processorList;
        private XCollection<ProcessorInstance> _processorConfig;

        public ProcessingPipeLine(XCollection<ProcessorInstance> config, ChannelInitializationParameter param)
        {
            _param = param;
            _log = param.Log;
            _processorConfig = config;
            _processorList = new List<ProcessorControler>();
        }

        public bool Initialize()
        {
            bool res = false;
            _log.Write(string.Format("Begin loading processors for channel: {0}", _param.ChannelName));

            try
            {
                foreach (ProcessorInstance pi in _processorConfig)
                {
                    ProcessorInitializationParameter param = new ProcessorInitializationParameter
                        (pi.Name, _param.StartupPath, pi.Setting, _param.Log);

                    _log.Write(string.Format("Begin initializing processor {0} as {1}", pi.Name, pi.DeviceName));

                    IProcessor proc = ProcessorFactory.CreateProcessor(pi.DeviceName, _log);
                    res = (proc != null && proc.Initialize(param));

                    _log.Write(string.Format("End initializing processor {0} as {1}, result: {2}", pi.Name, pi.DeviceName, res));

                    if (res)
                    {
                        _processorList.Add(new ProcessorControler(proc, pi));
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                _log.Write(e);
                res = false;
            }

            _log.Write(string.Format("Begin loading processors for channel: {0} Result: {1}", _param.ChannelName, res));
            return res;
        }

        public bool Process(MessagePackage message)
        {
            if (message == null) return false;

            bool res = false;
            foreach (ProcessorControler pc in _processorList)
            {
                string desc = pc.ProcessorConfig.Name;
                _log.Write(string.Format("Begin calling processor {0}.", desc));
                res = pc.ProcessorImpl.ProcessMessage(message);
                _log.Write(string.Format("End calling processor {0}, result: {1}", desc, res));
                if (!res) return false;
            }
            return res;
        }

        public void Uninitialize()
        {
            try
            {
                foreach (ProcessorControler proc in _processorList)
                {
                    bool res = proc.ProcessorImpl.Uninitilize();
                    _log.Write(string.Format("Uninitialize processor {0} as {1}, result: {2}", proc.ProcessorConfig.Name, proc.ProcessorConfig.DeviceName, res));
                }
                _processorList.Clear();
            }
            catch (Exception err)
            {
                _log.Write(err);
            }
        }
    }
}

using System;
using System.Xml;
using System.Text;
using System.Data;
using System.Timers;
using System.Collections.Generic;
using HYS.XmlAdapter.Common.Net;
using HYS.XmlAdapter.Common.Objects;
using HYS.XmlAdapter.Outbound.Objects;
using HYS.XmlAdapter.Outbound.Adapters;
using HYS.Common.Objects.Logging;

namespace HYS.XmlAdapter.Outbound.Controlers
{
    public class XIMClient
    {
        private Timer _timer;
        private ServiceMain _service;
        public XIMClient(ServiceMain service)
        {
            _service = service;

            //Program.ConfigMgt.Config.SocketConfig.ReceiveEndSign = XIMClientHelper.ResponseEndSign;
            //Program.ConfigMgt.Config.SocketConfig.SendEndSign = XIMClientHelper.RequestEndSign;

            _timer = new Timer(Program.ConfigMgt.Config.DataCheckingInterval);
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            Program.Log.Write("# Timer tick begin.", XIMClientHelper.ModuleName);

            int index = 0;
            foreach (XIMOutboundMessage msg in Program.ConfigMgt.Config.Messages)
            {
                try
                {
                    index++;
                    List<XIMWrapper> resultList = null;
                    string strPrefix = "(" + index.ToString() + ") ";
                    Program.Log.Write(strPrefix + "Begin processing message (" + msg.GWEventType.ToString() + ") to (" + msg.HL7EventType.ToString() + ").", XIMClientHelper.ModuleName);

                    #region Data Processing

                    DataSet dataSet = _service.RequestData(msg.Rule, null);
                    if (dataSet == null)
                    {
                        Program.Log.Write(LogType.Warning, "Query data from GC Gateway database failed.");
                    }
                    else
                    {
                        List<DataSet> splitedDataSet = XIMClientHelper.SplitDataSet(dataSet);
                        if (splitedDataSet == null || splitedDataSet.Count < 1)
                        {
                            Program.Log.Write(LogType.Warning, "No record found in GC Gateway database query result.");
                        }
                        else
                        {
                            Program.Log.Write(strPrefix + "Query data from GC Gateway database succeeded (Record count:" + splitedDataSet.Count.ToString() + ").");
                            XMLTransformer transformer = XMLTransformer.CreateFromMessage(msg);
                            if (transformer == null)
                            {
                                Program.Log.Write(LogType.Warning, "Cannot find mapping XSL file for current message (XSL File:" + msg.XSLFileName + ").");
                            }
                            else
                            {
                                resultList = XIMClientHelper.Transform(transformer, splitedDataSet);
                                if (resultList == null || resultList.Count < 1)
                                {
                                    Program.Log.Write(LogType.Warning, "Transform from DataSet xml to XIM xml failed (XSL File:" + msg.XSLFileName + ").");
                                }
                                else
                                {
                                    Program.Log.Write(strPrefix + "Transform from DataSet xml to XIM xml succeeded (Document count:" + resultList.Count.ToString() + ").");
                                    if (resultList.Count > 1 && Program.ConfigMgt.Config.EnableDataMerging)
                                    {
                                        int pkIndex = Program.ConfigMgt.Config.DataMergingPKIndex;
                                        resultList = XIMClientHelper.MergeProcedureStep(resultList, pkIndex);
                                        if (resultList == null || resultList.Count < 1)
                                        {
                                            Program.Log.Write(LogType.Warning, "SPS merging failed (PK Index:" + pkIndex.ToString() + ").");
                                        }
                                        else
                                        {
                                            Program.Log.Write(strPrefix + "SPS merging succeeded (PK Index:" + pkIndex.ToString() + ") (Document count:" + resultList.Count.ToString() + ").");
                                        }
                                    }
                                    else
                                    {
                                        Program.Log.Write(strPrefix + "Skip SPS merging.");
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region Data Sending

                    if (resultList == null || resultList.Count < 1)
                    {
                        Program.Log.Write(LogType.Warning, "No availiable XIM document, skip data sending.");
                    }
                    else
                    {
                        int ximIndex = 1;
                        Program.Log.Write(strPrefix + "Sending XIM document...");
                        foreach (XIMWrapper xim in resultList)
                        {
                            string ximLogIndex = "(" + (ximIndex++).ToString() + "/" + resultList.Count.ToString() + ")";

                            bool result = false;
                            if (Program.ConfigMgt.Config.OutboundToFile)
                            {
                                result = !XIMClientHelper.SaveToFile(Program.ConfigMgt.Config, xim.XIMDocument);
                            }
                            else
                            {
                                SocketResult res = SocketHelper.SendData(Program.ConfigMgt.Config.SocketConfig, xim.XIMDocument);
                                result = (res == null || res.Type != SocketResultType.Success);
                            }

                            if (result)
                            {
                                Program.Log.Write(LogType.Warning, "Send XIM document failed. " + ximLogIndex);
                            }
                            else
                            {
                                Program.Log.Write(strPrefix, "Send XIM document succeeded. " + ximLogIndex);
                                if (_service.DischargeData(xim.GUIDList.ToArray()))
                                {
                                    Program.Log.Write(strPrefix, "Discharge data succeeded. " + ximLogIndex);
                                }
                                else
                                {
                                    Program.Log.Write(LogType.Warning, "Discharge data failed. " + ximLogIndex);
                                }
                            }
                        }
                        Program.Log.Write(strPrefix + "Send XIM document completed.");
                    }

                    #endregion

                    Program.Log.Write(strPrefix + "End processing message.", XIMClientHelper.ModuleName);
                }
                catch (Exception err)
                {
                    Program.Log.Write(err);
                }
            }

            Program.Log.Write("# Timer tick end.", XIMClientHelper.ModuleName);
            _timer.Start();
        }

        public bool IsRunning
        {
            get { return _timer.Enabled; }
        }
        public bool Start()
        {
            _timer.Start();
            Program.Log.Write("@ Timer start. (interval: " + _timer.Interval.ToString() + "ms)", XIMClientHelper.ModuleName);
            return true;
        }
        public bool Stop()
        {
            _timer.Stop();
            Program.Log.Write("@ Timer stop.", XIMClientHelper.ModuleName);
            return true;
        }
    }
}

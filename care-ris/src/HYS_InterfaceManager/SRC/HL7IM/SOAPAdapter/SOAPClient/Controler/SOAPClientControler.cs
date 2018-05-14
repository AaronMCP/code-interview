using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Objects;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPClient.Config;
using HYS.Common.Xml;
using HYS.IM.Common.Logging;
using System.IO;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPClient.Controler
{
    public partial class SOAPClientControler
    {
        private ProgramContext _context;
        public SOAPClientControler(ProgramContext context)
        {
            _context = context;
        }

        private void DumpSoapMessage(string fname, string soapMsg)
        {
            try
            {
                string pn = ConfigHelper.GetFullPath(Path.Combine(_context.AppArgument.ConfigFilePath, "Temp"));
                string fn = Path.Combine(pn, fname);

                _context.Log.Write("Dumping SOAP message to file: " + fn);

                if (!Directory.Exists(pn)) Directory.CreateDirectory(pn);
                using (StreamWriter sw = File.CreateText(fn))
                {
                    sw.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + soapMsg);
                }
            }
            catch (Exception e)
            {
                _context.Log.Write(e);
            }
        }
        private bool IsSoapFailureMessage(string rspMsg)
        {
            if (!_context.ConfigMgr.Config.ThrowExceptionWhenReceiveFailureResponse) return false;

            string val = XSLTExtension.GetValueByXPath(rspMsg,
                "/s:Envelope/s:Body/s:Fault", "s|http://schemas.xmlsoap.org/soap/envelope/");
            bool isFailure = val != null && val.Length > 0;

            if (isFailure) _context.Log.Write(LogType.Error, rspMsg);
            return isFailure;
        }

        public SOAPClientControlerProcessStatus ProcessSubscribedMessage(Message msg)
        {
            if (msg == null || msg.Header == null) return SOAPClientControlerProcessStatus.OtherError;

            string soapReq = "";
            string soapRsp = "";

            if (GenerateSOAPMessage(msg, out soapReq))
            {
                SOAPSender sender = new SOAPSender(
                _context.ConfigMgr.Config.GetWCFConfigFileNameWithFullPath(),
                _context.Log);

                if (sender.SendMessage(
                    _context.ConfigMgr.Config.SOAPServiceURI,
                    _context.ConfigMgr.Config.SOAPAction,
                    soapReq, ref soapRsp))
                {
                    if (_context.Log.DumpData)
                    {
                        string msgID = msg.Header.ID.ToString();
                        DumpSoapMessage(string.Format("s_{0}_soap_req.xml", msgID), soapReq);
                        DumpSoapMessage(string.Format("s_{0}_soap_rsp.xml", msgID), soapRsp);
                    }

                    if (IsSoapFailureMessage(soapRsp))
                        return SOAPClientControlerProcessStatus.RecevingFailureSOAPResponse;

                    return SOAPClientControlerProcessStatus.Success;
                }
                else
                {
                    return SOAPClientControlerProcessStatus.SendingSOAPMessageError;
                }
            }
            else
            {
                return SOAPClientControlerProcessStatus.GenerateSOAPMessageError;
            }
        }
        public SOAPClientControlerProcessStatus ProcessRequestingMessage(Message req, out Message rsp)
        {
            rsp = null;
            if (req == null || req.Header == null) return SOAPClientControlerProcessStatus.OtherError;

            string soapReq = "";
            string soapRsp = "";

            if (GenerateSOAPMessage(req, out soapReq))
            {
                SOAPSender sender = new SOAPSender(
                    _context.ConfigMgr.Config.GetWCFConfigFileNameWithFullPath(),
                    _context.Log);

                if (sender.SendMessage(
                    _context.ConfigMgr.Config.SOAPServiceURI,
                    _context.ConfigMgr.Config.SOAPAction,
                    soapReq, ref soapRsp))
                {
                    if (_context.Log.DumpData)
                    {
                        string msgID = req.Header.ID.ToString();
                        DumpSoapMessage(string.Format("r_{0}_soap_req.xml",msgID), soapReq);
                        DumpSoapMessage(string.Format("r_{0}_soap_rsp.xml", msgID), soapRsp);
                    }

                    if (IsSoapFailureMessage(soapRsp))
                        return SOAPClientControlerProcessStatus.RecevingFailureSOAPResponse;

                    if (GenerateXDSGWMessage(soapRsp, out rsp))
                    {
                        return SOAPClientControlerProcessStatus.Success;
                    }
                    else
                    {
                        return SOAPClientControlerProcessStatus.GenerateXDSGWMessageError;
                    }
                }
                else
                {
                    return SOAPClientControlerProcessStatus.SendingSOAPMessageError;
                }
            }
            else
            {
                return SOAPClientControlerProcessStatus.GenerateSOAPMessageError;
            }
        }
    }

    public enum SOAPClientControlerProcessStatus
    {
        Success,
        GenerateSOAPMessageError,
        SendingSOAPMessageError,
        RecevingFailureSOAPResponse,
        GenerateXDSGWMessageError,
        OtherError,
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;
using HYS.IM.Messaging.Objects;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Config;
using System.IO;
using HYS.IM.Common.Logging;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Adapters;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Base;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Controler
{
    public partial class SOAPServerControler
    {
        private ProgramContext _context;
        public SOAPServerControler(ProgramContext context)
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

        public bool ProcessSoapSession(SOAPReceiverSession session, MessageDispatchModel dspModel, EntityImpl entity)
        {
            if (session == null ||
                session.Status != SOAPReceiverSessionStatus.IncomingSOAPEnvelopeReceived) return false;

            if (_context.Log.DumpData)
                DumpSoapMessage(string.Format("{0}_soap_req.xml", session.SessionID), session.IncomingSOAPEnvelope);

            string incomingMsgXml = "";
            if (GenerateXDSGWMessage(session.IncomingSOAPEnvelope, out incomingMsgXml))
            {
                session.IncomingMessageXml = incomingMsgXml;
                if (DispatchXDSGWMessage(session, dspModel, entity) && session != null
                    && session.Status == SOAPReceiverSessionStatus.OutgoingMessageReceived)
                {
                    string outgoingSoapEnvelope = "";
                    if (GenerateSOAPMessage(session.OutgoingMessage, out outgoingSoapEnvelope))
                    {
                        session.OutgoingSOAPEnvelope = outgoingSoapEnvelope;

                        if (_context.Log.DumpData)
                            DumpSoapMessage(string.Format("{0}_soap_rsp.xml", session.SessionID), session.OutgoingSOAPEnvelope);

                        return true;
                    }
                }
            }

            return false;
        }
    }
}

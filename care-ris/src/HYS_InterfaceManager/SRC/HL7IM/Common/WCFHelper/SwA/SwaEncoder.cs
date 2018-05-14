using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using HYS.IM.Common.WCFHelper.SwA.Mime;
using System.Diagnostics;
using System.ServiceModel;
using System.IO;
using System.Xml;
using System.ServiceModel.Web;

namespace HYS.IM.Common.WCFHelper.SwA
{
    public class SwaEncoder : MessageEncoder
    {
        private string _ContentType;
        private string _MediaType;

        protected MimeContent _MyContent;
        protected MimePart _SoapMimeContent;
        //protected MimePart _AttachmentMimeContent;

        protected readonly MimeParser _MimeParser;
        protected readonly SwaEncoderFactory _Factory;
        protected readonly MessageEncoder _InnerEncoder;

        public SwaEncoder(MessageEncoder innerEncoder, SwaEncoderFactory factory)
        {
            //
            // Initialize general fields
            //
            _ContentType = "multipart/related";
            _MediaType = _ContentType;

            //
            // Create owned objects
            //
            _Factory = factory;
            _InnerEncoder = innerEncoder;
            _MimeParser = new MimeParser();

            //
            // Create object for the mime content message
            // 
            _SoapMimeContent = new MimePart()
            {
                ContentType = "text/xml",
                ContentId = "<EFD659EE6BD5F31EA7BC0D59403AF049>",   // TODO: make content id dynamic or configurable?
                CharSet = "UTF-8",                                  // TODO: make charset configurable?
                TransferEncoding = "binary"                         // TODO: make transfer-encoding configurable?
            };
            //_AttachmentMimeContent = new MimePart()
            //{
            //    ContentType = "application/zip",                    // TODO: AttachmentMimeContent.ContentType configurable?
            //    ContentId = "<UZE_26123_>",                         // TODO: AttachmentMimeContent.ContentId configurable/dynamic?
            //    TransferEncoding = "binary"                         // TODO: AttachmentMimeContent.TransferEncoding dynamic/configurable?
            //};
            _MyContent = new MimeContent()
            {
                Boundary = "------=_Part_0_21714745.1249640163820"  // TODO: MimeContent.Boundary configurable/dynamic?
            };
            _MyContent.Parts.Add(_SoapMimeContent);
            //_MyContent.Parts.Add(_AttachmentMimeContent);
            _MyContent.SetAsStartPart(_SoapMimeContent);
        }

        public override string ContentType
        {
            get
            {
                VerifyOperationContext();

                if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(SwaEncoderConstants.AttachmentProperty))
                    return _MyContent.ContentType;
                else
                    return _InnerEncoder.ContentType;
            }
        }

        public override string MediaType
        {
            get { return _MediaType; }
        }

        public override MessageVersion MessageVersion
        {
            get { return _InnerEncoder.MessageVersion; }
            //get { return MessageVersion.Soap11WSAddressing10; }
            //get { return MessageVersion.Soap11; }
            //get { return MessageVersion.Soap12; }
        }

        public override bool IsContentTypeSupported(string contentType)
        {
            if (contentType.ToLower().StartsWith("multipart/related"))
                return true;
            else if (contentType.ToLower().StartsWith("text/xml"))
                return true;
            else
                return false;
        }

        public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
        {
            try
            {
                VerifyOperationContext();

                //
                // Verify the content type
                //
                byte[] MsgContents = new byte[buffer.Count];
                Array.Copy(buffer.Array, buffer.Offset, MsgContents, 0, MsgContents.Length);
                bufferManager.ReturnBuffer(buffer.Array);

                // Debug code
#if DEBUG
                string Contents = Encoding.UTF8.GetString(MsgContents);
                Debug.WriteLine("-------------------");
                Debug.WriteLine(Contents);
                Debug.WriteLine("-------------------");
#endif

                MemoryStream ms = new MemoryStream(MsgContents);
                return ReadMessage(ms, int.MaxValue, contentType);
            }
            catch (Exception e)
            {
                SwaLogMgt.SetLog(this, e);
                //Console.WriteLine(e.ToString());
                return null;
            }
        }

        public override Message ReadMessage(System.IO.Stream stream, int maxSizeOfHeaders, string contentType)
        {
            try
            {
                VerifyOperationContext();

                if (contentType.ToLower().StartsWith("multipart/related"))
                {
                    byte[] ContentBytes = new byte[stream.Length];
                    stream.Read(ContentBytes, 0, ContentBytes.Length);
                    MimeContent Content = _MimeParser.DeserializeMimeContent(contentType, ContentBytes);

                    if (Content.Parts.Count >= 2)
                    {
                        MemoryStream ms = new MemoryStream(Content.Parts[0].Content);
                        Message Msg = ReadMessage(ms, int.MaxValue, Content.Parts[0].ContentType);
                        //Msg.Properties.Add(SwaEncoderConstants.AttachmentProperty, Content.Parts[1].Content);

                        List<SwaAttachment> alist = new List<SwaAttachment>();
                        for (int i = 1; i < Content.Parts.Count; i++)
                        {
                            MimePart part = Content.Parts[i];
                            SwaAttachment attachement = new SwaAttachment();
                            attachement.ContentBinary = part.Content;
                            attachement.ContentType = part.ContentType;
                            attachement.ContentId = part.ContentId;
                            alist.Add(attachement);
                        }
                        Msg.Properties.Add(SwaEncoderConstants.AttachmentProperty, alist);

                        return Msg;
                    }
                    else
                    {
                        throw new ApplicationException("Invalid mime message sent! Soap with attachments makes sense, only, with at least 2 mime message content parts!");
                    }
                }
                else if (contentType.ToLower().StartsWith("text/xml"))
                {
                    XmlReader Reader = XmlReader.Create(stream);
                    return Message.CreateMessage(Reader, maxSizeOfHeaders, MessageVersion);
                }
                else
                {
                    throw new ApplicationException(
                        string.Format(
                            "Invalid content type for reading message: {0}! Supported content types are multipart/related and text/xml!",
                            contentType));
                }
            }
            catch (Exception e)
            {
                SwaLogMgt.SetLog(this, e);
                //Console.WriteLine(e.ToString());
                return null;
            }
        }

        public override void WriteMessage(Message message, System.IO.Stream stream)
        {
            try
            {
                VerifyOperationContext();

                message.Properties.Encoder = this._InnerEncoder;

                List<SwaAttachment> attachmentList = null;
                if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(SwaEncoderConstants.AttachmentProperty))
                    attachmentList = OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentProperty] as List<SwaAttachment>;

                if (attachmentList == null)
                {
                    _InnerEncoder.WriteMessage(message, stream);
                }
                else
                {
                    // Associate the contents to the mime-part
                    _SoapMimeContent.Content = Encoding.UTF8.GetBytes(message.GetBody<string>());
                    //_AttachmentMimeContent.Content = (byte[])OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentProperty];

                    foreach (SwaAttachment attachment in attachmentList)
                    {
                        MimePart attachmentPart = new MimePart();
                        attachmentPart.Content = GetBase64EncodingContent(attachment.ContentBinary);
                        attachmentPart.ContentType = attachment.ContentType;
                        attachmentPart.ContentId = attachment.ContentId;
                        attachmentPart.TransferEncoding = "base64";
                        _MyContent.Parts.Add(attachmentPart);
                    }

                    // Now create the message content for the stream
                    _MimeParser.SerializeMimeContent(_MyContent, stream);
                }
            }
            catch (Exception e)
            {
                SwaLogMgt.SetLog(this, e);
                //Console.WriteLine(e.ToString());
            }
        }

        public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
        {
            try
            {
                VerifyOperationContext();

                message.Properties.Encoder = this._InnerEncoder;

                List<SwaAttachment> attachmentList = null;
                if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(SwaEncoderConstants.AttachmentProperty))
                    attachmentList = OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentProperty] as List<SwaAttachment>;

                if (attachmentList == null)
                {
                    #region 20100310 the following code is to support SOAP Outbound Adapter (HYS.IMSOAPAdapter.SOAPClient)

                    if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(SwaEncoderConstants.SoapEnvelopeProperty))
                    {
                        string soapEnvelopeString = (string)OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.SoapEnvelopeProperty];
                        byte[] SoapContentBytes = Encoding.UTF8.GetBytes(soapEnvelopeString);
                        int SoapContentLength = SoapContentBytes.Length;

                        // Write the mime content into the section of the buffer passed into the method
                        byte[] TargetBuffer = bufferManager.TakeBuffer(SoapContentLength + messageOffset);
                        Array.Copy(SoapContentBytes, 0, TargetBuffer, messageOffset, SoapContentLength);

                        // Return the segment of the buffer to the framework
                        ArraySegment<byte> ret = new ArraySegment<byte>(TargetBuffer, messageOffset, SoapContentLength);

                        // Debug code
//#if DEBUG
//                        byte[] MsgContents = new byte[ret.Count];
//                        Array.Copy(ret.Array, ret.Offset, MsgContents, 0, MsgContents.Length);
//                        string Contents = Encoding.UTF8.GetString(MsgContents);
//                        Debug.WriteLine("-------------------");
//                        Debug.WriteLine(Contents);
//                        Debug.WriteLine("-------------------");
//#endif

                        return ret;
                    }

                    #endregion

                    return _InnerEncoder.WriteMessage(message, maxMessageSize, bufferManager, messageOffset);
                }
                else
                {
                    if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(SwaEncoderConstants.SoapEnvelopeProperty))
                    {
                        string soapEnvelopeString = (string)OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.SoapEnvelopeProperty];
                        _SoapMimeContent.Content = Encoding.UTF8.GetBytes(soapEnvelopeString);
                    }
                    else
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            XmlWriterSettings xws = new XmlWriterSettings();
                            xws.Encoding = Encoding.UTF8;
                            using (XmlWriter xw = XmlWriter.Create(ms))
                            {
                                message.WriteMessage(xw);
                                xw.Close();
                            }
                            ms.Close();
                            _SoapMimeContent.Content = ms.GetBuffer();
                        }
                    }

                    foreach (SwaAttachment attachment in attachmentList)
                    {
                        MimePart attachmentPart = new MimePart();
                        attachmentPart.Content = GetBase64EncodingContent(attachment.ContentBinary);
                        attachmentPart.ContentType = attachment.ContentType;
                        attachmentPart.ContentId = attachment.ContentId;
                        attachmentPart.TransferEncoding = "base64";
                        _MyContent.Parts.Add(attachmentPart);
                    }

                    // Now create the message content for the stream
                    byte[] MimeContentBytes = _MimeParser.SerializeMimeContent(_MyContent);
                    int MimeContentLength = MimeContentBytes.Length;

                    // Write the mime content into the section of the buffer passed into the method
                    byte[] TargetBuffer = bufferManager.TakeBuffer(MimeContentLength + messageOffset);
                    Array.Copy(MimeContentBytes, 0, TargetBuffer, messageOffset, MimeContentLength);

                    // Return the segment of the buffer to the framework
                    ArraySegment<byte> ret = new ArraySegment<byte>(TargetBuffer, messageOffset, MimeContentLength);
         
                    // Debug code
#if DEBUG
                    byte[] MsgContents = new byte[ret.Count];
                    Array.Copy(ret.Array, ret.Offset, MsgContents, 0, MsgContents.Length);
                    string Contents = Encoding.UTF8.GetString(MsgContents);
                    Debug.WriteLine("-------------------");
                    Debug.WriteLine(Contents);
                    Debug.WriteLine("-------------------");
#endif

                    return ret;
                }
            }
            catch (Exception e)
            {
                SwaLogMgt.SetLog(this, e);
                //Console.WriteLine(e.ToString());
                return new ArraySegment<byte>();
            }
        }

        private byte[] GetBase64EncodingContent(byte[] binaryBuffer)
        {
            //byte[] buffer = (byte[])OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentProperty];
            byte[] ret = Encoding.ASCII.GetBytes(Convert.ToBase64String(binaryBuffer));
            return ret;
        }

        private void VerifyOperationContext()
        {
            if (OperationContext.Current == null)
            {
                ApplicationException e = new ApplicationException
                (
                    "No current OperationContext available! On clients please use OperationScope as follows to establish " +
                    "an operation context: " + Environment.NewLine + Environment.NewLine +
                    "using(OperationScope Scope = new OperationScope(YourProxy.InnerChannel) { YouProxy.MethodCall(...); }"
                );

                SwaLogMgt.SetLog(this, e);

                // This kind of error should be resolved at design/unit-testing time, so throw exception here.
                throw e;
//#if DEBUG
//                Debug.WriteLine("No current OperationContext available!");
//#endif
            }
            else if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(SwaEncoderConstants.AttachmentProperty))
            {
                if (OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentProperty] != null)
                {
                    if (!(OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentProperty] is List<SwaAttachment>))
                    {
                        ArgumentException e = new ArgumentException(string.Format(
                            "OperationContext.Current.OutgoingMessageProperties[\"{0}\"] needs to be a List<SwaAttachment> with the attachment content!",
                                SwaEncoderConstants.AttachmentProperty));

                        SwaLogMgt.SetLog(this, e);

                        // This kind of error should be resolved at design/unit-testing time, so throw exception here.
                        throw e;
//#if DEBUG
//                        Debug.WriteLine(string.Format(
//                            "OperationContext.Current.OutgoingMessageProperties[\"{0}\"] needs to be a List<SwaAttachment> with the attachment content!",
//                                SwaEncoderConstants.AttachmentProperty));
//#endif
                    }
                }
            }
        }
    }
}

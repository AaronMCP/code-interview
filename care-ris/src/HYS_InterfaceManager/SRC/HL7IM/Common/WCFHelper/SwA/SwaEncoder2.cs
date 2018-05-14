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
    #region SwaEncoder support only one attachment
//    public class SwaEncoder : MessageEncoder
//    {
//        private string _ContentType;
//        private string _MediaType;

//        protected MimeContent _MyContent;
//        protected MimePart _SoapMimeContent;
//        protected MimePart _AttachmentMimeContent;

//        protected readonly MimeParser _MimeParser;
//        protected readonly SwaEncoderFactory _Factory;
//        protected readonly MessageEncoder _InnerEncoder;

//        public SwaEncoder(MessageEncoder innerEncoder, SwaEncoderFactory factory)
//        {
//            //
//            // Initialize general fields
//            //
//            _ContentType = "multipart/related";
//            _MediaType = _ContentType;

//            //
//            // Create owned objects
//            //
//            _Factory = factory;
//            _InnerEncoder = innerEncoder;
//            _MimeParser = new MimeParser();

//            //
//            // Create object for the mime content message
//            // 
//            _SoapMimeContent = new MimePart()
//            {
//                ContentType = "text/xml",
//                ContentId = "<EFD659EE6BD5F31EA7BC0D59403AF049>",   // TODO: make content id dynamic or configurable?
//                CharSet = "UTF-8",                                  // TODO: make charset configurable?
//                TransferEncoding = "binary"                         // TODO: make transfer-encoding configurable?
//            };
//            _AttachmentMimeContent = new MimePart()
//            {
//                ContentType = "application/zip",                    // TODO: AttachmentMimeContent.ContentType configurable?
//                ContentId = "<UZE_26123_>",                         // TODO: AttachmentMimeContent.ContentId configurable/dynamic?
//                TransferEncoding = "binary"                         // TODO: AttachmentMimeContent.TransferEncoding dynamic/configurable?
//            };
//            _MyContent = new MimeContent()
//            {
//                Boundary = "------=_Part_0_21714745.1249640163820"  // TODO: MimeContent.Boundary configurable/dynamic?
//            };
//            _MyContent.Parts.Add(_SoapMimeContent);
//            _MyContent.Parts.Add(_AttachmentMimeContent);
//            _MyContent.SetAsStartPart(_SoapMimeContent);
//        }

//        public override string ContentType
//        {
//            get
//            {
//                VerifyOperationContext();

//                if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(SwaEncoderConstants.AttachmentProperty))
//                    return _MyContent.ContentType;
//                else
//                    return _InnerEncoder.ContentType;
//            }
//        }

//        public override string MediaType
//        {
//            get { return _MediaType; }
//        }

//        public override MessageVersion MessageVersion
//        {
//            get { return _InnerEncoder.MessageVersion; }
//            //get { return MessageVersion.Soap11WSAddressing10; }
//            //get { return MessageVersion.Soap11; }
//            //get { return MessageVersion.Soap12; }
//        }

//        public override bool IsContentTypeSupported(string contentType)
//        {
//            if (contentType.ToLower().StartsWith("multipart/related"))
//                return true;
//            else if (contentType.ToLower().StartsWith("text/xml"))
//                return true;
//            else
//                return false;
//        }

//        public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
//        {
//            try
//            {
//                VerifyOperationContext();

//                //
//                // Verify the content type
//                //
//                byte[] MsgContents = new byte[buffer.Count];
//                Array.Copy(buffer.Array, buffer.Offset, MsgContents, 0, MsgContents.Length);
//                bufferManager.ReturnBuffer(buffer.Array);

//                // Debug code
//#if DEBUG
//                string Contents = Encoding.UTF8.GetString(MsgContents);
//                Debug.WriteLine("-------------------");
//                Debug.WriteLine(Contents);
//                Debug.WriteLine("-------------------");
//#endif

//                MemoryStream ms = new MemoryStream(MsgContents);
//                return ReadMessage(ms, int.MaxValue, contentType);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//                return null;
//            }
//        }

//        public override Message ReadMessage(System.IO.Stream stream, int maxSizeOfHeaders, string contentType)
//        {
//            try
//            {
//                VerifyOperationContext();

//                if (contentType.ToLower().StartsWith("multipart/related"))
//                {
//                    byte[] ContentBytes = new byte[stream.Length];
//                    stream.Read(ContentBytes, 0, ContentBytes.Length);
//                    MimeContent Content = _MimeParser.DeserializeMimeContent(contentType, ContentBytes);

//                    if (Content.Parts.Count >= 2)
//                    {
//                        MemoryStream ms = new MemoryStream(Content.Parts[0].Content);
//                        Message Msg = ReadMessage(ms, int.MaxValue, Content.Parts[0].ContentType);
//                        Msg.Properties.Add(SwaEncoderConstants.AttachmentProperty, Content.Parts[1].Content);
//                        return Msg;
//                    }
//                    else
//                    {
//                        throw new ApplicationException("Invalid mime message sent! Soap with attachments makes sense, only, with at least 2 mime message content parts!");
//                    }
//                }
//                else if (contentType.ToLower().StartsWith("text/xml"))
//                {
//                    XmlReader Reader = XmlReader.Create(stream);
//                    return Message.CreateMessage(Reader, maxSizeOfHeaders, MessageVersion);
//                }
//                else
//                {
//                    throw new ApplicationException(
//                        string.Format(
//                            "Invalid content type for reading message: {0}! Supported content types are multipart/related and text/xml!",
//                            contentType));
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//                return null;
//            }
//        }

//        public override void WriteMessage(Message message, System.IO.Stream stream)
//        {
//            try
//            {
//                VerifyOperationContext();

//                message.Properties.Encoder = this._InnerEncoder;

//                byte[] Attachment = null;
//                if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(SwaEncoderConstants.AttachmentProperty))
//                    Attachment = (byte[])OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentProperty];

//                if (Attachment == null)
//                {
//                    _InnerEncoder.WriteMessage(message, stream);
//                }
//                else
//                {
//                    // Associate the contents to the mime-part
//                    _SoapMimeContent.Content = Encoding.UTF8.GetBytes(message.GetBody<string>());
//                    _AttachmentMimeContent.Content = (byte[])OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentProperty];

//                    // Now create the message content for the stream
//                    _MimeParser.SerializeMimeContent(_MyContent, stream);
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//            }
//        }

//        public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
//        {
//            try
//            {
//                VerifyOperationContext();

//                message.Properties.Encoder = this._InnerEncoder;

//                byte[] Attachment = null;
//                if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(SwaEncoderConstants.AttachmentProperty))
//                    Attachment = (byte[])OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentProperty];

//                if (Attachment == null)
//                {
//                    return _InnerEncoder.WriteMessage(message, maxMessageSize, bufferManager, messageOffset);
//                }
//                else
//                {
//                    if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(SwaEncoderConstants.SoapEnvelopeProperty))
//                    {
//                        string soapEnvelopeString = (string)OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.SoapEnvelopeProperty];
//                        _SoapMimeContent.Content = Encoding.UTF8.GetBytes(soapEnvelopeString);
//                    }
//                    else
//                    {
//                        using (MemoryStream ms = new MemoryStream())
//                        {
//                            XmlWriterSettings xws = new XmlWriterSettings();
//                            xws.Encoding = Encoding.UTF8;
//                            using (XmlWriter xw = XmlWriter.Create(ms))
//                            {
//                                message.WriteMessage(xw);
//                                xw.Close();
//                            }
//                            ms.Close();
//                            _SoapMimeContent.Content = ms.GetBuffer();
//                        }
//                    }

//                    _AttachmentMimeContent.Content = GetBase64EncodingContent();
//                    _AttachmentMimeContent.ContentId = GetContentID();
//                    _AttachmentMimeContent.ContentType = GetContentType();
//                    _AttachmentMimeContent.TransferEncoding = "base64";

//                    // Now create the message content for the stream
//                    byte[] MimeContentBytes = _MimeParser.SerializeMimeContent(_MyContent);
//                    int MimeContentLength = MimeContentBytes.Length;

//                    // Write the mime content into the section of the buffer passed into the method
//                    byte[] TargetBuffer = bufferManager.TakeBuffer(MimeContentLength + messageOffset);
//                    Array.Copy(MimeContentBytes, 0, TargetBuffer, messageOffset, MimeContentLength);

//                    // Return the segment of the buffer to the framework
//                    ArraySegment<byte> ret = new ArraySegment<byte>(TargetBuffer, messageOffset, MimeContentLength);

//                    // Debug code
//#if DEBUG
//                    byte[] MsgContents = new byte[ret.Count];
//                    Array.Copy(ret.Array, ret.Offset, MsgContents, 0, MsgContents.Length);
//                    string Contents = Encoding.UTF8.GetString(MsgContents);
//                    Debug.WriteLine("-------------------");
//                    Debug.WriteLine(Contents);
//                    Debug.WriteLine("-------------------");
//#endif


//                    return ret;
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//                return new ArraySegment<byte>();
//            }
//        }


//        private string GetContentID()
//        {
//            if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(SwaEncoderConstants.AttachmentContentIdProperty))
//            {
//                return OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentContentIdProperty] as string;
//            }
//            else
//            {
//                return "[NULL]";
//            }
//        }
//        private string GetContentType()
//        {
//            if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(SwaEncoderConstants.AttachmentContentTypeProperty))
//            {
//                return OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentContentTypeProperty] as string;
//            }
//            else
//            {
//                return "[NULL]";
//            }
//        }
//        private byte[] GetBase64EncodingContent()
//        {
//            byte[] buffer = (byte[])OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentProperty];
//            byte[] ret = Encoding.ASCII.GetBytes(Convert.ToBase64String(buffer));
//            return ret;
//        }

//        private void VerifyOperationContext()
//        {
//            if (OperationContext.Current == null)
//            {
//                //throw new ApplicationException
//                //(
//                //    "No current OperationContext available! On clients please use OperationScope as follows to establish " +
//                //    "an operation context: " + Environment.NewLine + Environment.NewLine +
//                //    "using(OperationScope Scope = new OperationScope(YourProxy.InnerChannel) { YouProxy.MethodCall(...); }"
//                //);
//#if DEBUG
//                Debug.WriteLine("No current OperationContext available!");
//#endif
//            }
//            else if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(SwaEncoderConstants.AttachmentProperty))
//            {
//                if (OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentProperty] != null)
//                {
//                    if (!(OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentProperty] is byte[]))
//                    {
//                        //throw new ArgumentException(string.Format(
//                        //    "OperationContext.Current.OutgoingMessageProperties[\"{0}\"] needs to be a byte[] array with the attachment content!",
//                        //        SwaEncoderConstants.AttachmentProperty));
//#if DEBUG
//                        Debug.WriteLine(string.Format(
//                            "OperationContext.Current.OutgoingMessageProperties[\"{0}\"] needs to be a byte[] array with the attachment content!",
//                                SwaEncoderConstants.AttachmentProperty));
//#endif
//                    }
//                }
//            }
//        }
//    }
    #endregion

    #region SwaEncoder for the first prototype for integration test with System 5
    //    public class SwaEncoder : MessageEncoder
//    {
//        private string _ContentType;
//        private string _MediaType;

//        protected MimeContent _MyContent;
//        protected MimePart _SoapMimeContent;
//        protected MimePart _AttachmentMimeContent;

//        protected readonly MimeParser _MimeParser;
//        protected readonly SwaEncoderFactory _Factory;
//        protected readonly MessageEncoder _InnerEncoder;

//        public SwaEncoder(MessageEncoder innerEncoder, SwaEncoderFactory factory)
//        {
//            //
//            // Initialize general fields
//            //
//            _ContentType = "multipart/related";
//            _MediaType = _ContentType;

//            //
//            // Create owned objects
//            //
//            _Factory = factory;
//            _InnerEncoder = innerEncoder;
//            _MimeParser = new MimeParser();

//            //
//            // Create object for the mime content message
//            // 
//            _SoapMimeContent = new MimePart()
//            {
//                ContentType = "text/xml",
//                ContentId = "<EFD659EE6BD5F31EA7BC0D59403AF049>",   // TODO: make content id dynamic or configurable?
//                CharSet = "UTF-8",                                  // TODO: make charset configurable?
//                TransferEncoding = "binary"                         // TODO: make transfer-encoding configurable?
//            };
//            _AttachmentMimeContent = new MimePart()
//            {
//                ContentType = "image/jpeg",                         // TODO: AttachmentMimeContent.ContentType configurable?
//                ContentId = "<ALG_DOC_PID_PID0005>",                // TODO: AttachmentMimeContent.ContentId configurable/dynamic?
//                TransferEncoding = "base64"                         // TODO: AttachmentMimeContent.TransferEncoding dynamic/configurable?
//            };
//            _MyContent = new MimeContent()
//            {
//                Boundary = "------=_Part_0_21714745.1249640163820"  // TODO: MimeContent.Boundary configurable/dynamic?
//            };
//            _MyContent.Parts.Add(_SoapMimeContent);
//            _MyContent.Parts.Add(_AttachmentMimeContent);
//            _MyContent.SetAsStartPart(_SoapMimeContent);
//        }

//        public override string ContentType
//        {
//            get
//            {
//                VerifyOperationContext();

//                if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(SwaEncoderConstants.AttachmentProperty))
//                    return _MyContent.ContentType;
//                else
//                    return _InnerEncoder.ContentType;
//            }
//        }

//        public override string MediaType
//        {
//            get { return _MediaType; }
//        }

//        public override MessageVersion MessageVersion
//        {
//            get { return MessageVersion.Soap11; }
//            //get { return MessageVersion.Soap12; }
//        }

//        public override bool IsContentTypeSupported(string contentType)
//        {
//            if (contentType.ToLower().StartsWith("multipart/related"))
//                return true;
//            else if (contentType.ToLower().StartsWith("text/xml"))
//                return true;
//            else
//                return false;
//        }

//        public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
//        {
//            try
//            {
//                VerifyOperationContext();

//                //
//                // Verify the content type
//                //
//                byte[] MsgContents = new byte[buffer.Count];
//                Array.Copy(buffer.Array, buffer.Offset, MsgContents, 0, MsgContents.Length);
//                bufferManager.ReturnBuffer(buffer.Array);

//                // Debug code
//#if DEBUG
//                string Contents = Encoding.UTF8.GetString(MsgContents);
//                Debug.WriteLine("-------------------");
//                Debug.WriteLine(Contents);
//                Debug.WriteLine("-------------------");
//#endif

//                MemoryStream ms = new MemoryStream(MsgContents);
//                return ReadMessage(ms, int.MaxValue, contentType);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//                return null;
//            }
//        }

//        public override Message ReadMessage(System.IO.Stream stream, int maxSizeOfHeaders, string contentType)
//        {
//            try
//            {
//                VerifyOperationContext();

//                if (contentType.ToLower().StartsWith("multipart/related"))
//                {
//                    byte[] ContentBytes = new byte[stream.Length];
//                    stream.Read(ContentBytes, 0, ContentBytes.Length);
//                    MimeContent Content = _MimeParser.DeserializeMimeContent(contentType, ContentBytes);

//                    if (Content.Parts.Count >= 2)
//                    {
//                        MemoryStream ms = new MemoryStream(Content.Parts[0].Content);
//                        Message Msg = ReadMessage(ms, int.MaxValue, Content.Parts[0].ContentType);
//                        Msg.Properties.Add(SwaEncoderConstants.AttachmentProperty, Content.Parts[1].Content);
//                        return Msg;
//                    }
//                    else
//                    {
//                        throw new ApplicationException("Invalid mime message sent! Soap with attachments makes sense, only, with at least 2 mime message content parts!");
//                    }
//                }
//                else if (contentType.ToLower().StartsWith("text/xml"))
//                {
//                    XmlReader Reader = XmlReader.Create(stream);
//                    return Message.CreateMessage(Reader, maxSizeOfHeaders, MessageVersion);
//                }
//                else
//                {
//                    throw new ApplicationException(
//                        string.Format(
//                            "Invalid content type for reading message: {0}! Supported content types are multipart/related and text/xml!",
//                            contentType));
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//                return null;
//            }
//        }

//        public override void WriteMessage(Message message, System.IO.Stream stream)
//        {
//            try
//            {
//                VerifyOperationContext();

//                message.Properties.Encoder = this._InnerEncoder;

//                byte[] Attachment = null;
//                if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(SwaEncoderConstants.AttachmentProperty))
//                    Attachment = (byte[])OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentProperty];

//                if (Attachment == null)
//                {
//                    _InnerEncoder.WriteMessage(message, stream);
//                }
//                else
//                {
//                    // Associate the contents to the mime-part
//                    _SoapMimeContent.Content = Encoding.UTF8.GetBytes(message.GetBody<string>());
//                    _AttachmentMimeContent.Content = (byte[])OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentProperty];

//                    // Now create the message content for the stream
//                    _MimeParser.SerializeMimeContent(_MyContent, stream);
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//            }
//        }

//        public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
//        {
//            try
//            {
//                VerifyOperationContext();

//                message.Properties.Encoder = this._InnerEncoder;

//                byte[] Attachment = null;
//                if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(SwaEncoderConstants.AttachmentProperty))
//                    Attachment = (byte[])OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentProperty];

//                if (Attachment == null)
//                {
//                    return _InnerEncoder.WriteMessage(message, maxMessageSize, bufferManager, messageOffset);
//                }
//                else
//                {
//                    // Associate the contents to the mime-part
//                    //_SoapMimeContent.Content = Encoding.UTF8.GetBytes(message.ToString());

//                    _SoapMimeContent.Content = File.ReadAllBytes(@"..\..\..\xdsa_submit_doc_envelop_for_IHE.xml");
//                    //_SoapMimeContent.Content = File.ReadAllBytes(@"..\..\..\xdsa_submit_doc_envelop_for_CSP.xml");
//                    //_AttachmentMimeContent.Content = (byte[])OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentProperty];

//                    _AttachmentMimeContent.Content = Encoding.ASCII.GetBytes(Convert.ToBase64String(File.ReadAllBytes(@"..\..\..\9229846_b.jpg")));

//                    // Now create the message content for the stream
//                    byte[] MimeContentBytes = _MimeParser.SerializeMimeContent(_MyContent);
//                    int MimeContentLength = MimeContentBytes.Length;
                    
//                    // Write the mime content into the section of the buffer passed into the method
//                    byte[] TargetBuffer = bufferManager.TakeBuffer(MimeContentLength + messageOffset);
//                    Array.Copy(MimeContentBytes, 0, TargetBuffer, messageOffset, MimeContentLength);

//                    // Return the segment of the buffer to the framework
//                    return new ArraySegment<byte>(TargetBuffer, messageOffset, MimeContentLength);
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.ToString());
//                return new ArraySegment<byte>();
//            }
//        }

//        private void VerifyOperationContext()
//        {
//            if (OperationContext.Current == null)
//            {
//                //throw new ApplicationException
//                //(
//                //    "No current OperationContext available! On clients please use OperationScope as follows to establish " +
//                //    "an operation context: " + Environment.NewLine + Environment.NewLine +
//                //    "using(OperationScope Scope = new OperationScope(YourProxy.InnerChannel) { YouProxy.MethodCall(...); }"
//                //);
//#if DEBUG
//                Debug.WriteLine("No current OperationContext available!");
//#endif
//            }
//            else if (OperationContext.Current.OutgoingMessageProperties.ContainsKey(SwaEncoderConstants.AttachmentProperty))
//            {
//                if (OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentProperty] != null)
//                {
//                    if (!(OperationContext.Current.OutgoingMessageProperties[SwaEncoderConstants.AttachmentProperty] is byte[]))
//                    {
//                        //throw new ArgumentException(string.Format(
//                        //    "OperationContext.Current.OutgoingMessageProperties[\"{0}\"] needs to be a byte[] array with the attachment content!",
//                        //        SwaEncoderConstants.AttachmentProperty));
//#if DEBUG
//                        Debug.WriteLine(string.Format(
//                            "OperationContext.Current.OutgoingMessageProperties[\"{0}\"] needs to be a byte[] array with the attachment content!",
//                                SwaEncoderConstants.AttachmentProperty));
//#endif
//                    }
//                }
//            }
//        }
    //    }
    #endregion
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.MessageDevices.SOAPAdapter.Test.ServiceReference1;
using System.ServiceModel;
using System.IO;
using System.Xml;
using HYS.Common.WCFHelper;
using System.ServiceModel.Channels;
using HYS.Common.WCFHelper.SwA;

namespace HYS.MessageDevices.SOAPAdapter.Test.Forms
{
    public partial class FormSOAPClient : Form
    {
        public FormSOAPClient()
        {
            InitializeComponent();
        }

        private void buttonValueCall_Click(object sender, EventArgs e)
        {
            string msgIn = this.textBoxValueSnd.Text;
            string msgOut = "";

            try
            {
                PIXServiceSoapClient client = new PIXServiceSoapClient();
                int ret = client.MessageCom(msgIn, out msgOut);

                this.textBoxValueRcv.Text = msgOut;
                MessageBox.Show(this, "Return value: " + ret.ToString(), this.Text);
            }
            catch (Exception err)
            {
                MessageBox.Show(this, err.ToString(), this.Text);
            }
        }

        private void buttonMsgCall_Click(object sender, EventArgs e)
        {
            string uri = this.textBoxMsgURI.Text.Trim();
            string action = this.textBoxMsgAction.Text.Trim();
            string request = this.textBoxMsgSnd.Text.Trim();

            System.ServiceModel.Channels.Message wcfRequest = null;
            System.ServiceModel.Channels.Message wcfResponse = null;

            try
            {
                ChannelFactory<IAbstractContract> factory = new ChannelFactory<IAbstractContract>("ABSTRACT_CLIENT_ENDPOINT");

                IAbstractContract proxy = factory.CreateChannel(new EndpointAddress(uri));
                using (proxy as IDisposable)
                {
                    using (OperationContextScope sc = new OperationContextScope(proxy as IContextChannel))
                    {
                        using (wcfRequest = SoapMessageHelper.CreateEmptyWCFMessage(
                            SoapEnvelopeVersion.Soap11, 
                            WSAddressingVersion.None,
                            action))
                        {
                            //List<SwaAttachment> attachmentList = new List<SwaAttachment>();
                            //OperationContext.Current.OutgoingMessageProperties.Add(SwaEncoderConstants.AttachmentProperty, attachmentList);
                            OperationContext.Current.OutgoingMessageProperties.Add(SwaEncoderConstants.SoapEnvelopeProperty, request);
                            wcfResponse = proxy.SendMessage(wcfRequest);
                        }
                    }
                }

                if (wcfResponse != null)
                {
                    string response = SoapMessageHelper.DumpWCFMessage(wcfResponse);
                    wcfResponse.Close();
                    this.textBoxMsgRcv.Text = response;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(this, err.ToString(), this.Text);
            }
        }

        private void checkBoxMsgSndWrap_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxMsgSnd.WordWrap = this.checkBoxMsgSndWrap.Checked;
        }

        private void checkBoxMsgRcvWrap_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxMsgRcv.WordWrap = this.checkBoxMsgRcvWrap.Checked;
        }
    }
}

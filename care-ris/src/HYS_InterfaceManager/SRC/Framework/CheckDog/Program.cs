using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.License2;

namespace CheckDog
{
    class Program
    {
        static string getString(byte num)
        {
            return (num >> 4).ToString("X") + (num & 0x0F).ToString("X");
        }

        static void getEncodedLicense(StringBuilder sb, DeviceLicense devicelic, GWLicense gatewaylic)
        {
            if (sb == null) return;
            if (devicelic == null || gatewaylic == null)
            {
                // There is not license information in the dog for this interface.
                sb.Append("0000");
                //sb.Append("NULL");
            }
            else
            {
                // Encoding license information.
                sb.Append(getString(devicelic.MaxInterfaceCount));

                if (devicelic.IsExpired(gatewaylic.Header.CreateDate))
                {
                    sb.Append('0');
                }
                else
                {
                    sb.Append('1');
                }

                string strFeature = getString(devicelic.FeatureCode);
                if (strFeature == null || strFeature.Length < 2)
                {
                    sb.Append('0');
                }
                else
                {
                    sb.Append(strFeature[1]);
                }
            }
        }

        static void Main(string[] args)
        {
            if (args == null || args.Length < 1) return;
            
            string header = args[0];
            if (header.Length < 1) return;

            GWLicenseAgent agt = new GWLicenseAgent(true);
            GWLicense gatewaylic = agt.LoginGetLicenseLogout();

            StringBuilder sb = new StringBuilder();
            sb.Append(header);

            if (gatewaylic == null)
            {
                // Login dog or read license data failed.
                //sb.Append("NOTFOUND");
                sb.Append("00000000");
            }
            else
            {
                DeviceLicense licSender = gatewaylic.FindDevice(
                    DeviceName.HL7_SENDER.ToString(), DeviceType.HL7, DirectionType.OUTBOUND);
                DeviceLicense licReceiver = gatewaylic.FindDevice(
                    DeviceName.HL7_RECEIVER.ToString(), DeviceType.HL7, DirectionType.INBOUND);

                getEncodedLicense(sb, licSender, gatewaylic);
                getEncodedLicense(sb, licReceiver, gatewaylic);
            }
            
            DataCrypto dc = new DataCrypto(
                "DES", // Encrypt algorithm name.
                "skic8fh35l093kg8vj5u98jnx01plm938vuikjmna45hgjvnmyqtxzap09lxtsei" // Key
                );

            string content = dc.Encrypto(sb.ToString());

            using (StreamWriter sw = File.CreateText(Application.StartupPath + "\\CheckDog.dat"))
            {
                sw.Write(content);
            }

            //using (StreamWriter sw = File.CreateText(Application.StartupPath + "\\CheckDog.DescryptoTest.txt"))
            //{
            //    sw.Write(dc.Decrypto(content));
            //}
        }
    }
}

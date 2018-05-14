#region

using System.ServiceModel;
using Hys.CareAgent.WcfService.Contract;

#endregion

namespace Hys.CareAgent.WcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class FakeCardReaderService : ICardReaderService
    {
        public int Open()
        {
            return 0;
        }

        public CardInfo Read()
        {
            return new CardInfo
            {
                Identifier = "123456789012345678",
                Name = "Test name",
                Number = "123456"
            };
        }

        public void Close()
        {
        }


        public string Get()
        {
            return Read().Number;
        }

        public string FindComPort()
        {
            return "COM4";
        }
    }
}
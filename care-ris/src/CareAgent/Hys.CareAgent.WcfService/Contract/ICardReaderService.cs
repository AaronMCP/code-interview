#region

using System.ServiceModel;
using System.ServiceModel.Web;

#endregion

namespace Hys.CareAgent.WcfService.Contract
{
    [ServiceContract]
    public interface ICardReaderService
    {
        string FindComPort();
        int Open();
        CardInfo Read();
        void Close();

        [WebGet(UriTemplate = "get", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string Get();
    }
}
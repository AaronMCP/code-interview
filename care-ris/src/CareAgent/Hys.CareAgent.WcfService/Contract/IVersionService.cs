#region

using System.ServiceModel;
using System.ServiceModel.Web;

#endregion

namespace Hys.CareAgent.WcfService.Contract
{
    [ServiceContract]
    public interface IVersionService
    {
        [WebGet(UriTemplate = "get", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string Get();
    }
}
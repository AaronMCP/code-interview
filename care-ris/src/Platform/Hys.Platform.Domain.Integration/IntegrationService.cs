using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  System.Web.Script.Serialization;

namespace Hys.Platform.Domain.Integration
{
    public class IntegrationService
    {
        public static GetRequestInfoBodyReturnModel GetRequestInfo(GetRequestInfoBodyInputModel inputBody, string serviceURL)
        {
            ReturnModel returnModel = new ReturnModel();
            InputModel inputModel = new InputModel();
            GetRequestInfoBodyReturnModel returnbody = null;
           
                inputModel.MessageID = "GetRequestInfo";
                inputModel.MessageBodyFormat = "JSON";
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                inputModel.MessageBody = serializer.Serialize(inputBody);

                HISConService hisCon = new HISConService(serviceURL);
                hisCon.DoCommand("RISGC", inputModel, ref returnModel);
                if (returnModel.CalledResult == 0)
                {
                    returnbody = serializer.Deserialize(returnModel.ResultBody, typeof(GetRequestInfoBodyReturnModel)) as GetRequestInfoBodyReturnModel;
                }
            
            return returnbody;
        }

        public static string GetExamQueueNo(GetQueueInfoBodyInputModel inputBody, string serviceURL, ref string room)
        {
            ReturnModel returnModel = new ReturnModel();
            InputModel inputModel = new InputModel();

            inputModel.MessageID = "GetQueueInfo";
            inputModel.MessageBodyFormat = "JSON";

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            inputModel.MessageBody = serializer.Serialize(inputBody);

            HISConService hisCon = new HISConService(serviceURL);
            hisCon.DoCommand("RISGC", inputModel, ref returnModel);
            if (returnModel != null)
            {
                if (returnModel.CalledResult == 0)
                {
                    if (!string.IsNullOrWhiteSpace(returnModel.ResultBody))
                    {
                        GetQueueInfoBodyReturnModel returnbody
                            = serializer.Deserialize(returnModel.ResultBody, typeof(GetQueueInfoBodyReturnModel)) as GetQueueInfoBodyReturnModel;

                        room = returnbody.ExamRoom;

                        if (!string.IsNullOrWhiteSpace(returnbody.GlobalQueueNumber))
                        {
                            return returnbody.GlobalQueueNumber;
                        }

                        if (!string.IsNullOrWhiteSpace(returnbody.RoomQueueNumber))
                        {
                            return returnbody.RoomQueueNumber;
                        }
                    }
                }
            }

            return "";
        }

        public static bool UpdateBookingInfo(UpdateBookInfoBodyInputModel inputBody, string serviceURL)
        {
            bool result = true;

            ReturnModel returnModel = new ReturnModel();
            InputModel inputModel = new InputModel();

            inputModel.MessageID = "UpdateBookInfo";
            inputModel.MessageBodyFormat = "JSON";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            inputModel.MessageBody = serializer.Serialize(inputBody);

            HISConService hisCon = new HISConService(serviceURL);
            hisCon.DoCommand("RISGC", inputModel, ref returnModel);
            if (returnModel == null || returnModel.CalledResult != 0)
            {
                result = false;
            }

            return result;
        }

    }
}

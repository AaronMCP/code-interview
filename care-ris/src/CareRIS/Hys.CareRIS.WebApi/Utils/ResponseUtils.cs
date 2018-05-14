using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Hys.CareRIS.WebApi.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseUtils
    {
        private const string HEADER_CON_ERROR = "x-con-error";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static HttpResponseMessage BadRequest(string content)
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(content)
            };
            SetApplicationError(response, content);
            return response;
        }

        private static void SetApplicationError(HttpResponseMessage response, string content)
        {
            var error = content.Replace("\r\n", " ").Replace("’", " ");
            if (!string.IsNullOrEmpty(error))
            {
                // when line ends with enter needs add a blank
                response.Headers.Add(HEADER_CON_ERROR, error + " "); 
            }
        }
    }
}
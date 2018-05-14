using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace HYS.IM.Common.XDS
{
    public class XdsResponseCreator
    {
        public static XmlDocument ConstructRegistrySuccessResponse()
        {
            XmlDocument objRegistryDocument = null;

            objRegistryDocument = new XmlDocument();
            XmlElement rootElement = objRegistryDocument.CreateElement("RegistryResponse");

            //xmlns
            rootElement.Attributes.Append(objRegistryDocument.CreateAttribute("xmlns"));
            rootElement.Attributes["xmlns"].Value = "urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0";

            //status
            rootElement.Attributes.Append(objRegistryDocument.CreateAttribute("status"));
            rootElement.Attributes["status"].Value = XdsGlobalValues.CONST_RESPONSE_STATUS_TYPE_SUCCESS;

            //Append Child
            objRegistryDocument.AppendChild(rootElement);

            return objRegistryDocument;
        }

        public static XmlDocument ConstructRegistryErrorResponse(string status, string requestID, string severity,
                                                   string codeContext, string errorCode, string location)
        {
            XdsRegistryErrorList errorList = new XdsRegistryErrorList();
            errorList.HighestSeverity = severity;

            XdsRegistryError error = new XdsRegistryError();
            error.CodeContext = codeContext;
            error.ErrorCode = errorCode;
            error.Severity = severity;
            error.Location = location;

            errorList.RegistryErrors.Add(error);

            return ConstructRegistryErrorResponse(status, requestID, errorList);
        }

        #region Private methods
        private static XmlDocument ConstructRegistryErrorResponse(string status, string requestID, XdsRegistryErrorList registryErrorList)
        {
            XmlDocument xmlResponse = new XmlDocument();

            // root element
            XmlElement elmRegistryResponse = xmlResponse.CreateElement("tns:RegistryResponse", @"urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0");
            xmlResponse.AppendChild(elmRegistryResponse);

            // tns
            XmlAttribute attribute = xmlResponse.CreateAttribute("xmlns:tns");
            attribute.Value = "urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0";
            elmRegistryResponse.Attributes.Append(attribute);

            // rim
            attribute = xmlResponse.CreateAttribute("xmlns:rim");
            attribute.Value = "urn:oasis:names:tc:ebxml-regrep:xsd:rim:3.0";
            elmRegistryResponse.Attributes.Append(attribute);

            // status
            attribute = xmlResponse.CreateAttribute("status");
            attribute.Value = status;
            elmRegistryResponse.Attributes.Append(attribute);

            // request ID
            if (requestID != string.Empty)
            {
                attribute = xmlResponse.CreateAttribute("requestId");
                attribute.Value = requestID;
                elmRegistryResponse.Attributes.Append(attribute);
            }

            // registry error list
            XmlElement elmErrorList = xmlResponse.CreateElement("tns:RegistryErrorList", @"urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0");
            xmlResponse.AppendChild(elmErrorList);

            // set error information into the list
            attribute = xmlResponse.CreateAttribute("highestSeverity");
            attribute.Value = registryErrorList.HighestSeverity;
            elmErrorList.Attributes.Append(attribute);

            foreach (XdsRegistryError registryError in registryErrorList.RegistryErrors)
            {
                // create registry error element for each registry error
                XmlElement elmRegistryError = xmlResponse.CreateElement("tns:RegistryError", @"urn:oasis:names:tc:ebxml-regrep:xsd:rs:3.0");
                xmlResponse.AppendChild(elmRegistryError);

                // code context
                attribute = xmlResponse.CreateAttribute("codeContext");
                attribute.Value = registryError.CodeContext;
                elmRegistryError.Attributes.Append(attribute);

                // error code
                attribute = xmlResponse.CreateAttribute("errorCode");
                attribute.Value = registryError.ErrorCode;
                elmRegistryError.Attributes.Append(attribute);

                // severity
                attribute = xmlResponse.CreateAttribute("severity");
                attribute.Value = registryError.Severity;
                elmRegistryError.Attributes.Append(attribute);

                // location
                if (registryError.Location != null)
                {
                    attribute = xmlResponse.CreateAttribute("location");
                    attribute.Value = registryError.Location;
                    elmRegistryError.Attributes.Append(attribute);
                }
            }

            return xmlResponse;
        }
        #endregion
    }
}

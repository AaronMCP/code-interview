#region

using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Hys.CareAgent.Common;
using Hys.CareAgent.DAP;
using Hys.CareAgent.DAP.Entity;

#endregion

namespace Hys.CareAgent.WcfService.Contract
{
    [ServiceContract]
    public interface IRisProTaskService
    {
        [WebGet(UriTemplate = "AllPrinters", ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<string> AllPrinters();

        [WebGet(UriTemplate = "DefaultPrinter", ResponseFormat = WebMessageFormat.Json)]
        string DefaultPrinter();

        [WebGet(UriTemplate = "Print?accno={accno}&modalityType={modalityType}&templateType={templateType}&url={url}")]
        void Print(string accno, string modalityType, string templateType, string url);

        [WebGet(UriTemplate = "PrintOtherReport?accno={accno}&modalityType={modalityType}&templateType={templateType}&site={site}&url={url}&printer={printer}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string PrintOtherReport(string accno, string modalityType, string templateType, string site, string url, string printer);

        [WebInvoke(UriTemplate = "PrintHtml", Method = "POST", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        void PrintHtml(string htmlValue);

        [WebGet(UriTemplate = "PrintReport?id={id}&site={site}&domain={domain}&url={url}&printtemplateid={printtemplateid}&printer={printer}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string PrintReport(string id, string site, string domain, string url, string printtemplateid, string printer);

        [WebGet(UriTemplate = "ShowHtmlData?id={id}&domain={domain}&site={site}&url={url}&printtemplateid={printtemplateid}", ResponseFormat = WebMessageFormat.Json)]
        string ShowHtmlData(string id, string domain, string site, string url, string printtemplateid);

        [WebGet(UriTemplate = "GetSrcInfo?damid={damid}&apihost={apihost}", ResponseFormat = WebMessageFormat.Json)]
        string GetSrcInfo(string damid, string apihost);

        [WebGet(UriTemplate = "OpenSelectFiles", ResponseFormat = WebMessageFormat.Json)]
        string OpenSelectFiles();

        [WebGet(UriTemplate = "OpenSelectFolder", ResponseFormat = WebMessageFormat.Json)]
        string OpenSelectFolder();

        [WebGet(UriTemplate = "GetProcessID", ResponseFormat = WebMessageFormat.Json)]
        string GetProcessID();

        [WebGet(UriTemplate = "StartMeeting?ipaddress={ipaddress}&username={username}&userpassword={userpassword}&conferenceid={conferenceid}&conferencepass={conferencepass}&showname={showname}", ResponseFormat = WebMessageFormat.Json)]
        bool StartMeeting(string ipaddress, string username, string userpassword, string conferenceid, string conferencepass, string showname);

        [WebGet(UriTemplate = "IfAdobeReaderInstalled", ResponseFormat = WebMessageFormat.Json)]
        bool IfAdobeReaderInstalled();

        [WebGet(UriTemplate = "openVideo", ResponseFormat = WebMessageFormat.Json)]
        void OpenVideo();

        [WebGet(UriTemplate = "closeVideo", ResponseFormat = WebMessageFormat.Json)]
        void CloseVideo();

        [WebGet(UriTemplate = "capturePhoto?width={width}", ResponseFormat = WebMessageFormat.Json)]
        string CapturePhoto(int width);

        [WebGet(UriTemplate = "PlayAudio?text={text}", ResponseFormat = WebMessageFormat.Json)]
        void PlayAudio(string text);

        [WebGet(UriTemplate = "StopAudio", ResponseFormat = WebMessageFormat.Json)]
        void StopAudio();

        [WebGet(UriTemplate = "PrintPdfReport?url={url}&printer={printer}&server={server}&port={port}&name={name}&pwd={pwd}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string PrintPdfReport(string url, string printer, string server, int port, string name, string pwd);

        [WebGet(UriTemplate = "ShowDICOMViewer?studyinstanceuid={studyinstanceuid}", ResponseFormat = WebMessageFormat.Json)]
        void ShowDICOMViewer(string studyinstanceuid);

        [WebGet(
            UriTemplate = "searchDICOMData?patientName={patientName}&patientId={patientId}&accessionNo={accessionNo}&pageSize={pageSize}&pageIndex={pageIndex}",
            ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        SearchResult SearchDICOMData(string patientName, string patientId, string accessionNo, int pageSize, int pageIndex);

        /// <summary>
        /// Delete Dicom Files
        /// </summary>
        /// <param name="studyInstanceUid"></param>
        /// <returns></returns>
        [WebGet(UriTemplate = "deleteDICOM?accessionNo={accessionNo}&studyinstanceuid={studyinstanceuid}", ResponseFormat = WebMessageFormat.Json)]
        Utilities.StatusCode DeleteDICOM(string accessionNo, string studyInstanceUid);

        /// <summary>
        /// Check  recived DICOM data
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        [WebGet(UriTemplate = "checkDICOM?time={time}", ResponseFormat = WebMessageFormat.Json)]
        bool CheckDICOMData(string time);

        /// <summary>
        /// view images through PACS
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="accNo"></param>
        /// <param name="studyId"></param>
        /// <returns></returns>
        [WebGet(UriTemplate = "viewimage?patientId={patientId}&accNo={accNo}&studyId={studyId}", ResponseFormat = WebMessageFormat.Json)]
        bool ViewImage(string patientId, string accNo, string studyId);

        [WebGet(UriTemplate = "pacsConfig", ResponseFormat = WebMessageFormat.Json)]
        string PacsConfig();
        [WebInvoke(UriTemplate = "editPacsConfig", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool EditPacsConfig(PacsConfig jsonPacs);
    }
}
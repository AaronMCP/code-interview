using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    public class ProfileNames
    {
        #region Global

        public const string FTPPassword = "FTPPassword";

        public const string FTPPort = "FTPPort";

        public const string FTPRootFolder = "FTPRootFolder";

        public const string FTPServer = "FTPServer";

        public const string FTPUserID = "FTPUserID";

        public const string Logo = "logo";

        public const string SplitterColor = "SplitterColor";

        public const string SenderToGateServer = "SendToGateServer";

        public const string DefaultPrinter = "DefaultPrinter";

        public const string WorkingMode = "WorkingMode";

        public const string NecessaryItemBackColor = "NecessaryItemBackColor";

        public const string Radiologist = "Radiologist";

        public const string Nurse = "Nurse";

        public const string Technician = "Technician";

        public const string CanReferral = "CanReferral";

        public const string CanReferral4WriteReport = "CanReferral4WriteReport";

        public const string CanReferral4ApproveReport = "CanReferral4ApproveReport";

        public const string CanPublishReport = "CanPublishReport";

        public const string SameUserMultiPlaceLogin = "SameUserMultiPlaceLogin";
        #endregion

        #region Report Global

        public const string Report_PageSize = "Report_PageSize";

        public const string Report_PreWaringMinutes = "Report_PreWaringMinutes";

        public const string Report_WaringMinutes = "Report_WaringMinutes";

        public const string Report_PreWaringColor = "Report_PreWaringColor";

        public const string Report_WaringColor = "Report_WaringColor";

        public const string Report_NeedPasswordSave = "Report_NeedPasswordSave";

        public const string Report_SplitterColor = "Report_SplitterColor";

        public const string Report_NeedSaveImageOnPrinting = "Report_SaveSnapshotOnPrint";

        public const string ReportFtpSubFolder = "ReportFtpSubFolder";

        public const string Report_NeedImageOnCreating = "Report_NeedImageOnCreating";

        public const string Report_Integration_Mode = "PACSIntegrationType";

        public const string Report_Integration_User = "PACSUser";

        public const string Report_Integration_Pwd = "PACSPassword";

        public const string Report_Integration_Server = "PACSServer";

        public const string Report_Integration_WebServer = "PACSWebServer";

        public const string Report_Integration_WebConfigLocation = "WebConfigLocation";

        public const string Report_isOverwriteApproveDtOnRebuild = "Report_isOverwriteApproveDtOnRebuild";

        public const string Report_isOverwriteApproverOnRebuild = "Report_isOverwriteApproverOnRebuild";

        #endregion

        #region ConditionBuilder

        public const string ConditionBuilder_NeedMakePatientID = "ConditionBuilder_NeedMakePatientID";

        public const string ConditionBuilder_NeedMakeAccNO = "ConditionBuilder_NeedMakeAccNO";

        public const string ConditionBuilder_TextMaxLength = "ConditionBuilder_TextMaxLength";
        public const string CondiotnBuilder_RangeComboBox = "CondiotnBuilder_RangeComboBox";

        #endregion

        #region ReportList

        public const string ReportList_AssociatedReport = "ReportList_RelatedReport";

        public const string ReportList_AutoLoadImage = "ReportList_AutoLoadImage";

        #endregion

        #region ReportEditor

        public const string profile_canSave = "canSave";
        public const string profile_canSubmit = "canSubmit";
        public const string profile_canApprove = "canApprove";
        public const string profile_canReject = "canReject";
        public const string profile_canDelete = "canDelete";
        public const string profile_canPrint = "canPrint";
        public const string profile_canRebuild = "canRebuild";
        public const string profile_canHistory = "canHistory";
        public const string profile_canShowReg = "canShowReg";
        public const string profile_canShowApp = "canShowApp";
        public const string profile_canTeaching = "canTeaching";
        public const string profile_canSaveasTemplate = "canSaveasTemplate";
        public const string profile_canDisqualifyImage = "canDisqualifyImage";
        public const string profile_canExport = "canExport";
        public const string profile_isRichReport = "isRichReport";

        public const string ReportEditor_PreviewBeforePrinted = "ReportEditor_PreviewBeforePrinted";
        public const string ReportEditor_PrintAfterApprove = "ReportEditor_PrintAfterApprove";
        public const string ReportEditor_AppendTemplate = "ReportEditor_AppendTemplate";
        public const string ReportEditor_AppendImage = "ReportEditor_AppendImage";
        public const string ReportEditor_AllowModifyApprovedReport_Minutes = "ReportEditor_AllowModifyApprovedReport_Minutes";
        public const string ReportEditor_DefaultShowTemplate = "ReportEditor_DefaultShowTemplate";
        public const string ReportEditor_NeedEvaluationBeforeApprove = "ReportEditor_NeedEvaluationBeforeApprove";
        public const string ReportEditor_AutoLoadTemplate = "ReportEditor_AutoLoadTemplate";
        public const string ReportEditor_DefaultMatchingTemplateName = "ReportEditor_DefaultMatchingTemplateName";
        public const string ReportEditor_DefaultShowInfoList = "ReportEditor_DefaultShowInfoList";
        public const string ReportEditor_PictureListHeight = "ReportEditor_PictureListHeight";
        public const string ReportEditor_Rich1Height = "ReportEditor_Rich1Height";
        public const string ReportEditor_Rich2Height = "ReportEditor_Rich2Height";
        public const string ReportEditor_Rich3Height = "ReportEditor_Rich3Height";
        public const string ReportEditor_InfoListWidth = "ReportEditor_InfoListWidth";
        public const string ReportEditor_ImageDefaultSize = "ReportEditor_ImageDefaultSize";
        public const string ReportEditor_Font_Size = "ReportEditor_Font_Size";
        public const string ReportEditor_Font_FamilyName = "ReportEditor_Font_FamilyName";
        public const string ReportEditor_ExportPath = "ReportEditor_ExportPath";
        public const string ReportEditor_PublicSpellCheckingDic = "ReportEditor_PublicSpellCheckingDic";
        public const string ReportEditor_PrivateSpellCheckingDic = "ReportEditor_PrivateSpellCheckingDic";

        #endregion

        #region ReportPrintPanel

        public const string ReportPrint_isQuickPrint = "ReportPrint_isQuickPrint";
        public const string ReportPrint_MaxSelection = "ReportPrint_MaxSelection";
        public const string ReportPrint_MaxPrintCount = "ReportPrint_MaxPrintCount";

        #endregion

        #region Clinician

        public const string ClinicReport_ViewAllReports = "ClinicReport_ViewAllReports";
        public const string ClinicReport_SearchDayRange = "ClinicReport_SearchDayRange";

        #endregion

        #region Register

        public const string Register_Separator = "separator";
        public const string Register_PatientIDPrefix = "PatientIDPrefix";
        public const string Register_PatientIDLength = "PatientIDLength";
        public const string Register_AccNoPolicy = "AccNoPolicy";
        public const string Register_AccNoPrefix = "AccNoPrefix";
        public const string Register_AccNoLength = "AccNoLength";

        public const string Charge_CanAddCharge = "CanAddCharge";
        public const string Charge_CanConfirmCharge = "CanConfirmCharge";
        public const string Charge_CanRefundCharge = "CanRefundCharge";
        public const string Charge_CanDeductCharge = "CanDeductCharge";
        public const string Charge_CanCancelCharge = "CanCancelCharge";
        #endregion

        #region Exam
        public const string Exam_ClientLoglevel = "ExamClientLoglevel";
        public const string Exam_AutoQueue = "AutoQueue";
        public const string Exam_CanModifyFields = "CanModifyFields";
        public const string Exam_RememberItems = "ExamRememberItems";
        public const string Exam_ConditionBuilderMode = "Examination_ConditionBuilderMode";
        public const string Exam_SplitHeight = "Examination_SplitHeight";
        public const string Exam_Technician = "Technician";
        public const string Exam_Technician1 = "Technician1";
        public const string Exam_Technician2 = "Technician2";
        public const string Exam_Technician3 = "Technician3";
        public const string Exam_Technician4 = "Technician4";
        public const string Exam_NecessaryItems = "ExamNecessaryItems";
        public const string Exam_RequisitionShowPolicy = "ExamRequisitionWindowShowPolicy";
        public const string Exam_RequisitionWindowTimeOut = "ExamRequisitionWindowTimeOut";
        public const string Exam_CheckInConfirm = "CheckInConfirm";
        public const string ExamListRefreshTime = "ExamListRefreshTime";
        public const string Exam_BindingModality = "ExamBindingModality";

        #endregion

        #region ERequisition
        public const string ERequisition_Printer = "ERequisitionPrinter";
        #endregion
    }
}

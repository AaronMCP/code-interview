// enum for object model
commonModule.factory('enums', [function () {
    'use strict';

    return {
        checkStatus: {
            undo: 0,//已撤销
            booked: 10,	//已预约
            registered: 20,	//已登记
            onBoard: 23,	//已到检
            remake: 25,	//重拍
            examing: 30,	//正在检查
            examed: 50,//已检查
            created: 100,	//已创建
            rejected: 105,	//已拒绝
            submitted: 110,	//已提交
            verified: 120	//已审核
        },

        tabType: {
            worklist: 0,
            registration: 1,
            report: 2,
            clientSetting: 3,
            selfServiceSetting: 4
        },

        searchType: {
            all: 0,
            patientName: 1,
            patientNo: 2,
            accNo: 3
        },

        dictionaryTag: {
            gender: 1,
            patientType: 5,
            ageUnit: 6,
            examineStatus: 13,
            positive: 21,
            chargeType: 52,
            priority: 115,
            observation: 65,
            YesNo: 70,
            ReferralPurpose: 101,
            ReferralStatus: 103,
            filmspecList: 4,
            durationList: 15,
            position: 82,
            medicineUsage: 81,
            contrastName: 80,
            title: 99
        },

        consultationDictionaryType: {
            timeRange: 2,
            rejectReason: 3,
            cancaleReason: 4,
            applyReconsiderReason: 5,
            terminateReason: 6,
            applyCancelReason: 7
        },

        sexType: {
            Unknown: 0,
            Female: 1,
            Male: 2
        },

        keyCode: {
            backspace: 8,
            enter: 13,
            shift: 16,
            ctrl: 17,
            alt: 18,
            esc: 27,
            left: 37,
            up: 38,
            right: 39,
            down: 40,
            delete: 46,
            number0: 48,
            letterz: 90,
            padmin: 96,
            padmax: 111,
            semicolon: 186,
            graveaccent: 192,
            openbracket: 219,
            singlequote: 222
        },

        rpStatus: {
            unknown: 0,
            noCheck: 10,
            checkIn: 20,
            repeatshot: 25,
            examing: 30,
            rpCancel: 40,
            examination: 50,
            draft: 100,
            reject: 105,
            submit: 110,
            firstApprove: 120,
            secondApprove: 130
        },
        transferStatus: {
            reged: 'Reged',
            rejected: 'Rejected',
            booked: 'Booked',
            pending: 'Pending'
        },
        consultationRequestStatus: {
            All: 0,
            Applied: 1,
            Accepted: 2,
            Completed: 3,
            Cancelled: 4,
            Rejected: 5,
            Consulting: 6,
            Reconsider: 7,
            Terminate: 8,
            Delete: 9,
            ApplyCancel: 10,
            None: 32
        },

        consultantType: {
            center: 0,
            expert: 1
        },
        recipientMode: {
            single: 0,
            multiple: 1,
            expertOnly:3
        },
        serviceType: {
            normal: 1,
            emergency: 2
        },

        consultationRequestStatusMap: {
            1: 'Applied',
            2: 'Accepted',
            3: 'Completed',
            4: 'Cancelled',
            5: 'Rejected',
            6: 'Consulting',
            7: 'Reconsider',
            8: 'Terminate',
            10: 'ApplyCancel'
        },

        patientCaseStatus: {
            All: -1,
            NotApply: 0,
            Applied: 1,
            Deleted: 2,
            None: 32,
            DicomList:3
        },

        consultationTimeRange: {
            None: 0,
            Morning: 1,
            Afternoon: 2,
            Night: 3
        },

        shortcutCategory: {
            All: 0,
            RequestSearchDoctor: 1,
            RequestSearchCenter: 2,
            RequestSearchExpert: 3
        },

        Permissions: {
            _0RemoteRequest: {
                CreateNewCase: '001CreateNewCase',
                CombineCase: '002CombineCase',
                EditProcedure: '003EditProcedure',
                ViewCase: '004ViewCase',
                EditCaseExceptCompleted: '005EditCaseExceptCompleted',
                EditPatientExceptCompleted: '006EditPatientExceptCompleted',
                EditHistoryAndDiagnosisExceptCompleted: '007EditHistoryAndDiagnosisExceptCompleted',
                EditRequestAndRequirmentExceptCompleted: '008EditRequestAndRequirmentExceptCompleted',
                EditReceiverNotAccepted: '009EditReceiverNotAccepted',
                SearchCase: '010SearchCase',
                RequestConsultation: '011RequestConsultation',
                SubmitConsultationRequest: '012SubmitRequest',
                CancelRequest: '013CancelRequest',
                ForceEnd: '014RemoteRequestForceEnd',
                ViewConsultation: '015ViewConsultation',
                RequestCancelConsultation: '016RequestCancelConsultation',
                RequestReconsideration: '017RequestReconsideration',
                PrintResult: '018PrintResult',
            },
            _1ConsultationCenter: {
                EditReceiverAccept: '001EditReceiverAccept',
                ViewConsultation: '002ConsultationCenterViewInfo',
                AcceptRequest: '003AcceptRequest',
                RefuseRequest: '004RefuseRequest',
                StartMeeting: '005StartMeeting',
            },
            _2ExpertSideFunc: {
                ViewInfo: '001ExpertViewInfo',
                StartMeeting: '002ExpertStartMeeting',
                RefuseRequest: '003ExpertRefuseRequest',
            }
        },

        Gender: {
            Unspecified: 0,
            Male: 1,
            Female: 2
        },
        ConsultationDicType: {
            ConsultationTimeRange: 2,
            RejectType: 3,
            CancelType: 4,
        },

        ConsultationTimeRange:
        {
            None: '0',
            Morning: '1',
            Afternoon: '2',
            Night: '3'
        },

        HospitalDefaultType: {
            Hospital: 0,
            Expert: 1
        },

        IntegrationTypes: {
            NoIntegration: 0,
            SYSTEM5DX: 1,
            SYSTEM5WEB: 2
        },
        NotifyType: {
            SMS: 0,
            IM: 1
        },
        NotifyEvent: {
            DoctorSendRequest: 0,
            ConsolutionAdminAssignToExpert: 1,
            ConsolutionAdminAcceptedNotifyDoctor: 2,
            ConsolutionAdminDeclinedRequest: 3,
            ConsolutionAdminForceEndRequest: 4,
            DoctorCancelRequest: 5,
            ConsolutionReportUpdated: 6
        },
        ReferralStatus: {
            Created: 0,
            Sending: 1,
            Sent: 3,
            Arrived: 5,
            SentFailed: 8,
            Accept: 10,
            Cancelling: 12,
            Canceled: 15,
            CancelFailed: 18,
            Rejecting: 20,
            Rejected: 22,
            RejectFailed: 25,
            Finishing: 28,
            Finished: 30,
            FinishFailed: 32,
            All: 100,
            Unsend: 101
        },
        ReferralPurpose: {
            ReferralBooking: 20,
            Reporting: 50,
            AuditingReport: 110,
            ReportConsultation: 200,
        },
        ChangeReasonTitle: {
            Default: '',
            Cancel: 'CancelApplication',
            Reject: 'RefuseRequest',
            Terminate: 'ForceTerminate',
            Reconsider: 'ApplyReconsideration',
            ApplyCancel: 'ApplyCancel'
        },

        WlSearchType:
        {
            All: 0,
            Doctor: 1,
            Center: 2,
            Expert: 3
        },
        DeleteType: {
            ConsultationRequest: 0,
            PatientCase: 1
        },

        UserSettingType:
        {
            All: 0,
            ConsultationRequestWorkList: 1,
            ConsultationPatientCaseWorkList: 2,
            IsSystem5DX: 3
        },
        ActionReusltStatus:
        {
            Success: 0,
            Failed: -1,
            AccessDenied:-2
        },
        fileSendError:
        {
            SendDicomError001: 'SendDicomError001',
            SendDicomError002: 'SendDicomError002',
            SendFTPError001: 'SendFTPError001',
            SendFTPError002: 'SendFTPError002',
            SendFTPError003: 'SendFTPError003',
            SendFTPError004: 'SendFTPError004'
        }
    }
}]);
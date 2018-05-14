commonModule.filter("statusFilter", ["enums", function (enums) {
    'use strict';
    var filterfun = function (status, statusList, showIcom) {
        var statusText = status, statusDic, statusIcon = '';
        //showIcom = (showIcom === null || showIcom === undefined) ? true : showIcom;
        //switch (status) {
        //    case enums.checkStatus.undo:
        //        statusIcon = '<span class="icon-general icon-status_exam_cancel icon-red"></span>';
        //        break;
        //    case enums.checkStatus.booked:
        //        statusIcon = '<span class="icon-general icon-status_scheduled icon-blue"></span>';
        //        break;
        //    case enums.checkStatus.registered:
        //        statusIcon = '<span class="icon-general icon-status_registered icon-blue"></span>';
        //        break;
        //    case enums.checkStatus.onBoard:
        //        statusIcon = '<span class="icon-general icon-status_onBoard icon-blue"></span>';
        //        break;
        //    case enums.checkStatus.remake:
        //        statusIcon = '<span class="icon-general icon-status_shotAgain icon-red"></span>';
        //        break;
        //    case enums.checkStatus.examing:
        //        statusIcon = '<span class="icon-general icon-status_inExam icon-blue"></span>';
        //        break;
        //    case enums.checkStatus.examed:
        //        statusIcon = '<span class="icon-general icon-status_examed icon-blue"></span>';
        //        break;
        //    case enums.checkStatus.created:
        //        statusIcon = '<span class="icon-general icon-status_created icon-blue"></span>';
        //        break;
        //    case enums.checkStatus.rejected:
        //        statusIcon = '<span class="icon-general icon-status_refuse icon-red"></span>';
        //        break;
        //    case enums.checkStatus.submitted:
        //        statusIcon = '<span class="icon-general icon-status_submitted icon-blue"></span>';
        //        break;
        //    case enums.checkStatus.verified:
        //        statusIcon = '<span class="icon-general icon-status_verified icon-blue"></span>';
        //        break;
        //    default:
        //        statusIcon = '';
        //        break;
        //}

        if (statusList) {
            statusDic = _.findWhere(statusList, { value: status + '' });
        }
        if (statusDic) {
            statusText = '<span class="ris-detail-status-text">' + statusDic.text + '</span>';
        }
        return statusText + (showIcom ? statusIcon : '');
    };
    return filterfun;
}]);


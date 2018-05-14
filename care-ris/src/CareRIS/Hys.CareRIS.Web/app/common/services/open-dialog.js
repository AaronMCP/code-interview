commonModule.factory('openDialog', ['$log', 'risDialog', '$translate', function ($log, risDialog, $translate) {

    var defaultHeaderTemplate = '<div class="modal-header" style="border-bottom:none;"><button type="button" class="close" ng-click="$modalCancel()">&times;</button></div>';
    var defaultTemplate = '<div class="modal-body" style="padding-top:0;padding-right:0;">' +
                               '<div class="row-fluid">' +
                                   '<div class="{0}" style="float:left;width:50px;height:50px;"/>' +
                                   '<div style="width:90%;margin-left:65px;">' +
                                    '<h2 class="ng-binding">{1}</h2>' +
                                    '<h4>{2}</h4>' +
                                    '</div>' +
                               '<div>' +
                                '<div class="codebox">   </div>' +
                             '</div>' +
                        '</div>' +
                    '</div>';
    var defaultFooterTemplate = '<button class="btn btn-primary" ng-click="$modalSuccess()">{{$modalSuccessLabel}}</button>';
    var defaultFooterTemplateForOKCancel = '<button class="btn btn-primary" ng-click="$modalSuccess()">{{$modalSuccessLabel}}</button>' +
            '<button class="btn btn-primary" ng-click="$modalCancel()">{{$modalCancelLabel}}</button>';

    var defaultFooterTemplateClientAgent = '<button class="btn btn-primary" ng-click="$modalSuccess()">{{$modalSuccessLabel}}</button>' +
            '<button class="btn btn-primary" ng-click="$modalCustom()">{{$modalCustomLabel}}</button>';

    var notifyMessageType =
    {
        Error: 0,
        Success: 1,
        Warn: 2,
        Info: 3
    };

    var convertMessageTypeClass = function (notifyMessageTypeValue) {
        var annotationClass = '';
        switch (notifyMessageTypeValue) {
            case notifyMessageType.Error:
                annotationClass = 'annotation_error_img';
                break;
            case notifyMessageType.Info:
                annotationClass = 'annotation_info_img';
                break;
            case notifyMessageType.Success:
                annotationClass = 'annotation_success_img';
                break;
            case notifyMessageType.Warn:
                annotationClass = 'annotation_warn_img';
                break;
            default:
                annotationClass = 'annotation_info_img';
                break;
        }

        return annotationClass;
    };

    return {
        NotifyMessageType: notifyMessageType,
        openIconDialog: function (notifyMessageType, title, content) {
            var annotationClass = convertMessageTypeClass(notifyMessageType);
            risDialog({
                id: window.RIS.newGuid(),
                template:
                  defaultTemplate.replace('{0}', annotationClass).replace('{1}', title).replace('{2}', content),
                headerTemplate: defaultHeaderTemplate,
                footerTemplate: '<button class="btn btn-primary" ng-click="$modalSuccess()">{{$modalSuccessLabel}}</button>',
                title: title,
                backdrop: true
            });
        },
        openIconDialogOkFun: function (notifyMessageType, title, content, successCallback, managed) {
            var annotationClass = convertMessageTypeClass(notifyMessageType);
            return risDialog({
                id: window.RIS.newGuid(),
                template:
                  defaultTemplate.replace('{0}', annotationClass).replace('{1}', title).replace('{2}', content),
                success: { label: $translate.instant('Ok'), fn: successCallback, param: null },
                headerTemplate: defaultHeaderTemplate,
                footerTemplate: defaultFooterTemplate,
                title: title,
                backdrop: true,
                managed: managed
            });
        },
        openIconDialogClientAgent: function (notifyMessageType, title, content, successCallback, runCallback) {
            var annotationClass = convertMessageTypeClass(notifyMessageType);
            risDialog({
                id: window.RIS.newGuid(),
                template:
                  defaultTemplate.replace('{0}', annotationClass).replace('{1}', title).replace('{2}', content),
                success: { label: $translate.instant('Download'), fn: successCallback, param: null },
                custom: { label: $translate.instant('Run'), fn: runCallback },
                headerTemplate: defaultHeaderTemplate,
                footerTemplate: defaultFooterTemplateClientAgent,
                title: title,
                backdrop: true
            });
        },
        openIconDialogOkCancel: function (notifyMessageType, title, content, successCallback) {
            var annotationClass = convertMessageTypeClass(notifyMessageType);
            risDialog({
                id: window.RIS.newGuid(),
                template:
                  defaultTemplate.replace('{0}', annotationClass).replace('{1}', title).replace('{2}', content),
                headerTemplate: defaultHeaderTemplate,
                success: { label: $translate.instant('Ok'), fn: successCallback, param: null },
                cancel: { label: $translate.instant('Cancel'), fn: null },
                title: title,
                backdrop: true
            });
        },
        openIconDialogOkCancel2: function (notifyMessageType, title, content, successCallback) {
            var annotationClass = convertMessageTypeClass(notifyMessageType);
            risDialog({
                id: window.RIS.newGuid(),
                template:
                  defaultTemplate.replace('{0}', annotationClass).replace('{1}', title).replace('{2}', content),
                headerTemplate: defaultHeaderTemplate,
                footerTemplate: defaultFooterTemplateForOKCancel,
                success: { label: $translate.instant('Ok'), fn: successCallback, param: null },
                cancel: { label: $translate.instant('Cancel'), fn: null },
                title: title,
                backdrop: true
            });
        },
        openIconDialogOkCancelParam: function (notifyMessageType, title, content, successCallback, successParam) {
            var annotationClass = convertMessageTypeClass(notifyMessageType);
            risDialog({
                id: window.RIS.newGuid(),
                template:
                  defaultTemplate.replace('{0}', annotationClass).replace('{1}', title).replace('{2}', content),
                headerTemplate: defaultHeaderTemplate,
                success: { label: $translate.instant('Ok'), fn: successCallback, param: successParam },
                cancel: { label: $translate.instant('Cancel'), fn: null },
                title: title,
                backdrop: true
            });
        },
        openIconDialogOkCancelParam2: function (notifyMessageType, title, content, successCallback, successParam) {
            var annotationClass = convertMessageTypeClass(notifyMessageType);
            risDialog({
                id: window.RIS.newGuid(),
                template:
                  defaultTemplate.replace('{0}', annotationClass).replace('{1}', title).replace('{2}', content),
                headerTemplate: defaultHeaderTemplate,
                footerTemplate: defaultFooterTemplateForOKCancel,
                success: { label: $translate.instant('Ok'), fn: successCallback, param: successParam },
                cancel: { label: $translate.instant('Cancel'), fn: null },
                title: title,
                backdrop: true
            });
        },
        openIconDialogYesNo: function (notifyMessageType, title, content, successCallback, cancelCallback) {
            var annotationClass = convertMessageTypeClass(notifyMessageType);
            risDialog({
                id: window.RIS.newGuid(),
                template:
                  defaultTemplate.replace('{0}', annotationClass).replace('{1}', title).replace('{2}', content),
                headerTemplate: defaultHeaderTemplate,
                success: { label: $translate.instant('Yes'), fn: successCallback, param: null },
                cancel: { label: $translate.instant('No'), fn: cancelCallback },
                title: title,
                backdrop: true
            });
        }
    }
}]);
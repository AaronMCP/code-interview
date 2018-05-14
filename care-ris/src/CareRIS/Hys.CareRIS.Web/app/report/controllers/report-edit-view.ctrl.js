reportModule.controller('ReportEditController', [
    '$log', '$scope', 'enums', 'reportService', 'risDialog', '$window', '$modal', 'loginContext',
    '$location', '$rootScope', '$controller', '$translate', '$timeout', 'dictionaryManager', '$http', 'busyRequestNotificationHub', 'application', 'commonMessageHub',
    'registrationUtil', 'registrationService', 'sendReferralService', 'openDialog', 'clientAgentService', 'csdToaster', '$state', '$sce', 'consultationService', 'loginUser', 'constants',
function ($log, $scope, enums, reportService, risDialog, $window, $modal, loginContext,
    $location, $rootScope, $controller, $translate, $timeout, dictionaryManager, $http, busyRequestNotificationHub,
    application, commonMessageHub, registrationUtil, registrationService, sendReferralService, openDialog, clientAgentService, csdToaster, $state, $sce, consultationService, loginUser, constants) {
    'use strict';
    $log.debug('ReportEditController.ctor()...');

    $scope.openWindow = function () {
        $scope.reTemplateWindow.open();
    }
    var saveReport = function (type) {
        if (!validateData()) {
            return;
        }
        $scope.reportData.wys = $scope.reportData.wysText;
        $scope.reportData.wyg = $scope.reportData.wygText;

        if (!_.isUndefined($scope.reportId) && !_.isNull($scope.reportId) && $scope.reportId != '0') {
            //preview or print, if not update, not save
            if (type == 1 || type == 2) {
                if ($scope.reportData.wysText == $scope.oldReportData.wysText
                    && $scope.reportData.wygText == $scope.oldReportData.wygText
                    && $scope.reportData.isPositive == $scope.oldReportData.isPositive) {
                    if (type == 1) {
                        previewReportProcess();
                    }
                    else if (type == 2) {
                        printReportProcess($scope.reportData.procedureIDs[0]);
                    }
                    else if (type == 5) {
                        sendReferralService.judgeLockAndSendByOrderID(order.uniqueID);
                    }
                    return;
                }
            }
            //update report
            reportService.updateReport($scope.reportData).success(function (data) {
                if (type == 0) {
                    //
                    //openDialog.openIconDialog(openDialog.NotifyMessageType.Success, $translate.instant("Tips"), $translate.instant("SaveReportSuccess"));
                    csdToaster.pop('success', $translate.instant("SaveReportSuccess"), '');
                }
                else if (type == 1) {
                    previewReportProcess();
                }
                else if (type == 2) {
                    printReportProcess($scope.reportData.procedureIDs[0]);
                }
                else if (type == 4) {
                    saveOldReport();
                    returnWorkListProcess();
                }
                else if (type == 5) {
                    sendReferralService.judgeLockAndSendByOrderID(order.uniqueID);
                }

                saveOldReport();
            }).error(function (data, status, headers, config) {

            });

        }
        else {
            //preview or print, if not update, not save
            if (type == 1 || type == 2) {
                if ($scope.reportData.wysText == $scope.oldReportData.wysText
                    && $scope.reportData.wygText == $scope.oldReportData.wygText
                    && $scope.reportData.isPositive == $scope.oldReportData.isPositive
                    && $scope.reportData.comments == $scope.oldReportData.comments) {
                    if (type == 1) {
                        previewReportProcess();
                    }
                    else if (type == 2) {
                        printReportProcess($scope.reportData.procedureIDs[0]);
                    }
                    return;
                }
            }
            //new report
            reportService.createReport($scope.reportData).success(function (data) {
                //notify message
                notifyOrderUpdate();
                $scope.reportId = data.uniqueID;//加了没啥用，可以用来控制预览显示
                if (type == 0) {
                    //
                    //openDialog.openIconDialog(openDialog.NotifyMessageType.Success, $translate.instant("Tips"), $translate.instant("SaveReportSuccess"));
                    csdToaster.pop('success', $translate.instant("SaveReportSuccess"), '');
                }
                else if (type == 1) {
                    previewReportProcess();
                    return;
                }
                else if (type == 2) {
                    printReportProcess($scope.reportData.procedureIDs[0]);
                    return;
                }
                else if (type == 4) {
                    saveOldReport();
                    returnWorkListProcess();
                }
                var procedureIDs = $scope.reportData.procedureIDs;
                $scope.reportData = data;
                saveOldReport();
                //show html
                convertCommentsToHtml();
                $scope.reportData.procedureIDs = procedureIDs;
                $scope.reportId = data.uniqueID;
                $scope.isDisabledDeleteReport = false;
            }).error(function (data, status, headers, config) {

            });

        }

        deleteLock();
    };

    var notifyOrderUpdate = function () {
        //notify message
        var orderUpdateParams = new commonMessageHub.OrderUpdateParams();
        orderUpdateParams.uniqueID = $scope.selectedorderId;
        commonMessageHub.publish(commonMessageHub.Messages.OrderUpdated, orderUpdateParams);
    };

    var submitReport = function () {
        if (!validateData()) {
            return;
        }
        //set values
        $scope.reportData.wys = $scope.reportData.wysText;
        $scope.reportData.wyg = $scope.reportData.wygText;

        var statusChanged = false;
        if ($scope.reportData.status != enums.rpStatus.submit) {
            $scope.reportData.status = enums.rpStatus.submit;
            statusChanged = true;
        }

        if (!_.isUndefined($scope.reportId) && !_.isNull($scope.reportId) && $scope.reportId != '0') {
            reportService.updateReport($scope.reportData).success(function (data) {
                if (statusChanged) {
                    //notify message
                    notifyOrderUpdate();
                }
                $scope.isDisabledDeleteReport = false;
                returnWorkListProcess();
            }).error(function (data, status, headers, config) {

            });

        }
        else {
            reportService.createReport($scope.reportData).success(function (data) {
                //notify message
                notifyOrderUpdate();
                returnWorkListProcess();
            }).error(function (data, status, headers, config) {

            });
        }
    };

    var validateData = function (report) {

        if ($scope.reportData.wysText == "" && $scope.reportData.wygText == "") {
            //invalid
            csdToaster.pop('warn', $translate.instant("ImagingRepresentationAndImagingDiagnosticsIsNull"), '');
            return false;
        }
        return true;
    };


    var openPACSImageViewer = function () {
        if (!$rootScope.browser.versions.mobile) {
            if (application.clientConfig && application.clientConfig.integrationType == 2) {
                reportService.getPacsUrl($scope.reportData.procedureIDs[0]).success(function (data) {
                    $scope.pacsURL = data;
                    if ($scope.pacsURL == "") {
                        //
                        openDialog.openIconDialog(openDialog.NotifyMessageType.Error, $translate.instant("Tips"), $translate.instant("CallImageFail"));
                        return;
                    }
                    $window.open($scope.pacsURL);
                });
            } else if (application.clientConfig && application.clientConfig.integrationType == 1) {
                reportService.getPacsUrlDX($scope.reportData.procedureIDs[0]).success(function (data) {
                    $scope.pacsURL = data;
                    if ($scope.pacsURL == "") {
                        //
                        openDialog.openIconDialog(openDialog.NotifyMessageType.Error, $translate.instant("Tips"), $translate.instant("CallImageFail"));
                        return;
                    }
                    $window.open($scope.pacsURL);
                });
            }
        }
    };

    var autoOpenPACSImageViewer = function () {
        if (application.configuration.isAutoLoadImage && !$rootScope.browser.versions.mobile) {
            openPACSImageViewer();
        }
    };

    var toggleBaseInfo = function () {
        if ($scope.baseInfoClass == 'glyphicon-chevron-up') {
            $scope.baseInfoClass = 'glyphicon-chevron-down'
            $scope.toggleBaseInfoHtml(true);
        }
        else {
            $scope.baseInfoClass = 'glyphicon-chevron-up';
            $scope.toggleBaseInfoHtml(false);
        }
        $scope.onResize();
    };

    var previewReport = function () {
        saveReport(1);
    };

    var previewReportProcess = function () {
        var currentPath = $location.url();
        var startIndex = currentPath.indexOf('&orderId');
        if (startIndex > 0) {
            var endIndex = currentPath.indexOf('&', startIndex + 1);
            if (endIndex > -1) {
                currentPath = currentPath.substr(0, startIndex) + currentPath.substr(endIndex, currentPath.length - endIndex);
            }
            else {
                currentPath = currentPath.substr(0, startIndex);
            }

            currentPath += '&procedureId=' + $scope.reportData.procedureIDs[0];
        }
        currentPath = currentPath.replace('&reportId=0', '');
        currentPath = currentPath.replace('reportId=0', '');//bug4194:预览返回后检查信息丢失，原因：替换失败
        var localPrintTemplateID = '';
        if ($scope.reportData.printTemplateID && $scope.reportData.printTemplateID != '') {
            localPrintTemplateID = $scope.reportData.printTemplateID;
        }

        $state.go('ris.report', {
            isPreview: true,
            procedureId: $scope.reportData.procedureIDs[0],
            printId: localPrintTemplateID,
            from: encodeURIComponent(currentPath)
        });
    };

    var printReport = function () {
        saveReport(2);
    };

    var printReportProcess = function (procedureId) {
        var localPrintTemplateID = '';
        if ($scope.reportData.printTemplateID && $scope.reportData.printTemplateID != '') {
            localPrintTemplateID = $scope.reportData.printTemplateID;
        }

        if (localPrintTemplateID == '') {
            reportService.getReportPrintTemplateIDByProID(procedureId, loginContext.site, loginContext.domain).success(function (data) {
                if (!data) {
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Error, $translate.instant('Alert'), $translate.instant('NoPrintTemplate').replace('{0}', $translate.instant('PrintReport')));
                    return;
                }

                printReportProcessToClient(procedureId, localPrintTemplateID);
            });
        }
        else {
            printReportProcessToClient(procedureId, localPrintTemplateID);
        }

    };

    var printReportProcessToClient = function (procedureId, localPrintTemplateID) {
        var param = {
            id: procedureId,
            site: loginContext.site,
            domain: loginContext.domain,
            url: loginContext.apiHost + '/api/v1/report/html',
            printtemplateid: localPrintTemplateID,
            printer: application.clientConfig.defaultPrinter
        };
        clientAgentService.printReport(param).success(function (data) {
            busyRequestNotificationHub.requestEnded();
            reportService.updateReportPrintStatusByProcedureID(procedureId, data);
        }).error(function () {
            busyRequestNotificationHub.requestEnded();
        });
    };

    var deleteReport = function () {
        //
        openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant("ConformDeleteReport"), deleteReportProcess);
    };
    var deleteReportProcess = function () {
        $scope.reportData.deleteMark = true;

        if (!_.isUndefined($scope.reportId) && !_.isNull($scope.reportId)) {
            if (!$scope.reportData.creater || loginContext.userId === $scope.reportData.creater) {
                reportService.updateReport($scope.reportData).success(function (data) {
                    //notify message
                    notifyOrderUpdate();
                    $scope.isDisabledDeleteReport = true;
                    returnWorkListProcess(); 
                }).error(function (data, status, headers, config) {

                });
            } else {
                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'),'您没有权限删除该报告！');
            }
           
        }
    };

    var approveReport = function () {
        if (!validateData()) {
            return;
        }
        $scope.reportData.wys = $scope.reportData.wysText;
        $scope.reportData.wyg = $scope.reportData.wygText;

        var statusChanged = false;
        //if ($scope.reportData.status < enums.rpStatus.firstApprove) {
        if ($scope.reportData.status != enums.rpStatus.firstApprove) {
            $scope.reportData.status = enums.rpStatus.firstApprove;
            statusChanged = true;
        }
        //}
        //else if ($scope.reportData.status = enums.rpStatus.firstApprove) {
        //    $scope.reportData.status = enums.rpStatus.secondApprove;
        //}
        //if success, return
        if (!_.isUndefined($scope.reportId) && !_.isNull($scope.reportId) && $scope.reportId != '0') {
            reportService.updateReport($scope.reportData).success(function (data) {
                if (statusChanged) {
                    //notify message
                    notifyOrderUpdate();
                }

                returnWorkListProcess();
            }).error(function (data, status, headers, config) {

            });
        }
        else {
            reportService.createReport($scope.reportData).success(function (data) {
                //notify message
                notifyOrderUpdate();

                returnWorkListProcess();
            }).error(function (data, status, headers, config) {

            });
        }


    };

    var rejectReportProcess = function () {
        $scope.reportData.status = enums.rpStatus.reject;
        $scope.reportData.wys = $scope.reportData.wysText;
        $scope.reportData.wyg = $scope.reportData.wygText;

        if (!_.isUndefined($scope.reportId) && !_.isNull($scope.reportId)) {
            reportService.updateReport($scope.reportData).success(function (data) {
                //notify message
                notifyOrderUpdate();

                returnWorkListProcess();
            }).error(function (data, status, headers, config) {

            });
        }

    };


    var rejectReport = function () {
        $scope.reportData.rejectToObject = $scope.reportData.submitter;
        rejectReportProcess();
    };

    var saveOldReport = function () {
        $scope.oldReportData.wysText = $scope.reportData.wysText;
        $scope.oldReportData.wygText = $scope.reportData.wygText;
        $scope.oldReportData.isPositive = $scope.reportData.isPositive;
        $scope.oldReportData.comments = $scope.reportData.comments;
    };

    var returnWorkList = function () {
        //$scope.reportData = { 'wysText': '', 'wygText': '', 'isPositive': -1, 'comments': '' };
        if (!_.isUndefined($scope.reportId) && !_.isNull($scope.reportId) && $scope.reportId != '0') {
            if ($scope.reportData.wysText != $scope.oldReportData.wysText
                || $scope.reportData.wygText != $scope.oldReportData.wygText
                || $scope.reportData.isPositive != $scope.oldReportData.isPositive) {
                openDialog.openIconDialogYesNo(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant("SaveReportTip"), returnWorkListSaveProcess, returnWorkListProcess);
                return;
            }
        }
        else {
            if ($scope.reportData.wysText != $scope.oldReportData.wysText
                || $scope.reportData.wygText != $scope.oldReportData.wygText
                || $scope.reportData.isPositive != $scope.oldReportData.isPositive
                || $scope.reportData.comments != $scope.oldReportData.comments) {
                openDialog.openIconDialogYesNo(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant("SaveReportTip"), returnWorkListSaveProcess, returnWorkListProcess);
                return;

            }
        }

        returnWorkListProcess();
    };

    var returnWorkListProcess = function () {
        deleteLock();
        reportService.getProcedureByID($scope.reportData.procedureIDs[0]).success(function (data) {

            if ($scope.from == 1) { //来自详细页
                $state.go('ris.registration', { orderId: data.orderID });

            } else {
                $location.path('ris/worklist/registrations');
                $location.url($location.path());
                $rootScope.refreshSearch({ 'orderId': data.orderID });
            }
        }).error(function (data, status, headers, config) {
            $location.path('ris/worklist/registrations');
            $location.url($location.path());
            $rootScope.refreshSearch();
        });
    };

    var returnWorkListSaveProcess = function () {
        saveReport(4);
    };

    var addLock = function () {

        if ($scope.isReadOnly == true) {
            disableAll();
            return;
        }

        if (!_.isUndefined($scope.procedureId) && !_.isNull($scope.procedureId) && $scope.procedureId != '') {
            reportService.addLockByProcedureIDs($scope.reportData.procedureIDs).success(function (data) {
                if (data == false) {
                    disableAll();
                }
            });

        }
            //order
        else if (!_.isUndefined($scope.orderId) && !_.isNull($scope.orderId) && $scope.orderId != '') {
            reportService.addLockByProcedureIDs($scope.reportData.procedureIDs).success(function (data) {
                if (data == false) {
                    disableAll();
                }
            });

        }
        else if (!_.isUndefined($scope.reportId) && !_.isNull($scope.reportId) && $scope.reportId != '' && $scope.reportId != '0') {
            reportService.addLock($scope.reportId).success(function (data) {
                if (data == false) {
                    disableAll();
                }
            });

        }
    };
    var deleteLock = function () {
        if ($scope.isReadOnly == true) {
            return;
        }

        if (!_.isUndefined($scope.procedureId) && !_.isNull($scope.procedureId) && $scope.procedureId != '') {
            reportService.deleteLockByProcedureIDs($scope.reportData.procedureIDs).success(function (data) {
            });
        }
            //order
        else if (!_.isUndefined($scope.orderId) && !_.isNull($scope.orderId) && $scope.orderId != '') {
            reportService.deleteLockByProcedureIDs($scope.reportData.procedureIDs).success(function (data) {
            });

        }
        else if (!_.isUndefined($scope.reportId) && !_.isNull($scope.reportId) && $scope.reportId != '' && $scope.reportId != '0') {
            reportService.deleteLock($scope.reportId).success(function (data) {
            });
        }
    };

    var disableAll = function () {
        $scope.isDisabledDeleteReport = true;
        $scope.isDisabledSaveReport = true;
        $scope.isDisabledSubmitReport = true;
        $scope.isDisabledRejectReport = true;
        $scope.isDisabledApproveReport = true;
    };

    var getReportTemplate = function (id, callBack) {
        $scope.popPrepareReportTemplate = { 'wysText': '', 'wygText': '', 'uniqueId': '' };
        $scope.selectReportTemplateDirID = id;

        reportService.getTemplateID(id).success(function (data) {
            $scope.popPrepareReportTemplate = data;
            $scope.popReportTemplate = $scope.popPrepareReportTemplate;
            $scope.popReportTemplate.getReportTemplateId = id;
            $scope.popReportTemplate.isUserTemplate = false;
            if ($scope.popReportTemplate.type && $scope.popReportTemplate.type==1) {
                $scope.popReportTemplate.isUserTemplate = true;
            }
            if (angular.isFunction(callBack)) {
                callBack();
            }
        });
    }
    var showReportTemplate = function (id) {
        var popover = $('.popover');
        $scope.closeAllPopover();
        if (popover.length < 1) {
            getReportTemplate(id);
        }      
    };

    var editReportTemplate = function () {
        $scope.closeAllPopover();
        if ($scope.popReportTemplate && $scope.popReportTemplate.uniqueID != '') {
            var modalInstance = $modal.open({
                templateUrl: '/app/report/views/report-edit-template.html',
                controller: 'ReportTemplateEditController',
                backdrop: 'static',
                keyboard: false,
                resolve: {
                    selectTemplate: function () {
                        return $scope.popReportTemplate;
                    }
                }
            });
            modalInstance.result.then(function (result) {
                if (result) {
                    $log.debug(result);
                    reportService.UpdateReportTemplate(result).success(function (data) {
                        //
                        var treeView = $("#tvTemplate").data("kendoTreeView");
                        $scope.isRefreshUserTemplate = true;
                        treeView.setDataSource(gettdsTemplate());

                    }).error(function (data, status, headers, config) {
                        openDialog.openIconDialog(openDialog.NotifyMessageType.Error, $translate.instant("Tips"), $translate.instant("CreateReportTemplateError"));
                    });
                }
            });
        }
    };

    var deleteReportTemplate = function () {
        $scope.closeAllPopover();
        if ($scope.popReportTemplate) {
            var alertContent = $translate.instant("ConformDeleteReportTemplate").replace("{0}", $scope.popReportTemplate.templateName);
            openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), alertContent, deleteReportTemplateProcess);
        }
    };

    var deleteReportTemplateProcess = function () {
        if ($scope.popReportTemplate && $scope.popReportTemplate.uniqueID != '') {
            reportService.deleteTemplateByID($scope.popReportTemplate.uniqueID).success(function (data) {
                var treeView = $("#tvTemplate").data("kendoTreeView");
                $scope.isRefreshUserTemplate = true;
                treeView.setDataSource(gettdsTemplate());
            });
        }
    };

    //var closeReportTemplate = function () {
    //    $scope.closePopover($scope.selectReportTemplateDirID);
    //};

    var selectReportTemplate = function (id, isAppend) {
        getReportTemplate(id, function () {
            if ($scope.popReportTemplate && $scope.popReportTemplate.uniqueID != '') {
                if (!_.isUndefined($scope.popReportTemplate.gender) && !_.isNull($scope.popReportTemplate.gender) && $scope.popReportTemplate.gender != ''
                    && $scope.baseInfoDesc != '') {
                    var gender = $scope.baseInfoDesc.split(',');
                    if (gender.length > 2 && $scope.popReportTemplate.gender != gender[2]) {
                        //
                        openDialog.openIconDialogOkCancelParam(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant("TemplateGenderNotMatch"), selectReportTemplateProcess,isAppend);
                        return;
                    }
                }

                selectReportTemplateProcess(isAppend);
            }
        });
        $timeout(function () {
            $scope.closeAllPopover();
        }, 100);
    };

    var selectReportTemplateProcess = function (isAppend) {
        if ($scope.popReportTemplate && $scope.popReportTemplate.uniqueID != '') {
            if (isAppend) {
                if ($scope.reportData.wysText && $scope.reportData.wysText != '' && $scope.reportData.wysText != 'undefined') {
                    $scope.reportData.wysText += '\r\n' + $scope.popReportTemplate.wysText;
                }
                else {
                    $scope.reportData.wysText = $scope.popReportTemplate.wysText;
                }

                if ($scope.reportData.wygText && $scope.reportData.wygText != '' && $scope.reportData.wysText != 'undefined') {
                    $scope.reportData.wygText += '\r\n' + $scope.popReportTemplate.wygText;
                }
                else {
                    $scope.reportData.wygText = $scope.popReportTemplate.wygText;
                }
                $scope.reportData.isPositive = $scope.popReportTemplate.isPositive;
            }
            else {
                $scope.reportData.wysText = $scope.popReportTemplate.wysText;
                $scope.reportData.wygText = $scope.popReportTemplate.wygText;
                $scope.reportData.isPositive = $scope.popReportTemplate.isPositive;
            }
        }

    };

    var gettdsTemplate = function () {
        //retur kendo data
        var templateData = new kendo.data.HierarchicalDataSource({
            transport: {
                read: function (options) {
                    var id = options.data.id;
                    //get user or role
                    reportService.getTemplateByParentID(id).success(function (data) {
                        if (data) {
                            options.success(data);

                            if ($scope.isRefreshUserTemplate) {
                                userTemplateExpand();
                            }
                        } else {
                            options.error();
                        }
                    }).error(function (data, status, headers, config) {
                        options.error();
                    });
                }
            },
            messages: {
                requestFailed: '没有子目录'
            },
            schema: {
                model: {
                    id: "id",
                    hasChildren: "hasChildren",
                    value: "id"
                }
            }
        });

        return templateData;
    };

    var convertCommentsToHtml = function () {
        //to html format
        if ($scope.reportData.comments && $scope.reportData.comments != '') {
            $scope.reportData.comments = $scope.reportData.comments.replace(/&/gi, "&amp;").replace(/\r\n/gi, "<br>").replace(/\n/gi, "<br>");
        }
    };


    var commentsKeypress = function () {
        if (event.keyCode == enums.keyCode.enter) {
            var content = $.trim($('#txtInputReportComments').val());
            if (content) {
                //max length 128
                if (content.length > 120) {
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('CommentsLong'));
                    return;
                } else if (content.length > 0) {
                    //unformat html
                    //content = content.replace(/&/gi, "&amp;").replace(/</gi, "&lt;").replace(/>/gi, "&gt;").replace(/\r\n/gi, "<br>").replace(/\n/gi, "<br>");
                    reportService.getServerTime().success(function (data) {
                        if ($scope.reportData.comments == null || $scope.reportData.comments.length < 1) {
                            $scope.reportData.comments = "<span class='report-comment-info'>" + loginContext.localName + "[" + data + "]:</span><br>" + content;
                        }
                        else {
                            $scope.reportData.comments = "<span class='report-comment-info'>" + loginContext.localName + "[" + data + "]:</span><br>" + content + "<br>" + $scope.reportData.comments.trim();
                        }
                        $('#txtInputReportComments').val(''); 
                        if (!_.isUndefined($scope.reportId) && !_.isNull($scope.reportId) && $scope.reportId != '0') {
                            //save data
                            reportService.updateComments($scope.reportData).success(function (commentResult) {
                                console.log(commentResult);
                            });
                        }
                    });
                }
            }
        }
    };

    var selectCommentsTab = function () {
        $scope.selectTab = 1;
        $scope.$broadcast('ris::report:addComments');
    };

    var selectHisTab = function () {
        $scope.selectTab = 2;
        //get data
        if (!_.isUndefined($scope.reportId) && !_.isNull($scope.reportId) && $scope.reportId != '0') {
            reportService.getReportListByID($scope.reportId).success(function (data) {
                $scope.historyList = data;
                if (statusList.length == 0) {
                    dictionaryManager.getDictionaries(enums.dictionaryTag.examineStatus).then(function (data) {
                        if (data) {
                            setHistoryListStatus();
                        }
                    });

                }
                else {
                    setHistoryListStatus();
                }

            });
        }

    };

    var setHistoryListStatus = function () {
        angular.forEach($scope.historyList, function (item) {
            var s = _.findWhere(statusList, { value: item.status + '' });
            if (s) {
                item.statuses = s.text;
            } else {
                item.statuses = item.status;
            }
        });
    };

    var selectReportTab = function () {
        $scope.selectTab = 3;
        if (!_.isUndefined($scope.reportId) && !_.isNull($scope.reportId) && $scope.reportId != '0') {
            reportService.getOtherReportListByID($scope.reportId).success(function (data) {
                $scope.otherReportList = data;
                if (statusList.length == 0) {
                    dictionaryManager.getDictionaries(enums.dictionaryTag.examineStatus).then(function (data) {
                        if (data) {
                            selectReportTabProcess();
                        }
                    });

                }
                else {
                    selectReportTabProcess();
                }
            });
        }
        else {
            //$scope.reportData.procedureIDs
            reportService.getOtherReportListByProcedureID($scope.reportData.procedureIDs[0]).success(function (data) {
                $scope.otherReportList = data;
                if (statusList.length == 0) {
                    dictionaryManager.getDictionaries(enums.dictionaryTag.examineStatus).then(function (data) {
                        if (data) {
                            selectReportTabProcess();
                        }
                    });

                }
                else {
                    selectReportTabProcess();
                }
            });
        }

    };
    var selectReportTabProcess = function () {
        angular.forEach($scope.otherReportList, function (item) {
            var s = _.findWhere(statusList, { value: item.status + '' });
            if (s) {
                item.statuses = s.text;
            } else {
                item.statuses = item.status;
            }
        });
    };
    $scope.selectedItemId = '';
    $scope.clickReportOther = function (item) {        
        $scope.selectedItemId = item.uniqueID;
    };
    var showReportOther = function (item) {
        var popover = $('.popover');
        if (popover.length < 1) {
            $scope.popReportOther = item; 
        }
    };

    var selectReportOther = function (item) {
        //readonly
        if ($scope.isReadOnly == true) {
            $scope.closeAllPopover();
            return;
        }
        $scope.popReportOther = item;
        //set values
        if ($scope.popReportOther && $scope.popReportOther.uniqueID != '') {
            $scope.closeAllPopover();
            $scope.reportData.wysText = $scope.popReportOther.wysText;
            $scope.reportData.wygText = $scope.popReportOther.wygText;
        }

    };

    $scope.selectedHostoryId = '';

    $scope.clickReportHis = function (item) {
        $scope.selectedHostoryId = item.uniqueID;
    }
    var showReportHis = function (item) {
        var popover = $('.popover');
        if (popover.length < 1) {
            $scope.popReportHis = item;
        }
    };

    var selectReportHis = function (item) {
        if ($scope.isReadOnly == true) {
            $scope.closeAllPopover();
            return;
        }
        $scope.popReportHis = item;
        //set values
        if ($scope.popReportHis && $scope.popReportHis.uniqueID != '') {
            $scope.closeAllPopover();
            $scope.reportData.wysText = $scope.popReportHis.wysText;
            $scope.reportData.wygText = $scope.popReportHis.wygText;
        }

    };
    var statusList = [];
    var getStatusList = function () {
        dictionaryManager.getDictionaries(enums.dictionaryTag.examineStatus).then(function (data) {
            if (data) {
                statusList = data;
            }
        });
    };
    getStatusList();

    var getPositiveList = function () {
        dictionaryManager.getDictionaries(enums.dictionaryTag.positive).then(function (data) {
            if (data) {
                $scope.positiveList = data;
                $scope.positiveList.sort(function (a, b) {
                    return (a.orderID > b.orderID ? 1 : -1);
                });
            }
        });
    };
    getPositiveList();


    var saveUserTemplate = function () {
        //show dialog
        reportService.getProcedureByID($scope.reportData.procedureIDs[0]).success(function (data) {
            var modalInstance = $modal.open({
                templateUrl: '/app/report/views/report-edit-template.html',
                controller: 'ReportTemplateEditController',
                backdrop: 'static',
                keyboard: false,
                resolve: {
                    selectTemplate: function () {
                        return {
                            wysText: $scope.reportData.wysText,
                            wygText: $scope.reportData.wygText,
                            modalityType: data.modalityType,
                            bodyPart: data.bodyPart
                        };
                    }
                }
            });
            modalInstance.result.then(function (result) {
                if (result) {
                    $log.debug(result);
                    reportService.createReportTemplate(result).success(function (data) {
                        //
                        var treeView = $("#tvTemplate").data("kendoTreeView");
                        $scope.isRefreshUserTemplate = true;
                        treeView.setDataSource(gettdsTemplate());

                    }).error(function (data, status, headers, config) {
                        openDialog.openIconDialog(openDialog.NotifyMessageType.Error, $translate.instant("Tips"), $translate.instant("CreateReportTemplateError"));
                    });
                }
            });
        });
    };

    var userTemplateExpand = function () {
        $timeout(function () {
            var treeView = $("#tvTemplate").data("kendoTreeView");
            var userTemplateObj = $("#tvTemplate")[0].children[0].children[2];
            treeView.expand(userTemplateObj);
            $scope.isRefreshUserTemplate = false;
        }, 0, true);
    };

    var getPrintTemplateByCriteria = function () {
        reportService.getProcedureByID($scope.reportData.procedureIDs[0]).success(function (proData) {
            var printTemplatebycriteria = { type: 3, modalityType: proData.modalityType };
            reportService.getPrintTemplateByCriteria(printTemplatebycriteria).success(function (templateData) {
                $scope.PrintTemplates = templateData;
                setSelectedPrintTemplate();
            });
        });
    };

    var setSelectedPrintTemplate = function () {
        //选择模板
        if ($scope.reportData.printTemplateID && $scope.reportData.printTemplateID != '') {
            angular.forEach($scope.PrintTemplates, function (item, index) {
                item.selected = false;
                if (item.uniqueID == $scope.reportData.printTemplateID) {
                    item.selected = true;
                }
            });
        }
        else {
            //初始进入，可能选择或未选择
            angular.forEach($scope.PrintTemplates, function (item, index) {
                item.selected = false;
            });

            angular.forEach($scope.PrintTemplates, function (item, index) {
                if (item.isDefaultByModality == true) {
                    item.selected = true;
                    $scope.reportData.printTemplateID = item.uniqueID;
                    return;
                }
            });

        }
    };

    var selectPrintTemplate = function (item) {
        $scope.reportData.printTemplateID = item.uniqueID;
        setSelectedPrintTemplate();

        //only approve, template can be saved
        if (!_.isUndefined($scope.reportId) && !_.isNull($scope.reportId) && $scope.reportId != '0'
            && $scope.reportData.status >= enums.rpStatus.firstApprove) {
            //save data
            reportService.updateReportPrintTemplate($scope.reportId, $scope.reportData.printTemplateID).success(function (commentResult) {

            });
        }
    };

    $scope.$on('event:startSearch', function (e, criteria) {
        e.preventDefault();
        //to worklist, and delete lock
        deleteLock();
    });


    $scope.viewRequisition = function () {
        registrationUtil.viewRequisition(order.accNo);
    }

    var order;
    var getOrderByProcedureID = function () {
        reportService.getOrderByProcedureID($scope.reportData.procedureIDs[0]).success(function (result) {
            order = result;
            $scope.isScan = order.isScan;
            //not only read
            if (!$scope.isReadOnly) {
                registrationService.getProceduresByOrderID(order.uniqueID).success(function (procedures) {
                    order.procedures = procedures;
                    $scope.isDisabledReferral = !sendReferralService.judgeCanReferralByOrder(order, 1);

                });
            }
        });
    };

    $scope.sendReferral = function () {
        saveReport(5);
    };

    $scope.reportTemplateSelected = true;
    $scope.reportHistorySelected = false;
    $scope.messageSelected = false;
    $scope.reportModifySelected = false;
    $scope.consultationSelected = false;
    $scope.selectOperator = function (seletedField) {
        $scope.closeAllPopover();
        $scope.reportTemplateSelected && ($scope.reportTemplateSelected = false);
        $scope.reportHistorySelected && ($scope.reportHistorySelected = false);
        $scope.messageSelected && ($scope.messageSelected = false);
        $scope.reportModifySelected && ($scope.reportModifySelected = false);
        $scope.consultationSelected && ($scope.consultationSelected = false);

        $scope[seletedField] = true;
        switch (seletedField) {
            case "reportHistorySelected":
                $scope.selectReportTab();
                break;
            case "messageSelected":
                $scope.selectCommentsTab();
                break;
            case "reportModifySelected":
                $scope.selectHisTab();
                break;
        }
    };

    $scope.requestConsultation = function () {
        if ($scope.reportData.procedureIDs[0]) {
            reportService.getPacsUrl($scope.reportData.procedureIDs[0]).success(function (data) {
                if (!data) {
                    csdToaster.pop('error', $translate.instant('PACSURLError'), '');
                } else {
                    var patientId = getUrlParameter('patient_id', data),
                        accessionNo = getUrlParameter('accession_number', data),
                        orderId = $scope.patientCaseOrderId || $scope.orderId;
                    loginUser.user.defaultRoleID = constants.doctorRoleId;
                    loginUser.initPermissions();
                    $state.go('ris.consultation.newpatientcase', { id: orderId, patientId: patientId, accessionNo: accessionNo });
                }
            });
        }
    };

    var getUrlParameter = function (param, dummyPath) {
        var sPageURL = dummyPath || window.location.search.substring(1),
            sURLVariables = sPageURL.split(/[&||?]/),
            res;
        for (var i = 0; i < sURLVariables.length; i += 1) {
            var paramName = sURLVariables[i],
                sParameterName = (paramName || '').split('=');

            if (sParameterName[0] === param) {
                //res = sParameterName[1];
                res = decodeURIComponent(sParameterName[1].replace(/\+/g, " "));
            }
        }
        return res;
    };
    var initializeConsultationResult = function () {
        var orderId = $scope.patientCaseOrderId || $scope.orderId;
        consultationService.getConsultationResult(orderId).success(function (result) {
            $scope.consultationResultList = result;
        });
    };

    $scope.clickResult = function (item) {
        if (item.status === enums.consultationRequestStatus.Completed) {
            $scope.popResultAdvice = item;
            $scope.selectedResultId = item.uniqueID;
            $scope.showPopover(item.uniqueID);
        }
    };

    $scope.$watch("reportData.procedureIDs", function handleFooChange(newValue, oldValue) {
        // access to apply consultation
        if (newValue && newValue.length > 0) {
            var orderId = $scope.patientCaseOrderId || $scope.orderId;
            reportService.GetImageStatus(orderId).success(function (result) {
                $scope.isDisableConsultation = !(loginUser.hasDoctorRole && result);
            });
        }
    });
    // initialzation
    ; (function initialize() {
        $scope.from;
        $log.debug('ReportEditController.initialize()...');
        $scope.isMobile = $rootScope.browser.versions.mobile;
        $scope.isDisableConsultation = true;
        $scope.reportData = { 'wysText': '', 'wygText': '', 'isPositive': -1, 'comments': '' };
        $scope.oldReportData = { 'wysText': '', 'wygText': '', 'isPositive': -1, 'comments': '' };
        $scope.pacsURL = "http://www.baidu.com";
        $scope.isDisabledCallImage = true;
        $scope.isDisabledDeleteReport = true;
        $scope.isDisabledSaveReport = false;
        $scope.isDisabledSubmitReport = false;
        $scope.isDisabledRejectReport = true;
        $scope.isDisabledApproveReport = false;
        $scope.isDisabledReferral = true;
        $scope.isPACSIntegration = application.configuration.isPACSIntegration;
        if ($scope.isPACSIntegration && application.clientConfig && application.clientConfig.integrationType == 0) {
            $scope.isPACSIntegration = false;
        }
        $scope.isReadOnly = false;
        $scope.baseInfoDesc = '';
        $scope.selectTab = 3;
        $scope.isLoadAll = false;
        $scope.historyList = null;
        $scope.otherReportList = null;
        //css
        $scope.baseInfoClass = 'glyphicon-chevron-up';

        $scope.returnWorkList = returnWorkList;
        $scope.saveReport = saveReport;
        $scope.submitReport = submitReport;
        $scope.openPACSImageViewer = openPACSImageViewer;
        $scope.toggleBaseInfo = toggleBaseInfo;
        $scope.previewReport = previewReport;
        $scope.deleteReport = deleteReport;
        $scope.approveReport = approveReport;
        $scope.rejectReport = rejectReport;
        $scope.printReport = printReport;
        $scope.commentsKeypress = commentsKeypress;
        $scope.selectCommentsTab = selectCommentsTab;
        $scope.selectHisTab = selectHisTab;
        $scope.selectReportTab = selectReportTab;
        $scope.positiveList = null;
        $scope.saveUserTemplate = saveUserTemplate;
        $scope.selectedorderId = '';
        $scope.isScan = false;
        $('#tdBaseInfoDesc').hide();

        if (!_.isUndefined($scope.isRead) && !_.isNull($scope.isRead) && $scope.isRead == 'true') {
            $scope.isReadOnly = true;
        }
        //template
        $scope.isRefreshUserTemplate = false;
        $scope.tdsTemplate = gettdsTemplate();
        $scope.popPrepareReportTemplate = null;
        $scope.popReportTemplate = null;
        $scope.isAppend = true;
        $scope.selectReportTemplateDirID = '';
        $scope.showReportTemplate = showReportTemplate;
        //$scope.closeReportTemplate = closeReportTemplate;
        $scope.selectReportTemplate = selectReportTemplate;
        $scope.editReportTemplate = editReportTemplate;
        $scope.deleteReportTemplate = deleteReportTemplate;
        $scope.toTemplate = {
            dataSource: $scope.tdsTemplate,
            dataTextField: 'name',
            dataValueField: 'id',
            enable: 'enabled',
            template: kendo.template($("#treeview-template").html()),
            select: function (e) {
                var dataItem = this.dataItem(e.node);
                if (dataItem.hasChildren) {
                    $scope.closeAllPopover();
                }
            }
        };
        //history
        $scope.popReportHis = null;
        $scope.showReportHis = showReportHis;
        $scope.selectReportHis = selectReportHis;

        //other report
        $scope.popReportOther = null;
        $scope.showReportOther = showReportOther;
        $scope.selectReportOther = selectReportOther;

        $scope.PrintTemplates = null;
        $scope.selectPrintTemplate = selectPrintTemplate;
        initializeConsultationResult();
        if (!_.isUndefined($scope.reportId) && !_.isNull($scope.reportId) && $scope.reportId != '' && $scope.reportId != '0') {
            reportService.getReport($scope.reportId).success(function (data) {
                $scope.reportData = data;
                saveOldReport();
                $scope.isDisabledDeleteReport = false;
                convertCommentsToHtml();
                if ($scope.reportData.status >= enums.rpStatus.firstApprove) {      
                    //$scope.isDisabledSaveReport = true;
                    $scope.isDisabledSubmitReport = true;
                }

                if ($scope.reportData.status < enums.rpStatus.firstApprove && !_.isNull($scope.reportData.submitter) && loginContext.userId != $scope.reportData.submitter) {
                    $scope.isDisabledRejectReport = false;
                }
            }).error(function (data, status, headers, config) {
            });
            //getlock
            reportService.getLock($scope.reportId).success(function (data) {
                if (!_.isNull(data) && loginContext.userId != data.owner) {
                    //return;
                }
            });
            //add Lock
            reportService.addLock($scope.reportId).success(function (data) {

            });
        }

        else if (!_.isUndefined($scope.procedureId) && !_.isNull($scope.procedureId) && $scope.procedureId != '') {
            $scope.reportData.procedureIDs = [$scope.procedureId];
            getPrintTemplateByCriteria();
            autoOpenPACSImageViewer();
            getOrderByProcedureID();
            reportService.getBaseInfo($scope.procedureId).success(function (data) {
                $scope.baseInfo = $sce.trustAsHtml(data);
                //$("#tdBaseInfo").html(data);
                //$scope.isLoadAll = true; 太慢
                $scope.onResize();

            });
            reportService.getBaseInfoDesc($scope.procedureId).success(function (data) {
                //$scope.baseInfo = data;
                $scope.briefDesc = data;
                //$("#tdBaseInfoDesc").html(data);
                $scope.baseInfoDesc = data;
                $scope.isLoadAll = true;
            });

            //get orderid
            reportService.getProcedureByID($scope.procedureId).success(function (data) {
                $scope.selectedorderId = $scope.orderID;
            });
            if (!_.isUndefined($scope.reportId) && !_.isNull($scope.reportId) && $scope.reportId != '0') {
                reportService.getReportByProcedureID($scope.procedureId).success(function (data) {
                    $scope.reportId = data.uniqueID;
                    $scope.reportData = data;
                    $scope.isDisabledDeleteReport = false;
                    saveOldReport();
                    convertCommentsToHtml();

                    //show other report
                    selectReportTab();

                    $scope.reportData.procedureIDs = [$scope.procedureId];
                    if ($scope.reportData.status >= enums.rpStatus.firstApprove) {
                        
                        $scope.isDisabledSaveReport = true;
                        $scope.isDisabledSubmitReport = true;
                    }

                    if ($scope.reportData.status < enums.rpStatus.firstApprove && !_.isNull($scope.reportData.submitter) && loginContext.userId != $scope.reportData.submitter) {
                        //if ($scope.reportData.status < enums.rpStatus.firstApprove && !_.isNull($scope.reportData.submitter)) {
                        $scope.isDisabledRejectReport = false;
                    }

                    //self approve, can reject
                    if ($scope.reportData.status >= enums.rpStatus.firstApprove && loginContext.userId == $scope.reportData.firstApprover) {
                        $scope.isDisabledRejectReport = false;
                    }

                    //get relate
                    reportService.getProceduresByReportID($scope.reportId).success(function (data) {
                        $scope.reportData.procedureIDs = data;
                        addLock();

                    });
                });
                return;
            }
            else if (!_.isUndefined($scope.reportId) && !_.isNull($scope.reportId) && $scope.reportId == '0') {
                $scope.reportData.procedureIDs = [$scope.procedureId];
                $scope.isDisabledDeleteReport = true;
                //show other report
                selectReportTab();
            }
        }
            //order
        else if (!_.isUndefined($scope.orderId) && !_.isNull($scope.orderId) && $scope.orderId != '') {
            $scope.isDisabledDeleteReport = true;
            $scope.selectedorderId = $scope.orderId;
            reportService.getBaseInfoDescByOrderID($scope.orderId).success(function (data) {

                //$scope.baseInfo = data;
                $scope.briefDesc = data;
                //$("#tdBaseInfoDesc").html(data);
                $scope.baseInfoDesc = data;
                $scope.isLoadAll = true;
            });
            reportService.getBaseInfoByOrderID($scope.orderId).success(function (data) {
                $scope.baseInfo = $sce.trustAsHtml(data);
                //$("#tdBaseInfo").html(data);
                //$scope.isLoadAll = true;太慢
                $scope.onResize();             
                return;
            });
            reportService.getProceduresByOrderID($scope.orderId).success(function (data) {
                $scope.reportData.procedureIDs = data;
                selectReportTab();
                //get template for print
                getPrintTemplateByCriteria();
                autoOpenPACSImageViewer();
                getOrderByProcedureID();
                //add lock
               addLock();
                if ($rootScope.browser.versions.mobile) {
                    reportService.getPacsUrl($scope.reportData.procedureIDs[0]).success(function (data) {
                        $("#reportLoadImage").attr('href', data);
                        $("#reportLoadImage").attr('target', "_blank");
                    });
                }
            });
        }
        //add lock
        addLock();
        if (!_.isUndefined($scope.reportId)) {
        }

        if ($rootScope.browser.versions.mobile && $scope.reportData.procedureIDs) {
            reportService.getPacsUrl($scope.reportData.procedureIDs[0]).success(function (data) {
                $("#registrationLoadImage").attr('href', data);
                $("#registrationLoadImage").attr('target', "_blank");
            });
        }
    }());
}
]);
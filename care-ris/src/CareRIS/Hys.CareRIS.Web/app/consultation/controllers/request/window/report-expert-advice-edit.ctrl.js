consultationModule.controller('ReportExpertAdviceEditController', ['$log', '$scope', 'consultationService',
    '$stateParams', '$state',  '$timeout', '$translate', 'openDialog', 'loginContext',
    '$filter', 'commonMessageHub', '$sce',
    function ($log, $scope, consultationService, $stateParams, $state,  $timeout, $translate, openDialog, loginContext,
        $filter, commonMessageHub, $sce) {
        'use strict';
        $log.debug('ReportExpertAdviceEditController.ctor()...');

        var closeWindow = function (result) {
            $scope.close({ result: result });
        }

        $scope.saveAdvice = function () {
            $scope.advice.lastEditUser = '';

            var consultationAdviceText = $.trim($("#request-advice-text").data("kendoEditor").body.innerText);
            if ($scope.expertAdviceEdit == null || $scope.expertAdviceEdit == ''
                || consultationAdviceText == '') {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('ExpertAdvice') + $translate.instant('IsRequiredErrorMsg'),
                    function () {
                        $("#request-advice-text").data("kendoEditor").focus();
                    });

                return false;
            }

            var expertAdviceContent = {};
            expertAdviceContent.consultationRequestID = $scope.advice.requestID;
            expertAdviceContent.Comments = $scope.expertAdviceEdit;
            consultationService.saveExpertAdvices(expertAdviceContent).success(function () {
                sendMessage();
                closeWindow($scope.advice);
            });

        };

        var sendMessage = function () {
            var consultationAdviceUpdateParams = new commonMessageHub.ConsultationAdviceUpdateParams();
            consultationAdviceUpdateParams.requestId = $scope.advice.requestID;
            consultationAdviceUpdateParams.userId = loginContext.userId;
            commonMessageHub.publish(commonMessageHub.Messages.ConsultationAdviceUpdate, consultationAdviceUpdateParams);
        };

        var onConsultationAdviceUpdateMessage = function (e, params) {
            if (params.requestId == $scope.advice.requestID) {
                $scope.isUpdated = true;
                //update UI
                $scope.$digest();
            }
        };

        var gettdsTemplate = function () {
            //retur kendo data
            var templateData = new kendo.data.HierarchicalDataSource({
                transport: {
                    read: function (options) {
                        var id = options.data.id;
                        //get user or role
                        consultationService.getTemplateByParentID(id).success(function (data) {
                            options.success(data);

                        }).error(function (data, status, headers, config) {
                            options.error();
                        });
                    }
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

        $scope.closeAllPopover = function () {
            $('.ris-popover').popover('hide');
            $('.report-preview-content-popover').remove();
        };

        $scope.closeReportTemplate = function (id) {
            $scope.selectReportTemplateDirID = '';
            $timeout(function () {
                //close pop
                if ($scope.popoverEnter == false) {
                    $scope.closePopover(id);
                }
            }, 500, true);
        };

        $scope.showPopover = function (id) {
            $(document.getElementById(id)).popover('show');
        };

        $scope.closePopover = function (id) {
            $(document.getElementById(id)).popover('hide');
        };

        $scope.showReportTemplate = function (id) {
            $scope.popPrepareReportTemplate = { 'wysText': '', 'wygText': '', 'uniqueId': '' };
            $scope.closeAllPopover();
            $scope.selectReportTemplateDirID = id;
            consultationService.getTemplateID(id).success(function (data) {
                $scope.popPrepareReportTemplate = data;
                $scope.popReportTemplate = $scope.popPrepareReportTemplate;
                $scope.popReportTemplate.isUserTemplate = false;
                if ($scope.popReportTemplate.userID && $scope.popReportTemplate.userID != "") {
                    $scope.popReportTemplate.isUserTemplate = true;
                }
                $scope.isAppend = false;

                $scope.closeAllPopover();
                if ($scope.selectReportTemplateDirID != id) {
                    return;
                }
                //show content
                $scope.showPopover(id);
                $scope.popoverEnter = false;

                //add event
                $timeout(function () {
                    $('.popover').on('mouseenter', function () {
                        $scope.popoverEnter = true;
                        //$('#inputContainer').find('#' + $scope.selectReportTemplateDirID).popover('show');
                    });
                    $('.popover').on('mouseleave', function () {
                        $scope.popoverEnter = false;
                        $scope.closeAllPopover();
                    });
                }, 100, true);
            })

        };

        $scope.selectReportTemplate = function () {
            if ($scope.popReportTemplate && $scope.popReportTemplate.uniqueID != '') {
                $scope.closeAllPopover();

                selectReportTemplateProcess();
            }

        };

        var selectReportTemplateProcess = function () {
            if ($scope.popReportTemplate && $scope.popReportTemplate.uniqueID != '') {
                if ($scope.isAppend) {
                    if ($scope.expertAdviceEdit != '') {
                        $scope.expertAdviceEdit += '<br/><b>' + $translate.instant("WYS") + '</b><br/>' + $scope.popReportTemplate.wysText + '<br/><b>' + $translate.instant("WYG") + '</b><br/>' + $scope.popReportTemplate.wygText;
                    }
                    else {
                        $scope.expertAdviceEdit += '<b>' + $translate.instant("WYS") + '</b><br/>' + $scope.popReportTemplate.wysText + '<br/><b>' + $translate.instant("WYG") + '</b><br/>' + $scope.popReportTemplate.wygText;
                    }

                }
                else {
                    $scope.expertAdviceEdit = '<b>' + $translate.instant("WYS") + '</b><br/>' + $scope.popReportTemplate.wysText + '<br/><b>' + $translate.instant("WYG") + '</b><br/>' + $scope.popReportTemplate.wygText;
                }
            }
            console.log($scope.popReportTemplate);

        };

        $scope.toggleExpertAdvice = function () {
            if ($('#divExpertAdvice').height() == 30) {
                $('#iconExpertAdvice').toggleClass('glyphicon-triangle-top', false);
                $('#iconExpertAdvice').toggleClass('glyphicon-triangle-bottom', true);
                $('#divExpertAdvice').height('180px');
                calAdviceEditorHeight();
            }
            else {
                $('#iconExpertAdvice').toggleClass('glyphicon-triangle-bottom', false);
                $('#iconExpertAdvice').toggleClass('glyphicon-triangle-top', true);
                $('#divExpertAdvice').height('30px');
                calAdviceEditorHeight();
            }
        };

        $scope.toggleHostAdvice = function () {
            if ($('#divHostAdvice').height() == 30) {
                $('#iconHostAdvice').toggleClass('glyphicon-triangle-top', false);
                $('#iconHostAdvice').toggleClass('glyphicon-triangle-bottom', true);
                $('#divHostAdvice').height('180px');
                calAdviceEditorHeight();
            }
            else {
                $('#iconHostAdvice').toggleClass('glyphicon-triangle-bottom', false);
                $('#iconHostAdvice').toggleClass('glyphicon-triangle-top', true);
                $('#divHostAdvice').height('30px');
                calAdviceEditorHeight();
            }
        };

        var calAdviceEditorHeight = function () {
            if ($('#divExpertAdvice').height() == 30 && $('#divHostAdvice').height() == 30) {
                $("#request-advice-text").closest(".k-editor").height('538px');
            }
            else if ($('#divExpertAdvice').height() == 30 || $('#divHostAdvice').height() == 30) {
                $("#request-advice-text").closest(".k-editor").height('388px');
            }
            else {
                $("#request-advice-text").closest(".k-editor").height('238px');
            }
        };

        $scope.refreshExpertAdvice = function () {
            $scope.expertAdvices = '';
            consultationService.getExpertAdvices($scope.advice.requestID).success(function (data) {
                angular.forEach(data, function (item) {
                    if (item.avatar) {
                        item.avatar = 'data:image/png;base64,' + item.avatar;
                    }
                    else {
                        item.avatar = '/app-resources/images/consultation/user_portrait_default.png';
                    }
                    $scope.expertAdvices += '<img class="photo" src="' + item.avatar + '">' + item.displayName + ' ' + $filter('date')(item.lastEditTime, 'yyyy-MM-dd HH:mm') + '<br/>' + item.comments + '<hr/>';
                });
                $scope.expertAdvices = $sce.trustAsHtml($scope.expertAdvices);
            });

            consultationService.getHostAdviceReport($scope.advice.requestID).success(function (item) {
                if (item.avatar) {
                    item.avatar = 'data:image/png;base64,' + item.avatar;
                }
                else {
                    item.avatar = '/app-resources/images/consultation/user_portrait_default.png';
                }
                $scope.advice.consultationAdvice = item.comments;
                $scope.hostAdviceEdit = $sce.trustAsHtml('<img class="photo" src="' + item.avatar + '">' + item.displayName + ' ' + $filter('date')(item.lastEditTime, 'yyyy-MM-dd HH:mm') + '<br/>' + $scope.advice.consultationAdvice + '<hr/>');
            });

            $scope.isUpdated = false;
        };

        (function initialize() {
            $scope.advice = $scope.val;
            if ($scope.advice && $scope.advice.consultationAdvice && $scope.advice.consultationAdvice.toString) {
                $scope.advice.consultationAdvice = $scope.advice.consultationAdvice.toString();
            }

            $scope.popPrepareReportTemplate = null;
            $scope.popReportTemplate = null;
            $scope.isAppend = false;
            $scope.popoverEnter = false;
            $scope.selectReportTemplateDirID = '';

            $scope.tdsTemplate = gettdsTemplate();
            $scope.toTemplate = {
                dataSource: $scope.tdsTemplate,
                dataTextField: 'name',
                dataValueField: 'id',
                enable: 'enabled'
            };

            if ($scope.advice.consultationReportID && $scope.advice.consultationReportID != '') {
            }
            else {
                $scope.advice.writer = loginContext.localName;
            }

            $scope.expertAdvices = '';
            $scope.expertAdviceEdit = '';
            $scope.isUpdated = false;
            consultationService.getExpertAdviceReport($scope.advice.requestID).success(function (data) {
                $scope.expertAdviceEdit = data.comments;
            });
            $scope.refreshExpertAdvice();

            commonMessageHub.subscribe($scope, commonMessageHub.Messages.ConsultationAdviceUpdate, onConsultationAdviceUpdateMessage);

        }());
    }
]);
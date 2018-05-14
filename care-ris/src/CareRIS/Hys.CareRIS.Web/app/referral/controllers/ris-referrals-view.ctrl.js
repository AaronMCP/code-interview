referralModule.controller('RisReferralsController', ['$log', '$scope', '$state', 'constants', 'enums', '$rootScope', 'referralService',
    '$filter', '$stateParams', 'dictionaryManager', 'kendoService', 'loginUser', 'reportService', '$location', '$http', 'openDialog', 'loginContext',
    'busyRequestNotificationHub', '$window', '$modal', 'application', 'clientAgentService',
function ($log, $scope, $state, constants, enums, $rootScope, referralService,
    $filter, $stateParams, dictionaryManager, kendoService, loginUser, reportService, $location, $http, openDialog, loginContext,
    busyRequestNotificationHub, $window, $modal, application, clientAgentService) {
    'use strict';

    $log.debug('RisReferralsController.ctor()...');

    function getPatientBaseInfo(dataItem) {
        var result = dataItem.localName + '&nbsp;&nbsp;&nbsp;&nbsp;';

        if (dataItem.gender === '男') {
            result += '<span class="icon-male icon-blue icon-gender-font">';
        } else if (dataItem.gender === '女') {
            result += '<span class="icon-female icon-purple icon-gender-font">';
        } else {
            result += '<span class="icon-unknown icon-orange icon-gender-font">';
        }

        if (dataItem.currentAge) {
            result += '</span>&nbsp;&nbsp;';

            var ageArry = dataItem.currentAge.split(' ');
            var age = ageArry[0];
            var ageUnit = ageArry[1];
            var a = _.findWhere($rootScope.ageUnitList, { value: ageUnit });
            if (a) {
                ageUnit = a.text;
            }
            dataItem.currentAge = age + ' ' + ageUnit;

            result += dataItem.currentAge;
        }
        return result;
    };

    $scope.dsWorkList = new kendo.data.DataSource({
        schema: {
            data: 'referrals',
            total: 'count'
        },
        serverPaging: true,
        transport: {
            read: function (options) {
                var searchCriteria = {
                    pageIndex: options.data.page,
                    pageSize: options.data.pageSize
                };

                if ($stateParams.searchCriteria) {
                    _.extend(searchCriteria, $stateParams.searchCriteria);
                    referralService.getRefferalList(searchCriteria)
                         .success(function (data) {
                             options.success(data);
                         });
                } else {
                    referralService.getRefferalList(searchCriteria)
                       .success(function (data) {
                           options.success(data);
                       });
                }
            }
        },
        pageSize: constants.pageSize
    });
    var referralWorklist = {
        dataSource: $scope.dsWorkList,
        pageable: {
            refresh: true,
            buttonCount: 5,
            input: true
        },
        scrollable: true,
        filterable: false,
        sortable: true,
        resizable: true,
        reorderable: true,
        selectable: true,
        change: function (e) {
            disableAllButton();

            var data = $scope.dsWorkList.view(),
                selected = $.map(this.select(), function (item) {
                    return data[$(item).index()];
                });

            if (selected.length > 0) {
                $scope.selectedRow = selected[0];
                //only SentFailed can resend, Canceled and Rejected can be recover to send
                if (selected[0].refStatus == enums.ReferralStatus.SentFailed) {
                    $scope.isDisabledSend = false;
                }
                else if (selected[0].refStatus == enums.ReferralStatus.Accept ||
                    selected[0].refStatus == enums.ReferralStatus.Arrived ||
                    selected[0].refStatus == enums.ReferralStatus.Sent ||
                    selected[0].refStatus == enums.ReferralStatus.SentFailed ||
                    selected[0].refStatus == enums.ReferralStatus.CancelFailed) {
                    if (selected[0].refpurpose == enums.ReferralPurpose.Reporting &&
                        selected[0].rpStatus <= enums.rpStatus.examination) {
                        $scope.isDisabledCancel = false;
                    }
                    else if (selected[0].refpurpose == enums.ReferralPurpose.AuditingReport &&
                        selected[0].rpStatus < enums.rpStatus.firstApprove) {
                        $scope.isDisabledCancel = false;
                    }
                }

                if (selected[0].refStatus == enums.ReferralStatus.Finished) {
                    $scope.isDisabledPreviewReport = false;
                    $scope.isDisabledPrintReport = false;
                }
                $scope.$digest();
            }
        },
        columns: [{
            field: 'referralID',
            title: '{{ "ReferralID" | translate }}'
        }, {
            field: 'accNo',
            title: '{{ "AccNo" | translate }}'
        },
        {
            field: 'patientBaseInfo',
            title: '{{ "PatientBaseInfo" | translate }}',
            template: function (dataItem) {
                return getPatientBaseInfo(dataItem);
            }
        },
        {
            field: 'refStatus',
            title: '{{ "RefStatus" | translate }}',
            template: function (dataItem) {

                var s = _.findWhere($scope.context.referralStatus, { value: dataItem.refStatus + '' });
                if (s) {
                    return s.text;
                }

                return "{{ dataItem.refStatus | enumValueToString :'ReferralStatus' | translate }}";
            }
        },
        {
            field: 'rpStatus',
            title: '{{ "RPStatus" | translate }}',
            template: function (dataItem) {

                var s = _.findWhere($scope.context.statusList, { value: dataItem.rpStatus + '' });
                if (s) {
                    return s.text;
                }

                return dataItem.rpStatus;
            }
        },
        {
            field: 'modalityType',
            title: '{{ "ModalityType" | translate }}'
        },
        {
            field: 'refpurpose',
            title: '{{ "Refpurpose" | translate }}',
            template: function (dataItem) {
                var s = _.findWhere($scope.context.referralPurpose, { value: dataItem.refpurpose + '' });
                if (s) {
                    return s.text;
                }

                return "{{ dataItem.refpurpose | enumValueToString :'ReferralPurpose' | translate }}";
            }
        },
        {
            field: 'siteName',
            title: '{{ "TargetSite" | translate }}'
        }, {
            field: 'createDt',
            title: '{{ "CreateTime" | translate }}',
            template: kendo.template($('#column-menu-createDate').html())
        }]
    };



    var disableAllButton = function () {
        $scope.isDisabledSend = true;
        $scope.isDisabledCancel = true;
        $scope.isDisabledPreviewReport = true;
        $scope.isDisabledPrintReport = true;
        $scope.selectedRow = null;
    };


    $scope.send = function () {
        referralService.reSend($scope.selectedRow.referralID)
                        .success(function (data) {
                            $scope.dsWorkList.page(1);
                        });
    };

    $scope.cancelReferral = function () {
        referralService.cancelReferral($scope.selectedRow.referralID)
                        .success(function (data) {
                            if (data) {
                                $scope.dsWorkList.page(1);
                            }
                            else {
                                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant('CancelReferralNotFinished'));
                            }
                        });
    };

    $scope.previewReport = function () {

        referralService.getProcedureID($scope.selectedRow.accNo, $scope.selectedRow.procedureCode)
                        .success(function (procedureId) {
                            var modalInstance = $modal.open({
                                templateUrl:'/app/referral/views/preview-report-view.html',
                                controller: 'ReferralPreviewReportController',
                                backdrop: 'static',
                                keyboard: false,
                                windowClass: 'previewReport',
                                resolve: {
                                    procedureId: function () {
                                        return procedureId;
                                    }
                                }
                            });
                        });

    };
    $scope.printReport = function () {
        referralService.getProcedureID($scope.selectedRow.accNo, $scope.selectedRow.procedureCode)
                       .success(function (procedureId) {
                           reportService.getReportPrintTemplateIDByProID(procedureId, loginContext.site, loginContext.domain).success(function (data) {
                               if (!data) {
                                   openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant('NoPrintTemplate').replace('{0}', $translate.instant('PrintReport')));
                                   return;
                               }

                               var param = {
                                   id: procedureId,
                                   site: loginContext.site,
                                   domain: loginContext.domain,
                                   url: loginContext.apiHost + '/api/v1/report/html',
                                   printtemplateid: '',
                                   printer: application.clientConfig.defaultPrinter
                               };
                               clientAgentService.printReport(param).success(function (data) {
                                   busyRequestNotificationHub.requestEnded();
                                   reportService.updateReportPrintStatusByProcedureID(procedureId, data).success(function (result) {
                                   });
                               }).error(function () {
                                   busyRequestNotificationHub.requestEnded();
                               });
                           });
                       });

    };

    (function initialize() {
        $scope.isMobile = $rootScope.browser.versions.mobile;
        $scope.context = {};
        $scope.context.referralStatus = $rootScope.referralStatus;
        $scope.context.referralPurpose = $rootScope.referralPurpose;
        $scope.context.statusList = $rootScope.statusList;

        $scope.referralWorklist = referralWorklist;

        disableAllButton();

        kendoService.grid('.referral-gird').autoResize();
        $scope.$on("$destroy", function () {
            kendoService.destroy();
        });
    }());
}
]);
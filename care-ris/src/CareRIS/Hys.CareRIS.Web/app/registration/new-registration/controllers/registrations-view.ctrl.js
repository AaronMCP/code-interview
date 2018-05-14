registrationModule.controller('RegistrationsController', [
    '$log', '$scope', 'enums', 'worklistService', 'constants', 'loginContext', 'ngGridUtil',
    '$translate', '$timeout', 'configurationService', '$q', '$rootScope',
    '$modal', 'reportService', 'registrationService', 'risDialog', '$location', '$http', 'busyRequestNotificationHub', '$window', 'registrationUtil', 'commonMessageHub',
    'application', '$filter', '$interval', 'clientAgentService', 'imManager', 'readDocTogether', 'sendReferralService', 'openDialog', 'referralService', '$state', 'csdToaster',
    function ($log, $scope, enums, worklistService, constants, loginContext, ngGridUtil,
        $translate, $timeout, configurationService, $q, $rootScope,
        $modal, reportService, registrationService, risDialog, $location, $http, busyRequestNotificationHub, $window, registrationUtil,
        commonMessageHub, application, $filter, $interval, clientAgentService, imManager, readDocTogether, sendReferralService, openDialog, referralService, $state, csdToaster) {
        'use strict';
        $log.debug('RegistrationsController.ctor()...');
        var yesNoList = application.configuration.yesNoList;
        var statusList = application.configuration.statusList;
        var ageUnitList = application.configuration.ageUnitList;
        $scope.$on(application.events.searchRegistration, function (e, criteria) {
            e.preventDefault();
            searchResult(criteria);
        });
        $scope.$on(application.events.advancedSearchRegistration, function (e, criteria) {
            e.preventDefault();
            searchResult(criteria, true);
        });
        $scope.$on('event:refreshSearch', function (e, criteria) {
            e.preventDefault();
            //get selected order id
            if (criteria && criteria.orderId) {
                $scope.selectedOrderId = criteria.orderId;
            }
            $scope.registionsGrid.dataSource.read();
        });
        var currentSearchCriteria = {};


        //我的日期处理
        var turnRecentDaysToDateRange = function (criteria) {
            var endDate = new Date(new Date(new Date().toLocaleDateString()).getTime() + 24 * 60 * 60 * 1000 - 1);

            if (criteria.createTimeType === 'days') {
                if (_.isString(criteria.createDays) && criteria.createDays.trim().length > 0) {
                    criteria.createDays = parseInt(criteria.createDays);
                }

                if (_.isNumber(criteria.createDays) && criteria.createDays >= 0) {
                    var startDate = new Date(endDate);
                    startDate.setDate(endDate.getDate() - (Math.max(1, criteria.createDays)));
                    criteria.createStartDate = startDate;
                    criteria.createEndDate = endDate;
                } else {
                    criteria.createStartDate = null;
                    criteria.createEndDate = null;
                }
            }

            if (criteria.examineTimeType === 'days') {
                if (_.isString(criteria.examineDays) && criteria.examineDays.trim().length > 0) {
                    criteria.examineDays = parseInt(criteria.examineDays);
                }

                if (_.isNumber(criteria.examineDays) && criteria.examineDays >= 0) {
                    var startDate = new Date(endDate);
                    startDate.setDate(endDate.getDate() - (Math.max(1, criteria.examineDays)))//- 1;

                    criteria.examineStartDate = startDate;
                    criteria.examineEndDate = endDate;
                } else {
                    criteria.examineStartDate = null;
                    criteria.examineEndDate = null;
                }
            }
        };

        //我的查询
        var searchResult = function (criteria, isAdvancedSearch) {
            currentSearchCriteria = criteria;
            if (isAdvancedSearch) {
                turnRecentDaysToDateRange(currentSearchCriteria);
            }
            $scope.registionsGrid.dataSource.page(1);
        };

        //我的結果遍历
        var eachSearchResult = function (data) {
            if (data.total == 0) {
                return data;
            }
            //reset refresh icon
            $scope.isOrderUpdated = false;
            if (!data) return;
            $scope.orderItems = data.data;
            if ($scope.orderItems && $scope.orderItems.length > 0) {
                $scope.selectedItemIndex = 0;
                angular.forEach($scope.orderItems, function (item, index) {
                    if ($scope.selectedOrderId && $scope.selectedOrderId != '' && $scope.selectedOrderId == item.orderID) {
                        $scope.selectedItemIndex = index + 1;
                        $scope.selectedOrderId = "";
                    }
                    item.summary = null;
                    item.patientNameNo = item.patientName + '<br/>' + item.patientNo;
                    item.modalityType = '';
                    item.modalities = '';
                    item.rpDescs = [];
                    item.examineTimes = '';
                    item.statuses = '';
                    item.isPrints = '';
                    item.isExistImages = '';
                    item.examineTimes = [];
                    if (item.procedures) {
                        var maxStatus = 0;
                        var maxPrintStatus = 0;
                        var maxExistImages = 0;
                        angular.forEach(item.procedures, function (procedure) {
                            item.modalityType = procedure.modalityType;
                            item.modalities += procedure.modality + '<br/>';
                            item.rpDescs.push(procedure.rpDesc);

                            var examineTime = $filter('date')(procedure.examineTime, 'yyyy-MM-dd HH:mm');
                            //item.examineTimes += procedure.examineTime ? '<div  class="white-space" title="{{examineTime}}">' + $scope.examineTime + '</div>' : '';

                            item.examineTimes.push(examineTime);
                            // for status sort, if order has more than one procedure, the status will be ordered by the max value of them
                            if (procedure.status > maxStatus) {
                                maxStatus = procedure.status;
                            }

                            maxPrintStatus = updateIsPrints(item, procedure);

                            var s = _.findWhere(statusList, {
                                value: procedure.status + ''
                            });
                            if (s) {
                                item.statuses += s.text + '<br/>';
                            } else {
                                item.statuses += procedure.status + '<br/>';
                            }
                            item.status = procedure.status;
                            s = _.findWhere(yesNoList, {
                                value: procedure.isExistImage + ''
                            });

                            if (s) {
                                item.isExistImages += s.text + '<br/>';
                                maxExistImages = procedure.isExistImage;
                            } else {
                                maxExistImages = 0;
                                item.isExistImages += procedure.isExistImage + '<br/>';
                            }

                            // translate age unit
                            var ageArry = item.currentAge.split(' ');
                            var age = ageArry[0];
                            var ageUnit = ageArry[1];
                            var a = _.findWhere(ageUnitList, {
                                value: ageUnit
                            });
                            if (a) {
                                ageUnit = a.text;
                            }
                            item.currentAge = age + ' ' + ageUnit;
                        });
                        item.maxStatus = maxStatus;
                        item.maxPrintStatus = maxPrintStatus;
                        item.maxExistImages = maxExistImages;
                    }
                });
                data.data = $scope.orderItems
                return data;
                //selectOrder($scope.orderItems[selectedItemIndex]);
            } else {
                clearSelectOrder();
            }

            //$scope.searched = $scope.orderItems.length > 0;
            //$scope.accNoHighlightString = '';
            //$scope.patientNoHighlightString = '';
            //$scope.patientNameHighlightString = '';
        };

        //选择了 触发了
        var selectOrder = function (order) {
            $scope.selectedOrder = order;
            $scope.selectedOrderId = order.orderID;
            angular.forEach($scope.orderItems, function (o) {
                o.isSelected = o === order;
            });
            var result = registrationUtil.selectOrder(order);
            $scope.writeReprotCount = result.writeReprotCount || 0;
            $scope.dataWriteReprot = result.dataWriteReprot || [];
            $scope.dataViewReprot = result.dataViewReprot || [];
            $scope.dataFinishExam = result.dataFinishExam || [];
            //set button
            $scope.isDisabledWriteReport = result.isDisabledWriteReport || false;
            $scope.isDisabledPreviewReport = result.isDisabledPreviewReport || false;
            $scope.isDisabledFinishExam = result.isDisabledFinishExam || false;
            $scope.isDisabledLoadImage = result.isDisabledLoadImage || false;
            $scope.isDisabledTransferReg = result.isDisabledTransferReg || false;
            $scope.isDisabledReferral = !sendReferralService.judgeCanReferralByOrder(order, 0);

            if ($rootScope.browser.versions.mobile) {
                reportService.getPacsUrl($scope.selectedOrder.procedures[0].procedureID).success(function (data) {
                    $("#registrationsLoadImage").attr('href', data);
                    $("#registrationsLoadImage").attr('target', "_blank");

                    setAutoLoadImageForSafari(data);
                });
            }
        };

        //凹
        $scope.registionsGrid = {
            dataSource: new kendo.data.DataSource({
                schema: {
                    data: 'data',
                    total: 'total'
                },
                serverPaging: true,
                transport: {
                    read: function (options) {
                        currentSearchCriteria.Pagination = {
                            pageIndex: options.data.page,
                            pageSize: constants.pageSize

                        }
                        var searchCriteria = currentSearchCriteria;

                        worklistService.worksSearch(currentSearchCriteria).success(function (data) {
                            $log.debug('searchContext.search() successfully.');
                            options.success(eachSearchResult(data));
                            //默认选中某行
                            if ($scope.selectedItemIndex && $scope.selectedItemIndex > 0) {
                                var row = $('.registionsGrid tbody tr:nth-child(' + $scope.selectedItemIndex + ')');
                                $scope.registionsGrid.select(row);
                            }
                            else {//第一行
                                var row = $('.registionsGrid tbody tr:nth-child(1)');
                                $scope.registionsGrid.select(row);
                            }
                        });
                    },
                },
                pageSize: constants.pageSize,
                sort: [{ field: "accNo", dir: "desc" }],
            }),
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
            columns: [{
                field: 'accNo',
                title: $translate.instant('AccNo'),
                template: '<a href="javascript:void(0);" ng-bind-html="dataItem.accNo | risTextHighlight : $parent.accNoHighlightString" ng-click="viewOrder(dataItem)"></a>',
                attributes: {
                    'title': '{{dataItem.accNo}}',
                    style: "word-wrap:break-word;"
                },
                width: '146px'

            },
           {
               field: 'patientName',
               title: $translate.instant('PatientNameAndNo'),
               template: '<a href="javascript:void(0);" class="ris-popover" ng-mouseenter="$parent.prepareSummary(dataItem)" ng-click="$parent.showSummary()"' +
                         'ng-bind-html="dataItem.patientName | risTextHighlight : $parent.patientNameHighlightString" ris-popover popover-container="body" popover-auto-hide="true" data-overwrite="true"' +
                         ' use-optimized-placement-algorithm="true" data-placement="auto right left bottom top" data-templateurl="/app/registration/views/patient-registration-summary-view.html"></a>' +
                          ' <div><span style="font-size:12px;"ng-bind-html="dataItem.patientNo | risTextHighlight : $parent.patientNoHighlightString"></span></div>',
               width: '111px'

           },
           {
               field: 'currentAge',
               title: $translate.instant('Age'),
               width: '66px'

           },
           {
               field: 'createdTime',
               title: $translate.instant('CreateTime'),
               template: '{{dataItem.createdTime|date:"yyyy-MM-dd HH:mm"}}',
               width: '156px'

           },
           {
               field: 'patientType',
               title: $translate.instant('PatientType'),
               width:'86px'

           },
           {
               field: 'modalityType',
               title: $translate.instant('ModalityType'),
               width: '86px'

           },
           {
               field: 'modalities',
               title: $translate.instant('Modality'),
               template: '<span ng-bind-html="dataItem.modalities"></span>',
               width: '86px'

           },
           {
               field: 'rpDescs',
               title: $translate.instant('Procedure'),
               template: '<div class="white-space" ng-repeat="r in dataItem.rpDescs track by $index" title={{r}}>{{r}}</div>',
               width: '86px',


           },
           {
               field: 'examineTimes',
               title: $translate.instant('ExamineTime'),
               template: '<div class="white-space" ng-repeat="d in dataItem.examineTimes track by $index" title="{{d}}">{{d}}</div>',
               width: '156px'

           },
           {
               field: 'statuses',
               title: $translate.instant('Status'),
               template: '<span ng-bind-html="dataItem.statuses"></span>',
               width: '96px'
           },
           {
               field: 'isPrints',
               title: $translate.instant('Printed'),
               template: '<span ng-bind-html="dataItem.isPrints"></span>',
               width: '76px'
           },
           {
               field: 'isExistImages',
               title: $translate.instant('ExistImage'),
               template: '<span ng-bind-html="dataItem.isExistImages"></span>',
               width: '76px'
           }
            ],
            selectable: "row",
            change: function (e) {
                var selectedRows = this.select();
                var dataItem = this.dataItem(selectedRows[0]);
                $timeout(function () {
                    selectOrder(dataItem);
                });
            }
        };
        var setAutoLoadImageForSafari = function (data) {
            if ($scope.isPACSIntegration && !$scope.isDisabledLoadImage && $scope.isAutoLoadImage) {
                $("#writeReportLoadImage").attr('href', data);
                $("#writeReportLoadImage").attr('target', "_blank");

                $("#relateReportLoadImage").attr('href', data);
                $("#relateReportLoadImage").attr('target', "_blank");

                $("#listLoadImage a").attr('href', data);
                $("#listLoadImage a").attr('target', "_blank");
            } else {
                var herfString = "javascript:void(0);";
                $("#writeReportLoadImage").attr('href', herfString);
                $("#writeReportLoadImage").removeAttr('target');

                $("#relateReportLoadImage").attr('href', herfString);
                $("#relateReportLoadImage").removeAttr('target');

                $("#listLoadImage a").attr('href', herfString);
                $("#listLoadImage a").removeAttr('target');
            }
        };

        var selectLockedProcedure = function (procedureItem) {
            //procedureItem.procedureID
            registrationUtil.selectLockedProcedure(procedureItem, $scope.selectedOrder.orderID);
        };

        var selectRelateReport = function () {
            //get exam rocedureid
            registrationUtil.selectRelateReport($scope.dataWriteReprot, $scope.selectedOrder.orderID, 0);
        };

        var selectViewReport = function () {
            registrationUtil.selectViewReport($scope.dataViewReprot, 0);
        };
        var previewReport = function (id) {
            if (id) {
                $state.go('ris.report', {
                    isPreview: true,
                    from: 0,
                    procedureId: id
                });
            }

        };

        var updateReadingDoctor = function () {
            configurationService.getImOnlineUser($scope.readDocTog.site).success(function (users) {
                var len;
                $scope.imOnlineUsers = users || [];
                len = $scope.imOnlineUsers.length;
                $scope.readDocTog.doctor = len > 0 ? $scope.imOnlineUsers[0] : {};
            }).error(function (errorMsg) {
                $log.error(errorMsg);
            });
        };

        var printReport = function (procedureId, orderId) {
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

                        if (result) {
                            var item = _.findWhere($scope.orderItems, {
                                orderID: orderId
                            });

                            item.isPrints = '';

                            angular.forEach(item.procedures, function (procedure) {
                                if (procedure.procedureID && procedure.procedureID == procedureId) {
                                    procedure.isPrint = 1;

                                    var relatedReport = _.where(item.procedures, {
                                        reportID: procedure.reportID
                                    });
                                    angular.forEach(relatedReport, function (r) {
                                        r.isPrint = 1;
                                    });
                                    angular.forEach(item.procedures, function (procedure) {
                                        updateIsPrints(item, procedure);
                                    });
                                }
                            });
                        }
                    });
                }).error(function () {
                    busyRequestNotificationHub.requestEnded();
                });
            });
        };

        var clearSelectOrder = function () {
            $scope.selectedOrder = null;
            $scope.isDisabledWriteReport = true;
            $scope.isDisabledPreviewReport = true;
            $scope.isDisabledFinishExam = true;
        };

        var selectProcedure = function (procedure) {
            $scope.selectedProcedure = procedure;
            $scope.selectedProcedure.hasNoReport = procedure.reportID ? false : true;
            angular.forEach($scope.targetOrder.summary, function (p) {
                p.isSelected = p === procedure;
            });
        };

        var prepareSummary = function (order) {
            $scope.hoveredOrder = order;
            if (order.summary === null) {
                registrationService.getProcedures(order.patientID).success(function (summary) {
                    order.summary = summary;
                });
            }
        };

        var showSummary = function () {
            $scope.flag = true;
            $scope.targetOrder = $scope.hoveredOrder;
            selectProcedure($scope.targetOrder.summary[0]);
        };

        var viewOrder = function (order) {
            if (order) {
                $state.go('ris.registration', { orderId: order.orderID });
            } else if ($scope.selectedOrder) {
                $state.go('ris.registration', { orderId: $scope.selectedOrder.orderID });
            }
        };

        var printCurrentPage = function () {
            printReports($scope.orderItems);
        };

        var printAllPages = function () {
            openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant('ConfirmPrintAllPages'), function () {
                var criteria = angular.copy(currentSearchCriteria);
                criteria.pagination.deedNoPagination = true;
                worklistService.advancedSearch(criteria).success(function (data) {
                    if (data && data.orderItems) {
                        printReports(data.orderItems);
                    }
                });

            })
        };

        var printReports = function (orders) {
            var orderProcedures = _.map(orders, function (order) {
                return order.procedures;
            });
            var procedures = _.flatten(orderProcedures, true);
            //procedures = _.uniq(procedures, false, function (p) { return p.reportID });
            var reportIds = [];
            for (var i = procedures.length - 1; i > -1; i--) {
                var pro = procedures[i];
                if (_.isNull(pro.reportID) || _.contains(reportIds, pro.reportID)) {
                    procedures.splice(i, 1);
                } else {
                    reportIds.push(pro.reportID);
                }
            }

            if (procedures.length === 0) {
                return;
            }

            var warning = function () {
                if (hasFailed) {
                } else {
                    //refresh();
                }
            };
            var doWarning = _.after(procedures.length, warning);
            var hasFailed = false;

            angular.forEach(procedures, function (procedure) {
                printSingleReport(procedure.procedureID, function (data) {
                    reportService.updateReportPrintStatusByProcedureID(procedure.procedureID, data).success(function (result) {

                        if (result) {
                            var currentPageOrderIds = _.map($scope.orderItems, function (order) {
                                return order.orderID;
                            })

                            if (_.contains(currentPageOrderIds, procedure.orderId)) {

                                var item = _.findWhere($scope.orderItems, {
                                    orderID: procedure.orderId
                                });
                                var pro = _.findWhere(item.procedures, {
                                    procedureID: procedure.procedureID
                                });

                                item.isPrints = '';
                                pro.isPrint = 1;

                                var relatedReport = _.where(item.procedures, {
                                    reportID: pro.reportID
                                });

                                if (relatedReport && relatedReport.length > 1) {
                                    angular.forEach(relatedReport, function (r) {
                                        r.isPrint = 1;
                                    });
                                }

                                angular.forEach(item.procedures, function (p) {
                                    updateIsPrints(item, p);
                                });

                            }
                        }
                    });
                }, function () {
                    hasFailed = true;
                }, function () {
                    // This method will ne only called after last procedure has been processed.
                    doWarning();
                });
            });

        };

        var printSingleReport = function (procedureId, onSuccess, onFail, onFinaly) {
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
                    onSuccess(data);
                }).error(function () {
                    onFail();
                }).finally(function () {
                    onFinaly();
                });
            });
        };

        var finishExam = function () {
            //check if it is locked, if locked, tell user about it,else finish exam 
            registrationUtil.finishExam($scope.selectedOrder.orderID, $scope.selectedOrder.accNo, finishExamSuccess);
        };

        var finishExamSuccess = function () {
            $scope.selectedOrderId = $scope.selectedOrder.orderID;
            //searchContext.refresh();
            $scope.registionsGrid.dataSource.read();

            //notify message
            var orderUpdateParams = new commonMessageHub.OrderUpdateParams();
            orderUpdateParams.uniqueID = $scope.selectedOrder.orderID;
            commonMessageHub.publish(commonMessageHub.Messages.OrderUpdated, orderUpdateParams);
        };
        var transferBookingToReg = function () {
            registrationUtil.transferBookingToReg($scope.selectedOrderId, transferBookingToRegCallback);
        };
        var transferBookingToRegCallback = function () {
            // searchContext.refresh();
            $scope.registionsGrid.dataSource.read();
        };
        var openPACSImageViewer = function () {
            var order = $scope.selectedOrder;
            var parameter = {
                patientId: order.patientNo,
                accNo: order.accNo,
                studyId: order.studyInstanceUID
            };
            clientAgentService.
                viewImage(parameter).success(function (result) {
                    if (!result) {
                        csdToaster.warning('不能查看图像，请检查PACS配置！');
                    }
                });
        };

        var onOrderUpdatedMessage = function (e, params) {
            $scope.isOrderUpdated = true;
            //update UI
            $scope.$digest();
        };

        var setAutoLoadImage = function () {
            application.configuration.isAutoLoadImage = !application.configuration.isAutoLoadImage;
            $scope.isAutoLoadImage = application.configuration.isAutoLoadImage;
            var userProfile = {
                name: 'AutoLoadImage',
                value: '0'
            };
            if ($scope.isAutoLoadImage) {
                userProfile.value = '1';
            }
            if ($rootScope.browser.versions.mobile) {
                reportService.getPacsUrl($scope.selectedOrder.procedures[0].procedureID).success(function (data) {
                    setAutoLoadImageForSafari(data);
                });
            }
            configurationService.SaveUserProfiles(userProfile).success(function (data) { });
        };

        var updateIsPrints = function (item, procedure) {
            if (!procedure.isPrint) {
                procedure.isPrint = 0;
            }
            var maxPrintStatus = 0;

            var s = _.findWhere(yesNoList, {
                value: procedure.isPrint + ''
            });
            if (s) {
                item.isPrints += s.text + '<br/>';
                maxPrintStatus = procedure.isPrint;
            } else {
                maxPrintStatus = 0;
                item.isPrints += procedure.isPrint + '<br/>';
            }

            return maxPrintStatus;
        };

        var calledTheNumber = function () {
            alert("叫号成功");
        }

        var viewRequisition = function () {
            registrationUtil.viewRequisition($scope.selectedOrder.accNo);
        };

        $scope.sendReferral = function () {
            sendReferralService.judgeLockAndSendByOrderID($scope.selectedOrder.orderID);
        };

        // initialzation
        (function initialize() {
            $log.debug('RegistrationsController.initialize()...');

            $scope.isMobile = $rootScope.browser.versions.mobile;

            $scope.orderItems = null;
            $scope.selectedOrder = null;
            $scope.selectOrder = selectOrder;
            $scope.order = null;
            $scope.maxSize = 5;
            $scope.searched = false;
            $scope.isDisabledWriteReport = true;
            $scope.isDisabledPreviewReport = true;
            $scope.isDisabledFinishExam = true;
            $scope.isDisabledLoadImage = true;
            $scope.isDisabledTransferReg = true;
            $scope.isDisabledReferral = true;

            $scope.dataWriteReprot = [];
            $scope.dataViewReport = [];
            $scope.dataFinishExam = [];
            $scope.writeReprotCount = 0;
            $scope.enums = enums;
            $scope.prepareSummary = prepareSummary;
            $scope.showSummary = showSummary;
            $scope.selectProcedure = selectProcedure;
            $scope.selectLockedProcedure = selectLockedProcedure;
            $scope.selectRelateReport = selectRelateReport;
            $scope.viewOrder = viewOrder;
            $scope.selectViewReport = selectViewReport;
            $scope.previewReport = previewReport;
            $scope.selectedOrderId = '';
            $scope.printReport = printReport;
            $scope.printCurrentPage = printCurrentPage;
            $scope.printAllPages = printAllPages;
            $scope.finishExam = finishExam;
            $scope.transferBookingToReg = transferBookingToReg;
            $scope.openPACSImageViewer = openPACSImageViewer;
            $scope.setAutoLoadImage = setAutoLoadImage;

            $scope.isOrderUpdated = false;
            $scope.statusList = statusList;

            $scope.calledTheNumber = calledTheNumber;
            $scope.viewRequisition = viewRequisition;
            //scrible message
            commonMessageHub.subscribe($scope, commonMessageHub.Messages.OrderUpdated, onOrderUpdatedMessage);

            application.configuration.canReferral = false;
            referralService.getCanReferral().success(function (data) {
                application.configuration.canReferral = data;
            });
        }());
    }
]);
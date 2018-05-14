registrationModule.controller('RegistrationController', [
    '$log', '$scope', 'enums', '$rootScope', '$location', '$window', 'registrationService', 'loginContext',
    '$http', 'risDialog', '$translate', '$modal', 'registrationUtil', 'reportService', '$timeout', 'commonMessageHub',
    'application', 'configurationService', '$filter', 'openDialog', 'csdToaster', '$state',
function ($log, $scope, enums, $rootScope, $location, $window, registrationService, loginContext,
    $http, risDialog, $translate, $modal, registrationUtil, reportService, $timeout, commonMessageHub,
    application, configurationService, $filter, openDialog, csdToaster, $state) {
    'use strict';
    $log.debug('RegistrationController.ctor()...');

    var configurationData = application.configuration;
    var selectedModality, slice = {}, rpStatus = enums.rpStatus;

    var getOrder = function () {
        registrationService.getRegistrationInfo($scope.orderId).success(function (registrationView) {
            if (registrationView) {
                $scope.registration = registrationView.registration;
                $scope.order = registrationView.registration.order;
                $scope.patient = registrationView.registration.patient;
                $scope.procedures = registrationView.registration.procedures;
                $scope.accNo = $scope.registration.order.accNo;
                var procedure = $scope.registration.procedures[0];
                $scope.modalityType = procedure.modalityType;
                // get modality data
                registrationService.getBookingModalities($scope.modalityType, loginContext.site).success(function (data) {
                    var disabledModalities = (application.clientConfig && application.clientConfig.disabledModalities) ? application.clientConfig.disabledModalities.split('|') : null;
                        if (disabledModalities) {
                            $scope.modalities = _.reject(data, function (p) {
                                return _.contains(disabledModalities, p.uniqueID);
                            });
                        } else {
                            $scope.modalities = data;
                        }
                });
                $scope.status = procedure.status;
                $scope.isBooking = $scope.status === enums.rpStatus.noCheck;
                if ($scope.isBooking) {
                    slice.modality = procedure.modality;
                    slice.modalityType = procedure.modalityType;
                    slice.bookingBeginTime = procedure.bookingBeginTime;
                    slice.bookingEndTime = procedure.bookingEndTime;
                    slice.bookingTimeAlias = procedure.bookingTimeAlias;
                }
                $scope.orderItem = registrationView.orderItem;
                selectOrder(registrationView.orderItem);
                $scope.order.currentAge = translateAge($scope.order.currentAge, configurationData.ageUnitList);

                if ($rootScope.browser.versions.mobile) {
                    reportService.getPacsUrl($scope.procedures[0].uniqueID).success(function (data) {
                        $("#registrationLoadImage").attr('href', data);
                        $("#registrationLoadImage").attr('target', "_blank");

                        if ($scope.isPACSIntegration && !$scope.isDisabledLoadImage && application.configuration.isAutoLoadImage) {
                            $("#rwriteReportLoadImage").attr('href', data);
                            $("#rwriteReportLoadImage").attr('target', "_blank");

                            $("#rrelateReportLoadImage").attr('href', data);
                            $("#rrelateReportLoadImage").attr('target', "_blank");

                            $("#rlistLoadImage a").attr('href', data);
                            $("#rlistLoadImage a").attr('target', "_blank");
                        }
                    });
                }
            }
        });
    };

    var returnToSearchResult = function () {
        $rootScope.refreshSearch({
            orderId: $scope.order.uniqueID
        });
        $state.go('ris.worklist.registrations')
    };

    var printRequisition = function () {
        registrationUtil.printRequisition($scope.status, $scope.accNo, $scope.modalityType);
    };

    var printBarCode = function () {
        registrationUtil.printBarCode($scope.status, $scope.accNo, $scope.modalityType);
    };

    var selectOrder = function (order) {
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
    };

    var selectLockedProcedure = function (procedureItem) {
        //procedureItem.procedureID
        registrationUtil.selectLockedProcedure(procedureItem, $scope.order.uniqueID);
    };

    var selectRelateReport = function () {
        //get exam rocedureid
        registrationUtil.selectRelateReport($scope.dataWriteReprot, $scope.order.uniqueID, 1);
    };

    var selectViewReport = function () {
        registrationUtil.selectViewReport($scope.dataViewReprot, 1);
    };

    var finishExam = function () {
        //check if it is locked, if locked, tell user about it,else finish exam 
        registrationUtil.finishExam($scope.order.uniqueID, $scope.accNo, changeFinishExamStatus);
    };

    var transferBookingToReg = function () {
        registrationUtil.transferBookingToReg($scope.order.uniqueID, transferBookingToRegCallback);
    };

    var transferBookingToRegCallback = function () {
        $scope.isDisabledTransferReg = true;
        $scope.isBooking = false;
        refreshProcedures();
    };

    var openPACSImageViewer = function () {
        if (!$rootScope.browser.versions.mobile) {
            //check if it is locked, if locked, tell user about it,else finish exam 
            registrationUtil.openPACSImageViewer($scope.procedures[0].uniqueID);
        }
    };

    var changeFinishExamStatus = function () {
        $scope.isDisabledFinishExam = true;
        refreshProcedures();
        //notify message
        var orderUpdateParams = new commonMessageHub.OrderUpdateParams();
        orderUpdateParams.uniqueID = $scope.order.uniqueID;
        commonMessageHub.publish(commonMessageHub.Messages.OrderUpdated, orderUpdateParams);
    };

    var editPatient = function () {
        registrationService.getPatient($scope.patient.uniqueID).success(function (data) {
            if (data) {
                var modalInstance = $modal.open({
                    templateUrl: '/app/registration/views/patient-edit-view.html',
                    controller: 'PatientEditController',
                    backdrop: 'static',
                    keyboard: false,
                    size: 'lg',
                    resolve: {
                        patient: function () {
                            return data;
                        },
                        order: function () {
                            return $scope.order;
                        },
                        configurationData: function () {
                            return configurationData;
                        }
                    }
                });
                modalInstance.result.then(function (result) {
                    $scope.patient = result.patient;
                    // translate age unit
                    var age = translateAge(result.order.currentAge);
                    $scope.order.currentAge = age;
                });
            }
        });
    };

    var translateAge = function (currentAge, ageUnitList) {
        var ageArry = currentAge.split(' '), correctLength = 2;
        if (ageArry.length !== correctLength) {
            return currentAge;
        }
        var age = ageArry[0];
        var ageUnit = ageArry[1];
        var a = _.findWhere(configurationData.ageUnitList, {
            value: ageUnit
        });
        if (a) {
            ageUnit = a.text;
        }
        return age + ' ' + ageUnit;
    };

    var editOrder = function () {
        registrationService.getOrder($scope.order.uniqueID).success(function (data) {
            var modalInstance = $modal.open({
                templateUrl: '/app/registration/views/order-edit-view.html',
                controller: 'OrderEditController',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    order: function () {
                        return data;
                    },
                    configurationData: function () {
                        return configurationData;
                    }
                }
            });
            modalInstance.result.then(function (result) {
                $scope.order = result;
                $scope.order.currentAge = translateAge(result.currentAge);
            });
        });
    };

    var addProcedureCallback = function () {
        if (this.length > 0) {
            //status改为已检查
            if (validateExamStatus()) {
                _.each(this, function (p, i) {
                    p.status = rpStatus.examination;
                });
            }
            var data = {
                procedures: this,
                orderID: $scope.order.uniqueID
            };
            registrationService.addProcedure(data).success(function (data) {
                //notify message
                var orderUpdateParams = new commonMessageHub.OrderUpdateParams();
                orderUpdateParams.uniqueID = $scope.order.uniqueID;
                commonMessageHub.publish(commonMessageHub.Messages.OrderUpdated, orderUpdateParams);
            })
                .error(function (data) {
                    var title = $translate.instant("Tips");
                    var content = $translate.instant("SaveErrorMsg");
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
                    console.log("save procedures failed.");
                }).finally(function () {
                    refreshProcedures();
                });
        }
    };

    var addProcedure = function () {
        var modalityTypes = $scope.procedures[0] ? [$scope.procedures[0].modalityType] : null;
        var data = { procedures: $scope.procedures, modalityTypes: modalityTypes };
        if ($scope.isBooking) {
            data.bookingSlice = {
                bookingBeginTime: slice.bookingBeginTime,
                bookingEndTime: slice.bookingEndTime,
                bookingTimeAlias: slice.bookingTimeAlias,
                modality: slice.modality
            };
        }
        registrationUtil.openProcedureWindow(addProcedureCallback, data);
    };

    var updateProcedureCallBack = function () {
        if (this) {
            var oldProcedure = this.oldProcedure;
            var procedure = this.procedure;
            procedure.orderID = $scope.order.uniqueID;
            // the same procedure data,return
            if (oldProcedure.procedureCode === procedure.procedureCode && oldProcedure.modality === procedure.modality) {
                return;
            }
            if (validateExamStatus()) {
                procedure.mender = loginContext.userId;
            }
            registrationService.updateProcedure(oldProcedure.uniqueID, procedure).success(function (data) {
                //notify message
                var orderUpdateParams = new commonMessageHub.OrderUpdateParams();
                orderUpdateParams.uniqueID = $scope.order.uniqueID;
                commonMessageHub.publish(commonMessageHub.Messages.OrderUpdated, orderUpdateParams);
            })
                .error(function (data) {
                    var title = $translate.instant("Tips");
                    var content = $translate.instant("UpdateErrorMsg");
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
                    console.log("Update procedures failed.");
                }).finally(function () {
                    refreshProcedures();
                });
        }
    };

    var updateProcedure = function (procedure) {
        registrationService.getProcedure(procedure.uniqueID).success(function (procedure) {
            if (!procedure) {
                var title = $translate.instant("Tips");
                var content = $translate.instant("DeletedInfoMsg");
                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
                refreshProcedures();
                return;
            }
            var data = { updateProcedure: procedure, procedures: $scope.procedures, modalityTypes: [procedure.modalityType] };
            if ($scope.isBooking) {
                data.bookingSlice = {
                    bookingBeginTime: slice.bookingBeginTime,
                    bookingEndTime: slice.bookingEndTime,
                    bookingTimeAlias: slice.bookingTimeAlias,
                    modality: slice.modality
                };
            }
            registrationUtil.openProcedureWindow(updateProcedureCallBack, data);
        });

    };

    var deleteProcedure = function (deleteItem) {
        var title = $translate.instant("Tips");
        var content = $translate.instant("IsDeleteProcedureWarning");
        if ($scope.procedures.length === 1) {
            content = $translate.instant("DeleteProcedureLimitMsg");
            csdToaster.pop('error', content, '');
            return;
        }
        openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, title, content, function () {
            if (deleteItem.status > 50) {
                content = $translate.instant("CanNotDeleteMsg");
                csdToaster.pop('error', content, '');
                return;
            }

            registrationService.deleteProcedure(deleteItem.uniqueID).success(function (data) {
                if (data === -1) {
                    content = $translate.instant("CanNotDeleteMsg");
                    csdToaster.pop('error', content, '');
                } else if (data === -2) {
                    content = $translate.instant("DeleteProcedureLimitMsg");
                    csdToaster.pop('error', content, '');
                }
                    // success
                else if (data === 0) {
                    //notify message
                    var orderUpdateParams = new commonMessageHub.OrderUpdateParams();
                    orderUpdateParams.uniqueID = $scope.order.uniqueID;
                    commonMessageHub.publish(commonMessageHub.Messages.OrderUpdated, orderUpdateParams);
                }

            })
                .error(function (data) {
                    content = $translate.instant("DeleteErrorMsg");
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
                    console.log("delete procedures failed.");
                }).finally(function () {
                    refreshProcedures();
                });
        });
    };

    var prepareSummary = function (row) {
        $scope.hoverReport = row;
    };

    var showSummary = function () {
        $scope.report = $scope.hoverReport;
    };

    var getProcedureHistory = function () {
        $scope.isCollapsed = !$scope.isCollapsed;
        if (!$scope.isCollapsed) {
            registrationService.getProcedures($scope.patient.uniqueID, $scope.order.uniqueID).success(function (data) {
                $scope.procedureHistoryItems = data;
            });
        }
    };

    var refreshProcedures = function () {
        // refresh procedures history table
        registrationService.getProcedures($scope.patient.uniqueID, $scope.order.uniqueID).success(function (data) {
            $scope.procedureHistoryItems = data;
        });
        registrationService.getProceduresByOrderID($scope.order.uniqueID).success(function (data) {
            $scope.procedures = data;
            $scope.orderItem.procedures = [];
            _.each(data, function (item) {
                var procedure = {};
                procedure.procedureID = item.uniqueID;
                procedure.modalityType = item.modalityType;
                procedure.rpDesc = item.rpDesc;
                procedure.modality = item.modality;
                procedure.reportID = item.reportID;
                procedure.status = item.status;
                $scope.orderItem.procedures.push(procedure);
            });
            selectOrder($scope.orderItem);
        });
    };

    //add procedure，when all the rps status is >= 50,the new rp's have to be set 50
    var validateExamStatus = function () {
        if ($scope.procedures.length === 0) {
            return false;
        }
        var index = _.findIndex($scope.procedures, function (item) {
            return item.status < rpStatus.examination;
        });
        if (index === -1) {
            return true;
        } else {
            return false;
        }
    };

    var createNewFlup = function () {
        $rootScope.$broadcast('event:showNewRegistration', { patient: $scope.patient });
    };

    var showUpdateModalityView = function (row) {
        $scope.selectedProcedure = _.clone(row);
        selectedModality = row.modality;
    };

    var updateModality = function (selectedprocedure) {
        if (selectedprocedure.modality === selectedModality) {
            $scope.hideModalityPopOver(selectedprocedure.uniqueID);
            return;
        }
        registrationService.updateProcedure(selectedprocedure.uniqueID, selectedprocedure).success(function (data) {
            $scope.hideModalityPopOver(selectedprocedure.uniqueID);
            //notify message
            var orderUpdateParams = new commonMessageHub.OrderUpdateParams();
            orderUpdateParams.uniqueID = $scope.order.uniqueID;
            commonMessageHub.publish(commonMessageHub.Messages.OrderUpdated, orderUpdateParams);
        })
         .error(function (data) {
             var title = $translate.instant("Tips");
             var content = $translate.instant("UpdateErrorMsg");
             openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
             console.log("Update procedures failed.");
         }).finally(function () {
             refreshProcedures();
         });
    };

    $scope.$on('event:refreshProcedures', function (e) {
        e.preventDefault();
        refreshProcedures();
    });

    var scan = function () {
        registrationService.validateFtp().success(function (result) {
            if (result) {
                var erNo, relativePath;
                // isScan ditermine weather the requisition has files or not
                if ($scope.order.isScan) {
                    registrationService.downLoadRequisitionFiles($scope.accNo).success(function (result) {
                        if (result && result.length > 0) {
                            // has requisition already
                            erNo = result[0].fileName.substr(0, result[0].fileName.length - 7);
                            relativePath = result[0].relativePath;
                            showRequisitionWindow(result, erNo, relativePath);
                        } else {
                            scanNewRequisition();
                        }
                    });
                } else {
                    scanNewRequisition();
                }
            }
            else {
                registrationUtil.showFTPError();
            }
        });
    };
    // has no requisitions,generate new erno to send 
    var scanNewRequisition = function () {
        registrationService.generateErNo().success(function (result) {
            var erNo = result;
            var relativePath = loginContext.domain + '/Requisition/' + $filter('date')(new Date(), 'yyyy-MM-dd') + '/' + erNo;
            showRequisitionWindow(null, erNo, relativePath);
        });
    };
    var viewRequisition = function () {
        registrationUtil.viewRequisition($scope.accNo);
    };
    var showRequisitionWindow = function (requisitionFiles, erNo, relativePath) {
        var args = { requisitionFiles: requisitionFiles, erNo: erNo };
        var newScope = $scope.$new(true);
        newScope.args = args;
        var modalInstance = registrationUtil.showRequisition(args, newScope);
        modalInstance.result.then(function (result) {
            if (result) {
                var params = { accNo: $scope.accNo, erNo: erNo, relativePath: relativePath, imageQualityLevel: application.clientConfig.scanQualityLevel };
                registrationService.processRequisitionInOrder(params).success(function (result) {
                    if (result === 0) {
                        $scope.order.isScan = $scope.order.isScan || true;
                    } else {
                        var title = $translate.instant("Tips");
                        var content = $translate.instant("UploadRequisitionError");
                        openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
                    }
                });
            } else {
                //delete all requisition images
                $scope.order.isScan = false;
            }
        });
    };

    //time slice
    var timesliceSelected = function (lockId, timeslice) {
        slice = timeslice;
        slice.lockGuid = lockId;
        registrationService.updateProcedureSlice($scope.orderId, slice).success(function () {
            refreshProcedures();
            csdToaster.pop('success', $translate.instant('Success'), '');
            //openDialog.openIconDialog(
            //    openDialog.NotifyMessageType.Success,
            //    $translate.instant('Success'),
            //    $translate.instant('Success'));
        })
        .error(function () {
            if (slice.lockGuid) {
                registrationService.unLockTimeSlice().success(function () { });
            }
            openDialog.openIconDialog(
                openDialog.NotifyMessageType.Warn,
                $translate.instant('Warn'),
                $translate.instant('Failed'));
        });
    };

    $scope.modifySlice = function () {
        slice.modalityTypeEnable = false;
        slice.modalityEnable = true;
        $scope.timesliceOperater.unlockId = slice.lockGuid;
        $scope.timesliceOperater.open(timesliceSelected);
    };
    $scope.viewReport = function (procedureId) {
        $('.ris-popover').popover('destroy');
        $('.report-preview-content-popover').remove();
        $state.go('ris.report', {
            isPreview: true,
            procedureId: procedureId
        });
    };
    // initialzation
    (function initialize() {
        $log.debug('RegistrationController.initialize()...');
        $scope.flag = true;
        $scope.procedures = [];
        $scope.order = {};
        $scope.returnToSearchResult = returnToSearchResult;
        $scope.printRequisition = printRequisition;
        $scope.printBarCode = printBarCode;
        $scope.editPatient = editPatient;
        $scope.editOrder = editOrder;
        $scope.addProcedure = addProcedure;
        $scope.prepareSummary = prepareSummary;
        $scope.showSummary = showSummary;
        $scope.updateProcedure = updateProcedure;
        $scope.deleteProcedure = deleteProcedure;
        $scope.procedureHistoryItems = [];
        $scope.getProcedureHistory = getProcedureHistory;
        $scope.isCollapsed = true;
        $scope.Yes = $translate.instant("Yes");
        $scope.No = $translate.instant("No");
        $scope.createNewFlup = createNewFlup;
        //
        $scope.enums = enums;
        $scope.isDisabledWriteReport = true;
        $scope.isDisabledPreviewReport = true;
        $scope.isDisabledFinishExam = true;
        $scope.isDisabledLoadImage = true;
        $scope.isDisabledTransferReg = true;
        $scope.dataWriteReprot = [];
        $scope.dataViewReport = [];
        $scope.dataFinishExam = [];
        $scope.writeReprotCount = 0;
        $scope.selectLockedProcedure = selectLockedProcedure;
        $scope.selectRelateReport = selectRelateReport;
        $scope.selectViewReport = selectViewReport;
        $scope.finishExam = finishExam;
        $scope.transferBookingToReg = transferBookingToReg;
        $scope.openPACSImageViewer = openPACSImageViewer;
        $scope.statusList = configurationData.statusList;
        $scope.updateModality = updateModality;
        $scope.showUpdateModalityView = showUpdateModalityView;
        $scope.selectedProcedure = null;
        $scope.scan = scan;
        $scope.viewRequisition = viewRequisition;
        $scope.isBooking = false;
        $scope.timesliceOperater = {};
        $scope.timesliceOption = function () {
            return slice;
        };
        getOrder();
        $scope.isPACSIntegration = configurationData.isPACSIntegration;
        if ($scope.isPACSIntegration && application.clientConfig && application.clientConfig.integrationType == 0) {
            $scope.isPACSIntegration = false;
        }

        $scope.isMobile = $rootScope.browser.versions.mobile;
    }());
}
]);
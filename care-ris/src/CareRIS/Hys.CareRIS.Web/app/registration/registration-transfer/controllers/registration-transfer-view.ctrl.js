registrationModule.controller('RegistrationTransferController', ['$log', '$scope', '$rootScope', '$stateParams', '$translate',
    'registrationService', 'constants', 'registrationUtil', 'enums', 'loginContext', 'configurationService', 'application', '$modal', '$filter',
    'commonMessageHub', '$state', 'openDialog','csdToaster',
    function ($log, $scope, $rootScope, $stateParams, $translate, registrationService, constants, registrationUtil, enums, loginContext,
        configurationService, application, $modal, $filter, commonMessageHub, $state, openDialog, csdToaster) {
        'use strict';
        $log.debug('RegistrationTransferController.ctor()...');

        var configurationData = application.configuration;
        var processStatus = $translate.instant("Processing");
        var title = $translate.instant("Tips");
        var canTransferWhenApplyInfoDifferent = false;
        var notExistRequestItem = [], regedRequests = [], processedRequests = [], tempRegedProcedures = [];
        var transferStatus = enums.transferStatus;
        var mutipleOrderData = [], currentOrderId;
        var slice = {};

        var getItemHistory = function () {
            $scope.isCollapsed = !$scope.isCollapsed;
            // old patient
            if (!$scope.isCollapsed && $scope.patient.uniqueID) {
                registrationService.getProcedures($scope.patient.uniqueID).success(function (data) {
                    $scope.procedureHistoryItems = data;
                    $scope.historyOperation = $translate.instant("HideProcedureHistory");
                });
            } else {
                $scope.historyOperation = $translate.instant("ViewProcedureHistory");
            }
        };
        var selectRequest = function (request) {
            _.each(request.requestItems, function (v) {
                if (v.status.toLowerCase() === transferStatus.pending.toLowerCase()) {
                    v.isSelected = request.isSelected;
                }
            });
            validateOperationStatus();
        };
        var validateOperationStatus = function () {
            var selectedReuqests = getSelectedRequests();
            if (selectedReuqests.length === 0) {
                $scope.isDisableTransfer=$scope.isDisableReject = true;
            } else {
                $scope.isDisableTransfer = false;
                var index = _.findIndex(selectedReuqests, function (v) {
                    var exist = _.find(v.requestItems, function (p) {
                        return p.status.toLowerCase() === transferStatus.reged.toLowerCase()||
                            p.status.toLowerCase() === transferStatus.booked.toLowerCase();
                    });
                    return !!exist;
                });
                $scope.isDisableReject = index > -1;
            }
        };
        var transferToRegistration = function () {
            transfer(transferStatus.reged);
        };
        var transferToBooking = function () {
            transfer(transferStatus.booked);
        };
        var transfer = function (status) {
            var selectedRequests = getSelectedRequests();
            if (!validateRequests(selectedRequests)) {
                return;
            }
            //booking transfer
            if (status === transferStatus.booked) {
                var _types = _.keys(_.groupBy(selectedRequests, function (p) {
                    return p.requestItems[0].modalityType;
                }));
                if (_types.length > 1) {
                    csdToaster.pop('error', $translate.instant('BookingModalityTypeError'), '');
                    return;
                }
                var selectedModalities = [];
                _.each(selectedRequests, function (r) {
                    _.each(r.requestItems, function (item) {
                        if (!_.contains(selectedModalities, item.modality) && item.isSelected) {
                            selectedModalities.push(item.modality);
                        }
                    });
                });
                //  exist modalities  in CareRIS 
                configurationService.getModalities(loginContext.site).success(function (result) {
                    var modalities = _.filter(selectedModalities, function (m) {
                        var index = _.findIndex(result, function (p) { return p.modalityName.toUpperCase() === m.toUpperCase() });
                        return index > -1;
                    });
                    slice.modalityType = _types[0];
                    slice.modality = modalities ? modalities[0] : null;
                    slice.modalityTypeEnable = false;
                    slice.modalityEnable = true;
                    processSelectedRequests(selectedRequests, status);
                });
            }
            else {// registration transfer
                processSelectedRequests(selectedRequests, status);
            }
        };

        var processSelectedRequestsCallback = function (lockId, timeslice) {
            $scope.regedProcedures = _.union($scope.regedProcedures, tempRegedProcedures);
            slice = timeslice;
            slice.lockGuid = lockId;
            _.each($scope.regedProcedures, function(p) {
                p.modality = slice.modality;
                p.bookingBeginTime = slice.bookingBeginTime;
                p.bookingEndTime = slice.bookingEndTime;
                p.bookingTimeAlias = slice.bookingTimeAlias;
            });
            $scope.modifySliceEnable = true;
           
        };

        var processSelectedRequests = function (selectedRequests,status) {
            // to be processed request item count
            var toProcessCount = 0;
            var isNotMapped;
            tempRegedProcedures = [];//init  empty
            _.each(selectedRequests, function (v) {
                var count = _.countBy(v.requestItems, function (t) {
                    return t.isSelected ? 'selected' : 'notSelected';
                });
                toProcessCount += count['selected'];
            });
            _.each(selectedRequests, function (v) {
                v.isReged = true;
                // source requets info data to handle in tbRequest tbRequestItem tbRequestCharge
                var regedRequest = _.clone(_.findWhere($scope.requestInfo.requests, { erNo: v.erNo }));
                regedRequest.requestItems = [];
                _.each(v.requestItems, function (t) {
                    if (t.isSelected) {
                        // validate wheather exist the selected item
                        registrationService.getProcedureByCode(t.procedureCode, t.modality).success(function (data) {
                            toProcessCount--;
                            t.showStatus = processStatus;
                            //t.status = transferStatus.reged;
                            t.status = status;
                            delete t.isSelected;
                            regedRequest.requestItems.push(t);
                            if (data) {
                                if (data.modalityType.toUpperCase() !== t.modalityType.toUpperCase()) {
                                    isNotMapped = isNotMapped || true;
                                    $scope.isRequestInValid = $scope.isRequestInValid || true;
                                } else {
                                    data.requestItemUID = t.requestItemUID;
                                    if (status === transferStatus.reged) {
                                        // transfer to registration
                                        data.status = enums.rpStatus.checkIn;
                                        data.registrar = loginContext.userId;
                                        data.registrarName = loginContext.localName;
                                        data.showStatus = $translate.instant('Reged');
                                    } else { // transfer to  booking
                                        data.status = enums.rpStatus.noCheck;
                                        data.booker = loginContext.userId;
                                        data.showStatus = $translate.instant('Booked');
                                    }
                                    //additional fileds from hiscon
                                    data.remoteRPID = t.remoteRPID;
                                    data.isCharge = t.isCharge;
                                    data.optional1 = t.optional1;
                                    data.optional2 = t.optional2;
                                    data.optional3 = t.optional3;
                                    data.contrastName = t.contrastName || data.contrastName;
                                    data.contrastDose = t.contrastDose || data.contrastDose;
                                    tempRegedProcedures.push(data);
                                }
                            }
                            else {
                                $scope.isRequestInValid = $scope.isRequestInValid || true;
                                notExistRequestItem.push(t);
                            }
                            // all request has response
                            if (toProcessCount === 0) {
                                if (!$scope.isRequestInValid) {
                                    // valid
                                    if (status === transferStatus.booked) {
                                        openSlice(processSelectedRequestsCallback,cancelSlice);
                                    } else {
                                        $scope.regedProcedures = _.union($scope.regedProcedures, tempRegedProcedures);
                                    }
                                } else {
                                    // invalid request,pop up error message,unlock slice 
                                    var content;
                                    if (notExistRequestItem.length > 0) {
                                        var modalityTypes = _.pluck(notExistRequestItem, 'modalityType').join(';');
                                        var procedureCodes = _.pluck(notExistRequestItem, 'procedureCode').join(';');
                                        content = $translate.instant('NotExistProcedureCodeError').format(modalityTypes, procedureCodes);
                                    } else if (isNotMapped) {
                                        content = $translate.instant('RequestNotMappedError');
                                    }
                                    csdToaster.pop('error', content, '');
                                    clearInitData();
                                }
                            }
                        });
                    }
                });
                // regedRequests=> for update history in tbRequest tbRequestCharge tRequestChargeItem,the data can not be modified
                regedRequests.push(regedRequest);
                //processedRequests=> for order info,so the request info can be modified.
                processedRequests.push(v);
            });
            $scope.isTransfered = true;
        };
        var cancelSlice = function() {
            $scope.isTransfered = false;
            $scope.isDisableTransfer = $scope.isDisableReject = true;
            clearInitData();
        };
        var clearInitData = function () {
            _.each(regedRequests, function (r) {
                delete r.isSelected;
                delete r.isReged;
                _.each(r.requestItems, function (t) {
                    delete t.isSelected;
                    t.showStatus = $translate.instant(transferStatus.pending);
                    t.status = transferStatus.pending;
                });
            });
            // for disabling  order info editting button
            _.map(processedRequests, function (p) { delete  p.isReged; });
            $scope.regedProcedures = [];
            tempRegedProcedures = [];
            regedRequests = [];
            processedRequests = [];
            notExistRequestItem = [];
        };
        var validateRequests = function (requests) {
            //validate selected requests whether one request  has only one modality type
            if (!validateIsSameModalityType(requests)) {
                var content = $translate.instant('NotSameModalityTypeError');
                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
                return false;
            }
            if (requests.length === 1) {
                return true;
            }
            // multiple requests
            var groupRequests = _.groupBy(requests, function (p) {
                return p.requestItems[0].modalityType;
            });
            for (var key in groupRequests) {
                var groupItem = groupRequests[key];
                if (groupItem.length > 1) {
                    var restRequests = _.rest(groupItem);
                    // validate patientType/chargeType (based on configuration:applyDept/applyDoctor)
                    var errorMsg = $translate.instant("CanNotTransfer");
                    var validationIndex = _.findIndex(restRequests, function (p) {
                        var flag = false;
                        if (p.patientType !== groupItem[0].patientType) {
                            errorMsg += $translate.instant('PatientType');
                            flag = true;
                        }
                        if (p.chargeType !== groupItem[0].chargeType) {
                            errorMsg += (flag ? ',' : '') + $translate.instant('ChargeType');
                            flag = true;
                        }
                        if (!canTransferWhenApplyInfoDifferent) {
                            if (p.applyDept !== groupItem[0].applyDept) {
                                errorMsg += (flag ? ',' : '') + $translate.instant('ApplyDepartment');
                                flag = true;
                            }
                            if (p.applyDoctor !== groupItem[0].applyDoctor) {
                                errorMsg += (flag ? ',' : '') + $translate.instant('ApplyDoctor');
                                flag = true;
                            }
                        }
                        return flag;
                    });
                    if (validationIndex > -1) {
                        csdToaster.pop('error', errorMsg, '');
                        return false;
                    }
                    return true;
                }
                else {
                    return true;
                }
            }
        };
        var validateIsSameModalityType = function (requests) {
            var index = _.findIndex(requests, function (request) {
                var types = _.groupBy(request.requestItems, function (r) { return r.modalityType.toUpperCase(); });
                return _.keys(types).length > 1;
            });
            return index === -1;
        };
        var getSelectedRequests = function () {
            var selectedReuqests = _.filter($scope.requests, function (request) {
                var index = _.findIndex(request.requestItems, function (item) {
                    return item.isSelected;
                });
                return index > -1;
            });
            return selectedReuqests;
        };
        // add new procedure
        var addProcedureCallback = function () {
            if (this.length > 0) {
                _.map(this, function (p) {
                    p.showStatus = $translate.instant(p.status===enums.rpStatus.checkIn?'Reged':'Booked');
                });
                $scope.regedProcedures = $scope.regedProcedures.concat(this);
            }
        };

        var addProcedure = function () {
            var modalityTypes = processModalityTypes();
            var data = { procedures: $scope.regedProcedures, modalityTypes: modalityTypes };
            if (!_.isEmpty(slice)) {
                data.bookingSlice = slice;
            }
            registrationUtil.openProcedureWindow(addProcedureCallback, data);
        };
        var deleteRegedProcedure = function (procedure) {
            if ($scope.isSaved) return;
            var searchParams = procedure.requestItemUID ? { requestItemUID: procedure.requestItemUID } : { procedureCode: procedure.procedureCode };
            var index = _.findIndex($scope.regedProcedures, searchParams);
            if (index > -1) {
                $scope.regedProcedures.splice(index, 1);
            }
            //request item from request
            if (procedure.requestItemUID) {
                var request = _.find(regedRequests, function (r) {
                    return _.findIndex(r.requestItems, { requestItemUID: procedure.requestItemUID }) > -1;
                });
                var item = _.findWhere(request.requestItems, { requestItemUID: procedure.requestItemUID });
                if (item) {
                    item.status = transferStatus.pending;
                    item.showStatus = $translate.instant(transferStatus.pending);
                    request.requestItems = _.reject(request.requestItems, function (v) { return v.requestItemUID === item.requestItemUID; });
                    if (request.requestItems.length === 0) {
                        regedRequests = _.reject(regedRequests, function (v) { return v.erNo === request.erNo; });
                    }
                }
            }
        };
        var showStatus = function () {
            _.each($scope.requests, function(request) {
                _.each(request.requestItems, function(item) {
                    item.showStatus = $translate.instant(transferStatus[item.status.toLowerCase()]);
                });
            });
        };
        var showUpdateModalityView = function (row) {
            $scope.selectedProcedure = _.clone(row);
            // get modality data
            configurationService.getModalitiesByType(loginContext.site, row.modalityType).success(function (data) {
                $scope.modalities = data;
            });
        };
        var updateModality = function (selectedprocedure) {
            var procedure = _.findWhere($scope.regedProcedures, { procedureCode: selectedprocedure.procedureCode });
            procedure.modality = selectedprocedure.modality;
            $scope.hideModalityPopOver();
        };
        // complete transfering registration
        var complete = function (successCallBack) {
            if ($scope.regedProcedures.length === 0) {
                var content = $translate.instant("NotSelectCheckingItemErrorMsg");
                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
                return;
            }
            var patient = _.clone($scope.patient);
            if (patient.patientNo) {
                saveNewRegistration(patient, successCallBack);
            }
            else {
                registrationService.getPatientNO(loginContext.site).success(function(data) {
                    $scope.patient.patientNo = patient.patientNo = data;
                    saveNewRegistration(patient, successCallBack);
                });
            }
        };
        var saveNewRegistration = function (patient, successCallBack) {
            if (!validateRequests(processedRequests)) {
                return;
            }
            if ($scope.isSaved) {
                $state.go('ris.registration', { orderId: currentOrderId });
            } else {
                // patient data 
                patient.site = loginContext.site;
                patient.domain = loginContext.domain;
                patient.birthday = $filter('date')($scope.patient.birthday, 'yyyy-MM-dd HH:mm');
                var registrations = processOrders($scope.regedProcedures, patient);
                if (registrations && registrations.length > 0) {
                    registrationService.transferRegistration(registrations).success(function (data) {
                        //$scope.status, $scope.accNo, $scope.modalityType
                        _.each(data.orders, function (order) {
                            currentOrderId = order.uniqueID;
                            var procedure = _.findWhere(data.procedures, {
                                orderID: order.uniqueID
                            });
                            mutipleOrderData.push({
                                accNo: order.accNo,
                                modalityType: procedure.modalityType,
                                status: procedure.status
                            });
                            // handle merging Apply info merge bussiness
                            var isMultipleApplyDept = order.applyDept.indexOf(';') > -1;
                            var isMultipleApplyDoc = order.applyDept.indexOf(';') > -1;
                            if ($scope.canEditAppDepartmentAndAppDoctor && order.applyDept
                                && $.trim(order.applyDept) != '' && !isMultipleApplyDept) {
                                var applyDept = $.trim(order.applyDept);
                                var a = _.findWhere(configurationData.applyDeptList, {
                                    deptName: applyDept
                                });
                                if (a) { } else {
                                    configurationService.convertFirstPY(applyDept).success(function (data) {
                                        var newApplyDept = { deptName: applyDept, firstPingYinName: data, shortcutCode: data };
                                        configurationData.applyDeptList = _.sortBy($scope.applyDeptList, function (item) {
                                            return item.shortcutCode.toLowerCase();
                                        });
                                    });
                                }
                            }
                            if ($scope.canEditAppDepartmentAndAppDoctor && order.applyDoctor
                                    && $.trim(order.applyDoctor) != '' && !isMultipleApplyDoc) {
                                var applyDoctor = $.trim(order.applyDoctor);
                                var a = _.findWhere(configurationData.applyDoctorList, {
                                    doctorName: applyDoctor
                                });
                                if (a) { } else {
                                    configurationService.convertFirstPY(applyDoctor).success(function (data) {
                                        var newApplyDoctor = { doctorName: applyDoctor, firstPingYinName: data, shortcutCode: data };
                                        configurationData.applyDoctorList = _.sortBy($scope.applyDoctorList, function (item) {
                                            return item.shortcutCode.toLowerCase();
                                        });
                                    });
                                }
                            }
                            //notify message
                            var orderUpdateParams = new commonMessageHub.OrderUpdateParams();
                            orderUpdateParams.uniqueID = order.uniqueID;
                            commonMessageHub.publish(commonMessageHub.Messages.OrderUpdated, orderUpdateParams);
                        });
                        if (_.isFunction(successCallBack)) {
                            successCallBack();
                            $scope.isSaved = true;
                        } else {
                            $state.go('ris.registration', { orderId: currentOrderId });
                        }
                    })
                        .error(function () {
                            var content = $translate.instant("SaveErrorMsg");
                            openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
                            console.log("Save new registration failed.");
                        });
                } else {
                    $scope.isRequestInValid = true;  
                    var content = $translate.instant("RequestNotMappedError");
                    csdToaster.pop('error', content, '');
                }
            }
        };
        var processOrders = function (regedProcedures,patient) {
            var groupProcedures = _.groupBy(regedProcedures, 'modalityType');
            var registrations = [];
            //on request only one modalityType
            _.each(groupProcedures, function (procedures) {
                var mapModalityType = procedures[0].modalityType;
                var mapRequests = _.filter(processedRequests, function (request) {
                    var existRequestItem = _.find(request.requestItems, function (p) { return p.modalityType.toLowerCase() === mapModalityType.toLowerCase() });
                    return !!existRequestItem;
                });
                if (mapRequests&&mapRequests.length>0) {
                    var observatons = _.uniq(mapRequests, function (request) {
                        return request.observation;
                    });
                    var healhHistorys = _.uniq(mapRequests, function (request) {
                        return request.healthHistory;
                    });
                    var order = _.clone(mapRequests[0]);
                    var erNos = _.pluck(mapRequests, 'erNo');
                    order.domain = loginContext.domain;
                    order.erequisition = mapRequests[0].eAcquisition;
                    order.remoteAccNo = erNos.join('|');
                    order.observation = _.pluck(observatons, 'observation').join("\n");
                    order.healthHistory = _.pluck(healhHistorys, 'healthHistory').join("\n");
                    if (canTransferWhenApplyInfoDifferent) {
                        var applyDepts = _.uniq(mapRequests, function (request) {
                            return request.applyDept;
                        });
                        var applyDoctors = _.uniq(mapRequests, function (request) {
                            return request.applyDoctor;
                        });
                        order.applyDept = _.compact(_.pluck(applyDepts, 'applyDept')).join(";");
                        order.applyDoctor = _.compact(_.pluck(applyDoctors, 'applyDoctor')).join(";");
                    }
                    var registrationRequests = _.filter(regedRequests, function (request) {
                        var existRequestItem = _.find(request.requestItems, function (p) { return p.modalityType.toLowerCase() === mapModalityType.toLowerCase() });
                        return !!existRequestItem;
                    });
                    registrations.push({ orders: [order], patient: patient, procedures: procedures, requests: registrationRequests });
                }
                });
            return registrations;
        };
        var printMultipleRequisition = function () {
            _.each(mutipleOrderData, function (data) {
                registrationUtil.printRequisition(data.status, data.accNo, data.modalityType);
            });
        };
        var printRequisition = function () {
            if ($scope.isSaved) {
                printMultipleRequisition();
            } else {
                complete(printMultipleRequisition);
            }
        };
        var printMultipleBarCode = function () {
            _.each(mutipleOrderData, function (data) {
                registrationUtil.printBarCode(data.status, data.accNo, data.modalityType);
            });
        };
        var printBarCode = function () {
            if ($scope.isSaved) {
                printMultipleBarCode();
            } else {
                complete(printMultipleBarCode);
            }
        };
        var editOrderInfo = function (request) {
            if ($scope.isSaved||!request.isReged) return;
            request.isTransfer = true;
            var modalInstance = $modal.open({
                templateUrl:'/app/registration/views/order-edit-view.html',
                controller: 'OrderEditController',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    order: function () {
                        return _.clone(request);
                    },
                    configurationData: function () {
                        return configurationData;
                    }
                }
            });
            modalInstance.result.then(function (result) {
                request.observation = result.observation;
                request.applyDept = result.applyDept;
                request.applyDoctor = result.applyDoctor;
                request.healthHistory = result.healthHistory;
                request.patientType = result.patientType;
                request.chargeType = result.chargeType;
            });
        };
        var cancel = function () {
            $state.go('ris.worklist.registrations');
            $rootScope.refreshSearch();
        };
        var unlockTimeSlice = function (timeSlice) {
            if (timeSlice && timeSlice.lockGuid) {
                registrationService.unLockTimeSlice(timeSlice.lockGuid).success(function () {
                    $log.log('unlock:' + timeSlice.lockGuid + ' success!');
                });
            }
        };
        var reject = function () {
            var selectedRequests = getSelectedRequests();
            if (selectedRequests.length === 0) {
                return;
            }
            var isInValidate = false;
            selectedRequests.every(function (r) {
                var index = _.findIndex(r.requestItems, function (item) {
                    return item.status.toLowerCase() === transferStatus.reged.toLowerCase();
                });
                if (index > -1) {
                    var content = $translate.instant("RejectErrorMsg");
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
                    isInValidate = true;
                    return;
                }
            });
         
            if (isInValidate) {
                return;
            }
            var modalInstance = $modal.open({
                templateUrl:'/app/registration/registration-transfer/views/transfer-reject-reason-view.html',
                controller: 'TransferRejectReasonController',
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            modalInstance.result.then(function (result) {
                _.  each(selectedRequests, function (p) {
                    p.reason = result;
                    _.each(p.requestItems, function (item) {
                        item.status=transferStatus.rejected;
                    });
                });
                registrationService.rejectTransfer(selectedRequests).success(function (data) {
                    if (data) {
                        // success
                        cancel();
                    } else {
                        var content = $translate.instant("SaveErrorMsg");
                        openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
                    }
                });
            });
        };
        var prepareSummary = function (row) {
            $scope.hoverReport = row;
        };
        var showSummary = function () {
            $scope.report = $scope.hoverReport;
        };
        var processModalityTypes = function () {
            var modalityTypes = [];
            _.each(processedRequests, function (r) {
                var type = r.requestItems ? r.requestItems[0].modalityType : null;
                if (!_.contains(modalityTypes,type.toUpperCase())) {
                    modalityTypes.push(type.toUpperCase());
                }
            });
            return modalityTypes;
        };
        var openSlice = function (successCallback,cancelCallback) {
            $scope.timesliceOperater.open(successCallback, cancelCallback);
        };
        $scope.modifySlice = function () {
            var hasProcedure = $scope.regedProcedures&&$scope.regedProcedures.length > 0;
            slice.modalityEnable = true;
            slice.modalityTypeEnable = !hasProcedure;
            $scope.timesliceOperater.unlockId = slice.lockGuid;
            openSlice(function (lockId, timeslice) {
                slice = timeslice;
                slice.lockGuid = lockId;
                if (hasProcedure) {
                    _.each($scope.regedProcedures, function (p) {
                        p.bookingBeginTime = slice.bookingBeginTime;
                        p.bookingEndTime = slice.bookingEndTime;
                        p.bookingTimeAlias = slice.bookingTimeAlias;
                        p.modality = slice.modality;
                    });
                }
            });
        };
        $scope.$on('$destroy', function () {
            if (!$scope.isSaved) {
                unlockTimeSlice(slice);
            }
        });

        ; (function initialize() {
            // original request info  should be kept,clone the data for registration

            $scope.isMobile = $rootScope.browser.versions.mobile;
            $scope.requests = $scope.requestInfo?_.clone($scope.requestInfo.requests):null;
            $scope.patient = $scope.requestInfo?$scope.requestInfo.patient:null;
            $scope.historyOperation = $translate.instant("ViewProcedureHistory");
            $scope.getItemHistory = getItemHistory;
            $scope.selectRequest = selectRequest;
            $scope.regedProcedures = [];
            $scope.selectedRequests = [];
            $scope.transferToRegistration = transferToRegistration;
            $scope.deleteRegedProcedure = deleteRegedProcedure;
            $scope.addProcedure = addProcedure;
            $scope.showUpdateModalityView = showUpdateModalityView;
            $scope.updateModality = updateModality;
            $scope.editOrderInfo = editOrderInfo;
            $scope.complete = complete;
            $scope.printBarCode = printBarCode;
            $scope.printRequisition = printRequisition;
            $scope.regedRequests = regedRequests;
            $scope.cancel = cancel;
            $scope.reject = reject;
            $scope.showSummary = showSummary;
            $scope.prepareSummary = prepareSummary;
            $scope.isRequestInValid = false;
            // system profile
            $scope.canEditAppDepartmentAndAppDoctor = false;
            var s = _.findWhere(configurationData.canEditAppdepartmentAndAppDoctorList, { moduleID: '0300' });
            if (s) {
                $scope.canEditAppDepartmentAndAppDoctor = s.value === '1';
            };
            var t = _.findWhere(configurationData.canTransferWhenApplyInfoDifferentList, { moduleID: '0E00' });
            if (t) {
                canTransferWhenApplyInfoDifferent = t.value === '1';
            }
            $scope.statusList = configurationData.statusList;
            $scope.isCollapsed = true;;
            $scope.isDisableTransfer = $scope.isDisableReject = true;
            $scope.validateOperationStatus = validateOperationStatus;
            $scope.transferToBooking = transferToBooking;
            $scope.timesliceOperater = {};
            $scope.timesliceOption = function () {
                return slice;
            };
            $scope.modifySliceEnable = false;
            showStatus();
        })();
    }]);
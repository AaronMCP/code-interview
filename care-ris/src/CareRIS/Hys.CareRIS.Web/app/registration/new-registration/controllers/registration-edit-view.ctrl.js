registrationModule.controller('RegistrationEditController', ['$log', '$scope', '$rootScope', '$modalInstance', 'registrationService', 'loginContext',
    'enums', '$translate', '$modal', '$anchorScroll', '$location', 'risDialog',
    '$filter', 'registrationUtil', '$timeout', 'risInvalidFocus', 'application', '$state', 'configurationService', '$q', 'commonMessageHub', 'args','openDialog',
    function ($log, $scope, $rootScope, $modalInstance, registrationService, loginContext,
        enums, $translate, $modal, $anchorScroll, $location, risDialog,
        $filter, registrationUtil, $timeout, risInvalidFocus, application, $state, configurationService, $q, commonMessageHub, args, openDialog) {
        'use strict';
        $log.debug('RegistrationEditController.ctor()...');
        var addedProcedures = [];
        var updateProcedure = true;
        var patientTypeList;
        var configurationData = application.configuration;
        var mutipleOrderData = [];
        var requests;
        var requisitionFiles = null;
        var erNo;
        var bookingSlice;

        //下拉框搜索无数据
        $scope.noDataFound = function (e) {
            if (!e.sender.dataSource.view()[0]) {
                e.sender.ul.parent().parent().find('.k-nodata > div').html('无数据');
            }
        }
        // input filed
        var indexList = ['localName', 'englishName',
            'referenceNo', 'gender', 'currentAge', 'birthday',
            'telephone', 'patientType'
        ];

        var getPatientNo = function () {
            var site = loginContext.site;
            registrationService.getPatientNO(site).success(function (data) {
                $scope.patient.patientNo = data;
            });
        };
        // get base data for new registration add
        var initConfigurationData = function () {
            $scope.canEditAppDepartmentAndAppDoctor = false;
            var s = _.findWhere(configurationData.canEditAppdepartmentAndAppDoctorList, { moduleID: '0300' });
            if (s) {
                $scope.canEditAppDepartmentAndAppDoctor = s.value === '1';
            }
            $scope.ageUnitList = configurationData.ageUnitList;
            $scope.genderList = configurationData.genderList;
            $scope.patientTypeList = configurationData.patientTypeList;
            $scope.chargeTypeList = configurationData.chargeTypeList;;
            if ($scope.canEditAppDepartmentAndAppDoctor) {
                var applyDeptsPromise = configurationService.getApplyDepts(loginContext.site);
                var applyDoctorsPromise = configurationService.getApplyDoctors(loginContext.site);
                $q.all([applyDeptsPromise, applyDoctorsPromise]).then(function (results) {
                    //applyDeptsPromise
                    $scope.applyDeptList = results[0].data;
                    //applyDoctorsPromise
                    $scope.applyDoctorList = results[1].data;
                });
            }
            else {
                $scope.applyDeptList = configurationData.applyDeptList;
                $scope.applyDoctorList = configurationData.applyDoctorList;
            }
            $scope.statusList = configurationData.statusList;
            $scope.observationList = configurationData.observationList;

            if (args && args.request) {
                $scope.order = _.clone(args.request);
                $scope.order.isBedSide = $scope.order.isBedSide == '1';
                $scope.order.isThreedReBuild = $scope.order.isThreedReBuild == '1';
                requests = [args.request];
            }
            if (args && args.patient) {
                $scope.patient = _.clone(args.patient);
                // from hiscon,new patient
                if (!args.patient.patientNo) {
                    getPatientNo();
                }
                if ($scope.order.currentAge) {
                    var result = $scope.order.currentAge.split(' ')
                    $scope.order.currentAge = result[0];
                    var a = _.findWhere(configurationData.ageUnitList, {
                        text: result[1]
                    });
                    $scope.order.ageType = a ? a.value : null;
                } else {
                    $timeout(function () {
                        setAge();
                    });
                }
            } else {
                $scope.patient = {
                    IsVip: false
                };
                getPatientNo();
            }

            if (!$scope.patient.gender && configurationData.genderList.length > 0) {
                $scope.patient.gender = configurationData.genderList[0].value
            }
            if (!$scope.order.ageType && configurationData.ageUnitList.length > 0) {
                $scope.order.ageType = configurationData.ageUnitList[0].value
            }
        };

        var cuurentOrderId;

        var getPriority = function (value) {
            var patientType = _.findWhere(patientTypeList, {
                value: value
            });
            return patientType ? patientType.mapValue : null;
        };
        var save = function (form, successCallBack) {
            $scope.isShowNotSelectedError = addedProcedures.length === 0 ? true : false;
            if (!form.$valid) {
                $scope.isShowErrorMsg = true;
                risInvalidFocus(form, indexList);
                return;
            }
            if (addedProcedures.length === 0) {
                $scope.$broadcast('notSelectProcedure');
                return;
            }
            if ($scope.patient.address && $scope.patient.address.length > 50) {
                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '地址的长度不能超过50个字符！');
                return;
            }
            if ($scope.order.observation && $scope.order.observation.length > 200) {
                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '临床诊断的长度不能超过50个字符！');
                return;
            }
            var patient = _.clone($scope.patient),
                order = _.clone($scope.order);
            order.priority = getPriority(order.patientType);
            order.domain = loginContext.domain;
            order.currentAge = order.currentAge + ' ' + order.ageType;
            order.remoteAccNo = order.erNo || '';
            patient.site = loginContext.site;
            patient.domain = loginContext.domain;
            patient.birthday = $filter('date')($scope.patient.birthday, 'yyyy-MM-dd HH:mm')
            //$scope.patient.birthday.Format("yyyy-MM-dd HH:mm");
            var registration = {
                orders: [order],
                patient: patient,
                procedures: addedProcedures,
                requests: requests
            };
            if (requisitionFiles && requisitionFiles.length > 0) {
                registration.requisitionFiles = { requisitionFiles: requisitionFiles, erNo: erNo, imageQualityLevel: application.clientConfig.scanQualityLevel };
                order.isScan = true;
            }
            if ($scope.isSaved) {
                $modalInstance.close();
                if ($state.is('ris.registration')) {
                    $state.go('ris.registration', { orderId: cuurentOrderId });
                }
                $rootScope.refreshSearch();
            } else {
                registrationService.saveNewRegistration(registration).success(function (data) {
                     $scope.isSaved = true;
                    //$scope.status, $scope.accNo, $scope.modalityType
                    _.each(data.orders, function (order) {
                        cuurentOrderId = order.uniqueID;
                        var procedure = _.findWhere(data.procedures, {
                            orderID: order.uniqueID
                        });
                        mutipleOrderData.push({
                            accNo: order.accNo,
                            modalityType: procedure.modalityType,
                            status: procedure.status
                        });

                        //notify message
                        var orderUpdateParams = new commonMessageHub.OrderUpdateParams();
                        orderUpdateParams.uniqueID = order.uniqueID;
                        commonMessageHub.publish(commonMessageHub.Messages.OrderUpdated, orderUpdateParams);
                    });
                    if (_.isFunction(successCallBack)) {// print barcode or print requisitions trigger 
                        successCallBack();
                        disableApplyCombo();
                    } else { // Complte button trigger
                        //auto print
                        var isAutoPrintBarcode = $scope.isBooking ? application.clientConfig.appointmentAutoPrintBarcode : application.clientConfig.autoPrintBarcode;
                        var isAutoPrintNotice = $scope.isBooking ? application.clientConfig.appointmentAutoPrintNotice : application.clientConfig.autoPrintNotice;
                        if (isAutoPrintBarcode) {
                            printMultipleBarCode();
                        }
                        if (isAutoPrintNotice) {
                            printMultipleRequisition();
                        }
                        $modalInstance.close();
                        if ($state.is('ris.registration')) {
                            $state.go('ris.registration', { orderId: cuurentOrderId });
                        }
                        $rootScope.refreshSearch();
                    }

                    if ($scope.canEditAppDepartmentAndAppDoctor && $scope.order.applyDept
                        && $.trim($scope.order.applyDept) != '') {
                        var applyDept = $.trim($scope.order.applyDept);
                        var a = _.findWhere($scope.applyDeptList, {
                            deptName: applyDept
                        });
                        if (a) { } else {
                            configurationService.convertFirstPY(applyDept).success(function (data) {
                                var newApplyDept = { deptName: applyDept, firstPingYinName: data, shortcutCode: data }
                                $scope.applyDeptList.push(newApplyDept);
                                configurationData.applyDeptList = _.sortBy($scope.applyDeptList, function (item) {
                                    return item.shortcutCode.toLowerCase();
                                });
                            });
                        }
                    }

                    if ($scope.canEditAppDepartmentAndAppDoctor && $scope.order.applyDoctor
                            && $.trim($scope.order.applyDoctor) != '') {
                        var applyDoctor = $.trim($scope.order.applyDoctor);
                        var a = _.findWhere($scope.applyDoctorList, {
                            doctorName: applyDoctor
                        });
                        if (a) { } else {
                            configurationService.convertFirstPY(applyDoctor).success(function (data) {
                                var newApplyDoctor = { doctorName: applyDoctor, firstPingYinName: data, shortcutCode: data }
                                $scope.applyDoctorList.push(newApplyDoctor);
                                configurationData.applyDoctorList = _.sortBy($scope.applyDoctorList, function (item) {
                                    return item.shortcutCode.toLowerCase();
                                });
                            });
                        }
                    }
                })
                    .error(function () {
                        openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), 'SaveErrorMsg');
                        console.log("Save new registration failed.");
                    });
            }
        };

        var simplifiedToEnglish = function (patientName) {
            if (!patientName) {
                $scope.patient.englishName = '';
                return;
            }
            registrationUtil.simplifiedToEnglish(patientName, configurationData.UpperFirstLetter, configurationData.SeparatePolicy, configurationData.Separator)
                .success(function (data) {
                    $scope.patient.englishName = data;
                });
        };

        var cancel = function () {
            if (requisitionFiles && requisitionFiles.length > 0) {
                registrationService.clearTempFile(erNo).success(function (result) {
                });
            }
            $modalInstance.dismiss();
        };

        // add new procedure
        var addProcedureCallback = function () {
            if (this.length > 0) {
                addedProcedures = addedProcedures.concat(this);
                $scope.addedProcedures = addedProcedures;
                $scope.isShowNotSelectedError = false;
            }
        };

        var addProcedure = function () {
            var data = { procedures: addedProcedures};
            if (bookingSlice) {
                data.modalityTypes = [bookingSlice.modalityType];
                data.bookingSlice = bookingSlice;
            }
            registrationUtil.openProcedureWindow(addProcedureCallback, data);
        };


        var updateAddedProcedure = function (procedure) {
            if ($scope.isSaved) {
                return;
            }
            updateProcedure = procedure;
            var data = { updateProcedure: updateProcedure, procedures: addedProcedures };
            if (bookingSlice) {
                data.modalityTypes = [bookingSlice.modalityType];
                data.bookingSlice = bookingSlice;
            }
            registrationUtil.openProcedureWindow(updateProcedureCallback, data);
        };

        var updateProcedureCallback = function () {
            if (this) {
                registrationUtil.updateProcedure(addedProcedures, this.oldProcedure.procedureCode, this.procedure);
            }
        };

        var deleteAddedProcedure = function (procedureCode) {
            if ($scope.isSaved) {
                return;
            }
            if (procedureCode) {
                registrationUtil.deleteProcedure(addedProcedures, procedureCode);
            }
        };

        var searchPatient = function ($event) {
            //if ($event.keyCode == enums.keyCode.enter) {
            $timeout(function () {
                registrationService.searchPatient($scope.patient.localName).then(function (data) {
                    // bind to list 
                    if (!data.data) {
                        return;
                    }
                    $scope.patientItems = data.data;
                    if (data.data.length !== 0) {
                        $scope.isShowPatientContainer = true;
                    }
                });
            }, 100);
            //}
        };
        var getProcedureHistory = function (patientId) {
            registrationService.getProcedures(patientId).then(function (data) {
                // bind to list 
                $scope.procedureHistoryItems = data.data;
                //if (data.data.length !== 0) {
                //    $scope.isShowProcedureHistoryContainer = true;
                //} else {
                //    $scope.isShowProcedureHistoryContainer = false;
                //}
            });
        };

        var selectPatient = function (selectedPatient) {
            $scope.patient = selectedPatient;
            $scope.patient.birthday = new Date(selectedPatient.birthday);
            $scope.isShowPatientContainer = false;
            setAge();
            //search procedure history
            getProcedureHistory(selectedPatient.uniqueID);
        };

        var popoverProcedureHistoryContainer = function () {
            if ($scope.patient) {
                getProcedureHistory($scope.patient.uniqueID);
            }
            $scope.isShowProcedureHistoryContainer = true;
            $scope.isShowPopIcon = false;
        };

        var prepareSummary = function (row) {
            $scope.hoverReport = row;
        };
        var closeAllPopover = function () {
            $('.ris-popover').popover('destroy');
            $('.report-preview-content-popover').remove();
        }
        var showSummary = function () {
            $scope.report = $scope.hoverReport;
            $scope.notPreview = true;
        };

        // set birthday based on age
        var setBirthday = function () {
            var value = $scope.order.currentAge;
            var strAgeUnit = $scope.order.ageType;
            if (!value) {
                return;
            }
            if (!strAgeUnit) {
                return;
            }
            var result = registrationUtil.setBirthday(strAgeUnit, value);
            var ageValid = result.errorMsg === '';
            handleTransformError(true, ageValid, result);
        };

        var setAge = function () {
            var result = registrationUtil.setAge(configurationData.yearNumber, configurationData.monthNumber, configurationData.dayNumber, $scope.patient.birthday);
            var birthdayValid = result.errorMsg === '';
            handleTransformError(birthdayValid, true, result);
        };

        var handleTransformError = function (birthDayIsValid, ageIsValid, result) {
            $scope.newRegistrationForm.birthday.$valid = birthDayIsValid;
            $scope.newRegistrationForm.birthday.$invalid = !birthDayIsValid;
            $scope.newRegistrationForm.currentAge.$invalid = !ageIsValid;
            $scope.newRegistrationForm.currentAge.$valid = ageIsValid;
            $scope.newRegistrationForm.$valid = birthDayIsValid && ageIsValid;
            $scope.newRegistrationForm.$invalid = !$scope.newRegistrationForm.$valid;
            $scope.isShowTransformError = false;
            $scope.newRegistrationForm.birthday.transformAgeError = '';
            $scope.newRegistrationForm.currentAge.transformAgeError = '';
            if (!birthDayIsValid) {
                $scope.newRegistrationForm.birthday.$error.transform = true;
                $scope.newRegistrationForm.birthday.transformAgeError = result.errorMsg;
                $scope.isShowTransformError = true;
            }
            else {
                delete $scope.newRegistrationForm.birthday.$error.transform;
                $scope.order.ageType = result.ageType || $scope.order.ageType;
                $scope.order.currentAge = result.value || $scope.order.currentAge;
            }
            if (!ageIsValid) {
                $scope.isShowTransformError = true;
                $scope.newRegistrationForm.currentAge.$error.transform = true;
                $scope.newRegistrationForm.currentAge.transformAgeError = result.errorMsg;
            }
            else {
                delete $scope.newRegistrationForm.currentAge.$error.transform;
                $scope.patient.birthday = result.newDate || $scope.patient.birthday;
                $scope.patient.birthday = $filter('date')($scope.patient.birthday, 'yyyy-MM-dd');
            }
        };

        var ageTypeChage = function () {
            if ($scope.order.currentAge) {
                setBirthday();
            } else if ($scope.patient.birthday) {
                setAge();
            }
        };
        $scope.hidePopoverClick = function (event) {
            $scope.isShowProcedureHistoryContainer = false;    
            $scope.isShowPopIcon = true;
        };
        $scope.selectBirthday = function (event) {
            event.preventDefault();
            event.stopPropagation();
            $scope.selectBirthdayOpened = true;
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
                save($scope.newRegistrationForm, printMultipleRequisition);
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
                save($scope.newRegistrationForm, printMultipleBarCode);
            }
        };

        var applyDeptFiltering = function (kendoEvent) {
            if (kendoEvent && kendoEvent.filter && kendoEvent.filter.field) {
                $scope.applyDeptKendo = kendoEvent;

                var usePYFilter = true;
                var isSelected = false;
                //judge filter and select
                if (kendoEvent.filter.value != '') {
                    usePYFilter = true;
                    var s = _.findWhere($scope.applyDeptList, { deptName: kendoEvent.filter.value + '' });
                    if (s) {
                        usePYFilter = false;
                        isSelected = true;
                        $scope.order.applyDept = kendoEvent.filter.value;
                    }
                }
                else {
                    usePYFilter = false;
                }

                if (usePYFilter) {
                    //use pingying
                    kendoEvent.filter.field = 'firstPingYinName';
                    var filter = kendoEvent.sender.dataSource.filter();
                    if (filter) {
                        for (var i = 0; i < filter.filters.length; i++) {
                            filter.filters[i].field = "firstPingYinName";
                            filter.filters[i].value = kendoEvent.filter.value;
                        }
                    }
                } else {
                    //clear filter
                    kendoEvent.filter.field = 'deptName';
                    kendoEvent.filter.value = "";
                    var filter = kendoEvent.sender.dataSource.filter();
                    if (filter) {
                        for (var i = 0; i < filter.filters.length; i++) {
                            filter.filters[i].field = "deptName";
                            filter.filters[i].value = "";
                        }
                    }

                    //set select
                    if (isSelected) {
                        kendoEvent.sender.text($scope.order.applyDept);
                    }
                }
            }
        };

        var applyDoctorFiltering = function (kendoEvent) {
            if (kendoEvent && kendoEvent.filter && kendoEvent.filter.field) {
                $scope.applyDoctorKendo = kendoEvent;

                var usePYFilter = true;
                var isSelected = false;
                //judge filter and select
                if (kendoEvent.filter.value != '') {
                    usePYFilter = true;
                    var s = _.findWhere($scope.applyDoctorList, { doctorName: kendoEvent.filter.value + '' });
                    if (s) {
                        usePYFilter = false;
                        isSelected = true;
                        $scope.order.applyDoctor = kendoEvent.filter.value;
                    }
                }
                else {
                    usePYFilter = false;
                }

                if (usePYFilter) {
                    //use pingying
                    kendoEvent.filter.field = 'firstPingYinName';
                    var filter = kendoEvent.sender.dataSource.filter();
                    if (filter) {
                        for (var i = 0; i < filter.filters.length; i++) {
                            filter.filters[i].field = "firstPingYinName";
                            filter.filters[i].value = kendoEvent.filter.value;
                        }
                    }
                } else {
                    //clear filter
                    kendoEvent.filter.field = 'doctorName';
                    kendoEvent.filter.value = "";
                    var filter = kendoEvent.sender.dataSource.filter();
                    if (filter) {
                        for (var i = 0; i < filter.filters.length; i++) {
                            filter.filters[i].field = "doctorName";
                            filter.filters[i].value = "";
                        }
                    }

                    //set select
                    if (isSelected) {
                        kendoEvent.sender.text($scope.order.applyDoctor);
                    }
                }

            }
        };

        var applyDeptChange = function () {
            if (!$scope.canEditAppDepartmentAndAppDoctor && $scope.order.applyDept != null) {
                var a = _.findWhere($scope.applyDeptList, {
                    deptName: $scope.order.applyDept
                });
                if (a) { } else {
                    $scope.order.applyDept = "";
                    if ($scope.applyDeptKendo) {
                        $scope.applyDeptKendo.filter.field = 'deptName';
                        var filter = $scope.applyDeptKendo.sender.dataSource.filter();
                        if (filter) {
                            for (var i = 0; i < filter.filters.length; i++) {
                                filter.filters[i].field = "deptName";
                                filter.filters[i].value = "";
                            }
                        }
                    }
                }
            }
        };

        var applyDoctorChange = function () {
            if (!$scope.canEditAppDepartmentAndAppDoctor && $scope.order.applyDoctor != null) {
                var a = _.findWhere($scope.applyDoctorList, {
                    doctorName: $scope.order.applyDoctor
                });
                if (a) { } else {
                    $scope.order.applyDoctor = "";
                    if ($scope.applyDoctorKendo) {
                        $scope.applyDoctorKendo.filter.field = 'doctorName';
                        var filter = $scope.applyDoctorKendo.sender.dataSource.filter();
                        if (filter) {
                            for (var i = 0; i < filter.filters.length; i++) {
                                filter.filters[i].field = "doctorName";
                                filter.filters[i].value = "";
                            }
                        }
                    }
                }
            }
        };

        // disable combox since ie10 
        var disableApplyCombo = function () {
            if ($scope.deptCombox && $scope.doctCombox && $scope.observationCombox) {
                //
                $scope.deptCombox.enable(false);
                $scope.doctCombox.enable(false);
                $scope.observationCombox.enable(false);
            }
        };

        var scanRequisition = function () {
            registrationService.validateFtp().success(function (result) {
                if (!result) {
                    registrationUtil.showFTPError();
                }
                else {
                    if (!erNo) {
                        registrationService.generateErNo().success(function (result) {
                            erNo = result;
                            showRequisitionWindow();
                        });
                    } else {
                        showRequisitionWindow();
                    }
                }
            });
        };

        var showRequisitionWindow = function () {
            var args = { requisitionFiles: requisitionFiles, erNo: erNo };
            //$rootScope.$broadcast(application.events.showRequisition, args);
            var _scope=$scope.$new(true);
            _scope.args = args;
            var modalInstance = registrationUtil.showRequisition(args, _scope);
            modalInstance.result.then(function (result) {
                if (result && result.length > 0) {
                    requisitionFiles = result;
                }
            });
        };

        $scope.modifySlice = function () {
            var hasProcedure = addedProcedures && addedProcedures.length > 0;
            bookingSlice.modalityEnable = true;
            bookingSlice.modalityTypeEnable = !hasProcedure;
            $scope.timesliceOperater.unlockId = bookingSlice.lockGuid;
            $scope.timesliceOperater.open(function (lockId, timeslice) {
                // update modality in added procedures.
                bookingSlice = timeslice;
                bookingSlice.lockGuid = lockId;
                _.each(addedProcedures, function (p) {
                    p.bookingBeginTime = bookingSlice.bookingBeginTime;
                    p.bookingEndTime = bookingSlice.bookingEndTime;
                    p.bookingTimeAlias = bookingSlice.bookingTimeAlias;
                    p.modality = bookingSlice.modality;
                });

            }
            );
        };
        $scope.birthdayKey = function (e) {
            var e = e || window.event;
            if (e.keyCode === 8) {
                $scope.patient.birthday = '';
            }
            return false;
        };
        $scope.birthdayclick = function () {
            $scope.registEidtbirthdayPicker.open();
        };

        $scope.viewReport = function (procedureId) {
            $modalInstance.dismiss();
            $('.ris-popover').popover('destroy');
            $('.report-preview-content-popover').remove();
            $state.go('ris.report', {
                isPreview: true,
                procedureId: procedureId
            });
        };
        (function initialize() {
            $log.debug('RegistrationEditController.initialize()...');
            $scope.maxDate = new Date();
            
            $scope.isMobile = $rootScope.browser.versions.mobile;
            $scope.isShowPatientContainer = false;
            $scope.isShowProcedureHistoryContainer = false;
            $scope.isShowErrorMsg = false;
            $scope.isShowTransformError = false;
            $scope.addedProcedures = addedProcedures;
            $scope.save = save;
            $scope.cancel = cancel;
            $scope.selectPatient = selectPatient;
            $scope.addProcedure = addProcedure;
            $scope.popoverProcedureHistoryContainer = popoverProcedureHistoryContainer;
            $scope.prepareSummary = prepareSummary;
            $scope.showSummary = showSummary;
            $scope.ageTypeChage = ageTypeChage;
            $scope.patientItems = {};
            $scope.order = {};
            $scope.searchPatient = searchPatient;
            $scope.setAge = setAge;
            $scope.setBirthday = setBirthday;
            $scope.isSaved = false;
            $scope.simplifiedToEnglish = simplifiedToEnglish;
            $scope.deleteAddedProcedure = deleteAddedProcedure;
            $scope.updateAddedProcedure = updateAddedProcedure;
            $scope.closeAllPopover = closeAllPopover;
            $scope.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            $scope.printRequisition = printRequisition;
            $scope.printBarCode = printBarCode;
            $scope.isBooking = args.isBooking || false;
            $scope.title = $translate.instant($scope.isBooking ? 'NewBooking' : 'NewRegistration');
            bookingSlice = args.bookingSlice || null;
            $scope.timesliceOption = function () {
                return  bookingSlice;
            };
            $scope.timesliceOperater = {};
            initConfigurationData();
            $scope.applyDeptKendo = null;
            $scope.applyDeptFiltering = applyDeptFiltering;
            $scope.applyDeptChange = applyDeptChange;
            $scope.applyDoctorKendo = null;
            $scope.applyDoctorFiltering = applyDoctorFiltering;
            $scope.applyDoctorChange = applyDoctorChange;
            $scope.scanRequisition = scanRequisition;
        }());
    }
]);
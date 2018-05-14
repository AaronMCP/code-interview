registrationModule.controller('OrderEditController', ['$log', '$scope',
    '$modalInstance', 'registrationService', 'loginContext', 'application', 'dictionaryManager', 'configurationService',
    'enums', '$translate', '$modal', '$anchorScroll', '$location', 'risDialog', '$filter', 'registrationUtil', 'order', 'configurationData', 'commonMessageHub', '$q','openDialog',
    function ($log, $scope, $modalInstance, registrationService, loginContext, application,
        dictionaryManager, configurationService, enums, $translate, $modal, $anchorScroll, $location,
        risDialog, $filter, registrationUtil, order, configurationData, commonMessageHub, $q, openDialog) {
        'use strict';
        $log.debug('OrderEditController.ctor()...');

        // get base data for registration
        //$scope.patient.PatientNo = getPatientNo();//init the patientNo when new registration window pop up
        //下拉框无数据
        $scope.noDataFound = function (e) {
            if (!e.sender.dataSource.view()[0]) {
                e.sender.ul.parent().parent().find('.k-nodata > div').html('无数据');
            }
        }
        var save = function (form) {
            if (!form.$valid) {
                $scope.isShowErrorMsg = true;
                return;
            }
            if (order.isTransfer) {
                $modalInstance.close($scope.order);
                return;
            }
            if ($scope.order.observation && $scope.order.observation.length > 50) {
                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '临床诊断的长度不能超过50个字符！');
                return;
            }
            $scope.order.site = loginContext.site;
            $scope.order.domain = loginContext.domain;
            registrationService.updateOrder($scope.order).success(function (data) {
                $modalInstance.close(data);
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

                //notify message
                var orderUpdateParams = new commonMessageHub.OrderUpdateParams();
                orderUpdateParams.uniqueID = $scope.order.uniqueID;
                commonMessageHub.publish(commonMessageHub.Messages.OrderUpdated, orderUpdateParams);
            })
                .error(function () {
                    var title = $translate.instant("Tips");
                    var content = $translate.instant("SaveErrorMsg");
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
                    console.log("Save order failed.");
                });
        };


        var cancel = function () {
            $modalInstance.dismiss();
        };

        var changeObservation = function (observation) {
            $scope.order.observation = observation.text;
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

        (function initialize() {
            $log.debug('RegistrationEditController.initialize()...');
            $scope.isShowErrorMsg = false;
            $scope.save = save;
            $scope.cancel = cancel;
            $scope.patientTypeList = _.clone(configurationData.patientTypeList);
            $scope.chargeTypeList = _.clone(configurationData.chargeTypeList);

            if (order.patientTyp&&!_.findWhere($scope.patientTypeList, { value: order.patientType })) {
                $scope.patientTypeList.push({ text: order.patientType, value: order.patientType });
            }
        
            if (order.chargeType &&!_.findWhere($scope.chargeTypeList, { value: order.chargeType })) {
                $scope.chargeTypeList.push({ text: order.chargeType, value: order.chargeType });
          }
            //$scope.canEditAppDepartmentAndAppDoctor = configurationData.canEditAppdepartmentAndAppDoctor;
            $scope.canEditAppDepartmentAndAppDoctor = false;
            var s = _.findWhere(configurationData.canEditAppdepartmentAndAppDoctorList, { moduleID: '0300' });
            if (s) {
                $scope.canEditAppDepartmentAndAppDoctor = s.value === '1';
            }

            //if ($scope.canEditAppDepartmentAndAppDoctor) {
            //    var applyDeptsPromise = configurationService.getApplyDepts(loginContext.site);
            //    var applyDoctorsPromise = configurationService.getApplyDoctors(loginContext.site);
            //    $q.all([applyDeptsPromise, applyDoctorsPromise]).then(function (results) {
            //        //applyDeptsPromise
            //        $scope.applyDeptList = results[0].data;
            //        //applyDoctorsPromise
            //        $scope.applyDoctorList = results[1].data;
            //    });
            //}
            //else {
            //    $scope.applyDeptList = configurationData.applyDeptList;
            //    $scope.applyDoctorList = configurationData.applyDoctorList;
            //}
            $scope.applyDeptList = configurationData.applyDeptList;
            $scope.applyDoctorList = configurationData.applyDoctorList;
            $scope.observationList = configurationData.observationList;
            $scope.changeObservation = changeObservation;
            $scope.order = order;
            $scope.applyDeptKendo = null;
            $scope.applyDeptFiltering = applyDeptFiltering;
            $scope.applyDeptChange = applyDeptChange;
            $scope.applyDoctorKendo = null;
            $scope.applyDoctorFiltering = applyDoctorFiltering;
            $scope.applyDoctorChange = applyDoctorChange;

        }());
    }
]);
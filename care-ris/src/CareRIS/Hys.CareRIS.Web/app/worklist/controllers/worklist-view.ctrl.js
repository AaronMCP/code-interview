worklistModule.controller('WorklistController', [
    '$log', '$scope', 'worklistService', 'constants', 'loginContext',
    '$modal', '$state', 'configurationService', '$timeout', 'enums', 'risDialog', '$translate', '$location', '$rootScope', 'registrationService', 'application', 'registrationUtil', '$interval', 'openDialog', 'clientAgentService',
    function ($log, $scope, worklistService, constants, loginContext,
        $modal, $state, configurationService, $timeout, enums, risDialog, $translate, $location, $rootScope, registrationService, application, registrationUtil, $interval, openDialog, clientAgentService) {

        'use strict';

        $log.debug('WorklistController.ctor()...');

        var patientTypeList = application.configuration.patientTypeList;
        var statusList = application.configuration.statusList;

        var modalityTypeList = [];
        var getModalityTypeList = function () {
            configurationService.getModalityTypes().success(function (data) {
                if (data != null) {
                    modalityTypeList = data;
                }
            });
        };

        var modalityList = [];
        var getModalityList = function () {
            configurationService.getModalities(loginContext.site).success(function (data) {
                if (data != null) {
                    modalityList = _.sortBy(data, function (s) {
                        return s.modalityName;
                    });
                }
            });
        };
        //获取所有站点+用户？当前角色站点
        //站点
        var siteList = [];
        var getUserSiteList = function () {
            configurationService.getUserSites().success(function (data) {
                if (data != null) {
                    siteList = data;
                }
            });
        };
        var getCriteria = function () {
            return worklistService.searchCriteria();
        };

        var originalCriteria = getCriteria();

        var searchCriteria = getCriteria();

        var resetCriteria = function () {
            searchCriteria = getCriteria();
        };

        var setCriteria = function (criteria) {
            searchCriteria = criteria;
        };

        $scope.$on(application.events.searchRegistration, function () {
            resetCriteria();
        });

        var criteriaCleaner = function (criteria) {
            //remove attributes which are not used by searching
            if (!criteria) return;

            _.each(["createTimeRangeOptions", "examineTimeRangeOptions"], function (attr) {
                var attrVal = criteria[attr];
                var itemLen;
                if (angular.isArray(attrVal) && (itemLen = attrVal.length) > 0) {
                    for (var i = 0; i < itemLen; ++i) {
                        _.each(["showButton"], function (a) {
                            delete attrVal[i][a];
                        });
                    }
                }
            });

            switch (criteria.createTimeType) {
                case "range":
                    criteria.createDays = null;
                    break;
                case "days":
                    criteria.createStartDate = null;
                    criteria.createEndDate = null;
                    break;
            }

            switch (criteria.examineTimeType) {
                case "range":
                    criteria.examineDays = null;
                    break;
                case "days":
                    criteria.examineStartDate = null;
                    criteria.examineEndDate = null;
                    break;
            }
        };

        var criteriaEquals = function (criteria1, criteria2) {
            var c1 = _.clone(criteria1),
                c2 = _.clone(criteria2);

            //createTimeType and examineTimeType need not consider
            delete c1.createTimeType;
            delete c2.createTimeType;
            delete c1.examineTimeType;
            delete c2.examineTimeType;

            return angular.toJson(c1) === angular.toJson(c2);
        };
        //弹出搜索框
        var showAdvancedSearch = function (criteria) {
            var currentCriteria = criteria || searchCriteria;
            buildStatusesForDisplay(currentCriteria); //状态转一波字符串

            var modalInstance = $modal.open({
                templateUrl:'/app/worklist/views/advanced-search-view.html',
                controller: 'AdvancedSearchController',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    searchCriteria: function () {
                        return currentCriteria;
                    },
                    patientTypeList: function () {
                        return patientTypeList;
                    },
                    statusList: function () {
                        var list = _.filter(statusList, function (s) { return s.value > 0; });
                        // - '' is required for some reason
                        var sortedSatausList = _.sortBy(list, function (s) { return s.value - ''; });
                        return sortedSatausList;
                    },
                    modalityTypeList: function () {
                        return modalityTypeList;
                    },
                    modalityList: function () {
                        return modalityList;
                    },
                    siteList: function () {
                        return siteList;
                    },
                    getCriteria: function () {
                        return getCriteria;
                    },
                    resetCriteria: function () {
                        return resetCriteria;
                    },
                    setCriteria: function () {
                        return setCriteria;
                    },
                    shortcutAdded: function () {
                        return shortcutAdded;
                    },
                },
                
            });

            modalInstance.result.then(function (advancedCriteria) {
                var criteria = _.clone(advancedCriteria) || {};
                criteriaCleaner(criteria);

                if (!criteriaEquals(criteria, originalCriteria)) {
                    // do something here
                    $log.debug(criteria);

                    if (criteria) {
                        publishSearch(criteria);
                    }
                }
            });
        };

        var publishSearch = function (criteria) {
            //not worlist, notice process and go worklist page by timeout
            if ($location.path() != '/ris/worklist/registrations') {

                $scope.$broadcast('event:startSearch');
                //wait to process startSearch
                $timeout(function () {
                    $location.path('ris/worklist/registrations');
                    $location.url($location.path());
                    $timeout(function () {
                        $scope.$broadcast(application.events.advancedSearchRegistration, criteria);
                        $scope.$emit('event:clearSimpleSearchValue', criteria);
                    }, 0, true);
                }, 0, true);
            } else {
                $scope.$broadcast(application.events.advancedSearchRegistration, criteria);
                $scope.$emit('event:clearSimpleSearchValue', criteria);
            }

        };

        var getShortcuts = function (searchByDefaultShortcut) {
            worklistService.getShortcuts().success(function (shortcuts) {
                $scope.shortcuts = shortcuts;
                //没有选中快捷搜索 就选中默认的
                if (!$scope.selectedShortcut) {
                    for (var i = 0; i < $scope.shortcuts.length; i++) {
                        if ($scope.shortcuts[i].isDefault === true) {
                            $scope.selectedShortcut = $scope.shortcuts[i];
                            $scope.myFiter = $scope.selectedShortcut.name;
                        }
                    }
                    if (!$scope.selectedShortcut) {
                        $scope.myFiter = '我的筛选';
                    }
                }
                
                if (searchByDefaultShortcut) {
                    searchByDefaultShortcut();
                }
            });
        };

        var shortcutAdded = function (shortcut) {
            getShortcuts();
        };

        var buildSearchCriteriaByShortcut = function (shortcut) {
            var criteria = getCriteria();
            angular.extend(criteria, shortcut.criteria);

            if (_.isNull(criteria.createTimeType)) {
                criteria.createTimeType = 'range';
            }
            if (_.isNull(criteria.examineTimeType)) {
                criteria.examineTimeType = 'range';
            }

            if (criteria.createTimeRanges && criteria.createTimeRanges.length > 0) {
                criteria.createTimeRangeOptions = [];
                angular.forEach(criteria.createTimeRanges, function (range) {
                    criteria.createTimeRangeOptions.push({
                        start: new Date(range.startTime),
                        stratStr: '',
                        startMin: '0:00',
                        startMax: '23:00',
                        end: new Date(range.endTime),
                        endStr: '',
                        endMin: '1:00',
                        endMax: '23:59'
                    });
                });
            }

            if (criteria.examineTimeRanges && criteria.examineTimeRanges.length > 0) {
                criteria.examineTimeRangeOptions = [];
                angular.forEach(criteria.examineTimeRanges, function (range) {
                    criteria.examineTimeRangeOptions.push({
                        start: new Date(range.startTime),
                        stratStr: '',
                        startMin: '0:00',
                        startMax: '23:00',
                        end: new Date(range.endTime),
                        endStr: '',
                        endMin: '1:00',
                        endMax: '23:59'
                    });
                });
            }
            criteria.shortcutName = shortcut.name;
            return criteria;
        };

        //快捷搜索
        var searchByShortcut = function (shortcut) {
            if ($scope.selectedShortcut) {
                $scope.selectedShortcut.selected = false;
            }
            shortcut.selected = true;
            $scope.selectedShortcut = shortcut;
            $scope.myFiter = $scope.selectedShortcut.name;
            searchCriteria = buildSearchCriteriaByShortcut(shortcut);
            searchCriteria.pagination.pageIndex = 1;
            searchCriteria.pagination.pageSize = constants.pageSize;
            publishSearch(searchCriteria);
        };

        //删除快捷方式
        var deleteShortcut = function ($event, shortcut) {
            openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant('ConfirmDeleteSearchCriteriaShortcut').format(shortcut.name), function () {
                worklistService.deleteShortcut(shortcut.uniqueID).success(function () {
                    getShortcuts();
                });
            });

            $event.stopPropagation();
        };

        //编辑快捷方式
        var editShortcut = function ($event, shortcut) {
            var criteria = buildSearchCriteriaByShortcut(shortcut);

            showAdvancedSearch(criteria);
        };

        var buildStatusesForDisplay = function (criteria) {
            if (_.isArray(criteria.statuses) && criteria.statuses.length > 0 && !_.isObject(criteria.statuses[0])) {
                var statuses = [];
                _.each(criteria.statuses, function (status) {
                    var foundStatus = status + "";
                    if (foundStatus) {
                        statuses.push(foundStatus);
                    }
                });

                criteria.statuses = statuses;
            }
        };

        var setAsDefaultShortcut = function ($event, shortcut) {
            worklistService.setDefaultShortcut(shortcut.uniqueID).success(function () {
                getShortcuts();
            });

            $event.stopPropagation();
        };

        var searchAll = function () {
            resetCriteria();

            publishSearch(searchCriteria);
        };

        var searchToday = function () {
            resetCriteria();
            var today = new Date();
            searchCriteria.createStartDate = today;
            searchCriteria.createEndDate = today;

            publishSearch(searchCriteria);
        };

        var searchWeek = function () {
            resetCriteria();
            var aDay = new Date();
            aDay.setDate(aDay.getDate() - 7);
            searchCriteria.createStartDate = aDay;
            searchCriteria.createEndDate = new Date();

            publishSearch(searchCriteria);
        };


        $scope.$on('event:showAdvancedSearch', function (e) {
            e.preventDefault();

            $timeout(function () {
                showAdvancedSearch();
            }, 10);
        });

        var showNewRegistration = function (args) {
            var modalInstance = $modal.open({
                controller: 'RegistrationEditController',
                templateUrl:"/app/registration/views/registration-edit-view.html",
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    args: function () {
                        return args;
                    }
                }
            });
            modalInstance.result.then(function (result) {
                if (result) {
                    $log.debug(result);
                }
            });
        };

        $scope.$on('event:showNewRegistration', function (e, args) {
            e.preventDefault();
            showNewRegistration(args);
        });

        var showSimilarPatientsWindow = function (similarPatients, requestInfoParam) {
            var modalInstance = $modal.open({
                templateUrl:'/app/registration/registration-transfer/views/patient-similar-view.html',
                controller: 'PatientSimilarController',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    similarPatients: function () {
                        return similarPatients;
                    },
                    requestInfo: function () {
                        return requestInfoParam;
                    }
                }
            });
            modalInstance.result.then(function (result) {
                if (result) {
                    validatePatient(result);
                } else {
                    // null,new patient
                    validatePatient(requestInfo.patient);
                }
            });
        };

        // old patient merge
        var validatePatient = function (patient) {
            var contrastNames = ['localName', 'referenceNo', 'gender', 'currentAge', 'birthday', 'telephone', 'address'];
            var index = _.findIndex(contrastNames, function (name) {
                return patient[name] !== requestInfo.patient[name];
            });
            if (index > -1) {
                showMergePatientsWindow(patient);
            } else {
                // patient is the same,transfer immediately
                requestInfo.patient = patient;
                if (requestInfo.isHasNoRequestItem) {
                    createRegistration(patient, requestInfo);
                } else { // registration transfer
                    transferRegistration({
                        requestInfo: requestInfo
                    });
                }
            }
        };

        var showMergePatientsWindow = function (existPatient) {
            var modalInstance = $modal.open({
                templateUrl:'/app/registration/registration-transfer/views/patient-merge-view.html',
                controller: 'PatientMergeController',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    existPatient: function () {
                        return existPatient;
                    },
                    intergractionPatient: function () {
                        return requestInfo.patient;
                    }
                }
            });
            modalInstance.result.then(function (result) {
                requestInfo.patient = result;
                if (requestInfo.isHasNoRequestItem) {
                    createRegistration(result, requestInfo);
                } else { // registration transfer
                    transferRegistration({
                        requestInfo: requestInfo
                    });
                }
            });
        };

        // request info will be got firstly
        var requestInfo;
        $scope.readCard = function (event) {
            if (event.keyCode === enums.keyCode.enter) {
                if ($scope.card.cardNumber) {
                    processRequests($scope.card.cardNumber);
                } else {
                    getCardNo();
                }
            }
        };

        //var previousNo;
        var getCardNo = function () {
            clientAgentService.getCardNo().success(function (data) {
                if (data) {
                    $scope.card.cardNumber = data;
                    processRequests(data);
                }
            });
        };

        var processRequests = function (cardNumber) {
            registrationService.getRequestInfo(cardNumber, "").success(function (data) {
                requestInfo = data;
                if (!requestInfo) {
                    return;
                }
                var patient = requestInfo.patient;
                if (!patient) {
                    return;
                }
                //HISID must be not null
                if (!patient.hisID) {
                    var title = $translate.instant("Tips");
                    var content = $translate.instant("InValidData");
                    registrationUtil.openDialog(title, content);
                    return;
                }
                // validate whether the request has been transfered
                if (!requestInfo.isHasNoRequestItem) {
                    var loadedRequest = _.filter(requestInfo.requests, function (request) {
                        if (request.risPatientID) return true;
                    });
                    if (loadedRequest.length > 0) {
                        registrationService.getPatient(loadedRequest[0].risPatientID).success(function (result) {
                            if (result) {
                                validatePatient(result);
                            } else {
                                similarPatientHandle(patient);
                            }
                        })
                        .error(function (e) {
                            $log.error(e);
                        });
                    }
                    else {
                        similarPatientHandle(patient);
                    }
                }
                else {
                    similarPatientHandle(patient);
                }

            });
            $scope.card.cardNumber = '';
        };


        var similarPatientHandle = function (patient) {
            registrationService.getSimilarPatients(patient.globalID, patient.patientNo, patient.hisID, patient.localName)
                .success(function (data) {
                    if (patient.matchKey) {
                        var matchData = [];
                        switch (patient.matchKey.toLowerCase()) {
                            case 'socialid':
                                matchData = _.where(data, {
                                    referenceNo: patient.referenceNo
                                });
                                break;
                            case 'securityno':
                                matchData = _.where(data, {
                                    medicareNo: patient.medicareNo
                                });
                                break;
                            case 'socialsecurityno':
                                matchData = _.where(data, {
                                    socialSecurityNo: patient.socialSecurityNo
                                });
                                break;
                        }
                        var isOnlyOnePatient = matchData.length === 1;
                        if (isOnlyOnePatient) {
                            // new patient validation
                            if (determineIsNewPatient(data[0], patient)) {
                                //new patient
                                requestInfo.patient.patientNo = '';
                                if (requestInfo.isHasNoRequestItem) {
                                    createRegistration(requestInfo.patient, requestInfo);
                                } else {
                                    transferRegistration({
                                        requestInfo: requestInfo
                                    });
                                }
                            } else { //old patient,validate patient to show merge window
                                validatePatient(data[0]);
                            }
                            return;
                        } else if (matchData.length > 1) {
                            data = matchData;
                        }
                    }
                    var isShowPatientList = application.configuration.showPatientListDlg == "1";
                    //if (requestInfo.isHasNoRequestItem) {
                    // multiple patient or showPatientListDlg is true,old patient
                    if (data.length === 0) {
                        // new patient
                        requestInfo.patient.patientNo = "";
                        if (requestInfo.isHasNoRequestItem) {
                            createRegistration(requestInfo.patient, requestInfo);
                        } else {
                            transferRegistration({
                                requestInfo: requestInfo
                            });
                        }
                    } else if (data.length === 1 && !isShowPatientList) {
                        // new patient validation
                        if (determineIsNewPatient(data[0], patient)) {
                            //new patient
                            requestInfo.patient.patientNo = "";
                            if (requestInfo.isHasNoRequestItem) {
                                createRegistration(requestInfo.patient, requestInfo);
                            } else {
                                transferRegistration({
                                    requestInfo: requestInfo
                                });
                            }
                        } else { //old patient,validate patient to show merge window
                            validatePatient(data[0]);
                        }
                    } else { // multiple patients
                        showSimilarPatientsWindow(data);
                    }
                });
        };

        var createRegistration = function (patient, requestInfoData) {
            var request = (requestInfoData && requestInfoData.requests) ? requestInfoData.requests[0] : null;
            var params = {
                patient: patient,
                request: request
            };
            showNewRegistration(params);
        };

        var transferRegistration = function (transferParams) {
            $state.go("ris.worklist.registrationTransfer", transferParams);
        };

        // determine wheather the patient is new or old
        var determineIsNewPatient = function (similarPatient, patient) {
            var isSamePatientId = (similarPatient.patientNo && similarPatient.patientNo === patient.patientNo) ||
                (similarPatient.globalID && similarPatient.globalID === patient.globalID) ||
                (similarPatient.hisID && similarPatient.hisID === patient.hisID);
            if (isSamePatientId) {
                return similarPatient.localName !== patient.localName;
            } else {
                return !(similarPatient.localName === patient.localName &&
                    similarPatient.gender === patient.gender &&
                    similarPatient.birthday === patient.birthday);
            }
        };

        var timesliceSelected = function (lockId, timeslice) {
            timeslice.lockGuid = lockId;
            var args = { isBooking: true, bookingSlice: timeslice };
            showNewRegistration(args);
            console.log(args);
        };
        var timesliceCanceled = function () {
            $log.log('Timeslice canceled');
        };

        $scope.timeSliceClick = function () {
            $scope.timesliceOperater.open(timesliceSelected, timesliceCanceled);
        };
        // initialzation
        ; (function initialize() {
            $log.debug('WorklistController.initialize()...');

            getModalityTypeList();
            getModalityList();
            getUserSiteList();
            $scope.$state = $state;
            $scope.shortcuts = [];
            $scope.selectedShortcut = null;
            $scope.myFiter = '我的筛选';
            $scope.searchByShortcut = searchByShortcut;
            $scope.deleteShortcut = deleteShortcut;
            $scope.editShortcut = editShortcut;
            $scope.setAsDefaultShortcut = setAsDefaultShortcut;
            $scope.searchAll = searchAll;
            $scope.searchToday = searchToday;
            $scope.searchWeek = searchWeek;
            $scope.createRegistration = createRegistration;
            $scope.card = {};
            $scope.timesliceOption = function () {
                return {
                    modalityTypeEnable: true,
                    modalityEnable: true,
                    modalityType: '',
                    modality: '',
                    bookingStarttime: null,
                    bookingEndtime: null
                };
            };
            $scope.timesliceOperater = {};
            getShortcuts(function () {
                if ($scope.shortcuts.length > 0) {
                    var s = _.findWhere($scope.shortcuts, {
                        isDefault: true
                    });
                    if (s) {
                        $timeout(function () {
                            searchByShortcut(s);
                        }, 100);
                    }
                }
            });
        }());
    }
]);
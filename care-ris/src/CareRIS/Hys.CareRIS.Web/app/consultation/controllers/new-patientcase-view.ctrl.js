consultationModule.controller('NewPatientCaseController', ['$log', '$scope', 'consultationService', 'risDialog',
    'application', '$translate', 'registrationService', 'registrationUtil', 'loginContext', '$modal', '$state', '$compile', '$http', 'examService',
    '$stateParams', 'openDialog', '$timeout', '$window', 'loginUser', 'requestService', 'clientAgentService', '$rootScope', 'csdToaster',
    function ($log, $scope, consultationService, risDialog, application, $translate, registrationService, registrationUtil,
        loginContext, $modal, $state, $compile, $http, examService, $stateParams, openDialog, $timeout, $window, loginUser, requestService, clientAgentService, $rootScope, csdToaster) {
        'use strict';
        $log.debug('NewPatientCaseController.ctor()...');

        var processCaseInfo = function (newData) {
            if (newData) {
                $scope.isSavedBaseInfo = true;
                $scope.patientCase = newData;
                if ($scope.user.isJuniorDoctor) {
                    $scope.applyPermisson = $scope.patientCase.isDeleted ? false : true;
                }
                $scope.patientCase.deletedFileData = [];
                $scope.patientCase.deletedItemData = [];
                var age = $scope.patientCase.age.split(' ');
                if (age.length == 2) {
                    $scope.patientCase.currentAge = age[0];
                    $scope.patientCase.ageType = age[1];
                }

                $timeout(function () {
                    var oldValue = $scope.patientCase.history;
                    $scope.patientCase.history = oldValue + 'app';
                    $scope.$digest();
                    $scope.patientCase.history = oldValue;
                    $scope.$digest();
                }, 100, true);
                $timeout(function () {
                    var oldValue = $scope.patientCase.clinicalDiagnosis;
                    $scope.patientCase.clinicalDiagnosis = oldValue + 'app';
                    $scope.$digest();
                    $scope.patientCase.clinicalDiagnosis = oldValue;
                    $scope.$digest();
                }, 200, true);
            }
        };

        var getPatientCase = function () {
            consultationService.getPatientCaseNoItems($scope.patientCaseID).success(function (newData) {
                processCaseInfo(newData);
            });
        };

        var addExamInfo = function () {
            if ($('#patientCaseExamInfoContent').children().length == 0) {
                var tpl = '<exam-info-view disable-edit="disableEdit" patient-case-id="patientCaseID" on-file-deleted="examInfoDeleteFile(patientCaseId,uniqueId)" ' +
                'on-item-deleted="examInfoDeleteItem(patientCaseId,emrItemId)" ' +
                'ng-show="examInfoClass==\'glyphicon-minus\'"  modules-init-value="modulesInitValue"></exam-info-view>';
                $('#patientCaseExamInfoContent').html(tpl);

                $compile($('#patientCaseExamInfoContent').contents())($scope);

            }
        };

        $scope.savePatientCase = function () {
            if ($scope.baseInfoClass == 'glyphicon-minus' && $rootScope.browser.versions.mobile) {
                $scope.savePatientCaseProcess(requestService.goType.none);
            } else {
                $scope.savePatientCaseProcess(requestService.goType.worklist);
            }
        };

        var createNewPatientCaseItems = function () {
            //get exams
            $scope.patientCase.newEMRItems = [];
            $scope.emrItems = [];
            if (examService.modulesData) {
                angular.forEach(examService.modulesData, function (modulesDataItem, index) {
                    if (modulesDataItem && modulesDataItem.length > 0) {
                        angular.forEach(modulesDataItem, function (item, index) {
                            $scope.emrItems.push(item);
                        });
                    }
                });
            }
            //emritems
            if ($scope.emrItems && $scope.emrItems.length > 0) {
                $scope.patientCase.newEMRItems = [];
                angular.forEach($scope.emrItems, function (item, index) {
                    var newEMRItem = {};
                    if (item.uniqueID) {
                        newEMRItem.uniqueID = item.uniqueID;
                    }
                    newEMRItem.eMRItemType = item.emrItemType;
                    newEMRItem.accessionNo = item.accessionNo;
                    if (item.examSection) {
                        newEMRItem.examSection = item.examSection;
                    }
                    if (item.patientNo) {
                        newEMRItem.patientNo = item.patientNo;
                    }
                    newEMRItem.examDate = item.examDate;
                    if (item.bodyPart) {
                        newEMRItem.bodyPart = item.bodyPart;
                    }
                    if (item.examDescription) {
                        newEMRItem.examDescription = item.examDescription;
                    }

                    //files
                    if (item.itemDetails) {
                        newEMRItem.itemFiles = [];
                        angular.forEach(item.itemDetails, function (itemFile) {
                            var newItemFile = {};
                            if (itemFile.file.uniqueId) {
                                newItemFile.uniqueID = itemFile.file.uniqueId;
                            }
                            newItemFile.fileType = itemFile.file.fileType;
                            newItemFile.fileName = itemFile.file.fileName;
                            newItemFile.path = itemFile.file.path;
                            newItemFile.detailedId = itemFile.file.detailedId;
                            newItemFile.srcInfo = $scope.srcInfo;
                            newItemFile.isFromRis = itemFile.file.isFromRis||false;
                            newItemFile.pacsAccessionNo = itemFile.file.pacsAccessionNo||null;
                            newItemFile.pacsPatientId = itemFile.file.pacsPatientId||null;
                            newEMRItem.itemFiles.push(newItemFile);
                        }
                        );
                    }

                    $scope.patientCase.newEMRItems.push(newEMRItem);
                }
                );
            } else if ($scope.modulesInitValue) {
                for (var key in $scope.modulesInitValue) {
                    if ($scope.modulesInitValue.hasOwnProperty(key)) {
                        var emr = {}, value = $scope.modulesInitValue[key][0];
                        emr.eMRItemType = key;
                        emr.accessionNo = value.accessionNo;
                        emr.bodyPart = value.bodyPart;
                        emr.examDate = value.examDate;
                        emr.examDescription = value.examDescription;
                        emr.patientNo = value.patientNo;
                        _.each(value.fileList, function (p) {
                            p.srcInfo = $scope.srcInfo;
                        });
                        emr.itemFiles = value.fileList;
                        $scope.patientCase.newEMRItems.push(emr);
                    }
                }
            }
        };

        var savePatientCaseProcessData = function (type) {
            if ($scope.validateData()) {
                $scope.patientCase.isMobile = $rootScope.browser.versions.mobile;
                $scope.patientCase.age = $scope.patientCase.currentAge + ' ' + $scope.patientCase.ageType;
                createNewPatientCaseItems();
                $scope.isSavedBaseInfo = true;
                //add items
                if ($scope.patientCaseID === '') {
                    $scope.patientCase.modules = examService.modules;
                    consultationService.createPatientCase($scope.patientCase).success(function (newData) {
                        $scope.patientCaseID = newData.uniqueID;
                        $scope.patientCase.uniqueID = newData.uniqueID;
                        newData.isFromRis = $scope.patientCase.isFromRis;
                        var combinePatientCase = {
                            PatientId: newData.uniqueID,
                            PatientNo: newData.patientNo,
                            HospitalId: newData.hospitalId,
                            IdentityCard: newData.identityCard
                        };

                        consultationService.GetCombinePatientCaseListAsync(combinePatientCase).success(function (combinePatientCaseList) {
                            if (combinePatientCaseList && combinePatientCaseList.length > 0) {
                                requestService.combinePatient(newData, combinePatientCaseList, type);
                            }
                            else {
                                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Success, $translate.instant("SaveSuccess"), '', requestService.returnConsultWorkList(type, newData));

                            }
                        });
                    });
                }
                else {
                    $scope.patientCase.modules = [];
                    $scope.patientCase.moduleIsNew = examService.isNew;
                    if (examService.isNew) {
                        $scope.patientCase.modules = examService.modules;
                    }
                    else {
                        angular.forEach(examService.updatedModules, function (item) {
                            $scope.patientCase.modules.push(item);
                        });
                    }

                    consultationService.editPatientCase($scope.patientCase).success(function () {
                        if (type === requestService.goType.recover) {
                            $scope.patientCase.patientCaseID = $scope.patientCase.uniqueID;
                            requestService.recoverPatientCase(requestService.goType.recover, $scope.patientCase);
                        } else {
                            consultationService.GetCombinePatientCaseList($scope.patientCase.uniqueID, $scope.patientCase.identityCard).success(function (combinePatientCaseList) {
                                if (combinePatientCaseList && combinePatientCaseList.length > 0) {
                                    requestService.combinePatient($scope.patientCase, combinePatientCaseList, type);
                                }
                                else {
                                    openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Success, $translate.instant('SaveSuccess'), '', requestService.returnConsultWorkList(type, $scope.patientCase));
                                }
                            });
                        }
                    });
                }
            }
        };

        $scope.returnConsultWorkList = function (type) {
            requestService.returnConsultWorkList(type,$scope.patientCase);
        }

        $scope.savePatientCaseProcess = function (type) {
            if ($rootScope.browser.versions.mobile) {
                savePatientCaseProcessData(type);
            } else {
                if ($scope.srcInfo === '') {
                    var param = { damid: '', apihost: '' };
                    clientAgentService.GetProcessIDForDam(param).success(function (data) {
                        $scope.srcInfo = data;
                        savePatientCaseProcessData(type);
                    });
                }
                else {
                    savePatientCaseProcessData(type);
                }
            }

        };

        $scope.saveAndRequest = function () {
            $scope.savePatientCaseProcess(requestService.goType.request);
        };

        $scope.validateData = function () {
            if ($scope.patientCase.insuranceNumber == null || $scope.patientCase.insuranceNumber == '') {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('InsuranceNumber') + $translate.instant('IsRequiredErrorMsg'),
                    function () {
                        $scope.showBaseInfo();
                        $('#insuranceNumber').focus();
                    });
                return false;
            }

            if ($scope.patientCase.patientName == null || $scope.patientCase.patientName == '') {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('PatientName') + $translate.instant('IsRequiredErrorMsg'),
                    function () {
                        $scope.showBaseInfo();
                        $('#patientName').focus();
                    });
                return false;
            }

            if ($scope.patientCase.gender == null || $scope.patientCase.gender == '') {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('Gender') + $translate.instant('IsRequiredErrorMsg'),
                    function () {
                        $scope.showBaseInfo();
                        $('#gender').focus();
                    });
                return false;
            }

            if ($scope.patientCase.identityCard == null || $scope.patientCase.identityCard == '') {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('ReferenceNo') + $translate.instant('IsRequiredErrorMsg'),
                    function () {
                        $scope.showBaseInfo();
                        $('#referenceNo').focus();
                    });
                return false;
            }
            if (registrationUtil.IdCardValidate($scope.patientCase.identityCard) != 0) {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('IDCardNumberError'),
                    function () {
                        $scope.showBaseInfo();
                        $('#referenceNo').focus();
                    });

                return false;
            }

            if ($scope.patientCase.birthday == undefined) {
                csdToaster.pop('info', $translate.instant("InvalidBirthdayErrorMsg"), '');
                return false;
            }

            if ($scope.patientCase.birthday && $scope.patientCase.birthday != '') {
                var result = registrationUtil.setAge(application.configuration.yearNumber, application.configuration.monthNumber, application.configuration.dayNumber, $scope.patientCase.birthday);
                var birthdayValid = result.errorMsg === '';
                if (!birthdayValid) {
                    csdToaster.pop('info', result.errorMsg, '');
                    return false;
                }

            }

            if ($scope.patientCase.currentAge == null || $scope.patientCase.currentAge == '') {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('Age') + $translate.instant('IsRequiredErrorMsg'),
                    function () {
                        $scope.showBaseInfo();
                        $('#age').focus();
                    });
                return false;
            }

            if ($scope.patientCase.currentAge) {
                var value = $scope.patientCase.currentAge;
                var strAgeUnit = $scope.patientCase.ageType;
                if (!value) {
                    return;
                }
                if (!strAgeUnit) {
                    return;
                }
                var result = registrationUtil.setBirthday(strAgeUnit, value);
                var ageValid = result.errorMsg === '';
                //handleTransformError(true, ageValid, result);
                if (!ageValid) {
                    csdToaster.pop('info', $translate.instant('Age') + result.errorMsg, '');
                    return false;
                }

            }

            var historyText = $.trim($("#history").data("kendoEditor").body.innerText);
            if ($scope.patientCase.history == null || $scope.patientCase.history == ''
                || historyText == '') {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('PatientCaseHistory') + $translate.instant('IsRequiredErrorMsg'),
                    function () {
                        $scope.showBaseInfo();
                        $("#history").data("kendoEditor").focus();
                    });
                return false;
            }


            var clinicalDiagnosisText = $.trim($("#clinicalDiagnosis").data("kendoEditor").body.innerText);
            if ($scope.patientCase.clinicalDiagnosis == null || $scope.patientCase.clinicalDiagnosis == ''
                || clinicalDiagnosisText == '') {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('ClinicalDiagnosis') + $translate.instant('IsRequiredErrorMsg'),
                    function () {
                        $scope.showBaseInfo();
                        $("#clinicalDiagnosis").data("kendoEditor").focus();
                    });

                return false;
            }

            return true;
        };

        $scope.toggleBaseInfo = function () {
            if (!$scope.isSavedBaseInfo && $rootScope.browser.versions.mobile) {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"),
                    '保存病例基本信息先',
                    function () {
                        $scope.showBaseInfo();
                        $('#insuranceNumber').focus();
                    });

                $scope.savePatientButtonText = $translate.instant("SavePatientCase");

                $scope.baseInfoClass = 'glyphicon-minus';
                $('#patientCaseInfoContent').show();
                $scope.examInfoClass = 'glyphicon-plus';
                $('#patientCaseExamInfoContent').hide();
            } else {
                if ($scope.baseInfoClass == 'glyphicon-minus') {

                    if ($rootScope.browser.versions.mobile) {
                        $scope.savePatientButtonText = $translate.instant("SavePatientExam");
                    }

                    $scope.baseInfoClass = 'glyphicon-plus';
                    $('#patientCaseInfoContent').hide();
                    $scope.examInfoClass = 'glyphicon-minus';
                    $('#patientCaseExamInfoContent').show();
                    addExamInfo();
                }
                else {
                    if ($rootScope.browser.versions.mobile) {
                        $scope.savePatientButtonText = $translate.instant("SavePatientCase");
                    }

                    $scope.baseInfoClass = 'glyphicon-minus';
                    $('#patientCaseInfoContent').show();
                    $scope.examInfoClass = 'glyphicon-plus';
                    $('#patientCaseExamInfoContent').hide();
                }
            }
        };

        $scope.showBaseInfo = function () {
            if ($scope.baseInfoClass == 'glyphicon-plus') {
                $scope.baseInfoClass = 'glyphicon-minus';
                $('#patientCaseInfoContent').show();
                $scope.examInfoClass = 'glyphicon-plus';
                $('#patientCaseExamInfoContent').hide();

                if ($rootScope.browser.versions.mobile) {
                    $scope.savePatientButtonText = $translate.instant("SavePatientCase");
                } else {
                    $scope.savePatientButtonText = $translate.instant("SavePatientInfo");
                }
            }
        };

        $scope.toggleExamInfo = function () {
            if (!$scope.isSavedBaseInfo && $rootScope.browser.versions.mobile) {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"),
                   '保存病例基本信息先',
                     function () {
                         $scope.showBaseInfo();
                         $('#insuranceNumber').focus();
                     });

                $scope.savePatientButtonText = $translate.instant("SavePatientCase");

                $scope.examInfoClass = 'glyphicon-plus';
                $('#patientCaseExamInfoContent').hide();
                $scope.baseInfoClass = 'glyphicon-minus';
                $('#patientCaseInfoContent').show();
            } else {
                if ($scope.examInfoClass == 'glyphicon-minus') {
                    if ($rootScope.browser.versions.mobile) {
                        $scope.savePatientButtonText = $translate.instant("SavePatientCase");
                    }

                    $scope.examInfoClass = 'glyphicon-plus';
                    $('#patientCaseExamInfoContent').hide();
                    $scope.baseInfoClass = 'glyphicon-minus';
                    $('#patientCaseInfoContent').show();
                }
                else {
                    if ($rootScope.browser.versions.mobile) {
                        $scope.savePatientButtonText = $translate.instant("SavePatientExam");
                    }

                    $scope.examInfoClass = 'glyphicon-minus';
                    $('#patientCaseExamInfoContent').show();
                    addExamInfo();

                    $scope.baseInfoClass = 'glyphicon-plus';
                    $('#patientCaseInfoContent').hide();
                }
            }
        };

        $scope.changeReferenceNo = function () {
            if ($scope.patientCase.identityCard && ($scope.patientCase.identityCard.length == 18
            || $scope.patientCase.identityCard.length == 15)
                && registrationUtil.IdCardValidate($scope.patientCase.identityCard) == 0) {
                if ($scope.patientCase.identityCard.length == 18) {
                    var year = $scope.patientCase.identityCard.substring(6, 10);
                    var month = $scope.patientCase.identityCard.substring(10, 12);
                    var day = $scope.patientCase.identityCard.substring(12, 14);
                    var temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
                    $scope.patientCase.birthday = temp_date;
                    $scope.setAge();
                }
                else if ($scope.patientCase.identityCard.length == 15) {
                    var year = $scope.patientCase.identityCard.substring(6, 8);
                    var month = $scope.patientCase.identityCard.substring(8, 10);
                    var day = $scope.patientCase.identityCard.substring(10, 12);
                    var temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
                    $scope.patientCase.birthday = temp_date;
                    $scope.setAge();
                }
            }
        };

        $scope.selectBirthday = function (event) {
            event.preventDefault();
            event.stopPropagation();
            $scope.selectBirthdayOpened = true;
        };

        $scope.setBirthday = function () {
            var value = $scope.patientCase.currentAge;
            var strAgeUnit = $scope.patientCase.ageType;
            if (!value) {
                return;
            }
            if (!strAgeUnit) {
                return;
            }
            var result = registrationUtil.setBirthday(strAgeUnit, value);
            var ageValid = result.errorMsg === '';
            //handleTransformError(true, ageValid, result);
            if (ageValid) {
                $scope.patientCase.birthday = result.newDate || $scope.patientCase.birthday;
            }
        };

        $scope.ageTypeChage = function () {
            if ($scope.patientCase.currentAge) {
                $scope.setBirthday();
            } else if ($scope.patientCase.birthday) {
                $scope.setAge();
            }
        };

        $scope.setAge = function () {
            var result = registrationUtil.setAge(application.configuration.yearNumber, application.configuration.monthNumber, application.configuration.dayNumber, $scope.patientCase.birthday);
            var birthdayValid = result.errorMsg === '';
            if (birthdayValid) {
                $scope.patientCase.ageType = result.ageType || $scope.patientCase.ageType;
                $scope.patientCase.currentAge = result.value || $scope.patientCase.currentAge;
            }
        };  

        $scope.getPatientNo = function () {
            consultationService.generatePatientNoAsync().success(function (data) {
                if (data) {
                    $scope.patientCase.patientNo = data;
                } else {
                    patientNoFailedHandle();
                }
            })
            .error(function(error) {
                    patientNoFailedHandle();
                }); 
        };
        var patientNoFailedHandle = function() {
            csdToaster.pop('error', $translate.instant('GetPatientNoFailed'), '');
            requestService.returnConsultWorkList(requestService.goType.worklist);
        };

        $scope.addressKeypress = function (keyEvent) {
            var code = keyEvent.which;
            if (code == 9 || code == 13) {
                $("#history").data("kendoEditor").focus();
                keyEvent.preventDefault();
            }
        };

        $scope.examInfoDeleteFile = function (patientCaseId, uniqueId) {
            if (uniqueId) {
                var ids = uniqueId.split(',');
                $scope.patientCase.deletedFileData = $scope.patientCase.deletedFileData.concat(ids);
            }
        };

        $scope.examInfoDeleteItem = function (patientCaseId, emrItemId) {
            if (emrItemId) {
                $scope.patientCase.deletedItemData.push(emrItemId);
            }
        };

        $scope.openDeletePatientCaseWindow = function (patientCaseId) {
            requestService.openDeletePatientCaseWindow(patientCaseId);
        };

        $scope.recoverPatientCase = function () {
            $scope.savePatientCaseProcess(2);
        };

        var loadSrc = function () {
            consultationService.getUserDamIdAsync().success(function (damId) {
                if (damId) {
                    var param = { damid: damId, apihost: loginContext.apiHost };
                    clientAgentService.GetProcessIDForDam(param).success(function (data) {
                        $scope.srcInfo = data;
                    });
                }
            });
        };

        (function initialize() {
            $scope.user = requestService.getUserPermisson();

            $scope.isSavedBaseInfo = false;
            $scope.savePatientButtonText = $translate.instant("SavePatientInfo");
            $scope.applyPermisson = false;
            $scope.titleBarType = 'patientCase';
            $scope.baseInfoClass = 'glyphicon-minus';
            $scope.examInfoClass = 'glyphicon-plus';
            $scope.emrItems = [];
            $scope.srcInfo = '';
            $scope.patientCaseID = '';
            $scope.disableEdit = $scope.user.isExpert ? true : false;
            examService.clear();
            if (!$rootScope.browser.versions.mobile) {
                loadSrc();
            } else {
                $scope.savePatientButtonText = $translate.instant("SavePatientCase");
            }

            if ($stateParams.patientCaseID) {
                $scope.patientCaseID = $stateParams.patientCaseID;
                $stateParams.patientCaseID = '';
            }
            $scope.patientCase = { patientNo: '', insuranceNumber: '' };
            if ($stateParams.id) {
                $scope.patientCase.orderID = $stateParams.id;
                $stateParams.id = '';
            }
            var configurationData = application.configuration;
            $scope.genderList = configurationData.genderList;
            (configurationData.genderList.length > 0) && ($scope.patientCase.gender = configurationData.genderList[0].value);
            $scope.ageUnitList = configurationData.ageUnitList;
            (configurationData.ageUnitList.length > 0) && ($scope.patientCase.ageType = configurationData.ageUnitList[0].value);
            if (!$scope.patientCaseID) {
                $scope.applyPermisson = true;
                // from RIS
                if ($scope.patientCase.orderID) {
                    consultationService.getCaseInfo($scope.patientCase.orderID).success(function(result) {
                        if (result.isExistPatientCase) {
                            processCaseInfo(result.caseInfo);
                            $scope.patientCaseID = result.caseInfo.uniqueID;
                        } else {
                            $scope.getPatientNo();
                            _.extend($scope.patientCase, result.caseInfo.patientCase);
                            $scope.modulesInitValue = {};
                            var modulesInitValue = result.caseInfo.modulesInitValue;
                            _.extend(modulesInitValue, {
                                fileList: [
                                    {
                                        fileName: modulesInitValue.accessionNo,
                                        fileType: 'file',
                                        path: modulesInitValue.accessionNo,
                                        isFromRis: true,
                                        pacsPatientId: $stateParams.patientId,
                                        pacsAccessionNo: $stateParams.accessionNo
                                    }
                                ]
                            });
                            $scope.modulesInitValue.teleradiology = [modulesInitValue];
                        }
                        $scope.patientCase.isFromRis = true;
                        $scope.setAge();
                    });
                } else {
                    $scope.getPatientNo();
                    // from accepted consultation dicom list
                    if ($stateParams.autoFillCase) {
                        $scope.patientCase.patientName = $stateParams.autoFillCase.patientName;
                        $scope.patientCase.birthday = $stateParams.autoFillCase.birthday;
                        $scope.patientCase.gender = $stateParams.autoFillCase.gender;
                        $scope.modulesInitValue = $stateParams.autoFillCase.modulesInitValue;
                        $scope.setAge();
                    }
                }
            } else {
                getPatientCase();
            }
        }());
    }
]);
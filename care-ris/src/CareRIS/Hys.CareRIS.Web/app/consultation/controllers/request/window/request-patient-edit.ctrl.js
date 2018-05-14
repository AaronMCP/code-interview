consultationModule.controller('RequestPatientEditController', ['$log', '$scope', 'consultationService', 'result', '$modalInstance', 'loginUser', '$translate', 'registrationUtil', 'openDialog', 'application', 'risDialog','csdToaster',
    function ($log, $scope, consultationService, result, $modalInstance, loginUser, $translate, registrationUtil, openDialog, application, risDialog, csdToaster) {
        'use strict';
        $log.debug('RequestPatientEditController.ctor()...');

        $scope.updatePatientCaseBaseInfo = function () {
            if ($scope.validateData()) {
                $scope.patientCase.age = $scope.patientCase.currentAge + ' ' + $scope.patientCase.ageType;
                $scope.patientCase.lastEditUser = $scope.userId;
                $scope.patientCase.type = 2;
                consultationService.updatePatientCaseBaseInfo($scope.patientCase).success(function () {
                    $modalInstance.close($scope.patientCase);
                });
            }
        };

        $scope.validateData = function () {
            if ($scope.patientCase.insuranceNumber == null || $scope.patientCase.insuranceNumber == '') {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('InsuranceNumber') + $translate.instant('IsRequiredErrorMsg'),
                    function () {
                        $('#insuranceNumber').focus();
                    });
                return false;
            }

            if ($scope.patientCase.patientName == null || $scope.patientCase.patientName == '') {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('PatientName') + $translate.instant('IsRequiredErrorMsg'),
                    function () {
                        $('#patientName').focus();
                    });
                return false;
            }

            if ($scope.patientCase.gender == null || $scope.patientCase.gender == '') {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('Gender') + $translate.instant('IsRequiredErrorMsg'),
                    function () {
                        $('#gender').focus();
                    });
                return false;
            }

            if ($scope.patientCase.identityCard == null || $scope.patientCase.identityCard == '') {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('ReferenceNo') + $translate.instant('IsRequiredErrorMsg'),
                    function () {
                        $('#referenceNo').focus();
                    });
                return false;
            }
            if (registrationUtil.IdCardValidate($scope.patientCase.identityCard) != 0) {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('IDCardNumberError'),
                    function () {
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
                        $('#age').focus();
                    });
                return false;
            }

            if ($scope.patientCase.currentAge) {
                var value = $scope.patientCase.currentAge;
                var strAgeUnit = $scope.patientCase.ageType;
                if (!value) {
                    return false;
                }
                if (!strAgeUnit) {
                    return false;
                }
                var age = registrationUtil.setBirthday(strAgeUnit, value);
                var ageValid = age.errorMsg === '';
                if (!ageValid) {
                    csdToaster.pop('info', $translate.instant('Age') + age.errorMsg, '');
                    return false;
                }
            }
            return true;
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

        $scope.close = function () {
            $modalInstance.close();
        };

        (function initialize() {
            $scope.userId = loginUser.user.uniqueID;
            $scope.patientCase = result;

            var ages = $scope.patientCase.age.split(' ');
            $scope.patientCase.ageType = ages[1];
            $scope.patientCase.currentAge = ages[0];

            var configurationData = application.configuration;
            $scope.genderList = configurationData.genderList;
            $scope.ageUnitList = configurationData.ageUnitList;
        }());
    }
]);
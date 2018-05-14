worklistModule.controller('PatientCombineController', ['$log', '$scope', '$modalInstance', 'constants', '$timeout',
    'patientCase', 'consultationService', 'loginContext', '$translate', 'risDialog', 'dictionaryManager', 'enums', 'application', 'openDialog','csdToaster',
    function ($log, $scope, $modalInstance, constants, $timeout, patientCase, consultationService, loginContext, $translate, risDialog,
        dictionaryManager, enums, application, openDialog, csdToaster) {
        'use strict';
        $log.debug('PatientCombineController.ctor()...');

        var saveData = function () {
            //combine person process
            if ($scope.result.patientCombineNo.length < 2)
            {
                csdToaster.pop('info', $translate.instant("SelectPatientNoError"), '');
                return;
            }

            openDialog.openIconDialogOkCancel2(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant('ConformCombine'), function () {
                consultationService.CombinePatientCase($scope.result).success(function (data) {
                    $modalInstance.close();

                });
            })
           
            
        };

      
        var cancel = function () {
            $modalInstance.close();
        };

        var selectRow = function (patientNo) {
            $scope.result.patientCombineNo = [$scope.patientCase.newPatientCase.patientNo, patientNo];
            $scope.patientNoList = [$scope.patientCase.newPatientCase.patientNo, patientNo];
            $scope.result.patientNo = patientNo;
        };

        var convertAgeUnit = function (currentAge) {
            var ageArry = currentAge.split(' ');
            var age = ageArry[0];
            var ageUnit = ageArry[1];
            var a = _.findWhere(application.configuration.ageUnitList, { value: ageUnit });
            if (a) {
                ageUnit = a.text;
                return age + ' ' + ageUnit;
            }
            return currentAge;
        };

        (function initialize() {
            $log.debug('PatientCombineController.initialize()...');
            $scope.patientCase = patientCase;
            $scope.result = { patientCombineNo: [$scope.patientCase.newPatientCase.patientNo], patientNo: $scope.patientCase.newPatientCase.patientNo };
            $scope.combinePatientCaseList = [];
            $scope.convertAgeUnit = convertAgeUnit;

            angular.forEach($scope.patientCase.combinePatientCaseList, function (item) {
                item.age = $scope.convertAgeUnit(item.age);

                var s = _.findWhere($scope.combinePatientCaseList, { patientNo: item.patientNo + '' });
                if (s) {
                    s.patientName += '<br/>' + item.patientName;
                    s.gender += '<br/>' + item.gender;
                    s.age += '<br/>' + item.age;
                } else {
                    $scope.combinePatientCaseList.push(item)
                }
            });

            $scope.saveData = saveData;
            $scope.cancel = cancel;
            $scope.selectRow = selectRow;
            $scope.worklist = {
                dataSource: {
                    data: $scope.combinePatientCaseList,
                    schema: {
                        model: {
                            fields: {
                                patientNo: { type: "string" },
                                patientName: { type: "string" },
                                gender: { type: "string" },
                                age: { type: "string" }
                            }
                        }
                    }
                },
                height: 200,
                scrollable: true,
                columns: [
                    { title: $translate.instant("ConsultPatientNo"), width: "150px", "template": "<input type='radio' name='patientNo' ng-click='selectRow(dataItem.patientNo)'>{{dataItem.patientNo}}</input>" },
                    { field: "patientName", title: $translate.instant("PatientName"), width: "150px", encoded: false },
                    { field: "gender", title: $translate.instant("Gender"), width: "80px", encoded: false },
                    { field: "age", title: $translate.instant("Age"), width: "80px", encoded: false }
                ]

            };

            var configurationData = application.configuration;
            $scope.genderList = configurationData.genderList;

            $scope.combinePatientNoInfo = $translate.instant("CombinePatientNoInfo").replace("{0}", $scope.patientCase.newPatientCase.patientName)
                .replace("{1}", $scope.patientCase.newPatientCase.gender)
                .replace("{2}", $scope.convertAgeUnit($scope.patientCase.newPatientCase.age))
                .replace("{3}", $translate.instant("ReferenceNo") + $scope.patientCase.newPatientCase.identityCard);
            $scope.patientNoList = [$scope.patientCase.newPatientCase.patientNo];
                
        }());
    }
]);



consultationModule.controller('RequestPatientInfoController', ['$log', '$scope', 'consultationService', '$modal', 'loginUser', 'enums', '$rootScope', '$sce', 'requestService',
    function ($log, $scope, consultationService, $modal, loginUser, enums, $rootScope, $sce, requestService) {
        'use strict';
        $log.debug('RequestPatientInfoController.ctor()...');

        function openEditWindow(data, temaplte, controller) {
            var modalInstance = $modal.open({
                templateUrl: 'app/consultation/views/request/window/' + temaplte,
                controller: controller,
                windowClass: 'overflow-hidden advice-edit-window',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    result: function () {
                        return data;
                    }
                }
            });

            modalInstance.result.then(function (result) {
                if (result) {
                    if (result.type === 0) {
                        $scope.report.clinicalDiagnosis = $sce.trustAsHtml(result.clinicalDiagnosis);
                    } else if (result.type === 1) {
                        $scope.report.history = $sce.trustAsHtml(result.history);
                    } else {
                        $scope.report.currentAgeT = requestService.getCurrentAge(result.age);
                        $scope.report.patientNo = result.patientNo;
                        $scope.report.insuranceNumber = result.insuranceNumber;
                        $scope.report.patientName = result.patientName;
                        $scope.report.gender = result.gender;
                        $scope.report.birthday = result.birthday;
                        $scope.report.currentAge = result.age;
                        $scope.report.identityCard = result.identityCard;
                        $scope.report.telephone = result.telephone;
                    }
                }
            });
        };

        $scope.openPatientHisgoryWindow = function () {
            openEditWindow({
                history: $scope.report.history ? $scope.report.history.$$unwrapTrustedValue() : '', 
                patientCaseID: $scope.report.patientCaseID
            }, 'request-history-edit-window.html', 'RequestHistoryEditController');
        };

        $scope.openClinicalDiagnosisWindow = function () {
            openEditWindow({
                clinicalDiagnosis: $scope.report.clinicalDiagnosis ? $scope.report.clinicalDiagnosis.$$unwrapTrustedValue() : '',
                patientCaseID: $scope.report.patientCaseID
            }, 'request-diagnosis-edit-window.html', 'RequestDiagnosisEditController');
        };

        $scope.openPatientInfoWindow = function () {
            openEditWindow({
                patientCaseID: $scope.report.patientCaseID,
                patientNo: $scope.report.patientNo,
                insuranceNumber: $scope.report.insuranceNumber,
                patientName: $scope.report.patientName,
                gender: $scope.report.gender,
                birthday: $scope.report.birthday,
                currentAge: $scope.report.currentAge,
                identityCard: $scope.report.identityCard,
                telephone: $scope.report.telephone,
                age: $scope.report.currentAge
            }, 'request-patient-edit-window.html', 'RequestPatientEditController');
        };

        $scope.openExamInfoWindow = function () {
            $modal.open({
                templateUrl:'/app/consultation/views/request/window/request-examinfo-window.html',
                controller: 'RequestExamInfoEditController',
                windowClass: 'overflow-hidden exam-edit-window',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    patientCase: function () {
                        return {
                            patientCaseID: $scope.report.patientCaseID
                        };
                    }
                }
            });
        };
    }
]);
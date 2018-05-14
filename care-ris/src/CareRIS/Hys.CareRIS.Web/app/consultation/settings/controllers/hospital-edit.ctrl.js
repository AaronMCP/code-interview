consultationModule.controller('HospitalEditController', ['$log', '$scope', '$modalInstance', '$translate', 'consultationService', 'enums', 'hospitals', 'hospital',
    function ($log, $scope, $modalInstance, $translate, consultationService, enums, hospitals, hospital) {
        'use strict';
        $log.debug('HospitalEditController.ctor()...');

        $scope.isEdit = !!hospital;
        $scope.hospitals = hospitals;
        $scope.hospital = hospital || { uniqueID: RIS.newGuid(), status: true };

        $scope.saveHospital = function () {
            consultationService.saveHospital($scope.hospital)
                .success(function (data) { $modalInstance.close(data); })
                .error(function (error) { $log.error(error); });
        }

        $scope.close = function () {
            $modalInstance.close();
        };

        consultationService.getDams().success(function (data) {
            $scope.dams = data;
        });
    }
]);
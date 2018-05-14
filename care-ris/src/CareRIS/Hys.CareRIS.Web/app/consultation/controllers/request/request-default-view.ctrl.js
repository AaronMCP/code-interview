consultationModule.controller('RequestDefaultController', ['$log', '$scope', 'consultationService', '$stateParams',
    '$state', '$modal', 'enums', 'requestService', 
    function ($log, $scope, consultationService, $stateParams, $state, $modal, enums, requestService) {
        'use strict';
        $log.debug('RequestDefaultController.ctor()...');

        (function () {
            $scope.enums = enums;
            $scope.user = requestService.getUserPermisson();
            $scope.report = {};

            if ($stateParams.requestId) {
                consultationService.getConsultationDetailAsync($stateParams.requestId).success(function (data) {
                    $scope.report = requestService.getBaseRequestInfo(data);
   
                    $scope.editRequestCommentPermission = false;
                    $scope.editPatientInfoPermission = false;
                    $scope.editReceivePermission = false;
                });

                consultationService.getConsultationAssigns($stateParams.requestId).success(function (data) {
                    $scope.expertNames = requestService.getConsultationAssignsString(data);
                });
            } else {
                $log.debug('Request Id is null.');
            }
        }());
    }
]);
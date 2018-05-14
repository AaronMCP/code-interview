consultationModule.controller('RequestAcceptController', ['$log', '$scope', 'consultationService', '$stateParams',
    '$state', 'enums','$modal',
    function ($log, $scope, consultationService, $stateParams, $state, enums, $modal) {
        'use strict';
        $log.debug('RequestAcceptController.ctor()...');

        $scope.enums = enums;

        //if ($stateParams.requestId) {
        //    consultationService.getReport($stateParams.requestId).success(function (data) {
        //        $scope.report = data;
        //    });
        //} else {
        //    $state.go('ris.consultation.requests', { searchCriteria: $stateParams.searchCriteria });
        //}

        $scope.acceptRequest = function () {
            var modalInstance = $modal.open({
                templateUrl: 'app/consultation/views/request/request-accept-edit.html',
                controller: 'RequestAcceptEditController',
                backdrop: 'static',
                keyboard: false,
                windowClass: 'acceptRequest'
            });
            modalInstance.result.then(function (result) {
                if (result) {

                }
            }
            );

        };
    }
]);
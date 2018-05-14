consultationModule.controller('RequestRequestInfoController', ['$log', '$scope', 'consultationService', '$modal', '$sce',
    function ($log, $scope, consultationService, $modal, $sce) {
        'use strict';
        $log.debug('RequestRequestInfoController.ctor()...');

        function openEditWindow(request) {
            var modalInstance = $modal.open({
                templateUrl:'/app/consultation/views/request/window/request-info-edit-window.html',
                controller: 'RequestInfoEditController',
                windowClass: 'overflow-hidden advice-edit-window',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    result: function () {
                        return request;
                    }
                }
            });

            modalInstance.result.then(function (result) {
                if (result) {
                    $scope.report.requestPurpose = $sce.trustAsHtml(result.requestPurpose);
                    $scope.report.requestRequirement = $sce.trustAsHtml(result.requestRequirement);
                }
            });
        };

        $scope.openRequestInfoWindow = function () {
            var request = {
                requestId: $scope.report.requestId,
                requestPurpose: $scope.report.requestPurpose ? $scope.report.requestPurpose.$$unwrapTrustedValue() : '',
                requestRequirement: $scope.report.requestRequirement ? $scope.report.requestRequirement.$$unwrapTrustedValue() : ''
            };
            openEditWindow(request);
        };
    }
]);
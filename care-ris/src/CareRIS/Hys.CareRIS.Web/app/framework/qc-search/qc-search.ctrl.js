qualitycontrolModule.controller('QcSearchController', ['$scope', '$log', '$rootScope',
    function ($scope, $log, $rootScope) {
        'use strict';

        $scope.search = function () {
            $rootScope.$broadcast('event:QCSearch', {
                patientName: $scope.patientName,
                accNo: $scope.accNo
            });
        };

        ; (function initialize() {
            $scope.patientName = '';
            $scope.accNo = '';
        }());
    }
]);
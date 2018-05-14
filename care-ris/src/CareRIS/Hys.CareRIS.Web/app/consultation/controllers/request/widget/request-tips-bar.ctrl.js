consultationModule.controller('RequestTipsBarController', ['$log', '$scope',
    function ($log, $scope) {
        'use strict';

        (function initialize() {
            if ($scope.titleBarType) {
                $scope.report = $scope.patientCase;
            }
        }());
    }
]);
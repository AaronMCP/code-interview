consultationModule.controller('RequestInfoBackController', ['$log', '$scope', '$stateParams',
    '$state', '$modal',
    function ($log, $scope, $stateParams, $state) {
        'use strict';

        $scope.returnToRequests = function() {
            $state.go('ris.consultation.requests', { searchCriteria: $stateParams.searchCriteria });
        };
    }
]);
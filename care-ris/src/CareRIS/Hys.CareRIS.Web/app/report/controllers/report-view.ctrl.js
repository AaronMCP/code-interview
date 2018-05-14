reportModule.controller('ReportController', [
    '$log', '$scope', 'enums', 
    function ($log, $scope, enums) {
        'use strict';
        $log.debug('ReportController.ctor()...');

        // initialzation
        (function initialize() {
            $log.debug('ReportController.initialize()...');
            if (!_.isUndefined($scope.isPreview) && $scope.isPreview === 'true') {
                $scope.isPreviewReport = true;
            } else {
                $scope.isPreviewReport = false;

                if (!_.isUndefined($scope.reportId) && !_.isNull($scope.reportId)) {
                    $scope.isCreateNewReport = false;

                } else {
                    $scope.isCreateNewReport = true;
                }
            }

        }());
    }
]);
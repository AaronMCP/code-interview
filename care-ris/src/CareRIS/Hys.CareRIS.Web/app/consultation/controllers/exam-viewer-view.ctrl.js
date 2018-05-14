consultationModule.controller('ExamViewerController', ['$log', '$scope', '$timeout',
function ($log, $scope, $timeout) {
    'use strict';
    $log.debug('ExamViewerController.ctor()...');
    $scope.$watch('iniItem', function (newValue) {
        if (newValue) {
            $timeout(function () { $scope.showFile(newValue); }, 500);
        }
    });
}]);
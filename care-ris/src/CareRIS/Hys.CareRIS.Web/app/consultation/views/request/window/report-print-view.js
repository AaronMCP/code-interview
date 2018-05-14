consultationModule.directive('reportPrintView', ['$log', '$stateParams', function ($log, $stateParams) {
    $log.debug('reportPrintView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/window/report-print-view.html',
        controller: 'ReportPrintViewController',
        replace: true,
        scope: {
            rid: '@'
        },
        link: function (scope, element) {
        }
    };
}]);

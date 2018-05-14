reportModule.directive('reportView', ['$log', '$timeout', function ($log, $timeout) {
    $log.debug('reportView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/report/views/report-view.html',
        controller: 'ReportController',
        scope: {
            // if reportId has value, means edit report or preview report, then check isPreview
            // if reportId has no value, means create report, then check create report for order or for a single procedure
            reportId: '@',
            orderId: '@',
            procedureId: '@',
            isPreview: '@',
            isRead: '@',
            printId: '@',
            from: '@',
            patientCaseOrderId:'@'
        },
        link: function (scope, element) {

        }
    };
}]);

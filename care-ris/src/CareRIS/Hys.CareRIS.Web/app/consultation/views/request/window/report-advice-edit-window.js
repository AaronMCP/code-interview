consultationModule.directive('reportAdviceEditWindow', ['$log', '$stateParams', function ($log, $stateParams) {
    $log.debug('reportExpertAdviceEditWindow.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/window/report-advice-edit-window.html',
        controller: 'ReportAdviceEditController',
        replace: true,
        scope: {
            val: '=',
            close: '&'
        },
        link: function (scope, element) {
        }
    };
}]);

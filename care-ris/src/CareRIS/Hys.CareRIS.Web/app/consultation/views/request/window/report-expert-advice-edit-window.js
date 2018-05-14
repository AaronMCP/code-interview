consultationModule.directive('reportExpertAdviceEditWindow', ['$log', '$stateParams', function ($log, $stateParams) {
    $log.debug('reportExpertAdviceEditWindow.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/window/report-expert-advice-edit-window.html',
        controller: 'ReportExpertAdviceEditController',
        replace: true,
        scope: {
            val: '=',
            close: '&'
        },
        link: function (scope, element) {
        }
    };
}]);

consultationModule.directive('consultationRequestView', ['$log', function ($log) {
    $log.debug('consultationRequestView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/consultation-request-view.html',
        controller: 'ConsultationRequestController',
        replace: true,
        scope: {
        },
        link: function (scope, element) {
        }
    };
}]);
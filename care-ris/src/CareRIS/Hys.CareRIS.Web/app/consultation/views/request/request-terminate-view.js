consultationModule.directive('requestTerminateView', ['$log', function ($log) {
    $log.debug('requestTerminateView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/request-terminate-view.html',
        controller: 'RequestTerminateController',
        replace: true,
        scope: {
        },
        link: function () {
        }
    };
}]);

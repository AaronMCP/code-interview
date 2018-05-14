consultationModule.directive('requestCancelledView', ['$log', function ($log) {
    $log.debug('requestReconsiderationView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/request-cancelled-view.html',
        controller: 'RequestCancelledController',
        replace: true,
        scope: {
        },
        link: function () {
        }
    };
}]);

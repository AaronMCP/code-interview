consultationModule.directive('requestDefaultView', ['$log', function ($log) {
    $log.debug('requestDefaultView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/request-default-view.html',
        controller: 'RequestDefaultController',
        replace: true,
        scope: {
        },
        link: function () {
        }
    };
}]);

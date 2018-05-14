consultationModule.directive('requestReconsiderView', ['$log', function ($log) {
    $log.debug('requestReconsiderView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/request-reconsider-view.html',
        controller: 'RequestReconsiderController',
        replace: true,
        scope: {
        },
        link: function () {
        }
    };
}]);

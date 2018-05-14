consultationModule.directive('requestRejectedView', ['$log', function ($log) {
    $log.debug('requestRejectedView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/request-rejected-view.html',
        controller: 'RequestRejectedController',
        replace: true,
        scope: {
        },
        link: function () {
        }
    };
}]);

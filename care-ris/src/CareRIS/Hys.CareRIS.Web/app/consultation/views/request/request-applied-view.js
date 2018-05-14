consultationModule.directive('requestAppliedView', ['$log', function ($log) {
    $log.debug('requestAppliedView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/request-applied-view.html',
        controller: 'RequestAppliedController',
        replace: true,
        scope: {
        },
        link: function () {
        }
    };
}]);

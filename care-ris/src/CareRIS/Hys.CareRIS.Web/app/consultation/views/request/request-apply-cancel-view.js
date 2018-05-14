consultationModule.directive('requestApplyCancelView', ['$log', function ($log) {
    $log.debug('requestApplyCancelView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/request-apply-cancel-view.html',
        controller: 'RequestApplyCancelController',
        replace: true,
        scope: {
        },
        link: function () {
        }
    };
}]);

consultationModule.directive('requestCommonLayoutView', ['$log', '$stateParams', function ($log, $stateParams) {
    $log.debug('requestCommonLayoutView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/widget/request-common-layout-view.html',
        controller: 'RequestCommonLayoutController',
        replace: true,
        scope: false,
        link: function () {
        }
    };
}]);

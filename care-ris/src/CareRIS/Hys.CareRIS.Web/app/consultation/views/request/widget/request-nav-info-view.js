consultationModule.directive('requestNavInfoView', ['$log', '$stateParams', function ($log, $stateParams) {
    $log.debug('requestNavInfoView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/widget/request-nav-info-view.html',
        controller: 'RequestNavInfoController',
        replace: true,
        scope: false,
        link: function () {
        }
    };
}]);

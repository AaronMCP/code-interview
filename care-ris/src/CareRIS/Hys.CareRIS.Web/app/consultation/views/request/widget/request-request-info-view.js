consultationModule.directive('requestRequestInfoView', ['$log', '$stateParams', function ($log, $stateParams) {
    $log.debug('requestSuggestInfoView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/widget/request-request-info-view.html',
        controller: 'RequestRequestInfoController',
        replace: true,
        scope: false,
        link: function () {
        }
    };
}]);

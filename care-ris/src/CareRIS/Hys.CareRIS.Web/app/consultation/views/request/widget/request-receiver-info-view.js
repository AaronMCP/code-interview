consultationModule.directive('requestReceiverInfoView', ['$log', '$stateParams', function ($log, $stateParams) {
    $log.debug('requestReceiverInfoView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/widget/request-receiver-info-view.html',
        controller: 'RequestReceiverInfoController',
        replace: true,
        scope: false,
        link: function () {
        }
    };
}]);

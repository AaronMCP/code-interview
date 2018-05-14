consultationModule.directive('requestInfoBackButton', [function () {
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/widget/request-info-back-button.html',
        controller: 'RequestInfoBackController',
        replace: true,
        scope: false,
        link: function () {
        }
    };
}]);

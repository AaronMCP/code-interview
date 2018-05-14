consultationModule.directive('requestTipsBar', [function () {
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/widget/request-tips-bar.html',
        controller: 'RequestTipsBarController',
        replace: true,
        scope: false,
        link: function () {
        }
    };
}]);

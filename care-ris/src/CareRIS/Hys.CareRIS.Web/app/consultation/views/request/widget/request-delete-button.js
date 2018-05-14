consultationModule.directive('requestDeleteButton', [function () {
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/widget/request-delete-button.html',
        controller: 'RequestDeleteController',
        replace: true,
        scope: false,
        link: function () {
        }
    };
}]);

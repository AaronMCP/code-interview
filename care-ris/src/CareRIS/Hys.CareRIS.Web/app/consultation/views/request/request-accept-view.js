consultationModule.directive('requestAcceptView', ['$log', '$stateParams', '$compile', function ($log, $stateParams, $compile) {
    $log.debug('requestAccetpView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/request-accept-view.html',
        controller: 'RequestAcceptController',
        replace: true,
        scope: {
        },
        link: function ($scope, $http, $attrs) {

        }
    };
}]);

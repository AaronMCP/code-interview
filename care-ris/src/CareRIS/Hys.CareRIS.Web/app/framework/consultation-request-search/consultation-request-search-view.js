frameworkModule.directive('consultationRequestSearchView', ['$log', function ($log) {
    $log.debug('consultationRequestSearchView');

    return {
        restrict: 'E',
        replace: true,
        templateUrl:'/app/framework/consultation-request-search/consultation-request-search-view.html',
        controller: 'ConsultationRequestSearchController',
        scope: {
        },
    };
}]);
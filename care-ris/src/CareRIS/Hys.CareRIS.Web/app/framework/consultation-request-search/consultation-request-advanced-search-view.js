frameworkModule.directive('consultationRequestAdvancedSearchView', ['$log', function ($log) {
    $log.debug('consultationRequestAdvancedSearchView');

    return {
        restrict: 'E',
        replace: true,
        templateUrl:'/app/framework/consultation-request-search/consultation-request-advanced-search-view.html',
        controller: 'ConsultationRequestAdvancedSearchController',
        scope: {
        },
        link: function (scope, element) {
            scope.closeAddShortcutDialog = function () {
                $('#showShortcutNameButton').popover('hide');
            };
        }
    };
}]);
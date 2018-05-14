consultationModule.directive('consultationView', ['$log', 'application', function ($log, application) {
    $log.debug('consultationView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/consultation-view.html',
        controller: 'ConsultationController',
        replace: true,
        scope: {
        },
        link: function (scope, element) {
            // handlke kendo splitter size change
            scope.onResize = function () {
                scope.$broadcast(application.consultationSidePanelResize);
            };
        }
    };
}]);

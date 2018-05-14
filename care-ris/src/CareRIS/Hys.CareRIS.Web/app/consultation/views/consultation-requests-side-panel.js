consultationModule.directive('consultationRequestsSidePanel', ['$log', function ($log) {
    $log.debug('consultationRequestsSidePanel.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/consultation-requests-side-panel.html',
        controller: 'ConsultationRequestsSidePanelController',
        replace: true,
        scope: true,
        link: function (scope, element) {
        }
    };
}]);

consultationModule.directive('consultationPatientCasesSidePanel', ['$log', function ($log) {
    $log.debug('consultationRequestsSidePanel.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/patientcase/consultation-patient-cases-side-panel.html',
        controller: 'ConsultationPatientCasesSidePanelController',
        replace: true,
        scope: true,
        link: function (scope, element) {
        }
    };
}]);

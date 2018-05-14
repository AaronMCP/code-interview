consultationModule.directive('patientCasesView', ['$log', function ($log) {
    $log.debug('patientCasesView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/patientcase/patient-cases-view.html',
        controller: 'PatientCasesController',
        replace: true,
        scope: {
        },
        link: function (scope, element) {
        }
    };
}]);

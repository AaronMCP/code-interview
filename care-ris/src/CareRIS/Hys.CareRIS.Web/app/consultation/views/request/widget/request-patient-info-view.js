consultationModule.directive('requestPatientInfoView', ['$log', '$stateParams', function ($log, $stateParams) {
    $log.debug('requestPatientInfoView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/widget/request-patient-info-view.html',
        controller: 'RequestPatientInfoController',
        replace: true,
        scope: false,
        link: function () {
        }
    };
}]);

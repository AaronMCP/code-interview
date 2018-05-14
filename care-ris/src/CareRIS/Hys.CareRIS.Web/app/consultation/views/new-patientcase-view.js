consultationModule.directive('newPatietcaseView', ['$log', 'application', '$compile','$timeout',
function ($log, application, $compile, $timeout) {
    $log.debug('newPatientCaseView.ctor()...');

    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/new-patientcase-view.html',
        controller: 'NewPatientCaseController',
        replace: true,
        scope: {
        },
        link: function (scope, element) {
            var $e = $(element);
            var $mainContent = $('#newPatientCaseContainer');
            var $createPatientCaseFormContainer = $e.find('#newPatientCaseFormContainer');
            var $patientCaseExamInfoContent = $e.find('#patientCaseExamInfoContent');
            var onResize = function () {
                $timeout(function () {
                    var newHeight = $mainContent.height() - 55;
                    if (newHeight > 0) {
                        $createPatientCaseFormContainer.height(newHeight);
                        $patientCaseExamInfoContent.height(newHeight - 65);
                    }
                }, 100);
            };
            scope.onResize = onResize;
            $(window).resize(onResize);
            onResize();

        }
    };
}]);
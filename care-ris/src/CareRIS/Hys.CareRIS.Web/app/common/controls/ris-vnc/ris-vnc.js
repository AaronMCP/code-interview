commonModule.directive("risVnc", ['$log', '$timeout', 'consultationService', 'application', '$window','$translate',
    function ($log, $timeout, consultationService, application, $window, $translate) {
        return {
            restrict: 'E',
            templateUrl:'/app/common/controls/ris-vnc/ris-vnc.html',
            replace: true,
            link: function (scope, element) {

                consultationService.getVNC().then(function (vncUrl) {
                    scope.vncUrl = vncUrl.data;
                });
            }
        };
    }
]);
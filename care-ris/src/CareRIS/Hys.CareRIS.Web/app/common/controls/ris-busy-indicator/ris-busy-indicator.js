commonModule.directive('risBusyIndicator', ['busyRequestNotificationHub', '$log', function (busyRequestNotificationHub, $log) {
    'use strict';

    $log.debug('ris-busy-indicator constructor...');

    return {
        restrict: "E",
        templateUrl:'/app/common/controls/ris-busy-indicator/ris-busy-indicator.html',
        replace: true,
        link: function (scope, element) {
            // hide the element initially
            element.hide();

            var startRequestHandler = function () {
                // got the request start notification, show the element
                element.show();
            };

            var endRequestHandler = function () {
                // got the request start notification, show the element
                element.hide();
            };

            busyRequestNotificationHub.onRequestStarted(scope, startRequestHandler);

            busyRequestNotificationHub.onRequestEnded(scope, endRequestHandler);
        },

    };
}]);
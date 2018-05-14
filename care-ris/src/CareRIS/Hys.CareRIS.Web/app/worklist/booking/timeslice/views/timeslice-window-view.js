worklistModule.directive('timesliceWindowView', ['$log', '$compile',
function ($log, $compile) {
    'use strict';
    return {
        restrict: 'E',
        controller: 'TimesliceWindowController',
        templateUrl:'/app/worklist/booking/timeslice/views/timeslice-window-view.html',
        replace: true,
        scope: {
            tWindow: '=?',
            timesliceOption: '=?',
            onTimesliceSelected: '&?',
            onTimesliceCanceled: '&?',
            unlockId:'='
        },
        link: function (scope) {
            scope.timesliceWinActived = function (e) {
                var ae = angular.element(e.sender.element).find('.timeslice-win-content');
                ae.html('<time-slice-view option="timesliceOption"></time-slice-view>');
                scope.timesliceSelected = false;
                $compile(ae.contents())(scope);
            };
            scope.timesliceWinDeactivated = function (e) {
                var ae = angular.element(e.sender.element).find('.timeslice-win-content');
                ae.empty();
                if (scope.timesliceSelected) {
                    scope.onTimesliceSelected({
                        lockId: scope.lockId,
                        timeslice: scope.timesliceValue
                    });
                } else {
                    scope.onTimesliceCanceled();
                }
            };
        }
    }
}
]);
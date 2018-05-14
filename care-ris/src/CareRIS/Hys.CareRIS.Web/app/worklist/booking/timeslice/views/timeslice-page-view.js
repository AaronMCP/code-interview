worklistModule.directive('timeslicePageView', ['$log', '$compile',
    function ($log, $compile) {
        'use strict';
        return {
            restrict: 'E',
            controller: 'TimeslicePageController',
            templateUrl:'/app/worklist/booking/timeslice/views/timeslice-page-view.html',
            replace: true,
            scope: {
                timesliceOption: '=?',
            },
            link: function (scope) {
            
            }
        }
    }
]);
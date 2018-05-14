configurationModule.directive('setupShare', ['$log', '$compile',
    function ($log, $compile) {
        'use strict';
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/app/configuration/management/time-management/setup-share/setup-share.html',
            scope: {
                searchOption: '=',
                isShareData: '=',
                items: '=',
                onOk: '&',
                onCancel: '&',
                sliceids: '=',
                isShareSliceId: '=',
                sliceIdsArr: '='
            },
            controller: 'SetupShareCtrl',
            link: function (scope, element) {
            }
        }
    }])
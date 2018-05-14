/*
sticky table header
*
usage:
*  <div data-ris-table-sticky-header style="max-height: 170px; overflow-x: hidden; overflow-y: auto; margin-bottom: 10px;">
*           <table id="transactions" class="csd-table csd-col8-right" style="display: table;">
*                <thead>
*                    <tr>
*/
commonModule.directive("risTableStickyHeader", ['$log', '$timeout', function ($log, $timeout) {
    'use strict';

    return function (scope, element, attrs) {
        // auto scroll
        if (attrs.scrollOn) {
            scope.$on(attrs.scrollOn, function (event, args) {
                // 35 for sticky header height
                element.scrollTop(element.scrollTop() - 35 + element.find('#' + args.target).closest('tr').position().top);
            });
        }

        $timeout(function () {
            element.find('table').floatThead({
                scrollContainer: function () {
                    return element;
                }
            });
        }, 1000);
    }
}]);
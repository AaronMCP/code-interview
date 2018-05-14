/*
 * @description:
 * select all checkbox effect and control child checkbox with indeterminate status
 * 
 * @usage:
 * <input type="checkbox" data-ris-select-all="#transactions :checkbox">
 * 
 */

commonModule.directive('risSelectAll', ['$timeout', function ($timeout) {
    'use strict';

    return {
        restrict: 'AE',
        link: function ($scope, $element, attr) {
            var allItems = attr.risSelectAll + ' :checkbox';

            function apply() {
                setSelectAllStatus(); // invoke to calculate selected all status, when item selected set by default or via programmably.
                $(allItems).not($element).on('click', setSelectAllStatus);
            }

            function setSelectAllStatus() {
                var $allItems = $(allItems);
                var checkItems = _.filter($allItems, function (item) { return item.checked; });
                // change select all checkbox status
                if (checkItems.length === 0) {
                    $element.prop('indeterminate', false);
                    $element.prop('checked', false);
                }
                else if (checkItems.length === $allItems.length) {
                    $element.prop('indeterminate', false);
                    $element.prop('checked', true);
                } else {
                    $element.prop('indeterminate', true);
                    $element.prop('checked', false);
                }
            }

            $element.on('click', function () {
                var checked = this.checked;
                _.each($(allItems), function (item) {
                    item.checked = checked;
                    $(item).triggerHandler('click');
                });
                setSelectAllStatus();
            });

            if (attr.applyOn) {
                $scope.$on(attr.applyOn, apply);
            } else {
                $timeout(apply, 0);
            }
        }
    };
}]);
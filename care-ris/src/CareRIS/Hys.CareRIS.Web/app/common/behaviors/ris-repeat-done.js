/*
* @description:

* @usage:
* <tr ris-repeat-done="items_done" ng-repeat="item in items">
*/
commonModule.directive('risRepeatDone', ['$timeout', function ($timeout) {
    return {
        restriction: 'A',
        link: function ($scope, element, attr) {
            if ($scope.$last) {
                $timeout(function () {
                    $scope.$emit(attr.risRepeatDone || "repeat_done", element);
                });
            }
        }
    }
}]);
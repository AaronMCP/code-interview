/*
* @description:

* @usage:
* <div ris-auto-height="80%" data-min ="200" data-max="500" data-parent='.parent'>
*/
commonModule.directive('risAutoHeight', ['$timeout', '$window', function ($timeout, $window) {
    return {
        restriction: 'A',
        link: function ($scope, element, attr) {
            var min = +attr.min || 0;
            var max = +attr.max || Number.MAX_VALUE;
            var percentage = parseFloat(attr.risAutoHeight) / 100.0;

            function onResize() {
                var parentHeight = angular.element(attr.parent || $window).height();
                var height = parentHeight * percentage;
                if (height > max) {
                    height = max;
                }
                if (height < min) {
                    height = min;
                }
                element.height(height);
            }

            $timeout(onResize);
            $($window).resize(onResize);
        }
    }
}]);
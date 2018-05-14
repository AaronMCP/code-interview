commonModule.directive("singleClick", ['$timeout', '$parse', '$log', function ($timeout, $parse, $log) {
    return {
        restrict: "A",
        link: function (scope, element,attr) {
            var fn = $parse(attr['singleClick']);
            var delay = 300, clicks = 0, timer = null;
            element.on('click', function (event) {
                clicks++;  //count clicks
                if (clicks === 1) {
                    timer = $timeout(function () {
                        scope.$apply(function () {
                            fn(scope, { $event: event });
                        });
                        clicks = 0;             //after action performed, reset counter
                    }, delay);
                } else {
                    $timeout.cancel(timer);    //prevent single-click action
                    clicks = 0;             //after action performed, reset counter
                }
            });
            scope.$on('$destroy', function () {
                timer && $timeout.cancel(timer);
            });
        }
    };
}]);
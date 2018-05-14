
/*
call with local filter defined in $scope
Usage:
{{input |call:fn:args... | filters...}}
{{summary.copayAmtRange |callwith:toNotSpecified:summary.covered |translate}}
*/
commonModule.filter('call', [function () {
    'use strict';
    return function (input, fn) {
        var restArgs = Array.prototype.slice.call(arguments, 2);
        restArgs.unshift(input);
        return fn.apply(this, restArgs);
    };
}]);
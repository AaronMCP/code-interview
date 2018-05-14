commonModule.filter('default', ['$log', function ($log) {
    'use strict';
    return function (input, defaultValue) {
        if (!input) {
            return defaultValue;
        }
        return input;
    };
}]);

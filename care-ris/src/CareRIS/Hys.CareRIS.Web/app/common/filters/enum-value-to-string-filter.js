commonModule.filter('enumValueToString', ['$log', 'enums', function ($log, enums) {
    'use strict';
    return function (input, enumType) {
        if (enums.hasOwnProperty(enumType)) {
            var theEnum = enums[enumType];
            if (theEnum) {
                return _.invert(theEnum)[input];
            }
        }
        $log.error('js enum has not defined this enum type :{0} or the enum option: {1}, please cleck app/commom/utils/enums.js'.formart(enumType, input));
    };
}]);

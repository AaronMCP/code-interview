/*
datepicker support multiformat

usage:
<datepicker ris-datepicker-multiformat="yyyyMMdd"
*/

(function (angular, $, undefined) {
    'use strict';
    var commonModule = angular.module('app.common');
    commonModule.directive("risDatepickerMultiformat", ['$timeout', function ($timeout) {
        return {
            restrict: "A",
            require: "ngModel",
            link: function (scope, element, attrs, ctrl) {
                $timeout(function () {
                    ctrl.$parsers.unshift(function (datepickerValue) {
                        if (angular.isString(datepickerValue)) {
                            var dateVal = datepickerValue.replace(/[^\d]/g, function () { return ""; });
                            var yPart, mPart, dPart;

                            dateVal.replace(/^(\d{4})(\d{2})(\d{2})$/g, function ($0, $1, $2, $3) {
                                $1 && (yPart = $1);
                                $2 && (mPart = $2);
                                $3 && (dPart = $3);
                            });
                            if (yPart && mPart && dPart) {
                                return new Date(yPart + "/" + mPart + "/" + dPart);
                            }
                        }
                        return datepickerValue;
                    })
                });
            }
        };
    }]);
})(angular, jQuery);
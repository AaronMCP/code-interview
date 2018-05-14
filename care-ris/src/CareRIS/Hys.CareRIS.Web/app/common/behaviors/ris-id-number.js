/* 
 * validate for ID Number
 * usage: <input type="text" ris-id-number/>
 */
(function (angular, $) {
    'use strict';
    var commonModule = angular.module('app.common');
    commonModule.directive("risIdNumber", ["$timeout", function ($timeout) {
        /*********************************************ID Card Validation*****************************************************************/
        var Wi = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1];
        var ValideCode = [1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2]; // the last number validation.10 as X   
        /**  
         * validate 18bit ID Card
         * @param ID Card
         * @return
         * -1:invalid birthday
         *-2:invalida parity bit
         *-3:invalid length 
         */
        var IdCardValidate = function (idCard) {
            idCard = trim(idCard.replace(/ /g, "")); // remove the blank                  
            if (idCard.length == 15) {
                return isValidityBrithBy15IdCard(idCard); // old ID Cards:15 bit
            } else if (idCard.length == 18) {
                var a_idCard = idCard.split(""); // the array of ID Card
                if (isValidityBrithBy18IdCard(idCard) === 0) {
                    return isTrueValidateCodeBy18IdCard(a_idCard);
                }
                else {
                    return -1;
                }
            } else {
                return -3;
            }
        };
        /**  
         * validate 18bit ID Card
         * @param a_idCard array of id card
         * @return
         */
        function isTrueValidateCodeBy18IdCard(a_idCard) {
            var sum = 0;
            if (a_idCard[17].toLowerCase() == 'x') {
                a_idCard[17] = 10;
            }
            for (var i = 0; i < 17; i++) {
                sum += Wi[i] * a_idCard[i];
            }
            var valCodePosition = sum % 11;
            if (a_idCard[17] == ValideCode[valCodePosition]) {
                return 0;
            } else {
                return -2;
            }
        }

        /**  
         * validate 18bit ID Card
         * @param id card
         * @return
         */
        function isValidityBrithBy18IdCard(idCard18) {
            var year = idCard18.substring(6, 10);
            var month = idCard18.substring(10, 12);
            var day = idCard18.substring(12, 14);
            var temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
            if (temp_date.getFullYear() != parseFloat(year) || temp_date.getMonth() != parseFloat(month) - 1 || temp_date.getDate() != parseFloat(day)) {
                return -1;
            } else {
                return 0;
            }
        }

        /**  
         * 15bit validation
         * @param idCard15
         * @return
         */
        function isValidityBrithBy15IdCard(idCard15) {
            var year = idCard15.substring(6, 8);
            var month = idCard15.substring(8, 10);
            var day = idCard15.substring(10, 12);
            var temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
            if (temp_date.getYear() != parseFloat(year) || temp_date.getMonth() != parseFloat(month) - 1 || temp_date.getDate() != parseFloat(day)) {
                return -1;
            } else {
                return 0;
            }
        }

        function trim(str) {
            return str.replace(/(^\s*)|(\s*$)/g, "");
        }
        /****************************************************************************************************************************************/

        return {
            restrict: "A",
            require: 'ngModel',
            link: function (scope, ele, attrs, c) {
                scope.$watch(attrs.ngModel, function () {
                    var idNumber = ele.val();
                    var required = attrs.required;
                    var validatedResult = IdCardValidate(idNumber);
                    
                    if (!required && !idNumber) {
                        c.$setValidity('IDNumber_valid', true);
                        return;
                    }

                    if (required && !idNumber) {
                        c.$setValidity('IDNumber_valid', true);
                        return;
                    }
                    switch (validatedResult) {
                        case 0:
                            c.$setValidity('IDNumber_valid', true);
                            break;
                        default:
                            c.$setValidity('IDNumber_valid', false);
                            break;
                    }
                });
            }
        };
    }]);
})(angular, jQuery);
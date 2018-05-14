/* 
 * auto focus on element
 * usage: <input type="text" ris-auto-focus/>
 */
(function (angular, $) {
    'use strict';
    var commonModule = angular.module('app.common');
    commonModule.directive("risAutoFocus", ['$timeout', '$rootScope', function ($timeout, $rootScope) {
        return {
            restrict: "A",
            link: function (scope, element) {
                if (!$rootScope.browser.versions.mobile) {
                    $timeout(function () {
                        element[0].focus();
                        var elementDom = element.get()[0];
                        if (element.is(":text,textarea")) {

                            if (elementDom.setSelectionRange) {
                                var len = element.val().length * 2;
                                elementDom.setSelectionRange(len, len);
                            }
                            else {
                                element.val(element.val());
                            }
                            elementDom.scrollTop = elementDom.scrollHeight;
                        }
                    }, 800);
                }
            }
        };
    }]);
})(angular, jQuery);
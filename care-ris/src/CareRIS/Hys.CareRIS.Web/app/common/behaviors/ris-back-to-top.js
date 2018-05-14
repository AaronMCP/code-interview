/*
 * @description:
 * scroll specified element to top when item clicked
 * 
 * @usage:
 * <div ris-back-to-top="#elementid">
 * 
 */
commonModule.directive("risBackToTop", function () {
    'use strict';
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            element.on("click", function () {
                $(attr.risBackToTop).animate({ scrollTop: 0 }, "slow");
            });
        }
    };
});
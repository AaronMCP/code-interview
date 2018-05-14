/*
 * @description:
 * only can input number [0-9] string and backspace, 
 * but Allow: backspace, delete, tab, escape, enter, Ctrl+A, home, end, left, right
 * 
 * @usage:
 * <div ris-input-number-string>
 * 
 */

commonModule.directive("risInputNumberString", function () {
    'use strict';
    return {
        restrict: 'A',
        link: function (scope, element, attrs, ngModel) {
            element.on("keydown", function (e) {
                // Allow: backspace, delete, tab, escape, enter and .
                if (_.contains([46, 8, 9, 27, 13, 110], e.keyCode) ||
                    // Allow: Ctrl+A
                    e.ctrlKey === true && _.contains([65, 67, 86, 88], e.keyCode) ||
                    // Allow: home, end, left, right
                    (e.keyCode >= 35 && e.keyCode <= 39)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });
            element.on("paste", function (e) {
                var text = (e.originalEvent || e).clipboardData.getData('text/plain');
                if (text && !text.match(new RegExp("^\\d*$"))) {
                    e.preventDefault();
                }
            });
            element.on("change", function (e) {
                if (attrs.risInputNumberString === 'LargerThanZero' && this.value === '0') {
                    this.value = '';
                }
            });
            element.on("dragenter", function () {
                return false;
            });
            element.on("dragover", function () {
                return false;
            });
            element.on("dragleave", function () {
                return false;
            });
            element.on("drop", function (e) {
                e.preventDefault();
                return false;
            });
        }
    };
});
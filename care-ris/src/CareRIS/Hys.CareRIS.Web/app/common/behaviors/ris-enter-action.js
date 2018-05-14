/* 
 * auto focus on element
 * usage: 
 * <div ris-enter-action>
 * <input type="text" ris-enter-index />
 * <input type="text" ris-enter-index />
 * </div>
 * or
 * <ris-enter-action>
 * <input type="text" ris-enter-index />
 * <input type="text" ris-enter-index />
 * </ris-enter-action>
 */
(function (angular, $) {
    'use strict';
    var commonModule = angular.module('app.common');
    commonModule.directive('risEnterAction', ['$log', '$timeout', 'enums', function ($log, $timeout, enums) {
        return {
            restrict: 'EA',
            link: function (scope, element) {
                $timeout(function () {
                    element.find("[kendo-combo-box][ris-enter-index]").each(function () {
                        var ts = $(this);
                        var indexVal = ts.attr("ris-enter-index");
                        ts.removeAttr("ris-enter-index");
                        ts.parent().find(":text").attr("ris-enter-index", indexVal);
                    });

                    var risEnterElement = element.find("[ris-enter-index]");
                    var risEnterSize = risEnterElement.size();

                    element.on("keypress", "[ris-enter-index]", function (e) {
                        var code = e.which, ts = $(this), indexVal = ts.attr("ris-enter-index");

                        var targetElement = null;
                        if (indexVal) targetElement = $(indexVal);

                        if (code === enums.keyCode.enter) {
                            var currentIndex = risEnterElement.index(ts);
                            var nextIndex = currentIndex + 1;
                            (nextIndex < 0 || nextIndex >= risEnterSize) && (nextIndex = 0);

                            var nextElement = risEnterElement.eq(nextIndex);
                            var nextDom = nextElement.get()[0];
                            $timeout(function () {
                                nextElement.focus();

                                if (nextElement.is(":text,textarea")) {
                                    if (nextDom.setSelectionRange) {
                                        var len = nextElement.val().length * 2;
                                        nextDom.setSelectionRange(len, len);
                                    }
                                    else {
                                        nextElement.val(nextElement.val());
                                    }
                                    nextDom.scrollTop = nextDom.scrollHeight;
                                }
                            });

                            if (targetElement && targetElement.size() > 0) {
                                targetElement.trigger("click");
                            }

                            if (ts.is("select,textarea")) {
                                return false;
                            }
                        }
                    }).on("keypress", "textarea[ris-enter-index]", function (e) {
                        if (e.ctrlKey && e.keyCode == 10) {
                            var ts = this, tsJQ = $(ts);
                            var val = "\n";

                            if (document.selection) {
                                ts.focus();
                                var sel = document.selection.createRange();
                                sel.text = val;
                                ts.focus();
                            }
                            else if (ts.selectionStart || ts.selectionStart == '0') {
                                var startPos = ts.selectionStart;
                                var endPos = ts.selectionEnd;
                                var scrollTop = ts.scrollTop;
                                ts.value = ts.value.substring(0, startPos) + val + ts.value.substring(endPos, ts.value.length);
                                ts.focus();
                                ts.selectionStart = startPos + val.length;
                                ts.selectionEnd = startPos + val.length;
                                ts.scrollTop = scrollTop + 25;
                            } else {
                                ts.value += val;
                                ts.focus();
                            }
                        }
                    })
                }, 800);
            }
        };
    }]);
})(angular, jQuery);
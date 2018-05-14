// Directive for truncating the text and show whole text as tooltip if it is truncated.
// Notice that this directive is working with the '.text-truncate' style class.
// .text-truncate {
//   /*max-width: 200px;*/
//   white-space: nowrap;
//   overflow: hidden;
//   text-overflow: ellipsis;
//   display:inline-block;
// }
// @example,
// - truncate the tab.tabName if its width is greater than 150px. The max-width will be specifed by the value of this directive. 
//   NOTE: specify the max-width in this directive is NOT recommended and may be removed! Please specify it in the css file.
//   <span ris-text-truncate="150px">{{tab.tabName}}</span>
// - the value of max-width is optional, if you already have width or max-width set on the element.
//   <span ris-text-truncate>{{tab.tabName}}</span>
// - in case you do not want to show default tooltip, specify the attribute 'disable-truncate-tooltip ="false"'
//   <span ris-text-truncate="150px" disable-truncate-tooltip="true">{{tab.tabName}}</span>
// - 'truncate-tooltip' can be specified for tooltip content in case the text and tooltip is different or other specical cases.
//   <span ris-text-truncate="150px" truncate-tooltip="tooltip content">{{tab.tabName}}</span>
commonModule.directive('risTextTruncate', ['$log',
function ($log) {
    'use strict';

    $log.debug('risTextTruncate.ctor()...');

    // Runs during compile
    return {
        scope: true, // {} = isolate, true = child, false/undefined = no change // conflict with translate -> multiple directives asking for isolated scope
        restrict: 'A', // E = Element, A = Attribute, C = Class, M = Comment
        link: function ($scope, iElm) {
            var maxWidthWithUnit = iElm.attr('ris-text-truncate');
            if (maxWidthWithUnit) {
                iElm.css('max-width', maxWidthWithUnit);
                // ris-text-truncate: max-width should be set by CSS file on the HTML element, not by the value of "ris-text-truncate"!
            }

            // set 'text-truncate' class to current element
            iElm.addClass('text-truncate');

            // show whole text as tooltip if text is truncated.
            var disableTruncateTooltip = iElm.attr('disable-truncate-tooltip');
            if (!disableTruncateTooltip) {
                iElm.on('mouseover', function () {
                    var jqThis = $(this);
                    jqThis.css({ 'overflow': 'visible' });
                    if (this.scrollWidth > this.offsetWidth) {
                        var tooltipText = iElm.attr('truncate-tooltip');
                        if (!angular.isString(tooltipText) && !angular.isNumber(tooltipText)) {
                            tooltipText = jqThis.text();
                        }

                        if (jqThis.attr('data-original-title') !== tooltipText) {
                            jqThis.attr('data-original-title', tooltipText);
                            jqThis.tooltip({
                                placement: 'bottom',
                                container: 'body'
                            });
                            jqThis.tooltip('show');
                        }
                    } else {
                        jqThis.attr('data-original-title', '');
                        jqThis.tooltip('hide');
                    }
                    jqThis.css({ 'overflow': 'hidden' });
                });
            }
        }
    };
}
]);
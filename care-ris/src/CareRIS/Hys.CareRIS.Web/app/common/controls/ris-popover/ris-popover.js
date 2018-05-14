// Popover control inherit from bootstrap popover.

// Simplest eg. 
//<button title="Test Title" data-content="Here is an example popover" data-placement="right" ris-popover>PopTest</button>

// data-placement attribute.
// It is used to specify the placement of popover.
// Available values: 'bottom' 'top' 'left' 'right' 'topLeft' 'topRight' 'rightTop' 'rightBottom' 'bottomLeft' 'bottomRight' 'leftTop' 'leftBottom'

// Use 'auto' option in data-placement.
// This will allow popover to locate itself automatically which consider the boundary of its parent.
// It will go through an ordered placement list. If there is not enough space for current placement, then it will go to next.
// The placements specified in data-placement attribute will be add to the top of the ordered list.
// eg. <button title="Test Title" data-content="Here is an example popover" data-placement="auto right left top bottom" ris-popover>PopTest</button>

// popover-auto-hide attribute.
// It is used to specify whether hide the popover when mouse click out.
// Set it to 'true' to hide the popover otherwise 'false'.
// Default is 'false'.
// eg. <button popover-auto-hide="true" title="Test Title" data-content="Here is an example popover" ris-popover>PopTest</button>

// data-templateurl attribute.
// It is used to specify url of html template. This template will be load each time the popover openned.
// It will be compiled and link to the scope of this directive.
// eg. <button data-templateurl="/app/ris-popover-test.html" ris-popover>PopTest</button>
// 'onPopoverShown' will be triggered when popover is shown. for example, on-popover-shown="onPopupShown" 
// 'onPopoverHidden' will be triggered when popover is hidden. for exmaple, on-popover-hidden="onPopupHidden"

commonModule.directive('risPopover', ['$compile', '$document', '$parse', function ($compile, $document, $parse) {
    return {
        restrict: "A",
        link: function (scope, element, attr) {
            var popoverOptions = {
                compliler: $compile,
                scope: scope,
                // Disable fade animation for now. Because if you click the button to toggle popover multiple times, sometimes the 
                // popover is not removed from DOM and is invisible. This will block you to click the content behind it.
                animation: false
            };

            if (attr['useOptimizedPlacementAlgorithm']) {
                popoverOptions.useOptimizedPlacementAlgorithm = attr['useOptimizedPlacementAlgorithm'];
            }

            if (attr['popoverContainer']) {
                popoverOptions.container = attr['popoverContainer'];
            }

            if (attr['popoverTrigger']) {
                popoverOptions.trigger = attr['popoverTrigger'];
            }

            element.popover(popoverOptions);

            //update by Alsace
            var isSelect = attr["popoverIsSelect"];;

            var popContent;
            var onClick = function (event) {
                if (!popContent)
                    return;
                var iswithinElement = popContent.find(event.target).length > 0 || popContent.context === event.target;
                var isOnCalendar = $(event.target).closest('.datepicker').length > 0;
                var isInTooltip = $(event.target).closest('.tooltip-inner').length > 0;
                //=>Alsace Updatev
                //var isInChildElement
                var isInModal = false;
               // var isInModal = $(event.target).closest('.modal').length > 0;
                var isInChildPopver = $(event.target).closest('.child-popover').length > 0;
                if ((iswithinElement && !isSelect) || isOnCalendar || isInTooltip || isInModal || isInChildPopver) {
                    event.stopPropagation();
                } else {
                    //for popover is select
                    if (iswithinElement && isSelect)
                    {
                        angular.element(event.target).trigger("click");
                    }
                    element.popover('hide');
                }
            };

            var onResize = function () {
                element.popover('hide');
            };

            var onHybridHeaderClick = function () {
                element.popover('hide');
            };

            var subscribeGlobalEvent = function () {
                $document.on('mousedown', onClick);
                $(window).on('resize', onResize);
            };

            var unsubscribeGlobalEvent = function () {
                $document.off('mousedown', onClick);
                $(window).off('resize', onResize);
            };

            var isAutoHide = attr['popoverAutoHide'] === 'true';
            var onShowPopover = attr['onShowPopover'];

            if (onShowPopover) {
                element.on('show.bs.popover', function (event) {
                    scope[onShowPopover](event);
                });
            }

            element.on('shown.bs.popover', function (event, popElement) {
                var onPopoverShown = attr['onPopoverShown'];
                if (onPopoverShown) {
                    var onPopoverShownAction = $parse(onPopoverShown);
                    onPopoverShownAction(scope).call();

                }

                if (isAutoHide) {
                    unsubscribeGlobalEvent();
                    subscribeGlobalEvent();
                    popContent = $(popElement);
                }
            });

            element.on('hidden.bs.popover', function (event) {
                var onPopoverHidden = attr['onPopoverHidden'];
                if (onPopoverHidden) {
                    scope[onPopoverHidden](event);
                }
                unsubscribeGlobalEvent();
            });

            scope.$on('ris-popover:event', function (event, action) {
                element.popover(action);
            });
        }
    };
}]);
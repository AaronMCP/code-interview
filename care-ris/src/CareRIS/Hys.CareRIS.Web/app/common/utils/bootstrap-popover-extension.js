
(function ($) {
    "use strict";

    var _super = $.fn.popover;

    var Popover = function (element, options) {
        _super.Constructor.apply(this, arguments);
    };

    Popover.prototype = $.extend({}, _super.Constructor.prototype, {
        constructor: Popover,
        _super: function () {
            var args = $.makeArray(arguments);
            _super.Constructor.prototype[args.shift()].apply(this, args);
        },
        show: function () {
            var that = this;
            var e = $.Event('show.bs.' + this.type);
            var templateUrl = this.$element.attr('data-templateUrl');
            var onShow = function () {
                if (templateUrl) {
                    $.get(templateUrl, function (template) {
                        var tip = $(template);
                        that.options.compliler(tip[0])(that.options.scope);
                        that.options.scope.$apply();
                        that.$tip = tip;
                        that.onTipLoaded(that.$tip);
                    })
                } else {
                    that.onTipLoaded(that.tip());
                }
            }

            e.onShow = onShow;

            if ((this.hasContent() || templateUrl) && this.enabled) {
                this.$element.trigger(e);

                if (e.isDefaultPrevented()) return;

                onShow();
            }
        },
        onTipLoaded: function (tip) {
            var that = this;
            var $tip = tip;

            if (this.$element.attr('data-overwrite') !== 'true') {
                this.setContent();
            }

            if (this.options.animation) $tip.addClass('fade');

            var placement = typeof this.options.placement === 'function' ?
                this.options.placement.call(this, $tip[0], this.$element[0]) :
                this.options.placement;
            placement = placement.trim();
            var autoToken = /\s?auto?\s?/i;
            var autoPlace = autoToken.test(placement);
            if (autoPlace) {
                placement = placement.replace(autoToken, '') || 'rightTop';
            }

            $tip
              .detach()
              .css({ top: 0, left: 0, display: 'block' })
              .addClass(placement);

            this.options.container ? $tip.appendTo(this.options.container) : $tip.insertAfter(this.$element);

            var pos = this.getPosition();
            var actualWidth = $tip[0].offsetWidth;
            var actualHeight = $tip[0].offsetHeight;

            if (autoPlace) {
                var $parent = this.$element.parent();

                var orgPlacement = placement;
                var docScroll = document.documentElement.scrollTop || document.body.scrollTop;
                var parentWidth = this.options.container === 'body' ? window.innerWidth : $parent.outerWidth();
                var parentHeight = this.options.container === 'body' ? window.innerHeight : $parent.outerHeight();
                var parentLeft = this.options.container === 'body' ? 0 : $parent.offset().left;
                var parentTop = this.options.container === 'body' ? 0 : $parent.offset().top;
                parentTop += docScroll;

                var placementOrder = placement.split(' ');

                placement = this.getPlacement({
                    placementOrder: placementOrder,
                    pos: pos,
                    actualWidth: actualWidth,
                    actualHeight: actualHeight,
                    parentWidth: parentWidth,
                    parentHeight: parentHeight,
                    parentTop: parentTop,
                    parentLeft: parentLeft,
                    useOptimizedPlacementAlgorithm: this.options.useOptimizedPlacementAlgorithm
                });

                $tip
                  .removeClass(orgPlacement)
                  .addClass(placement);
            }

            var calculatedOffset = this.getCalculatedOffset(placement, pos, actualWidth, actualHeight);

            this.applyPlacement(calculatedOffset, placement);
            this.hoverState = null;

            var complete = function () {
                that.$element.trigger('shown.bs.' + that.type, $tip);
            };

            $.support.transition && this.$tip.hasClass('fade') ?
              $tip
                .one($.support.transition.end, complete)
                .emulateTransitionEnd(150) :
              complete();
        },
        getCalculatedOffset: function (placement, pos, actualWidth, actualHeight) {
            return placement === 'bottom' ? { top: pos.top + pos.height, left: pos.left + pos.width / 2 - actualWidth / 2 } :
                placement === 'top' ? { top: pos.top - actualHeight, left: pos.left + pos.width / 2 - actualWidth / 2 } :
                placement === 'left' ? { top: pos.top + pos.height / 2 - actualHeight / 2, left: pos.left - actualWidth } :
                placement === 'right' ? { top: pos.top + pos.height / 2 - actualHeight / 2, left: pos.left + pos.width } :
                placement === 'topLeft' ? { top: pos.top - actualHeight, left: pos.left + pos.width / 2 - (actualWidth * .25) } :
                placement === 'topRight' ? { top: pos.top - actualHeight, left: pos.left + pos.width / 2 - (actualWidth * .75) } :
                placement === 'rightTop' ? { top: pos.top + pos.height / 2 - (actualHeight * .25), left: pos.left + pos.width } :
                placement === 'rightBottom' ? { top: pos.top + pos.height / 2 - (actualHeight * .75), left: pos.left + pos.width } :
                placement === 'bottomLeft' ? { top: pos.top + pos.height, left: pos.left + pos.width / 2 - (actualWidth * .25) } :
                placement === 'bottomRight' ? { top: pos.top + pos.height, left: pos.left + pos.width / 2 - (actualWidth * .75) } :
                placement === 'leftTop' ? { top: pos.top + pos.height / 2 - (actualHeight * .25), left: pos.left - actualWidth } :
                /*placement === 'leftBottom' ?*/ { top: pos.top + pos.height / 2 - (actualHeight * .75), left: pos.left - actualWidth };
        },
        getPlacement: function (placementInfo) {
            placementInfo.placementOrder.splice(placementInfo.placementOrder.length, 0
            , 'rightTop'
            , 'rightBottom'
            , 'leftTop'
            , 'leftBottom'
            , 'bottomLeft'
            , 'bottomRight'
            , 'topLeft'
            , 'topRight');


            var placementOrder = placementInfo.placementOrder;
            var pos = placementInfo.pos;
            var actualWidth = placementInfo.actualWidth;
            var actualHeight = placementInfo.actualHeight;
            var parentTop = placementInfo.parentTop;
            var parentLeft = placementInfo.parentLeft;
            var parentWidth = placementInfo.parentWidth;
            var parentHeight = placementInfo.parentHeight;

            var minOffsetRate = 100;
            var minOffsetRateIndex = -1;
            for (var i = 0; i < placementOrder.length; i++) {
                var placement = placementOrder[i];
                var tl = this.getCalculatedOffset(placement, pos, actualWidth, actualHeight);
                var canDisplay = tl.top >= parentTop && tl.left >= parentLeft && tl.left + actualWidth <= parentLeft + parentWidth && tl.top + actualHeight <= parentTop + parentHeight;

                if (canDisplay) {
                    return placement;
                } else if (placementInfo.useOptimizedPlacementAlgorithm) {
                    // calculate the offset rate (area of popup outside parent area/popup area)
                    var offsetRate = 0;
                    if (tl.left < parentLeft) {
                        offsetRate += (parentLeft - tl.left) / actualWidth;
                    }
                    if (tl.left + actualWidth > parentLeft + parentWidth) {
                        offsetRate += (tl.left + actualWidth - parentLeft - parentWidth) / actualWidth;
                    }
                    if (tl.top < parentTop) {
                        offsetRate += (parentTop - tl.top) / actualHeight;
                    }
                    if (tl.top + actualHeight > parentTop + parentHeight) {
                        offsetRate += (tl.top + actualHeight - parentTop - parentHeight) / actualHeight;
                    }
                    // find the minimal offset rate
                    if (offsetRate < minOffsetRate) {
                        minOffsetRate = offsetRate;
                        minOffsetRateIndex = i;
                    }
                }
            }

            return placementOrder[placementInfo.useOptimizedPlacementAlgorithm ? minOffsetRateIndex : 0];
        },
        getPosition: function ($element) {
            $element = $element || this.$element

            var el = $element[0]
            var isBody = el.tagName == 'BODY'

            var parentDeclare = this.$element.attr('data-placement-target');
            if (parentDeclare) {
                var parents = this.$element.parents(parentDeclare);

                if (parents.length > 0) {
                    el = parents[0];
                }
            }

            if (!el) {
                el = this.$element[0];
            }

            var elRect    = el.getBoundingClientRect()
            if (elRect.width == null) {
                // width and height are missing in IE8, so compute them manually; see https://github.com/twbs/bootstrap/issues/14093
                elRect = $.extend({}, elRect, { width: elRect.right - elRect.left, height: elRect.bottom - elRect.top })
            }
            var elOffset  = isBody ? { top: 0, left: 0 } : $element.offset()
            var scroll    = { scroll: isBody ? document.documentElement.scrollTop || document.body.scrollTop : $element.scrollTop() }
            var outerDims = isBody ? { width: $(window).width(), height: $(window).height() } : null

            return $.extend({}, elRect, scroll, outerDims, elOffset)
        }
    });

    $.fn.popover = $.extend(function (option) {
        return this.each(function () {
            var $this = $(this)
              , data = $this.data('bs.popover')
              , options = typeof option === 'object' && option;
            if (!data) $this.data('bs.popover', (data = new Popover(this, options)));
            if (typeof option === 'string') data[option]();
        });
    }, _super);
})(jQuery);
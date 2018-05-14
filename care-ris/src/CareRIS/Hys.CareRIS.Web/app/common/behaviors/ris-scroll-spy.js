commonModule.directive('risScrollSpy', ['$window', function ($window) {
    return {
        restrict: 'A',
        controller: ['$scope', function ($scope) {
            $scope.spies = [];
            $scope.isClickNav = false;
            $scope.clear = this.clear = function (ele) {
                var _ref = $scope.spies;
                _.each(_ref, function (e, i) {
                    e.out();
                });
                if (ele) {
                    ele.addClass("itemActive");
                }
            };
            this.addSpy = function (spyObj) {
                $scope.spies.push(spyObj);
            };
            this.changeNavFlag = function (isClickNav) {
                $scope.isClickNav = isClickNav;
            }
        }],
        link: function (scope, elem, attrs) {
            var spyElems;
            spyElems = [];

            scope.$watch('spies', function (spies) {
                var spy, _i, _len, _results;
                _results = [];

                for (_i = 0, _len = spies.length; _i < _len; _i++) {
                    spy = spies[_i];

                    if (spyElems[spy.id] == null) {
                        _results.push(spyElems[spy.id] = elem.find('#' + spy.id));
                    }
                }
                return _results;
            });

            elem.scroll(function () {
                var highlightSpy, pos, spy, _i, _len, _ref,
                    offset = attrs['offset'], isClickNav = scope.isClickNav;
                highlightSpy = null;
                _ref = scope.spies;
                if (isClickNav) {
                    scope.isClickNav = false;
                    return;
                }
                // cycle through `spy` elements to find which to highlight
                for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                    spy = _ref[_i];
                    spy.out();

                    // catch case where a `spy` does not have an associated `id` anchor
                    if (spyElems[spy.id].offset() === undefined) {
                        continue;
                    }

                    if ((pos = spyElems[spy.id].offset().top - offset) - elem[0].scrollTop <= 0) {
                        // the window has been scrolled past the top of a spy element
                        if (-(spyElems[spy.id].offset().top - offset) < spyElems[spy.id][0].offsetHeight) {

                            highlightSpy = spy;
                        } else {
                            highlightSpy = null;
                        }

                        //spy.pos = pos;
                        //if (highlightSpy == null) {
                        //    highlightSpy = spy;
                        //}
                        //if (highlightSpy.pos < spy.pos) {
                        //    highlightSpy = spy;
                        //}
                    }


                }

                //select the last `spy` if the scrollbar is at the bottom of the page
                if (spy && (elem[0].scrollHeight - elem[0].scrollTop) <= elem[0].offsetHeight) {
                    spy.pos = pos;
                    highlightSpy = spy;
                }
                return highlightSpy != null ? highlightSpy["in"]() : void 0;
            });
        }
    };
}]);

commonModule.directive('risSpy', ['$location', '$anchorScroll', function ($location, $anchorScroll) {
    return {
        restrict: "A",
        require: "^risScrollSpy",
        link: function (scope, elem, attrs, affix) {

            elem.click(function () {
                affix.changeNavFlag(true);
                $location.hash(attrs.risSpy);
                $anchorScroll();
                affix.clear(elem);
            });

            affix.addSpy({
                id: attrs.risSpy,
                in: function () {
                    elem.addClass('itemActive');
                },
                out: function () {
                    elem.removeClass('itemActive');
                }
            });
        }
    };
}]);
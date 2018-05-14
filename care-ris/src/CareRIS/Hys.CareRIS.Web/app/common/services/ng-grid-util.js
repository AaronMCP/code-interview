commonModule.factory('ngGridUtil', [function () {
    'use strict';

    return {
        GridLayoutPlugin: function () {
            var self = this;
            this.grid = null;
            this.scope = null;
            this.init = function (scope, grid, services) {
                self.domUtilityService = services.DomUtilityService;
                self.grid = grid;
                self.scope = scope;
            };

            this.updateGridLayout = function () {
                if (!self.scope.$$phase) {
                    self.scope.$apply(function () {
                        self.domUtilityService.RebuildGrid(self.scope, self.grid);
                    });
                } else {
                    // $digest or $apply already in progress
                    self.domUtilityService.RebuildGrid(self.scope, self.grid);
                }
            };
        },

        FlexRowHeight: function () {
            var self = this;
            self.grid = null;
            self.scope = null;
            self.init = function (scope, grid, services) {
                self.domUtilityService = services.DomUtilityService;
                self.grid = grid;
                self.scope = scope;

                var adjust = function () {
                    setTimeout(function () {
                        var row = $(self.grid.$canvas).find('.ngRow'),
                            offs;
                        row.each(function () {
                            var mh = 0,
                                s = angular.element(this).scope();

                            $(this).find('> div').each(function () {
                                var h = $(this)[0].scrollHeight;
                                if (h > mh)
                                    mh = h

                                $(this).css('height', '100%');
                            });

                            $(this).height(mh)

                            if (offs)
                                $(this).css('top', offs + 'px');

                            offs = $(this).position().top + mh;
                            self.scope.$apply(function () {
                                s.rowHeight = mh;
                            });
                        });
                    }, 1);
                }

                self.adjust = adjust;

                self.scope.$watch(self.grid.config.data, adjust, true);
            }
        }
    }
}]);
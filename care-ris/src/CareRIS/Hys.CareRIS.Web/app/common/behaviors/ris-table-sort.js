/*
 angular-tablesort v1.0.4
 (c) 2013 Mattias Holmlund, http://mattiash.github.io/angular-tablesort
 License: MIT
*/

commonModule.directive('risTableSortWrapper', ['$log', '$parse', function ($log, $parse) {
    'use strict';
    return {
        scope: true,
        controller: ['$scope', function ($scope) {
            $scope.sortExpression = [];
            $scope.headings = [];

            var parse_sortexpr = function (expr) {
                return [$parse(expr), null, false];
            };

            this.setSortField = function (sortexpr, element) {
                var i;
                var expr = parse_sortexpr(sortexpr);
                if ($scope.sortExpression.length === 1
                    && $scope.sortExpression[0][0] === expr[0]) {
                    if ($scope.sortExpression[0][2]) {
                        element.removeClass("tablesort-desc");
                        element.addClass("tablesort-asc");
                        $scope.sortExpression[0][2] = false;
                    }
                    else {
                        element.removeClass("tablesort-asc");
                        element.addClass("tablesort-desc");
                        $scope.sortExpression[0][2] = true;
                    }
                }
                else {
                    for (i = 0; i < $scope.headings.length; i = i + 1) {
                        $scope.headings[i]
                            .removeClass("tablesort-desc")
                            .removeClass("tablesort-asc");
                    }
                    element.addClass("tablesort-asc");
                    $scope.sortExpression = [expr];
                }
            };

            this.addSortField = function (sortexpr, element) {
                var i;
                var toggle_order = false;
                var expr = parse_sortexpr(sortexpr);
                for (i = 0; i < $scope.sortExpression.length; i = i + 1) {
                    if ($scope.sortExpression[i][0] === expr[0]) {
                        if ($scope.sortExpression[i][2]) {
                            element.removeClass("tablesort-desc");
                            element.addClass("tablesort-asc");
                            $scope.sortExpression[i][2] = false;
                        }
                        else {
                            element.removeClass("tablesort-asc");
                            element.addClass("tablesort-desc");
                            $scope.sortExpression[i][2] = true;
                        }
                        toggle_order = true;
                    }
                }
                if (!toggle_order) {
                    element.addClass("tablesort-asc");
                    $scope.sortExpression.push(expr);
                }
            };

            this.registerHeading = function (headingelement) {
                $scope.headings.push(headingelement);
            };

            $scope.sortFun = function (a, b) {
                var i, aval, bval, descending, filterFun;
                for (i = 0; i < $scope.sortExpression.length; i = i + 1) {
                    aval = $scope.sortExpression[i][0](a);
                    bval = $scope.sortExpression[i][0](b);
                    filterFun = b[$scope.sortExpression[i][1]];
                    if (filterFun) {
                        aval = filterFun(aval);
                        bval = filterFun(bval);
                    }
                    if (_.isUndefined(aval) || _.isNull(aval)) {
                        aval = "";
                    }
                    if (_.isUndefined(bval) || _.isNull(bval)) {
                        bval = "";
                    }
                    descending = $scope.sortExpression[i][2];
                    if (aval > bval) {
                        return descending ? -1 : 1;
                    }
                    else if (aval < bval) {
                        return descending ? 1 : -1;
                    }
                }
                return 0;
            };
        }]
    };
}]);

commonModule.directive('risTableSortCriteria', function () {
    return {
        require: "^risTableSortWrapper",
        link: function (scope, element, attrs, tsWrapperCtrl) {
            var clickingCallback = function (event) {
                scope.$apply(function () {
                    if (event.shiftKey) {
                        tsWrapperCtrl.addSortField(attrs.risTableSortCriteria, element);
                    }
                    else {
                        tsWrapperCtrl.setSortField(attrs.risTableSortCriteria, element);
                    }
                });
            };
            element.bind('click', clickingCallback);
            element.addClass('tablesort-sortable');
            if ("risTableSortDefault" in attrs && attrs.risTableSortDefault !== "0") {
                tsWrapperCtrl.addSortField(attrs.risTableSortCriteria, element);
                if (attrs.risTableSortDefault === "descending") {
                    tsWrapperCtrl.addSortField(attrs.risTableSortCriteria, element);
                }
            }
            tsWrapperCtrl.registerHeading(element);
        }
    };
});

commonModule.directive("risTableSortRepeat", ['$compile', function ($compile) {
    return {
        terminal: true,
        require: "^risTableSortWrapper",
        priority: 1000000,
        link: function (scope, element, attr) {
            var repeatExpr = element.attr("ng-repeat") || attr.ngRepeat;
            repeatExpr = repeatExpr.replace(/^\s*([\s\S]+?)\s+in\s+([\s\S]+?)(\s+track\s+by\s+[\s\S]+?)?\s*$/,
                "$1 in $2 | tablesortOrderBy:sortFun$3");
            element.removeAttr("ris-table-sort-repeat");
            element.attr("ng-repeat", repeatExpr);
            $compile(element)(scope);
        }
    };
}]);

commonModule.filter('tablesortOrderBy', function () {
    return function (array, sortfun) {
        if (!array) return;
        var arrayCopy = [];
        for (var i = 0; i < array.length; i++) { arrayCopy.push(array[i]); }
        return arrayCopy.sort(sortfun);
    };
});

commonModule.filter('parseInt', function () {
    return function (input) {
        return parseInt(input);
    };
});

commonModule.filter('parseFloat', function () {
    return function (input) {
        return parseFloat(input);
    };
});
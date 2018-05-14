commonModule.directive('risContrast', ['$compile', function ($compile) {
    return {
        restrict: 'EA',
        scope: {
            mergeCheck: "&"
        },
        link: function (scope, element, attrs) {
            var eventList = [];
            var onchange = function () {
                scope.mergeCheck({ name: v, checked: $(this).is(':checked') });
            };
            _.each(scope.$parent.diffNames, function(v) {
                var diffEles = $(element).find('[name=' + v + ']');
                diffEles.addClass("ele-diff");
                var eleTag = $("<input type='checkbox' class='check-diff' checked=true>");

                eleTag.on('change', onchange);
                eventList.push(function() {
                    eleTag.off('change', onchange);
                });
                diffEles.eq(0).children().eq(0).prepend(eleTag);
                $compile(eleTag)(scope);
            });
            // todo
            scope.$destroy(function () {
                _.each(eventList, function (event) { event.call() });
            });
        }
    }
}]);
commonModule.directive('risFieldset', function () {
    return {
        restrict: 'E',
        link: function (scope, element, attrs) {
            scope.$watch(function () { return element.attr('disabled'); }, function (disabled) {
                element.find('input, dropdown,button,a,select, textarea').attr('disabled', disabled)
            });
        }
    }
});
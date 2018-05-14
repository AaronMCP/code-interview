commonModule.directive("risOutsideClick", ['$document', '$parse', function ($document, $parse) {
    return {
        restrict: "A",
        link: function ($scope, $element, $attributes) {
            var scopeExpression = $attributes.risOutsideClick,
                 ignore = $attributes.ignore,
            onClick = function (event) {
                var isChild =$element.context===event.target || $element.find(event.target).length > 0 || (ignore ? event.target.id === ignore : false);
                var isInChildPopver = $(event.target).closest('.popover').length > 0;
                if (!isChild && !isInChildPopver) {
                    $scope.$apply(scopeExpression);
                }
            };
            $document.off('mousedown', onClick);
            $document.on('mousedown', onClick);
            $element.on('$destroy', function () {
                $document.off("mousedown", onClick);
            });
        }
    }
}]);
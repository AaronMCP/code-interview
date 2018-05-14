commonModule.directive('unique', ['$q', function ($q) {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {
            ctrl.$asyncValidators.duplicate = function (modelValue, viewValue) {
                if (ctrl.$pristine || ctrl.$isEmpty(modelValue) || !scope[attrs.unique]) {
                    return $q.when();
                }
                return scope[attrs.unique](viewValue, modelValue);
            };
        }
    }
}]);
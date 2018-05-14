commonModule.directive('risAvatar', ['$log', '$compile', '$timeout', function ($log, $compile, $timeout) {
    'use strict';
    $log.debug('ris-avatar constructor...');
    return {
        restrict: 'E',
        templateUrl:'/app/common/controls/ris-avatar/ris-avatar.html',
        replace: true,
        scope: {
            data: '=',
            defaultImage: '@',
            onChanged: '&?'
        },
        link: function (scope, element) {
            var picker = '<input id="avatarPicker" type="file" ng-model="avatar" base-sixty-four-input accept="image/*" style="display: none;">';

            scope.pickImage = function () {
                $('#avatarPicker').remove();
                $timeout(function() {
                    element.append($compile(picker)(scope));
                    $('#avatarPicker').trigger('click');
                },0);
            }

            scope.remove = function () {
                scope.src = scope.defaultImage;
                scope.data = '';
                scope.onChanged(scope.data);
            }

            scope.$watch(function (s) { return s.avatar; }, function (newValue) {
                if (newValue) {
                    scope.src = 'data:image/png;base64,' + newValue.base64;
                    scope.data = newValue.base64;
                    scope.onChanged(scope.data);
                }
            });

            if (!scope.data && scope.defaultImage) {
                scope.src = scope.defaultImage;
            } else {
                scope.src = 'data:image/png;base64,' + scope.data;
            }
        },
    };
}]);
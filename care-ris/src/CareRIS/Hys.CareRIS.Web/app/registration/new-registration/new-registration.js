newregistrationModule.directive('newRegistration', ['$log',
    function ($log) {
        'use strict';
        return {
            restrict: 'E',
            replace: true,
            templateUrl:'/app/registration/new-registration/new-registration.html',
            controller: 'NewRegistratController',
            link: function (scope, element) {
                element.find("#registbirthday")
                    .on("keydown", function () { return false; })
                    .on("click", function () {
                        scope.registbirthdayPicker.open();
                    });
            }
        }
}])
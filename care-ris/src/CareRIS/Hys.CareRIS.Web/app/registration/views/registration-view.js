registrationModule.directive('registrationView', [
    '$log', '$timeout',
    function ($log, $timeout) {
        'use strict';
        $log.debug('registrationView.ctor()...');
        return {
            restrict: 'E',
            templateUrl:'/app/registration/views/registration-view.html',
            controller: 'RegistrationController',
            scope: {
                orderId: '@',
            },
            link: function (scope, element, attrs) {
                scope.hideModalityPopOver = function (selectorid) {
                    var id = 'P_' + selectorid;
                    $('#' + id + '').popover('hide');
                };
            }
        };
    }]);
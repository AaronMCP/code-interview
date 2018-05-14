configurationModule.directive('roleSystem', ['$log', '$document',
    function ($log, $document) {
        'use strict';
        $log.debug('roleSystem.ctor()...');

        return {
            restrict: 'E',
            templateUrl: '/app/configuration/role-manage/role-system/role-system.html',
            replace: true,
            scope: {
                roleProfileList: '='
            },
            controller: 'RoleSystemController',
            link: function (scope, element) {
                element.find(".input-number")
                    .on("keydown", function () {
                        return false;
                    })
            }
        }
    }
]);
configurationModule.directive('roleManage', ['$log', '$document',
    function ($log, $document) {
        'use strict';
        $log.debug('roleManage.ctor()...');

        return {
            restrict: 'E',
            templateUrl: '/app/configuration/role-manage/role-manage.html',
            replace: true,
            controller: 'RoleManageCtrl'
        }
    }
]);
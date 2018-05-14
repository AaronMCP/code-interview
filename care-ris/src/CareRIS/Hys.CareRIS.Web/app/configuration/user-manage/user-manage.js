configurationModule.directive('userManage', ['$log', '$document',
    function ($log, $document) {
        'use strict';
        $log.debug('userManage.ctor()...');

        return {
            restrict: 'E',
            templateUrl: '/app/configuration/user-manage/user-manage.html',
            replace: true,
            controller: 'UserManageCtrl',
            link: function (scope, element) {
                
            }
        }
    }
]);
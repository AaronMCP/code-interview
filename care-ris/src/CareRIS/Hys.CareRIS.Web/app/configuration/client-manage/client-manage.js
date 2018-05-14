configurationModule.directive('clientManage', ['$log', '$document',
    function ($log, $document) {
        'use strict';
        $log.debug('clientManage.ctor()...');

        return {
            restrict: 'E',
            templateUrl: '/app/configuration/client-manage/client-manage.html',
            replace: true,
            controller: 'ClientManageCtrl'
        }
    }
]);
templateModule.directive('template', ['$log',
    function ($log) {
        'use strict';
        return {
            restrict: 'E',
            replace: true,
            templateUrl: '/app/template/views/template.html',
            controller: 'TemplateCtrl'
        }
    }])
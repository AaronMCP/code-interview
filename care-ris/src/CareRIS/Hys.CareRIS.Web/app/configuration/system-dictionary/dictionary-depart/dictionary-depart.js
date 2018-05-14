configurationModule.directive('dictionaryDepart', ['$log', '$document',
    function ($log, $document) {
        'use strict';
        return {
            restrict: 'E',
            templateUrl: '/app/configuration/system-dictionary/dictionary-depart/dictionary-depart.html',
            replace: true,
            controller: 'DictionaryeDepartCtrl'
        }
    }
]);
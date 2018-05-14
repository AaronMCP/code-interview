configurationModule.directive('dictionaryRelation', ['$log', '$document',
    function ($log, $document) {
        'use strict';
        return {
            restrict: 'E',
            templateUrl: '/app/configuration/system-dictionary/dictionary-relation/dictionary-relation.html',
            replace: true,
            controller: 'DictionaryeRelationCtrl'
        }
    }
]);
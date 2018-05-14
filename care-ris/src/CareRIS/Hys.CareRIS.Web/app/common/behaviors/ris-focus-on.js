/*
focus on element when event is triggered.

usage:
<textarea id="newNoteTextArea" csd-focus-on="eventName"></textarea>
*/

commonModule.directive('risFocusOn', ['$timeout', function ($timeout) {
    'use strict';

    return function (scope, elem, attr) {
        scope.$on(attr.risFocusOn, function () {
            $timeout(function () {
                elem[0].focus();
                //  }, 500);
            }, 0);
            // alsace =>update  500 to 0
        });
    };
}]);
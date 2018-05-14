// appNavBar
// search control for worklist
frameworkModule.directive('appNavBar', ['$log', '$document',
    function ($log, $document) {
        'use strict';

        $log.debug('appNavBar.ctor()...');

        return {
            restrict: 'E',
            templateUrl: '/app/framework/app-nav-bar/app-nav-bar.html',
            replace: true,
            controller: 'AppNavBar',
            link: function (scope, element) {
                var appSwitcherButton = element.find(".glyphicon-th-large");
                var appSwitcher = element.find(".rispro-app-switcher");

                var docClickHandler = function (e) {
                    var eventOutsideTarget = (element[0] !== e.target) && (0 === element.find(e.target).length);
                    if (eventOutsideTarget) {
                        appSwitcher.is(":visible") && appSwitcher.fadeOut();
                    }
                }

                appSwitcherButton.on("click", function () {
                    appSwitcher.is(":visible") ? appSwitcher.fadeOut() : appSwitcher.fadeIn();
                });

                appSwitcher.on("click", ".rispro-app-module", function () {
                    appSwitcher.fadeOut();
                });

                $document.on("click", docClickHandler);

                scope.$on("$destroy", function () {
                    $document.off("click", docClickHandler);
                });
            }
        };
    }
]);
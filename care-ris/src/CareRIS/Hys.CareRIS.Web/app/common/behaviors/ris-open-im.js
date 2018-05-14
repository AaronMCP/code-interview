commonModule.directive("risOpenIm", ['imManager', 'application', function (imManager, application) {
    'use strict';
    return {
        restrict: "A",
        link: function (scope, element) {
            element.on("click", function () {
                if (application.configuration.enableIM) {
                    imManager.openIm();
                    }
            });
        }
    };
}
]);
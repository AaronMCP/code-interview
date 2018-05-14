qualitycontrolModule.directive('editPatientInfo', ['$log',
    function ($log) {
        'use strict';

        return {
            restrict: 'E',
            templateUrl: '/app/quality-control/qc-edit/qc-edit-patientInfo/qc-edit-patientInfo.html',
            replace: true,
            scope: {
            },
            controller: 'QcEditPatientInfoController',
            link: function (scope, element) {
                element.find("#birthdayPicker").on("keydown", function () { return false; }).on("click", function () {
                    if (!scope.birthdayPicker.options.opened)                        scope.birthdayPicker.open();
                });
            }
        }
    }
]);
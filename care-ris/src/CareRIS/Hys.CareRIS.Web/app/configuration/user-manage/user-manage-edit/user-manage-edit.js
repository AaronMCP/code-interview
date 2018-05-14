configurationModule.directive('userManageEdit', ['$document', '$timeout',
    function ($document, $timeout) {
        'use strict';

        return {
            restrict: 'E',
            templateUrl: '/app/configuration/user-manage/user-manage-edit/user-manage-edit.html',
            replace: true,
            controller: 'UserManageEditController',
            scope: {
                userCancel: '&',
                userOk: '&',
                modalOption: '=',
                isNew: '=',
                changeUser: '=',
                departs: '=',
                accessIndex: '=',
                belongIndex: '=',
                titleList: '='
            },
            link: function (scope,element) {
                element.find("#patientTimeEnd")
                    .on("keydown", function (e) {
                        return false;
                    })
                    .on("click", function () {
                        scope.patientTimeEnd.open();
                    });
                element.find("#patientTimeStart")
                    .on("keydown", function (e) {
                        return false;
                    })
                    .on("click", function () {
                        scope.patientTimeStart.open();
                    });
            }
        }
    }
]);
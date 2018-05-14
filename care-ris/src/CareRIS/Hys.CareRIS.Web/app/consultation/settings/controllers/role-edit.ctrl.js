consultationModule.controller('RoleEditController', ['$log', '$scope', '$modalInstance', '$translate', 'consultationService', 'enums', 'role',
    function ($log, $scope, $modalInstance, $translate, consultationService, enums, role) {
        'use strict';
        $log.debug('RoleEditController.ctor()...');

        var permissionSeparator = ',';

        $scope._ = _;
        $scope.isEdit = !!role;
        $scope.permissions = enums.Permissions;
        //var allPermissions = _.chain(enums.Permissions).values().map(_.values).flatten().map(String).value();
        $scope.role = role || { uniqueID: RIS.newGuid(), status: true, permissionList: [] };
        if ($scope.role.permissions) {
            $scope.role.permissionList = $scope.role.permissions.split(permissionSeparator);
        }
        var originalStatus = $scope.role.status;

        $scope.saveRole = function () {
            if ($scope.isEdit && originalStatus && !$scope.role.status) {
                // change role to invalidate
                consultationService.getRoleRelatedUser($scope.role.uniqueID).success(function (relatedUserNames) {
                    // notify related user permission will be removed
                    if (relatedUserNames ){//&& !window.confirm($translate.instant('ConfrimInactiveRole').format(relatedUserNames))) {
                        alert($translate.instant('ConfrimInactiveRole').format(relatedUserNames));
                        return;// cancel save.
                    }
                    saveRole();
                });
            } else {
                saveRole();
            }
        }

        $scope.validateRoleName = function (roleName) {
            return consultationService.validateRoleName($scope.role.uniqueID, roleName);
        }

        function saveRole() {
            $scope.role.lastEditTime = new Date();
            $scope.role.permissions = $scope.role.permissionList.join(permissionSeparator);
            consultationService.saveRole($scope.role).success(function (data) { $modalInstance.close(data); });
        }

        $scope.close = function () {
            $modalInstance.close();
        };
    }
]);
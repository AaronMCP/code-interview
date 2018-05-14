consultationModule.controller('UserEditController', ['$log', '$scope', '$translate', '$modalInstance', '$filter', 'consultationService', 'constants', 'enums', 'user', 'userRelatedData', 'loginUser', 'openDialog',
    function ($log, $scope, $translate, $modalInstance, $filter, consultationService, constants, enums, user, userRelatedData, loginUser, openDialog) {
        'use strict';
        $log.debug('UserEditController.ctor()...');

        if (userRelatedData) {
            if (!loginUser.isSuperAdmin) {
                userRelatedData.roles = $scope.roles = _.without(userRelatedData.roles, _.find(userRelatedData.roles, function (role) { return role.uniqueID == constants.adminRoleID; }));
                userRelatedData.hospitals = $scope.hospitals = _.filter(userRelatedData.hospitals, function (hospital) { return hospital.uniqueID == loginUser.user.hospitalID });
            } else {
                $scope.roles = userRelatedData.roles;
                $scope.hospitals = userRelatedData.hospitals;
            }
            //$scope.departments = userRelatedData.departments;
            consultationService.getDepartments().success(function (data) {
                $scope.departments = data;
            });
        }

        $scope.loginUser = loginUser;
        $scope.isEdit = !!user;
        $scope.user = user || { userID: RIS.newGuid() };
        $scope.maxDate = new Date();

        $scope.roleChanged = function () {
            if ($scope.user && $scope.user.roles) {
                $scope.user.isExpert = _.some($scope.user.roles, function (role) { return role.uniqueID == constants.expertRoleId; });

                if ($scope.user.isExpert) {
                    // expert require ConsultationCenter
                    if ($scope.user.hospitalID) {
                        var hospital = _.find($scope.hospitals, function (h) { return h.uniqueID == user.hospitalID; });
                        if (hospital && !hospital.isConsultation) {
                            $scope.user.hospitalID = undefined;
                        }
                    }
                    $scope.hospitals = _.where($scope.hospitals, { isConsultation: true });
                } else {
                    $scope.hospitals = userRelatedData.hospitals;
                }

                //set default Role
                var hasDefaultRole = _.some($scope.user.roles, function (role) {
                    return role.uniqueID == $scope.user.defaultRoleID;
                });

                if (!hasDefaultRole) {
                    $scope.user.defaultRoleID = $scope.user.roles.length > 0 ? $scope.user.roles[0].uniqueID : '';
                }
            }
        }
        var existingRoles = $scope.user.roles;
        $scope.user.roles = [];
        if (existingRoles) {
            $scope.user.roles = _.filter($scope.roles, function (item) { return _.findWhere(existingRoles, { 'uniqueID': item.uniqueID }); });
            $scope.roleChanged();
        }

        $scope.saveUser = function () {
            $scope.user.lastEditTime = new Date();
            if (!$scope.user.roles || $scope.user.roles.length === 0) {
                openDialog.openIconDialogYesNo(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant('NoRoles'), function () { }, function () {
                    consultationService.saveUser($scope.user).success(function (data) { $modalInstance.close(data); });
                });
            } else {
                consultationService.saveUser($scope.user).success(function (data) { $modalInstance.close(data); });
            }
        }

        $scope.close = function () {
            $modalInstance.close();
        };
    }
]);
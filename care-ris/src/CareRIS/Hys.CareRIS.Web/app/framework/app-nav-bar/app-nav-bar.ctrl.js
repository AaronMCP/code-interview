frameworkModule.controller('AppNavBar', [
    '$rootScope', '$scope', '$log', '$state', 'consultationService', 'configurationService', 'loginUser', 'enums',
    '$window', 'clientAgentService', 'imManager', 'constants', 'loginContext', 'commonMessageHub', 'openDialog',
    '$translate', 'application', 'risChangePassword',
    function ($rootScope, $scope, $log, $state, consultationService, configurationService, loginUser, enums,
        $window, clientAgentService, imManager, constants, loginContext, commonMessageHub, openDialog,
        $translate, application, risChangePassword) {
        'use strict';
        $log.debug('AppNavBar.ctor()...');
        var consultationRolePic = function (roleId) {
            var iconSpan = $('#icon-menu-role-consultation');

            iconSpan.removeClass();
            if (roleId === constants.adminRoleID || roleId === constants.siteAdminRoleID) {
                iconSpan.addClass('icon-general icon-role_admin ng-scope');
            } else if (roleId === constants.expertRoleId) {
                iconSpan.addClass('icon-general icon-role_expert ng-scope');
            } else if (roleId === constants.consAdminRoleId) {
                iconSpan.addClass('icon-general icon-hospital ng-scope');
            } else if (roleId === constants.doctorRoleId) {
                iconSpan.addClass('icon-general icon-doctor ng-scope');
            }
        };

        function switchApp(status, highlightStatus) {
            if ($scope.loginUser.user.uniqueID) {
                consultationService.updateUser(loginUser.user.uniqueID, { lastStatus: status }).success(function () {
                    if (highlightStatus || highlightStatus === 0) {
                        $state.go(status, { highlightStatus: highlightStatus, searchCriteria: { includeDeleted: true } });
                    } else {
                        $state.go(status);
                    }
                });
            } else {
                if (highlightStatus || highlightStatus === 0) {
                    $state.go(status, { highlightStatus: highlightStatus, searchCriteria: { includeDeleted: true } });
                } else {
                    $state.go(status);
                }
            }
        };

        $scope.gotoRisConfig = function () {
            $state.go('ris.worklist.config');
        };

        $scope.switchToRisApp = function () {
            switchApp('ris.worklist.registrations');
        };

        $scope.gotoConsultationConfig = function () {
            $state.go('ris.consultation.roles');
        };

        $scope.switchToConsultationApp = function () {
            if (loginUser.canAccessConslution) {
                var highlightStatus = {};
                highlightStatus.requests = enums.consultationRequestStatus.All;

                switchApp('ris.consultation.requests', highlightStatus);

            } else {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant('CannotAccessConsultation'));
            }
        };

        $scope.switchToCases = function () {
            if (loginUser.canAccessConslution) {
                $state.go('ris.consultation.cases', {
                    highlightStatus: {
                        cases: enums.patientCaseStatus.All
                    }
                });
            } else {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant('CannotAccessConsultation'));
            }
        };

        $scope.switchToReferralApp = function () {
            $state.go('ris.referral.referrals');
        };

        $scope.setDefaultRole = function (role) {
            if (role && (role.uniqueID !== loginUser.user.defaultRoleID)) {
                consultationService.updateUser(loginUser.user.uniqueID, { DefaultRoleID: role.uniqueID }).success(function () {
                    loginUser.user.defaultRoleID = role.uniqueID;

                    consultationRolePic(role.uniqueID);

                    var defaultRole = _.findWhere(loginUser.user.roles, { uniqueID: $scope.loginUser.user.defaultRoleID });
                    if (defaultRole) {
                        loginUser.user.defaultRoleName = defaultRole.roleName;
                    }

                    loginUser.initPermissions();
                    $state.go('ris.consultation.requests', {
                        timestamp: Date.now(),
                        highlightStatus: { requests: enums.consultationRequestStatus.None },
                        reload: true
                    });
                });
            }
        };
        $scope.logout = function () {
            if (!$rootScope.browser.versions.mobile) {
                imManager.logoutIm();
            }
            $window.location.replace("/Account/LogOut");
        };
        $scope.changePassword = function () {
            risChangePassword.open();
        }
        $scope.setRisDefaultRole = function (role) {
            if (role && (role.roleName !== loginContext.roleName)) {
                configurationService.updateRisUserDefaultRole(loginUser.user.uniqueID, loginUser.user.domain, { roleName: role.roleName }).success(function () {
                    loginUser.user.risRole = role.roleName;
                    loginContext.roleName = role.roleName;

                    loginUser.initPermissions();

                    var risRole = _.findWhere($scope.risUserRoles, { roleName: loginUser.user.risRole });
                    if (risRole) {
                        $scope.loginUser.user.defaultRisRoleName = risRole.description;
                    }
                    if (!$state.includes('ris.worklist.registrations')) {
                        $state.go('ris.worklist.registrations');
                    }
                });
            }
        };
        $scope.statisticsUri = '';
        (function initialize() {
            $scope.loginUser = loginUser;
            $scope.enums = enums;
            $scope.constants = constants;
            $scope.loginUser.user.defaultRoleName = '';
            $scope.loginUser.user.defaultRisRoleName = '';

            var defaultRole = _.findWhere($scope.loginUser.user.roles, { uniqueID: $scope.loginUser.user.defaultRoleID });
            if (defaultRole) {
                $scope.loginUser.user.defaultRoleName = defaultRole.roleName;
            }
            consultationRolePic($scope.loginUser.user.defaultRoleID);

            configurationService.getRisUserRoles(loginUser.user.uniqueID).success(function (data) {
                $scope.risUserRoles = data;

                var risRole = _.findWhere($scope.risUserRoles, { roleName: $scope.loginUser.user.risRole });
                if (risRole) {
                    $scope.loginUser.user.defaultRisRoleName = risRole.description;
                }
            });
        }());
    }
]);
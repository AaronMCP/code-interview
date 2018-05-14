commonModule
    .service('risChangePassword', ['$rootScope',
        function ($rootScope) {
            this.open = function () {
                $rootScope.$broadcast('open-change-password');
            }
            this.close = function () {
                $rootScope.$broadcast('close-change-password');
            }
        }])
    .directive('risChangePasswordWindow', ['$compile', '$sce', '$timeout',
        function ($compile, $sce, $timeout) {
            return {
                replace: true,
                restrict: 'EA',
                scope: true,
                link: function (scope, element, attrs) {

                },
                controller: ['$scope', 'risChangePassword', '$http', 'csdToaster', 'openDialog', '$translate', 'loginContext', 'configurationService',
                    function ($scope, risChangePassword, $http, csdToaster, openDialog, $translate, loginContext, configurationService) {
                        $scope.alert = function (info) {
                            openDialog.openIconDialog(
                                openDialog.NotifyMessageType.Warn,
                                $translate.instant('Alert'),
                                info);
                        }
                        $scope.$on('open-change-password', function () {
                            $scope.risChangePasswordWindow.open();
                        });
                        $scope.$on('close-change-password', function () {
                            $scope.newPassword = '';
                            $scope.newErrorInfo = ''
                            $scope.oldPassword = '';
                            $scope.oldErrorInfo = ''
                            $scope.confirmPassword = '';
                            $scope.confirmErrorInfo = ''
                            $scope.risChangePasswordWindow.close();
                        });
                        $scope.getOldErrorInfo = function () {
                            if (!$scope.oldPassword) {
                                $scope.oldErrorInfo = '原密码不能为空！';
                            } else {
                                $scope.oldErrorInfo = '';
                            }
                        }
                        $scope.getNewErrorInfo = function () {
                            if (!$scope.newPassword) {
                                $scope.newErrorInfo = '新密码不能为空！';
                            } else {
                                $scope.newErrorInfo = '';
                            }
                        }
                        $scope.getConfirmErrorInfo = function () {
                            if (!$scope.confirmPassword) {
                                $scope.confirmErrorInfo = '确认密码不能为空！';

                            } else {
                                if ($scope.confirmPassword !== $scope.newPassword) {
                                    $scope.confirmErrorInfo = '两次密码输入不一致！';
                                } else {
                                    $scope.confirmErrorInfo = '';
                                }
                            }
                        }
                        $scope.okChange = function () {
                            if ($scope.changePasswordForm.newPassword.$dirty && !$scope.newErrorInfo && $scope.changePasswordForm.confirmPassword.$dirty && !$scope.confirmErrorInfo && $scope.oldPassword) {
                                var params = {
                                    userID: loginContext.userId,
                                    oldPassword: $scope.oldPassword,
                                    newPassword: $scope.newPassword
                                }
                                configurationService.updatePassword(params).success(function (res) {
                                    if (res === -2) {
                                        csdToaster.info('用户不存在！');
                                    } else if (res === -1) {
                                        csdToaster.info('新密码与原密码一样，无须修改！');
                                    } else if (res === 0) {
                                        csdToaster.info('原密码不正确，请重新输入！');
                                    } else {
                                        risChangePassword.close();
                                        csdToaster.info('密码修改成功！');
                                    }
                                    
                                });
                            } else {
                                return false;
                            }
                        }
                        $scope.cancelChange = function () {
                            risChangePassword.close();
                        }
                    }
                ],
                templateUrl: '/app/common/controls/ris-change-password/ris-change-password.html'
            }
        }
    ])
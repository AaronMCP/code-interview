configurationModule.controller('UserManageEditController',
    ['$rootScope', '$scope', 'configurationService', 'openDialog', '$translate', 'csdToaster', 'dictionaryManager', 'enums', 'loginContext',
    function ($rootScope, $scope, configurationService, openDialog, $translate, csdToaster, dictionaryManager, enums, loginContext) {
        'use strict';

        //医院
        $scope.hospitalList = [];
        $scope.hospitalList.push(loginContext.domain);
        //站点
        configurationService.getAllSite().success(function (result) {
            var sites = _.map(result, function (item) {
                return item.siteName;
            });
            sites.sort(function (a, b) { return a > b });
            $scope.siteList = sites;
        });

        //职称
        $scope.getTitle = function () {
            dictionaryManager.getDictionaries(enums.dictionaryTag.title).then(function (data) {
                if (data) {
                    $scope.titleList = data;
                    $scope.titleList.sort(function (a, b) {
                        return (a.orderID > b.orderID ? 1 : -1);
                    });
                }
            });
        }
        //下拉框搜索无数据
        $scope.noDataFound = function (e) {
            if (!e.sender.dataSource.view()[0]) {
                e.sender.ul.parent().parent().find('.k-nodata > div').html('无数据');
            }
        }
        // 提醒框
        $scope.alert = function (info) {
            openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), info);
        }
        $scope.cliclUserOk = function () {
            //修改密码
            if ($scope.modalOption.confirmPassword !== $scope.modalOption.newPassword) {
                $scope.alert('两次输入密码不一致！');
                return false;
            }
            $scope.changeUser.startDate = new Date($scope.modalOption.startDate);
            $scope.changeUser.endDate = new Date($scope.modalOption.endDate + ' 23:59:59');
            if ($scope.changeUser.endDate < $scope.changeUser.startDate) {
                $scope.alert('起始日不能大于截止日！');
                return false;
            }
            if (!$scope.changeUser.rolesName || $scope.changeUser.rolesName.length < 1) {
                $scope.alert('角色不能为空！');
                return false;
            }
            //修改
            if (!$scope.isNew) {
                //验证本地显示名
                configurationService.displayNameExist($scope.changeUser).success(function (isDisplayNameExist) {
                    if (isDisplayNameExist) {
                        if ($scope.modalOption.newPassword !== '') {
                            $scope.changeUser.password = $scope.modalOption.newPassword;
                        }
                        for (var i = 0; i < $scope.changeUser.userProfileList.length; i++) {
                            var value = $scope.changeUser.userProfileList[i].Value;
                            if (Object.prototype.toString.call(value) == '[object Array]') {
                                $scope.changeUser.userProfileList[i].Value = value.join('|');
                            }
                        }
                        configurationService.updateUser($scope.changeUser).success(function (res) {
                            if (res) {
                                csdToaster.info('修改成功！');
                                $scope.userOk();
                            } else {
                                csdToaster.info('修改失败！');
                            }
                        })
                    } else {
                        $scope.alert('用户名已存在！');
                    }
                })
            } else {
                //验证登录名
                configurationService.loginNameExist($scope.changeUser.loginName).success(function (isLoginNameExist) {
                    if (isLoginNameExist) {
                        //验证本地显示名
                        configurationService.displayNameExist($scope.changeUser).success(function (isDisplayNameExist) {
                            if (isDisplayNameExist) {
                                $scope.changeUser.password = $scope.modalOption.newPassword;
                                if($scope.changeUser.accessSites) {
                                    $scope.changeUser.accessSites = $scope.changeUser.accessSites.join('|');
                                }
                                configurationService.saveUser($scope.changeUser).success(function (res) {
                                    if (res) {
                                        csdToaster.info('添加成功！');
                                        $scope.userOk();                                  
                                    } else {
                                        csdToaster.info('添加失败！');
                                    }
                                    
                                })
                            } else {
                                $scope.alert('用户名已存在！');
                            }
                        })
                    } else {
                        $scope.alert('登入名称已存在！');
                    }

                })
            }
        }

        $scope.clickUserCancel = function () {
            $scope.confirmPassword = '';
            $scope.newPassword = '';
            $scope.userCancel();
        }

        //选中一行
        $scope.userProfileClick = function (userProfile) {
            if (userProfile.selected) {
                if ($scope.selectedUserProfile.PropertyType === '11') {
                    if (userProfile.Value === '0') {
                        userProfile.Value = '1';
                    } else {
                        userProfile.Value = '0';
                    }
                }
                userProfile.isChange = true;
            }
            if ($scope.selectedUserProfile) {
                $scope.selectedUserProfile.selected = false;
            }
            userProfile.selected = true;
            $scope.selectedUserProfile = userProfile;
        }

        ; +(function init() {
            $scope.rolesList = [];
            $scope.getTitle();
            //角色
            $scope.roleEditSelectOptions = {
                placeholder: "",
                dataTextField: "description",
                dataValueField: "roleName",
                valuePrimitive: true,
                autoBind: false,
                dataSource: {
                    type: "json",
                    serverFiltering: true,
                    transport: {
                        read: function (options) {
                            configurationService.getRoles().success(function (result) {
                                options.success(result);
                            })
                        }
                    }
                }
            };
            $scope.userProfileChange = function (userProfile) {
                userProfile.selectValue = userProfile.Value.join('|');
                userProfile.isChange = true;
            };
        })()
    }])
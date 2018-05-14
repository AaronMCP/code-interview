configurationModule.controller('RoleManageCtrl', ['$scope', '$state', 'constants', 'configurationService', 'openDialog', '$translate', 'csdToaster', 'loginContext',
    function ($scope, $state, constants, configurationService, openDialog, $translate, csdToaster, loginContext) {
        'use strict';
        //医院
        $scope.hospitalList = [];
        $scope.hospitalList.push(loginContext.domain);
        $scope.domain = loginContext.domain;
        //提醒框
        $scope.alert = function (info) {
            openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), info);
        }

        $scope.roleTreeClick = function (dataItem) {
            $scope.initData();
            if(dataItem.leaf!==0 || dataItem.parentID!==null) {
                if(dataItem.leaf) {
                    for (var i = 0; i < $scope.systemNode.length; i++) {
                        if ($scope.systemNode[i].uniqueID === dataItem.parentID) {
                            $scope.selectedParent = $scope.systemNode[i];
                            break;
                        }
                    }
                    $scope.selectedRole = dataItem;
                    var params = {
                        roleName: dataItem.name,
                        domain: dataItem.domain
                    }
                    configurationService.getRoleProfiles(params).success(function (res) {
                        var list = JSON.parse(res).RoleProfiles;
                        $scope.roleProfiles = list;
                        for (var i = 0; i < list.length; i++) {   
                            // type = 1  数字框
                            if (list[i].PropertyType === '1') {
                                var str = list[i].PropertyOptions.slice(1, -1);
                                var arr = str.split('-');
                                list[i].maxNumber = arr[1];
                                list[i].minNumber = arr[0];
                                list[i].Value = Number(list[i].Value);
                            }

                            // type = 3,4 下拉框
                            if (list[i].PropertyType === '3' || list[i].PropertyType === '4') {
                                list[i].selectOptions = list[i].PropertyOptions.split('|');
                            }

                            //展示的value和多选框需要的options  type=5  下拉多选框 
                            if (list[i].PropertyType === '5') {
                                list[i].selectValue = list[i].Value.split('|');
                                if (angular.isString(list[i].PropertyOptions)) {
                                    list[i].selectOptions = list[i].PropertyOptions.split('|');
                                } else {
                                    list[i].selectOptions = [];
                                    for (var j = 0; j < list[i].PropertyOptions.length; j++) {
                                        list[i].selectOptions.push(list[i].PropertyOptions[j].value);
                                    }
                                }
                            }

                            //type = 6 颜色选择器
                            if (list[i].PropertyType === '10') {
                                console.log('color');
                            }
                            var moduleId = list[i].ModuleId;
                            switch (moduleId) {
                                case '0000':
                                    $scope.roleProfileListGlobal.push(list[i]);
                                    break;
                                case '0100':
                                    $scope.roleProfileListAuthorization.push(list[i]);
                                    break;
                                case '0300':
                                    $scope.roleProfileListRegistration.push(list[i]);
                                    break;
                                case '0400':
                                    $scope.roleProfileListReport.push(list[i]);
                                    break;
                                case '0500':
                                    $scope.roleProfileListTeaching.push(list[i]);
                                    break;
                                case '0600':
                                    $scope.roleProfileListExamination.push(list[i]);
                                    break;
                                case '0700':
                                    $scope.roleProfileListStatistic.push(list[i]);
                                    break;
                                case '0D00':
                                    $scope.roleProfileListQualityControl.push(list[i]);
                                    break;
                            }
                        }
                    })
                } else {
                    $scope.selectedParent = dataItem;
                    $scope.selectedRole = null;
                }
            } else {
                $scope.selectedParent = null;
            }
        }

        //处理角色tree
        $scope.dataHandle = function (data) {
            var arr = [];
            //第一层roleManagement
            for (var i = 0; i < data.length; i++) {
                data[i].text = data[i].name;
                if (data[i].leaf === 0 && data[i].parentID === null) {
                    arr.push(data[i]);
                }
            }
            //第二层Global和site
            for (var l = 0; l < arr.length; l++) {
                arr[l].items = [];
                for (var m = 0 ; m < data.length; m++) {
                    if (data[m].leaf === 0 && data[m].parentID !== null) {
                        arr[l].items.push(data[m]);
                    }
                }
                $scope.systemNode = arr[l].items;
            }
            // 第三层 role
            for (var i = 0; i < arr.length; i++) {
                for (var j = 0; j < arr[i].items.length; j++) {
                    arr[i].items[j].items = [];
                    for (var k = 0; k < data.length; k++) {
                        if (data[k].parentID === arr[i].items[j].uniqueID) {
                            arr[i].items[j].items.push(data[k]);
                        }
                    }
                }
            }
            return arr;
        }

        //刷新
        $scope.refreshRole = function () {
            $scope.getRoleProfiles();
            $scope.selectedRole = null;
        }

        //新增角色
        $scope.addRole = function () {
            $scope.addRoleWindow.open();
            $scope.isCopy = false;
            
        }

        //拷贝角色
        $scope.copyRole = function () {
            $scope.addRoleWindow.open();
            $scope.isCopy = true;
        }

        //删除角色
        $scope.deleteRole = function (selectedRole) {
            openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '是否删除该角色信息', function () {
                $scope.selectedRole.roleName = $scope.selectedRole.name;
                $scope.selectedRole.uniqueID = $scope.selectedRole.roleID;
                configurationService.deleteRole($scope.selectedRole).success(function (res) {
                    if (res) {
                        var array = selectedRole.parent();
                        var index = array.indexOf(selectedRole);
                        array.splice(index, 1);
                        $scope.selectedRole = null;
                        $scope.roleProfiles = [];
                        $scope.initData();
                        csdToaster.info('删除成功！');
                    } else {
                        csdToaster.info('删除失败！');
                    }
                });
            })      
        }

        //弹出框确定
        $scope.addRoleSure = function () {
            //新增
            if (!$scope.isCopy) {
                var params = {
                    roleName: $scope.roleName,
                    description: $scope.roleDescription,
                    isCopy: $scope.isCopy,
                    domain: $scope.selectedParent.domain,
                    parentID: $scope.selectedParent.uniqueID
                }
                if ($scope.selectedParent.name !== 'GlobalRole') {
                    params.site = '';
                } else {
                    params.site = $scope.selectedParent.name;
                }
                configurationService.saveRole(params).success(function (res) {
                    if (res) {
                        $scope.addRoleWindow.close();
                        $scope.getRoleProfiles();
                        csdToaster.info('新增成功！');
                    } else {
                        csdToaster.info('新增失败！');
                    }                 
                });

            } else {
                var params = {
                    roleName: $scope.selectedRole.name,
                    copyRoleName: $scope.roleName,
                    description: $scope.roleDescription,
                    isCopy: $scope.isCopy,
                    domain: $scope.selectedParent.domain,
                    parentID: $scope.selectedParent.uniqueID
                }
                if ($scope.selectedParent.name !== 'GlobalRole') {
                    params.site = '';
                } else {
                    params.site = $scope.selectedParent.name;
                }
                configurationService.saveRole(params).success(function (res) {
                    if (res) {
                        $scope.addRoleWindow.close();
                        $scope.getRoleProfiles();
                        csdToaster.info('拷贝成功！');
                    } else {
                        csdToaster.info('拷贝失败！');
                    }
                });
            }
            
        }

        //新增取消
        $scope.addRoleCancel = function () {
            $scope.addRoleWindow.close();
            $scope.roleName = '',
            $scope.roleDescription = '';
        }

        //保存更新角色配置信息
        $scope.updateRole = function () {
            for (var i = 0; i < $scope.roleProfiles.length; i++) {
                $scope.roleProfiles.Value = $scope.roleProfiles.selectValue;
            }
            var params = {
                domain: $scope.selectedRole.domain,
                roleProfileList: $scope.roleProfiles
            }
            configurationService.updateRole(params).success(function (res) {
                if (res) {
                    csdToaster.info('修改成功！');
                } else {
                    csdToaster.info('修改失败！');
                }
            });
        }

        // 获取角色信息
        $scope.getRoleProfiles = function () {
            configurationService.getAllRoleNodes().success(function (res) {
                res = $scope.dataHandle(res);
                $scope.res = res;    
                $scope.roleTree.setDataSource(new kendo.data.HierarchicalDataSource({
                    data: $scope.res
                }));
            });
        }
        
        //初始化配置数据
        $scope.initData = function () {
            $scope.roleProfileListGlobal = [];
            $scope.roleProfileListAuthorization = [];
            $scope.roleProfileListRegistration = [];
            $scope.roleProfileListReport = [];
            $scope.roleProfileListTeaching = [];
            $scope.roleProfileListExamination = [];
            $scope.roleProfileListStatistic = [];
            $scope.roleProfileListQualityControl = [];
        }
        ; +(function init() {
            $scope.oldTreeData = [];
            $scope.newTreeData = [];
            $scope.selectedParent = null;
            $scope.isCopy = false;
            $scope.systemNode = [];
            $scope.roleProfiles = [];
            $scope.initData();
            $scope.getRoleProfiles();
        })()
    }])
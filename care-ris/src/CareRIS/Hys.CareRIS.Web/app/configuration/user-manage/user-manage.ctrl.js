configurationModule.controller('UserManageCtrl', [
    '$scope', '$state', 'constants', 'configurationService', '$timeout', 'openDialog', '$translate', 'csdToaster', 'loginContext', '$filter', 'dictionaryManager', 'enums',
    function ($scope, $state, constants, configurationService, $timeout, openDialog, $translate, csdToaster, loginContext, $filter, dictionaryManager, enums) {
        'use strict';
        //医院
        $scope.hospitalList = [];
        $scope.hospitalList.push(loginContext.domain);
        $scope.searchOption = {
            domain: loginContext.domain,
            department: '',
            loginName: '',
            localName: ''
        };
  
        //职称
        $scope.getTitles = function () {
            dictionaryManager.getDictionaries(enums.dictionaryTag.title).then(function (data) {
                if (data) {
                    $scope.titleList = data;
                    $scope.titleList.sort(function (a, b) {
                        return (a.orderID > b.orderID ? 1 : -1);
                    });
                }
            });
        }

        //部门
        $scope.getDeparts = function () {
            configurationService.getDepart().success(function (result) {
                $scope.departs = result;
            })
        }

        //获取角色
        $scope.getRoles = function () {
            configurationService.getRoles().success(function (result) {
                $scope.allRoles = result;
            })
        }

        //获取
        $scope.getSites = function () {
            configurationService.getAllSite().success(function (result) {
                result.unshift('');
                $scope.sites = result;
            })
        }

        //刷新
        $scope.refreshUser = function () {
            $scope.getTitles();
            $scope.getRoles();
            $scope.getDeparts();
            $scope.getSites();
            $scope.userManageGrid.dataSource.read();
            $scope.userManageGrid.refresh();
        }
        //搜索
        $scope.searchUser = function () {
            $scope.searchFlag = true;
            $scope.searchOptionMirror = angular.copy($scope.searchOption);
            $scope.userManageGrid.dataSource.page(1);
        }

        // 修改
        $scope.modifyUser = function () {
            $scope.isNew = false;
            $scope.modalOption = {
                passwordText: '新密码',
                confirmText: '确认密码',
                newPassword: '',
                confirmPassword: ''
            }
            $scope.changeUser = angular.copy($scope.selectedUser);
            if ($scope.changeUser.isSetExpireDate) {
                $scope.modalOption.startDate = $scope.dateFormat($scope.changeUser.startDate);
                $scope.modalOption.endDate = $scope.dateFormat($scope.changeUser.endDate);
            } else {
                $scope.modalOption.startDate = '';
                $scope.modalOption.endDate = '';
            }                      
            configurationService.getUserProfiles($scope.selectedUser.uniqueID).success(function (res) {
                res = JSON.parse(res);
                res.UserProfile.sort(function (a, b) { return b.Name > a.Name;})
                //默认角色选中
                $scope.changeUser.rolesName = res.UserRole;
                for (var i = 0; i < $scope.changeUser.rolesName.length; i++) {
                    $scope.changeUser.rolesName[i] = $scope.changeUser.rolesName[i].RoleName
                }
                $scope.changeUser.userProfileList = res.UserProfile;
                var list = $scope.changeUser.userProfileList;
                //展示的value和多选框需要的options
                for (var i = 0; i < list.length; i++) {
                    if ($scope.changeUser.userProfileList[i].PropertyType === '5') {
                        $scope.changeUser.userProfileList[i].selectValue = angular.copy(list[i].Value);
                        $scope.changeUser.userProfileList[i].Value = list[i].Value.split('|');
                        if (angular.isString(list[i].PropertyOptions)) {
                            $scope.changeUser.userProfileList[i].selectOptions = list[i].PropertyOptions.split('|');
                        } else {
                            $scope.changeUser.userProfileList[i].selectOptions = [];
                            for (var j = 0; j < list[i].PropertyOptions.length; j++) {
                                $scope.changeUser.userProfileList[i].selectOptions.push(list[i].PropertyOptions[j].value);
                            }

                        }
                    }
                    if ($scope.changeUser.userProfileList[i].Name === 'BelongToSite') {
                        $scope.belongIndex = i;
                    }
                    if ($scope.changeUser.userProfileList[i].Name === 'AccessSite') {
                        $scope.accessIndex = i;
                    }
                }
            }); 
            $('#userManageModal').modal('show');
        }

        $scope.addUser = function () {
            $scope.isNew = true;
            $scope.changeUser = '';
            $scope.modalOption = {
                passwordText: '密码',
                confirmText: '确认密码',
                startDate: '',
                endDate: '',
                newPassword: '',
                confirmPassword: ''
            }
            $('#userManageModal').modal('show');
        }

        $scope.userCancel = function () {
            $('#userManageModal').modal('hide');
        }
        $scope.userOk = function () {
            $('#userManageModal').modal('hide');
            $scope.userManageGrid.dataSource.read();
            $scope.userManageGrid.refresh();
        }

        // 删除
        $scope.deleteUser = function () {
            openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '是否删除该用户', function () {
                configurationService.deleteUser($scope.selectedUser.uniqueID).success(function (res) {
                    if (res) {
                        csdToaster.info('删除成功！');
                        $scope.userManageGrid.dataSource.read();
                        $scope.userManageGrid.refresh();
                    }
                })
            })
            
        }

        // 日期转换
        $scope.dateFormat = function (string) {
            var date = new Date(string);
            var year = date.getFullYear();
            var month = date.getMonth() + 1;
            var day = date.getDate();

            date = year + '/' + month + '/' + day;

            return date;
        }
        ; +(function init() {
            $scope.selectedUser = '';
            $scope.changeUser = '';
            $scope.roles = [];
            $scope.isNew = false;
            $scope.searchFlag = false;
            $scope.filter = null;
            $scope.getTitles();
            $scope.getRoles();
            $scope.getDeparts();
            $scope.getSites();
            
            $scope.roleSelectOptions = {
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
        $scope.userManageGridOption = {
            dataSource: new kendo.data.DataSource({
                schema: {
                    data: 'data',
                    total: 'total',
                    aggregates: "aggregates",
                    model: {
                        fields: {
                            examineTime: { type: 'date' }
                        }
                    }
                },
                transport: {
                    read: function (options) {
                        var flag = false;
                        if ($scope.searchOptionMirror) {
                            flag = true;
                            for (var attr in $scope.searchOption) {
                                if ($scope.searchOption[attr] !== $scope.searchOptionMirror[attr]) {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        if ($scope.searchFlag && flag) {
                            var filter = {
                                logic: 'and',
                                filters: []
                            };
                            for (var attr in $scope.searchOption) {
                                filter.filters.push({ field: attr, operator: 'contains', value: $scope.searchOption[attr] })
                            }
                            $scope.filter = filter;
                        }
                        options.data.filter = $scope.filter;
                        configurationService.getUserList(options.data, $scope.roles).success(function (result) {
                            options.success(result);
                            //默认选中第一行
                            var row = $('.user-manage-content tbody tr:nth-child(1)');
                            $scope.userManageGrid.select(row);
                        })
                    },  
                },
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true,
                pageSize: constants.pageSize,
                sort: [{ field: "loginName", dir: "desc" }],
            }),
            pageable: {
                refresh: true,
                buttonCount: 5,
                input: true
            },
            sortable: {
                allowUnsort: false
            },
            columns: [
                {
                    field: 'loginName', title: "登入名称",
                    attributes: {
                        'title': '{{dataItem.loginName}}',
                        style: "overflow:hidden;white-space: nowrap;text-overflow: ellipsis;"
                    }
                },
                {
                    field: 'domain', title: "医院",
                    attributes: {
                        'title': '{{dataItem.domain}}'
                    }
                },
                { field: 'department', title: "影像科室" },
                {
                    field: 'telephone', title: "电话",
                    attributes: {
                        'title': '{{dataItem.telephone}}',
                        style: "overflow:hidden;white-space: nowrap;text-overflow: ellipsis;"
                    }
                },
                {
                    field: 'mobile', title: "手机",
                    attributes: {
                        'title': '{{dataItem.mobile}}',
                        style: "overflow:hidden;white-space: nowrap;text-overflow: ellipsis;"
                    }
                },
                {
                    field: 'email', title: "电子邮件",
                    attributes: {
                        'title': '{{dataItem.email}}',
                        style: "overflow:hidden;white-space: nowrap;text-overflow: ellipsis;"
                    }
                },
                {
                    field: 'isSetExpireDate', title: "是否设定有效期",
                    template: '{{dataItem.isSetExpireDate===true?"是":"否"}}',
                    attributes: {
                        style: 'text-align: center;'
                    },
                    width:'140px'
                },
                {
                    field: 'startDay', title: "起始日",
                    template: '{{dataItem.isSetExpireDate === true ? dataItem.startDate: ""|date:"yyyy-MM-dd"}}'
                },
                {
                    field: 'endDay', title: "截止日",
                    template: '{{dataItem.isSetExpireDate === true ? dataItem.endDate: ""|date:"yyyy-MM-dd"}}'
                },
                {
                    field: 'localName', title: "本地名称",
                    attributes: {
                        'title': '{{dataItem.localName}}',
                        style: "overflow:hidden;white-space: nowrap;text-overflow: ellipsis;"
                    }
                }
            ],
            selectable: "row",
            change: function (e) {
                var selectedRow = this.dataItem(this.select());
                $timeout(function () {
                    $scope.selectedUser = selectedRow;
                    $scope.changeUser = angular.copy($scope.selectedUser);
                });
            }
        };
    })()
}])
configurationModule.controller('DictionaryeDepartCtrl', ['$scope', '$rootScope', 'configurationService', 'csdToaster', 'openDialog', '$translate', 'application', '$timeout',
    function ($scope, $rootScope, configurationService, csdToaster, openDialog, $translate, application, $timeout) {
        var configurationData = application.configuration;
        //我的结果遍历
        var eachDepartResult = function (data) {
            if (!data) return;
            $scope.dicDepartList = data;
            $scope.applyDepartList = [];
            if (data && data.length > 0) {
                angular.forEach(data, function (item, index) {
                    var dept = {
                        text: item.deptName,
                        value: item.ApplyDeptID
                    };
                    $scope.applyDepartList.push(dept);
                });
            }
        }

        //查询
        var getDepartList = function () {
            $scope.selectedDepart = null;
            $scope.departGridOption.dataSource.read();
        }
        var getDoctorList = function () {
            $scope.selectedDoctor = null;
            $scope.doctorGridOption.dataSource.read();
        }
        //点击修改
        $scope.updateDept = function () {
            if (!$scope.selectedDepart) {
                return;
            }
            $scope.currentDepart = null;
            $scope.currentDepart = angular.copy($scope.selectedDepart);
            $scope.updateDeptWindow.open();
        }
        $scope.updateDoctor = function () {
            if (!$scope.selectedDoctor) {
                return;
            }
            $scope.currentDoctor = null;
            $scope.currentDoctor = angular.copy($scope.selectedDoctor);
            $scope.updateDoctorWindow.open();
        }
        //点击新建
        $scope.addDept = function () {
            $scope.currentDepart = null;
            $scope.updateDeptWindow.open();
        }
        $scope.addDoctor = function () {
            $scope.currentDoctor = null;
            $scope.updateDoctorWindow.open();
        }
        //数据验证
        var deptValidate = function (data) {

            if (!data.deptName) {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant("申请部门不能为空！"), function () {
                });
                return false;
            }
            return true;
        }
        var doctorValidate = function (data) {

            if (!data.doctorName) {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant("申请不医生能为空！"), function () {
                });
                return false;
            }
            return true;
        }
        //编辑确定
        $scope.saveDept = function () {
            if (deptValidate($scope.currentDepart)) {
                if ($scope.currentDepart.uniqueID) {
                    configurationService.updateApplyDept($scope.currentDepart).success(function (result) {
                        if (result > 0) {
                            csdToaster.info('修改成功！');
                            $scope.updateDeptWindow.close();
                            getDepartList();
                        } else if (result == -1) {
                            csdToaster.info('申请部门重复！');
                        } else {
                            csdToaster.info('修改失败！');
                        }
                    });
                } else {
                    configurationService.addApplyDept($scope.currentDepart).success(function (result) {
                        if (result > 0) {
                            csdToaster.info('添加成功！');
                            $scope.updateDeptWindow.close();
                            getDepartList();
                        } else if (result == -1) {
                            csdToaster.info('申请部门重复！');
                        } else {
                            csdToaster.info('添加失败！');
                        }
                    });
                }
            }
        }

        $scope.saveDoctor = function () {
            if (doctorValidate($scope.currentDoctor)) {
                if ($scope.currentDoctor.uniqueID) {
                    configurationService.updateApplyDoctor($scope.currentDoctor).success(function (result) {
                        if (result > 0) {
                            csdToaster.info('修改成功！');
                            $scope.updateDoctorWindow.close();
                            getDoctorList();
                        } else if (result == -1) {
                            csdToaster.info('申请部门重复！');
                        } else {
                            csdToaster.info('修改失败！');
                        }
                    });
                } else {
                    configurationService.addApplyDoctor($scope.currentDoctor).success(function (result) {
                        if (result > 0) {
                            csdToaster.info('添加成功！');
                            $scope.updateDoctorWindow.close();
                            getDoctorList();
                        } else if (result == -1) {
                            csdToaster.info('申请部门重复！');
                        } else {
                            csdToaster.info('添加失败！');
                        }
                    });
                }
            }
        }

        $scope.deptCancle = function () {
            $scope.updateDeptWindow.close();
        }

        $scope.doctorCancle = function () {
            $scope.updateDoctorWindow.close();
        }
        //删除
        $scope.delDept = function () {
            if (!$scope.selectedDepart)
                return
            openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant("确定删除吗？"), function () {
                configurationService.deleteApplyDept($scope.selectedDepart.uniqueID).success(function (result) {
                    if (result) {
                        csdToaster.info('删除成功！');
                        getDepartList();
                    } else {
                        csdToaster.info('删除失败！');
                    }
                });
            });

        }
        $scope.delDoctor = function () {
            if (!$scope.selectedDoctor)
                return
            openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant("确定删除吗？"), function () {
                configurationService.deleteApplyDoctor($scope.selectedDoctor.uniqueID).success(function (result) {
                    if (result) {
                        csdToaster.info('删除成功！');
                        getDoctorList();
                    } else {
                        csdToaster.info('删除失败！');
                    }
                });
            });

        }

        //申请部门表格
        $scope.departGridOption = {
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        configurationService.getAllApplyDepts().success(function (result) {
                            eachDepartResult(result);
                            options.success(result);
                        });
                    }
                },
                sort: [{ field: "deptName", dir: "asc" }]
            }),
            sortable: {
                allowUnsort: false
            },
            resizable: true,
            columns: [
                { field: 'deptName', title: '申请部门' },
                { field: 'telephone', title: '电话' },
                { field: 'shortcutCode', title: '快捷码' },
                { field: 'site', title: '站点' },
            ],
            selectable: 'row',
            change: function (e) {
                var selectedRow = this.select();
                var dataItem = this.dataItem(selectedRow[0]);
                $timeout(function () {
                    $scope.selectedDepart = dataItem;
                });
            }
        }

        //申请医生表格
        $scope.doctorGridOption = {
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        configurationService.getAllApplyDoctors().success(function (result) {
                            options.success(result);
                        });
                    }
                },
                sort: [{ field: "departName", dir: "asc" }],
            }),
            sortable: {
                allowUnsort: false
            },
            reorderable: true,
            resizable: true,
            columns: [
                { field: 'deptName', title: '申请部门' },
                { field: 'doctorName', title: '申请医生' },
                { field: 'gender', title: '性别' },
                { field: 'mobile', title: '手机' },
                { field: 'telephone', title: '电话' },
                { field: 'staffNo', title: '员工号' },
                { field: 'email', title: '邮箱' },
                { field: 'shortcutCode', title: '快捷码' },
                { field: 'site', title: '站点' },
            ],
            selectable: "row",
            change: function (e) {
                var selectedRow = this.select();
                var dataItem = this.dataItem(selectedRow[0]);
                $timeout(function () {
                    $scope.selectedDoctor = dataItem;
                });
            }
        }
        ; +(function init() {
            $scope.dicDepartList = null;
            $scope.dicDoctorList = null;
            $scope.selectedDepart = null;
            $scope.selectedDoctor = null;
            $scope.currentDepart = null;
            $scope.currentDoctor = null;
            $scope.applyDepartList = [];
            $scope.genderList = configurationData.genderList;
            getDepartList();
            getDoctorList();

        })()
    }])
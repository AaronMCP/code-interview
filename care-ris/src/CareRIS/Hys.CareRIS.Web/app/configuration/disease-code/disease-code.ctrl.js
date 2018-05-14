configurationModule.controller('DisCodeCtrl', ['$scope', '$state', '$log', 'configurationService', '$timeout', 'constants', 'csdToaster', 'openDialog', '$translate', '$timeout',
    function ($scope, $state, $log, configurationService, $timeout, constants, csdToaster, openDialog, $translate, $timeout) {
        // 查询条件
        $scope.searchOption = {
            id: '',
            name: ''
        }
        $scope.searchIcd = function () {
            $scope.searchFlag = true;
            $scope.searchOptionMirror = angular.copy($scope.searchOption);
            $scope.codeGrid.dataSource.page(1);
        }
        // 根据id判断是否已有ICD10
        var repeatJudge = function (id) {
            var n = 0;
            var arr = $scope.allIcd.data;
            for (var i = 0; i < arr.length; i++) {
                if (arr[i].id === id) {
                    return false;
                } else {
                    n++;
                }
            }
            if (n === arr.length) {
                return 'true';
            }
        }
        //取消
        $scope.cancel = function () {
            $scope.isChange = false;
            $scope.selectIcdMirror = null;
        }

        //保存
        $scope.save = function () {
            // 新增
            if ($scope.isNew) {
                var flag = repeatJudge($scope.selectIcdMirror.id);
                if (!flag) {
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '该编号已存在！');
                } else {
                    var newIcd = {
                        id: $scope.selectIcdMirror.id,
                        name: $scope.selectIcdMirror.name,
                        py: $scope.selectIcdMirror.py,
                        wb: $scope.selectIcdMirror.wb,
                        memo:$scope.selectIcdMirror.memo
                    }
                    configurationService.addIcd(newIcd).success(function () {
                        $scope.codeGrid.dataSource.read();
                        $scope.codeGrid.refresh();
                        $scope.selectIcdMirror = null;
                        $scope.selectIcd = null;
                        $scope.isChange = false;
                        csdToaster.info('新增成功！');
                    });
                }
            } else {              
                var flag = repeatJudge($scope.selectIcdMirror.id);
                if (flag) {
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '该编号不存在，请先添加信息！');
                } else {
                    configurationService.modifyIcd($scope.selectIcdMirror).success(function () {
                        $scope.codeGrid.dataSource.read();
                        $scope.codeGrid.refresh();
                        $scope.selectIcdMirror = null;
                        $scope.selectIcd = null;
                        $scope.isChange = false;
                        csdToaster.info('修改成功！');
                    });
                }
            }
        }

        $scope.addIcd = function () {
            $scope.selectIcdMirror = null;
            $scope.isChange = true;
            $scope.isNew =true;
        }

        $scope.modify = function () {
            if ($scope.selectIcd) {
                $scope.selectIcdMirror = angular.copy($scope.selectIcd);
                $scope.isChange = true;
                $scope.isNew = false;
            } else {
                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '请先选择一行数据！');
            }

        }

        $scope.delete = function () {
            if ($scope.selectIcd) {
                openDialog.openIconDialogOkCancel(
                     openDialog.NotifyMessageType,
                     $translate.instant('Alert'),
                     '确定删除本条数据？', function () {
                         configurationService.deleteIcd($scope.selectIcd).success(function () {
                             $scope.codeGrid.dataSource.read();
                             $scope.codeGrid.refresh();
                             $scope.selectIcd = null;
                             $scope.selectIcdMirror = null;
                         });
                     }) 
            } else {
                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '请先选择一行数据！');
            }

        }

        ; (function init() {
            $scope.filter = null;
            $scope.searchFlag = false;
            $scope.isNew = false;
            $scope.isChange = false;
            $scope.selectIcd = null;
            $scope.selectIcdMirror = null;
            $scope.selectId = '';
            $scope.allIcd = [];
            $scope.codeGridOption = {
                dataSource: new kendo.data.DataSource({
                    schema: {
                        data: 'data',
                        total: 'total',
                        aggregates: "aggregates",
                        model: {
                            fields: {
                                creatTime: { type: 'date' }
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
                                    filters: [
                                        { field: 'id', operator: "contains", value: $scope.searchOption.id },
                                        { field: 'name', operator: "contains", value: $scope.searchOption.name },
                                    ]
                                };
                                $scope.filter = filter;
                            }
                            options.data.filter = $scope.filter;
                            configurationService.getIcds(options.data).success(function (result) {
                                $scope.allIcd = result;
                                options.success(result);
                            }).error(function () {
                                busyRequestNotificationHub.requestEnded();
                            });
                        }
                    },
                    pageSize: constants.pageSize,
                    sort: [{ field: "id", dir: "asc" }],
                    serverPaging: true,
                    serverFiltering: true,
                    serverSorting: true
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
                    { field: 'id', title: "编号", width: '10%' },
                    { field: 'name', title: "病名", width: '20%' },
                    { field: 'py', title: "拼音", width: '10%' },
                    { field: 'wb', title: "五笔", width: '10%' },
                    { field: 'memo', title: "备注" }
                ],
                selectable: "row",
                change: function (e) {
                    var selectedRows = this.select();
                    var dataItem = this.dataItem(selectedRows[0]);
                    $timeout(function () {
                        $scope.selectIcd = dataItem;
                        $scope.selectId = dataItem.id;
                        $scope.selectIcdMirror = angular.copy($scope.selectIcd);
                    });
                }
            }
        })();
    }]);

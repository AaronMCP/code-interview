qualitycontrolModule.controller('QcCheckInfoController', [
'$scope', 'qualityService', 'application', '$timeout', 'constants', 'openDialog', '$translate', 'csdToaster',
function ($scope, qualityService, application, $timeout, constants, openDialog, $translate, csdToaster) {

    // 提醒框
    $scope.alert = function (info) {
        openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), info);
    }

    $scope.searchOption = {
        patientNo: '',
        localName: '',
        createTimeStart: '',
        createTimeEnd: ''
    };

    var statusMap = {};

    _.each(application.configuration.statusList, function (item) {
        statusMap[item.value] = item.text;
    });

    //status转为汉字
    var statusChange = function (data) {
        for (var i = 0; i < data.length; i++) {
            var status = data[i].status + '';
            var preStatus = data[i].preStatus + '';
            data[i].statusText = status ? statusMap[status] || '' : '';
            data[i].preStatusText = preStatus ? statusMap[preStatus] || '' : '';
        }
    }

    // 源病人查询
    $scope.sourceSearch = function () {
        $scope.sourceSearchFlag = true;
        $scope.sourceSearchOptionMirror = angular.copy($scope.searchOption);
        $scope.sourcePatitentInfoGrid.dataSource.page(1);
    };

    // 目标病人查询
    $scope.targetSearch = function () {
        $scope.targetSearchFlag = true;
        $scope.targetSearchOptionMirror = angular.copy($scope.searchOption);
        $scope.targetPatitentInfoGrid.dataSource.page(1);
    };

    //源病人点击
    $scope.clickSourcePatient = function () {
        $scope.isSourcePatient = true;
        qualityService.getOrder($scope.selectedSourcePatient.uniqueID).success(function (res) {
            $scope.sourceOrders = res;
            $scope.selectedSourceOrder = $scope.sourceOrders[0];
            if ($scope.selectedSourceOrder) {
                $scope.selectedSourceOrder.selectedBorder = true;
                qualityService.getProcedure($scope.selectedSourceOrder.uniqueID).success(function (res) {
                    $scope.sourceProcedures = res;
                    statusChange($scope.sourceProcedures);
                    $scope.selectedSourceProcedure = $scope.sourceProcedures[0];
                    if ($scope.selectedSourceProcedure) {
                        $scope.selectedSourceProcedure.selectedBorder = true;
                    } else {
                        $scope.sourceProcedures = [];
                        $scope.selectedSourceProcedure = null;
                    }
                })
            } else {
                $scope.sourceOrders = [];
                $scope.selectedSourceOrder = null;
                $scope.sourceProcedures = [];
                $scope.selectedSourceProcedure = null;
            }
        });
    }

    //源病人检查信息点击
    $scope.clickSourceOrder = function (order) {
        $scope.isSourcePatient = false;
        $scope.selectedSourceOrder.selected = false;
        $scope.selectedSourceOrder.selectedBorder = false;
        order.selected = true;
        $scope.selectedSourceOrder = order;
        qualityService.getProcedure(order.uniqueID).success(function (res) {
            $scope.sourceProcedures = res;
            statusChange($scope.sourceProcedures);
            $scope.selectedSourceProcedure = $scope.sourceProcedures[0];
            if ($scope.selectedSourceProcedure) {
                $scope.selectedSourceProcedure.selectedBorder = true;
            } else {
                $scope.sourceProcedures = [];
            }
        })
    };

    //源病人检查部位信息点击
    $scope.clickSourceProcedure = function (procedure) {
        $scope.isSourcePatient = false;
        $scope.selectedSourceProcedure.selected = false;
        $scope.selectedSourceProcedure.selectedBorder = false;
        procedure.selected = true;
        $scope.selectedSourceOrder.selected = false;
        $scope.selectedSourceOrder.selectedBorder = true;
        $scope.selectedSourceProcedure = procedure;
    }

    //目标病人点击
    $scope.clickTargetPatient = function () {
        $scope.isTargetPatient = true;
        qualityService.getOrder($scope.selectedTargetPatient.uniqueID).success(function (res) {
            $scope.targetOrders = res;
            $scope.selectedTargetOrder = $scope.targetOrders[0];
            if ($scope.selectedTargetOrder) {
                $scope.selectedTargetOrder.selectedBorder = true;
                qualityService.getProcedure($scope.selectedTargetOrder.uniqueID).success(function (res) {
                    $scope.targetProcedures = res;
                    statusChange($scope.targetProcedures);
                    $scope.selectedTargetProcedure = $scope.targetProcedures[0];
                    if ($scope.selectedTargetProcedure) {
                        $scope.selectedTargetProcedure.selectedBorder = true;
                    } else {
                        $scope.targetProcedures = [];
                        $scope.selectedTargetProcedure = null;
                    }
                })
            } else {
                $scope.targetOrders = [];
                $scope.targetProcedures = [];
            }
        });
    }

    //目标病人检查信息点击
    $scope.clickTargetOrder = function (order) {
        $scope.isTargetPatient = false;
        $scope.selectedTargetOrder.selected = false;
        $scope.selectedTargetOrder.selectedBorder = false;
        order.selected = true;
        $scope.selectedTargetOrder = order;
        qualityService.getProcedure(order.uniqueID).success(function (res) {
            $scope.targetProcedures = res;
            statusChange($scope.targetProcedures);
            $scope.selectedTargetProcedure = $scope.targetProcedures[0];
            if ($scope.selectedTargetProcedure) {
                $scope.selectedTargetProcedure.selectedBorder = true;
            } else {
                $scope.targetProcedures = [];
            }
        })
    };

    //目标病人检查部位信息点击
    $scope.clickTargetProcedure = function (procedure) {
        $scope.isTargetPatient = false;
        $scope.selectedTargetProcedure.selected = false;
        $scope.selectedTargetProcedure.selectedBorder = false;
        procedure.selected = true;
        $scope.selectedTargetOrder.selected = false;
        $scope.selectedTargetOrder.selectedBorder = true;
        $scope.selectedTargetProcedure = procedure;
    }

    // 验证病人是否被锁死并返回相关信息
    $scope.isLock = function (callback) {
        qualityService.getLockByPatientId($scope.selectedSourcePatient.patientNo).success(function (res) {
            if (res.isLock) {
                var info = $scope.selectedSourcePatient.localName + '（' + $scope.selectedSourcePatient.patientNo + '）已被' + res.lock.ownerName + '锁死在' + res.lock.ownerIP + '上';
                $scope.alert(info);
            } else {
                qualityService.getLockByPatientId($scope.selectedTargetPatient.patientNo).success(function (res2) {
                    if (res2.isLock) {
                        var info = $scope.selectedTargetPatient.localName + '（' + $scope.selectedTargetPatient.patientNo + '）已被' + res2.lock.ownerName + '锁死在' + res2.lock.ownerIP + '上';
                        $scope.alert(info);
                    } else {
                        callback();
                    }
                });
            }
        })
    }

    // 合并病人
    $scope.mergePatient = function () {
        if ($scope.selectedSourcePatient.uniqueID !== $scope.selectedTargetPatient.uniqueID) {
            $scope.isMergePatient = true;
            $scope.mergePatientWindow.title('合并病人');
            $scope.tipInfo = '选中的源病人：' + $scope.selectedSourcePatient.localName + '（' + $scope.selectedSourcePatient.patientNo + '）' + '将会被合并到目标病人：' + $scope.selectedTargetPatient.localName + '（' + $scope.selectedTargetPatient.patientNo + '），您确定吗？';
            $scope.mergePatientWindow.open();
        } else {
            $scope.alert('目标病人与源病人相同，无须合并！');
        }
    }

    //移动检查到目标病人
    $scope.moveOrder = function () {
        if ($scope.selectedSourcePatient.uniqueID !== $scope.selectedTargetPatient.uniqueID) {
            $scope.isMoveOrder = true;
            $scope.mergePatientWindow.title('移动检查');
            $scope.tipInfo = '选中的源检查：' + $scope.selectedSourceOrder.accNo + '将会移动到到目标病人：' + $scope.selectedTargetPatient.localName + '（' + $scope.selectedTargetPatient.patientNo + '），您确定吗？';
            $scope.mergePatientWindow.open();
        } else {
            $scope.alert('该检查已在目标病人下，无须移动！');
        }
    }

    //合并检查
    $scope.mergeOrder = function () {
        if ($scope.selectedSourceOrder.uniqueID !== $scope.selectedTargetOrder.uniqueID) {
            if ($scope.selectedSourceProcedure.modalityType === $scope.selectedTargetProcedure.modalityType) {
                $scope.isMergeOrder = true;
                $scope.mergePatientWindow.title('合并检查');
                $scope.tipInfo = '选中的源检查：' + $scope.selectedSourceOrder.accNo + '将会被合并到目标检查：' + $scope.selectedTargetOrder.accNo + '，您确定吗？';
                $scope.mergePatientWindow.open();
            } else {
                $scope.alert('源检查与目标检查设备类型不一致，无法合并！');
            }
        } else {
            $scope.alert('源检查与目标检查相同，无须合并！');
        }
    }

    //移动部位到目标病人
    $scope.moveCheckingItem = function () {
        if ($scope.selectedSourceOrder.uniqueID !== $scope.selectedTargetOrder.uniqueID) {
            if ($scope.selectedSourceProcedure.modalityType === $scope.selectedTargetProcedure.modalityType) {
                if ($scope.sourceProcedures.length < 2) {
                    $scope.alert('源检查部位只有一个，无法移动，请直接合并检查！')
                } else {
                    $scope.isMoveProcedure = true;
                    $scope.mergePatientWindow.title('移动部位');
                    $scope.tipInfo = '选中的源检查部位将会被移动到目标检查：' + $scope.selectedTargetOrder.accNo + '，您确定吗？';
                    $scope.mergePatientWindow.open();
                }
            } else {
                $scope.alert('源检查与目标检查设备类型不一致，无法合并！');
            }
        } else {
            $scope.alert('源检查项目与目标检查项目属于同一个检查，无须移动！');
        }
    }

    //弹出框确定
    $scope.mergeSure = function () {
        //合并病人
        if ($scope.isMergePatient) {
            var mergeInfo = {
                srcPatientID: $scope.selectedSourcePatient.uniqueID,
                targetPatientID: $scope.selectedTargetPatient.uniqueID,
                afterDelPatient: $scope.afterDelPatient
            }
            qualityService.mergePatient(mergeInfo).success(function (res) {
                if (res) {
                    $scope.mergePatientWindow.close();
                    $scope.sourcePatitentInfoGrid.dataSource.read();
                    $scope.sourcePatitentInfoGrid.refresh();
                    $scope.targetPatitentInfoGrid.dataSource.read();
                    $scope.targetPatitentInfoGrid.refresh();
                    csdToaster.info('合并成功！');
                    $scope.isMergePatient = false;
                } else {
                    csdToaster.info('合并失败！');
                }

            });
            //移动检查
        } else if ($scope.isMoveOrder) {
            var mergeInfo = {
                srcOrderID: $scope.selectedSourceOrder.uniqueID,
                targetPatientID: $scope.selectedTargetPatient.uniqueID
            }
            qualityService.moveOrder(mergeInfo).success(function (res) {
                if (res) {
                    $scope.mergePatientWindow.close();
                    $scope.selectedSourceOrder.selected = false;
                    var index = $scope.sourceOrders.indexOf($scope.selectedSourceOrder);
                    $scope.sourceOrders.splice(index, 1);
                    $scope.targetOrders.push($scope.selectedSourceOrder);
                    if ($scope.sourceOrders.length > 0) {
                        $scope.clickSourceOrder($scope.sourceOrders[0]);
                    } else {
                        $scope.selectedSourceOrder = null;
                        $scope.selectedSourceProcedure = null;
                        $scope.sourceProcedures = [];
                    }
                    csdToaster.info('移动成功！');
                    $scope.isMoveOrder = false;
                } else {
                    csdToaster.info('移动失败！');
                }
            });
            //合并检查
        } else if ($scope.isMergeOrder) {
            var mergeInfo = {
                srcOrderID: $scope.selectedSourceOrder.uniqueID,
                targetOrderID: $scope.selectedTargetOrder.uniqueID,
                isMergeRequisition: $scope.isMergeRequisition,
                isMergeOrderCharge: $scope.isMergeOrderCharge
            }
            qualityService.mergeOrder(mergeInfo).success(function (res) {
                if (res) {
                    $scope.mergePatientWindow.close();
                    $scope.selectedSourceProcedure.selected = false;
                    $scope.selectedSourceProcedure.selectedBorder = false;
                    var index = $scope.sourceOrders.indexOf($scope.selectedSourceOrder);
                    $scope.sourceOrders.splice(index, 1);
                    $scope.targetProcedures.push.apply($scope.targetProcedures, $scope.sourceProcedures);
                    if ($scope.sourceOrders.length > 0) {
                        $scope.clickSourceOrder($scope.sourceOrders[0]);
                    } else {
                        $scope.selectedSourceOrder = null;
                        $scope.selectedSourceProcedure = null;
                        $scope.sourceProcedures = [];
                    }
                    csdToaster.info('合并成功！');
                    $scope.isMergeOrder = false;
                } else {
                    csdToaster.info('合并失败！');
                }
            });
        } else {
            //移动部位
            var mergeInfo = {
                srcProcedureID: $scope.selectedSourceProcedure.uniqueID,
                targetProcedureID: $scope.selectedTargetProcedure.uniqueID
            }
            qualityService.moveCheckingItem(mergeInfo).success(function (res) {
                if (res) {
                    $scope.mergePatientWindow.close();
                    $scope.selectedSourceProcedure.selected = false;
                    var index = $scope.sourceProcedures.indexOf($scope.selectedSourceProcedure);
                    $scope.sourceProcedures.splice(index, 1);
                    $scope.targetProcedures.push($scope.selectedSourceProcedure);
                    if ($scope.sourceProcedures.length > 0) {
                        $scope.selectedSourceProcedure = $scope.sourceProcedures[0];
                        $scope.selectedSourceProcedure.selected = true;
                    } else {
                        $scope.selectedSourceProcedure = null;
                    }
                    csdToaster.info('移动成功！');
                } else {
                    csdToaster.info('移动失败！');
                }
            });
        }
    }
    //$scope.isLock($scope.mergeSure);
    //弹出框取消
    $scope.mergeCancel = function () {
        $scope.isMergePatient = false;
        $scope.isDeleteSourcePatient = false;
        $scope.mergePatientWindow.close();
    }

    ; +(function init() {

        //源病人信息表格
        $scope.sourcePatitentInfoGridOption = {
            dataSource: new kendo.data.DataSource({
                schema: {
                    data: 'data',
                    total: 'total',
                    aggregates: "aggregates",
                    model: {
                        fields: {
                            birthday: { type: 'date' }
                        }
                    }
                },
                transport: {
                    read: function (options) {
                        var flag = false;
                        if ($scope.sourceSearchOptionMirror) {
                            flag = true;
                            for (var attr in $scope.searchOption) {
                                if ($scope.searchOption[attr] !== $scope.sourceSearchOptionMirror[attr]) {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        if ($scope.sourceSearchFlag && flag) {
                            var filter = {
                                logic: "and",
                                filters: [
                                    { field: 'patientNo', operator: "contains", value: $scope.searchOption.patientNo },
                                    { field: 'localName', operator: "contains", value: $scope.searchOption.localName }
                                ]
                            };
                            if ($scope.searchOption.createTimeStart) {
                                filter.filters.push({
                                    field: "createTime",
                                    operator: "gte",
                                    value: new Date($scope.searchOption.createTimeStart)
                                })
                            }
                            if ($scope.searchOption.createTimeEnd) {
                                filter.filters.push({
                                    field: "createTime",
                                    operator: "lte",
                                    value: new Date($scope.searchOption.createTimeEnd + ' 23:59:59')
                                });
                            }
                            $scope.sourceFilter = filter;
                        }
                        options.data.filter = $scope.sourceFilter;
                        qualityService.getPageablePatients(options.data).success(function (result) {
                            options.success(result);
                            // 默认选中第一行
                            var row = $('.source-patient tbody tr:nth-child(1)');
                            $scope.sourcePatitentInfoGrid.select(row);
                            $scope.selectedSourcePatient = $scope.sourcePatitentInfoGrid.dataItem($scope.sourcePatitentInfoGrid.select());
                            if (!$scope.selectedSourcePatient) {
                                $scope.sourceOrders = [];
                                $scope.selectedSourceOrder = null;
                                $scope.sourceProcedures = [];
                                $scope.selectedSourceProcedure = null;
                            }

                        });
                    }
                },
                pageSize: constants.pageSize,
                sort: [{ field: "patientNo", dir: "desc" }],
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true,
            }),
            pageable: {
                refresh: true,
                buttonCount: 5,
                input: true
            },
            columns: [
                {
                    field: 'patientNo', title: "病人编号", width: '25%',
                    attributes: {
                        'title': '{{dataItem.patientNo}}',
                        style: 'word-wrap:break-word;'
                    }
                },
                {
                    field: 'localName', title: "病人姓名",
                    attributes: {
                        'title': '{{dataItem.localName}}',
                        style: 'word-wrap:break-word;'
                    }
                },
                {
                    field: 'birthday', title: "生日", format: '{0: yyyy-MM-dd}',
                    attributes: {
                        'title': '{{dataItem.birthday}}',
                        style: 'word-wrap:break-word;'
                    }
                },
                {
                    field: 'gender', title: "性别",
                    attributes: {
                        'title': '{{dataItem.gender}}',
                        style: 'word-wrap:break-word;'
                    }
                },
                {
                    field: 'telephone', title: "电话",
                    attributes: {
                        'title': '{{dataItem.telephone}}',
                        style: 'word-wrap:break-word;'
                    }
                }
            ],
            selectable: "row",
            sortable: {
                allowUnsort: false
            },
            change: function (e) {
                var selectedItem = this.dataItem(this.select()[0]);
                $timeout(function () {
                    $scope.selectedSourcePatient = selectedItem;
                    if ($scope.selectedSourcePatient) {
                        $scope.clickSourcePatient();
                    }
                });
            }
        };

        //目标病人信息表格
        $scope.targetPatitentInfoGridOption = {
            dataSource: new kendo.data.DataSource({
                schema: {
                    data: 'data',
                    total: 'total',
                    aggregates: "aggregates",
                    model: {
                        fields: {
                            birthday: { type: 'date' }
                        }
                    }
                },
                transport: {
                    read: function (options) {
                        var flag = false;
                        if ($scope.targetSearchOptionMirror) {
                            flag = true;
                            for (var attr in $scope.searchOption) {
                                if ($scope.searchOption[attr] !== $scope.targetSearchOptionMirror[attr]) {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        if ($scope.targetSearchFlag && flag) {
                            var filter = {
                                logic: "and",
                                filters: [
                                    { field: 'patientNo', operator: "contains", value: $scope.searchOption.patientNo },
                                    { field: 'localName', operator: "contains", value: $scope.searchOption.localName }
                                ]
                            };
                            if ($scope.searchOption.createTimeStart) {
                                filter.filters.push({
                                    field: "createTime",
                                    operator: "gte",
                                    value: new Date($scope.searchOption.createTimeStart)
                                })
                            }
                            if ($scope.searchOption.createTimeEnd) {
                                filter.filters.push({
                                    field: "createTime",
                                    operator: "lte",
                                    value: new Date($scope.searchOption.createTimeEnd + ' 23:59:59')
                                });
                            }
                            $scope.targetFilter = filter;
                        }
                        options.data.filter = $scope.targetFilter;
                        qualityService.getPageablePatients(options.data).success(function (result) {
                            options.success(result);
                            // 默认选中第一行
                            var row = $('.target-patient tbody tr:nth-child(1)');
                            $scope.targetPatitentInfoGrid.select(row);
                            $scope.selectedTargetPatient = $scope.targetPatitentInfoGrid.dataItem($scope.targetPatitentInfoGrid.select());
                            if (!$scope.selectedTargetPatient) {
                                $scope.targetOrders = [];
                                $scope.selectedTargetOrder = null;
                                $scope.targetProcedures = [];
                                $scope.selectedTargetProcedure = null;
                            }
                        });
                    }
                },
                pageSize: constants.pageSize,
                sort: [{ field: "patientNo", dir: "desc" }],
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true,
            }),
            pageable: {
                refresh: true,
                buttonCount: 5,
                input: true
            },
            columns: [
                {
                    field: 'patientNo', title: "病人编号", width: '25%',
                    attributes: {
                        'title': '{{dataItem.patientNo}}',
                        style: 'word-wrap:break-word;'
                    }
                },
                {
                    field: 'localName', title: "病人姓名",
                    attributes: {
                        'title': '{{dataItem.localName}}',
                        style: 'word-wrap:break-word;'
                    }
                },
                {
                    field: 'birthday', title: "生日", format: '{0: yyyy-MM-dd}',
                    attributes: {
                        'title': '{{dataItem.birthday}}',
                        style: 'word-wrap:break-word;'
                    }
                },
                {
                    field: 'gender', title: "性别",
                    attributes: {
                        'title': '{{dataItem.gender}}',
                        style: 'word-wrap:break-word;'
                    }
                },
                {
                    field: 'telephone', title: "电话",
                    attributes: {
                        'title': '{{dataItem.telephone}}',
                        style: 'word-wrap:break-word;'
                    }
                }
            ],
            selectable: "row",
            sortable: {
                allowUnsort: false
            },
            change: function (e) {
                var selectedItem = this.dataItem(this.select()[0]);
                $timeout(function () {
                    $scope.selectedTargetPatient = selectedItem;
                    if ($scope.selectedTargetPatient) {
                        $scope.clickTargetPatient();
                    }
                });
            }
        };

        $scope.selectedSourcePatient = null;
        $scope.selectedSourceOrder = null;
        $scope.selectedSourceProcedure = null;
        $scope.selectedTargetPatient = null;
        $scope.selectedTargetOrder = null;
        $scope.selectedTargetProcedure = null;
        $scope.sourceOrders = [];
        $scope.sourceProcedures = [];
        $scope.targetOrders = [];
        $scope.targetProcedures = [];
        $scope.isMergeRequisition = false;
        $scope.isMergeOrderCharge = false;
        $scope.afterDelPatient = false;
    })();
}]);

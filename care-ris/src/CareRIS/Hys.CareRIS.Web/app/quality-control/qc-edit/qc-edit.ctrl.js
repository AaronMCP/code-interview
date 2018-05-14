qualitycontrolModule.controller('QcEditController', [
'$scope', 'constants', 'qualityService', 'application', '$timeout', '$rootScope', 'csdToaster', 'openDialog', '$translate',
function ($scope, constants, qualityService, application, $timeout, $rootScope, csdToaster, openDialog, $translate) {

    $scope.searchOption = {
        patientNo: '',
        localName: '',
        createTimeStart: '',
        createTimeEnd: ''
    };

    //查询
    $scope.search = function () {
        $scope.searchFlag = true;
        $scope.searchOptionMirror = angular.copy($scope.searchOption);
        $scope.patitentInfoGrid.dataSource.page(1);
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

    //提醒框
    $scope.alert = function (info) {
        openDialog.openIconDialog(
            openDialog.NotifyMessageType.Warn,
            $translate.instant('Alert'),
            info);
    }

    //病人点击
    $scope.clickPatient = function () {
        $scope.isPatient = true;
        $scope.isOrder = false;
        $scope.isProcedure = false;
        $('.edit-title-tab').removeClass('active').eq(0).addClass('active');
        $('.edit-content-tab').removeClass('active').eq(0).addClass('in active');
        qualityService.getOrder($scope.selectedPatient.uniqueID).success(function (res) {
            $scope.orders = res;
            for (var i = 0; i < $scope.orders.length; i++) {
                $scope.orders[i].createTime = $scope.dateFormat($scope.orders[i].createTime, true)
            }
            $scope.selectedOrder = $scope.orders[0];
            if ($scope.selectedOrder) {
                $scope.selectedOrder.selectedBorder = true;
                qualityService.getProcedure($scope.selectedOrder.uniqueID).success(function (res) {
                    $scope.procedures = res;
                    statusChange($scope.procedures);
                    $scope.selectedProcedure = $scope.procedures[0];
                    if ($scope.selectedProcedure) {       
                        $scope.selectedProcedure.selectedBorder = true;
                        if (!$scope.selectedProcedure.preStatus) {
                            $scope.operateText = '撤销检查';
                            $scope.operateReason = '撤销理由';
                            $scope.operateFlag = true;
                        } else {
                            $scope.operateText = '恢复检查';
                            $scope.operateReason = '恢复理由';
                            $scope.operateFlag = false;
                        }
                    }
                })
            } else {
                $scope.procedures = null;
            }
        });
    }

    //检查信息点击
    $scope.clickOrder = function (order) {
        $scope.isOrder = true;
        $scope.isPatient = false;
        $scope.isProcedure = false;
        $scope.selectedOrder.selected = false;
        $scope.selectedOrder.selectedBorder = false;
        order.selected = true;
        $scope.selectedOrder = order;
        $scope.selectedProcedure.selected = false;
        $('.edit-title-tab').removeClass('active').eq(1).addClass('active');
        $('.edit-content-tab').removeClass('active').eq(1).addClass('in active');
        qualityService.getProcedure(order.uniqueID).success(function (res) {
            $scope.procedures = res;
            statusChange($scope.procedures);
            $scope.selectedProcedure = $scope.procedures[0];
            if ($scope.selectedProcedure) {
                if (!$scope.selectedProcedure.preStatus) {
                    $scope.operateText = '撤销检查';
                    $scope.operateReason = '撤销理由';
                    $scope.operateFlag = true;
                } else {
                    $scope.operateText = '恢复检查';
                    $scope.operateReason = '恢复理由';
                    $scope.operateFlag = false;
                }
                $scope.selectedProcedure.selectedBorder = true;
            }
        })
    };

    //检查部位信息点击
    $scope.clickProcedure = function (procedure) {
        $scope.isPatient = false;
        $scope.isOrder = false;
        $scope.isProcedure = true;
        $scope.selectedProcedure.selected = false;
        $scope.selectedProcedure.selectedBorder = false;
        procedure.selected = true;
        $scope.selectedOrder.selectedBorder = true;
        $scope.selectedOrder.selected = false;
        $scope.selectedProcedure = procedure;
        $('.edit-title-tab').removeClass('active').eq(2).addClass('active');
        $('.edit-content-tab').removeClass('active').eq(2).addClass('in active');
    }

    //修改
    $scope.changeSomething = function () {
        $scope.patientName = $scope.selectedMirrorPatient.localName ? '（' + $scope.selectedMirrorPatient.localName + '）' : '';
        $scope.patientAccNo = $scope.selectedOrder ? '（' + $scope.selectedOrder.accNo + '）' : '';
        $scope.patientCheckIngItem = $scope.selectedProcedure ? '（' + $scope.selectedProcedure.checkingItem + '）' : '';
        $scope.selectedMirrorPatient.birthday = $scope.dateFormat($scope.selectedMirrorPatient.birthday);
        $rootScope.$broadcast('event:changeInfo', {
            patient: $scope.selectedMirrorPatient,
            order: $scope.selectedOrder,
            procedure: $scope.selectedProcedure,
            isPatient: $scope.isPatient,
            isOrder: $scope.isOrder,
            isProcedure: $scope.isProcedure
        });
    }


    //保存修改
    $scope.save = function () {
        if ($scope.isPatient) {
            $scope.selectedPatient = angular.copy($scope.selectedMirrorPatient);
            if ($scope.selectedPatient.gender && $scope.selectedPatient.localName) {
                $scope.selectedPatient.birthday = new Date($scope.selectedPatient.birthday);
                if ($scope.selectedPatient.birthday > new Date()) {
                    $scope.alert('生日不能超过今天！');
                    return;
                } else {
                    var params = {
                        patient: $scope.selectedPatient,
                    }
                    qualityService.updatePatient($scope.selectedPatient.uniqueId, params).success(function (res) {
                        if (res) {
                            $scope.patitentInfoGrid.dataSource.read();
                            $scope.patitentInfoGrid.refresh();
                            $('#qcEditModal').modal('hide');
                            csdToaster.info('病人信息修改成功！');
                        } else {
                            csdToaster.info('病人信息修改失败！');
                        }
                    });
                }
            } else {
                $scope.alert( '带星号的为必填项！');
            }
        } else if ($scope.isOrder) {
            qualityService.updateOrder($scope.selectedOrder.uniqueID, $scope.selectedOrder).success(function (res) {
                if (res) {
                    $('#qcEditModal').modal('hide');
                    csdToaster.info('检查信息修改成功！');
                } else {
                    csdToaster.info('检查信息修改失败！');
                }
            });
        } else {
            qualityService.updateProcedure($scope.selectedProcedure).success(function (res) {
                if (res) {
                    $('#qcEditModal').modal('hide');
                    csdToaster.info('检查部位信息修改成功！');
                } else {
                    csdToaster.info('检查部位信息修改失败！');
                }
            });
        }
    }
    //判断检查状态状态
    $scope.chcekStatus = function () {
        if ($scope.procedures) {
            for (var i = 0; i < $scope.procedures.length; i++) {
                if ($scope.procedures[i].status > 50 || $scope.procedures[i].status === 50) {
                    return true;
                }
            }
        } 
        return false;
    }
    // 删除
    $scope.delete = function () {
        if ($scope.isPatient) {
            openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '是否删除该病人信息', function () {
                if ($scope.chcekStatus()) {
                    $scope.alert('已检查的病人不能删除！');
                    return;
                }
                qualityService.deletePatient($scope.selectedPatient.uniqueID).success(function (res) {        
                        $scope.patitentInfoGrid.dataSource.read();
                        $scope.patitentInfoGrid.refresh();
                        csdToaster.info('删除成功！');          
                });
            });

        } else if ($scope.isOrder) {
            openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '是否删除该检查信息', function () {
                if ($scope.chcekStatus()) {
                    $scope.alert('已检查的病人不能删除检查信息！');
                    return;
                }
                qualityService.deleteOrder($scope.selectedOrder.uniqueID).success(function (res) {
                        var index = $scope.orders.indexOf($scope.selectedOrder);
                        $scope.orders.splice(index, 1);
                        $scope.selectedOrder = $scope.orders[0];
                        if ($scope.selectedOrder) {
                            $scope.selectedOrder.selected = true;
                            $scope.clickOrder($scope.selectedOrder);
                        }
                        $scope.procedures = null;
                        csdToaster.info('删除成功！');
                });
            });

        } else {
            openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '是否删除该检查部位信息', function () {
                if ($scope.selectedProcedure.status > 50 || $scope.selectedProcedure.status === 50) {
                    $scope.alert('已检查的病人不能删除检查部位信息！');
                    return;
                }
                if ($scope.procedures.length < 2) {
                    $scope.alert('检查部位信息只有一个，不可删除！');
                    return;
                }
                qualityService.deleteProcedure($scope.selectedProcedure.uniqueID).success(function (res) {
                    if (res===0) {
                        var index = $scope.procedures.indexOf($scope.selectedProcedure);
                        $scope.procedures.splice(index, 1);
                        $scope.selectedProcedure = $scope.procedures[0];
                        $scope.selectedProcedure.selected = true;
                        csdToaster.info('删除成功！');
                    } else {
                        csdToaster.info('删除失败！');
                    }

                });
            });
        }
    }

    //检查操作
    $scope.operateProcedure = function () {
        if ($scope.operateFlag) {
            openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '是否要撤销该检查', function () {
                qualityService.revokeProcedure($scope.selectedOrder.uniqueID).success(function (res) {
                    if (res) {
                        $scope.clickOrder($scope.selectedOrder);
                        csdToaster.info('撤销成功！');
                        $scope.operateFlag = false;
                        $scope.operateText = '恢复检查';
                        $scope.operateReason = '恢复理由';
                    }
                });
            });
        } else {
            openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '是否要恢复该检查？', function () {
                qualityService.recoverProcedure($scope.selectedOrder.uniqueID).success(function (res) {
                    if (res) {
                        $scope.clickOrder($scope.selectedOrder);
                        csdToaster.info('恢复成功！');
                        $scope.operateFlag = false;
                        $scope.operateText = '撤销检查';
                        $scope.operateReason = '撤销理由';
                    }
                });
            });
        }
    }
    // 日期转换
    $scope.dateFormat = function (string, flag) {
        var date = new Date(string);
        var year = date.getFullYear();
        var month = date.getMonth() + 1;
        var day = date.getDate();
        var hour = date.getHours();
        var min = date.getMinutes();
        if (hour < 10) {
            hour = '0' + hour;
        }
        if (min < 10) {
            min = '0' + min;
        }
        if (flag) {
            date = year + '-' + month + '-' + day + ' ' + hour + ':' + min;
        } else {
            date = year + '/' + month + '/' + day;
        }
        return date;
    }

    ; +(function init() {
        $scope.patitentInfoGridOption = {
            dataSource: new kendo.data.DataSource({
                schema: {
                    data: 'data',
                    total: 'total',
                    aggregates: "aggregates",
                    model: {
                        fields: {
                            birthday: { type: 'date' },
                            createTime: { type: 'date' }
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
                            $scope.filter = filter;
                        }
                        options.data.filter = $scope.filter;
                        qualityService.getPageablePatients(options.data).success(function (result) {
                            options.success(result);
                            // 默认选中第一行
                            var row = $('.infopatient tbody tr:nth-child(1)');
                            $scope.patitentInfoGrid.select(row);
                            $scope.selectedPatient = $scope.patitentInfoGrid.dataItem($scope.patitentInfoGrid.select());
                            $scope.selectedMirrorPatient = angular.copy($scope.selectedPatient);
                            if (!$scope.selectedPatient) {
                                $scope.orders = [];
                                $scope.procedures = [];
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
                    field: 'patientNo', title: "病人编号",
                    attributes: {
                        'title': '{{dataItem.patientNo}}',
                        style: "word-wrap:break-word;"
                    }
                },
                {
                    field: 'localName', title: "病人姓名", width: '10%',
                    attributes: {
                        'title': '{{dataItem.localName}}',
                        style: "word-wrap:break-word;"
                    }
                },
                {
                    field: 'birthday', title: "生日", format: '{0: yyyy-MM-dd}',
                    attributes: {
                        'title': '{{dataItem.birthday}}',
                        style: "word-wrap:break-word;"
                    }
                },
                { field: 'gender', title: "性别", width: '5%' },
                {
                    field: 'telephone', title: "电话",
                    attributes: {
                        'title': '{{dataItem.telephone}}',
                        style: "word-wrap:break-word;"
                    }
                },
                { field: 'isvip', title: "VIP", template: '{{dataItem.isvip===0?"否":"是"}}',width:'5%' },
                {
                    field: 'createTime', title: "创建时间", format: '{0: yyyy-MM-dd HH:mm}',
                    attributes: {
                        'title': '{{dataItem.createTime}}',
                        style: "word-wrap:break-word;"
                    }
                },
                {
                    field: 'referenceNo', title: "证件号码", width: '17%',
                    attributes: {
                        'title': '{{dataItem.referenceNo}}',
                        style: "word-wrap:break-word;"
                    }
                },
                {
                    field: 'medicareNo', title: "医保号码",
                    attributes: {
                        'title': '{{dataItem.medicareNo}}',
                        style: "word-wrap:break-word;"
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
                    $scope.selectedPatient = selectedItem;
                    $scope.selectedMirrorPatient = angular.copy($scope.selectedPatient);
                    $scope.clickPatient();
                });
            }
        };
        $scope.operateFlag = false;
        $scope.selectedPatient = null;
        $scope.selectedOrder = null;
        $scope.selectedProcedure = null;
        $scope.isPatient = false;
        $scope.isOrder = false;
        $scope.isProcedure = false;
        $scope.orders = [];
        $scope.procedures = [];
        $scope.patientName = '';
        $scope.patientAccNo = '';
        $scope.patientCheckIngItem = '';
    })();
}]);
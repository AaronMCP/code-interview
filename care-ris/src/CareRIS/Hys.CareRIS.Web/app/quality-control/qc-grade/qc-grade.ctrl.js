qualitycontrolModule.controller('QcGradeController', [
'$scope', 'constants', 'qualityService', 'application', '$timeout', 'csdToaster',
function ($scope, constants, qualityService, application, $timeout, csdToaster) {
    var statusMap = {};
    _.each(application.configuration.statusList, function (item) {
        statusMap[item.value] = item.text;
    });

    $scope.searchOption = {
        localName: '',
        accNo: '',
        examineTimeStart: '',
        examineTimeEnd: ''
    };

    $scope.search = function () {
        $scope.searchFlag = true;
        $scope.searchOptionMirror = angular.copy($scope.searchOption);
        $scope.pingfenGrid.dataSource.page(1);
    };

    $scope.togglePan = function (name) {
        for (var attr in $scope.pan) {
            $scope.pan[attr] && ($scope.pan[attr] = false);
        }
        !$scope.pan[name] && ($scope.pan[name] = true);
    };


    +(function init() {
        $scope.pan = {
            image: true,
            report: false
        };
        $scope.filter = null;
        $scope.pingfenGridOption = {
            dataSource: new kendo.data.DataSource({
                schema: {
                    data: "data",
                    total: "total",
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
                                logic: "and",
                                filters: [
                                    { field: 'accNo', operator: "contains", value: $scope.searchOption.accNo },
                                    { field: 'localName', operator: "contains", value: $scope.searchOption.localName }
                                ]
                            };
                            if ($scope.searchOption.examineTimeStart) {
                                filter.filters.push({
                                    field: "examineTime",
                                    operator: "gte",
                                    value: new Date($scope.searchOption.examineTimeStart)
                                })
                            }
                            if ($scope.searchOption.examineTimeEnd) {
                                filter.filters.push({
                                    field: "examineTime",
                                    operator: "lte",
                                    value: new Date($scope.searchOption.examineTimeEnd + ' 23:59:59')
                                });
                            }
                            $scope.filter = filter;
                        }
                        options.data.filter = $scope.filter;
                        qualityService.getQcList(options.data).success(function (result) {
                            for (var i = 0; i < result.data.length; i++) {
                                result.data[i].statusText = result.data[i].status + '';
                                result.data[i].statusText = result.data[i].statusText ? statusMap[result.data[i].statusText] || '' : '';
                            }
                            options.success(result);
                        }).error(function (e, status) {
                            options.error(e.name, status, e.message);
                        });
                    }
                },
                error: function (e) {
                    csdToaster.error('请求数据失败！');
                },
                sort: [{ field: "accNo", dir: "desc" }],
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true,
                pageSize: constants.pageSize,
            }),
            pageable: {
                refresh: true,
                buttonCount: 5,
                input: true
            },
            sortable: {
                allowUnsort: false
            },
            reorderable: true,
            resizable: true,
            columns: [
                {
                    field: 'accNo', title: '放射编号',
                    attributes: {
                        'title': "{{dataItem.accNo}}",
                        style: 'word-wrap:break-word;'
                    },
                    width: '150px'
                },
                {
                    field: 'localName', title: '病人姓名',
                    attributes: {
                        'title': "{{dataItem.localName}}",
                        style: 'word-wrap:break-word;'
                    },
                    width: '120px'
                },
                {
                    field: 'examSystem', title: '检查系统',
                    attributes: {
                        'title': "{{dataItem.examSystem}}",
                        style: 'word-wrap:break-word;'
                    },
                    width: '120px'
                },
                {
                    field: 'modality', title: '设备',
                    attributes: {
                        'title': "{{dataItem.modality}}",
                        style: 'word-wrap:break-word;'
                    },
                    width: '80px'
                },
                {
                    field: 'statusText', title: '检查状态',
                    attributes: {
                        'title': "{{dataItem.statusText}}",
                        style: 'word-wrap:break-word;'
                    },
                    width: '80px'
                },
                {
                    field: 'gender', title: '性别',
                    attributes: {
                        'title': "{{dataItem.gender}}",
                        style: 'word-wrap:break-word;'
                    },
                    width: '80px'
                },
                {
                    field: 'applyDept', title: '申请部门',
                    attributes: {
                        'title': "{{dataItem.applyDept}}",
                        style: 'word-wrap:break-word;'
                    },
                    width: '110px'
                },
                {
                    field: 'patientType', title: '病人类型',
                    attributes: {
                        'title': "{{dataItem.patientType}}",
                        style: 'word-wrap:break-word;'
                    },
                    width: '90px'
                },
                {
                    field: 'rpDesc', title: '检查',
                    attributes: {
                        'title': "{{dataItem.rpDesc}}",
                        style: 'word-wrap:break-word;'
                    },
                    width: '150px'
                },
                {
                    field: 'examineTime', title: '检查时间', format: '{0: yyyy-MM-dd HH:mm}',
                    attributes: {
                        'title': "{{dataItem.examineTime}}",
                        style: 'word-wrap:break-word;'
                    },
                    width: '160px'
                },
                {
                    field: 'checkingItem', title: '检查项目',
                    attributes: {
                        'title': "{{dataItem.checkingItem}}",
                        style: 'word-wrap:break-word;'
                    },
                    width: '150px'
                },
                {
                    field: 'result', title: '图像质量',
                    attributes: {
                        'title': "{{dataItem.result}}",
                        style: 'word-wrap:break-word;'
                    },
                    width: '80px'
                },
                {
                    field: 'comment', title: '图像评分备注',
                    attributes: {
                        'title': '{{dataItem.comment}}',
                        style: 'word-wrap:break-word;'
                    },
                    width: '100px'
                },
                {
                    field: 'reportQuality', title: '报告质量',
                    attributes: {
                        'title': "{{dataItem.reportQuality}}",
                        style: 'word-wrap:break-word;'
                    },
                    width: '80px'
                },
                {
                    field: 'reportQualityComments', title: '报告备注',
                    attributes: {
                        'title': "{{dataItem.reportQualityComments}}",
                        style: 'word-wrap:break-word;'
                    },
                    width: '100px'
                },
                {
                    field: 'accordRate', title: '诊断符合',
                    attributes: {
                        'title': "{{dataItem.accordRate}}",
                        style: 'word-wrap:break-word;'
                    },
                    width: '80px'
                }
            ],
            selectable: "multiple, row",
            change: function (e) {
                var selectedRows = this.select();
                selectedItems = [];
                for (var i = 0; i < selectedRows.length; i++) {
                    var dataItem = this.dataItem(selectedRows[i]);
                    selectedItems.push(dataItem);
                }
                $timeout(function () {
                    $scope.$broadcast('event:QCSelectedItemChanged', selectedItems);
                });
            }
        };
    })();
}]);
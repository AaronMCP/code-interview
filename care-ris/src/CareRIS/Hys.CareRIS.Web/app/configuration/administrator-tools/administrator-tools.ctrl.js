configurationModule.controller('AdministratorToolsCtrl', ['$scope', 'configurationService', '$timeout', 'openDialog', '$translate', 'csdToaster', 'constants',
function ($scope, configurationService, $timeout, openDialog, $translate, csdToaster, constants) {

    $scope.searchCriteria = {
        ownerName: null,
        createStartTime: null,
        createEndTime: null,
        haveTime: false
    };
    //解锁表格
    $scope.openLockGridOption = {
        dataSource: new kendo.data.DataSource({
            schema: {
                data: 'data',
                total: 'total',
            },
            transport: {
                read: function (options) {
                    $scope.criteria.Pagination = {
                        pageIndex: options.data.page ? options.data.page : 1,
                        pageSize: constants.pageSize
                    }
                    configurationService.searchLocks($scope.criteria).success(function (data) {
                        options.success(data);
                    });
                },
            },
            error: function (e) {
                csdToaster.error('请求数据失败！');
            },
            pageSize: constants.pageSize,
            sort: [{ field: "createTime", dir: "desc" }],
        }),
        pageable: {
            refresh: true,
            buttonCount: 5,
            input: true
        },
        scrollable: true,
        filterable: false,
        sortable: true,
        resizable: true,
        reorderable: true,
        columns: [
            {
                field: 'loginName',
                title: "用户登录名",
                attributes: {
                    'title': '{{dataItem.loginName}}',
                    style: "word-wrap:break-word;"
                }
            },
            {
                field: 'ownerName',
                title: "所属用户",
                attributes: {
                    'title': '{{dataItem.ownerName}}',
                    style: "word-wrap:break-word;"
                }
            },
            {
                field: 'ownerIP',
                title: "来自IP",
                attributes: {
                    'title': '{{dataItem.ownerIP}}',
                    style: "word-wrap:break-word;"
                }
            },
            {
                field: 'createTime',
                title: "被锁时间",
                template: '{{dataItem.createTime|date:"yyyy-MM-dd HH:mm"}}',
                attributes: {
                    'title': "{{dataItem.createTime|date:'yyyy-MM-dd HH:mm'}}",
                    style: "word-wrap:break-word;"
                }
            },
            {
                field: 'moduleID',
                title: "模块ID"
            },
            {
                field: 'moduleTitle',
                title: "模块名称",
                attributes: {
                    'title': '{{dataItem.moduleTitle}}',
                    style: "word-wrap:break-word;"
                }
            },
            {
                field: 'patientID',
                title: "病人编号"
                , attributes: {
                    'title': '{{dataItem.patientID}}',
                    style: "word-wrap:break-word;"
                }
            },
            {
                field: 'patientName',
                title: "病人姓名"
            },
            {
                field: 'accNo',
                title: "放射编号",
                attributes: {
                    'title': '{{dataItem.accNo}}',
                    style: "word-wrap:break-word;"
                }
            },

        ],
        selectable: "row",
        change: function (e) {

            var selectedRows = this.select();
            var dataItem = this.dataItem(selectedRows[0]);
            $timeout(function () {
                $scope.currentItem = dataItem;
            });
        }
    };


    //查询
    $scope.search = function () {

        $scope.criteria = angular.copy($scope.searchCriteria);
        $scope.openLockGridOption.dataSource.read();
    };
    //解锁
    $scope.openLock = function () {
        if ($scope.currentItem) {
            configurationService.openLock($scope.currentItem).success(function (data) {
                if (data) {
                    csdToaster.info('解锁成功！');
                    $scope.search();
                    $scope.currentItem = null;
                } else {
                    csdToaster.info('解锁失败！');
                }
            });
        }
    };
    ; +(function init() {
        $scope.currentItem = null;
        $scope.criteria = {};
    })()
}
])
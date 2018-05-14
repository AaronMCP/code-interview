worklistModule.controller('ReportSelectUserController', ['$log', '$scope', '$modalInstance', 'constants', '$timeout',
    'selectReport', 'reportService', 'loginContext', '$translate', 'risDialog',
    function ($log, $scope, $modalInstance, constants, $timeout, selectReport, reportService, loginContext, $translate, risDialog) {
        'use strict';
        $log.debug('ReportSelectUserController.ctor()...');

        var select = function () {
            $modalInstance.close(selectReport);
        };

        var cancel = function () {
            $scope.selectReport.rejectToObject = "";
            $modalInstance.dismiss();
        };

        var treeDataSource = new kendo.data.HierarchicalDataSource({
            transport: {
                read: function (options) {
                    var id = options.data.id;
                    //get user or role
                    reportService.getAllUserByParentID(id).success(function(data) {
                        options.success(data);
                    });
                }
            },
            schema: {
                model: {
                    id: "id",
                    hasChildren: "hasChildren",
                    value: "id"
                }
            }
        });

        var treeSelect = function (e) {
            if (e.node.innerText.indexOf('(') == -1) {
                risDialog({
                    id: 'simpleDialog',
                    template:
                      '<div class="row-fluid">' +
                      ' <h4>' + $translate.instant("PleaseSelectUser") + '</h4>' +
                      ' <div>' +
                      '   <div class="codebox">' +
                      '   </div>\n' +
                      ' </div>\n' +
                      '</div>',
                    footerTemplate: '<button class="btn btn-primary" ng-click="$modalSuccess()">{{$modalSuccessLabel}}</button>',
                    title: $translate.instant("Tips"),
                    backdrop: true
                });
                return;
            }
            $scope.selectReport.rejectToObject = $(e.node).closest("li").data("uid");
        };

        (function initialize() {
            $log.debug('ReportSelectUserController.initialize()...');

            $scope.selectReport = selectReport;
            $scope.userList = null;
            $scope.select = select;
            $scope.cancel = cancel;
            $scope.treeDataSource = treeDataSource;
            $scope.treeSelect = treeSelect;

            $scope.selectReport.rejectToObject = $scope.selectReport.submitter;

            $scope.treeOptions = {
                dataSource: treeDataSource,
                dataTextField: 'name',
                dataValueField: 'id',
                select:treeSelect
            };
            
        }());
    }
]);



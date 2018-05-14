consultationModule.controller('UsersController', ['$log', '$scope', '$modal', '$translate', 'consultationService', 'loginUser', 'constants', 'kendoService',
    function ($log, $scope, $modal, $translate, consultationService, loginUser, constants, kendoService) {
        'use strict';
        $log.debug('UsersController.ctor()...');

        $scope._ = _;
        $scope.loginUser = loginUser;
        $scope.filter = {};
        $scope.gridOption = {
            dataSource: new kendo.data.DataSource({
                schema: {
                    data: "data",
                    total: "total"
                },
                serverPaging: true,
                transport: {
                    read: function (options) {
                        $scope.filter.pageIndex = options.data.page;
                        $scope.filter.pageSize = constants.pageSize;
                        $scope.filter.includeMobile = true;
                        consultationService.getUsers($scope.filter).success(function (data) {
                            options.success(data);
                        });
                    }
                },
                pageSize: constants.pageSize
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
            selectable: true,
            columnMenu: {
                columns: true,
                sortable: false,
                filterable: false
            },
            columns: [{
                title: ' ',
                template: '<span ng-class="{\'invisible\':$parent.disableEdit(dataItem)}" ng-click="$parent.editUser(dataItem)" style="margin-left: 10px;" class="icon-general icon-pencil line2Icon"></span>',
                width: 100,
                filterable: false,
                sortable: false,
                resizable: false,
                reorderable: false,
            },
            {
                field: 'loginName',
                title: '{{ "UserName" | translate }}',
                width: '15%',
            },
            {
                field: 'localName',
                title: '{{ "RealName" | translate }}',
                width: '15%',
            },
            {
                field: 'roles',
                title: '{{ "Role" | translate }}',
                template: "{{_.pluck(dataItem.roles, 'roleName').join('、');}}",
            },
            {
                field: 'hospital',
                title: '{{ "Organization" | translate }}',
                template: "{{dataItem.hospital.hospitalName}}",
                width: '20%',
            },
            {
                field: 'department',
                title: '{{ "Department" | translate }}',
                template: "{{dataItem.department.name}}",
                width: '20%',
            }]
        };

        kendoService.grid('.userListGrid').autoResize();
        $scope.$on("$destroy", function () {
            kendoService.destroy();
        });

        $scope.searchUser = function () {
            $scope.gridOption.dataSource.page(1);
        };

        $scope.editUser = function (user) {
            openEditWindow(angular.extend({}, user)); //shallow copy role.
        }

        $scope.disableEdit = function (user) {
            return user.uniqueID == constants.adminUserID || user.uniqueID == constants.gcRisUserID;
        }

        $scope.createNewUser = function () {
            openEditWindow();
        };

        function openEditWindow(user) {
            var modalInstance = $modal.open({
                templateUrl:'/app/consultation/settings/views/user-edit-window.html',
                controller: 'UserEditController',
                windowClass: 'overflow-hidden user-edit-window',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    userRelatedData: function () {
                        return {
                            roles: $scope.roles,
                            hospitals: $scope.hospitals,
                            departments: $scope.departments,
                        }
                    },
                    user: function () {
                        return user;
                    }
                }
            });

            modalInstance.result.then(function () {
                $scope.gridOption.dataSource.read();//refresh
            });
        }

        (function initialize() {
            consultationService.getRoles().success(function (data) {
                $scope.roles = data;
            });
            consultationService.getHospitals().success(function (data) {
                $scope.hospitals = _.filter(data, function (hospital) {
                    return hospital.status;
                });
            });
            consultationService.getDepartments().success(function (data) {
                $scope.departments = data;
            });
        })();
    }
]);
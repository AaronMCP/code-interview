consultationModule.controller('RolesController', ['$log', '$scope', '$modal', '$translate', '$state', 'consultationService', 'constants', 'kendoService', 'loginUser',
    function ($log, $scope, $modal, $translate, $state, consultationService, constants, kendoService, loginUser) {
        'use strict';
        $log.debug('RolesController.ctor()...');

        $scope.constants = constants;

        if (loginUser.isSiteAdmin) {
            $state.go('ris.consultation.users');
        }

        $scope.gridOption = {
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        consultationService.getRoles().success(function (data) {
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
                template: '<span ng-class="{\'invisible\':$parent.disableEdit(dataItem)}" ng-click="$parent.editRole(dataItem)" style="margin-left: 10px;" class="icon-general icon-pencil line2Icon"></span>',//kendo.template($('#column-menu-roles').html()),
                width: 100,
                filterable: false,
                sortable: false,
                resizable: false,
                reorderable: false,
            },
            {
                field: 'roleName',
                title: '{{ "RoleName" | translate }}',
                width: '30%',
            },
            {
                field: 'description',
                title: '{{ "Description" | translate }}',
            },
            {
                field: 'status',
                title: '{{ "Status" | translate }}',
                template: "{{ dataItem.status ? 'Active':'Inactive' | translate }}",
                width: '10%',
            },
            {
                field: 'lastEditTime',
                template: "{{ dataItem.lastEditTime | date:'yyyy/MM/dd, HH:mm:ss' }}",
                title: '{{ "LastEditTime" | translate }}',
                width: '20%',
            }]
        };

        kendoService.grid('.roleListGrid').autoResize();
        $scope.$on("$destroy", function () {
            kendoService.destroy();
        });

        $scope.disableEdit = function (role) {
            return role.uniqueID == constants.adminRoleID || role.uniqueID == constants.siteAdminRoleID;
        }

        $scope.editRole = function (role) {
            openEditWindow(angular.extend({}, role)); //shallow copy role.
        }

        $scope.createNewRole = function () {
            openEditWindow();
        };

        function openEditWindow(role) {
            var modalInstance = $modal.open({
                templateUrl:'/app/consultation/settings/views/role-edit-window.html',
                controller: 'RoleEditController',
                windowClass: 'overflow-hidden role-edit-window',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    role: function () {
                        return role;
                    }
                }
            });

            modalInstance.result.then(function () {
                $scope.gridOption.dataSource.read();//refresh
            });
        }
    }
]);
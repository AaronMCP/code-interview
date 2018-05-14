consultationModule.controller('RecipientConfigController', ['$log', '$scope', '$modal', '$translate', 'consultationService', 'loginUser', 'constants', 'kendoService',
    function ($log, $scope, $modal, $translate, consultationService, loginUser, constants, kendoService) {
        'use strict';
        $log.debug('RecipientConfigController.ctor()...');
        $scope.loginUser = loginUser;
        $scope.gridOption = {
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        consultationService.getRecipientConfigs().success(function (data) {
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
                template: kendo.template($('#column-menu-recipition').html()),
                width: 100,
                filterable: false,
                sortable: false,
                resizable: false,
                reorderable: false
            },
            {
                field: 'requestType',
                title: '{{ "RequestorType" | translate }}',
                template: "{{ dataItem.requestType | enumValueToString :'HospitalDefaultType' | translate }}",
                width: '20%'
            },
            {
                field: 'requestName',
                title: '{{ "RequestorName" | translate }}',
                width: '20%'
            },
            {
                field: 'responseType',
                title: '{{ "RecipientType" | translate }}',
                template: "{{ dataItem.responseType | enumValueToString :'HospitalDefaultType' | translate }}",
                width: '20%'
            },
            {
                field: 'responseName',
                title: '{{ "RecipientName" | translate }}',
                width: '20%'
            },
            {
                field: 'description',
                title: '{{ "Description" | translate }}',
                width: '20%'
            }]
        };

        kendoService.grid('.recipientListGrid').autoResize();
        $scope.$on("$destroy", function () {
            kendoService.destroy();
        });

        function openEditWindow(config) {
            var modalInstance = $modal.open({
                templateUrl:'/app/consultation/settings/views/recipient-config-edit-window.html',
                controller: 'RecipientConfigEditController',
                windowClass: 'overflow-hidden recipient-config-edit-window',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    config: function () {
                        return config;
                    },
                }
            });

            modalInstance.result.then(function () {
                $scope.gridOption.dataSource.read();//refresh
            });
        }

        $scope.editRecipientConfig = function (config) {
            openEditWindow(angular.extend({}, config)); //shallow copy hospital.
        }

        $scope.deleteRecipientConfig = function (config) {
            config.isDeleted = true;
            consultationService.saveRecipientConfig(config).success(function (data) {
                $scope.gridOption.dataSource.read();
            });
        }

        $scope.createRecipientConfig = function () {
            openEditWindow();
        };
    }
]);
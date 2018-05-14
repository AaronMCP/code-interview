consultationModule.controller('HospitalsController', ['$log', '$scope', '$modal', '$translate', 'consultationService', 'constants', 'kendoService', 'loginUser',
    function ($log, $scope, $modal, $translate, consultationService, constants, kendoService, loginUser) {
        "use strict";
        $log.debug('HospitalsController.ctor()...');

        $scope.loginUser = loginUser;
        $scope.gridOption = {
            dataSource: new kendo.data.DataSource({
                transport: {
                    read: function (options) {
                        consultationService.getHospitals().success(function (data) {
                            if (loginUser.isSiteAdmin) {
                                $scope.hospitals = _.filter(data, function (hospital) { return hospital.uniqueID === loginUser.user.hospitalID });
                            } else {
                                $scope.hospitals = data;
                            }
                            options.success($scope.hospitals);
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
                template: '<span ng-click="$parent.editHospital(dataItem)" style="margin-left: 10px;" class="line2Icon icon-general icon-pencil"></span>',
                width: 100,
                filterable: false,
                sortable: false,
                resizable: false,
                reorderable: false
            },
            {
                field: 'hospitalName',
                title: '{{ "OrganizationName" | translate }}',
                width: '15%'
            },
            {
                field: 'hospitalType',
                title: '{{ "Type" | translate }}',
                width: '15%'
            },
            {
                field: 'hospitalLevel',
                title: '{{ "Level" | translate }}',
                width: '15%'
            },
            {
                field: 'address',
                title: '{{ "Address" | translate }}',
                template: "{{dataItem.province}} {{dataItem.city ==dataItem.province ?'': dataItem.city}} {{dataItem.area}} {{dataItem.address}}"
            },
            {
                field: 'telePhone',
                title: '{{ "Phone" | translate }}',
                width: '15%'
            },
            {
                field: 'status',
                title: '{{ "Status" | translate }}',
                template: "{{ dataItem.status ? 'Active':'Inactive' | translate }}",
                width: '10%'
            }
            ]
        };

        kendoService.grid('.hospitalListGrid').autoResize();
        $scope.$on("$destroy", function () {
            kendoService.destroy();
        });

        function openEditWindow(hospital) {
            var modalInstance = $modal.open({
                templateUrl:'/app/consultation/settings/views/hospital-edit-window.html',
                controller: 'HospitalEditController',
                windowClass: 'overflow-hidden hospital-edit-window',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    hospital: function () {
                        return hospital;
                    },
                    hospitals: function () {
                        return $scope.hospitals;
                    }
                }
            });

            modalInstance.result.then(function () {
                $scope.gridOption.dataSource.read();//refresh
            });
        }

        $scope.editHospital = function (hospital) {
            openEditWindow(angular.extend({}, hospital)); //shallow copy hospital.
        }

        $scope.createNewHospital = function () {
            openEditWindow();
        };
    }
]);
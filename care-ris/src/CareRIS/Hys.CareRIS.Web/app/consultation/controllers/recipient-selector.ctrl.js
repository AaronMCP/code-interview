consultationModule.controller('RecipientSelectorController', ['$log', '$scope', 'constants', '$timeout',
    'loginContext', '$translate', 'risDialog', 'dictionaryManager', 'enums', 'application', 'consultationService', 'csdToaster',
    function ($log, $scope, constants, $timeout, loginContext, $translate, risDialog,
        dictionaryManager, enums, application, consultationService, csdToaster) {
        'use strict';
        $log.debug('RecipientSelectorController.ctor()...');

        $scope.selectHospital = function () {
            $scope.selectTab = 1;
        };

        $scope.selectExpert = function () {
            $scope.selectTab = 2;
        };
        $scope.onChangeSelectHospital = function () {
            var data = $scope.dshospitalList.view(),
                 selected = $.map($("#lvHospitalList").data('kendoListView').select(), function (item) {
                    return data[$(item).index()];
                });
            if (selected.length > 0) {
                $scope.selection = [{ type: 0, value: selected[0].uniqueID, text: selected[0].hospitalName }];
                $scope.clearSelection("#lvExpertList");
            }
            $scope.onSelected($scope.selection);
        };

       
        $scope.clickExpert = function (dataItem) {
            var data = $scope.dshospitalList.view(),
                selectedHops = $.map($("#lvHospitalList").data('kendoListView').select(), function (item) {
                return data[$(item).index()];
           });
           if (selectedHops.length > 0) {
               $scope.selection = _.reject($scope.selection, { type: 0 });
               $("#lvHospitalList").data('kendoListView').clearSelection();
           }
           var index = _.findIndex($scope.selection, { value: dataItem.uniqueID });
           if ($scope.mode === 'single') {
                if (index < 0) {
                    // remove other selection
                    $scope.clearSelection('#lvExpertList');
                    $scope.selection = [{
                        type: enums.consultantType.expert,
                        value: dataItem.uniqueID,
                        text: dataItem.localName,
                        hospitalName: dataItem.hospital.hospitalName
                    }];
                    $scope.selectItemInlistView(dataItem.uid, true);
                }
           } else {
               //mode:expertOnly/ multiple/ default
               var selected = false;
               if (index < 0) {
                   if ($scope.selection[0] && $scope.selection[0].hospitalID && $scope.selection[0].hospitalID !== dataItem.hospital.uniqueID) {
                       csdToaster.pop('warning', $translate.instant("CanSelectOnlyOneHospital").format($scope.selection[0].hospitalName), '');
                       return;
                   }
                   $scope.selection.push({
                       type: enums.consultantType.expert,
                       value: dataItem.uniqueID,
                       text: dataItem.localName,
                       hospitalName: dataItem.hospital.hospitalName,
                       hospitalID: dataItem.hospital.uniqueID
                   });
                   selected = true;
               } else {
                   // remove the selection item if the expert was selected
                   $scope.selection.splice(index, 1);
               }
               $scope.selectItemInlistView(dataItem.uid, selected);
           }
           $scope.onSelected($scope.selection);
        };

        $scope.$on('event:updateExpertItemStatus', function (event,items) {
             var dataItems = $("#lvExpertList").data("kendoListView").dataItems();
            _.each(items,function(item) {
                var dataItem = _.findWhere(dataItems, { uniqueID: item.value });
                if (dataItem && dataItem.uid) {
                    $scope.selectItemInlistView(dataItem.uid, item.selected);
                }
            });
        });

        $scope.searchHospital = function () {
            $scope.dshospitalList.page(1);
        };

        $scope.searchExpert = function () {
            $scope.dsexpertList.page(1);
        };

        $scope.prepareArea = function () {
            if ($scope.hoveredArea === null) {
                consultationService.getConsultArea().success(function (areaData) {
                    $scope.hoveredArea = areaData;
                });
            }
        };

        $scope.selectArea = function (province, city) {
            if ($scope.areaHospitalProvince !== province || $scope.areaHospitalCity !== city) {
                $scope.areaHospitalProvince = province;
                $scope.areaHospitalCity = city;
                $scope.searchArea = (province + city) || $translate.instant('SearchArea');;
                $scope.dshospitalList.page(1);
                $scope.dsexpertList.page(1);
            }
            $scope.hidePopover();
        };

        $scope.showArea = function () {
            $scope.prepareArea();
        };

        $scope.prepareDepartment = function () {
            if (!$scope.hoveredDepartments) {
                consultationService.getDepartments().success(function (data) {
                    $scope.hoveredDepartments = data; 
                });
            }
        };

        $scope.selectDepartment = function (dp) {
            if ($scope.departmentId !== dp.uniqueID) {
                $scope.departmentId = dp.uniqueID;
                $scope.filteredDepartment = dp.name;
                $scope.dsexpertList.page(1);
            }
            $scope.hidePopover();
        };

        $scope.showDepartment = function () {
            $scope.prepareDepartment();
        };

        //hospital
        $scope.prepareFilterHospital = function () {
            if (!$scope.hoveredHospitals) {
                consultationService.getHospitals(true).success(function (data) {
                    $scope.hoveredHospitals = data;
                });
            }
        };

        $scope.selectFilterHospital = function (hp) {
            if ($scope.hospitalID !== hp.uniqueID) {
                $scope.hospitalID = hp.uniqueID;
                $scope.filteredHospital = hp.hospitalName;
                $scope.dsexpertList.page(1);
            }
            $scope.hidePopover();
        };

        $scope.showFilterHospital = function () {
            $scope.prepareFilterHospital();
        };

        var initializeFilterBaseData = function () {
            $scope.prepareDepartment();
            $scope.prepareFilterHospital();
        };

        $scope.clearDep = function () {
            if ($scope.departmentId) {
                $scope.departmentId = '';
                $scope.dsexpertList.page(1);
            }
            $scope.hidePopover();
            $scope.filteredDepartment = $translate.instant("Department");
        };

        $scope.clearHop = function () {
            if ($scope.hospitalID) {
                $scope.hospitalID = '';
                $scope.dsexpertList.page(1);
            }
            $scope.hidePopover();
            $scope.filteredHospital = $translate.instant('Hospital');
        };

        var initDataSource = function() {
            $scope.dshospitalList = new kendo.data.DataSource({
                schema: {
                    data: "data",
                    total: "total"
                },
                serverPaging: true,

                transport: {
                    read: function (options) {
                        var searchCriteria = {
                            pageIndex: options.data.page,
                            pageSize: options.data.pageSize,
                            name: $scope.searchHospitalData,
                            provinceName: $scope.areaHospitalProvince,
                            cityName: $scope.areaHospitalCity
                        };
                        consultationService.getConsultHospitals(searchCriteria)
                            .success(function (data) {
                                angular.forEach(data.data, function (item) {
                                    if (item.hospitalImage) {
                                        item.hospitalImage = 'data:image/png;base64,' + item.hospitalImage;
                                    }
                                    else {
                                        item.hospitalImage = '/app-resources/images/consultation/user_portrait_default.png';
                                    }
                                }
                                );
                                options.success(data);
                            });
                    }
                },
                pageSize: $scope.pageSize || 6
            });


            $scope.dsexpertList = new kendo.data.DataSource({
                schema: {
                    data: "data",
                    total: "total"
                },
                serverPaging: true,

                transport: {
                    read: function (options) {
                        var searchCriteria = {
                            pageIndex: options.data.page,
                            pageSize: options.data.pageSize,
                            name: $scope.searchExpertData,
                            RoleID: constants.expertRoleId,
                            provinceName: $scope.areaHospitalProvince,
                            cityName: $scope.areaHospitalCity,
                            departmentID: $scope.departmentId,
                            hospitalID: $scope.hospitalID,
                            showAllUser: true,
                            isInCenter: 1
                        };

                        consultationService.getUsers(searchCriteria).success(function (data) {
                            angular.forEach(data.data, function (item) {
                                if (item.avatar) {
                                    item.avatar = 'data:image/png;base64,' + item.avatar;
                                }
                                else {
                                    item.avatar = '/app-resources/images/consultation/user_portrait_default.png';
                                }
                            }
                            );
                            options.success(data);
                        });
                    }
                },
                pageSize: $scope.pageSize || 6
            });
        };
        (function initialize() {
            $log.debug('SelectReceiverController.initialize()...');

            if (($scope.selection&&$scope.selection.length > 0 && $scope.selection[0].type === 1)) {
                $scope.selectTab =2;
            } else {
                $scope.selectTab = 1;

            }
            $scope.selection=$scope.selection||[];
            $scope.searchHospitalData = '';
            $scope.hoveredArea = null;
            $scope.areaHospitalProvince = '';
            $scope.areaHospitalCity = '';
            $scope.areaExpertProvince = '';
            $scope.areaExpertCity = '';
            $scope.filteredDepartment = $translate.instant("Department");
            $scope.filteredHospital = $translate.instant('Hospital');
            $scope.searchArea = $translate.instant('SearchArea');
            initializeFilterBaseData();
            if ($scope.mode === 'expertOnly') {
                consultationService.getUserHospital().success(function(hospital) {
                    $scope.hospitalID = hospital.uniqueID;
                    initDataSource();
                });
            } else {
                initDataSource();
            }
       
        }());
    }
]);
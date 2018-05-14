worklistModule.controller('PatientCasesController', ['$log', '$scope', '$state', 'loginContext', 'constants', 'enums', '$rootScope', 'consultationService', 'kendoService', '$stateParams', 'loginUser', 'requestService', 'openDialog', '$translate', '$timeout',
function ($log, $scope, $state, loginContext, constants, enums, $rootScope, consultationService, kendoService, $stateParams, loginUser, requestService, openDialog, $translate, $timeout) {
    'use strict';

    $log.debug('PatientCasesController.ctor()...');

    if ($stateParams.searchCriteria) {
        var searchCriteria = $stateParams.searchCriteria;
        $scope.searchVal = {};
        $scope.searchVal.patientName = searchCriteria.patientName ? searchCriteria.patientName : '';
        $scope.searchVal.patientNo = searchCriteria.patientNo ? searchCriteria.patientNo : '';
        $scope.searchVal.insuranceNumber = searchCriteria.insuranceNumber ? searchCriteria.insuranceNumber : '';
        $scope.searchVal.identityCard = searchCriteria.identityCard ? searchCriteria.identityCard : '';
    };

    $scope.$state = $state;
    $scope.user = {
        isJuniorDoctor: loginUser.isDoctor,
        isConsultationCenter: loginUser.isConsAdmin,
        isExpert: loginUser.isExpert
    };

    $scope.search = function () {
        var timestamp = Date.now();

        if (!$scope.searchVal ||
            (!$scope.searchVal.patientName && !$scope.searchVal.patientNo && !$scope.searchVal.insuranceNumber && !$scope.searchVal.identityCard)) {
            $state.go('ris.consultation.cases', {
                searchCriteria: null,
                timestamp: timestamp,
                reload: true
            });
        }
        var searchCriteria = {};
        searchCriteria.patientName = $scope.searchVal.patientName;
        searchCriteria.patientNo = $scope.searchVal.patientNo;
        searchCriteria.insuranceNumber = $scope.searchVal.insuranceNumber;
        searchCriteria.identityCard = $scope.searchVal.identityCard;

        $state.go('ris.consultation.cases', {
            searchCriteria: searchCriteria,
            timestamp: timestamp,
            highlightStatus: { cases: enums.patientCaseStatus.None },
            reload: true
        });
    };

    $scope.editCase = function (patientCaseID) {
        $state.go('ris.consultation.newpatientcase', { patientCaseID: patientCaseID, searchCriteria: $stateParams.searchCriteria });
    };

    $scope.applyRequest = function (patientCase) {
        $state.go('ris.consultation.apply', { patientCase: patientCase, searchCriteria: $stateParams.searchCriteria });
    };

    $scope.openDeletePatientCaseWindow = function (patientCaseId) {
        requestService.openDeletePatientCaseWindow(patientCaseId);
    };

    $scope.recoverPatientCase = function (patientCase) {
        requestService.recoverPatientCase(requestService.goType.recover, patientCase);
    };

    $scope.onColumnReorder = function (e) {
        $timeout(function () {
            createUserSetting();
        }, 10);
    };

    $scope.onColumnResize = function (e) {
        createUserSetting();
    };

    $scope.onColumnHide = function (e) {
        createUserSetting();
    };

    $scope.onColumnShow = function (e) {
        createUserSetting();
    };

    $scope.consulationCases = {
        dataSource: new kendo.data.DataSource({
            schema: {
                data: 'cases',
                total: 'count'
            },
            serverPaging: true,

            transport: {
                read: function (options) {
                    var searchCriteria = {
                        pageIndex: options.data.page,
                        pageSize: options.data.pageSize
                    };

                    //sort
                    onColumnSort();

                    if ($stateParams.searchCriteria) {
                        _.extend(searchCriteria, $stateParams.searchCriteria);
                    }

                    consultationService.getPatientCases(searchCriteria)
                        .success(function (data) {
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
        columnReorder: $scope.onColumnReorder,
        columnResize: $scope.onColumnResize,
        columnHide: $scope.onColumnHide,
        columnShow: $scope.onColumnShow,
        columnMenu: {
            columns: true,
            sortable: false,
            filterable: false
        },
        columns: [{
            title: '{{ "Edit" | translate }}',
            template: kendo.template($('#column-menu-cases').html()),
            width: 100,
            filterable: false,
            sortable: false,
            resizable: false,
            reorderable: false,
            menu: false
        }, {
            field: 'patientBaseInfo',
            title: '{{ "PatientBaseInfo" | translate }}',
            template: function (dataItem) {
                var result = dataItem.patientName + '&nbsp;&nbsp;&nbsp;&nbsp; ';

                if (dataItem.gender === '男') {
                    result += '<span class="icon-male icon-blue icon-gender-font">';
                } else if (dataItem.gender === '女') {
                    result += '<span class="icon-female icon-purple icon-gender-font">';
                } else {
                    result += '<span class="icon-unknown icon-orange icon-gender-font">';
                }
                result += '</span>&nbsp;&nbsp; ';

                var ageArry = dataItem.currentAge.split(' ');
                var age = ageArry[0];
                var ageUnit = ageArry[1];
                var a = _.findWhere($rootScope.ageUnitList, { value: ageUnit });
                if (a) {
                    ageUnit = a.text;
                }
                dataItem.currentAge = age + ' ' + ageUnit;
                result += dataItem.currentAge;
                return result;
            }
        }, {
            field: 'identityCard',
            title: '{{ "IdentityCard" | translate }}'
        }, {
            field: 'patientNo',
            title: '{{ "ConsultPatientNo" | translate }}'
        },{
            field: 'examIDs',
            title: '{{ "ExamNo" | translate }}'
        }, {
            field: 'createTime',
            title: '{{ "CreateTime" | translate }}',
            template: kendo.template($('#column-menu-createTime').html())
        }, {
            field: 'lastUpdateTime',
            title: '{{ "LastUpdateTime" | translate }}',
            template: kendo.template($('#column-menu-lastUpdateTime').html())
        }]
    };

    var onColumnSort = function () {
        if ($('#consulationCases').data("kendoGrid").dataSource._sort && $('#consulationCases').data("kendoGrid").dataSource._sort.length > 0) {
            //sort
            var columnSort = $('#consulationCases').data("kendoGrid").dataSource._sort[0].field + ',' + $('#consulationCases').data("kendoGrid").dataSource._sort[0].dir;
            if ($scope.userSettingSort != columnSort) {
                $scope.userSettingSort = columnSort;
                createUserSetting();
            }
        }
        else {
            //no sort
            if ($scope.userSettingSort != '') {
                $scope.userSettingSort = '';
                createUserSetting();
            }
        }
    };

    var createUserSetting = function () {
        var userSetting = {
            userSettingID: $scope.userSettingID,
            roleID: loginUser.user.defaultRoleID,
            userID: loginUser.user.uniqueID,
            type: enums.UserSettingType.ConsultationPatientCaseWorkList,
            settingValue: ''
        };

        var userSettingColumns = [];
        angular.forEach($('#consulationCases').data("kendoGrid").columns, function (column, index) {
            var userSettingColumn = {};
            userSettingColumn.field = column.field;
            if (column.hidden) {
                userSettingColumn.hidden = column.hidden;
            }
            else {
                userSettingColumn.hidden = false;
            }

            if (column.width) {
                userSettingColumn.width = column.width;
            }

            userSettingColumn.position = index;

            if ($('#consulationCases').data("kendoGrid").dataSource._sort && $('#consulationCases').data("kendoGrid").dataSource._sort.length > 0) {
                if (userSettingColumn.field == $('#consulationCases').data("kendoGrid").dataSource._sort[0].field) {
                    userSettingColumn.sort = $('#consulationCases').data("kendoGrid").dataSource._sort[0].dir;
                }
            }

            userSettingColumns.push(userSettingColumn);
        });

        userSetting.settingValue = JSON.stringify(userSettingColumns);
        consultationService.saveUserSetting(userSetting).success(function (data) {
        });
    };

    var pareseUserSettingValue = function (userSettingValue) {
        var userSettingValueObj = JSON.parse(userSettingValue);
        var columnSort = [];
        angular.forEach($scope.consulationCases.columns, function (column, index) {
            if (column.field) {
                var s = _.findWhere(userSettingValueObj, { field: column.field + '' });
                if (s) {
                    if (s.hidden) {
                        column.hidden = s.hidden;
                    }
                    if (s.width) {
                        column.width = s.width;
                    }

                    if (s.position) {
                        column.position = s.position;
                    }

                    if (s.sort) {
                        columnSort.push({ dir: s.sort, field: column.field });
                        $scope.userSettingSort = column.field + ',' + s.sort;
                    }
                }
                else {
                    column.position = 0;
                }
            }
            else {
                column.position = 0;
            }
        });
        if (columnSort.length > 0) {
            $scope.consulationCases.dataSource._sort = columnSort;
        }

        $scope.consulationCases.columns = _.sortBy($scope.consulationCases.columns, function (item) {
            return item.position;
        });
    };

    var kendoGridCommon = function () {
        kendoService.grid('.patient-cases-gird').autoResize();
        $scope.$on("$destroy", function () {
            kendoService.destroy();
        });
    };

    (function initialize() {
        $scope.userSettingID = '';
        $scope.isLoadUserSetting = false;
        $scope.userSettingSort = '';

        var userSettingQuery = { roleid: loginUser.user.defaultRoleID, userid: loginUser.user.uniqueID, type: enums.UserSettingType.ConsultationPatientCaseWorkList };
        consultationService.getUserSettingAsync(userSettingQuery).success(function (data) {
            if (data) {
                $scope.userSettingID = data.userSettingID;
                if (data.settingValue) {
                    pareseUserSettingValue(data.settingValue);
                }
            }
            $scope.isLoadUserSetting = true;
            kendoGridCommon();
        }).error(function (data, status, headers, config) {
            $scope.isLoadUserSetting = true;
            kendoGridCommon();
        });
    }());
}
]);
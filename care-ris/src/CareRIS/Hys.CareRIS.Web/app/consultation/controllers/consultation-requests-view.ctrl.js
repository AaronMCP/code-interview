worklistModule.controller('ConsultationRequestsController', ['$log', '$scope', '$state', 'constants', 'enums', '$rootScope', 'consultationService', '$filter', '$stateParams', 'dictionaryManager', 'kendoService', 'loginUser', 'requestService', '$modal', '$translate', 'openDialog', '$timeout',
function ($log, $scope, $state, constants, enums, $rootScope, consultationService, $filter, $stateParams, dictionaryManager, kendoService, loginUser, requestService, $modal, $translate, openDialog, $timeout) {
    'use strict';

    $log.debug('ConsultationRequestsController.ctor()...');

    $scope.viewAppliedRequest = function (requestId, status) {
        var routeNameSuffix;
        switch (status) {
            case enums.consultationRequestStatus.Applied:
                routeNameSuffix = 'applied';
                break;
            case enums.consultationRequestStatus.Accepted:
                routeNameSuffix = 'accepted';
                break;
            case enums.consultationRequestStatus.Completed:
                routeNameSuffix = 'completed';
                break;
            case enums.consultationRequestStatus.Cancelled:
                routeNameSuffix = 'cancelled';
                break;
            case enums.consultationRequestStatus.Rejected:
                routeNameSuffix = 'rejected';
                break;
            case enums.consultationRequestStatus.Reconsider:
                routeNameSuffix = 'reconsider';
                break;
            case enums.consultationRequestStatus.Terminate:
                routeNameSuffix = 'terminate';
                break;
            case enums.consultationRequestStatus.Consulting:
                routeNameSuffix = 'accepted';
                break;
            case enums.consultationRequestStatus.ApplyCancel:
                routeNameSuffix = 'applycancel';
                break;
            default:
                routeNameSuffix = 'default';
                break;
        }

        var routeName = 'ris.consultation.' + routeNameSuffix;
        $state.go(routeName, {
            requestId: requestId,
            searchCriteria: $stateParams.searchCriteria
        });
    };

    $scope.rejectRequest = function (requestId) {
        consultationService.getChangeReason(requestId).success(function (data) {
            requestService.rejectRequest({
                requestID: data.requestID,
                changeReasonType: data.changeReasonType,
                otherReason: data.otherReason,
                reasonType: enums.consultationDictionaryType.rejectReason,
                changeStatus: enums.consultationRequestStatus.Rejected
            });
        });
    };

    $scope.cancelApplication = function (requestId) {
        consultationService.getChangeReason(requestId).success(function (data) {
            requestService.cancelApplication({
                requestID: data.requestID,
                changeReasonType: data.changeReasonType,
                otherReason: data.otherReason,
                reasonType: enums.consultationDictionaryType.cancaleReason,
                changeStatus: enums.consultationRequestStatus.Cancelled
            });
        });
    };

    $scope.terminatRequest = function (requestId) {
        consultationService.getChangeReason(requestId).success(function (data) {
            requestService.terminatRequest({
                requestID: data.requestID,
                changeReasonType: data.changeReasonType,
                otherReason: data.otherReason,
                reasonType: enums.consultationDictionaryType.terminateReason,
                changeStatus: enums.consultationRequestStatus.Terminate
            });
        });
    };

    $scope.requestReconsideration = function (requestId) {
        consultationService.getChangeReason(requestId).success(function (data) {
            requestService.requestReconsideration({
                requestID: data.requestID,
                changeReasonType: data.changeReasonType,
                otherReason: data.otherReason,
                reasonType: enums.consultationDictionaryType.applyReconsiderReason,
                changeStatus: enums.consultationRequestStatus.Reconsider
            });
        });
    };

    $scope.ApplyCancelRequest = function (requestId) {
        consultationService.getChangeReason(requestId).success(function (data) {
            requestService.ApplyCancelRequest({
                requestID: data.requestID,
                changeReasonType: data.changeReasonType,
                otherReason: data.otherReason,
                reasonType: enums.consultationDictionaryType.applyCancelReason,
                changeStatus: enums.consultationRequestStatus.ApplyCancel
            });
        });
    };

    $scope.acceptRequest = function (requestId) {
        requestService.acceptRequest({ requestId: requestId });
    };

    $scope.applyRequest = function (pid) {
        requestService.applyRequest({ patientCaseID: pid });
    };

    $scope.printResults = function (requestId) {
        $scope.selectedRequestId = requestId;
        $scope.reportWin.open();
    };

    $scope.openDeleteRequestWindow = function (requestId) {
        requestService.openDeleteRequestWindow(requestId);
    };

    $scope.openRecoverRequestWindow = function (requestId) {
        consultationService.recoverRequest(requestId).success(function () {
            openDialog.openIconDialogOkFun(
                openDialog.NotifyMessageType.Success,
                $translate.instant('RecoverSuccess'),
                '', requestService.backToWorkList('ris.consultation.requests', true));
        });
    };

    $scope.startMeeting = function (requestId) {
        var modalInstance = $modal.open({
            templateUrl:'/app/consultation/views/request/window/request-meeting-window.html',
            controller: 'RequestMeetingController',
            backdrop: 'static',
            keyboard: false,
            resolve: {
                requestId: function () {
                    return requestId;
                }
            }
        });
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

    function getconsultationTimeInfo(dataItem) {
        var result = '';

        if (dataItem.consultationDate || dataItem.expectedDate) {
            var dateTime = dataItem.consultationDate ? dataItem.consultationDate : dataItem.expectedDate;
            var timeRage = dataItem.consultationStartTime ? dataItem.consultationStartTime : dataItem.expectedTimeRange;
            var overdueTitle = dataItem.isOverdue ? $translate.instant('OverDue') : '';
            result += '<table class="innerGrid" ng-class="{\'overdue-warning\':' + dataItem.isOverdue + '}" title=' + overdueTitle + '><tr><td>';
            var d = kendo.toString(kendo.parseDate(dateTime), 'yyyy/MM/dd dddd').split(' ');
            result += d[0];
            result += '</td>';
            result += '<td rowspan="2">';
            switch (timeRage) {
                case enums.ConsultationTimeRange.Morning:
                    result += '<span class="icon-general icon-morning icon-orange" title="' + $filter('translate')('Morning') + '"></span>';
                    break;
                case enums.ConsultationTimeRange.Afternoon:
                    result += '<span class="icon-general icon-afternoon icon-orange" title="' + $filter('translate')('Afternoon') + '"></span>';
                    break;
                case enums.ConsultationTimeRange.Night:
                    result += '<span class="icon-general icon-night icon-blue" title="' + $filter('translate')('Night') + '"></span>';
                    break;
                default:
                    break;
            }
            result += '</td>';
            result += '</tr><tr><td>' + $filter('translate')(d[1]) + '</td></tr></table>';
        }
        return result;
    };

    function getPatientBaseInfo(dataItem) {
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

    var juniorDoctorWl = {
        dataSource: new kendo.data.DataSource({
            schema: {
                data: 'requests',
                total: 'count'
            },
            serverPaging: true,

            transport: {
                read: function (options) {
                    var searchCriteria = {
                        pageIndex: options.data.page,
                        pageSize: options.data.pageSize,
                        searchType: $scope.searchType
                    };
                    //sort
                    onColumnSort();

                    if ($stateParams.searchCriteria) {
                        _.extend(searchCriteria, $stateParams.searchCriteria);
                        consultationService.getRequests(searchCriteria)
                            .success(function (data) {
                                options.success(data);
                            });
                    } else {

                        // search by default shortcut
                        consultationService.getShortcuts($scope.user.userId, $scope.shortcutCategory)
                            .success(function (shortcuts) {
                                if (shortcuts && shortcuts.length > 0) {
                                    var defaultShortcut = _.findWhere(shortcuts, { isDefault: true });
                                    if (defaultShortcut) {
                                        _.extend(searchCriteria, JSON.parse(defaultShortcut.value));
                                    }
                                }
                                // perform searhing anyway if no default shortcut
                                consultationService.getRequests(searchCriteria)
                                    .success(function (data) {
                                        options.success(data);
                                    });
                            });
                    }
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
        columns: [
        {
            template: kendo.template($('#column-menu-consultation').html()),
            filterable: false,
            sortable: false,
            resizable: false,
            reorderable: false,
            menu: false,
            title: '{{ "Edit" | translate }}',
            width: 70
        }, {
            field: 'status',
            title: '{{ "Status" | translate }}',
            template: kendo.template($('#column-menu-status').html()),
            width: 90
        }, {
            field: 'patientBaseInfo',
            title: '{{ "PatientBaseInfo" | translate }}',
            template: function (dataItem) {
                return getPatientBaseInfo(dataItem);
            }
        }, {
            field: 'identityCard',
            title: '{{ "IdentityCard" | translate }}',
            width: 180
        }, {
            field: 'patientNo',
            title: '{{ "ConsultPatientNo" | translate }}'
        }, {
            field: 'consultationTimeInfo',
            title: '{{ "ConsultationTimeInfo" | translate }}',
            encoded: false,
            template: function (dataItem) {
                return getconsultationTimeInfo(dataItem);
            }
        }, {
            field: 'receiver',
            title: '{{ "Receiver" | translate }}'
        }, {
            field: 'requestCreateDate',
            title: '{{ "RequestCreateDate" | translate }}',
            template: kendo.template($('#column-menu-applyTime').html())
        }, {
            field: 'statusUpdateTime',
            title: '{{ "StatusUpdateTime" | translate }}',
            template: kendo.template($('#column-menu-statusUpdateTime').html())
        }, {
            field: 'experts',
            title: '{{ "ConsultationExpert" | translate }}'
        }]
    };

    var consultationCenterWl = {
        dataSource: new kendo.data.DataSource({
            schema: {
                data: 'requests',
                total: 'count'
            },
            serverPaging: true,

            transport: {
                read: function (options) {
                    var searchCriteria = {
                        pageIndex: options.data.page,
                        pageSize: options.data.pageSize,
                        searchType: $scope.searchType
                    };

                    //sort
                    onColumnSort();

                    if ($stateParams.searchCriteria) {
                        _.extend(searchCriteria, $stateParams.searchCriteria);
                        consultationService.getSpecialistRequests(searchCriteria)
                             .success(function (data) {
                                 options.success(data);
                             });
                    } else {
                        // search by default shortcut
                        consultationService.getShortcuts($scope.user.userId, $scope.shortcutCategory)
                            .success(function (shortcuts) {
                                if (shortcuts && shortcuts.length > 0) {
                                    var defaultShortcut = _.findWhere(shortcuts, { isDefault: true });
                                    if (defaultShortcut) {
                                        _.extend(searchCriteria, JSON.parse(defaultShortcut.value));
                                    }
                                }
                                // perform searhing anyway if no default shortcut
                                consultationService.getSpecialistRequests(searchCriteria)
                                   .success(function (data) {
                                       options.success(data);
                                   });
                            });
                    }
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
            template: kendo.template($('#column-menu-consultation').html()),
            filterable: false,
            sortable: false,
            resizable: false,
            reorderable: false,
            menu: false,
            title: '{{ "Edit" | translate }}',
            width: 70
        }, {
            field: 'status',
            title: '{{ "Status" | translate }}',
            template: kendo.template($('#column-menu-status').html()),
            width: 90
        }, {
            field: 'patientBaseInfo',
            title: '{{ "PatientBaseInfo" | translate }}',
            template: function (dataItem) {
                return getPatientBaseInfo(dataItem);
            }
        }, {
            field: 'identityCard',
            title: '{{ "IdentityCard" | translate }}',
            width: 180
        }, {
            field: 'patientNo',
            title: '{{ "ConsultPatientNo" | translate }}'
        }, {
            field: 'consultationTimeInfo',
            title: '{{ "ConsultationTimeInfo" | translate }}',
            encoded: false,
            template: function (dataItem) {
                return getconsultationTimeInfo(dataItem);
            }
        }, {
            field: 'receiver',
            title: '{{ "Receiver" | translate }}'
        }, {
            field: 'requester',
            title: '{{ "Requester" | translate }}'
        }, {
            field: 'requesterHospital',
            title: '{{ "RequesterHospital" | translate }}'
        }, {
            field: 'requestCreateDate',
            title: '{{ "RequestCreateDate" | translate }}',
            template: kendo.template($('#column-menu-applyTime').html())
        }, {
            field: 'statusUpdateTime',
            title: '{{ "StatusUpdateTime" | translate }}',
            template: kendo.template($('#column-menu-statusUpdateTime').html())
        }, {
            field: 'experts',
            title: '{{ "ConsultationExpert" | translate }}'
        }]
    };

    var kendoGridCommon = function () {
        kendoService.grid('.consulation-gird').autoResize();
        $scope.$on("$destroy", function () {
            kendoService.destroy();
        });
    };

    var createUserSetting = function () {
        var userSetting = {
            userSettingID: $scope.userSettingID,
            roleID: loginUser.user.defaultRoleID,
            userID: loginUser.user.uniqueID,
            type: enums.UserSettingType.ConsultationRequestWorkList,
            settingValue: ''
        };

        var userSettingColumns = [];
        angular.forEach($('#consulationWorklist').data("kendoGrid").columns, function (column, index) {
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

            if ($('#consulationWorklist').data("kendoGrid").dataSource._sort && $('#consulationWorklist').data("kendoGrid").dataSource._sort.length > 0) {
                if (userSettingColumn.field == $('#consulationWorklist').data("kendoGrid").dataSource._sort[0].field) {
                    userSettingColumn.sort = $('#consulationWorklist').data("kendoGrid").dataSource._sort[0].dir;
                }
            }

            userSettingColumns.push(userSettingColumn);
        });

        userSetting.settingValue = JSON.stringify(userSettingColumns);
        consultationService.saveUserSetting(userSetting).success(function (data) {
        });
    };

    var onColumnSort = function () {
        if ($('#consulationWorklist').data("kendoGrid").dataSource._sort && $('#consulationWorklist').data("kendoGrid").dataSource._sort.length > 0) {
            //sort
            var columnSort = $('#consulationWorklist').data("kendoGrid").dataSource._sort[0].field + ',' + $('#consulationWorklist').data("kendoGrid").dataSource._sort[0].dir;
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

    var pareseUserSettingValue = function (userSettingValue) {
        var userSettingValueObj = JSON.parse(userSettingValue);
        var columnSort = [];
        angular.forEach($scope.consulationWorklist.columns, function (column, index) {
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
            $scope.consulationWorklist.dataSource._sort = columnSort;
        }

        $scope.consulationWorklist.columns = _.sortBy($scope.consulationWorklist.columns, function (item) {
            return item.position;
        });
    };

    (function initialize() {
        $scope.$state = $state;
        $scope.enums = enums;
        $scope.user = {
            isJuniorDoctor: loginUser.isDoctor,
            isConsultationCenter: loginUser.isConsAdmin,
            isExpert: loginUser.isExpert,
            userId: loginUser.user.uniqueID
        };
        $scope.isLoadUserSetting = false;
        $scope.userSettingID = '';
        $scope.userSettingSort = '';

        if ($scope.user.isJuniorDoctor) {
            $scope.shortcutCategory = enums.shortcutCategory.RequestSearchDoctor;
            $scope.searchType = enums.WlSearchType.Doctor;
        } else if ($scope.user.isConsultationCenter) {
            $scope.shortcutCategory = enums.shortcutCategory.RequestSearchCenter;
            $scope.searchType = enums.WlSearchType.Center;
        } else if ($scope.user.isExpert) {
            $scope.shortcutCategory = enums.shortcutCategory.RequestSearchExpert;
            $scope.searchType = enums.WlSearchType.Expert;
        } else {
            $scope.shortcutCategory = enums.shortcutCategory.All;
            $scope.searchType = enums.WlSearchType.All;
        }

        if (loginUser.isSiteAdmin || loginUser.isSuperAdmin) {
            $state.go('ris.consultation.roles');
            return;
        }

        if (loginUser.isDoctor) {
            $scope.consulationWorklist = juniorDoctorWl;

            $log.debug('isDoctor...');
        } else if (loginUser.isConsAdmin) {
            $scope.consulationWorklist = consultationCenterWl;

            $log.debug('isconsultationCenter...');
        } else if (loginUser.isExpert) {
            $scope.consulationWorklist = consultationCenterWl;

            $log.debug('isExpert...');
        } else {

            //  $state.go('ris.worklist.registrations');

        }

        var userSettingQuery = { roleid: loginUser.user.defaultRoleID, userid: loginUser.user.uniqueID, type: enums.UserSettingType.ConsultationRequestWorkList };
        consultationService.getUserSetting(userSettingQuery).success(function (data) {
            if (data) {
                $scope.userSettingID = data.userSettingID;
                if (data.settingValue) {
                    pareseUserSettingValue(data.settingValue);
                }
            } else {
                angular.forEach($scope.consulationWorklist.columns, function (column) {
                    if (column.field === 'identityCard') {
                        column.hidden = true;
                    }
                    if (column.field === 'receiver') {
                        column.hidden = true;
                    }
                    if (column.field === 'statusUpdateTime') {
                        column.hidden = true;
                    }
                    if (column.field === 'requester') {
                        column.hidden = true;
                    }
                });
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
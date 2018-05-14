worklistModule.controller('TimeSliceController', ['$scope', '$log', 'configurationService', 'loginContext', 'registrationService', '$q', '$filter', 'application', '$timeout',
function ($scope, $log, configurationService, loginContext, registrationService, $q, $filter, application, $timeout) {
    'use strict';
    var getArrayItemIgnoreCase = function (array, val) {
        var len = array.length, tmpVal;
        if (!val) return val;

        for (var i = 0; i < len; ++i) {
            tmpVal = array[i];
            if (String(tmpVal).toLowerCase() === String(val).toLowerCase()) {
                return tmpVal;
            }
        }
        return null;
    }
    var disableModality = ((application.clientConfig || {}).appointmentDisabledModalities || '').split('|') || [];
    var disableModalityType = ((application.clientConfig || {}).appointmentDisabledModalityTypes || '').split('|') || [];
    var setModality = function (callBack) {
        $scope.ready = false;

        registrationService.getBookingModalities($scope.selectedModalityType, loginContext.site).success(function (data) {
            var modalities = [];
            var oriSelectedModality = '';
            _.each(data || [], function (item) {
                if (disableModality.indexOf(item.uniqueID) < 0) {
                    modalities.push(item.modalityName);
                }
            });
            $scope.modalities = modalities;
            if ($scope.modalities.length > 0) {
                oriSelectedModality = getArrayItemIgnoreCase($scope.modalities, $scope.selectedModality || $scope.selectedModalityOri);
                $scope.selectedModality = oriSelectedModality || $scope.modalities[0];
            } else {
                $scope.selectedModality = null;
            }

            if ($scope.selectedModalityOri && !oriSelectedModality) {
                $scope.$emit('event:noMatchedModality');
            }
        }).finally(function () {
            getTimeSlice(function () {
                if (angular.isFunction(callBack)) {
                    callBack();
                } else {
                    $scope.ready = true;
                }
            });
        });
    };

    var getTimeSlice = function (callBack) {
        $scope.ready = false;
        var view = $scope.scheduler.view();
        registrationService.getModalityTimeSlice($scope.selectedModality || '',
            view.startDate(), view.endDate(), loginContext.site, loginContext.userId, loginContext.roleName).success(function (data) {
                var schedulerSource = [];
                var now = new Date();
                var year = now.getFullYear();
                var month = now.getMonth();
                var dayOfMonth = now.getDate();
                var dayBegin = new Date(year, month, dayOfMonth, 0, 0, 0);
                var dayEnd = new Date(year, month, dayOfMonth, 23, 59, 59);
                var periods = 43200000;//12hours

                var startMin = null;
                var endMax = null;

                angular.forEach(data, function (item, index) {
                    var start = new Date(item.startDt);
                    var end = new Date(item.endDt);
                    var initSelected = $scope.option.start !== null && ($scope.option.start - start) === 0
                        && $scope.option.end !== null && ($scope.option.end - end === 0);
                    var selected = $scope.timeSliceStart !== null && ($scope.timeSliceStart - start) === 0
                                            && $scope.timeSliceEnd !== null && ($scope.timeSliceEnd - end === 0);

                    if (startMin === null ||
                        startMin.getHours() > start.getHours() ||
                        (startMin.getHours() === start.getHours() && startMin.getMinutes() > start.getMinutes())) {
                        startMin = new Date(start.getTime());
                    }
                    if (endMax === null ||
                        endMax.getHours() < end.getHours() ||
                        (endMax.getHours() === end.getHours() && endMax.getMinutes() < end.getMinutes())) {
                        endMax = new Date(end.getTime());
                    }
                    var dateFilter = $filter('date');
                    var title = dateFilter(start, 'HH:mm') + '-' + dateFilter(end, 'HH:mm');
                    var totals = [item.totalPrivateQuota];
                    if (item.totalSharedQuota) totals.push(item.totalSharedQuota);

                    schedulerSource.push({
                        id: index,
                        title: title,
                        start: start,
                        end: end,
                        customData: {
                            timeSlice: JSON.stringify(item),
                            initSelected: initSelected,
                            selected: selected,
                            fulled: (item.totalSharedQuota <= 0 && item.totalPrivateQuota <= item.totalUsedQuota),
                            totalUsedQuota: item.totalUsedQuota,
                            totalQuota: totals.join('+')
                        }
                    });

                });

                if (startMin === null || endMax === null) {
                    startMin = new Date(year, month, dayOfMonth, 8, 0, 0);
                    endMax = new Date(year, month, dayOfMonth, 20, 0, 0);
                } else {
                    startMin = new Date(startMin.setFullYear(year, month, dayOfMonth));
                    endMax = new Date(endMax.setFullYear(year, month, dayOfMonth));
                }

                while ((startMin > dayBegin || endMax < dayEnd) && endMax - startMin < periods) {
                    startMin > dayBegin && (startMin = new Date(startMin.setHours(startMin.getHours() - 1)));
                    endMax < dayEnd && (endMax = new Date(endMax.setHours(endMax.getHours() + 1)));
                }

                startMin < dayBegin && (startMin = dayBegin);
                endMax > dayEnd && (endMax = dayEnd);

                startMin = new Date(startMin.setMinutes(0, 0));
                endMax = new Date(endMax.setMinutes(59, 59));

                var schedulerDatasource = new kendo.data.SchedulerDataSource({
                    data: schedulerSource
                });

                $scope.scheduler.setDataSource(schedulerDatasource);

                if (($scope.scheduler.options.startTime - startMin) !== 0 || ($scope.scheduler.options.endTime - endMax) !== 0) {
                    $scope.scheduler.setOptions({
                        startTime: startMin,
                        endTime: endMax
                    });
                    $scope.scheduler.view($scope.scheduler.viewName());
                    $scope.scheduler.refresh();
                }

            }).finally(function () {
                if (angular.isFunction(callBack)) {
                    callBack();
                } else {
                    $scope.ready = true;
                }
            });
    };

    var iniModality = function () {
        $scope.ready = false;
        configurationService.getModalityTypes().success(function (data) {
            var modalityTypes = data || [];
            var oriSelectedModalityType = '';

            _.each(disableModalityType, function (item) {
                var index = _.indexOf(modalityTypes, item);
                if (index > -1) {
                    modalityTypes.splice(index, 1);
                }
            });
            $scope.modalityTypes = modalityTypes;

            if ($scope.modalityTypes.length > 0) {
                oriSelectedModalityType = getArrayItemIgnoreCase($scope.modalityTypes, $scope.selectedModalityTypeOri);
                $scope.selectedModalityType = oriSelectedModalityType || $scope.modalityTypes[0];
            }

            if ($scope.selectedModalityTypeOri && !oriSelectedModalityType) {
                $scope.modalityTypes = [$scope.selectedModalityTypeOri];
                $scope.selectedModalityType = $scope.selectedModalityTypeOri;
            }
            setModality(function () {
                $scope.ready = true;
            });
        });
    };

    var modalityTypeChanged = function () {
        setModality();
    };
    var modalityChanged = function () {
        getTimeSlice();
    };
    ; (function initialize() {
        $scope.option = angular.extend({
            modalityTypeEnable: true,
            modalityEnable: true,
            modalityType: null,
            modality: null,
            start: null,
            end: null
        }, $scope.option);

        !$scope.option.modalityTypeEnable && ($scope.option.modalityTypeEnable = $scope.option.modalityTypeEnable !== false);
        !$scope.option.modalityEnable && ($scope.option.modalityEnable = $scope.option.modalityEnable !== false);

        $scope.option.start && ($scope.option.start = new Date($scope.option.start));
        $scope.option.start && ($scope.option.end = new Date($scope.option.end));

        $scope.option.getValue = function () {
            return $scope.timeSlice;
        };
        $scope.option.refresh = function () {
            getTimeSlice();
        };

        $scope.schedulerOptions = {
            date: $scope.option.start || new Date(),
            footer: false,
            allDaySlot: false,
            editable: false,
            height: '100%',
            startTime: new Date(2015, 9, 15, 0, 0, 0),
            endTime: new Date(2015, 9, 15, 23, 59, 59),
            navigate: function (e) {
                $timeout(function () {
                    getTimeSlice();
                }, 300);
            },
            views: [{
                type: 'day',
                selected: true
            }, {
                type: 'week'
            }]
        }

        $scope.ready = false;
        $scope.selectedModalityTypeOri = $scope.option.modalityType;
        $scope.selectedModalityOri = $scope.option.modality;
        $scope.selectedModalityType = $scope.option.modalityType;
        $scope.selectedModality = $scope.option.modality;
        $scope.modalityTypes = [];
        $scope.modalities = [];
        $scope.timeSliceStart = null;
        $scope.timeSliceEnd = null;
        $scope.timeSlice = null;
        $scope.modalityTypeChanged = modalityTypeChanged;
        $scope.modalityChanged = modalityChanged;

        iniModality();
    }());
}
]);
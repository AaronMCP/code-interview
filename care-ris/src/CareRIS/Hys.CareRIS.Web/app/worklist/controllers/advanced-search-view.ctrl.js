worklistModule.controller('AdvancedSearchController', ['$location', '$log', '$scope', '$modalInstance', 'constants', '$translate',
    '$timeout', 'loginContext', 'worklistService', 'risDialog', 'searchCriteria', 'patientTypeList', 'statusList',
    'modalityTypeList', 'modalityList', 'siteList', 'getCriteria', 'resetCriteria', 'setCriteria', 'shortcutAdded', 'openDialog', 'csdToaster', 'configurationService',
    function($location, $log, $scope, $modalInstance, constants, $translate,
        $timeout, loginContext, worklistService, risDialog, searchCriteria, patientTypeList, statusList,
        modalityTypeList, modalityList, siteList, getCriteria, resetCriteria, setCriteria, shortcutAdded, openDialog, csdToaster, configurationService) {
        'use strict';
        $log.debug('AdvancedSearchController.ctor()...');
        
        //没有advanced-search-view.js
        $scope.dateKeydown = function (e, attr) {
            var e = e || window.event;
            if (e.keyCode === 8) {
                $scope.searchCriteria[attr] = '';
            }
            return false;
        }
        $scope.dateClick = function (attr) {
            if (!$scope[attr].options.opened)                $scope[attr].open();
        }
        $scope.dateBlur = function (value, attr) {
            var str = /^[1-9]\d{3}\/([1-9]|1[0-2])\/([1-9]|[1-2][0-9]|3[0-1])\s+([0-9]|1[0-9]|2[0-3]):[0-5]\d$/;
            if (!str.test(value)) {
                $scope.searchCriteria[attr] = '';
            }
        }

        var cancel = function() {
            setCriteria(searchCriteria);
            $modalInstance.dismiss();
        };

        var search = function () {
            $scope.searchCriteria.createTimeType = $scope.searchCriteria.createTimeFlag === 1 ? 'range' : 'days';
            $scope.searchCriteria.examineTimeType = $scope.searchCriteria.examineTimeFlag === 1? 'range' : 'days';
            createTimeRangesToCriteria();
            examineTimeRangesToCriteria();
            setCriteria(searchCriteria);
            $modalInstance.close(searchCriteria);
        };

        $scope.clear = function() {
            $scope.dt = null;
        };

        var onModalityTypesChange = function($item, $model) {
            filterModailityByModalityTypes();
        };

        // Disable weekend selection
        $scope.disabled = function(date, mode) {
            return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
        };

        $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[1];

        $scope.kFormat = 'HH:mm';

        var getNewTimeRange = function() {
            return {
                start: null,
                stratStr: '',
                startMin: '0:00',
                startMax: '23:00',
                end: null,
                endStr: '',
                endMin: '1:00',
                endMax: '23:59'
            };
        };

        var addTimeRange = function(timeRanges, index) {
            timeRanges.push(getNewTimeRange());
        };

        var removeTimeRange = function(timeRanges, index) {
            timeRanges.splice(index, 1);

            if (timeRanges.length === 0) {
                timeRanges.push(getNewTimeRange());
            }
        };

        var startTimeChange = function(e, range) {
            if (range && range.stratStr) {
                range.endMin = range.stratStr;
                var startTime = new Date(range.start);
                startTime.setMinutes(startTime.getMinutes() + 60);
                range.end = startTime;
            }
        };

        // 添加创建时间筛选
        var createTimeRangesToCriteria = function() {
            if ($scope.createTimeRanges && $scope.createTimeRanges.length > 0) {
                searchCriteria.createTimeRanges = [];
                angular.forEach($scope.createTimeRanges, function(range) {
                    if (range.start || range.end) {
                        searchCriteria.createTimeRanges.push({
                            startTime: range.start,
                            endTime: range.end
                        });
                    }
                });
            }
        };

        // 添加检查时间筛选
        var examineTimeRangesToCriteria = function() {
            if ($scope.examineTimeRanges && $scope.examineTimeRanges.length > 0) {
                searchCriteria.examineTimeRanges = [];
                angular.forEach($scope.examineTimeRanges, function(range) {
                    if (range.start || range.end) {
                        searchCriteria.examineTimeRanges.push({
                            startTime: range.start,
                            endTime: range.end
                        });
                    }
                });
            }
        };

        var reset = function() {
            searchCriteria = getCriteria();
            $scope.searchCriteria = searchCriteria;
            $scope.searchCriteria.createTimeFlag = $scope.searchCriteria.createTimeType === 'range' ? 1 : 2;
            $scope.searchCriteria.examineTimeFlag = $scope.searchCriteria.examineTimeType === 'range' ? 1 : 2;
            $scope.createTimeRanges = searchCriteria.createTimeRangeOptions;
            $scope.examineTimeRanges = searchCriteria.examineTimeRangeOptions;
            resetCriteria();
        };

        var filterModailityByModalityTypes = function() {
            if ($scope.searchCriteria.modalityTypes && $scope.searchCriteria.modalityTypes.length > 0) {
                // if user selected some modality types, the modality type of modality options should only be those selected ones
                var list = _.filter(modalityList, function(modality) {
                    return _.contains($scope.searchCriteria.modalityTypes, modality.modalityType);
                });
                $scope.modalityList = _.sortBy(list, function(s) {
                    return s.modalityName;
                });

                // if user already selected some modalities but which type not in the selected modality types, remove them
                if ($scope.searchCriteria.modalities && $scope.searchCriteria.modalities.length > 0) {
                    for (var i = $scope.searchCriteria.modalities.length - 1; i > -1; i--) {
                        var item = $scope.searchCriteria.modalities[i];
                        var modality = _.findWhere(modalityList, {
                            modalityName: item
                        });
                        if (modality && !_.contains($scope.searchCriteria.modalityTypes, modality.modalityType)) {
                            $scope.searchCriteria.modalities.splice(i, 1);
                        }
                    }
                }
            } else {
                $scope.modalityList = modalityList;
            }
        };

        var clearShortcutName = function() {
            $scope.shortcut.name = searchCriteria.shortcutName ? searchCriteria.shortcutName : '';

            $timeout(function() {
                $scope.$broadcast('shortcutNameInputView:inputName');
                // above not work on a popover!!!
                $('#inputShortcutName').focus();
            }, 100);
        };
        var convertStatusesForSearch = function (criteria) {
            if (_.isArray(criteria.statuses) && criteria.statuses.length > 0 && _.isObject(criteria.statuses[0])) {
                var statuses = [];
                _.each(criteria.statuses, function (status) {
                    statuses.push(parseInt(status.value));
                })
                criteria.statuses = statuses;
            }
        };
        var addShortcut = function () {
            //convertStatusesForSearch(criteria);

            createTimeRangesToCriteria();
            examineTimeRangesToCriteria();
            searchCriteria.createTimeType = searchCriteria.createTimeFlag ===1?'range': 'days';
            searchCriteria.examineTimeType  = searchCriteria.examineTimeFlag===1?'range': 'days';
            var shortcut = {
                uniqueID: RIS.newGuid(),
                name: $scope.shortcut.name,
                owner: loginContext.userId,
                domain: loginContext.domain,
                criteria: _.clone(searchCriteria),
                ignoreNameDuplicated: false
            };

            convertStatusesForSearch(shortcut.criteria);

            worklistService.addShortcut(shortcut).success(function (data) {
                shortcutAdded(data);
                searchCriteria.shortcutName = shortcut.name;
                setCriteria(searchCriteria);
                $modalInstance.dismiss();
                csdToaster.info('快捷方式保存成功！');
            }).error(function (errorMsg) {
                if (errorMsg.name === 'DuplicateNameException') {
                    openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, $translate.instant('NameDuplicated'), $translate.instant('NameDuplicatedMsg'), function () {
                        shortcut.ignoreNameDuplicated = true;
                        worklistService.addShortcut(shortcut).success(function (data) {
                            shortcutAdded(data);
                            searchCriteria.shortcutName = shortcut.name;
                        });
                    });
                }
            }).finally(function () {
                $('#showShortcutNameButton').popover('hide');
            });
        };
        //ar span = document.createElement('')
        var span = $('.ui-select-match');
        var div = $('#inputPatientType');
        console.log(div);
        $('.ui-select-match').append('<span class="k-icon k-i-arrow-60-down select-icon"></span>');
        ;(function initialize() {
            $log.debug('AdvancedSearchController.initialize()...');

            $scope.patientTypeList = patientTypeList;
            $scope.statusList = statusList;
            $scope.modalityTypeList = modalityTypeList;
            $scope.modalityList = modalityList;
            $scope.siteList = siteList;
            $scope.onModalityTypesChange = onModalityTypesChange;

            $scope.searchCriteria = searchCriteria;
            $scope.searchCriteria.createTimeFlag = $scope.searchCriteria.createTimeType === 'range' ? 1 : 2;
            $scope.searchCriteria.examineTimeFlag = $scope.searchCriteria.examineTimeType === 'range' ? 1 : 2;
            $scope.search = search;
            $scope.cancel = cancel;
            $scope.siteList = siteList;
            $scope.createTimeRanges = searchCriteria.createTimeRangeOptions;
            $scope.examineTimeRanges = searchCriteria.examineTimeRangeOptions;
            $scope.addTimeRange = addTimeRange;
            $scope.removeTimeRange = removeTimeRange;
            $scope.startTimeChange = startTimeChange;

            $scope.reset = reset;

            if (searchCriteria.createTimeType === 'days') {
                searchCriteria.createStartDate = null;
                searchCriteria.createEndDate = null;
            }

            if (searchCriteria.examineTimeType === 'days') {
                searchCriteria.examineStartDate = null;
                searchCriteria.examineEndDate = null;
            }

            filterModailityByModalityTypes();

            $scope.shortcut = {
                name: ''
            };
            $scope.clearShortcutName = clearShortcutName;
            $scope.addShortcut = addShortcut;
        }());
    }
]);
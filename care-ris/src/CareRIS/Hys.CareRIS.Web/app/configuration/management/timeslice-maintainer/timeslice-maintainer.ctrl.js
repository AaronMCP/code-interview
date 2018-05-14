configurationModule.controller('TimesliceMaintainerController', [
    '$scope', 'constants', '$log', 'configurationService', 'csdToaster', 'openDialog', '$translate',
function ($scope, constants, $log, configurationService, csdToaster, openDialog, $translate) {
    'use strict';

    $log.debug('TimesliceMaintainerController.ctor()...');
    $scope.refreshModality = function () {
        $scope.searchOption.site = $scope.searchOption.site || '';
        $scope.modalityGetter($scope.searchOption);
    };
    $scope.dataTypeChanged = function () {
        var len = $scope.dateTypes.length;
        for (var i = 0; i < len; ++i) {
            if ($scope.dateTypes[i].checked) {
                return;
            }
        }

        $scope.dateTypes[0].checked = true;
    };
    $scope.timeRangeChanged = function () {
        if ($scope.timeslice.startDt && $scope.timeslice.endDt) {
            $scope.timeslice.description = $scope.timeslice.startDt + '-' + $scope.timeslice.endDt;
        } else {
            $scope.timeslice.description = '';
        }
    };

    $scope.clickCancel = function () {
        $scope.onCancel()
    };
    $scope.clickOk = function () {
        var dateTypes = [];
        _.each($scope.dateTypes, function (dateType) {
            if (dateType.checked) {
                dateTypes.push(dateType.id);
            }
        })
        var params = {
            modalityType: $scope.searchOption.modalityType,
            modality: $scope.timeslice.modality,
            startTime: $scope.timeslice.startDt,
            endTime: $scope.timeslice.endDt,
            description: $scope.timeslice.description,
            amount: $scope.timeslice.maxNumber,
            dateTypes: dateTypes,
            availableDate: $scope.timeslice.availableDate,
            interval: $scope.timeslice.interval
        };

        params.startTime = new Date(params.availableDate + ' ' + params.startTime);
        params.endTime = new Date(params.availableDate + ' ' + params.endTime);
        params.availableDate = new Date(params.availableDate);

        if (params.startTime >= params.endTime) {
            openDialog.openIconDialogOkFun(
                openDialog.NotifyMessageType.Warn,
                $translate.instant('Alert'),
                '起始时间必须小于结束时间！'
            );
            return;
        }

        if ($scope.isNew) {
            configurationService.addTimeslice(params).success(function () {
                csdToaster.info('新增成功.');
                $scope.onOk();
            }).error(function (error) {
                csdToaster.error('操作失败，请检查时间片时间的配置。')
                $log.error(error);
            });
        } else {
            configurationService.modifyTimeslice($scope.timeslice.uniqueID, params).success(function () {
                csdToaster.info('修改成功.');
                $scope.timeslice.availableDate = new Date($scope.timeslice.availableDate);
                $scope.timeslice.startDt = new Date('2000-01-01 ' + $scope.timeslice.startDt);
                $scope.timeslice.endDt = new Date('2000-01-01 ' + $scope.timeslice.endDt);
                for (var attr in $scope.editTimeslice) {
                    $scope.editTimeslice[attr] = $scope.timeslice[attr];
                }
                $scope.onOk($scope.timeslice);
            }).error(function (error) {
                csdToaster.error('操作失败，请检查时间片时间的配置。');
                $log.error(error);
            });
        }
    };

    +function init() {
        $scope.dateTypes = angular.copy(constants.dateTypes);
        $scope.today = new Date();
        $scope.isNew = $scope.editTimeslice ? false : true;

        if (!$scope.isNew) {
            $scope.timeslice = angular.copy($scope.editTimeslice);
            $scope.timeslice.availableDate = $scope.timeslice.availableDate.formatDateTime('yyyy/MM/dd');
            $scope.timeslice.startDt = $scope.timeslice.startDt.formatDateTime('HH:mm');
            $scope.timeslice.endDt = $scope.timeslice.endDt.formatDateTime('HH:mm');
            _.each($scope.dateTypes, function (item) {
                item.checked = item.id === $scope.timeslice.dateType;
            });
        } else {
            $scope.timeslice = {};
            _.each($scope.dateTypes, function (item) {
                item.checked = false;
            });
            $scope.dateTypes[0].checked = true;
        }
        $scope.modalityTypes || ($scope.modalityTypes = []);
        $scope.modalities = [];
        
        $scope.searchOption = {
            site: $scope.site,
            modalityType: $scope.modalityType,
            callback: function (modalities) {
                $scope.modalities = modalities;
                if (modalities.length > 0) {
                    if (modalities.indexOf($scope.modality) < 0) {
                        $scope.timeslice.modality = modalities[0];
                    } else {
                        $scope.timeslice.modality = $scope.modality;
                    }
                } else {
                    $scope.timeslice.modality = '';
                }
            }
        };

        $scope.refreshModality();
    }();
}
]);
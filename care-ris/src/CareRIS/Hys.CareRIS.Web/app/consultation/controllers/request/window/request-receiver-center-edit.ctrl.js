consultationModule.controller('RequestReceiverEditCenterController', ['$log', '$scope', 'consultationService', 'result', '$modalInstance', 'configurationService', 'openDialog', '$modal', 'enums', '$translate', 'constants','csdToaster',
    function ($log, $scope, consultationService, result, $modalInstance, configurationService, openDialog, $modal, enums, $translate, constants, csdToaster) {
        'use strict';
        $log.debug('RequestReceiverEditCenterController.ctor()...');

        $scope.close = function () {
            $modalInstance.close();
        };

        $scope.selectExpectedDate = function (event) {
            event.preventDefault();
            event.stopPropagation();
            $scope.selectExpectedDateOpened = true;
        };

        var setDefaultHost = function () {
            if ($scope.selectExpertData) {
                if ($scope.selectExpertData.length === 0) {
                    $scope.defaultID = '';
                    return;
                }
                var hasDefaultRole = _.some($scope.selectExpertData, function (expert) {
                    return expert.value === $scope.defaultID;
                });

                if (!hasDefaultRole) {
                    $scope.defaultID = $scope.selectExpertData.length > 0 ? $scope.selectExpertData[0].value : '';
                }
            }
            else {
                $scope.defaultID = '';
            }
        };

        $scope.setHost = function (id) {
            $scope.defaultID = id;
        };

        $scope.removeExpert = function (value) {
            var index = _.findIndex($scope.selectExpertData, { value: value });
            if (index > -1) {
                var items = [{ value: value, selected: false }];
                $scope.$broadcast('event:updateExpertItemStatus', items);
                $scope.selectExpertData.splice(index, 1);
            }
        };
        $scope.$watchCollection('selectExpertData', function () {
            setDefaultHost();
        });

        $scope.validateData = function () {
            if ($scope.selectExpertData.length == 0) {
                csdToaster.pop('info', $translate.instant("SelectExpert"), '');
                return false;
            }

            if (!$scope.applyRequestData.currentDate) {
                csdToaster.pop('info', $translate.instant("SelectConsultDate"), '');
                return false;
            }

            var currentTime = new Date();
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();

            if ($scope.applyRequestData.currentDate < new Date(year, month - 1, day)) {
                csdToaster.pop('info', $translate.instant("ConsultDateLessCurrent"), '');
                return false;
            }
            return true;
        };

        $scope.saveRequestReceive = function () {
            if ($scope.validateData()) {
                $scope.applyRequestData.defaultID = $scope.defaultID;
                $scope.applyRequestData.expertList = [];
                angular.forEach($scope.selectExpertData, function (item) {
                    $scope.applyRequestData.expertList.push(item.value);
                });

                $scope.applyRequestData.timeRange = $("#expectedTime").data('kendoDropDownList').value();
                $scope.applyRequestData.consultationDate = $scope.applyRequestData.currentDate;
                $scope.applyRequestData.consultationStartTime = $scope.applyRequestData.timeRange;

                consultationService.updateAcceptRequest($scope.applyRequestData).success(function () {
                    $modalInstance.close({
                        requestId: $scope.applyRequestData.requestId,
                        isExpected: $scope.applyRequestData.isExpected
                    });
                });
            }
        };

        (function initialize() {
            $scope.applyRequestData = {};
            $scope.applyRequestData = result;
            $scope.consultationTimeRange1 = [];
            $scope.consultationTimeRange2 = [];
            $scope.selectExpertData =[];
            $scope.defaultID = '';
            $scope.initHide = true;
                    consultationService.getConsultationAssigns($scope.applyRequestData.requestId).success(function (data) {
                        var host = _.findWhere(data, { isHost: 1 });
                        if (host) {
                            $scope.defaultID = host.uniqueID;
                        }
                        _.each(data, function (item) {
                            var selection = { text: item.displayName, value: item.uniqueID, type: enums.consultantType.expert };
                            $scope.selectExpertData.push(selection);
                        });
                    });


            consultationService.getDictionaryByType(enums.ConsultationDicType.ConsultationTimeRange).success(function (data) {
                angular.forEach(data, function (item) {
                    switch (item.value) {
                        case enums.ConsultationTimeRange.Morning:
                            item.imageurl = 'icon-general icon-morning icon-orange';
                            break;
                        case enums.ConsultationTimeRange.Afternoon:
                            item.imageurl = 'icon-general icon-afternoon icon-orange';
                            break;
                        case enums.ConsultationTimeRange.Night:
                            item.imageurl = 'icon-general icon-night icon-blue';
                            break;
                        default:
                            break;
                    }

                    if (item.value != enums.ConsultationTimeRange.Night) {
                        $scope.consultationTimeRange1.push(item);
                    }
                    $scope.consultationTimeRange2.push(item);
                });

                $scope.dsExpectedTime = new kendo.data.DataSource({
                    data: $scope.applyRequestData.serviceTypeID == enums.serviceType.normal ? $scope.consultationTimeRange1 : $scope.consultationTimeRange2
                });

                $("#expectedTime").kendoDropDownList({
                    dataTextField: "name",
                    dataValueField: "value",
                    valueTemplate: '<span class="selected-value #:data.imageurl#"  style="color:orange;"></span><span>#:data.name#</span>',
                    template: '<span class="k-state-default #:data.imageurl#" ></span>' +
                              '<span class="k-state-default" >#: data.name #</span>',
                    dataSource: $scope.dsExpectedTime,
                    value: $scope.applyRequestData.timeRange
                });
            });

        }());
    }
]);
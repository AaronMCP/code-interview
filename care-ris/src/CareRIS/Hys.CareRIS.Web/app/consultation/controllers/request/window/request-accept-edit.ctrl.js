worklistModule.controller('RequestAcceptEditController', ['$log', '$scope', '$modalInstance', 'constants', '$timeout',
    'loginContext', '$translate', 'risDialog', 'enums', 'application', 'openDialog', 'consultationService', 'changeReason', '$state', '$stateParams','csdToaster',
    function ($log, $scope, $modalInstance, constants, $timeout, loginContext, $translate, risDialog,
        enums, application, openDialog, consultationService, changeReason, $state, $stateParams, csdToaster) {
        'use strict';
        $log.debug('RequestAcceptEditController.ctor()...');

        $scope.setHost = function (id) {
            $scope.defaultID = id;
        };

        $scope.acceptRequest = function (form) {

            if (!form.$valid) {
                return;
            }

            if ($scope.selectExpertData.length == 0) {
                csdToaster.pop('info', $translate.instant("SelectExpert"), '');
                return;
            }

            if (!$scope.acceptData.consultationDate || $scope.acceptData.consultationDate == '') {
                csdToaster.pop('info', $translate.instant("SelectConsultDate"), '');
                return;
            }

            var currentTime = new Date();
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();

            if ($scope.acceptData.consultationDate < new Date(year, month - 1, day)) {
                csdToaster.pop('info', $translate.instant("ConsultDateLessCurrent"), '');
                return;
            }

            $scope.acceptData.defaultID = $scope.defaultID;

            $scope.acceptData.expertList = [];
            angular.forEach($scope.selectExpertData, function (item, index) {
                $scope.acceptData.expertList.push(item.value);
            });
            $scope.acceptData.requestID = changeReason.requestId;
            $scope.acceptData.consultationStartTime = $("#expectedTime").data('kendoDropDownList').value();

            consultationService.acceptRequest($scope.acceptData).success(function (data) {
                $modalInstance.dismiss();
                $state.go('ris.consultation.requests', { searchCriteria: $stateParams.searchCriteria, timestamp: Date.now(), reload: true });
            });
        };

        $scope.cancel = function () {
            $modalInstance.dismiss();

        };

        $scope.close = function () {
            $modalInstance.close();
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

        $scope.removeExpert = function(value) {
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
        (function initialize() {
            $log.debug('RequestAcceptEditController.initialize()...');
      
            $scope.selectExpertData = [];
            $scope.defaultID = '';
            $scope.expertList = [];
            $scope.meetingRooms = [];
            $scope.consultationTimeRange1 = [];
            $scope.consultationTimeRange2 = [];
            $scope.today = new Date();
            $scope.acceptData = { consultationDate: new Date(), consultationStartTime: '', consultationEndTime: '', description: '', meetingRoom: '' };
            $scope.initHide = true;
            

            consultationService.getDictionaryByType(enums.ConsultationDicType.ConsultationTimeRange).success(function (data) {
                //time
                angular.forEach(data, function (item) {
                    if (item.value == 1) {
                        item.imageurl = 'icon-general icon-morning icon-orange';
                    }
                    else if (item.value == 2) {
                        item.imageurl = 'icon-general icon-afternoon icon-orange';
                    }
                    else if (item.value == 3) {
                        item.imageurl = 'icon-general icon-night icon-blue';
                    }
                    //normal consultation, no night
                    if (item.value != 3) {
                        $scope.consultationTimeRange1.push(item);
                    }
                    $scope.consultationTimeRange2.push(item);
                }
                );
         
                consultationService.getInfoForAcceptRequest(changeReason.requestId).success(function (data) {
                    $scope.acceptData.receiver = data.receiver;
                    $scope.acceptData.serviceTypeID = data.serviceTypeID;
                    $scope.acceptData.serviceTypeName = data.serviceTypeName;
                    $scope.acceptData.consultationDate = new Date(data.expectedDate);
                    $scope.acceptData.consultationStartTime = data.expectedTimeRange;
                    $scope.selectExpertData = data.selections || [];
                    if (data.serviceTypeID == '1') {
                        $scope.dsExpectedTime = new kendo.data.DataSource({
                            data: $scope.consultationTimeRange1
                        });
                    }
                    else {
                        $scope.dsExpectedTime = new kendo.data.DataSource({
                            data: $scope.consultationTimeRange2
                        });
                    }

                    $("#expectedTime").kendoDropDownList({
                        dataTextField: "name",
                        dataValueField: "value",
                        valueTemplate: '<span class="selected-value #:data.imageurl#"></span><span>#:data.name#</span>',
                        template: '<span class="k-state-default #:data.imageurl#" ></span>' +
                                  '<span class="k-state-default" >#: data.name #</span>',
                        dataSource: $scope.dsExpectedTime
                    });

                    $("#expectedTime").data('kendoDropDownList').select(function (dataItem) {
                        return dataItem.value === data.expectedTimeRange;
                    });
                });
            });
        }());
    }
]);



consultationModule.controller('RequestReceiverEditController', ['$log', '$scope', 'consultationService', 'result', '$modalInstance', 'configurationService', 'openDialog', '$modal', 'enums', '$translate','csdToaster',
    function ($log, $scope, consultationService, result, $modalInstance, configurationService, openDialog, $modal, enums, $translate, csdToaster) {
        'use strict';
        $log.debug('RequestReceiverEditController.ctor()...');

        $scope.close = function () {
            $modalInstance.close();
        };

        $scope.selectExpectedDate = function (event) {
            event.preventDefault();
            event.stopPropagation();
            $scope.selectExpectedDateOpened = true;
        };

        $scope.onChangeConsultationType = function () {
            var data = $scope.dsConsultationType.view(),
                selected = $.map(this.select(), function (item) {
                    return data[$(item).index()];
                });

            $scope.applyRequestData.serviceTypeID = selected[0].uniqueID;

            $scope.dsExpectedTime = new kendo.data.DataSource({
                data: $scope.applyRequestData.serviceTypeID == enums.serviceType.normal ? $scope.consultationTimeRange1 : $scope.consultationTimeRange2
            });

            if ($("#expectedTime").data('kendoDropDownList') && $("#expectedTime").data('kendoDropDownList').setDataSource) {
                $("#expectedTime").data('kendoDropDownList').setDataSource($scope.dsExpectedTime);
                $("#expectedTime").data('kendoDropDownList').dataSource.read();
            }
        };

        $scope.onChangeSelectHospitalExpert = function () {
            var data = $scope.dsSelectHospitalExpert.view(),
                selected = $.map(this.select(), function (item) {
                    return data[$(item).index()];
                });

            if (selected.length > 0) {
                if (selected[0].responseType == 0) {
                    $scope.applyRequestData.consultantType = enums.consultantType.center;
                }
                else {
                    $scope.applyRequestData.consultantType = enums.consultantType.expert;
                }

                $scope.applyRequestData.receiverIDs = [selected[0].responseID];

                if ($scope.selectOtherData.isSelectedOther) {
                    $scope.selectOtherData.isSelectedOther = false;
                    $scope.$applyAsync();
                }
            }
        };

        var selectionData = [];
        $scope.selectOther = function () {
            var modalInstance = $modal.open({
                templateUrl:'/app/consultation/views/select-receiver-view.html',
                controller: 'SelectReceiverController',
                backdrop: 'static',
                keyboard: false,
                windowClass: 'selectReceiver',
                resolve: {
                    selection: function () {
                        return _.clone(selectionData);
                    }
                }
            });
            modalInstance.result.then(function (result) {
                if (result) {
                    selectionData = result;
                    $scope.selectedHospitalItem = $scope.selectedExpertItems = $scope.applyRequestData.receiverIDs = null;
                    var type = result.length > 0 && result[0].type;
                    $scope.selectOtherData.hasSelectedItem = true;
                    var validationResult = false;
                    if (selectionData.length === 1) {
                        validationResult = validateDefaultItem(selectionData[0].value);
                    }
                    if (!validationResult) {
                        //hospital
                        if (type === enums.consultantType.center) {
                            $scope.selectedHospitalItem = result[0];
                        } else if (type === enums.consultantType.expert) {
                            $scope.selectedExpertItems = result;
                        } else {
                            $scope.selectOtherData.hasSelectedItem = false;
                        }
                        $scope.selectOtherData.hasSelectedItem && $scope.selectOtherItem();
                    } else {// selected item is default item
                        $scope.selectOtherData.hasSelectedItem = false;
                        selectionData = [];
                    }
                }
            });
        };

        var validateDefaultItem = function (id) {
            var filter = '[id="' + id + '"]';
            var selectHospitalExpertLv = $("#lvSelectHospitalExpert").data('kendoListView');
            if (selectHospitalExpertLv.element.children().filter(filter).length > 0) {
                selectHospitalExpertLv.clearSelection();
                selectHospitalExpertLv.select(selectHospitalExpertLv.element.children().filter(filter).first());
                return true;
            } else {
                return false;
            }
        };

        $scope.selectOtherItem = function () {
            $scope.selectOtherData.isSelectedOther = true;
            if ($scope.selectedHospitalItem) {
                $scope.applyRequestData.receiverIDs = [$scope.selectedHospitalItem.value];
                $scope.applyRequestData.consultantType = enums.consultantType.center;
            } else if ($scope.selectedExpertItems) {
                $scope.applyRequestData.receiverIDs = _.pluck($scope.selectedExpertItems, 'value');
                $scope.applyRequestData.consultantType = enums.consultantType.expert;
            }
            if ($("#lvSelectHospitalExpert").data('kendoListView')) {
                $("#lvSelectHospitalExpert").data('kendoListView').clearSelection();
            }
        };

        var validateData = function () {
            if (!($scope.applyRequestData.receiverIDs && $scope.applyRequestData.receiverIDs.length > 0)) {
                csdToaster.pop('info', $translate.instant("SelectResponser"), '');
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
            if (validateData()) {
                $scope.applyRequestData.timeRange = $("#expectedTime").data('kendoDropDownList').value();
                if (!$scope.applyRequestData.isExpected) {
                    $scope.applyRequestData.consolutionDate = $scope.applyRequestData.currentDate;
                    $scope.applyRequestData.consolutionTimeRange = $scope.applyRequestData.timeRange;
                } else {
                    $scope.applyRequestData.expectedDate = $scope.applyRequestData.currentDate;
                    $scope.applyRequestData.expectedTimeRange = $scope.applyRequestData.timeRange;
                }

                consultationService.updateRequestReceive($scope.applyRequestData).success(function () {
                    $modalInstance.close(
                    {
                        requestId: $scope.applyRequestData.requestId,
                        isExpected: $scope.applyRequestData.isExpected
                    });
                });
            }
        };

        var initSelectedItem = function (selections) {
            if (selections && selections.length>0) {
                var type;
                if (selections[0]) {
                    type = selections[0].type;
                }
                $scope.selectOtherData.isSelectedOther = $scope.selectOtherData.hasSelectedItem = true;
                if (type === enums.consultantType.center) {
                    $scope.selectedHospitalItem = selectionData[0];
                } else if (type === enums.consultantType.expert) {
                    $scope.selectedExpertItems = selectionData;
                } else {
                    $scope.selectOtherData.isSelectedOther = $scope.selectOtherData.hasSelectedItem = false;
                }
            }
        };
        (function initialize() {
            $scope.applyRequestData = {};
            $scope.applyRequestData = result;
            $scope.applyRequestData.currentDate = new Date($scope.applyRequestData.currentDate);
            $scope.otherText = '';
            $scope.consultationTimeRange1 = [];
            $scope.consultationTimeRange2 = [];
            $scope.selectOtherData = {};
            consultationService.getServiceType().success(function (data) {
                $scope.dsConsultationType = new kendo.data.DataSource({
                    data: data
                });

                $("#lvConsultationType").kendoListView({
                    dataSource: $scope.dsConsultationType,
                    selectable: "true",
                    change: $scope.onChangeConsultationType,
                    template: kendo.template($("#templateConsultationType").html()),
                    value: $scope.applyRequestData.consultantType
                });

                var consultationTypeLv = $("#lvConsultationType").data('kendoListView');

                if ($scope.applyRequestData.serviceTypeID || $scope.applyRequestData.serviceTypeID == 0) {
                    var filter = '[id="' + $scope.applyRequestData.serviceTypeID + '"]';
                    consultationTypeLv.select(consultationTypeLv.element.children().filter(filter));
                } else {
                    consultationTypeLv.select(consultationTypeLv.element.children().first());
                }
            });

            consultationService.getRecipientConfigsReceiver().success(function (data) {
                $scope.dsSelectHospitalExpert = new kendo.data.DataSource({
                    data: data
                });

                $("#lvSelectHospitalExpert").kendoListView({
                    dataSource: $scope.dsSelectHospitalExpert,
                    selectable: "multiple",
                    change: $scope.onChangeSelectHospitalExpert,
                    template: kendo.template($("#templateSelectHospitalExpert").html())
                });

                var selectHospitalExpertLv = $("#lvSelectHospitalExpert").data('kendoListView');

                if ($scope.applyRequestData.consultantType || $scope.applyRequestData.consultantType == 0) {
                    //only one receiver item,maybe default receiver
                    if ($scope.applyRequestData.selections && $scope.applyRequestData.selections.length === 1) {
                        var filter = '[id="' + $scope.applyRequestData.selections[0].value + '"]';
                        if (selectHospitalExpertLv.element.children().filter(filter).length > 0) {
                            selectHospitalExpertLv.select(selectHospitalExpertLv.element.children().filter(filter).first());
                        } else {
                            selectionData = $scope.applyRequestData.selections;
                            initSelectedItem(selectionData);
                        }
                    }
                  else {// must be other receiver
                        selectionData = $scope.applyRequestData.selections;
                        initSelectedItem(selectionData);
                    }
                } else {
                    selectHospitalExpertLv.select(selectHospitalExpertLv.element.children().first());
                }
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
                    valueTemplate: '<span class="applyselectexpectdate-title selected-value #:data.imageurl#"  style="color:orange;"></span><span class="applyselectexpectdate-content">#:data.name#</span>',
                    template: '<span class="k-state-default #:data.imageurl#" ></span>' +
                              '<span class="k-state-default" >#: data.name #</span>',
                    dataSource: $scope.dsExpectedTime,
                    value: $scope.applyRequestData.timeRange
                });
            });

        }());
    }
]);
consultationModule.controller('RequestApplyController', ['$log', '$scope', 'consultationService', 'risDialog',
    'application', '$translate', 'loginContext', '$modal', '$state', '$stateParams', 'configurationService', 'openDialog',
    'enums','csdToaster',
    function ($log, $scope, consultationService, risDialog, application, $translate, 
        loginContext, $modal, $state, $stateParams, configurationService, openDialog, enums, csdToaster) {
    	'use strict';
    	$log.debug('RequestApplyController.ctor()...');

        var isReturnRis = false;
    	$scope.returnConsultWorkList = function (type) {
    	    if (type == 0) {
                if (isReturnRis) {
                    $state.go('ris.report');
                } else {
                    $state.go('ris.consultation.cases', {
                        searchCriteria: $stateParams.searchCriteria,
                        timestamp: Date.now(),
                        reload: true
                    });
                }
    	    }
    	    else if (type == 1) {
    	        $state.go('ris.consultation.requests');
    	    }
    	};

    	$scope.applyRequest = function () {
    	    $scope.applyRequestData.expectedTimeRange = $("#expectedTime").data('kendoDropDownList').value();
    	    if (validateData())
    	    {
    	        consultationService.createRequest($scope.applyRequestData).success(function (newData) {
    	            // if orderID is not null or empty,the data is from RIS
    	            if (isReturnRis) {
	                    $state.go('ris.report');
	                } else {
    	                $scope.returnConsultWorkList(1);
	                }
    	        });
    	    }
    	};

    	var validateData = function () {

    	    if (!$scope.applyRequestData.selectHospital && !($scope.applyRequestData.selectExperts && $scope.applyRequestData.selectExperts.length>0)) {
    	        csdToaster.pop('info', $translate.instant("SelectResponser"), '');
    	        return;
    	    }

    	    if (!$scope.applyRequestData.expectedDate || $scope.applyRequestData.expectedDate == '') {
    	        csdToaster.pop('info', $translate.instant("SelectConsultDate"), '');
    	        return;
    	    }

	        var currentTime = new Date();
	        var month = currentTime.getMonth() + 1;
	        var day = currentTime.getDate();
	        var year = currentTime.getFullYear();

    	    if ($scope.applyRequestData.expectedDate < new Date(year, month - 1, day)) {
    	        csdToaster.pop('info', $translate.instant("ConsultDateLessCurrent"), '');
    	        return;
    	    }

    	    var requestPurposeText = $.trim($("#requestPurpose").data("kendoEditor").body.innerText);
    	    if ($scope.applyRequestData.requestPurpose == null || $scope.applyRequestData.requestPurpose == ''
                || requestPurposeText == '') {
    	        openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('RequestPurpose') + $translate.instant('IsRequiredErrorMsg'),
                    function () {
                        $("#requestPurpose").data("kendoEditor").focus();
                    });
    	        return false;
    	    }

    	    var requestRequirementText = $.trim($("#requestPurpose").data("kendoEditor").body.innerText);
    	    if ($scope.applyRequestData.requestRequirement == null || $scope.applyRequestData.requestRequirement == ''
                || requestRequirementText == '') {
    	        openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant('RequestRequirement') + $translate.instant('IsRequiredErrorMsg'),
                    function () {
                        $("#requestRequirement").data("kendoEditor").focus();
                    });
    	        return false;
    	    }

    	    return true;
    	};
    	

    	$scope.selectExpectedDate  = function (event) {
    	    event.preventDefault();
    	    event.stopPropagation();
    	    $scope.selectExpectedDateOpened = true;
    	};

    	$scope.onChangeConsultationType = function () {
    	    var data = $scope.dsConsultationType.view(),
                selected = $.map(this.select(), function (item) {
                    return data[$(item).index()];
                });

    	    $scope.applyRequestData.consultationType = selected[0].uniqueID;
            //normal service
    	    if ($scope.applyRequestData.consultationType == '1')
    	    {
    	        //$("#expectedTime")
    	        $scope.dsExpectedTime = new kendo.data.DataSource({
    	            data: $scope.consultationTimeRange1
    	        });
            }
    	    else
    	    {
    	        $scope.dsExpectedTime = new kendo.data.DataSource({
    	            data: $scope.consultationTimeRange2
    	        });
    	    }
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
    	            $scope.applyRequestData.selectHospital = selected[0].responseID;
	                $scope.applyRequestData.selectExperts = null;
	            }
    	        else
    	        {
    	            $scope.applyRequestData.selectExperts = [selected[0].responseID];
	                $scope.applyRequestData.selectHospital = null;
    	        }
    	        if ($scope.selectOtherData.isSelectedOther) {
    	            $scope.selectOtherData.isSelectedOther = false;
	                $scope.$applyAsync();
	            }
    	    }
    	};
   

        var selection = [];
    	$scope.selectOther = function () {
    	    var modalInstance = $modal.open({
    	        templateUrl:'/app/consultation/views/select-receiver-view.html',
    	        controller: 'SelectReceiverController',
    	        backdrop: 'static',
    	        keyboard: false,
    	        windowClass: 'selectReceiver',
                resolve: {
                    selection:function() {
                        return _.clone(selection);
                    }
                }
    	    });
    	    modalInstance.result.then(function (result) {
    	        if (result) {
    	            selection = result;
    	            $scope.selectedHospitalItem = $scope.selectedExpertItems = $scope.applyRequestData.selectHospital = $scope.applyRequestData.selectExperts = null;
	                var type = result.length > 0 && result[0].type;
	                $scope.selectOtherData.hasSelectedItem = true;
	                var validationResult = false;
	                if (selection.length === 1) {
	                    validationResult= $scope.validateDefaultItem(selection[0].value);
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
	                } else {
	                    $scope.selectOtherData.hasSelectedItem = false;
	                    selection = [];
	                }
    	        }
    	    });
    	};

    	(function initialize() {
    	    $scope.patientCase = $stateParams.patientCase;
    	    isReturnRis =($stateParams.patientCase&&$stateParams.patientCase.isFromRis)||false;
    	    $scope.applyRequestData = { requestPurpose: '', requestRequirement: '', patientCaseID: $scope.patientCase.patientCaseID };
    	    $scope.otherText = '';
    	    $scope.applyRequestData.selectHospital = '';
    	    $scope.applyRequestData.selectExperts = null;
    	    $scope.applyRequestData.expectedDate = new Date();
    	    $scope.selectOtherData = {};
    	    $scope.consultationTimeRange1 = [];
    	    $scope.consultationTimeRange2 = [];

    	    //servicetype
    	    consultationService.getServiceType().success(function (data) {
    	        $scope.dsConsultationType = new kendo.data.DataSource({
    	            data: data
    	        });
    	        $("#lvConsultationType").kendoListView({
    	            dataSource: $scope.dsConsultationType,
    	            selectable: "multiple",
    	            change: $scope.onChangeConsultationType,
    	            template: kendo.template($("#templateConsultationType").html()),
    	        });
    	        $("#lvConsultationType").data('kendoListView').select($("#lvConsultationType").data('kendoListView').element.children().first());
    	    });

    	    consultationService.getRecipientConfigsReceiver().success(function (data) {
    	        $scope.dsSelectHospitalExpert = new kendo.data.DataSource({
    	            data: data
    	        });

    	        $("#lvSelectHospitalExpert").kendoListView({
    	            dataSource: $scope.dsSelectHospitalExpert,
    	            selectable: true,
    	            change: $scope.onChangeSelectHospitalExpert,
    	            template: kendo.template($("#templateSelectHospitalExpert").html())
    	        });

    	        if (data.length > 0) {
    	            $("#lvSelectHospitalExpert").data('kendoListView').select($("#lvSelectHospitalExpert").data('kendoListView').element.children().first());
    	            if (data[0].responseType == 0) {
    	                $scope.applyRequestData.selectHospital = data[0].responseID;
    	            }
    	            else {
    	                $scope.applyRequestData.selectExperts = [data[0].responseID];
    	            }
    	        }
    	    });
  	 
    	    consultationService.getDictionaryByType(enums.ConsultationDicType.ConsultationTimeRange).success(function (data) {
    	        //time
    	        angular.forEach(data, function (item) {
    	            if (item.value == 1)
    	            {
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

    	        $scope.dsExpectedTime = new kendo.data.DataSource({
    	            data: $scope.consultationTimeRange1
    	        });
    	        $("#expectedTime").kendoDropDownList({
    	            dataTextField: "name",
    	            dataValueField: "value",
    	            valueTemplate: '<span class="applyselectexpectdate-title  selected-value #:data.imageurl#"></span><span class="applyselectexpectdate-content">#:data.name#</span>',
    	            template: '<span class="k-state-default #:data.imageurl#" ></span>' +
                              '<span class="k-state-default" >#: data.name #</span>',
    	            dataSource: $scope.dsExpectedTime
    	        });
    	    });

     }());
    }
]);
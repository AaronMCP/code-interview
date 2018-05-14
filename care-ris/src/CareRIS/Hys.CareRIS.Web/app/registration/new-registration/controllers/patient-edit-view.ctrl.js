registrationModule.controller('PatientEditController', ['$log', '$scope',
    '$modalInstance', 'registrationService', 'loginContext', 'dictionaryManager', 'configurationService',
    'enums', '$translate', '$modal', '$anchorScroll', '$location', 'risDialog', '$filter', 'registrationUtil', 'patient', 'configurationData', 'order','$q', 'commonMessageHub','openDialog',
 function ($log, $scope, $modalInstance, registrationService, loginContext,
     dictionaryManager, configurationService, enums, $translate, $modal, $anchorScroll, $location,
     risDialog, $filter, registrationUtil, patient, configurationData, order, $q, commonMessageHub, openDialog) {
     'use strict';
     $log.debug('PatientEditController.ctor()...');


     var UpperFirstLetter;
     var SeparatePolicy;
     var Separator;
     var priorityList;
     var yearNumber, monthNumber, dayNumber;
     // get base data for registration
     //$scope.patient.PatientNo = getPatientNo();//init the patientNo when new registration window pop up
     var initializeBaseData = function () {
         $scope.order =_.clone(order);
         $scope.ageUnitList = configurationData.ageUnitList;
         $scope.genderList = configurationData.genderList;
         //$scope.priorityList = configurationData.priorityList;
         yearNumber = configurationData.yearNumber;
         monthNumber = configurationData.monthNumber;
         dayNumber = configurationData.dayNumber;
         UpperFirstLetter = configurationData.UpperFirstLetter;
         SeparatePolicy = configurationData.SeparatePolicy;
         Separator = configurationData.Separator;
         patient.birthday = new Date(patient.birthday);
         patient.birthday = patient.birthday = $filter('date')(patient.birthday, 'yyyy-MM-dd')
       
         if ($scope.order.currentAge) {
             var result = $scope.order.currentAge.split(' ')
            
             $scope.order.currentAge = result[0];
             var a = _.findWhere(configurationData.ageUnitList, {
                 text: result[1]
             });
             $scope.order.ageType =a? a.value:null;
         }

         $scope.patient = patient;
         if (!$scope.patient.gender && configurationData.genderList.length > 0) {
             $scope.patient.gender = configurationData.genderList[0].value
         }
         if (!$scope.order.ageType && configurationData.ageUnitList.length > 0) {
             $scope.order.ageType = configurationData.ageUnitList[0].value
         }
     };
     var save = function (form) {
         if (!form.$valid) {
             $scope.isShowErrorMsg = true;
             return;
         }
         if ($scope.patient.address && $scope.patient.address.length > 50) {
             openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '地址的长度不能超过50个字符！');
             return;
         }
         var patient = _.clone($scope.patient), order = {};
         patient.site = loginContext.site;
         patient.domain = loginContext.domain;
         patient.birthday = $filter('date')($scope.patient.birthday, 'yyyy-MM-dd HH:mm')
         //order info
         order.currentAge = $scope.order.currentAge + ' ' + $scope.order.ageType;
         order.uniqueID = $scope.order.uniqueID;
         var patientEdit = {patient:patient,orderID:order.uniqueID,currentAge:order.currentAge};
         registrationService.updatePatient(patientEdit).success(function (data) {
             var resultData = { patient: data, order: order };
             $modalInstance.close(resultData);

             //notify message
             var orderUpdateParams = new commonMessageHub.OrderUpdateParams();
             orderUpdateParams.uniqueID = $scope.order.uniqueID;
             commonMessageHub.publish(commonMessageHub.Messages.OrderUpdated, orderUpdateParams);
         })
         .error(function () {
             var title = $translate.instant("Tips");
             var content = $translate.instant("SaveErrorMsg");
             openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
             console.log("Save patient failed.");
         });

     };

     var setAge = function () {
         var result = registrationUtil.setAge(yearNumber, monthNumber, dayNumber, $scope.patient.birthday);
         var birthdayValid = result.errorMsg === '';
         handleTransformError(birthdayValid, true, result);
     };
     var setBirthday = function () {
         var value = $scope.order.currentAge;
         var strAgeUnit = $scope.order.ageType;
         if (!value) {
             return;
         }
         if (!strAgeUnit) {
             return;
         }
         var result = registrationUtil.setBirthday(strAgeUnit, value);
         var ageValid = result.errorMsg === '';
         handleTransformError(true, ageValid, result);
     };

     var handleTransformError = function (birthDayIsValid, ageIsValid, result) {
         $scope.editPatientForm.birthday.$valid = birthDayIsValid;
         $scope.editPatientForm.birthday.$invalid = !birthDayIsValid;
         $scope.editPatientForm.currentAge.$invalid = !ageIsValid;
         $scope.editPatientForm.currentAge.$valid = ageIsValid;
         $scope.editPatientForm.$valid = birthDayIsValid && ageIsValid;
         $scope.editPatientForm.$invalid = !$scope.editPatientForm.$valid;
         $scope.isShowTransformError = false;
         $scope.editPatientForm.birthday.transformAgeError = '';
         $scope.editPatientForm.currentAge.transformAgeError = '';
         if (!birthDayIsValid) {
             $scope.editPatientForm.birthday.$error.transform = true;
             $scope.editPatientForm.birthday.transformAgeError = result.errorMsg;
             $scope.isShowTransformError = true;
         }
         else {
             delete $scope.editPatientForm.birthday.$error.transform;
             $scope.order.ageType = result.ageType || $scope.order.ageType;
             $scope.order.currentAge = result.value || $scope.order.currentAge;
         }
         if (!ageIsValid) {
             $scope.isShowTransformError = true;
             $scope.editPatientForm.currentAge.$error.transform = true;
             $scope.editPatientForm.currentAge.transformAgeError = result.errorMsg;
         }
         else {
             delete $scope.editPatientForm.currentAge.$error.transform;
             $scope.patient.birthday = result.newDate || $scope.patient.birthday;
             $scope.patient.birthday = $filter('date')($scope.patient.birthday, 'yyyy-MM-dd');
         }
     };

     var ageTypeChage = function () {
         if ($scope.order.currentAge) {
             setBirthday();
         }
         else if ($scope.patient.birthday) {
             setAge();
         }
     };
     var cancel = function () {
         $modalInstance.dismiss();
     };
     var simplifiedToEnglish = function (patientName) {
         if (!patientName) {
             $scope.patient.englishName = '';
             return;
         }
         registrationUtil.simplifiedToEnglish(patientName, UpperFirstLetter, SeparatePolicy, Separator).success(function (data) {
             $scope.patient.englishName = data;
         });
     };
     $scope.selectBirthday = function (event) {
         event.preventDefault();
         event.stopPropagation();
         $scope.selectBirthdayOpened = true;
     };
     $scope.birthdayKey = function (e) {
         var e = e || window.event;
         if (e.keyCode === 8) {
             $scope.patient.birthday = '';
         }
         return false;
     };
     $scope.birthdayclick = function () {
         $scope.patientbirthdayPicker.open();
     };
     (function initialize() {
         $log.debug('PatientEditController.initialize()...');
         $scope.maxDate = new Date();
         initializeBaseData();
         $scope.isShowErrorMsg = false;
         $scope.isShowTransformError = false;
         $scope.save = save;
         $scope.cancel = cancel;
         $scope.ageTypeChage = ageTypeChage;
         $scope.setAge = setAge;
         $scope.setBirthday = setBirthday;
         $scope.simplifiedToEnglish = simplifiedToEnglish;
         $scope.dateOptions = {
             formatYear: 'yy',
             startingDay: 1
         };
     }());
 }
]);
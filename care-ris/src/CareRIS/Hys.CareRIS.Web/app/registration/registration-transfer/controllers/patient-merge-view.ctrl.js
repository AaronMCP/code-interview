registrationModule.controller('PatientMergeController', ['$log', '$scope', '$modalInstance', 'registrationService', '$translate',
    '$modal', 'existPatient', 'intergractionPatient',
 function ($log, $scope, $modalInstance, registrationService, $translate, $modal, existPatient, intergractionPatient) {
     'use strict';
     $log.debug('PatientMergeController.ctor()...');

     var contrastNames = ['localName', 'referenceNo', 'gender', 'currentAge', 'birthday', 'telephone', 'address'];
     var checkedNames = [];
     var cancel = function () {
         //to do
         $modalInstance.close(existPatient);
     };
     var merge = function () {
         _.each(checkedNames, function (name) {
             existPatient[name] = intergractionPatient[name];
             if (name === 'localName') {
                 existPatient.englishName = intergractionPatient.englishName;
             }
         });
         $modalInstance.close(existPatient);// return the update patient
     };

     var contrastPatient = function () {
         _.each(contrastNames, function (v) {
             if (intergractionPatient[v] !== existPatient[v]) {
                 $scope.diffNames.push(v);
                 $scope.mergeCheck(v,true);
             }
         })
     };

     $scope.mergeCheck = function (name, checked) {
         if (checked)
         {
             if (!_.contains(checkedNames, name)) {
                 checkedNames.push(name);
             }
         }
         else {
             var index=_.indexOf(checkedNames,name);
             checkedNames.splice(index,1);
         }
     };

     (function initialize() {
         $log.debug('PatientMergeController.initialize()...');
         $scope.diffNames = [];
         $scope.checkedNames = [];
         $scope.cancel = cancel;
         $scope.merge = merge;
         $scope.intergrationPatient = intergractionPatient;
         $scope.existPatient = existPatient;
         contrastPatient();
     }());
 }
]);
registrationModule.controller('PatientSimilarController', ['$log', '$scope', '$modalInstance', 'registrationService',
    'loginContext', 'enums', '$translate', '$modal',  '$location', 'risDialog', 'similarPatients',
 function ($log, $scope, $modalInstance, registrationService, loginContext, enums, $translate, $modal, $location,
     risDialog, similarPatients) {
     'use strict';
     $log.debug('PatientSimilarController.ctor()...');

     var createNewPatient = function () {
         $modalInstance.close(null);
     };
    
     var cancel = function () {
         $modalInstance.dismiss();
     };
     var selectSimilarPatient = function (patient) {
         $scope.selectedSimilarPatient = patient;
         _.each($scope.similarPatients, function (v) {
             v.isSelected = v === patient;
         });
     };

     $scope.ok = function () {
         $modalInstance.close($scope.selectedSimilarPatient);
     };

     (function initialize() {
         $log.debug('PatientSimilarController.initialize()...');
         $scope.similarPatients = similarPatients;
         $scope.createNewPatient = createNewPatient;
         $scope.cancel = cancel;
         $scope.selectSimilarPatient = selectSimilarPatient;
         $scope.selectedSimilarPatient = similarPatients[0];
         similarPatients[0].isSelected = true;
     }());
 }
]);
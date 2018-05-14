qualitycontrolModule.controller('QcEditPatientInfoController', [
'$scope', 'qualityService', 'application',
function ($scope, qualityService, application) {
    $scope.$on('event:changeInfo', function (e, args) {
        $scope.patient = args.patient;
        $scope.isPatient = args.isPatient;
    })
      
    //性别列表
    var configurationData = application.configuration;
    $scope.genderList = configurationData.genderList;

    ; +(function init() {
        $scope.patient = null;
        $scope.isPatient = false;
    })()
}])
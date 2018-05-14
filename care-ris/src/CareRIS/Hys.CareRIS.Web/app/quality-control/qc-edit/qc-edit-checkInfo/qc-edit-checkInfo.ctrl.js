qualitycontrolModule.controller('QcEditCheckInfoController', [
'$scope', 'qualityService', 'configurationService','loginContext','application',
function ($scope, qualityService, configurationService, loginContext, application) {

    $scope.$on('event:changeInfo', function (e, args) {
        $scope.isOrder = args.isOrder;
        $scope.order = args.order;
    })

    //// 申请部门列表
    //configurationService.getApplyDepts(loginContext.site).success(function (res) {
    //    $scope.applyDeptList = res;
    //});

    //// 申请医生列表
    //configurationService.getApplyDoctors(loginContext.site).success(function (res) {
    //    $scope.applyDoctorList = res;
    //})

    //病人、收费类型
    var configurationData = application.configuration;
    $scope.patientTypeList = configurationData.patientTypeList;
    $scope.chargeTypeList = configurationData.chargeTypeList;
    //
    $scope.applyDoctorList = configurationData.applyDoctorList;
    $scope.applyDeptList = configurationData.applyDeptList;
    ; +(function init() {
        $scope.order = null;
        $scope.isOrder = false;
    })()
}])
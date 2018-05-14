qualitycontrolModule.controller('QcEditCheckPartController', [
'$scope', 'qualityService', 'configurationService','dictionaryManager','enums',
function ($scope, qualityService, configurationService, dictionaryManager, enums) {

    $scope.$on('event:changeInfo', function (e, args) {
        $scope.isProcedure = args.isProcedure;
        $scope.procedure = args.procedure;
    });

    //胶片规格
    dictionaryManager.getDictionaries(enums.dictionaryTag.filmspecList).then(function (data) {
        $scope.filmspecList = data;
    });

    //用药方式
    dictionaryManager.getDictionaries(enums.dictionaryTag.medicineUsage).then(function (data) {
        $scope.medicineUsageList = data;
    });

    //体位
    dictionaryManager.getDictionaries(enums.dictionaryTag.position).then(function (data) {
        $scope.positionList = data;
    });

    //上机医生
    qualityService.getUserByRolenames(['HLRadiologist', 'MLRadiologist', 'LLRadiologist']).success(function (data) {
        $scope.doctorList = data;
    });

    //上机技师
    qualityService.getUserByRolenames(['Technician']).success(function (data) {
        $scope.technicianList = data;
    });

    //上机护士
    qualityService.getUserByRolenames(['Nurse']).success(function (data) {
        $scope.nurseList = data;
    });
    
    ; +(function init() {
        
        $scope.procedure = null;
        $scope.isProcedure = false;
    })()
}])
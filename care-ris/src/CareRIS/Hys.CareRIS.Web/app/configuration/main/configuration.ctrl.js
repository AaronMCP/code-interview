configurationModule.controller('ConfigurationCtrl', ['$scope', '$state', '$log','loginUser',
function ($scope, $state, $log, loginUser) {
    'use strict';
    $scope.showTab = function (tabName) {
        $scope.panel[tabName] = true;
    };
    +function init() {
        $scope.panel = {};
        $scope.loginUser = loginUser;
        if (loginUser.isAdmin || loginUser.isSiteAdmin) {
            $scope.showTab('management');
        } else {
            $scope.showTab('clientManage');
        }  
    }();
}
]);
consultationModule.directive('requestAcceptedView', ['$log', '$stateParams', '$compile', function ($log, $stateParams, $compile) {
    $log.debug('requestAccetpedView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/request-accepted-view.html',
        controller: 'RequestAcceptedController',
        replace: true,
        scope: {
        },
        link: function ($scope, $http, $attrs) {

            $scope.activeReportHostWin = function (e) {
                var ae = angular.element(e.sender.element);
                var tpl = '<report-advice-edit-window val="report" close="closeHostWin(result)"></report-advice-edit-window>';
                ae.html(tpl);
                $compile(ae.contents())($scope);
            };
            $scope.deactiveReportHostWin = function (e) {
                var ae = angular.element(e.sender.element);
                ae.html('');
            };
            $scope.closeHostWin = function (result) {
                $scope.reportHostWin.close();
            };

            $scope.activeReportExpertWin = function (e) {
                var ae = angular.element(e.sender.element);
                var tpl = '<report-expert-advice-edit-window val="report" close="closeExpertWin(result)"></report-expert-advice-edit-window>';
                ae.html(tpl);
                $compile(ae.contents())($scope);
            };
            $scope.deactiveReportExpertWin = function (e) {
                var ae = angular.element(e.sender.element);
                ae.html('');
            };
            $scope.closeExpertWin = function (result) {
                $scope.reportExpertWin.close();
            };
        }
    };
}]);

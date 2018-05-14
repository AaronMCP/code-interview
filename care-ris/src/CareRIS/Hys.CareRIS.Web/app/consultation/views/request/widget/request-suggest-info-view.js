consultationModule.directive('requestSuggestInfoView', ['$log', '$stateParams', '$compile', function ($log, $stateParams, $compile) {
    $log.debug('requestSuggestInfoView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/widget/request-suggest-info-view.html',
        controller: 'RequestSuggestInfoController',
        replace: true,
        scope: false,
        link: function ($scope, $http, $attrs) {
            $scope.activeReportHostWin = function (e) {
                var ae = angular.element(e.sender.element);
                var tpl = '<report-advice-edit-window val="advice" close="closeWin(result)"></report-advice-edit-window>';
                ae.html(tpl);
                $compile(ae.contents())($scope);
            };
            $scope.deactiveReportHostWin = function (e) {
                var ae = angular.element(e.sender.element);
                ae.html('');
            };

            $scope.closeWin = function (result) {
                $scope.reportHostWin.close();
                if (result)
                {
                    $scope.reload(result);
                }
            };
        }
    };
}]);

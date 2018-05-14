consultationModule.directive('requestCompletedView', ['$log', '$compile', '$stateParams', function ($log, $compile, $stateParams) {
    $log.debug('requestCompletedView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request/request-completed-view.html',
        controller: 'RequestCompletedController',
        replace: true,
        scope: false,
        link: function ($scope) {
            $scope.activeReportPrintWin = function (e) {
                var ae = angular.element(e.sender.element);
                var tpl = '<report-print-view rid="' + $stateParams.requestId + '"></report-print-view>';
                ae.html(tpl);
                $compile(ae.contents())($scope);
            };
            $scope.deactiveReportPrintWin = function (e) {
                var ae = angular.element(e.sender.element);
                ae.html('');
            };
        }
    };
}]);

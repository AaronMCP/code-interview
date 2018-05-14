consultationModule.directive('consultationRequestsView', ['$log', function ($log) {
    $log.debug('consultationRequestsView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/consultation-requests-view.html',
        controller: 'ConsultationRequestsController',
        replace: true,
        scope: {
        },
        link: function ($scope, element) {
            $scope.selectedRequestId = '';
            $scope.activeReportWin = function (e) {
                var ae = angular.element(e.sender.element);
                var tpl = '<report-print-view rid="' + $scope.selectedRequestId + '"></report-print-view>';
                ae.html(tpl);
                $compile(ae.contents())($scope);
            };
            $scope.deactiveReportWin = function (e) {
                var ae = angular.element(e.sender.element);
                ae.html('');
            };
        }
    };
}]);

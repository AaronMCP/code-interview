referralModule.directive('referralView', ['$log', 'application', function ($log, application) {
    $log.debug('referralView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/referral/views/shared/referral-view.html',
        controller: 'ReferralController',
        replace: true,
        scope: {
        },
        link: function (scope, element) {
            // handlke kendo splitter size change
            scope.onResize = function () {
                scope.$broadcast(application.referralSidePanelResize);
            };
        }
    };
}]);

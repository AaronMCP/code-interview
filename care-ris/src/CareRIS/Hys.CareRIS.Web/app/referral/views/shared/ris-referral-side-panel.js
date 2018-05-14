referralModule.directive('risReferralSidePanel', ['$log', function ($log) {
    $log.debug('risReferralSidePanel.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/referral/views/shared/ris-referral-side-panel.html',
        controller: 'RisReferralSidePanelController',
        replace: true,
        scope: true,
        link: function (scope, element) {
        }
    };
}]);

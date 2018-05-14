referralModule.directive('risReferralsView', ['$log', function ($log) {
    $log.debug('risReferralView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/referral/views/ris-referrals-view.html',
        controller: 'RisReferralsController',
        replace: true,
        scope: {
        },
        link: function (scope, element) {
        }
    };
}]);

referralModule.controller('RisReferralSidePanelController', ['$log', '$scope', '$state', 'enums', 'loginUser', 'consultationService', 'referralSidePanel', '$stateParams',
	function ($log, $scope, $state, enums, loginUser, consultationService, referralSidePanel, $stateParams) {
	    'use strict';

	    $log.debug('RisReferralSidePanelController.ctor()...');

	    var statusFilterContext = (function () {
	        var filter = function (status) {
	            var timestamp = Date.now();
	            var searchCriteria = null;

	            if (status == enums.ReferralStatus.All) {
	                searchCriteria = {
	                    statusList: []
	                };
	                $scope.highlightList.all = true;
	                $scope.highlightList.unsend = false;
	            } else if (status == enums.ReferralStatus.Unsend) {
	                searchCriteria = {
	                    statusList: [enums.ReferralStatus.Canceled, enums.ReferralStatus.Rejected, enums.ReferralStatus.SentFailed]
	                };
	                $scope.highlightList.unsend = true;
	                $scope.highlightList.all = false;
	            } else {
	                searchCriteria = {
	                    statusList: [status]
	                };
	            }

	            $state.go('ris.referral.referrals', {
	                searchCriteria: searchCriteria,
	                timestamp: timestamp,
	                reload: true
	            });
	        };

	        return {
	            status: enums.ReferralStatus,
	            filter: filter
	        };
	    }());

	    (function () {
	        $scope.$state = $state;
	        $scope.sidePanel = referralSidePanel;
	        $scope.statusFilterContext = statusFilterContext;
	    }());
	}
]);
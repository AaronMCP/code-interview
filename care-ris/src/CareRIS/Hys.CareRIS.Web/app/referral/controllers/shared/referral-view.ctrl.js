referralModule.controller('ReferralController', ['$log', '$scope', '$state', 'referralSidePanel',
	function ($log, $scope, $state, referralSidePanel) {
	    'use strict';

	    $log.debug('ReferralController.ctor()...');

	    $scope.$state = $state;
	    $scope.sidePanel = referralSidePanel;

	    $scope.highlightList = {
	        all: true,
	        unsend: false
	    };
	}
]);
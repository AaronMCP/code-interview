consultationModule.controller('ConsultationSettingsSidePanel', ['$log', '$scope', '$state', 'enums', 'loginUser',
	function ($log, $scope, $state,enums, loginUser) {
	    'use strict';
	    $log.debug('ConsultationSettingsSidePanel.ctor()...');
	    $scope.enums = enums;
	    $scope.loginUser = loginUser;
	    if (!loginUser.isSiteAdmin && !loginUser.isSuperAdmin) {
	        $state.go('ris.consultation.requests');
	    }
	}
]);
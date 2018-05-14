registrationModule.controller('TransferRejectReasonController', ['$log', '$scope', '$modalInstance', 'registrationService',
    'loginContext', 'enums', '$translate', '$modal', '$location', 'risDialog',
 function ($log, $scope, $modalInstance, registrationService, loginContext, enums, $translate, $modal, $location,risDialog) {
 	'use strict';
 	$log.debug('TransferRejectReasonController.ctor()...');

 	var cancel = function () {
 		$modalInstance.dismiss();
 	};

 	$scope.ok = function () {
 	    $modalInstance.close($scope.reason);
 	};

 	(function initialize() {
 		$log.debug('TransferRejectReasonController.initialize()...');
 		$scope.cancel = cancel;
 	}());
 }
]);
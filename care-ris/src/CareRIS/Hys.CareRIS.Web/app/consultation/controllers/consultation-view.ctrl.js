consultationModule.controller('ConsultationController', ['$log', '$scope', '$state', 'consultationSidePanel', 'enums',
	function ($log, $scope, $state, consultationSidePanel, enums) {
	    'use strict';

	    $log.debug('ConsultationController.ctor()...');

	    $scope.$state = $state;
	    $scope.sidePanel = consultationSidePanel;
	    $scope.caseHighlight = [
          { status: enums.patientCaseStatus.All, active: true },
          { status: enums.patientCaseStatus.NotApply, active: false },
          { status: enums.patientCaseStatus.Applied, active: false },
          { status: enums.patientCaseStatus.Deleted, active: false },
          { status: enums.patientCaseStatus.DicomList, active: false }
	    ];
	    $scope.requestHighlight = [
         { status: enums.consultationRequestStatus.All, active: true },
         { status: enums.consultationRequestStatus.Applied, active: false },
         { status: enums.consultationRequestStatus.Accepted, active: false },
         { status: enums.consultationRequestStatus.Completed, active: false },
         { status: enums.consultationRequestStatus.Cancelled, active: false },
         { status: enums.consultationRequestStatus.Rejected, active: false },
         { status: enums.consultationRequestStatus.Consulting, active: false },
         { status: enums.consultationRequestStatus.Reconsider, active: false },
         { status: enums.consultationRequestStatus.Terminate, active: false },
         { status: enums.consultationRequestStatus.Delete, active: false },
         { status: enums.consultationRequestStatus.ApplyCancel, active: false }
	    ];
	}
]);
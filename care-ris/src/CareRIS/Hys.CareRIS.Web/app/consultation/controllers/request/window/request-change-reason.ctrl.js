consultationModule.controller('RequestChangeReasonController', ['$log', '$scope', 'consultationService', 'changeReason', '$modalInstance', 'enums', '$state', 'loginUser',
function ($log, $scope, consultationService, changeReason, $modalInstance, enums, $state, loginUser) {
    'use strict';
    $log.debug('RequestChangeReasonController.ctor()...');

    $scope.updateRequestChangeReason = function () {
        $scope.reason.lastEditUser = $scope.userId;
        $scope.reason.status = $scope.reason.changeStatus;
        $scope.reason.otherReason = $scope.reason.newReason;
        consultationService.updateRequestChangeReason($scope.reason).success(function () {
            $modalInstance.close(true);
        });
    }

    $scope.close = function () {
        $modalInstance.close(false);
    };

    (function initialize() {
        $scope.reason = changeReason;
        $scope.reason.newReason = '';
        $scope.enums = enums;
        $scope.userId = loginUser.user.uniqueID;
        $scope.dailogTitle = enums.ChangeReasonTitle.Default;

        if ($scope.reason.changeStatus == enums.consultationRequestStatus.Rejected) {
            $scope.dailogTitle = enums.ChangeReasonTitle.Reject;
        }
        else if ($scope.reason.changeStatus == enums.consultationRequestStatus.Cancelled) {
            $scope.dailogTitle = enums.ChangeReasonTitle.Cancel;
        }
        else if ($scope.reason.changeStatus == enums.consultationRequestStatus.Terminate) {
            $scope.dailogTitle = enums.ChangeReasonTitle.Terminate;
        }
        else if ($scope.reason.changeStatus == enums.consultationRequestStatus.Reconsider) {
            $scope.dailogTitle = enums.ChangeReasonTitle.Reconsider;
        }
        else if ($scope.reason.changeStatus == enums.consultationRequestStatus.ApplyCancel) {
            $scope.dailogTitle = enums.ChangeReasonTitle.ApplyCancel;
        }

        if ($scope.reason.reasonType) {
            consultationService.getDictionaryByType($scope.reason.reasonType).success(function (data) {
                $scope.reasonList = data;

                if (!$scope.reason.changeReasonType) {
                    if ($scope.reasonList && $scope.reasonList.length > 0) {
                        $scope.reason.changeReasonType = $scope.reasonList[0].value;
                    }
                }
            });
        }
    }());
}
]);



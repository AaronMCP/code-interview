consultationModule.controller('RequestReceiverInfoController', ['$log', '$scope', 'consultationService', '$modal', 'enums', 'requestService', '$rootScope', '$sce',
    function ($log, $scope, consultationService, $modal, enums, requestService, $rootScope, $sce) {
        'use strict';
        $log.debug('RequestReceiverInfoController.ctor()...');

        var editClass = 'overflow-hidden apply-edit-window', editCenterClass = 'overflow-hidden apply-edit-center-window';
        function openEditWindow(receive, view, crtl) {
            var modalInstance = $modal.open({
                templateUrl: 'app/consultation/views/request/window/' + view,
                controller: crtl,
                windowClass: crtl === 'RequestReceiverEditController' ? editClass : editCenterClass,
                resolve: {
                    result: function () {
                        return receive;
                    }
                }
            });

            modalInstance.result.then(function (passData) {
                var requestId = passData.requestId;
                if (requestId) {
                    consultationService.getConsultationDetailAsync(requestId).success(function (data) {
                        $scope.report = requestService.getBaseRequestInfo(data);
                    });

                    consultationService.isHost(requestId).success(function (data) {
                        $scope.isHost = data;
                        if ($scope.isHost && $scope.user.isExpert) {
                            $scope.completePermisson = true;
                        }

                        consultationService.isExpert(requestId).success(function (data) {
                            $scope.isExpert = data;
                            $scope.wirteAdvicePermisson = ($scope.isHost || $scope.isExpert) && ($scope.user.isConsultationCenter || $scope.user.isExpert);
                        });
                    });

                    consultationService.getConsultationAssigns(requestId).success(function (data) {
                        $scope.expertNames = requestService.getConsultationAssignsString(data);
                    });
                }
            });
        };

        $scope.openRequestReceiveWindow = function () {

            if ($scope.report.status === enums.consultationRequestStatus.Applied) {
                openEditWindow({
                    requestId: $scope.report.requestId,
                    receiver: $scope.report.receiver,
                    serviceTypeID: $scope.report.serviceTypeID,
                    currentDate: $scope.report.currentDate,
                    timeRange: $scope.report.timeRange,
                    selections: $scope.report.selections,
                    receiverIDs:$scope.report.receiverIDs,
                    consultantType: $scope.report.consultantType,
                    isExpected: $scope.report.isExpected,
                    description: $scope.report.assignedDescription
                }, 'request-receiver-edit-window.html', 'RequestReceiverEditController');
            } else if ($scope.report.status === enums.consultationRequestStatus.Accepted || $scope.report.status === enums.consultationRequestStatus.Consulting) {
                openEditWindow({
                    requestId: $scope.report.requestId,
                    receiver: $scope.report.receiver,
                    serviceTypeID: $scope.report.serviceTypeID,
                    currentDate: $scope.report.currentDate,
                    timeRange: $scope.report.timeRange,
                    consultantType: $scope.report.consultantType,
                    isExpected: $scope.report.isExpected,
                    assignExpertIDs: $scope.report.assignExpertIDs,
                    serviceTypeName: $scope.report.serviceTypeName,
                    description: $scope.report.assignedDescription,
                }, 'request-receiver-center-edit-window.html', 'RequestReceiverEditCenterController');
            } else {
                alert('other status cannot edit receiver infomation');
            }
        };

        (function () {
            $scope.user = requestService.getUserPermisson();
        }());
    }
]);
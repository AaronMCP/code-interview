consultationModule.controller('RequestMeetingController', ['$log', '$scope', 'consultationService', '$modalInstance', 'configurationService', 'openDialog', '$modal', 'enums', '$translate', 'kendoService',
    '$filter', 'clientAgentService', 'requestId', 'csdToaster', 'loginContext',
    function ($log, $scope, consultationService, $modalInstance, configurationService, openDialog, $modal, enums, $translate, kendoService,
        $filter, clientAgentService, requestId, csdToaster, loginContext) {
        'use strict';
        $log.debug('RequestMeetingController.ctor()...');

        $scope.selectRow = function (confKey, hostName) {
            $scope.confKey = confKey;
            $scope.hostName = hostName;
        };

        $scope.cancel = function () {
            $modalInstance.close();
        };

        $scope.startMeeting = function () {
            if ($scope.confKey == '') {
                csdToaster.pop('info', $translate.instant('SelectMeeting') + $translate.instant('IsRequiredErrorMsg'), '');
                return;
            };

            if ($scope.displayName == '') {
                csdToaster.pop('info', $translate.instant('MeetingDisplayName') + $translate.instant('IsRequiredErrorMsg'), '');
                return;
            };

            if (!$scope.meetingPassword) {
                $scope.meetingPassword = '';
            }
            consultationService.updateMeetingStatus({ requestId: requestId, confKey: $scope.confKey, hostName: $scope.hostName, meetingPassword: $scope.meetingPassword }).success(function (data) {
                if (data == true) {
                    //ipaddress, string username, string userpassword, string conferenceid, string conferencepass, string showname
                    var parameters = {
                        ipaddress: $scope.meetingInfo.ipAddress,
                        username: $scope.meetingInfo.user,
                        userpassword: $scope.meetingInfo.password,
                        conferenceid: $scope.confKey,
                        conferencepass: $scope.meetingPassword,
                        showname: $scope.displayName
                    };
                    clientAgentService.startMeeting(parameters).success(function (startMeetingdata) {
                        if (startMeetingdata == true) {
                            $modalInstance.close();
                        }
                    }).error(function () {

                    });
                }
                else {
                    csdToaster.pop('info', $translate.instant('StartMeetingValidateError'), '');
                    return;
                }
            });
        };

        (function initialize() {
            $scope.meetingInfo = {};
            $scope.confKey = '';
            $scope.hostName = '';
            $scope.displayName = loginContext.localName;
            $scope.meetingPassword = '';
            
            consultationService.getMeetings().success(function (data) {
                $scope.meetingInfo = data;
                $scope.meetingPassword = $scope.meetingInfo.meetingPassword;

                $scope.meetingInfo.meetings = $filter('filter')($scope.meetingInfo.meetings, { status: '1' });

                var isfirst = true;
                angular.forEach($scope.meetingInfo.meetings, function (item) {
                    item.startTime = kendo.toString(kendo.parseDate(item.startTime), 'yyyy/MM/dd HH:mm');
                    if (isfirst) {
                        item.isFirst = true;
                        isfirst = false;
                    }
                });

                $scope.worklist = {
                    dataSource: {
                        data: $scope.meetingInfo.meetings,
                        schema: {
                            model: {
                                fields: {
                                    startTime: { type: "string" },
                                    subject: { type: "string" },
                                    hostName: { type: "string" },
                                    confKey: { type: "string" }
                                }
                            }
                        }
                    },
                    height: 200,
                    scrollable: true,
                    columns: [
                        {
                            title: $translate.instant("MeetingStartTime"),
                            width: "150px",
                            template: function (dataItem) {
                                var radioButton = "<input type='radio' name='meeting' ng-click='selectRow(dataItem.confKey, dataItem.hostName)'";
                                if (dataItem.isFirst) {
                                    radioButton += " checked";
                                    $scope.selectRow(dataItem.confKey, dataItem.hostName);
                                }
                                radioButton += ">{{dataItem.startTime}}</input>";
                                return radioButton;
                            }
                        },
                        { field: "subject", title: $translate.instant("MeetingTitle"), width: "150px", encoded: false },
                        { field: "hostName", title: $translate.instant("MeetingHost"), width: "150px", encoded: false }
                    ]

                };
            });

        }());
    }
]);
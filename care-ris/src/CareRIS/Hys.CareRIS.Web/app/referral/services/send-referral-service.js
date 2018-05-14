referralModule.factory('sendReferralService',
    ['$log', 'application', 'reportService', '$modal', 'enums', '$location', '$rootScope', 'configurationService', 'openDialog',
        '$translate','loginContext','$state',
        function ($log, application, reportService, $modal, enums, $location, $rootScope, configurationService, openDialog,
            $translate, loginContext, $state) {
            'use strict';
            var judgeCanReferralByOrder = function (order, type) {
                //type, 0:worklist,1:write report
                if (!application.configuration.canReferral) {
                    return false;
                }

                if (order.referralID) {
                    return false;
                } 
                //order,isreferral
                //50
                var isAllExamination = true;
                if (type == 0) {
                    _.any(order.procedures, function (item) {
                        if (item.status != enums.rpStatus.examination || item.isExistImage != 1) {
                            isAllExamination = false;
                            return false;
                        }
                    });
                }
                else {
                    isAllExamination = false;
                }

                //110
                if (!isAllExamination) {
                    var isAllSubmit = true;
                    _.any(order.procedures, function (item) {
                        if (item.status != enums.rpStatus.submit) {
                            isAllSubmit = false;
                            return false;
                        }
                    });

                    if (!isAllSubmit) {
                        return false;
                    }
                }

                return true;
            };

            var judgeLockAndSendByOrderID = function (orderID) {
                //check if it is locked, if locked, tell user about it,else finish exam 
                reportService.getLockByOrderIDByAnyLockType(orderID).success(function (lockData) {
                    if (lockData != null && lockData != '') {
                        var loginName = loginContext.userName;
                        var loginGuid = loginContext.userId;
                        var ownerGuids = [];
                        var ownerIPs = [];
                        var procedureIDs = lockData.procedureIDs;

                        if (procedureIDs != '') {
                            var procedures = procedureIDs.split('|');
                            for (var i = 0; i < procedures.length; i++) {
                                var procedure = procedures[i];
                                var procedureDetails = procedure.split('&');
                                var userGuid = procedureDetails[1];
                                var ownerIP = procedureDetails[2];
                                if (userGuid != loginGuid) {
                                    if (ownerGuids.indexOf(userGuid) == -1) {
                                        ownerGuids.push(userGuid);
                                        ownerIPs.push(ownerIP);
                                    }
                                }
                            }

                            //just show one locked info
                            if (ownerGuids.length > 0) {
                                var ownerID = ownerGuids[0];
                                configurationService.getUserName(ownerID).success(function (data) {
                                    var tipInfo = $translate.instant("SendReferralLocked");
                                    tipInfo = tipInfo.replace('{0}', data.localName).replace('{1}', ownerIPs[0]);
                                    openDialog.openIconDialog(openDialog.NotifyMessageType.Info, $translate.instant("Tips"), tipInfo);
                                    return;
                                });
                            }
                            else
                            {
                                openSendReferralDialog(orderID);
                            }

                        } else {
                            var ownerID = lockData.owner;
                            var ownerIP = lockData.ownerIP;
                            configurationService.getUserName(ownerID).success(function (data) {
                                var tipInfo = $translate.instant("SendReferralLocked");
                                tipInfo = tipInfo.replace('{0}', data.localName).replace('{1}', ownerIP);
                                openDialog.openIconDialog(openDialog.NotifyMessageType.Info, $translate.instant("Tips"), tipInfo);
                                return;
                            });
                        }
                    } else {
                        openSendReferralDialog(orderID);
                    }
                });
            };

            var judgeCanReferralByOrderID = function (orderID) {
                if (!application.configuration.canReferral) {
                    return false;
                }
            };

            var openSendReferralDialog = function (orderID) {
                var modalInstance = $modal.open({
                    templateUrl:'/app/referral/views/send-referral.html',
                    controller: 'SendReferralController',
                    backdrop: 'static',
                    keyboard: false,
                    resolve: {
                        orderID: function () {
                            return orderID;
                        }
                    }
                });
                modalInstance.result.then(function (result) {
                    if (result)
                    {
                        reportService.deleteLockByOrderID(orderID).success(function () {
                            $location.path('ris/worklist/registrations');
                            $location.url($location.path());
                            $rootScope.refreshSearch();
                            //can not execute refreshSearch
                            //if ($state.includes('ris.worklist.registrations')) {
                            //    $state.go('ris.worklist.registrations', {
                            //        timestamp: Date.now()
                            //    });
                            //} else {
                            //    $state.go('ris.worklist.registrations', {
                            //        ts: Date.now()
                            //    });
                            //}
                        });
                    }
                    else
                    {
                        openDialog.openIconDialog(openDialog.NotifyMessageType.Info, $translate.instant("Tips"), $translate.instant("ReferralNotFinished"));
                    }
                });
            };
            return {
                judgeCanReferralByOrder: judgeCanReferralByOrder,
                judgeCanReferralByOrderID: judgeCanReferralByOrderID,
                judgeLockAndSendByOrderID: judgeLockAndSendByOrderID,
                openSendReferralDialog: openSendReferralDialog
            }
        }]);
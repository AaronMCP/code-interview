consultationModule.factory('requestService',
    ['$stateParams', '$modal', '$state', 'consultationService', 'enums', '$translate', 'openDialog', '$rootScope', '$sce', 'loginUser',
        function ($stateParams, $modal, $state, consultationService, enums, $translate, openDialog, $rootScope, $sce, loginUser) {
            'use strict';

            var openEditWindow = function (data, temaplte, controller) {
                var modalInstance = $modal.open({
                    templateUrl: 'app/consultation/views/request/window/' + temaplte,
                    controller: controller,
                    windowClass: 'overflow-hidden advice-edit-window',
                    backdrop: 'static',
                    keyboard: false,
                    size: 'lg',
                    resolve: {
                        changeReason: function () {
                            return data;
                        }
                    }
                });

                modalInstance.result.then(function (status) {
                    if (status) {
                        $state.go('ris.consultation.requests', {
                            searchCriteria: $stateParams.searchCriteria,
                            timestamp: Date.now(),
                            reload: true
                        });
                    }
                });
            };

            var openDeleteWindow = function (deleteType, id) {
                $modal.open({
                    templateUrl:'/app/consultation/views/request/window/request-delete-reason-window.html',
                    controller: 'RequestDeleteReasonController',
                    windowClass: 'overflow-hidden advice-edit-window',
                    backdrop: 'static',
                    keyboard: false,
                    size: 'lg',
                    resolve: {
                        deleteData: function () {
                            return {
                                id: id,
                                deleteType: deleteType
                            };
                        }
                    }
                });
            };

            var goType = {
                worklist: 0,
                request: 1,
                recover: 2,
                none: 3
            };

            var returnConsultWorkList = function (type, newData) {
                if (type === goType.none && newData && newData.isFromRis) {
                    $state.go('ris.report');
                }else if (type === goType.worklist || type === goType.recover) {
                    if (newData && newData.isFromRis) {
                        $state.go('ris.report');
                    } else {
                        $state.go('ris.consultation.cases', {
                            searchCriteria: $stateParams.searchCriteria,
                            timestamp: Date.now()
                        });
                    }
                }
                else if (type === goType.request) {
                    newData.patientCaseID = newData.uniqueID;
                    $state.go('ris.consultation.apply', { searchCriteria: $stateParams.searchCriteria, patientCase: newData });
                }
            };

            var combinePatient = function (newData, combinePatientCaseList, goType) {
                if (!newData.age && newData.currentAge) {
                    newData.age = newData.currentAge;
                }
                var modalInstance = $modal.open({
                    templateUrl:'/app/consultation/views/patient-combine-view.html',
                    controller: 'PatientCombineController',
                    backdrop: 'static',
                    keyboard: false,
                    resolve: {
                        patientCase: function () {
                            return { newPatientCase: newData, combinePatientCaseList: combinePatientCaseList };
                        }
                    }
                });

                modalInstance.result.then(function () {
                    returnConsultWorkList(goType, newData);
                });
            };

            var recoverPatientCase = function (type, patientCase) {
                consultationService.recoverPatientCase(patientCase.patientCaseID).success(function () {
                    openDialog.openIconDialogOkFun(
                        openDialog.NotifyMessageType.Success, $translate.instant('RecoverSuccess'),
                        '',
                        returnConsultWorkList(type, patientCase));

                    consultationService.GetCombinePatientCaseList(patientCase.patientCaseID, patientCase.identityCard).success(function (combinePatientCaseList) {
                        if (combinePatientCaseList && combinePatientCaseList.length > 0) {
                            combinePatient(patientCase, combinePatientCaseList, type);
                        }
                    });
                });
            };

            var getCurrentAge = function (dicAge) {
                var ageArry = dicAge.split(' ');
                var age = ageArry[0];
                var ageUnit = ageArry[1];
                var a = _.findWhere($rootScope.ageUnitList, { value: ageUnit });
                if (a) {
                    ageUnit = a.text;
                }
                return age + ' ' + ageUnit;
            };

            return {
                goType: goType,
                rejectRequest: function (data) {
                    openEditWindow(data, 'request-change-reason-window.html', 'RequestChangeReasonController');
                },
                terminatRequest: function (data) {
                    openEditWindow(data, 'request-change-reason-window.html', 'RequestChangeReasonController');
                },
                cancelApplication: function (data) {
                    openEditWindow(data, 'request-change-reason-window.html', 'RequestChangeReasonController');
                },
                requestReconsideration: function (data) {
                    openEditWindow(data, 'request-change-reason-window.html', 'RequestChangeReasonController');
                },
                ApplyCancelRequest: function (data) {
                    openEditWindow(data, 'request-change-reason-window.html', 'RequestChangeReasonController');
                },
                acceptRequest: function (data) {
                    openEditWindow(data, 'request-accept-edit.html', 'RequestAcceptEditController');
                },
                printResults: function (data) {
                    $modal.open({
                        templateUrl:'/app/consultation/views/request/window/report-print-view.html',
                        controller: 'ReportPrintViewController',
                        windowClass: 'overflow-hidden report-print-window',
                        backdrop: 'static',
                        keyboard: false,
                        size: 'lg',
                        resolve: {
                            changeReason: function () {
                                return data;
                            }
                        }
                    });
                },
                applyRequest: function (data) {
                    $state.go('ris.consultation.apply', {
                        patientCase: data
                    });
                },
                openDeleteRequestWindow: function (requestId) {
                    openDeleteWindow(enums.DeleteType.ConsultationRequest, requestId);
                },
                openDeletePatientCaseWindow: function (patientCaseId) {
                    openDeleteWindow(enums.DeleteType.PatientCase, patientCaseId);
                },
                backToWorkList: function (toStatus, reload) {
                    if (reload) {
                        $state.go(toStatus, {
                            searchCriteria: $stateParams.searchCriteria,
                            timestamp: Date.now(),
                            reload: true
                        });
                    } else {
                        $state.go(toStatus, {
                            searchCriteria: $stateParams.searchCriteria
                        });
                    }
                },
                recoverPatientCase: function (type, patientCase) {
                    recoverPatientCase(type, patientCase);
                },
                combinePatient: function (newData, combinePatientCaseList, goType) {
                    combinePatient(newData, combinePatientCaseList, goType);
                },
                returnConsultWorkList: function (type, newData) {
                    returnConsultWorkList(type, newData);
                },
                getCurrentAge: function (dicAge) {
                    return getCurrentAge(dicAge);
                },
                getConsultationAssignsString: function (data) {
                    var expertNames = '';
                    angular.forEach(data, function (item) {
                        expertNames += item.displayName;
                        if (item.isHost === 1) {
                            expertNames += '<span class="icon-general icon-host ng-scope favorite"></span>';
                        }
                        expertNames += ';&nbsp;';
                    });
                    return expertNames;
                },
                getBaseRequestInfo: function (data, requestStatus) {
                    data.showReason = true;

                    switch (requestStatus) {
                        case enums.consultationRequestStatus.Cancelled:
                            data.reasonTitle = 'CancelReason';
                            break;
                        case enums.consultationRequestStatus.ApplyCancel:
                            data.reasonTitle = 'ApplyCancelReason';
                            break;
                        case enums.consultationRequestStatus.Rejected:
                            data.reasonTitle = 'RejectReason';
                            break;
                        case enums.consultationRequestStatus.Reconsider:
                            data.reasonTitle = 'ReconsiderReason';
                            break;
                        case enums.consultationRequestStatus.Terminate:
                            data.reasonTitle = 'TerminateReason';
                            break;
                        default:
                            data.reasonTitle = '';
                            data.showReason = false;
                            break;
                    }

                    data.history = $sce.trustAsHtml(data.history);
                    data.clinicalDiagnosis = $sce.trustAsHtml(data.clinicalDiagnosis);
                    data.requestPurpose = $sce.trustAsHtml(data.requestPurpose);
                    data.requestRequirement = $sce.trustAsHtml(data.requestRequirement);
                    data.consultationAdvice = $sce.trustAsHtml(data.consultationAdvice);
                    data.consultationRemark = $sce.trustAsHtml(data.consultationRemark);

                    data.currentAgeT = getCurrentAge(data.currentAge);

                    data.currentDate = data.consultationDate ? data.consultationDate : data.expectedDate;
                    data.timeRange = data.consultationStartTime ? data.consultationStartTime : data.expectedTimeRange;
                    data.isExpected = !data.consultationDate;

                    return data;
                },
                getUserPermisson: function () {
                    return {
                        isJuniorDoctor: loginUser.isDoctor,
                        isConsultationCenter: loginUser.isConsAdmin,
                        isExpert: loginUser.isExpert
                    };
                }
            }
        }]);
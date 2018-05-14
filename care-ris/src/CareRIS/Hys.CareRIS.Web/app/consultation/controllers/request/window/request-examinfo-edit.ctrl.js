consultationModule.controller('RequestExamInfoEditController', ['$log', '$scope', 'consultationService', 'patientCase', '$modalInstance', '$compile', '$timeout', 'examService', 'loginUser', '$http', 'openDialog',
    '$window', '$translate', 'loginContext', 'clientAgentService', '$rootScope',
    function ($log, $scope, consultationService, patientCase, $modalInstance, $compile, $timeout, examService, loginUser, $http, openDialog,
        $window, $translate, loginContext, clientAgentService, $rootScope) {
        'use strict';
        $log.debug('RequestExamInfoEditController.ctor()...');

        $scope.close = function () {
            $modalInstance.dismiss();
        };

        $scope.examInfoDeleteFile = function (patientCaseId, uniqueId) {
            consultationService.examInfoDeleteFile(patientCaseId, uniqueId).success(function (data) {
            });
        };

        $scope.examInfoDeleteItem = function (patientCaseId, emrItemId) {
            consultationService.examInfoDeleteItem(patientCaseId, emrItemId).success(function (data) {
            });
        };

        $scope.examInfoFileNameChanged = function (patientCaseId, uniqueId, fileName) {
            consultationService.examInfoFileNameChanged(patientCaseId, uniqueId, fileName).success(function (data) {
            });
        };

        $scope.examInfoItemAdded = function (patientCaseId, newItem, complete) {
            var patientCase = { uniqueID: patientCaseId, newEMRItems: [] };
            patientCase.newEMRItems.push(createNewItemData(newItem));
            consultationService.examInfoItemAdded(patientCase).success(function (data) {
                if (data && data.newEMRItems && data.newEMRItems.length > 0)
                {
                    updateNewItemData(newItem, data.newEMRItems[0], complete);
                    complete();
                }
                 
            });
        };

        $scope.examInfoItemEdited = function (patientCaseId, item, complete) {
            var patientCase = { uniqueID: patientCaseId, newEMRItems: [] };
            patientCase.newEMRItems.push(createNewItemData(item));
            consultationService.examInfoItemEdited(patientCase).success(function (data) {
                if (data && data.newEMRItems && data.newEMRItems.length > 0) {
                    updateNewItemData(item, data.newEMRItems[0], complete);
                    complete();
                }
            });
        };

        $scope.examInfoModuleUpdated = function (module) {
            consultationService.updateModule(module).success(function (data) {
                
            });
        };

        var createNewItemData = function (item) {
            var newEMRItem = {};
            if (item.uniqueID) {
                newEMRItem.uniqueID = item.uniqueID;
            }
            newEMRItem.eMRItemType = item.emrItemType;
            newEMRItem.accessionNo = item.accessionNo;
            if (item.examSection) {
                newEMRItem.examSection = item.examSection;
            }
            if (item.patientNo) {
                newEMRItem.patientNo = item.patientNo;
            }
            newEMRItem.examDate = item.examDate;
            if (item.bodyPart) {
                newEMRItem.bodyPart = item.bodyPart;
            }
            if (item.examDescription) {
                newEMRItem.examDescription = item.examDescription;
            }

            //files
            if (item.itemDetails) {
                newEMRItem.itemFiles = [];
                angular.forEach(item.itemDetails, function (itemFile, indexFile) {
                    var newItemFile = {};
                    if (itemFile.file.uniqueId) {
                        newItemFile.uniqueID = itemFile.file.uniqueId;
                    }
                    newItemFile.fileType = itemFile.file.fileType;
                    newItemFile.fileName = itemFile.file.fileName;
                    newItemFile.path = itemFile.file.path;
                    newItemFile.detailedId = itemFile.file.detailedId;
                    newItemFile.srcInfo = $scope.srcInfo;
                    newEMRItem.itemFiles.push(newItemFile);
                }
                );
            }
            return newEMRItem;
        };

        var updateNewItemData = function (item, newItem, complete) {
            if (item.uniqueID) {
            }
            else
            {
                item.uniqueID = newItem.uniqueID;
            }
            //files
            if (item.itemDetails) {
                angular.forEach(item.itemDetails, function (itemFile, indexFile) {
                    var newItemFile = {};
                    if (itemFile.file.uniqueId) {
                    }
                    else
                    {
                        //newItem.itemFiles path, itemFile.file.path
                        var s = _.findWhere(newItem.itemFiles, { path: itemFile.file.path + '' });
                        if (s) {
                            itemFile.file.uniqueId = s.uniqueID;
                            //complete.push(s.uniqueID);
                            if (!itemFile.detailID) {
                                itemFile.detailID = s.uniqueID;
                            }
                        }
                        
                    }

                    
                }
                );
            }

            //return newEMRItem;
        };

        (function initialize() {
            $scope.user = {
                isJuniorDoctor: loginUser.isDoctor,
                isConsultationCenter: loginUser.isConsAdmin,
                isExpert: loginUser.isExpert
            };
            $scope.patientCase = patientCase;
            $scope.disableEdit = $scope.user.isExpert ? true : false;
            $scope.srcInfo = '';

            if ($rootScope.browser.versions.mobile) {
                $scope.srcInfo = 'Mobile';
            } else {
                consultationService.getUserDam().success(function (damData) {
                    //apiHost
                    if (damData) {
                        var param = { damid: damData.uniqueID, apihost: loginContext.apiHost };
                        clientAgentService.GetProcessIDForDam(param).success(function (data) {
                            $scope.srcInfo = data;
                        });
                    }
                });
            }

            $timeout(function () {
                var tpl = '<exam-info-view disable-edit="disableEdit" patient-case-id="patientCase.patientCaseID" on-file-deleted="examInfoDeleteFile(patientCaseId,uniqueId)" ' +
                'on-item-deleted="examInfoDeleteItem(patientCaseId,emrItemId)" ' +
                'on-filename-changed="examInfoFileNameChanged(patientCaseId,uniqueId,fileName)" ' +
                 'on-item-added="examInfoItemAdded(patientCaseId,newItem,complete)" ' +
                 'on-item-edited="examInfoItemEdited(patientCaseId,item,complete)" ' +
                 'on-module-updated="examInfoModuleUpdated(module)" ' +
                '></exam-info-view>';
                $('#examInfoContainer').html(tpl);

                $compile($('#examInfoContainer').contents())($scope);

            }, 100);
        }());
    }
]);
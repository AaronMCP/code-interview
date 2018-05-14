consultationModule.controller('ExamModuleController', ['$log', '$scope', 'consultationService', 'examService', '$q', '$http', '$timeout', '$translate', 'openDialog', 'loginUser', 'enums',
    function ($log, $scope, consultationService, examService, $q, $http, $timeout, $translate, openDialog, loginUser, enums) {
        'use strict';
        $log.debug('ExamModuleController.ctor()...');

        var detailFolderIdCollection = {
            getIds: function () {
                var ts = this, ids = [];
                for (var id in ts) {
                    ts[id] === true && ids.push(id);
                }
                return ids;
            },
            dics: {}
        };
        var uniqueIdsNotComplete = [];
        var uniqueId2fileDic = {};

        var removeUniqueIds = function (details) {
            /// <summary>
            /// remove uniqueId from [uniqueIdsNotComplete] and [uniqueId2fileDic]
            /// </summary>
            /// <param name="details">Array of detail</param>
            var detailLen = details.length;
            var tmpLen;
            var tmpDetail;
            for (var i = 0; i < detailLen; ++i) {
                tmpDetail = details[i];
                if (tmpDetail.file && tmpDetail.file.uniqueId) {
                    delete uniqueId2fileDic[tmpDetail.file.uniqueId]
                    tmpLen = uniqueIdsNotComplete.length;
                    for (var j = 0; j < tmpLen; ++j) {
                        if (uniqueIdsNotComplete[j] === tmpDetail.file.uniqueId) {
                            uniqueIdsNotComplete.splice(j, 1);
                            break;
                        }
                    }
                }
            }
        };

        var workerInterval = 1000;

        var getDamApi = function () {
            if (!getDamApi.defer) {
                getDamApi.defer = $q.defer();
                consultationService.getUserDam().success(function (urlObj) {
                    if (urlObj.webApiUrl) {
                        getDamApi.defer.resolve(urlObj.webApiUrl);
                    } else {
                        getDamApi.defer.reject('Dam webApiUrl is unavalable');
                    }
                }).error(function (errorMsg) {
                    getDamApi.defer.reject(errorMsg);
                });
            }
            return getDamApi.defer.promise;
        };
        var getFileInfo = function (detailIds) {
            var fileDefer = $q.defer();
            getDamApi().then(function (webApiUrl) {
                var apiUrl = webApiUrl + '/api/v1/registration/items';
                $http.post(apiUrl, detailIds).success(function (fileInfoes) {
                    fileDefer.resolve(fileInfoes);
                }).error(function (errorMsg) {
                    fileDefer.reject(errorMsg);
                });
            }, function (errorMsg) {
                fileDefer.reject(errorMsg);
            });

            return fileDefer.promise;
        };

        var fileProcesser = function (detailIds, detailIdDic, data) {
            var defer = $q.defer();
            if (detailIds.length < 1) {
                defer.resolve(data);
                return defer.promise;
            }

            getFileInfo(detailIds).then(function (fileInfoes) {
                var fileInfoesLen = fileInfoes.length, tmpLen = 0, detailLen = 0, dataCollection = {}, tmpfileInfo = {}, tmpDicItem = {}, detail,
                    needInserts = {}, insertItem, insertArr, folderIdsAdded = false, fileIdsAdded = false, detailID;

                for (var i = 0; i < fileInfoesLen; ++i) {
                    tmpfileInfo = fileInfoes[i];
                    (dataCollection[tmpfileInfo.parentID] || (dataCollection[tmpfileInfo.parentID] = [])).push(tmpfileInfo);
                }

                for (var detailId in detailIdDic) {
                    detailLen = (dataCollection[detailId] || []).length;
                    tmpDicItem = detailIdDic[detailId];
                    detail = data[tmpDicItem.itemIndex].itemDetails[tmpDicItem.detailIndex];
                    detailID = detail.detailID;

                    if (detailLen < 1) continue;
                    if (detailLen >= 1) {
                        tmpfileInfo = dataCollection[detailId][0];
                        tmpfileInfo.description = convertSendError(tmpfileInfo.description);
                        detail.file = {
                            uniqueId: tmpfileInfo.uniqueID,
                            fileName: tmpfileInfo.fileName,
                            fileType: String(tmpfileInfo.fileExtension).toLowerCase(),
                            path: tmpfileInfo.destFilePath,
                            progress: tmpfileInfo.progress,
                            error: tmpfileInfo.fileStatus >= 10,
                            errorDesc: tmpfileInfo.fileStatus >= 10 ? tmpfileInfo.description : ''
                        };

                        if (tmpfileInfo.itemType === 1 || (tmpfileInfo.fileType === 6 && tmpfileInfo.fileStatus === 0)) {
                            detail.file.fileType = (tmpfileInfo.fileType === 6 && tmpfileInfo.fileStatus === 0) ? 'dicom' : 'folder';

                            if (detail.file.error) continue;

                            detailFolderIdCollection[detailId] = true;
                            folderIdsAdded = true;
                            detailFolderIdCollection.dics[detailId] = tmpDicItem;

                            continue;
                        };

                        if (detailFolderIdCollection[detailId] === true) {
                            delete detailFolderIdCollection[detailId];
                            delete detailFolderIdCollection.dics[detailId];
                        }

                        if (detail.file.progress < 100 && !detail.file.error) {
                            uniqueIdsNotComplete.push(detail.file.uniqueId);
                            uniqueId2fileDic[detail.file.uniqueId] = detail.file;
                            fileIdsAdded = true;
                        }

                        if (detailLen == 1) continue;

                        insertItem = {
                            insertIndex: tmpDicItem.detailIndex + 1,
                            details: []
                        };
                        for (var j = 1; j < detailLen; ++j) {
                            tmpfileInfo = dataCollection[detailId][j];
                            tmpfileInfo.description = convertSendError(tmpfileInfo.description);
                            detail = {
                                detailID: detailID,
                                clientId: RIS.newGuid(),
                                folderConverted: true,
                                file: {
                                    uniqueId: tmpfileInfo.uniqueID,
                                    fileName: tmpfileInfo.fileName,
                                    fileType: String(tmpfileInfo.fileExtension).toLowerCase(),
                                    path: tmpfileInfo.destFilePath,
                                    progress: tmpfileInfo.progress,
                                    error: tmpfileInfo.fileStatus >= 10,
                                    errorDesc: tmpfileInfo.fileStatus >= 10 ? tmpfileInfo.description : ''
                                }
                            }
                            insertItem.details.push(detail);

                            if (detail.file.progress < 100 && !detail.file.error) {
                                uniqueIdsNotComplete.push(detail.file.uniqueId);
                                uniqueId2fileDic[detail.file.uniqueId] = detail.file;
                            }
                        }

                        (needInserts[tmpDicItem.itemIndex + ''] || (needInserts[tmpDicItem.itemIndex + ''] = [])).push(insertItem);
                    }
                };

                var insertIndex, details, offset;

                for (var itemIndex in needInserts) {
                    insertArr = needInserts[itemIndex];
                    tmpLen = insertArr.length;
                    offset = 0;
                    for (var k = 0; k < tmpLen; ++k) {
                        insertItem = insertArr[k];
                        insertIndex = insertItem.insertIndex + offset;
                        details = insertItem.details;
                        offset += details.length;
                        details.splice(0, 0, insertIndex, 0);
                        Array.prototype.splice.apply(data[itemIndex].itemDetails, details);
                    }
                };

                if (fileIdsAdded && !fileStateWorking && $scope.moduleReady) fileStateWorker();
                if (folderIdsAdded && !detailFolderWorking && $scope.moduleReady) detailFolderWorker();

                defer.resolve(data);
            }, function (errorMsg) {
                defer.reject(errorMsg);
            });

            return defer.promise;
        };

        var convertSendError = function (description) {
            if (description)
            {
                var newDesc = description;
                angular.forEach(enums.fileSendError, function (item, index) {
                    newDesc = newDesc.replace(item, $translate.instant(item));
                });
                return newDesc;
            }
            else{
                return '';
            }
        };

        var fileStateWorking = false;
        var detailFolderWorking = false;
        var fileStateWorkTimer = null;
        var detailFolderTimer = null;

        var fileStateWorker = function () {
            fileStateWorkTimer = $timeout(function () { $log.log($scope.module.type + ' fileStateWorker timer executed ', Date.now()); }, workerInterval);
            fileStateWorking || (fileStateWorking = true);

            fileStateWorkTimer.then(
                function () {
                    if (uniqueIdsNotComplete.length < 1) {
                        fileStateWorking = false;
                        return;
                    }

                    if (!$scope.moduleReady) fileStateWorker();

                    getDamApi().then(function (webApiUrl) {
                        var apiUrl = webApiUrl + '/api/v1/registration/itemsbyuid';
                        $http.post(apiUrl, uniqueIdsNotComplete).success(function (files) {
                            var tmpLen = files.length;
                            var tmpFile;
                            var temLen1 = 0;

                            for (var i = 0; i < tmpLen; ++i) {
                                tmpFile = files[i];
                                uniqueId2fileDic[tmpFile.uniqueID].progress = tmpFile.progress;
                                uniqueId2fileDic[tmpFile.uniqueID].errorDesc = '';
                                if (tmpFile.fileStatus >= 10) {
                                    uniqueId2fileDic[tmpFile.uniqueID].error = true;
                                    tmpFile.description = convertSendError(tmpFile.description);
                                    uniqueId2fileDic[tmpFile.uniqueID].errorDesc = tmpFile.description
                                    continue;
                                }

                                if (tmpFile.progress < 100) continue;

                                temLen1 = uniqueIdsNotComplete.length;
                                for (var j = 0; j < temLen1; ++j) {
                                    if (uniqueIdsNotComplete[j] === tmpFile.uniqueID) {
                                        uniqueIdsNotComplete.splice(j, 1);
                                        break;
                                    }
                                }
                            }

                            fileStateWorker();
                        }).error(function (errorMsg) {
                            $log.error(errorMsg);
                            fileStateWorker();
                        });
                    }, function (errorMsg) {
                        $log.error(errorMsg);
                        fileStateWorker();
                    });
                },
                function () {
                    fileStateWorking = false;
                    $log.log($scope.module.type + ' fileStateWorker timer reject ', Date.now())
                }
            );
        };
        var detailFolderWorker = function () {
            detailFolderTimer = $timeout(function () { $log.log($scope.module.type + ' detailFolderWorker timer executed ', Date.now()); }, workerInterval);

            detailFolderWorking || (detailFolderWorking = true);
            detailFolderTimer.then(
                function () {
                    var detailIds = detailFolderIdCollection.getIds();
                    var dics = detailFolderIdCollection.dics;

                    if (detailIds.length < 1) {
                        detailFolderWorking = false;
                        return;
                    }

                    fileProcesser(detailIds, dics, $scope.moduleData).then(function () {
                        detailFolderWorker();
                    }, function (errorMsg) {
                        detailFolderWorker();
                        $log.error(errorMsg);
                    });

                },
                function () {
                    detailFolderWorking = false;
                    $log.log($scope.module.type + ' detailFolderWorker timer reject ', Date.now());
                });
        };

        var workerManager = function (file, itemIndex, detailIndex, detailID) {
            var fileAdded = false, folderAdded = false;

            if (file.fileType !== 'folder' && file.fileType !== 'dicom') {
                uniqueId2fileDic[file.uniqueId] = file;
                uniqueIdsNotComplete.push(file.uniqueId);
                fileAdded = true;
            } else {
                detailFolderIdCollection[detailID] = true;
                folderAdded = true;
                detailFolderIdCollection.dics[detailID] = { itemIndex: itemIndex, detailIndex: detailIndex }
            }
            if (fileAdded && !fileStateWorking && $scope.moduleReady) fileStateWorker();
            if (folderAdded && !detailFolderWorking && $scope.moduleReady) detailFolderWorker();
        };
        var operateItemCompleteFunc = function (item) {
            var len = item.itemDetails.length;
            var operateItemDetail, operateFile;

            for (var i = 0; i < len; ++i) {
                operateItemDetail = item.itemDetails[i];
                operateFile = operateItemDetail.file;

                if (!operateItemDetail.isNew || !operateFile.uniqueId || operateFile.progress >= 100) continue;
                workerManager(operateFile, $scope.operateContent.itemIndex, i, operateItemDetail.detailID);
            }
        };
        var initModuleData = function () {
            if (!angular.isArray($scope.module.initContentValue) || $scope.module.initContentValue.length <= 0) {
                return;
            }
            var len = $scope.module.initContentValue.length,
                temObj, fileList, fileListLen, tmpFile, file, item;
            for (var i = 0; i < len; ++i) {
                temObj = angular.copy($scope.module.initContentValue[i]);
                fileList = temObj.fileList;
                delete temObj.fileList;
                item = angular.extend({
                    clientId: RIS.newGuid(),
                    emrItemType: $scope.module.type,
                    itemDetails: [],
                    isNew: true
                }, temObj);

                fileListLen = fileList ? fileList.length : 0;
                for (var j = 0; j < fileListLen; ++j) {
                    tmpFile = fileList[j];

                    file = $.extend({}, tmpFile, {
                        fileName: tmpFile.fileName,
                        fileType: tmpFile.fileType,
                        path: tmpFile.path,
                        detailedId: tmpFile.detailedId,
                        progress: 0
                    });

                    item.itemDetails.push({
                        clientId: RIS.newGuid(),
                        isNew: true,
                        file: file
                    });
                }
                $scope.moduleData.unshift(item);

                $scope.$emit('event:ItemAdded', $scope.patientCaseId, item, function () { operateItemCompleteFunc(item); });
            }
        };

        (function initialize() {
            $scope.$on('$destroy', function () {
                fileStateWorkTimer && $timeout.cancel(fileStateWorkTimer);
                detailFolderTimer && $timeout.cancel(detailFolderTimer);
            });

            $scope.moduleEmpty = true;
            $scope.moduleReady = false;
            $scope.moduleData = [];
            examService.addModulesData($scope.module.type, $scope.moduleData);

            initModuleData();
            consultationService.getEMRItems($scope.patientCaseId, $scope.module.type).success(function (data) {
                var dataLen = data.length;
                var detailsLen = 0, tmpItemDetail;
                var detailIds = [];
                var detailIdDic = {};

                for (var i = 0; i < dataLen; ++i) {

                    detailsLen = data[i].itemDetails.length;
                    data[i].clientId = RIS.newGuid();

                    for (var j = 0; j < detailsLen; ++j) {
                        tmpItemDetail = data[i].itemDetails[j];
                        tmpItemDetail.clientId = RIS.newGuid();

                        detailIdDic[tmpItemDetail.detailID] = { itemIndex: i, detailIndex: j };
                        detailIds.push(tmpItemDetail.detailID);
                    }
                };

                fileProcesser(detailIds, detailIdDic, data).then(function (processedData) {
                    $scope.moduleData.push.apply($scope.moduleData, processedData);
                    $scope.moduleEmpty = $scope.moduleData.length < 1;
                    $scope.moduleReady = true;

                    fileStateWorker();
                    detailFolderWorker();
                }, function (errorMsg) {
                    $log.error(errorMsg);
                });
            }).error(function (errorMsg) {
                $log.error(errorMsg);
            });

            $scope.operateContent = function (item, itemIndex) {
                $scope.operateContent.isNew = false;

                if (!item) {
                    item = {};
                    $scope.operateContent.isNew = true;
                    $scope.operateContent.itemIndex = 0;
                } else {
                    $scope.operateContent.operateItem = item;
                    $scope.operateContent.itemIndex = itemIndex;
                    item = angular.copy(item);
                    //delete item.itemDetails;
                }

                $scope.$emit('event:operateContent', $scope.module, item, $scope.operateContent.isNew);
            };

            $scope.$on('event:operateItemComplete', function (e, module, comingItem) {
                if (module.id !== $scope.module.id) return;
                var item = null;

                var fileList = comingItem.fileList;
                var itemDetails = comingItem.itemDetails;

                delete comingItem.fileList;
                delete comingItem.itemDetails;

                if ($scope.operateContent.isNew) {
                    item = {
                        clientId: RIS.newGuid(),
                        emrItemType: module.type,
                        itemDetails: [],
                        isNew: true
                    };
                    $scope.moduleData.unshift(item);
                } else {
                    item = $scope.operateContent.operateItem;
                    var deletedFileIds = [], details = [], deleted = false, detailsLen = itemDetails.length, detail;

                    for (var i = 0; i < detailsLen; ++i) {
                        detail = itemDetails[i];
                        if (detail.deleted) {
                            item.itemDetails.splice(detail.deletedIndex, 1);
                            if (detail.file && detail.file.uniqueId) {
                                deletedFileIds.push(detail.file.uniqueId);
                                details.push(detail);
                                deleted = true;
                            }
                        }
                    }

                    if (deleted) {
                        removeUniqueIds(details);
                        $scope.$emit('event:FileDeleted', $scope.patientCaseId, deletedFileIds.join(','));
                    }
                }

                angular.extend(item, comingItem);

                var fileListLen = fileList ? fileList.length : 0;
                var tmpFile, file;
                for (var i = 0; i < fileListLen; ++i) {

                    tmpFile = fileList[i];
                    file = {
                        fileName: tmpFile.fileName,
                        fileType: tmpFile.fileType,
                        path: tmpFile.path,
                        detailedId: tmpFile.detailedId,
                        progress: 0
                    };

                    item.itemDetails.push({
                        clientId: RIS.newGuid(),
                        isNew: true,
                        file: file
                    });
                }

                $scope.moduleEmpty = false;

                if ($scope.operateContent.isNew)
                    $scope.$emit('event:ItemAdded', $scope.patientCaseId, item, function () { operateItemCompleteFunc(item) });
                else {
                    $scope.$emit('event:ItemEdited', $scope.patientCaseId, item, function () { operateItemCompleteFunc(item) });
                }
                e.preventDefault();
            });

            function deleteItem(index) {
                var deletedItem = $scope.moduleData[index];
                var emrItemId = deletedItem.uniqueID;

                removeUniqueIds(deletedItem.itemDetails);

                $scope.moduleData.splice(index, 1);
                var len = $scope.moduleData.length;
                $scope.moduleEmpty = len < 1;
                if (emrItemId !== null && emrItemId !== undefined) {
                    $scope.$emit('event:ItemDeleted', $scope.patientCaseId, emrItemId);
                }
            };

            $scope.deleteItem = function (index) {
                openDialog.openIconDialogYesNo(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant("ConfrimDelete"), function () {
                    deleteItem(index);
                });
            }

            $scope.editFile = function (file) {
                file.editingFileName = file.fileName;
                file.fileEditing = true;
            };
            $scope.deleteFile = function (details, index) {
                openDialog.openIconDialogYesNo(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant("ConfrimDelete"), function () {
                    var deleteItem = details[index];
                    var uniqueId = deleteItem.file ? deleteItem.file.uniqueId : '';

                    removeUniqueIds([deleteItem]);

                    details.splice(index, 1);
                    if (uniqueId)
                        $scope.$emit('event:FileDeleted', $scope.patientCaseId, uniqueId);
                });
            };
            $scope.saveEditFile = function (file) {
                var uniqueId = file.uniqueId;
                file.fileName = file.editingFileName;
                file.fileEditing = false;
                if (uniqueId) {
                    $scope.$emit('event:FilenameChanged', $scope.patientCaseId, uniqueId, file.fileName);
                }
            };
            $scope.cancelEditing = function (file) {
                file.fileEditing = false;
            };
            $scope.selectFile = function (moduleType, itemClientId, detailClientId) {
                $scope.$emit('event:FileSelected', moduleType, itemClientId, detailClientId);
            };
            $scope.continueUpload = function (file, itemIndex, detailIndex, detailID) {
                consultationService.ReUploadFileItem(file.uniqueId).success(function () {
                    file.error = false;

                    workerManager(file, itemIndex, detailIndex, detailID);

                }).error(function (errorMsg) {
                    $log.error(errorMsg);
                });

            };

        })();
    }
]);
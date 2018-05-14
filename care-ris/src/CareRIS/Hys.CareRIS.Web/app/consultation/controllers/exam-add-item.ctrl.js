consultationModule.controller('ExamAddItemController', ['$log', '$scope', 'consultationService', 'loginContext', '$http', 'loginUser', '$translate', 'openDialog', 'clientAgentService', '$rootScope',
    function ($log, $scope, consultationService, loginContext, $http, loginUser, $translate, openDialog, clientAgentService, $rootScope) {
        'use strict';
        $log.debug('ExamAddItemController.ctor()...');

        $scope.isMobile = $rootScope.browser.versions.mobile;
        $scope.ERMItemId = RIS.newGuid();
        $scope.damServerSaveFile = loginUser.damWebApiUrl + '/api/v1/upload/files?ERMItemId="' + $scope.ERMItemId + '"';
        $scope.clientAgentError = false;
        $scope.isLoading = true;

        $translate('CheckNo', { name: $scope.module.title }).then(function (checkNo) {
            $scope.checkNo = checkNo;
        });
        $translate('CheckNoRequireErrorMsg', { name: $scope.module.title }).then(function (checkNoRequireErrorMsg) {
            $scope.checkNoRequireErrorMsg = checkNoRequireErrorMsg;
        });
        $translate('CheckNoMaxLengthErrorMsg', { name: $scope.module.title }).then(function (checkNoMaxLengthErrorMsg) {
            $scope.checkNoMaxLengthErrorMsg = checkNoMaxLengthErrorMsg;
        });

        if ($scope.operateItem && angular.isArray($scope.operateItem.itemDetails) && $scope.operateItem.itemDetails.length > 0) {
            _.forEach($scope.operateItem.itemDetails, function (detail) {
                detail.deleted = false;
            });
        }

        $scope.item = $scope.operateItem;
        $scope.item.fileList = [];

        var examSectionOld = $scope.item.examSection;
        $scope.examSectionList = [{ name: '' }];

        consultationService.getDepartments().success(function (data) {
            data && data.length > 0 && ($scope.examSectionList = data);
            if (!examSectionOld && $scope.item && loginUser.user.department) {
                $scope.item.examSection = loginUser.user.department.name;
            } else {
                $scope.item.examSection = examSectionOld;
            }

            $scope.isLoading = false;

        }).error(function (errorMsg) {
            $log.error(errorMsg);
            $scope.isLoading = false;
        });

        $scope.cancellItem = function () {
            $scope.cancel();
        };

        $scope.onSuccess = function (e) {
            var response = e.XMLHttpRequest.responseText.replace('"', '');
            var fileItems = response.split(',');
            var files = [];
            _.forEach(e.files, function (f) {
                files.push({
                    fileType: f.rawFile.type,
                    fileName: f.name,
                    selected: true,
                    detailedId: fileItems[0]
                });
            });

            $scope.$apply(function () {
                $scope.item.fileList.push.apply($scope.item.fileList, files);
            });
        };

        $scope.onError = function (e) {
            $log.error(e.XMLHttpRequest.responseText);
        };

        $scope.onUpload = function (e) {
            $log.debug('Excute file upload..');
        };

        $scope.confirmItem = function () {
            if ($scope.examAddItemForm.$valid)
                $scope.confirm({ item: $scope.item });
        };

        $scope.openFileManager = function (fileOrFolder) {
            var methodName = '';

            switch (fileOrFolder) {
                case 'file':
                    methodName = 'OpenSelectFiles';
                    break;
                case 'folder':
                    methodName = 'OpenSelectFolder';
                    break;
                default:
                    $scope.clientAgentError = true;
                    return;
            }

            clientAgentService.openFile(methodName).success(function (data) {
                $scope.clientAgentError && ($scope.clientAgentError = false);
                $scope.addFile(fileOrFolder, data);
            }).error(function () {
                $scope.clientAgentError = true;
            });

        };

        $scope.addFile = function (fileOrFolder, absolutePath) {
            if (!absolutePath) return;
            var fileListLen = $scope.item.fileList.length, tmpFile, tmpNewFile;
            var files = [], filesLen, added;

            var filePathProcesser = function (pathString) {
                var reg = /^.*\\([^\\]*\.([^\.]+))$/g;
                var paths = pathString.split(',');
                var fileName, fileType, path;
                var pathsCount = paths.length;

                for (var i = 0; i < pathsCount; ++i) {
                    path = paths[i];
                    fileName = '', fileType = '';

                    path.replace(reg, function ($0, $1, $2) {
                        $1 && (fileName = $1);
                        $2 && (fileType = $2);
                    });

                    if (fileName && fileType) {
                        fileType == 'dcm' && (fileType = 'dicom');
                        files.push({
                            fileType: fileType,
                            fileName: fileName,
                            path: path,
                            selected: true
                        });
                    }
                }
            };
            var folderPathProcesser = function (forderString) {
                files.push({
                    fileType: 'folder',
                    fileName: forderString,
                    path: forderString,
                    selected: true
                });
            };
            switch (fileOrFolder) {
                case 'folder':
                    folderPathProcesser(absolutePath);
                    break;
                case 'file':
                    filePathProcesser(absolutePath);
                    break;
            }
            filesLen = files.length;
            for (var n = 0; n < filesLen; ++n) {
                tmpNewFile = files[n];

                for (var i = 0; i < fileListLen; ++i) {
                    tmpFile = $scope.item.fileList[i];
                    added = false;
                    if (tmpFile.path === tmpNewFile.path) {
                        tmpFile.selected = true;
                        added = true;
                    } else {
                        tmpFile.selected = false;
                    }
                }

                if (added) {
                    files.splice(n, 1);
                    n--;
                    filesLen--;
                };
            }

            if (files.length > 0) {
                _.forEach($scope.item.itemDetails, function (detail) {
                    detail.selected = false;
                });
                Array.prototype.splice.apply($scope.item.fileList, [0, 0].concat(files));
            }
        };

        $scope.fileSelected = function (item) {
            _.forEach($scope.item.fileList, function (file) {
                file.selected = false;
            });
            _.forEach($scope.item.itemDetails, function (detail) {
                detail.selected = false;
            });
            item.selected = true;
        };

        $scope.deleteFile = function (index) {
            $scope.item.fileList.splice(index, 1);
        };
        $scope.deleteItemDetails = function (detail, index) {
            detail.deleted = true;
            detail.deletedIndex = index;
        };
    }
]);
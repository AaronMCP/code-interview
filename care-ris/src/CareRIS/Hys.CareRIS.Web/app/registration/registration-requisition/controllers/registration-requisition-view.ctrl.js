registrationModule.controller('RegistrationRequisitionController', [
    '$log', '$scope', 'enums', '$rootScope', '$location', '$window', 'registrationService', 'loginContext',
    '$http', 'risDialog', '$translate', '$modal', 'registrationUtil', 'reportService', '$timeout', 'commonMessageHub', 'application', 'configurationService', 'clientAgentService','openDialog',
    function ($log, $scope, enums, $rootScope, $location, $window, registrationService, loginContext,
        $http, risDialog, $translate, $modal, registrationUtil, reportService, $timeout, commonMessageHub, application, configurationService, clientAgentService, openDialog) {
        'use strict';
        $log.debug('RegistrationRequisitionController.ctor()...');

        var erNo;
        var title = $translate.instant("Tips");
        var urlAddress = loginContext.printHtml.substr(0, loginContext.printHtml.lastIndexOf('/') + 1);

        var endScan = function () {
            if ($scope.isViewRequisition) {
                $scope.modalInstance().dismiss();
                registrationService.clearTempFile(erNo).success(function (result) {
                });
            }
            else {
                var result = ($scope.fileList && $scope.fileList.length > 0) ? $scope.fileList : null;
                $scope.modalInstance().close(result);
            }
        };

        $scope.saveTempImage = function (base64Str) {
            var imageData,
                 fileType = '.jpg',
                 newFileName,
                 firstIndex = '001';
            // ERNO+'00N' Format
            if ($scope.fileList.length === 0) {
                newFileName = erNo + firstIndex + fileType;
            }
            else {
                var maxFileName = _.last(_.sortBy($scope.fileList, 'fileName')).fileName;
                // name rule:ERNO+"00N"
                var maxIndex = parseInt(maxFileName.substr(maxFileName.length - 7, 3).replace(/^0+/, ''));
                newFileName = erNo + pad((maxIndex + 1).toString(), 3) + fileType;
            }
            imageData = { fileName: newFileName, base64Str: base64Str, imageQualityLevel: application.clientConfig.scanQualityLevel, isUpdate: false };
            $scope.isSavingImg = true;
            registrationService.saveTempImage(imageData).success(function (data) {
                insertToFileList(data);
                $scope.selectScanFile(data);
                $scope.isSavingImg = false;
            }).error(function (error) {

            });;
        };

        $scope.updateImage = function (updateImgData) {
            return registrationService.saveTempImage(updateImgData);
        };

        var insertToFileList = function (imageData) {
            $scope.fileList.push(imageData);
        };

        $scope.delete = function () {
            if (!$scope.selectedFile) return;
            var selectedFile = $scope.selectedFile;
            registrationService.deleteImage(selectedFile.fileName, selectedFile.relativePath || "", selectedFile.requisitionID || "").success(function (result) {
                if (!result) {//failed
                    var errorMsg = $translate.instant("DeleteErrorMsg");
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, errorMsg);
                } else {
                    $scope.fileList = _.reject($scope.fileList, { fileName: selectedFile.fileName });
                    if ($scope.fileList.length !== 0) {
                        $scope.selectScanFile($scope.fileList[0]);
                    } else {
                        $scope.clearphoto();
                    }
                }
            });
        };

        // format to "00N"
        var pad = function (str, max) {
            str = str.toString();
            return str.length < max ? pad("0" + str, max) : str;
        };

        var initCamera = function () {
            // not ipad 
            if (!$scope.isSafari) {
                openVideo();
            }
            else {
                $scope.injectVideoEvent();
            }
        };

        var openVideo = function () {
            clientAgentService.openVideo().success(function (result) {
                $log.log("open video success.");
                $scope.isStreaming = true;
                $scope.$broadcast('focusScan');
            })
        .error(function () {

        });
        };

        $scope.reInitCamera = function() {
            clientAgentService.restartClient();
        };

        $scope.capturePhoto = function () {
            $scope.isSavingImg = true;
            var param = { width: 500 };
            clientAgentService.capturePhoto(param).success(function (result) {
                $scope.saveTempImage(result);
            })
        .error(function () {
            $scope.isSavingImg = false;
            openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Tips'), $translate.instant('CapatureError'));
        });
        };


        $scope.$on('$destroy', function () {
            if (!$scope.isViewRequisition) {
                // Make sure that the vedio is closed
                clientAgentService.closeVideo().success(function (result) {
                    $log.log("close video success.");
                }).error(function () { });
            }
        });
        var sleep=  function (d) {
            for (var t = Date.now() ; Date.now() - t <= d;);
        }
        //关闭网页时关闭摄像头
        window.onbeforeunload = onbeforeunload_handler;
        function onbeforeunload_handler() {
            clientAgentService.closeVideo().success(function (result) {
                $log.log("close video success.");
            })
            sleep(500);
        }

        (function initialize() {
            $scope.scannedImages = [];
            $scope.isDisableScan = true;
            $scope.endScan = endScan;

            $scope.openVideo = openVideo;
            $timeout(function () {
                $scope.isViewRequisition = $scope.args.isViewRequisition || false;
                $scope.requisitionTitle = $translate.instant($scope.isViewRequisition ? 'ViewRequisition' : 'ScanRequisition');
                $scope.fileList = $scope.args.requisitionFiles || [];
                erNo = $scope.args.erNo || '';
                $scope.endScanInfo = $scope.isViewRequisition ? $translate.instant('Close') : $translate.instant('SaveAndEndScan');
                if (!$scope.isViewRequisition) {
                    initCamera();
                }
                if ($scope.fileList.length > 0) {
                    $scope.selectScanFile($scope.fileList[0]);
                }
            });

        })();
    }]);
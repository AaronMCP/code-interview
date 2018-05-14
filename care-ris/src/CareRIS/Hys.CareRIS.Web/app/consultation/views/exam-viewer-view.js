consultationModule.directive('examViewerView', ['$log', 'application', '$compile', '$window', '$translate', '$http', 'loginContext', 'clientAgentService', 'consultationService', 'loginUser', 'enums', '$rootScope',
function ($log, application, $compile, $window, $translate, $http, loginContext, clientAgentService, consultationService, loginUser, enums, $rootScope) {
    $log.debug('examViewerView.ctor()...');
    return {
        restrict: 'E',
        templateUrl: '/app/consultation/views/exam-viewer-view.html',
        controller: 'ExamViewerController',
        scope: {
            treeSource: '=',
            iniItem: '='
        },
        replace: true,
        link: function (scope, element) {
            var viwerBody = element.find('.exam-viewer-body');
            var displayer = element.find('.exam-viewer-file-container');
            var downloadStr = $translate.instant('downloadFile');
            var tipsForViewPdf = $translate.instant('TipsForViewPdf');
            var downloadPdfDirectly = $translate.instant('DownloadPdfDirectly');
            var canvas;

            scope.isMobile = $rootScope.browser.versions.mobile;
            scope.isSystem5DX = false;

            var drawImage = function (src) {
                scope.fileLoading = true;
                if (displayer.not(':empty')) displayer.empty();

                canvas = $('<canvas></canvas>').appendTo(displayer);
                canvas = canvas.get()[0];
                var width = displayer.width();
                var height = displayer.height();
                var heightOffset = 50;

                canvas = new fabric.Canvas(canvas, { width: width, height: height });

                fabric.util.loadImage(src, function (img) {
                    //var imgPara = {};
                    //if (img.width > width - 20) {
                    //    imgPara.scaleX = width / (img.width + 20);
                    //    imgPara.left = 10;
                    //} else {
                    //    imgPara.left = width / 2 - img.width / 2;
                    //}
                    //if (img.height > height - 100) {
                    //    imgPara.scaleY = height / (img.height + 100);
                    //    imgPara.top = 50;
                    //} else {
                    //    imgPara.top = height / 2 - img.height / 2;
                    //}

                    var legimg = new fabric.Image(img);

                    canvas.add(legimg);
                    canvas.renderAll();
                    scope.$apply(function () {
                        scope.fileLoading = false;
                    });
                });

                if (scope.isMobile) {
                    scope.fileLoading = false;
                }
            };
            var displayWord = function (src) {
                var url = loginContext.serverUrl + '/FileDownload/Word2Html';
                var param = { wordUri: src };
                scope.fileLoading = true;
                $http.post(url, param).success(function (res) {
                    if (res.success) {
                        var targetUrl = loginContext.serverUrl + res.url;
                        displayer.append('<iframe class="exam-viewer-iframe" src="' + targetUrl + '" width="100%" height="100%" />');
                    } else {
                        displayer.append('<div class="exam-viewer-message">' + $translate.instant('NoOfficeInstalled') + '</div>');
                    }

                    if (scope.isMobile) {
                        scope.fileLoading = false;
                    }
                }).error(function (errorMsg) {
                    console.log(errorMsg);
                    scope.fileLoading = false;
                }).finally(function () {
                    scope.fileLoading = false;
                });

                if (scope.isMobile) {
                    scope.fileLoading = false;
                }
            };

            scope.fullScreen = function () {
                viwerBody.addClass('full-screen');
                if (canvas) {
                    canvas.setHeight(displayer.height());
                    canvas.setWidth(displayer.width());
                }
                scope.isFullScreen = true;
            }
            scope.restore = function () {
                viwerBody.removeClass('full-screen');

                if (canvas) {
                    canvas.setHeight(displayer.height());
                    canvas.setWidth(displayer.width());
                }
                scope.isFullScreen = false;
            };

            scope.viewDicom = function () {
                $window.open(scope.dicomViewerSrc);
            }

            scope.setIsSystem5DX = function () {
                var userSetting = {
                    userSettingID: '',
                    roleID: '',
                    userID: loginUser.user.uniqueID,
                    type: enums.UserSettingType.IsSystem5DX,
                    settingValue: '0'
                };

                if (scope.isSystem5DX) {
                    userSetting.settingValue = '1';
                }

                consultationService.saveUserSetting(userSetting).success(function (data) {
                });
            }

            scope.showFile = function (item) {
                var src = item.path;
                var fileType = '', reg = /^.*\.([^\.]+)$/g;
                var wmpInstalled = function (src) {
                    /// <summary>
                    /// detect if Windows Media Player is installed
                    /// </summary>
                    /// <param name="src"></param>
                    try {
                        var installed = false;
                        wmpObj = false;
                        if ($window.navigator.plugins && $window.navigator.plugins.length) {
                            var pluginsLen = $window.navigator.plugins.length;
                            for (var i = 0; i < pluginsLen; ++i) {
                                var plugin = $window.navigator.plugins[i];
                                if (plugin.name.indexOf("Windows Media Player") > -1) {
                                    installed = true;
                                    break;
                                }
                            }
                        } else {
                            execScript('on error resume next: wmpObj = IsObject(CreateObject("MediaPlayer.MediaPlayer.1"))', 'VBScript');
                            installed = wmpObj;
                        }

                        return installed;
                    }
                    catch (error) {
                        return false;
                    }
                };
                var createDownloadLink = function (linkSrc) {
                    return $('<div class="exam-viewer-downloadlink"><a href="' + linkSrc + '" target="_blank">' + downloadStr + '</a></div>');
                };
                var createPDFReader = function (src, displayer) {
                    displayer.empty();
                    scope.fileLoading = true;
                    if (scope.isMobile) {
                        scope.fileLoading = false;
                        displayer.append('<div class="exam-viewer-message">' + $translate.instant('NotSupportMobile') + '</div>');
                    } else {
                        clientAgentService.ifAdobeReaderInstalled().success(function (installed) {
                            if (!installed) {
                                displayer.append('<div class="exam-viewer-downloadlink">' + tipsForViewPdf + '<a href="' + src + '" target="_blank">' + downloadPdfDirectly + '</a></div>');
                            } else {
                                displayer.append('<embed src="' + src + '" width="100%" height="100%" />');
                            }
                        }).finally(function () {
                            scope.fileLoading = false;
                        });
                    }
                };
                var createObject = function (src) {
                    //var wmpRead = wmpInstalled(src);
                    //if (wmpRead) {
                    //    var obj = [
                    //         '<object id="MediaPlayer" class="attachment" classid="CLSID:6BF52A50-394A-11D3-B153-00C04F79FAA6" codebase="http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,4,5,715" standby="Loading Microsoft Windows Media Player components..." type="application/x-oleobject">'
                    //            , '<param name="FileName" value="', src, '">'
                    //            , '<param name="AutoSize" value="true">'
                    //            , '<param name="AutoStart" value="false">'
                    //            , '<param name="Balance" value="0">'
                    //            , '<param name="DisplaySize" value="0">'
                    //            , '<param name="Mute" value="false">'
                    //            , '<param name="PlayCount" value="0">'
                    //            , '<param name="Rate" value="1.0">'
                    //            , '<param name="ShowAudioControls" value="true">'
                    //            , '<param name="ShowControls" value="true">'
                    //            , '<param name="ShowDisplay" value="false">'
                    //            , '<param name="ShowStatusBar" value="true">'
                    //            , '<param name="ShowTracker" value="true">'
                    //            , '<param name="StretchToFit" value="false">'
                    //            , '<param name="TransparentAtStart" value="false">'
                    //            , '<param name="Volume" value="100">'
                    //            , '<embed type="application/x-mplayer2" name="mediaplayer" pluginspage="http://www.microsoft.com/Windows/MediaPlayer" src="', src, '"'
                    //            , 'class="attachment" autosize="1" autostart="0" balance="0" displaysize="0" mute="0" playcount="0" rate="1.0" showaudiocontrols="1" '
                    //            , 'showcontrols="1" showdisplay="0" showstatusbar="1" showtracker="1" stretchtofit="0" transparentatstart="0" volume="100" />'
                    //        , '</object>'
                    //    ].join("");
                    //    return $(obj);
                    //}
                    //else {
                    return createDownloadLink(src);
                    //}
                };
                var errorFunc = function () {
                    $(this).replaceWith(createObject(src));
                };

                displayer.empty();
                if (!item.ready) {
                    displayer.append('<div class="exam-viewer-message">' + $translate.instant('FileUploadNotComplete') + '</div>');
                    return;
                }
                if (!src) return;
                var srcList = src.split(',');
                if (srcList.length == 2) {//dicom
                    displayer.append('<iframe class="exam-viewer-iframe" src="' + srcList[0] + '" width="100%" height="100%" />');
                    scope.isDicom = true;
                    scope.dicomViewerSrc = srcList[1];
                    var userSettingQuery = { roleid: '', userid: loginUser.user.uniqueID, type: enums.UserSettingType.IsSystem5DX };

                    if (!scope.isMobile) {
                        consultationService.getUserSetting(userSettingQuery).success(function (data) {
                            if (data && data.settingValue == '1') {
                                scope.isSystem5DX = true;
                                scope.viewDicom();
                            }
                        });
                    }
                    return;
                } else {
                    scope.isDicom = false;
                }

                scope.isImage = false;
                src.replace(reg, function ($0, $1) { fileType = String($1).toLowerCase() });

                switch (fileType) {
                    case 'pdf':
                        createPDFReader(src, displayer);
                        break;
                    case 'txt':
                        displayer.append('<iframe class="exam-viewer-iframe" src="' + src + '" width="100%" height="100%" />');
                        break;
                    case 'jpg':
                    case 'jpeg':
                    case 'bmp':
                    case 'gif':
                    case 'png':
                        scope.isImage = true;
                        drawImage(src);
                        break;
                    case 'wav':
                    case 'mp3':
                        $('<audio class="attachment" src="' + src + '" preload controls="controls""></audio>').append(createObject(src))
                            .appendTo(displayer)
                            .one("error", errorFunc);
                        break;
                    case 'ogg':
                    case 'mp4':
                    case 'webm':
                        $('<video class="attachment" src="' + src + '" preload controls="controls" ></video>').append(createObject(src))
                            .appendTo(displayer).one("error", errorFunc);
                        break;
                    case 'asf':
                    case 'wma':
                    case 'wmv':
                    case 'asx':
                    case 'wax':
                    case 'wvx':
                    case 'wmx':
                    case 'wpl':
                    case 'dvr-ms':
                    case 'wmd':
                    case 'avi':
                    case 'mpg':
                    case 'mpeg':
                    case 'm1v':
                    case 'mp2':
                    case 'mpa':
                    case 'mpe':
                    case 'm3u':
                    case 'mid':
                    case 'midi':
                    case 'rmi':
                    case 'aif':
                    case 'aifc':
                    case 'aiff':
                    case 'au':
                    case 'snd':
                    case 'cda':
                    case 'ivf':
                    case 'wmz':
                    case 'wms':
                    case 'mov':
                    case 'm4a':
                    case 'm4v':
                    case 'mp4v':
                    case '3g2':
                    case '3gp2':
                    case '3gp':
                    case '3gpp':
                    case 'aac':
                    case 'adt':
                    case 'adts':
                    case 'm2ts':
                        createObject(src).appendTo(displayer);
                        break;
                    case 'doc':
                    case 'docx':
                        displayWord(src);
                        break;
                    case 'ppt':
                    case 'xls':
                        createDownloadLink(src).appendTo(displayer);
                        break;
                    default:
                        $window.open(src);
                        break;
                };
            };
        }
    };
}]);
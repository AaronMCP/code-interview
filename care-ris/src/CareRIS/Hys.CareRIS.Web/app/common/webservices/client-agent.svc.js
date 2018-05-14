webservices.factory('clientAgentService', ['$http', '$q', '$translate', 'loginContext', 'openDialog', '$window', 'configurationService', 'application', 'busyRequestNotificationHub', '$timeout',
    function ($http, $q, $translate, loginContext, openDialog, $window, configurationService, application, busyRequestNotificationHub, $timeout) {
        'use strict';
        var agentInstalled;
        var agentApiHostUrl = loginContext.agentHost + '/api';

        function jsonpParam(params) {
            var param = params || {};
            return '?' + window.RIS.queryString(param);
        }

        var agentDialog = null;
        function internalDonload() {
            if (!agentDialog) {
                var downloadUrl = loginContext.serverUrl + 'FileDownload/CareAgent?version=' + loginContext.agentVersion;
                var msgTemplate = '请启动CareAgent；若未安装，请点击<a href="' + downloadUrl + '">下载</a>并安装CareAgent.';
                agentDialog = openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), msgTemplate, function () { }, true);
            }

            agentDialog.show();
        }

        var agentNotAvailable = false;
        var requestQueue = [];

        var getRequest = function (complete) {
            var requestEntity = requestQueue.shift();
            busyRequestNotificationHub.requestStarted();
            var method = requestEntity.method;
            var config = requestEntity.httpConfig || {};
            config.headers = {
                Version: loginContext.agentVersion,
                Update: loginContext.serverUrl + 'CareAgentCast.xml'
            };

            var request;
            switch (method) {
                case 'get':
                case 'delete':
                    request = $http[method](agentApiHostUrl + requestEntity.route + jsonpParam(requestEntity.data), config);
                    break;
                case 'post':
                case 'put':
                    request = $http[method](agentApiHostUrl + requestEntity.route, requestEntity.data, config);
                    break;
                default:
                    throw new Error("Unknown request method");
            }

            request.error(function (error) {
                if (!error) {
                    agentNotAvailable = true;
                    internalDonload();
                    requestQueue.length = 0;
                    return;
                }

                if (angular.isFunction(requestEntity.error)) {
                    requestEntity.error.apply({}, arguments);
                }
            });
            request.success(function () {
                agentNotAvailable = false;
                if (angular.isFunction(requestEntity.success)) {
                    requestEntity.success.apply({}, arguments);
                }
            });
            request.finally(function () {
                busyRequestNotificationHub.requestEnded();
                if (angular.isFunction(requestEntity.finally)) {
                    requestEntity.finally.apply({}, arguments);
                }
                complete();
            });
        };

        var working = false;
        var requestWorker = function () {
            if (requestQueue.length < 1) {
                working = false;
                return;
            }
            working = true;
            getRequest(function () {
                requestWorker();
            });
        };

        function request(params) {
            var requestEntity = angular.isString(params) ?
                {
                    route: params,
                    method: 'get',//get,post,put,delete
                    data: arguments[1] || {},
                    isBusyRequest: true
                } :
                (params || {
                    route: '',
                    data: {},
                    method: 'get',//get,post,put,delete
                    httpConfig: {},
                    isBusyRequest: true
                });

            var fakeResult = {
                success: function (callback) {
                    requestEntity.success = callback;
                    return fakeResult;
                },
                'finally': function (callback) {
                    requestEntity.finally = callback;
                    return fakeResult;
                },
                'catch': function (errorCallback) {
                    requestEntity.catch = errorCallback;
                    return fakeResult;
                },
                error: function (errorCallback) {
                    requestEntity.error = errorCallback;
                    return fakeResult;
                }
            }

            requestQueue.push(requestEntity);

            if (!working) {
                requestWorker();
            }

            return fakeResult
        }

        return {
            //      post demo
            //postTest: function (data) {
            //    return request({
            //        route: 'route/to/action',
            //        method: 'post',
            //        data: data
            //    });
            //},
            GetProcessID: function () {
                return request('/risprotasks/GetProcessID');
            },
            GetAllPrinters: function () {
                return request('/risprotasks/AllPrinters');
            },
            GetDefaultPrinter: function () {
                return request('/risprotasks/DefaultPrinter');
            },
            getDam: function (data) {
                return request('/risprotasks/GetSrcInfo', data);
            },
            startMeeting: function (data) {
                return request('/risprotasks/StartMeeting', data);
            },
            loginIm: function (data) {
                return request('/risprotasks/loginIm', data);
            },
            openIm: function (data) {
                return request('/risprotasks/OpenIm', data);
            },
            logoutIm: function () {
                return request('/risprotasks/LogoutIm');
            },
            ifAdobeReaderInstalled: function () {
                return request('/risprotasks/IfAdobeReaderInstalled');
            },
            GetProcessIDForDam: function (data) {
                return request('/risprotasks/GetSrcInfo', data);
            },
            openFile: function (methodName) {
                return request('/risprotasks/' + methodName);
            },
            printReport: function (data) {
                return request('/risprotasks/PrintReport', data);
            },
            previewReport: function (data) {
                return request('/risprotasks/ShowHtmlData', data);
            },
            openVideo: function () {
                return request('/risprotasks/openVideo');
            },
            capturePhoto: function (data) {
                return request('/risprotasks/capturePhoto', data);
            },
            closeVideo: function () {
                return request('/risprotasks/closeVideo');
            },
            printOther: function (data) {
                return request('/risprotasks/PrintOtherReport', data);
            },
            getCardNo: function () {
                return request(loginContext.cardService);
            },
            getDicoms: function (query) {
                return request('/risprotasks/searchDICOMData', query);
            },
            deleteDicoms: function (data) {
                return request('/risprotasks/deleteDICOM', data);
            },
            loadImage: function (data) {
                return request('/risprotasks/ShowDICOMViewer', data);
            },
            checkDicom: function (data) {
                return request('/risprotasks/checkDICOM', data);
            },
            openConvertWord: function (data) {
                return request('/risprotasks/openConvertWord', data);
            },
            viewImage: function (data) {
                return request('/risprotasks/viewimage', /*{
                    patientId: '201201061940',
                    accNo: '9275000456789',
                    studyId: '1.3.6.1.4.1.5962.99.1.2058824253.112554731.1337793678158.14474.0'
                }*/data);
            },
            pacsConfig: function () {
                return request('/risprotasks/pacsConfig');
            },
            editPacsConfig: function (data) {
                return request({
                    route: '/risprotasks/editPacsConfig',
                    method: 'post',
                    data: data
                });
            }
        }
    }]);
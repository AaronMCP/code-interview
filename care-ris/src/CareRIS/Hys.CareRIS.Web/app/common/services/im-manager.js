commonModule.factory('imManager', ['$log', '$http', 'configurationService', 'loginContext', '$window', '$q', 'clientAgentService', 'commonMessageHub',
function ($log, $http, configurationService, loginContext, $window, $q, clientAgentService, commonMessageHub) {
    'use strict';

    return {
        loginIm: function () {
            var defer = $q.defer();
            var data = {
                ln: loginContext.userName,
                rn: loginContext.roleName,
                st: loginContext.site,
                p: loginContext.password
            };
            if (loginContext.roleName === 'Clinician') {
                defer.reject('Clinician can not login IM');
                return defer.promise;
            }

            commonMessageHub.publish("pub-sub:logoutIM", { userName: loginContext.userName });

            var imConfigUri = loginContext.serverUrl + '/Common/ImConfig';
            $http.get(imConfigUri).success(function (imConfig) {
                clientAgentService.imSetting({ config: JSON.stringify(imConfig) }).success(function () {
                    clientAgentService.loginIm(data).success(function () {
                        defer.resolve();
                    }).error(function (errorMsg) {
                        defer.reject(errorMsg);
                    });
                }).error(function (errorMsg) {
                    defer.reject(errorMsg);
                });
            }).error(function (errorMsg) {
                defer.reject(errorMsg);
            });

            return defer.promise;
        },
        openIm: function () {
            commonMessageHub.publish("pub-sub:logoutIM", { userName: loginContext.userName });

            var defer = $q.defer();
            var data = {
                ln: loginContext.userName,
                rn: loginContext.roleName,
                st: loginContext.site,
                p: loginContext.password
            };
            clientAgentService.openIm(data).success(function () {
                defer.resolve();
            }).error(function (errorMsg) {
                defer.reject(errorMsg);
            });
            return defer.promise;
        },
        logoutIm: function () {
            var defer = $q.defer();

            clientAgentService.logoutIm().success(function () {
                defer.resolve();
            }).error(function (errorMsg) {
                defer.reject(errorMsg);
            });
            return defer.promise;
        },
        coViewMsgTip: false,
        coViewMsg: function () {
            var defer = $q.defer();
            clientAgentService.coViewMsg().success(function (msg) {
                defer.resolve(msg);
            }).error(function (errorMsg) {
                defer.reject(errorMsg);
            });
            return defer.promise;
        }
    };
}
]);
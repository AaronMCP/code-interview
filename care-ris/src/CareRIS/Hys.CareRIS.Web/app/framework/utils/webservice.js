// apiInterceptor is responsible to handle the aspect of each request and response.
webservices.factory('apiInterceptor', ['$q', 'loginContext', '$log', '$window', '$injector', 'risRelogin',
    function ($q, loginContext, $log, $window, $injector, risRelogin) {
        'use strict';

        $log.debug('apiInterceptor.ctor()...');

        var redirectToLogOut = function () {
            if (loginContext.auth && loginContext.auth.authorized) {
                loginContext.auth.authorized = false;
            }
            risRelogin.open();
        };

        if (!loginContext.auth || !loginContext.auth.authorized) {
            redirectToLogOut();
            return;
        }
        var tokenType = loginContext.auth.token_type;
        var webApiHostUrl = loginContext.apiHost + "/api/v1";

        return {
            //token save to services for further usage
            tokenType: tokenType,
            apiToken: loginContext.auth.access_token,
            webApiHostUrl: webApiHostUrl,

            // On request success
            request: function (config) {
                // Check if this request is a web api call, we should only update url and header for each web api request.
                // Because angular will use the $http service as well internally, such as ng-include to request a html file from server.
                // Set this config to send request to a different web server will break the functions of angularJS.
                if (config.isWebApiRequest) {
                    config.url = webApiHostUrl + config.url;
                    config.headers = config.headers || {};
                    config.headers.Authorization = tokenType + ' ' + loginContext.auth.access_token;
                    config.withCredentials = true;
                }
                return config;
            },
            // On request failure
            requestError: function (rejection) {
                $log.error(rejection); // Contains the data about the error on the request.

                // Return the promise rejection.
                return $q.reject(rejection);
            },
            // On response success
            response: function (response) {
                if (response.status === 401) {
                    redirectToLogOut();
                }
                return response || $q.when(response);
            },
            // On response failture
            responseError: function (rejection) {
                if (rejection.status === 401) {
                    redirectToLogOut();
                }

                // Return the promise rejection.
                return $q.reject(rejection);
            }
        };
    }
])
    .factory('requestNotificationInterceptor', ['$q', '$injector', '$log', function ($q, $injector, $log) {
        'use strict';

        $log.debug('busyRequestNotificationInterceptor.ctor()...');

        var busyRequestNotificationHub;
        var busyRequestUrl = null;

        function onRequestStarted(config) {
            if (config.isWebApiRequest && config.isBusyRequest) {
                $log.debug('busyRequestNotificationInterceptor: Start busy request ' + config.url);
                busyRequestNotificationHub = busyRequestNotificationHub || $injector.get('busyRequestNotificationHub');
                busyRequestNotificationHub.requestStarted();
                busyRequestUrl = config.url;
            }
        }

        function onRequestEnded(config) {
            if (busyRequestUrl && config.url === busyRequestUrl) {
                $log.debug('busyRequestNotificationInterceptor: Finish busy request ' + config.url);
                busyRequestNotificationHub = busyRequestNotificationHub || $injector.get('busyRequestNotificationHub');
                busyRequestNotificationHub.requestEnded();
                busyRequestUrl = null;
            }
        }

        return {
            // On request success
            request: function (config) {
                onRequestStarted(config);
                return config;
            },
            // On request failure
            requestError: function (rejection) {
                onRequestEnded(rejection.config);
                return $q.reject(rejection);
            },
            // On response success
            response: function (response) {
                onRequestEnded(response.config);
                return response || $q.when(response);
            },
            // On response failture
            responseError: function (rejection) {
                onRequestEnded(rejection.config);
                return $q.reject(rejection);
            }
        }
    }]);

webservices.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('apiInterceptor');
    $httpProvider.interceptors.push('requestNotificationInterceptor');
}]);

// All web api request should use this service to create its config for request.
webservices.factory('apiConfig', ['$log', function ($log) {
    return {
        create: function (config) {
            var cfg = config || {};
            cfg.isWebApiRequest = true;
            return cfg;
        }
    };
}]);
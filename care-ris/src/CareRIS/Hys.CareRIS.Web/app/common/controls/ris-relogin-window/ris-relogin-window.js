commonModule
    .service('risRelogin', ['$rootScope',
        function ($rootScope) {
            this.open = function () {
                $rootScope.$broadcast('open-relogin-window');
            };
            this.clear = function () {
                $rootScope.$broadcast('close-relogin-window');
            };
        }
    ])
    .directive('risReloginWindow', ['$compile', '$timeout', '$sce',
        function ($compile, $timeout, $sce) {
            return {
                replace: true,
                restrict: 'EA',
                scope: true, // creates an internal scope for this directive
                link: function (scope, elm, attrs) {

                },
                controller: ['$scope', '$element', '$attrs', '$cookies', 'loginContext', '$http', 'risRelogin', 'csdToaster', 'apiConfig', 'busyRequestNotificationHub',
                    function ($scope, $element, $attrs, $cookies, loginContext, $http, risRelogin, csdToaster, apiConfig, busyRequestNotificationHub) {
                        var auth = loginContext.auth;
                        $scope.errorInfo = '';
                        $scope.changeErrorInfo = function () {
                            $scope.errorInfo = '';
                        };
                        $scope.$on('open-relogin-window', function () {
                            if (!$scope.reloginWindow.options.visible) {
                                $scope.reloginWindow.open();
                            }
                        });
                        $scope.$on('close-relogin-window', function () {
                            $scope.reloginWindow.close();
                        });
                        $scope.userName = loginContext.userName;
                        $scope.login = function (failCallBack) {
                            var params = {
                                grant_type: 'password',
                                username: $scope.userName,
                                password: $scope.loginFullPwd,
                                client_id: auth.clientId
                            };
                            busyRequestNotificationHub.requestStarted();
                            $http({
                                url: loginContext.apiHost + '/oauth2/token',
                                method: 'POST',
                                data: $.param(params),
                                headers: {
                                    'Content-Type': 'application/x-www-form-urlencoded'
                                }
                            }).success(function (result) {
                                var newAuth = result;
                                newAuth.clientId = auth.clientId;
                                newAuth.authorized = true;
                                $cookies.auth = JSON.stringify(newAuth);
                                loginContext.auth = newAuth;
                                risRelogin.clear();
                            }).catch(function (error) {
                                if (error.status) {
                                    var errDesc = error.data.error_description;
                                    $scope.errorInfo = errDesc ? errDesc : '用户名或密码错误';
                                } else if (error.request) {
                                    csdToaster.info('网络连接失败！');
                                } else {
                                    csdToaster.info('系统错误，请联系管理员！');
                                }
                            }).finally(function () {
                                busyRequestNotificationHub.requestEnded();
                            });
                        };
                    }
                ],
                templateUrl: '/app/common/controls/ris-relogin-window/ris-relogin-window.html'
            };
        }
    ]);
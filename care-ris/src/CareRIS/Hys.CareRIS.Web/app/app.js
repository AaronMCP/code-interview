window.app = angular.module('app', ['ui.bootstrap', 'ui.bootstrap.tpls', 'ui.event', 'pascalprecht.translate', 'ngMessages',
    'ui.utils', 'ngRoute', 'ngCookies', 'ngSanitize', 'ct.ui.router.extras', 'ui.select', 'webcam', 'ngGrid',
    'app.common', 'app.webservices', 'app.framework', 'app.worklist', 'app.newbooking', 'app.qualitycontrol',
    'app.newregistration', 'app.configuration', 'app.template', 'app.registration', 'app.report', 'app.consultation',
    'app.referral', 'ngAnimate', 'lr.upload', 'ngIdle'
])
    //run blocks are executed after the injector is created and are the first
    //methods that are executed in any Angular app.
    .run(['$log', '$rootScope', '$state',
        function ($log, $rootScope, $state) {
            $log.debug('app.run()...');
            $rootScope.$state = $state;
        }
    ])
    // We always place constant at the beginning of all configuration blocks.
    .constant('application', {
        // region of current application
        region: 'zh-CN',
        // the current logged on user
        currentUser: {},

        configuration: {},
        clientConfig: {},

        // define angular events for $broadcast/$emit and $on
        events: {
            searchRegistration: 'event:searchRegistration',
            advancedSearchRegistration: 'event:advancedSearchRegistration',
            beforeUnload: 'event:beforeUnload',
            navigateToTab: 'event:navigateToTab',
            showNotificationBar: 'event:showNotificationBar',
            consultationSidePanelResize: 'event:consultationSidePanelResize',
            referralSidePanelResize: 'event:referralSidePanelResize',
            consultationRequestShortcutAdded: 'event:consultationRequestShortcutAdded',
            consultationAdvancedSearchRequestsCompleted: 'event:consultationAdvancedSearchRequestsCompleted',
            showRequisition: 'event:showRequisition'
        }
    })
    // only providers and constants should be injected in config block
    .config(['$logProvider', '$translateProvider', '$translatePartialLoaderProvider', 'application', '$stateProvider',
        '$urlRouterProvider', '$cookiesProvider', '$windowProvider', 'IdleProvider', 'KeepaliveProvider',
        function ($logProvider, $translateProvider, $translatePartialLoaderProvider, application, $stateProvider,
            $urlRouterProvider, $cookiesProvider, $windowProvider, IdleProvider, KeepaliveProvider) {
            'use strict';
            var APPCONFIG;
            if (APPCONFIG && APPCONFIG.debug) {
                $logProvider.debugEnabled(true);
            } else {
                $logProvider.debugEnabled(false);
            }

            IdleProvider.timeout(false);
            var $window = $windowProvider.$get();
            application.region = $window.document.documentElement.lang;
            kendo.culture(application.region.split('-')[0].toLowerCase() + '-' + application.region.split('-')[1].toUpperCase());

            // angular-translate configuration
            var configurateTranslation = function () {

                $translateProvider.useLoader('$translatePartialLoader', {
                    // the translation table are organized by language and module under folder 'app/i18n/'
                    urlTemplate: '/app-resources/i18n/{lang}/{part}.json'
                });

                $translateProvider
                    .preferredLanguage(application.region)
                    .fallbackLanguage('en-US');
            };

            // configurate app root state
            var configurateRoute = function () {
                $stateProvider
                    .state({
                        name: 'ris',
                        url: '/ris',
                        'abstract': true,
                        controller: 'AppController',
                        templateUrl: '/app/index.html',
                        resolve: {
                            beforeLoaded: [
                                '$log', '$q', 'loginContext', 'configurationService', 'loginUser', '$window', 'clientAgentService', '$rootScope', 'application', 'enums',
                                function ($log, $q, loginContext, configurationService, loginUser, $window, clientAgentService, $rootScope, application, enums) {
                                    var getConfigurationData = function () {
                                        var deferred = $q.defer();
                                        var configurationData = {};

                                        var profilePromise = configurationService.getProfileValues(loginContext.userId, ['YearNumber', 'MonthNumber', 'DayNumber',
                                            'UpperFirstLetter', 'SeparatePolicy', 'Separator', 'CanEditAppdepartmentAndAppDoctor', 'AutoLoadImage', 'IsPACSIntegration',
                                            'ShowPatientListDlg', 'CanTransferWhenApplyInfoDifferent', 'ScanQualityLevel', 'EnableIM'
                                        ]);
                                        var applyDeptsPromise = configurationService.getApplyDepts(loginContext.site);
                                        var applyDoctorsPromise = configurationService.getApplyDoctors(loginContext.site);
                                        var configurationPromise = configurationService.getDictionariesByTags(loginContext.site, [
                                            enums.dictionaryTag.ageUnit,
                                            enums.dictionaryTag.gender,
                                            enums.dictionaryTag.patientType,
                                            enums.dictionaryTag.priority,
                                            enums.dictionaryTag.chargeType,
                                            enums.dictionaryTag.examineStatus,
                                            enums.dictionaryTag.observation,
                                            enums.dictionaryTag.YesNo,
                                            enums.dictionaryTag.ReferralStatus,
                                            enums.dictionaryTag.ReferralPurpose
                                        ]);

                                        var clientIPPromise = configurationService.getClientIP();

                                        $q.all([profilePromise, applyDeptsPromise, applyDoctorsPromise, configurationPromise, clientIPPromise, loginUser.gerLicensePromise]).then(function (results) {
                                            //profilePromise
                                            var profiles = results[0].data;
                                            configurationData.yearNumber = profiles['YearNumber'.toLowerCase()][0] ? profiles['YearNumber'.toLowerCase()][0].value : null;
                                            configurationData.monthNumber = profiles['MonthNumber'.toLowerCase()][0] ? profiles['MonthNumber'.toLowerCase()][0].value : null;
                                            configurationData.dayNumber = profiles['DayNumber'.toLowerCase()][0] ? profiles['DayNumber'.toLowerCase()][0].value : null;
                                            configurationData.UpperFirstLetter = profiles['UpperFirstLetter'.toLowerCase()][0] ? profiles['UpperFirstLetter'.toLowerCase()][0].value : null;
                                            configurationData.SeparatePolicy = profiles['SeparatePolicy'.toLowerCase()][0] ? profiles['SeparatePolicy'.toLowerCase()][0].value : null;
                                            configurationData.Separator = profiles['Separator'.toLowerCase()][0] ? profiles['Separator'.toLowerCase()][0].value : null;
                                            //configurationData.canEditAppdepartmentAndAppDoctor = profiles['CanEditAppdepartmentAndAppDoctor'.toLowerCase()].value === '1';
                                            configurationData.canEditAppdepartmentAndAppDoctorList = profiles['CanEditAppdepartmentAndAppDoctor'.toLowerCase()];
                                            configurationData.showPatientListDlg = profiles['ShowPatientListDlg'.toLowerCase()][0] ? profiles['ShowPatientListDlg'.toLowerCase()][0].value : null;
                                            configurationData.canTransferWhenApplyInfoDifferentList = profiles['CanTransferWhenApplyInfoDifferent'.toLowerCase()];
                                            configurationData.scanQualityLevel = profiles['ScanQualityLevel'.toLowerCase()][0] ? profiles['ScanQualityLevel'.toLowerCase()][0].value : 100; //default value 100
                                            //autoloadiamge
                                            configurationData.isAutoLoadImage = false;
                                            if (profiles['AutoLoadImage'.toLowerCase()] && profiles['AutoLoadImage'.toLowerCase()].length > 0) {
                                                configurationData.isAutoLoadImage = profiles['AutoLoadImage'.toLowerCase()][0].value === '1';
                                            }

                                            //show autoloadiamge function
                                            configurationData.isPACSIntegration = false;
                                            if (profiles['ISPACSIntegration'.toLowerCase()] && profiles['ISPACSIntegration'.toLowerCase()].length > 0) {
                                                configurationData.isPACSIntegration = profiles['ISPACSIntegration'.toLowerCase()][0].value === '1';
                                                if (!configurationData.isPACSIntegration) {
                                                    configurationData.isAutoLoadImage = false;
                                                }
                                            }

                                            //applyDeptsPromise
                                            configurationData.applyDeptList = results[1].data;
                                            //applyDoctorsPromise
                                            configurationData.applyDoctorList = results[2].data;

                                            angular.forEach(results[3].data, function (dic) {
                                                var values = dic.values;
                                                switch (dic.tag) {
                                                    case enums.dictionaryTag.ageUnit:
                                                        configurationData.ageUnitList = values;
                                                        break;
                                                    case enums.dictionaryTag.gender:
                                                        configurationData.genderList = values;
                                                        break;
                                                    case enums.dictionaryTag.patientType:
                                                        configurationData.patientTypeList = values;
                                                        break;
                                                    case enums.dictionaryTag.priority:
                                                        configurationData.priorityList = values;
                                                        break;
                                                    case enums.dictionaryTag.chargeType:
                                                        configurationData.chargeTypeList = values;
                                                        break;
                                                    case enums.dictionaryTag.examineStatus:
                                                        configurationData.statusList = values;
                                                        break;
                                                    case enums.dictionaryTag.observation:
                                                        configurationData.observationList = values;
                                                        break;
                                                    case enums.dictionaryTag.YesNo:
                                                        configurationData.yesNoList = values;
                                                        break;
                                                    case enums.dictionaryTag.ReferralStatus:
                                                        configurationData.referralStatus = values;
                                                        break;
                                                    case enums.dictionaryTag.ReferralPurpose:
                                                        configurationData.referralPurpose = values;
                                                        break;
                                                }
                                            });

                                            configurationData.clientIP = results[4].data;

                                            deferred.resolve(configurationData);
                                        });

                                        return deferred.promise;
                                    };
                                    var getUserInfo = function () {
                                        var deferred = $q.defer();

                                        configurationService.getCurrentUser()
                                        .success(function (user) {
                                            loginContext.userName = user.loginName;
                                            loginContext.roleName = user.roleName;
                                            loginContext.password = user.password;
                                            loginContext.localName = user.localName;
                                            loginContext.userId = user.uniqueID;
                                            loginContext.domain = user.domain;
                                            loginContext.site = user.site;
                                            loginContext.user = user;

                                            loginUser.getUserPromise(loginContext.userId)
                                                .success(function (data) {
                                                    deferred.resolve(data);
                                                })
                                                .error(function (error) {
                                                    deferred.reject(error);
                                                });
                                        })
                                        .error(function (error) {
                                            $window.location.replace(loginContext.serverUrl);
                                            deferred.reject(error);
                                        });

                                        return deferred.promise;
                                    };

                                    var beforDeferred = $q.defer();

                                    getUserInfo().then(function () {
                                        getConfigurationData().then(function (data) {
                                            application.configuration = data;
                                            $rootScope.statusList = application.configuration.statusList;
                                            $rootScope.ageUnitList = application.configuration.ageUnitList;
                                            $rootScope.yesNoList = application.configuration.yesNoList;
                                            $rootScope.clientIP = application.configuration.clientIP;
                                            $rootScope.referralStatus = application.configuration.referralStatus;
                                            $rootScope.referralPurpose = application.configuration.referralPurpose;
                                            beforDeferred.resolve(data);

                                        }, function (error) { beforDeferred.reject(error); });
                                    }, function (error) { beforDeferred.reject(error); });

                                    return beforDeferred.promise;
                                }
                            ]
                        }
                    });

                $urlRouterProvider.otherwise("/ris/worklist/registrations");
            };

            configurateTranslation();
            configurateRoute();
        }
    ]).
    // IE10 fires input event when a placeholder is defined so that form element is in dirty instead of pristine state 
    // refer to: https://github.com/angular/angular.js/issues/2614
    config(['$provide', function ($provide) {
        $provide.decorator('$sniffer', ['$delegate', function ($sniffer) {
            var msieVersion = parseInt((/msie (\d+)/.exec(angular.lowercase(navigator.userAgent)) || [])[1], 10);
            var hasEvent = $sniffer.hasEvent;
            $sniffer.hasEvent = function (event) {
                if (event === 'input' && msieVersion === 10) {
                    return false;
                }
                hasEvent.call(this, event);
            };
            return $sniffer;
        }]);
    }])
    // Provide the localization function for application and support async load translation table by parts on demand.
    // Note: When trying to adding new translation resource into .json file, please check if the same KEY is existing 
    // in .json files under i18n folder. Because if two parts have the same property, the property value will be overwrited 
    // by the loaded last part.
    // for example,
    // We load app.json first and then load patient.json. "app.json" file contains a property {"Text" : "Test"} 
    // and "patient.json" file contains property {"Text" : "Overwrite Test"} the "Text" on view will be translated to 
    // be "Overwrite Test".
    .factory('appTranslation', ['$log', '$translatePartialLoader', '$translate',
        function ($log, $translatePartialLoader, $translate) {
            'use strict';
            $log.debug('appTranslation.ctor()...');

            var translation = {
                // part names for modules
                // AppPart is for the translation of application level, not for a module for a business logic.
                appPart: 'app',
                // other parts for business logic
                worklist: 'worklist',
                consultation: 'consultation',
                registration: 'registration',
                report: 'report',
                referral: 'referral',

                /// <summary>
                /// async load translation tables into application for specified part names that required for the view.
                /// </summary>
                /// <param name="partNames">part names of array type</param>
                load: function (partNames) {
                    if (!angular.isArray(partNames)) {
                        throw new TypeError('"partNames" should be an array!');
                    }

                    partNames.forEach(function (name) {
                        $translatePartialLoader.addPart(name);
                    });

                    $translate.refresh();
                },
                loadAll: function () {
                    _.map(_.values(translation), function (part) {
                        if (_.isString(part)) {
                            $translatePartialLoader.addPart(part);
                        }
                    });
                    $translate.refresh();
                }
            };
            return translation;
        }
    ])
    .factory('busyRequestNotificationHub', ['$rootScope',
        function ($rootScope) {
            // private notification messages
            var startRequestMessage = '_START_BUSY_REQUEST_';
            var endRequestMessage = '_END_BUSY_REQUEST_';

            // publish start request notification
            var requestStarted = function () {
                $rootScope.$broadcast(startRequestMessage);
            };
            // publish end request notification
            var requestEnded = function () {
                $rootScope.$broadcast(endRequestMessage);
            };
            // subscribe to start request notification
            var onRequestStarted = function ($scope, handler) {
                $scope.$on(startRequestMessage, function (event) {
                    handler();
                });
            };
            // subscribe to end request notification
            var onRequestEnded = function ($scope, handler) {
                $scope.$on(endRequestMessage, function (event) {
                    handler();
                });
            };

            return {
                requestStarted: requestStarted,
                requestEnded: requestEnded,
                onRequestStarted: onRequestStarted,
                onRequestEnded: onRequestEnded
            };
        }
    ])
    .factory('commonMessageHub', ['$rootScope',
        function ($rootScope) {

            var commonMessagehub = $.connection.messageHub;

            var Messages = {
                OrderUpdated: 'Message:OrderUpdated',
                ConsultationAdviceUpdate: 'Message:ConsultationAdviceUpdate'
            };

            var OrderUpdateParams = function () {
                this.connectionId = $.connection.hub.id;
                this.uniqueID = '';
            };

            var ConsultationAdviceUpdateParams = function () {
                this.connectionId = $.connection.hub.id;
                this.requestId = '';
                this.userId = '';
            };

            commonMessagehub.client.broadcastMessage = function (message, params) {
                //to client
                var paramObj = JSON.parse(params);
                if (paramObj.connectionId != $.connection.hub.id) {
                    $rootScope.$broadcast(message, JSON.parse(params));
                }
            };

            // send
            var publish = function (message, params) {
                try {
                    if (!params.connectionId) { // set connectionID when it's empty to ensure self not receive this message
                        params.connectionId = $.connection.hub.id;
                    }
                    commonMessagehub.server.send(message, JSON.stringify(params));
                } catch (ex) { }
            };

            var start = function () {
                $.connection.hub.start();
            };

            return {
                Messages: Messages,
                OrderUpdateParams: OrderUpdateParams,
                ConsultationAdviceUpdateParams: ConsultationAdviceUpdateParams,
                publish: publish,
                start: start,
                subscribe: function (scope, message, callback) {
                    scope.$on(message, callback);
                }
            };
        }
    ])
    .controller('AppController', ['$scope', '$log', '$location', '$translate', '$translatePartialLoader',
        '$window', 'application', 'appTranslation', '$timeout', '$modal', 'loginContext',
        'configurationService', '$rootScope', 'enums', '$state', '$q', 'loginUser', 'clientAgentService',
        'imManager', 'commonMessageHub', '$cookies', 'Idle', 'Keepalive', 'risRelogin',
        function ($scope, $log, $location, $translate, $translatePartialLoader, $window,
            application, appTranslation, $timeout, $modal, loginContext,
            configurationService, $rootScope, enums, $state, $q, loginUser, clientAgentService,
            imManager, commonMessageHub, $cookies, Idle, Keepalive, risRelogin) {
            'use strict';
            $log.debug('AppController.ctor()...');

            var auth = loginContext.auth;
            Idle.setIdle(auth.expires_in - 5);
            Keepalive.setInterval(auth.expires_in - 4);
            Idle.watch();
            Keepalive.start();

            $scope.$on('IdleStart', function () {
                risRelogin.open();
            });

            $scope.$on('Keepalive', function () {
                if (!auth.authorized) return;
                configurationService.refreshToken().success(function (token) {
                    auth.access_token = token;
                    $cookies.auth = JSON.stringify(auth);
                });
            });

            (function initialize() {
                appTranslation.loadAll();

                $rootScope.browser = {
                    versions: function () {
                        var u = $window.navigator.userAgent,
                            app = $window.navigator.appVersion;
                        return {
                            trident: u.indexOf('Trident') > -1, //IE
                            presto: u.indexOf('Presto') > -1, //opera
                            webKit: u.indexOf('AppleWebKit') > -1, //google
                            gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') === -1, //firfox
                            mobile: (function () {
                                if (u.match(/Android/i) || u.match(/webOS/i) || u.match(/iPhone/i) || u.match(/iPad/i) || u.match(/iPod/i) || u.match(/BlackBerry/i) || u.match(/Windows Phone/i)) {
                                    return true;
                                } else {
                                    return false;
                                }
                            })(u),
                            ios: !!u.match(/(i[^;]+\;(U;)? CPU.+Mac OS X)/),
                            android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android or uc
                            iPhone: u.indexOf('iPhone') > -1 || u.indexOf('Mac') > -1, //iPhone or QQHD
                            iPad: u.indexOf('iPad') > -1, //iPad
                            webApp: u.indexOf('Safari') === -1
                        };
                    }(),
                    language: (navigator.browserLanguage || navigator.language).toLowerCase()
                }
                $scope.localName = loginContext.localName;
                $scope.loginUser = loginUser.user;

                $scope.$on('$locationChangeSuccess', function () {
                    $scope.actualLocation = $location.path();
                });

                $scope.$on('$stateChangeStart',
                    function (event, toState, toParams, fromState, fromParams) {
                        // event.preventDefault();
                        // transitionTo() promise will be rejected with 
                        // a 'transition prevented' error
                        $log.debug('$stateChangeStart: ' + toState.name);
                    });

                $scope.$on('$stateChangeSuccess',
                    function (event, toState, toParams, fromState, fromParams) {
                        $log.debug('$stateChangeSuccess: ' + toState.name);
                        if (toState.name === 'ris.worklist.registrations') {
                            $timeout(function () {
                                angular.element($window).trigger('resize');
                            });
                        }
                    });

                // publish unbeforeunload event to child scopes
                $scope.onbeforeunload = function (event) {
                    $scope.$broadcast(application.events.beforeUnload);
                };

                $rootScope.refreshSearch = function (param) {
                    $timeout(function () {
                        $rootScope.$broadcast('event:refreshSearch', param);
                    }, 0, true);
                };

                $scope.goHome = function () {
                    $state.go('ris.worklist.registrations');
                };

                $scope.addBooking = function () {
                    $state.go('ris.newbooking');
                };

                $scope.addRegistration = function () {
                    $state.go('ris.newregistration');
                };

                $scope.goControl = function () {
                    $state.go('ris.qualitycontrol');
                }

                $scope.goConfiguration = function () {
                    $state.go('ris.configuration');
                }

                $scope.goTemplate = function () {
                    $state.go('ris.template');
                }

                $scope.createReport = function () {
                    $state.go('ris.report', { isPreview: false });
                };

                $timeout(function () {
                    commonMessageHub.start();
                }, 2000);
            }());
        }
    ]);

angular.element(document).ready(function () {
    angular.bootstrap(document, ['app']);
});
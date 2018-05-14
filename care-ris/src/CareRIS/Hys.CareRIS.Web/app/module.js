// register common module for shared functionalities for all other feature modules
window.commonModule = angular.module('app.common', ['pascalprecht.translate', 'ngAnimate'])
    .run(['$log', function ($log) {
        $log.debug('app.common.run()...');
    }]);

// register services module
window.webservices = angular.module('app.webservices', ['app.common'])
    .run(['$log', function ($log) {
        $log.debug('app.webservices.run()...');
    }]);

// register framework module for application framework
window.frameworkModule = angular.module('app.framework', ['app.webservices', 'app.common'])
    .run(['$log', function ($log) {
        $log.debug('app.framework.run()...');
    }]);

window.worklistModule = angular.module('app.worklist', [])
    .run(['$log', function ($log) {
        $log.debug('app.worklist.run()...');
    }])
    .config(['$controllerProvider', '$compileProvider', '$filterProvider', '$provide', '$stateProvider',
        function ($controllerProvider, $compileProvider, $filterProvider, $provide, $stateProvider) {
            'use strict';
            // this is required to add controller/directive/filter/service after angular bootstrap
            worklistModule.controller = $controllerProvider.register;
            worklistModule.directive = $compileProvider.directive;
            worklistModule.filter = $filterProvider.register;
            worklistModule.factory = $provide.factory;
            worklistModule.service = $provide.service;

            $stateProvider.state({
                name: 'ris.worklist',
                url: '/worklist',
                views: {
                    'worklist@ris': {
                        controller: ['$scope', '$stateParams', 'appTranslation',
                            function ($scope, $stateParams, appTranslation) {
                                appTranslation.load([appTranslation.worklist]);
                            }
                        ],
                        template: '<worklist-view></worklist-view>',
                    }
                },
                params: {
                    timestamp: null
                },
                sticky: true
            });
        }
    ]);
window.newbookingModule = angular.module('app.newbooking', [])
    .config(['$controllerProvider', '$compileProvider', '$filterProvider', '$provide', '$stateProvider',
        function ($controllerProvider, $compileProvider, $filterProvider, $provide, $stateProvider) {
            'use strict';
            $stateProvider.state({
                name: 'ris.newbooking',
                url: '/newbooking',
                views: {
                    'newbooking@ris': {
                        template: '<timeslice-page-view></timeslice-page-view>',
                    }
                },
            });
        }
    ]);
window.newregistrationModule = angular.module('app.newregistration', [])
    .config(['$stateProvider',
        function ($stateProvider) {
            'use strict';
            $stateProvider.state({
                name: 'ris.newregistration',
                url: '/newregistration',
                views: {
                    'newregistration@ris': {
                        template: '<new-registration></new-registration>'
                    }
                },
            });
        }
    ]);
window.qualitycontrolModule = angular.module('app.qualitycontrol', [])
    .config(['$controllerProvider', '$compileProvider', '$filterProvider', '$provide', '$stateProvider',
        function ($controllerProvider, $compileProvider, $filterProvider, $provide, $stateProvider) {
            'use strict';
            $stateProvider.state({
                name: 'ris.qualitycontrol',
                url: '/qualitycontrol',
                views: {
                    'qualitycontrol@ris': {
                        template: '<quality-control></quality-control>',
                    }
                },
            });
        }
    ]);

window.configurationModule = angular.module('app.configuration', [])
    .config(['$controllerProvider', '$compileProvider', '$filterProvider', '$provide', '$stateProvider',
        function ($controllerProvider, $compileProvider, $filterProvider, $provide, $stateProvider) {
            'use strict';
            $stateProvider.state({
                name: 'ris.configuration',
                url: '/configuration',
                views: {
                    'configuration@ris': {
                        template: '<configuration></configuration>',
                    }
                },
            });
        }
    ]);

window.templateModule = angular.module('app.template', [])
    .config(['$controllerProvider', '$compileProvider', '$filterProvider', '$provide', '$stateProvider',
        function ($controllerProvider, $compileProvider, $filterProvider, $provide, $stateProvider) {
            'use strict';
            $stateProvider.state({
                name: 'ris.template',
                url: '/template',
                views: {
                    'template@ris': {
                        template: '<template></template>',
                    }
                },
            });
        }
    ]);

window.registrationModule = angular.module('app.registration', [])
    .run(['$log', function ($log) {
        $log.debug('app.registration.run()...');
    }])
    .config(['$controllerProvider', '$compileProvider', '$filterProvider', '$provide', '$stateProvider',
        function ($controllerProvider, $compileProvider, $filterProvider, $provide, $stateProvider) {
            'use strict';
            // this is required to add controller/directive/filter/service after angular bootstrap
            registrationModule.controller = $controllerProvider.register;
            registrationModule.directive = $compileProvider.directive;
            registrationModule.filter = $filterProvider.register;
            registrationModule.factory = $provide.factory;
            registrationModule.service = $provide.service;

            $stateProvider.state({
                name: 'ris.worklist.registrations',
                url: '/registrations',
                views: {
                    'ris-side-panel@ris.worklist': {
                        templateUrl: '/app/worklist/views/worklist-sidepanel-view.html'
                    },
                    'registrations@ris.worklist': {
                        controller: ['$scope', '$stateParams', 'appTranslation',
                            function ($scope, $stateParams, appTranslation) {
                                appTranslation.load([appTranslation.registration]);
                            }
                        ],
                        template: '<registrations-view></registrations-view>',
                    }
                },
                sticky: true,
                params: {
                    ts: null
                }
            }).state({
                name: 'ris.registration',
                url: '/registration/{orderId}',
                views: {
                    'registration@ris': {
                        controller: ['$scope', '$stateParams',
                            function ($scope, $stateParams) {
                                $scope.orderId = $stateParams.orderId;
                            }
                        ],
                        template: '<registration-view order-id="{{orderId}}"></registration-view>',
                    }
                }
            }).state({
                name: 'ris.worklist.registrationTransfer',
                url: '/registrationTransfer',
                views: {
                    'ris-side-panel@ris.worklist': {
                        templateUrl: '/app/worklist/views/worklist-sidepanel-view.html'
                    },
                    'registrationTransfer@ris.worklist': {
                        controller: ['$scope', '$stateParams',
                            function ($scope, $stateParams) {
                                $scope.requestInfo = $stateParams.requestInfo;
                            }
                        ],
                        template: '<registration-transfer-view request-info="requestInfo"></registration-transfer-view>',
                    }
                },
                params: {
                    requestInfo: null
                }
            }).state({
                name: 'ris.worklist.role',
                url: '/settings/role',
                views: {
                    'ris-side-panel@ris.worklist': {
                        controller: 'RisSettingsSidePanel',
                        templateUrl:'/app/worklist/settings/sidepanel/views/ris-settings-side-panel.html'
                    },
                    'role@ris.worklist': {
                        controller: 'RisRoleController',
                        templateUrl:'/app/worklist/settings/role/views/ris-role-view.html'
                    }
                }
            }).state({
                name: 'ris.worklist.config',
                url: '/settings/config',
                views: {
                    'ris-side-panel@ris.worklist': {
                        controller: 'RisSettingsSidePanel',
                        templateUrl:'/app/worklist/settings/sidepanel/views/ris-settings-side-panel.html'
                    },
                    'config@ris.worklist': {
                        controller: 'RisConfigController',
                        templateUrl:'/app/worklist/settings/config/views/ris-config-view.html'
                    }
                }
            });
        }
    ]);

window.reportModule = angular.module('app.report', [])
    .run(['$log', function ($log) {
        $log.debug('app.report.run()...');
    }])
    .config(['$controllerProvider', '$compileProvider', '$filterProvider', '$provide', '$stateProvider',
        function ($controllerProvider, $compileProvider, $filterProvider, $provide, $stateProvider) {
            'use strict';
            // this is required to add controller/directive/filter/service after angular bootstrap
            reportModule.controller = $controllerProvider.register;
            reportModule.directive = $compileProvider.directive;
            reportModule.filter = $filterProvider.register;
            reportModule.factory = $provide.factory;
            reportModule.service = $provide.service;

            $stateProvider.state({
                name: 'ris.report',
                url: '/report?reportId&orderId&procedureId&isPreview&isRead&printId&from&patientCaseOrderId',
                views: {
                    'report@ris': {
                        controller: ['$scope', '$stateParams', 'appTranslation',
                            function ($scope, $stateParams, appTranslation) {
                                $scope.reportId = $stateParams.reportId;
                                $scope.orderId = $stateParams.orderId;
                                $scope.procedureId = $stateParams.procedureId;
                                $scope.isPreview = $stateParams.isPreview;
                                $scope.isRead = $stateParams.isRead;
                                $scope.printId = $stateParams.printId;
                                $scope.from = $stateParams.from;
                                $scope.patientCaseOrderId = $stateParams.patientCaseOrderId;
                                appTranslation.load([appTranslation.report]);
                            }
                        ],
                        template: '<report-view report-id="{{reportId}}" order-id="{{orderId}}" procedure-id="{{procedureId}}" is-preview="{{isPreview}}"  is-read="{{isRead}}" print-id="{{printId}}" from="{{from}}" patient-case-order-id="{{patientCaseOrderId}}"></report-view>'
                    }
                }
            });
        }
    ]);

window.consultationModule = angular.module('app.consultation', ['kendo.directives'])
    .run(['$log', function ($log) {
        $log.debug('app.consultation.run()...');
    }])
    .factory('consultationSidePanel', function () {
        return {
            isExpanded: true,
            expand: function () {
                this.isExpanded = true;
            },
            collapse: function () {
                this.isExpanded = false;
            }
        };
    })
    .config(['$controllerProvider', '$compileProvider', '$filterProvider', '$provide', '$stateProvider',
        function ($controllerProvider, $compileProvider, $filterProvider, $provide, $stateProvider) {
            'use strict';
            // this is required to add controller/directive/filter/service after angular bootstrap
            consultationModule.controller = $controllerProvider.register;
            consultationModule.directive = $compileProvider.directive;
            consultationModule.filter = $filterProvider.register;
            consultationModule.factory = $provide.factory;
            consultationModule.service = $provide.service;

            $stateProvider
                .state({
                    name: 'ris.consultation',
                    url: '/consultation',
                    views: {
                        'consultation@ris': {
                            //default view for consultation
                            template: '<consultation-view></consultation-view>',
                        }
                    }
                })
                .state({
                    name: 'ris.consultation.requests',
                    url: '/requests',
                    views: {
                        'consultation-side-panel@ris.consultation': {
                            template: '<consultation-requests-side-panel></consultation-requests-side-panel>'
                        },
                        'requests@ris.consultation': {
                            controller: ['$scope', '$stateParams', 'appTranslation',
                                function ($scope, $stateParams, appTranslation) {
                                    appTranslation.load([appTranslation.consultation]);
                                    $scope.searchCriteria = $stateParams.searchCriteria;
                                    $scope.highlightStatus = $stateParams.highlightStatus;
                                }
                            ],
                            template: '<consultation-requests-view></consultation-requests-view>',
                        }
                    },
                    params: {
                        searchCriteria: { includeDeleted: true },
                        highlightStatus: null,
                        timestamp: null,
                        reload: false
                    },
                    onEnter: function ($stateParams) {

                    }
                })
                .state({
                    name: 'ris.consultation.exam',
                    url: '/exam',
                    views: {
                        'consultation-side-panel@ris.consultation': {
                            template: '<consultation-requests-side-panel></consultation-requests-side-panel>'
                        },
                        'exam@ris.consultation': {
                            controller: ['$scope', '$stateParams', 'appTranslation',
                                function ($scope, $stateParams, appTranslation) {
                                    appTranslation.load([appTranslation.consultation]);
                                }
                            ],
                            template: '<exam-info-view></exam-info-view>',
                        }
                    }
                })
                .state({
                    name: 'ris.consultation.request',
                    url: '/request',
                    views: {
                        'consultation-side-panel@ris.consultation': {
                            template: '<consultation-requests-side-panel></consultation-requests-side-panel>'
                        },
                        'request@ris.consultation': {
                            controller: ['$scope', '$stateParams', 'appTranslation',
                                function ($scope, $stateParams, appTranslation) {
                                    appTranslation.load([appTranslation.consultation]);
                                }
                            ],
                            template: '<consultation-request-view></consultation-request-view>',
                        }
                    }
                })
            .state({
                name: 'ris.consultation.newpatientcase',
                url: '/newpatientcase',
                views: {
                    'consultation-side-panel@ris.consultation': {
                      
                        template: '<consultation-patient-cases-side-panel></consultation-patient-cases-side-panel>'
                    },
                    'newpatientcase@ris.consultation': {
                        controller: ['$scope', '$stateParams', 'appTranslation',
                                  function ($scope, $stateParams, appTranslation) {
                                      appTranslation.load([appTranslation.consultation]);
                                  }
                        ],
                        template: '<new-patietcase-view></new-patietcase-view>'
                    }
                },
                params: {
                    timestamp: null,
                    patientCaseID: null,
                    searchCriteria: null,
                    autoFillCase:null,
                    id: null,
                    patientId: null,
                    accessionNo:null
                }
            }).state({
                name: 'ris.consultation.cases',
                url: '/cases',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        template: '<consultation-patient-cases-side-panel></consultation-patient-cases-side-panel>'
                    },
                    'cases@ris.consultation': {
                        controller: ['$scope', '$stateParams', 'appTranslation',
                            function ($scope, $stateParams, appTranslation) {
                                appTranslation.load([appTranslation.consultation]);
                                $scope.searchCriteria = $stateParams.searchCriteria;
                                $scope.highlightStatus = $stateParams.highlightStatus;
                            }
                        ],
                        template: '<patient-cases-view></patient-cases-view>',
                    }
                },
                params: {
                    searchCriteria: null,
                    highlightStatus: null,
                    timestamp: null,
                    reload: false
                }
            }).state({
                name: 'ris.consultation.dicoms',
                url: '/dicoms',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        template: '<consultation-patient-cases-side-panel></consultation-patient-cases-side-panel>'
                    },
                    'dicoms@ris.consultation': {
                        controller: ['$scope', '$stateParams', 'appTranslation',
                            function ($scope, $stateParams, appTranslation) {
                                appTranslation.load([appTranslation.consultation]);
                                $scope.searchCriteria = $stateParams.searchCriteria;
                                $scope.highlightStatus = $stateParams.highlightStatus;
                            }
                        ],
                        template: '<dicom-list-view></dicom-list-view>'
                    }
                },
                params: {
                    searchCriteria: null,
                    highlightStatus: null,
                    timestamp: null
                }
            }).state({
                name: 'ris.consultation.roles',
                url: '/settings/roles',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        controller: 'ConsultationSettingsSidePanel',
                        templateUrl:'/app/consultation/settings/views/consultation-settings-side-panel.html'
                    },
                    'roles@ris.consultation': {
                        controller: 'RolesController',
                        templateUrl:'/app/consultation/settings/views/roles-view.html',
                    }
                }
            }).state({
                name: 'ris.consultation.users',
                url: '/settings/users',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        controller: 'ConsultationSettingsSidePanel',
                        templateUrl:'/app/consultation/settings/views/consultation-settings-side-panel.html'
                    },
                    'users@ris.consultation': {
                        controller: 'UsersController',
                        templateUrl:'/app/consultation/settings/views/users-view.html',
                    }
                }
            }).state({
                name: 'ris.consultation.hospitals',
                controller: 'ConsultationSettingsSidePanel',
                url: '/settings/hospitals',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        controller: 'ConsultationSettingsSidePanel',
                        templateUrl:'/app/consultation/settings/views/consultation-settings-side-panel.html'
                    },
                    'hospitals@ris.consultation': {
                        controller: 'HospitalsController',
                        templateUrl:'/app/consultation/settings/views/hospitals-view.html',
                    }
                }
            }).state({
                name: 'ris.consultation.recipientconfig',
                controller: 'ConsultationSettingsSidePanel',
                url: '/settings/recipientconfig',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        controller: 'ConsultationSettingsSidePanel',
                        templateUrl:'/app/consultation/settings/views/consultation-settings-side-panel.html'
                    },
                    'recipientconfig@ris.consultation': {
                        controller: 'RecipientConfigController',
                        templateUrl:'/app/consultation/settings/views/recipient-config-view.html',
                    }
                }
            }).state({
                name: 'ris.consultation.reporttemplate',
                controller: 'ConsultationSettingsSidePanel',
                url: '/settings/reporttemplate',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        controller: 'ConsultationSettingsSidePanel',
                        templateUrl:'/app/consultation/settings/views/consultation-settings-side-panel.html'
                    },
                    'reporttemplate@ris.consultation': {
                        controller: 'ReportTemplateController',
                        templateUrl:'/app/consultation/settings/views/report-template-view.html'
                    }
                }
            }).state({
                name: 'ris.consultation.notificationconfig',
                controller: 'ConsultationSettingsSidePanel',
                url: '/settings/notificationconfig',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        controller: 'ConsultationSettingsSidePanel',
                        templateUrl:'/app/consultation/settings/views/consultation-settings-side-panel.html'
                    },
                    'notificationconfig@ris.consultation': {
                        controller: 'NotificationConfigController',
                        templateUrl:'/app/consultation/settings/views/notification-config-view.html'
                    }
                }
            }).state({
                name: 'ris.consultation.completed',
                url: '/request/completed',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        template: '<consultation-requests-side-panel></consultation-requests-side-panel>'
                    },
                    'completed@ris.consultation': {
                        controller: ['$scope', '$stateParams', 'appTranslation',
                            function ($scope, $stateParams, appTranslation) {
                                appTranslation.load([appTranslation.consultation]);
                                $scope.requestId = $stateParams.requestId;
                                $scope.searchCriteria = $stateParams.searchCriteria;
                            }
                        ],
                        template: '<request-completed-view></request-completed-view>'
                    }
                },
                params: {
                    searchCriteria: null,
                    requestId: null
                }
            }).state({
                name: 'ris.consultation.applied',
                url: '/request/applied',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        template: '<consultation-requests-side-panel></consultation-requests-side-panel>'
                    },
                    'applied@ris.consultation': {
                        controller: ['$scope', '$stateParams', 'appTranslation',
                            function ($scope, $stateParams, appTranslation) {
                                appTranslation.load([appTranslation.consultation]);
                                $scope.requestId = $stateParams.requestId;
                                $scope.searchCriteria = $stateParams.searchCriteria;
                            }
                        ],
                        template: '<request-applied-view></request-applied-view>'
                    }
                },
                params: {
                    searchCriteria: null,
                    requestId: null
                }
            }).state({
                name: 'ris.consultation.accepted',
                url: '/request/accepted',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        template: '<consultation-requests-side-panel></consultation-requests-side-panel>'
                    },
                    'accepted@ris.consultation': {
                        controller: ['$scope', '$stateParams', 'appTranslation',
                            function ($scope, $stateParams, appTranslation) {
                                appTranslation.load([appTranslation.consultation]);
                                $scope.requestId = $stateParams.requestId;
                                $scope.searchCriteria = $stateParams.searchCriteria;
                            }
                        ],
                        template: '<request-accepted-view></request-accepted-view>'
                    }
                },
                params: {
                    searchCriteria: null,
                    requestId: null
                }
            }).state({
                name: 'ris.consultation.terminate',
                url: '/request/terminate',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        template: '<consultation-requests-side-panel></consultation-requests-side-panel>'
                    },
                    'terminate@ris.consultation': {
                        controller: ['$scope', '$stateParams', 'appTranslation',
                            function ($scope, $stateParams, appTranslation) {
                                appTranslation.load([appTranslation.consultation]);
                                $scope.requestId = $stateParams.requestId;
                                $scope.searchCriteria = $stateParams.searchCriteria;
                            }
                        ],
                        template: '<request-terminate-view></request-terminate-view>'
                    }
                },
                params: {
                    searchCriteria: null,
                    requestId: null
                }
            }).state({
                name: 'ris.consultation.cancelled',
                url: '/request/cancelled',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        template: '<consultation-requests-side-panel></consultation-requests-side-panel>'
                    },
                    'cancelled@ris.consultation': {
                        controller: ['$scope', '$stateParams', 'appTranslation',
                            function ($scope, $stateParams, appTranslation) {
                                appTranslation.load([appTranslation.consultation]);
                                $scope.requestId = $stateParams.requestId;
                                $scope.searchCriteria = $stateParams.searchCriteria;
                            }
                        ],
                        template: '<request-cancelled-view></request-cancelled-view>'
                    }
                },
                params: {
                    searchCriteria: null,
                    requestId: null
                }
            }).state({
                name: 'ris.consultation.applycancel',
                url: '/request/applycancel',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        template: '<consultation-requests-side-panel></consultation-requests-side-panel>'
                    },
                    'applycancel@ris.consultation': {
                        controller: ['$scope', '$stateParams', 'appTranslation',
                            function ($scope, $stateParams, appTranslation) {
                                appTranslation.load([appTranslation.consultation]);
                                $scope.requestId = $stateParams.requestId;
                                $scope.searchCriteria = $stateParams.searchCriteria;
                            }
                        ],
                        template: '<request-apply-cancel-view></request-apply-cancel-view>'
                    }
                },
                params: {
                    searchCriteria: null,
                    requestId: null
                }
            }).state({
                name: 'ris.consultation.rejected',
                url: '/request/rejected',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        template: '<consultation-requests-side-panel></consultation-requests-side-panel>'
                    },
                    'rejected@ris.consultation': {
                        controller: ['$scope', '$stateParams', 'appTranslation',
                            function ($scope, $stateParams, appTranslation) {
                                appTranslation.load([appTranslation.consultation]);
                                $scope.requestId = $stateParams.requestId;
                                $scope.searchCriteria = $stateParams.searchCriteria;
                            }
                        ],
                        template: '<request-rejected-view></request-rejected-view>'
                    }
                },
                params: {
                    searchCriteria: null,
                    requestId: null
                }
            }).state({
                name: 'ris.consultation.reconsider',
                url: '/request/reconsider',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        template: '<consultation-requests-side-panel></consultation-requests-side-panel>'
                    },
                    'reconsider@ris.consultation': {
                        controller: ['$scope', '$stateParams', 'appTranslation',
                            function ($scope, $stateParams, appTranslation) {
                                appTranslation.load([appTranslation.consultation]);
                                $scope.requestId = $stateParams.requestId;
                                $scope.searchCriteria = $stateParams.searchCriteria;
                            }
                        ],
                        template: '<request-reconsider-view></request-reconsider-view>'
                    }
                },
                params: {
                    searchCriteria: null,
                    requestId: null
                }
            }).state({
                name: 'ris.consultation.deleted',
                url: '/request/deleted',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        template: '<consultation-requests-side-panel></consultation-requests-side-panel>'
                    },
                    'deleted@ris.consultation': {
                        controller: ['$scope', '$stateParams', 'appTranslation',
                            function ($scope, $stateParams, appTranslation) {
                                appTranslation.load([appTranslation.consultation]);
                                $scope.requestId = $stateParams.requestId;
                                $scope.searchCriteria = $stateParams.searchCriteria;
                            }
                        ],
                        template: '<request-delete-view></request-delete-view>'
                    }
                },
                params: {
                    searchCriteria: null,
                    requestId: null
                }
            }).state({
                name: 'ris.consultation.default',
                url: '/request/default',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        template: '<consultation-requests-side-panel></consultation-requests-side-panel>'
                    },
                    'default@ris.consultation': {
                        controller: ['$scope', '$stateParams', 'appTranslation',
                            function ($scope, $stateParams, appTranslation) {
                                appTranslation.load([appTranslation.consultation]);
                                $scope.requestId = $stateParams.requestId;
                                $scope.searchCriteria = $stateParams.searchCriteria;
                            }
                        ],
                        template: '<request-default-view></request-default-view>'
                    }
                },
                params: {
                    searchCriteria: null,
                    requestId: null
                }
            }).state({
                name: 'ris.consultation.apply',
                url: '/apply',
                views: {
                    'consultation-side-panel@ris.consultation': {
                        template: '<consultation-requests-side-panel></consultation-requests-side-panel>'
                    },
                    'apply@ris.consultation': {
                        controller: ['$scope', '$stateParams', 'appTranslation',
                            function ($scope, $stateParams, appTranslation) {
                                appTranslation.load([appTranslation.consultation]);
                                $scope.patientCase = $stateParams.patientCase;
                            }
                        ],
                        template: '<request-apply-view></request-apply-view>'
                    }
                },
                params: {
                    searchCriteria: null,
                    patientCase: null
                }
            });
        }
    ]);

window.referralModule = angular.module('app.referral', [])
    .run(['$log', function ($log) {
        $log.debug('app.referral.run()...');
    }])
    .factory('referralSidePanel', function () {
        return {
            isExpanded: true,
            expand: function () {
                this.isExpanded = true;
            },
            collapse: function () {
                this.isExpanded = false;
            }
        };
    })
    .config(['$controllerProvider', '$compileProvider', '$filterProvider', '$provide', '$stateProvider',
        function ($controllerProvider, $compileProvider, $filterProvider, $provide, $stateProvider) {
            'use strict';
            // this is required to add controller/directive/filter/service after angular bootstrap
            referralModule.controller = $controllerProvider.register;
            referralModule.directive = $compileProvider.directive;
            referralModule.filter = $filterProvider.register;
            referralModule.factory = $provide.factory;
            referralModule.service = $provide.service;

            $stateProvider
                .state({
                    name: 'ris.referral',
                    url: '/referral',
                    views: {
                        'referral@ris': {
                            //default view for referral
                            template: '<referral-view></referral-view>'
                        }
                    }
                })
            .state({
                name: 'ris.referral.referrals',
                url: '/referrals',
                views: {
                    'referral-side-panel@ris.referral': {
                        template: '<ris-referral-side-panel></ris-referral-side-panel>'
                    },
                    'referrals@ris.referral': {
                        controller: ['$scope', '$stateParams', 'appTranslation',
                            function ($scope, $stateParams, appTranslation) {
                                appTranslation.load([appTranslation.consultation]);
                                $scope.searchCriteria = $stateParams.searchCriteria;
                            }
                        ],
                        template: '<ris-referrals-view></ris-referrals-view>'
                    }
                },
                params: {
                    searchCriteria: null,
                    timestamp: null,
                    reload: false
                }
            });
        }
    ]);
consultationModule.factory('examService',
    ['$log', 'consultationService', 'loginContext', '$http', 'openDialog', '$window', '$translate',
        function ($log, consultationService, loginContext, $http, openDialog, $window, $translate) {
            'use strict';

            $log.debug('examService.ctor()...');
            var damInfo = {};
            var srcInfo = {};


            return {
                isNew: false,
                modules: {},
                updatedModules: {},
                modulesData: {},
                clear: function () {
                    this.modules = [];
                    this.updatedModules = {};
                    this.modulesData = {};
                },
                getModulesData: function () {
                    this.modulesData;
                },
                addModulesData: function (type, data) {
                    this.modulesData[type] = data;
                },
                removeModulesData: function (type) {
                    if (type in this.modulesData)
                        delete this.modulesData[type];
                }
            };

        }]);
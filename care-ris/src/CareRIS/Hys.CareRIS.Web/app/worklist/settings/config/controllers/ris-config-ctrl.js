worklistModule.controller('RisConfigController', ['$scope', '$log', '$translate', 'clientAgentService', 'configurationService', 'enums', 'openDialog', 'loginContext','csdToaster',
    function ($scope, $log, $translate, clientAgentService, configurationService, enums, openDialog, loginContext, csdToaster) {
        'use strict';
        $log.debug('RisConfigController.ctor()...');
        var separator = '|';

        $scope._ = _;
        $scope.integrationTypes = _.values(enums.IntegrationTypes);

        $scope.updateConfig = function () {

            //reg
            var disableTypes = [];
            _.each(_.keys($scope.enabledModalityTypes), function (key) {
                if (!$scope.enabledModalityTypes[key]) {
                    disableTypes.push(key);
                }
            });
            $scope.config.disabledModalityTypes = disableTypes.join(separator);

            var selectedModalities = _.flatten(_.values($scope.selectedModality));
            var disabledModalities = _.difference($scope.modalities, selectedModalities);
            $scope.config.disabledModalities = _.pluck(disabledModalities, 'uniqueID').join(separator);

            //appt
            var apptDisableTypes = [];
            _.each(_.keys($scope.apptEnabledModalityTypes), function (key) {
                if (!$scope.apptEnabledModalityTypes[key]) {
                    apptDisableTypes.push(key);
                }
            });
            $scope.config.appointmentDisabledModalityTypes = apptDisableTypes.join(separator);

            var appointmentSelectedModalities = _.flatten(_.values($scope.apptSelectedModality));
            var appointmentDisabledModalities = _.difference($scope.modalities, appointmentSelectedModalities);
            $scope.config.appointmentDisabledModalities = _.pluck(appointmentDisabledModalities, 'uniqueID').join(separator);
            
            //save
            configurationService.saveClientConfig($scope.config).success(function () {
                csdToaster.pop('success', $translate.instant("SaveSuccess"), '');
            });
        }

        clientAgentService.GetProcessID().success(function (identity) {
            if (identity) {
                configurationService.getClientConfig(identity).success(function (config) {
                    $scope.config = config;

                    configurationService.getModalities(loginContext.site).success(function (data) {
                        $scope.modalities = data;
                        $scope.selectedModality = _.groupBy($scope.modalities, 'modalityType');
                        $scope.apptSelectedModality = _.groupBy($scope.modalities, 'modalityType');
                        $scope.modalityTypes = _.uniq(_.pluck(data, 'modalityType'));
                        $scope.enabledModalityTypes = {};
                        $scope.apptEnabledModalityTypes = {};

                        _.each($scope.modalityTypes, function(type) {
                            $scope.enabledModalityTypes[type] = true;
                            $scope.apptEnabledModalityTypes[type] = true;
                        });

                        if ($scope.config) {
                            if ($scope.config.disabledModalities) {
                                var disabledModalities = $scope.config.disabledModalities.split(separator);
                                var modalities = _.reject($scope.modalities, function (m) {
                                    return _.some(disabledModalities, function (id) { return m.uniqueID === id; });
                                });
                                $scope.selectedModality = _.groupBy(modalities, 'modalityType');
                            }
                            if ($scope.config.disabledModalityTypes) {
                                var disableTypes = $scope.config.disabledModalityTypes.split(separator);
                                _.each(disableTypes, function (type) { $scope.enabledModalityTypes[type] = false; });
                            }

                            //appt
                            if ($scope.config.appointmentDisabledModalities) {
                                var appointmentDisabledModalities = $scope.config.appointmentDisabledModalities.split(separator);
                                var appointmentModalities = _.reject($scope.modalities, function (m) {
                                    return _.some(appointmentDisabledModalities, function (id) { return m.uniqueID === id; });
                                });
                                $scope.apptSelectedModality = _.groupBy(appointmentModalities, 'modalityType');
                            }
                            
                            if ($scope.config.appointmentDisabledModalityTypes) {
                                var apptDisableTypes = $scope.config.appointmentDisabledModalityTypes.split(separator);
                                _.each(apptDisableTypes, function (type) { $scope.apptEnabledModalityTypes[type] = false; });
                            }
                        }

                    });

                    clientAgentService.GetAllPrinters().success(function (printers) {
                        $scope.printers = printers;
                    });
                });
            }
        });
    }]);
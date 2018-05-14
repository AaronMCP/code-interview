consultationModule.factory('dicomService',
    ['$log', 'clientAgentService', function ($log, clientAgentService) {
            'use strict';
            $log.debug('dicomService.ctor()...');

            return {
                lastTime: null,
                isUpdatedDicom:false
            };

        }]);
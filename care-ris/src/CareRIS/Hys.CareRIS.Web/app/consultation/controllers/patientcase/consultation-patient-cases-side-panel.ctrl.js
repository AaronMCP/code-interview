consultationModule.controller('ConsultationPatientCasesSidePanelController', ['$log', '$scope', '$state', 'consultationSidePanel',
    'enums', '$stateParams', 'loginUser', 'clientAgentService', 'dicomService', '$interval', '$rootScope', '$filter',
    function ($log, $scope, $state, consultationSidePanel, enums, $stateParams, loginUser, clientAgentService, dicomService, $interval, $rootScope, $filter) {
        'use strict';

        $log.debug('ConsultationPatientCasesSidePanelController.ctor()...');

        $scope.user = {
            isJuniorDoctor: loginUser.isDoctor,
            isConsultationCenter: loginUser.isConsAdmin,
            isExpert: loginUser.isExpert
        };

        $scope.highlightStauts = function (status) {
            var r = _.findWhere($scope.caseHighlight, { status: status });
            if (r && r.active) {
                return 'click';
            }
        };

        var statusFilterContext = (function () {
            var filter = function (status) {
                $scope.isDICOMListc = false;
                $stateParams.highlightStatus =
                  {
                      cases: status
                  }
                $state.go('ris.consultation.cases', {
                    searchCriteria: {
                        statusList: [status]
                    }, highlightStatus:
                  {
                      cases: status
                  },
                    timestamp: Date.now(),
                    reload: true
                });
            };
            return {
                filter: filter
            };
        }());

        $scope.createPatientCase = function () {
            $state.go('ris.consultation.newpatientcase', {
                timestamp: Date.now(),
                searchCriteria: $stateParams.searchCriteria,
                autoFillCase: null,
                id: null,
                patientId: null,
                accessionNo: null
            });
        };

        $scope.showDICOMList = function () {
            dicomService.isUpdatedDicom = false;
            $state.go('ris.consultation.dicoms', {
                highlightStatus: { cases: enums.patientCaseStatus.DicomList },
                searchCriteria: null,
                timestamp: Date.now()
            });
        };

        if (!$rootScope.browser.versions.mobile) {
            var checkDicomInterval = $interval(function () {
                dicomService.lastTime = dicomService.lastTime || $filter('date')(new Date(), 'yyyy-MM-dd HH:mm:ss');
                var data = { time: dicomService.lastTime };
                clientAgentService.checkDicom(data).success(function (result) {
                    dicomService.isUpdatedDicom = $scope.isUpdatedDicom = result | false;
                });
            }, 10000);
            $scope.$on('$destroy', function () { $interval.cancel(checkDicomInterval) });
        }

        (function initialize() {
            $scope.sidePanel = consultationSidePanel;
            $scope.statusFilterContext = statusFilterContext;
            $scope.enums = enums;
            $scope.isUpdatedDicom = dicomService.isUpdatedDicom;
            $scope.isMobile = $rootScope.browser.versions.mobile;
            if ($stateParams.highlightStatus && ($stateParams.highlightStatus.cases || $stateParams.highlightStatus.cases == 0)) {
                if ($stateParams.highlightStatus.cases == enums.patientCaseStatus.None) {
                    angular.forEach($scope.caseHighlight, function (item) {
                        item.active = false;
                    });
                } else {
                    var r = _.findWhere($scope.caseHighlight, { status: $stateParams.highlightStatus.cases });
                    if (r) {
                        angular.forEach($scope.caseHighlight, function (item) {
                            item.active = false;
                        });
                        r.active = true;
                    }
                }
            }
        }());
    }
]);


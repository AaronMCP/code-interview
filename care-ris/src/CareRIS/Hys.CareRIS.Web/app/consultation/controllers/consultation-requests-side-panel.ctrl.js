consultationModule.controller('ConsultationRequestsSidePanelController', ['$log', '$scope', '$state', 'enums', 'loginUser', 'consultationService', 'application', 'consultationSidePanel', 'loginContext', '$stateParams', '$rootScope',
	function ($log, $scope, $state, enums, loginUser, consultationService, application, consultationSidePanel, loginContext, $stateParams, $rootScope) {
	    'use strict';

	    $log.debug('ConsultationRequestsSidePanelController.ctor()...');

	    $scope.highlightStauts = function (status) {
	        var r = _.findWhere($scope.requestHighlight, {
	            status: status
	        });
	        if (r && r.active) {
	            return 'click';
	        }
	    };

	    var statusFilterContext = (function () {
	        var filter = function (status) {
	            $stateParams.highlightStatus = {
	                requests: status
	            }

	            var timestamp = Date.now();
	            var searchCriteria = {
	                statusList: [status]
	            };

	            $state.go('ris.consultation.requests', {
	                searchCriteria: searchCriteria,
	                timestamp: timestamp,
	                highlightStatus: {
	                    requests: status
	                },
	                reload: true
	            });

	            $rootScope.$broadcast(application.events.consultationAdvancedSearchRequestsCompleted, searchCriteria);
	        };

	        return {
	            status: enums.consultationRequestStatus,
	            filter: filter
	        };
	    }());

	    $scope.createPatientCase = function () {
	        $state.go('ris.consultation.newpatientcase', {
	            timestamp: Date.now()
	        });
	    };

	    var shortcutContext = (function () {
	        var refreshShortcuts = function () {

	            consultationService.getShortcuts(loginContext.userId, $scope.shortcutCategory).success(function (data) {
	                shortcutContext.shortcuts = data;
	            });
	        };
	        var deleteShortcut = function (event, shortcut) {
	            event.stopPropagation();

	            consultationService.deleteShortcut(shortcut.uniqueId)
					.success(function () {
					    refreshShortcuts();
					});
	        };
	        var setDefaultShortcut = function (event, shortcut) {
	            event.stopPropagation();

	            shortcut.isDefault = true;
	            consultationService.updateShortcut(shortcut)
					.success(function () {
					    refreshShortcuts();
					});
	        };
	        var searchByShortcut = function (shortcut) {
	            var timestamp = Date.now();
	            var searchCriteria = JSON.parse(shortcut.value);

	            $state.go('ris.consultation.requests', {
	                searchCriteria: searchCriteria,
	                timestamp: timestamp,
	                highlightStatus: {
	                    requests: enums.consultationRequestStatus.None
	                },
	                reload: true
	            });

	            $rootScope.$broadcast(application.events.consultationAdvancedSearchRequestsCompleted, searchCriteria);
	        };
	        var switchVisibility = function () {
	            shortcutContext.visible = !shortcutContext.visible;
	        };
	        return {
	            visible: true,
	            switchVisibility: switchVisibility,
	            shortcuts: [],
	            refreshShortcuts: refreshShortcuts,
	            searchByShortcut: searchByShortcut,
	            deleteShortcut: deleteShortcut,
	            setDefaultShortcut: setDefaultShortcut
	        };
	    }());

	    (function initialize() {
	        $scope.user = {
	            isJuniorDoctor: loginUser.isDoctor,
	            isConsultationCenter: loginUser.isConsAdmin,
	            isExpert: loginUser.isExpert,
	            userId: loginUser.user.uniqueID
	        };

	        $scope.shortcutCategory = $scope.user.isJuniorDoctor ? enums.shortcutCategory.RequestSearchDoctor :
				$scope.user.isConsultationCenter ? enums.shortcutCategory.RequestSearchCenter :
				$scope.user.isExpert ? enums.shortcutCategory.RequestSearchExpert : enums.shortcutCategory.All;

	        if ($stateParams.highlightStatus && ($stateParams.highlightStatus.requests || $stateParams.highlightStatus.requests == 0)) {
	            var r = _.findWhere($scope.requestHighlight, {
	                status: $stateParams.highlightStatus.requests
	            });
	            if (r) {
	                angular.forEach($scope.requestHighlight, function (item) {
	                    item.active = false;
	                });
	                r.active = true;
	            }
	        }

	        if ($stateParams.highlightStatus && ($stateParams.highlightStatus.requests || $stateParams.highlightStatus.requests == 0)) {
	            if ($stateParams.highlightStatus.requests == enums.consultationRequestStatus.None) {
	                angular.forEach($scope.requestHighlight, function (item) {
	                    item.active = false;
	                });
	            } else {
	                var r = _.findWhere($scope.requestHighlight, {
	                    status: $stateParams.highlightStatus.requests
	                });
	                if (r) {
	                    angular.forEach($scope.requestHighlight, function (item) {
	                        item.active = false;
	                    });
	                    r.active = true;
	                }
	            }
	        }

	        $scope.$state = $state;
	        $scope.sidePanel = consultationSidePanel;
	        $scope.statusFilterContext = statusFilterContext;
	        $scope.shortcutContext = shortcutContext;
	        shortcutContext.refreshShortcuts();
	        $scope.$on(application.events.consultationRequestShortcutAdded, function () {
	            shortcutContext.refreshShortcuts();
	        });
	    }());
	}
]);
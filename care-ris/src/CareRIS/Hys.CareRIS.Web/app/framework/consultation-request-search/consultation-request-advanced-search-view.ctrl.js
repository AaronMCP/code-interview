frameworkModule.controller('ConsultationRequestAdvancedSearchController', ['$log', '$scope', '$state', '$modalInstance', 'enums', 'loginContext', 'consultationService', '$rootScope', 'application', 'loginUser', 'previousAdvancedSearchCriteria',
    function($log, $scope, $state, $modalInstance, enums, loginContext, consultationService, $rootScope, application, loginUser, previousAdvancedSearchCriteria) {

        $log.debug('ConsultationRequestAdvancedSearchController.ctor()...');

        $scope.searchCriteria = previousAdvancedSearchCriteria;
        $scope.shortcut = {};

        var convertStatusesForSearch = function(criteria) {
            if (_.isArray(criteria.statusList) && criteria.statusList.length > 0 && _.isObject(statusList.statuses[0])) {
                var statuses = [];
                _.each(criteria.statusList, function(status) {
                    statuses.push(parseInt(status.value));
                })
                criteria.statusList = statuses;
            }
        };

        // NOTE: handle the case if you select one date and clear it, then the min date will be selected by default.
        // use this code the clear up the date if date string is empty.
        var minDate = new Date('1970-01-01Z');
        var clearUpEmptyDate = function(searchCriteria) {
            if (_.isDate(searchCriteria.consultationStartDate) && searchCriteria.consultationStartDate.valueOf() === minDate.valueOf()) {
                searchCriteria.consultationStartDate = undefined;
            }

            if (_.isDate(searchCriteria.consultationEndDate) && searchCriteria.consultationEndDate.valueOf() === minDate.valueOf()) {
                searchCriteria.consultationEndDate = undefined;
            }

            if (_.isDate(searchCriteria.requestStartDate) && searchCriteria.requestStartDate.valueOf() === minDate.valueOf()) {
                searchCriteria.requestStartDate = undefined;
            }

            if (_.isDate(searchCriteria.requestEndDate) && searchCriteria.requestEndDate.valueOf() === minDate.valueOf()) {
                searchCriteria.requestEndDate = undefined;
            }
        };

        clearUpEmptyDate($scope.searchCriteria);

        var convertToSearchCriteriaDto = function() {
            var searchCriteria = {};
            searchCriteria.patientName = $scope.searchCriteria.patientName;
            searchCriteria.patientNo = $scope.searchCriteria.patientNo;
            searchCriteria.insuranceNumber = $scope.searchCriteria.insuranceNumber;
            searchCriteria.identityCard = $scope.searchCriteria.identityCard;
            searchCriteria.statusList = _.map($scope.searchCriteria.statusList, function(status) {
                if (status.hasOwnProperty('value')) {
                    return status.value;
                }
                return status;
            });

            clearUpEmptyDate($scope.searchCriteria);

            searchCriteria.consultationStartDate = $scope.searchCriteria.consultationStartDate;
            searchCriteria.consultationEndDate = $scope.searchCriteria.consultationEndDate;
            searchCriteria.requestStartDate = $scope.searchCriteria.requestStartDate;
            searchCriteria.requestEndDate = $scope.searchCriteria.requestEndDate;
            searchCriteria.includeDeleted = $scope.searchCriteria.includeDeleted;
            
            return searchCriteria;
        };

        $scope.search = function() {
            $log.debug('ConsultationRequestAdvancedSearchController.search()');

            if (!$scope.searchCriteria ||
                (!$scope.searchCriteria.patientName &&
                    !$scope.searchCriteria.patientNo &&
                    !$scope.searchCriteria.insuranceNumber &&
                    !$scope.searchCriteria.identityCard &&
                    !$scope.searchCriteria.statusList &&
                    !$scope.searchCriteria.consultationStartDate &&
                    !$scope.searchCriteria.consultationEndDate &&
                    !$scope.searchCriteria.requestStartDate &&
                    !$scope.searchCriteria.requestEndDate)) {
                return;
            }

            var timestamp = Date.now();
            $log.debug(timestamp);
            $state.go('ris.consultation.requests', {
                searchCriteria: convertToSearchCriteriaDto(),
                timestamp: timestamp,
                highlightStatus: {
                    requests: enums.consultationRequestStatus.None
                },
                reload: true
            });

            $rootScope.$broadcast(application.events.consultationAdvancedSearchRequestsCompleted, $scope.searchCriteria);
            $modalInstance.close();
        };

        $scope.resetCriteria = function() {
            $scope.searchCriteria = {};
        };

        $scope.cancel = function() {
            clearUpEmptyDate($scope.searchCriteria);
            $modalInstance.dismiss();
        };

        $scope.addShortcut = function() {
            $scope.user = {
                isJuniorDoctor: loginUser.isDoctor,
                isConsultationCenter: loginUser.isConsAdmin,
                isExpert: loginUser.isExpert,
                userId: loginUser.user.uniqueID
            };

            $scope.shortcutCategory = $scope.user.isJuniorDoctor ? enums.shortcutCategory.RequestSearchDoctor :
                $scope.user.isConsultationCenter ? enums.shortcutCategory.RequestSearchCenter : $scope.user.isExpert ? enums.shortcutCategory.RequestSearchExpert : enums.shortcutCategory.All;

            var shortcut = {
                uniqueID: RIS.newGuid(),
                category: $scope.shortcutCategory,
                name: $scope.shortcut.name,
                owner: loginContext.userId,
                value: JSON.stringify(convertToSearchCriteriaDto()),
                ignoreDuplicatedName: true
            };

            consultationService.addShortcut(shortcut)
                .success(function(data) {
                    // TODO: reload shortcuts
                    $rootScope.$broadcast(application.events.consultationRequestShortcutAdded);
                }).finally(function() {
                    // TODO avoid doing this
                    $('#showShortcutNameButton').popover('hide');
                });
        };

        $scope.statusOptions = _.map(enums.consultationRequestStatusMap, function(value, key) {
            return {
                value: key,
                text: value
            };
        });
    }
]);
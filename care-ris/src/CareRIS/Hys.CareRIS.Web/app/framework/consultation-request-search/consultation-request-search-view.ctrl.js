frameworkModule.controller('ConsultationRequestSearchController', ['$log', '$scope', '$state', '$modal', '$timeout', 'enums', 'application',
    function($log, $scope, $state, $modal, $timeout, enums, application) {

        $log.debug('ConsultationRequestSearchController.ctor()...');

        var previousAdvancedSearchCriteria = {};

        $scope.search = function() {

            $log.debug('ConsultationRequestSearchController.search()');

            // clear the previous criteria if simple search has been performed.
            previousAdvancedSearchCriteria = {};

            $timeout(function() {
                if (!$scope.searchVal ||
                    (!$scope.searchVal.patientName && !$scope.searchVal.patientNo && !$scope.searchVal.insuranceNumber && !$scope.searchVal.identityCard)) {
                    return;
                }
                var searchCriteria = {};
                searchCriteria.patientName = $scope.searchVal.patientName;
                searchCriteria.patientNo = $scope.searchVal.patientNo;
                searchCriteria.insuranceNumber = $scope.searchVal.insuranceNumber;
                searchCriteria.identityCard = $scope.searchVal.identityCard;

                var timestamp = Date.now();
                $log.debug(timestamp);
                $state.go('ris.consultation.requests', {
                    searchCriteria: searchCriteria,
                    timestamp: timestamp,
                    highlightStatus: {
                        requests: enums.consultationRequestStatus.None
                    },
                    reload: true
                });

            }, 100, true);
        };



        $scope.showAdvancedSearchDialog = function() {
            var buildCriteriaForDisplay = function(criteria) {
                if (_.isArray(criteria.statusList) && criteria.statusList.length > 0 && !_.isObject(criteria.statusList[0])) {
                    var statusOptions = _.map(enums.consultationRequestStatusMap, function(value, key) {
                        return {
                            value: key,
                            text: value
                        };
                    });

                    var statusList = [];
                    _.each(criteria.statusList, function(status) {
                        var foundStatus = _.findWhere(statusOptions, {
                            value: status.toString()
                        });
                        if (foundStatus) {
                            statusList.push(foundStatus);
                        }
                    });

                    criteria.statusList = statusList;
                }

                criteria.consultationStartDate = _.isDate(criteria.consultationStartDate) ?
                    criteria.consultationStartDate : new Date(criteria.consultationStartDate);
                criteria.consultationEndDate = _.isDate(criteria.consultationEndDate) ?
                    criteria.consultationEndDate : new Date(criteria.consultationEndDate);
                criteria.requestStartDate = _.isDate(criteria.requestStartDate) ?
                    criteria.requestStartDate : new Date(criteria.requestStartDate);
                criteria.requestEndDate = _.isDate(criteria.requestEndDate) ?
                    criteria.requestEndDate : new Date(criteria.requestEndDate);
            };

            var modalInstance = $modal.open({
                templateUrl:'/app/framework/consultation-request-search/consultation-request-advanced-search-view.html',
                controller: 'ConsultationRequestAdvancedSearchController',
                scope: $scope.$new(),
                backdrop: 'static',
                keyboard: false,
                resolve: {
                    previousAdvancedSearchCriteria: function() {
                        buildCriteriaForDisplay(previousAdvancedSearchCriteria);
                        return previousAdvancedSearchCriteria;
                    }
                }
            });
        };

        $scope.$on(application.events.consultationAdvancedSearchRequestsCompleted, function(event, criteria) {
            previousAdvancedSearchCriteria = criteria;
            $scope.searchVal = {};
        });
    }
]);
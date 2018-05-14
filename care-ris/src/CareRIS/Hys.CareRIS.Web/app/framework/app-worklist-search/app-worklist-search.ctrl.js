frameworkModule.controller('AppWorklistSearch', ['$rootScope', '$scope', '$log', 'worklistService', '$location', '$timeout', 'application', 'constants',
    function ($rootScope, $scope, $log, worklistService, $location, $timeout, application, constants) {
        'use strict';
        $log.debug('AppWorklistSearch.ctor()...');

        // since the simple search input cannot be modified by other child scope, use event to do this
        $scope.$on('event:clearSimpleSearchValue', function (e, criteria) {
            $scope.SearchVal = {};
        });
        // simple search on the menu bar, the search logic will be included in registration module
        $scope.search = function () {

            if (!$scope.SearchVal || (!$scope.SearchVal.PatientName && !$scope.SearchVal.PatientNo && !$scope.SearchVal.AccNo)) {
                return;
            }

            var searchCriteria = {
                pagination: {
                    pageIndex: 1,
                    pageSize: constants.pageSize
                }
            };

            searchCriteria.patientName = $scope.SearchVal.PatientName;
            searchCriteria.patientNo = $scope.SearchVal.PatientNo;
            searchCriteria.accNo = $scope.SearchVal.AccNo;

            //not worlist, notice process and go worklist page by timeout
            if ($location.path() != '/ris/worklist/registrations') {
                $rootScope.$broadcast('event:startSearch');
                //wait to process startSearch
                $timeout(function () {
                    $location.path('ris/worklist/registrations');
                    $location.url($location.path());
                    // if not in worklist page, broadcast can not process
                    $timeout(function () {
                        $rootScope.$broadcast(application.events.searchRegistration, searchCriteria);
                    }, 0, true);
                }, 0, true);
            }
            else {
                $rootScope.$broadcast(application.events.searchRegistration, searchCriteria);
            }
        };
        // the advanced search stuff will be included in worlist module
        $scope.showAdvancedSearch = function () {
            $rootScope.$broadcast('event:showAdvancedSearch');
        };

        (function initialize() {
            $log.debug('AppWorklistSearch.initialize()...');

            $scope.SearchVal = null;
        }());
    }
]);
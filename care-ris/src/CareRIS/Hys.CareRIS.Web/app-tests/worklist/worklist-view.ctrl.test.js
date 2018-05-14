/// <reference path="../../Scripts/jasmine.js" />
/// <reference path="../../app/worklist/controllers/worklist-view.ctrl.js" />

// Test for WorklistController
describe('Worklist View Controller Test', function () {
    'use strict';

    function mockService(successData, errorData) {
        return function () {
            return {
                success: function (successCallback) {
                    successCallback(successData);
                    return {
                        error: function (errorCallback) {
                            errorCallback(errorData);
                        }
                    }
                }
            }
        }
    }

    var scope, mockWorklistService;

    var data = {
        worklistItems: [],
        pagination: {
            totalCount: 100,
            pageIndex: 1,
            pageSize: 20
        }
    };

    // load relevant modules and initialize test data before starting any specs
    beforeEach(module('app'));
    beforeEach(module('app.worklist', function ($provide) {
        mockWorklistService = {
            searchWorklistItems: mockService(data),
        };

        $provide.value('worklistService', mockWorklistService);
    }));

    // Check logic in controller
    it('Search worklist items', function () {
        inject(function ($rootScope, $controller) {
            scope = $rootScope.$new();
            $controller('WorklistController', {
                $scope: scope
            });
        });

        var searchWorklistItemsSvc = spyOn(mockWorklistService, 'searchWorklistItems').and.callThrough();
        scope.search();

        expect(searchWorklistItemsSvc).toHaveBeenCalled();
        expect(scope.data).jsonEquals(data);
    });
});
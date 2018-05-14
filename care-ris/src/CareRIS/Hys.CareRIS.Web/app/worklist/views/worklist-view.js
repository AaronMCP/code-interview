worklistModule.directive('worklistView', ['$log', '$timeout', '$compile', function ($log, $timeout, $compile) {
    $log.debug('worklistView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/worklist/views/worklist-view.html',
        controller: 'WorklistController',
        replace: true,
        scope: {
        },
        link: function (scope, element) {
            var $e = $(element);
            var $registrationsContainer = $e.find('#registrationsContainer');
            var $registrationContainer = $e.find('#registrationContainer');
            var $reportContainer = $e.find('#reportContainer');

            // fix ui-view cannot be append class in HTML issue
            $timeout(function () {
                $registrationsContainer.addClass('content-container');
                $registrationContainer.addClass('content-container');
                $reportContainer.addClass('content-container');
            }, 100);

            // handlke kendo splitter size change
            scope.onResize = function () {
                scope.$broadcast('event:sidePanelResize');
            };
            scope.timesliceOption = {};
            scope.timesliceWinActived = function (e) {
                var ae = angular.element(e.sender.element).find('.timeslice-win-content');
                ae.html('<time-slice-view option="timesliceOption"></time-slice-view>');

                $compile(ae.contents())(scope);
            };
            scope.timesliceWinDeactivated = function (e) {
                var ae = angular.element(e.sender.element).find('.timeslice-win-content');
                ae.empty();
            };
        }
    };
}]);
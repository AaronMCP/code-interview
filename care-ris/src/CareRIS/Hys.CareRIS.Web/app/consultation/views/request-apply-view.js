consultationModule.directive('requestApplyView', ['$log', 'application', '$compile', '$timeout',
    function ($log, application, $compile, $timeout) {
    $log.debug('requestApplyView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/request-apply-view.html',
        controller: 'RequestApplyController',
        replace: true,
        scope: {
        },
        link: function (scope, element) {
            var $e = $(element);
            var $mainContent = $('#applyRequestContainer');
            var $applyRequestFormContainer = $e.find('#applyRequestFormContainer');
            var onResize = function () {
                $timeout(function () {
                    var newHeight = $mainContent.height() - 60;
                    if (newHeight > 0) {
                        $applyRequestFormContainer.height(newHeight);
                    }
                }, 100);
            };
            scope.onResize = onResize;
            $(window).resize(onResize);
            onResize();
            scope.today = new Date();

            scope.selectOtherItem = function () {
                scope.selectOtherData.isSelectedOther = true;
                if (scope.selectedHospitalItem) {
                    scope.applyRequestData.selectHospital = scope.selectedHospitalItem.value;
                } else if (scope.selectedExpertItems) {
                    scope.applyRequestData.selectExperts = _.pluck(scope.selectedExpertItems, 'value');
                }
                if ($("#lvSelectHospitalExpert").data('kendoListView')) {
                    $("#lvSelectHospitalExpert").data('kendoListView').clearSelection();
                }
            };

            scope.validateDefaultItem = function(id) {
                var filter = '[id="' + id+ '"]';
                var selectHospitalExpertLv = $("#lvSelectHospitalExpert").data('kendoListView');
                if (selectHospitalExpertLv.element.children().filter(filter).length > 0) {
                    selectHospitalExpertLv.clearSelection();
                    selectHospitalExpertLv.select(selectHospitalExpertLv.element.children().filter(filter).first());
                    return true;
                } else {
                    return false;
                }
            };
        }
    };
}]);

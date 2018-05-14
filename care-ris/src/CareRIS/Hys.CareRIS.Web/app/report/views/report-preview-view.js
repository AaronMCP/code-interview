reportModule.directive('reportPreviewView', ['$log', '$timeout',  function ($log, $timeout) {
    $log.debug('reportPreviewView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/report/views/report-preview-view.html',
        controller: 'ReportPreviewController',
        scope: {
            reportId: '@',
            procedureId: '@',
            printId: '@',
            from: '@',
            patientCaseOrderId: '@'
        },
        link: function (scope, element) {
            var $e = $(element);
            var $mainContent = $('#reportContainer');
            var $inputContainer = $e.find('#inputContainer');

            var onResize = function () {
                $timeout(function () {
                    var newHeight = $mainContent.height() - 55;
                    if (newHeight > 0) {
                        $inputContainer.height(newHeight);
                    }
                }, 100);
            };

            $(window).resize(onResize);

            onResize();
        }
    };
}]);
consultationModule.directive('examAddItem', ['$log', 'application', '$compile',
function ($log, application, $compile) {
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/exam-add-item.html',
        controller: 'ExamAddItemController',
        scope: {
            cancel: "&onCancel",
            confirm: "&onConfirm",
            operateItem: "=",
            module: "=",
            disableEdit: '='
        },
        replace: true,
        link: function (scope, element) {
            scope.today = new Date();
            element.find("#addItemExamDate").on("keydown", function () { return false; }).on("click", function () {
                if (!scope.examDatePicker.options.opened)
                    scope.examDatePicker.open();
            });

            scope.selectDICOM = function () {
                scope.dicomWin.open();
            };
            scope.cancelDicom = function () {
                scope.dicomWin.close();
            };
            scope.confirmDicom = function (dicom) {
                _.map(dicom,function(value,key) {
                    scope.item[key] = scope.item[key] || value;
                });
                scope.addFile('folder', dicom.dicomPath);
                scope.dicomWin.close();
            };
            scope.activeDicomWin = function (e) {
                var ae = angular.element(e.sender.element);
                var tpl = '<dicom-list-view is-select-dicom="true" cancel-dicom="cancelDicom()" confirm-dicom="confirmDicom(dicom)"></dicom-list-view>';
                ae.html(tpl);
                $compile(ae.contents())(scope);
            }
            scope.deactiveDicomWin = function (e) {
                var ae = angular.element(e.sender.element);
                ae.html('');
            }
        }
    }
}]);
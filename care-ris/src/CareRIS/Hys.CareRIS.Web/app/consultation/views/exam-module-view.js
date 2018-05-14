consultationModule.directive('examModuleView', ['$log', 'application', '$compile', 'examService', '$translate', 'openDialog',
function ($log, application, $compile, examService, $translate, openDialog) {
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/exam-module-view.html',
        controller: 'ExamModuleController',
        scope: {
            module: '=',
            cellNet: '=containerNet',
            pageIndex: '=containerPage',
            belongToPage: '=',
            patientCaseId: '=',
            updateModule: '&updateModule',
            disableEdit: '='
        },
        replace: true,
        link: function (scope, element) {
            var ele = $(element).css(scope.module.css).data('moduleSetting', scope.module);

            scope.module.modulePanel = ele;

            scope.needShow = function () {
                var visible = scope.pageIndex + '' === scope.belongToPage + '';
                if (visible) {
                    angular.forEach(scope.module.cells, function (item) {
                        item.cell.data("belongToModulePannel", ele);
                    });
                } else {
                    angular.forEach(scope.module.cells, function (item) {
                        var oldPanel = item.cell.data("belongToModulePannel");
                        if (oldPanel && oldPanel.is(ele))
                            item.cell.removeData("belongToModulePannel");
                    });
                }
                return visible;
            }
            function removeModule() {
                var module = scope.module;
                scope.module.modulePanel.remove();
                scope.module.visible = false;
                scope.updateModule({ module: scope.module });
                examService.removeModulesData(scope.module.type);
                scope.$emit("event:removeModule", module);
            }

            scope.removeModule = function () {
                openDialog.openIconDialogYesNo(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant("ConfrimDelete"), function () {
                    removeModule();
                });
            }

            scope.$on("event:moduleSizeChanging", function (e, module, selectedCells) {
                if (module.id !== scope.module.id) return;

                var css = scope.cellNet.getCssBySquares(selectedCells);

                angular.forEach(module.cells, function (item) {
                    item.cell.removeData("belongToModulePannel");
                });

                angular.forEach(selectedCells, function (item) {
                    item.cell.data("belongToModulePannel", ele);
                });

                module.cells = selectedCells;

                css.position.push(scope.pageIndex)
                module.position = css.position;
                module.css = css.css;
                ele.css(css.css);
                scope.updateModule({ module: scope.module });
                e.preventDefault();
            });
            scope.$on("event:modulePositionChanging", function (e, module, selectedCells) {
                if (module.id !== scope.module.id) return;

                var fromElementCells = scope.module.cells;

                var fromElementSize = {
                    rowLen: fromElementCells.maxCell.row - fromElementCells.minCell.row + 1
                    , colLen: fromElementCells.maxCell.col - fromElementCells.minCell.col + 1
                }
                var minCell = selectedCells.minCell;
                var position = [minCell.row, minCell.col, fromElementSize.colLen, fromElementSize.rowLen, scope.pageIndex];
                var area = scope.cellNet.getCellArea(position);
                angular.forEach(fromElementCells, function (item) { item.cell.removeData("belongToModulePannel"); });
                angular.forEach(area.cells, function (item) { item.cell.data("belongToModulePannel", ele); });

                scope.module.cells = area.cells;
                scope.module.position = position;
                scope.module.css = area.css;
                ele.css(area.css);
                scope.updateModule({ module: scope.module });
                e.preventDefault();
            });
            scope.$on(application.consultationSidePanelResize, function () {
                var cellArea = scope.cellNet.getCellArea(scope.module.position);

                scope.module.css = cellArea.css;
                scope.module.modulePanel.css(scope.module.css);
            });
        }
    }
}]);
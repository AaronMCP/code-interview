//<exam-info-view
//      on-filename-changed="filenameChanged(patientCaseId,uniqueId,fileName)"
//      on-file-deleted="fileDeleted(patientCaseId,uniqueId)"
//      on-item-added="itemAdded(patientCaseId,newItem,complete)"
//      on-item-deleted="itemDeleted(patientCaseId,emrItemId)"
//      on-item-edited="itemEdited(patientCaseId,item,complete)"
//      on-module-updated="moduleUpdated(module)"
//      disable-edit="disableEdit"
//      modules-init-value="modulesInitValue" /* {'ultrasound':[{patientId:{},fileList:[{fileName:'',fileType:'folder',path:''}]},{}],'teleradiology':[]} */
//      patient-case-id="">
//</exam-info-view>

consultationModule.directive('examInfoView', ['$log', 'application', '$compile', 'examService',
function ($log, application, $compile, examService) {
    $log.debug('examInfoView.ctor()...');

    var createNet = function (netSetting, existsCellNet) {
        var setting = $.extend({
            container: null,
            insertBefore: null,
            coordinateSetting: {}
        }, netSetting);

        if (setting.container && setting.container.size() > 0) {
            var cells = $.coordinate(setting.container, setting.coordinateSetting);

            if (existsCellNet) {
                angular.forEach(existsCellNet.cells, function (existsCell) {
                    var newCell = cells.getItemByCoordinate(existsCell.row, existsCell.col);
                    var newCSS = {
                        left: newCell.left,
                        top: newCell.top,
                        width: newCell.width,
                        height: newCell.height
                    };

                    existsCellNet.cells.calculatePosition(existsCell, newCSS);
                    existsCell.cell.css(newCSS);
                });
                return existsCellNet;
            }

            if (setting.insertBefore && setting.insertBefore.size() > 0) {
                var cellCellection = $();
                angular.forEach(cells, function (item) {
                    var cssSetting = {
                        left: item.left,
                        top: item.top,
                        width: item.width,
                        height: item.height
                    };
                    var cellHtml = '<div class="ei-module-cell"></div>'

                    var cell = $(cellHtml).css(cssSetting);

                    cellCellection = cellCellection.add(cell);
                    var cellInfo = $.extend({}, item, true);
                    cell.data("positionInfo", cellInfo);
                    item.cell = cell;
                });
                cellCellection.insertBefore(setting.insertBefore);
            }

            setting.container.data("cells", cells);
            return {
                container: setting.container
                , cells: cells
                , getCssBySquares: function (squares) {
                    var cssString = ""
                    , css = {
                        left: squares.minCell.point.A.x
                        , top: squares.minCell.point.A.y
                        , width: squares.maxCell.point.C.x - squares.minCell.point.A.x
                        , height: squares.maxCell.point.C.y - squares.minCell.point.A.y
                    };
                    var cssString = "left:" + css.left + "px;top:" + css.top + "px;width:" + css.width + "px;height:" + css.height + "px;";
                    return {
                        css: css
                        , cssString: cssString
                        , position: [
                            squares.minCell.row
                            , squares.minCell.col
                            , squares.maxCell.col - squares.minCell.col + 1
                            , squares.maxCell.row - squares.minCell.row + 1
                        ]
                    };
                }
                , getCellArea: function (position) {
                    var cells = this.cells;
                    var row = position[0], col = position[1], width = position[2], height = position[3];
                    var maxRow = row + height - 1, maxCol = col + width - 1;
                    var squares = cells.getSquaresByCoordinate({ row: row, col: col }, { row: maxRow, col: maxCol });
                    var css = this.getCssBySquares(squares);
                    css.cells = squares;

                    return css;
                }
            };
        }
        return null;
    };
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/exam-info-view.html',
        controller: 'ExamInfoController',
        scope: {
            patientCaseId: '=?patientCaseId',
            changeFileName: '&?onFilenameChanged',
            deleteFile: '&?onFileDeleted',
            addItem: '&?onItemAdded',
            deleteItem: '&?onItemDeleted',
            editItem: '&?onItemEdited',
            moduleUpdated: '&?onModuleUpdated',
            modulesInitValue: '=?',
            disableEdit: '=?'
        },
        replace: true,
        link: function (scope, element) {
            var container = element.find(".ei-work-panel");

            var panelHolder = container.find(".module-panel-placeholder");
            scope.modulesInitValue = scope.modulesInitValue || {};
            scope.cellNet = null;
            var drawCellNet = function () {
                if (container.is(":visible")) {
                    scope.cellNet = createNet({
                        container: container,
                        insertBefore: panelHolder,
                        coordinateSetting: { xLen: 12, ylen: 6 }
                    }, scope.cellNet);
                }
            };

            drawCellNet();

            scope.pageIndex = -1;
            scope.pagedModule = {};

            var invisibleModules = [];
            scope.allPages = [];
            scope.allModules = [];
            scope.drawModules = function (modules) {
                var tmpAllPage = [];
                var tmpPagedModule = {};

                angular.forEach(modules, function (module) {
                    module.originalObject = _.clone(module);
                    module.position = angular.fromJson('[' + module.position + ']');
                    module.initContentValue = scope.modulesInitValue[module.type] || [];
                    if (module.visible) {
                        var cellArea = scope.cellNet.getCellArea(module.position);

                        module.css = cellArea.css;
                        module.cells = cellArea.cells;
                        
                        var page = module.position[4] || 1;
                        module.page = page;

                        if (page in tmpPagedModule) {
                            tmpPagedModule[page].push(module);
                        }
                        else {
                            tmpAllPage.push(page);
                            tmpPagedModule[page] = [module];
                        }
                    } else {
                        invisibleModules.push(module);
                    };

                    scope.allModules.push(module);
                });

                tmpAllPage.sort(function (a, b) { return a > b; });
                tmpAllPage.length < 1 && tmpAllPage.push(1);

                scope.pagedModule = tmpPagedModule;

                scope.allPages = tmpAllPage;
                scope.pageIndex = scope.allPages[0];
            };
            scope.$on(application.consultationSidePanelResize, function () {
                drawCellNet();
            });
            scope.getInvisibleModules = function () {
                return invisibleModules;
            };

            var fromElement = null;
            var fromPoint = {};
            var selectedDynamicArea = container.find(".selectedDynamicArea");
            var modulePanelSizeAdjust = container.find(".module-panel-size-adjust");
            var getRelativePosition = function (relativeContainer, eventData) {
                var relativeContainerOffset = relativeContainer.offset();
                return {
                    left: eventData.originalEvent.pageX - relativeContainerOffset.left
                    , top: eventData.originalEvent.pageY - relativeContainerOffset.top
                };
            }
            var showModulePanelHolder = function (topLeftPoint, bottomRightPoint, modulePanel) {
                var cells = scope.cellNet.cells;

                var selectedCells = cells.getSquaresByOffset(topLeftPoint, bottomRightPoint);
                if (selectedCells) {
                    var canMove = true;
                    for (var i = 0; i < selectedCells.length; ++i) {
                        var cell = selectedCells[i].cell;
                        var belongTo = cell.data("belongToModulePannel");
                        if (belongTo && (!modulePanel || !belongTo.is(modulePanel))) {//when the cell was selected by some module,can't move
                            panelHolder.addClass("disabled");
                            canMove = false;
                            break;
                        }
                    }
                    var css = {
                        left: selectedCells.minCell.point.A.x
                        , top: selectedCells.minCell.point.A.y
                        , width: selectedCells.maxCell.point.C.x - selectedCells.minCell.point.A.x
                        , height: selectedCells.maxCell.point.C.y - selectedCells.minCell.point.A.y
                    }
                    if (canMove) {
                        panelHolder.removeClass("disabled");
                    }
                    panelHolder.data("holderCells", selectedCells).css(css).show();
                }
            };
            var showInvisibleModuleListWindow = function () {
                scope.invisibleModuleWin.center().open();
            }
            scope.contentWindowOpened = function (e) {
                var ae = angular.element(e.sender.element);
                var tpl = '<exam-add-item disable-edit="disableEdit" on-cancel="operateContentWin.close()" on-confirm="confirmOperateItem(item)" operate-item="operateItem" module="operateModule"></exam-add-item>';
                ae.html(tpl);

                $compile(ae.contents())(scope);
            }
            scope.contentWindowClosed = function (e) {
                var ae = angular.element(e.sender.element);
                ae.html("");
            }
            scope.selectModule = function (module) {
                var len = invisibleModules.length;
                for (var i = 0; i < len; ++i) {
                    var item = invisibleModules[i];
                    if (module.id == item.id) {
                        var cells = panelHolder.data("holderCells");
                        item.cells = cells;
                        var cssObj = scope.cellNet.getCssBySquares(cells);

                        item.css = cssObj.css;

                        cssObj.position.push(scope.pageIndex);

                        item.position = cssObj.position;
                        item.page = scope.pageIndex;
                        invisibleModules.splice(i, 1);
                        item.visible = true;
                        scope.pagedModule[scope.pageIndex].push(item);
                        scope.updateModule(item);
                        break;
                    }
                }
                scope.invisibleModuleWin.close();
            }
            scope.$on("event:removeModule", function (e, module) {

                var setting = module;
                angular.forEach(setting.cells, function (item) { item.cell.removeData("belongToModulePannel"); });

                delete setting.cells;

                var moduleSize = scope.pagedModule[setting.page].length;

                for (var i = 0; i < moduleSize; ++i) {
                    //splice
                    if (scope.pagedModule[setting.page][i].id == setting.id) {
                        scope.pagedModule[setting.page].splice(i, 1);
                        break;
                    }
                }
                delete setting.page;
                invisibleModules.push(setting);

                e.stopPropagation();
            });

            scope.$on(application.consultationSidePanelResize, function () {
                var moduleLen = 0, tmpModules = [], tmpModule = null, tmpCellArea;
                for (var page in scope.pagedModule) {
                    if (page === scope.pageIndex) continue;

                    tmpModules = scope.pagedModule[page];
                    moduleLen = tmpModules.length;
                    for (var i = 0; i < moduleLen; ++i) {
                        tmpModule = tmpModules[i];
                        tmpCellArea = scope.cellNet.getCellArea(tmpModule.position);
                        tmpModule.css = tmpCellArea.css;
                    }
                }
            });

            if (scope.disableEdit) return;

            container.kendoDraggable({
                filter: ".ei-module-cell,.module-panel,.module-panel-border",
                group: "moduleContainer",
                ignore: ":not(.ei-module-cell,.module-move-handle,.module-panel-border),button,a,input",
                container: container,
                hint: function (element) {
                    if (element.is(".module-panel")) {
                        return element.clone().addClass("module-panel-placeholder-temp");
                    }
                    return null;
                },
                dragstart: function (e) {
                    fromElement = e.currentTarget;
                    if (fromElement.is(".ei-module-cell")) {
                        panelHolder.hide();
                        var point = getRelativePosition(container, e);
                        point.width = 0;
                        point.height = 0;
                        selectedDynamicArea.css(point).show();
                        fromPoint = point;
                        container.addClass("module-panel-drawing");
                    }
                    if (fromElement.is(".module-panel-border")) {
                        var modulePan = fromElement.closest(".module-panel");
                        var borderRole = fromElement.data("border");
                        var position = modulePan.position();
                        var modulePanSize = {
                            left: position.left,
                            top: position.top,
                            width: modulePan.width(),
                            height: modulePan.height()
                        };

                        modulePanelSizeAdjust.css(modulePanSize).show();
                        panelHolder.css(modulePanSize).show();

                        var borderParameter = {
                            widgetPosition: modulePanSize
                            , modulePan: modulePan
                            , borderRole: borderRole
                            , containerHeight: container.height()
                            , containerWidth: container.width()
                        };
                        fromElement.data("borderParameter", borderParameter);
                        switch (borderParameter.borderRole) {
                            case "top":
                            case "bottom":
                                container.addClass("module-border-horizon-resize");
                                break;
                            case "left":
                            case "right":
                                container.addClass("module-border-vertical-resize");
                                break;
                            case "north-west":
                            case "south-east":
                                container.addClass("module-border-corner-nwse");
                                break;
                            case "north-east":
                            case "south-west":
                                container.addClass("module-border-corner-nesw");
                                break;
                        }
                    }
                    if (fromElement.is(".module-panel")) {
                        fromElement.hide();
                    };
                },
                drag: function (e) {
                    if (fromElement.is(".ei-module-cell")) {
                        var point = getRelativePosition(container, e);

                        var dynamicPoint = {}, toPoint = {};
                        if (point.left < fromPoint.left) {
                            dynamicPoint.left = point.left;
                            toPoint.left = fromPoint.left;
                        }
                        else {
                            dynamicPoint.left = fromPoint.left;
                            toPoint.left = point.left;
                        }
                        if (point.top < fromPoint.top) {
                            dynamicPoint.top = point.top;
                            toPoint.top = fromPoint.top;
                        }
                        else {
                            dynamicPoint.top = fromPoint.top;
                            toPoint.top = point.top;
                        }

                        selectedDynamicArea.css({
                            left: dynamicPoint.left
                            , top: dynamicPoint.top
                            , width: toPoint.left - dynamicPoint.left
                            , height: toPoint.top - dynamicPoint.top
                        });
                        showModulePanelHolder(dynamicPoint, toPoint);
                    }
                    if (fromElement.is(".module-panel")) {
                        var holder = $(".module-panel-placeholder-temp");
                        var holderOffset = holder.offset();
                        var containerOffset = container.offset();
                        var topLeftPoint = { left: holderOffset.left - containerOffset.left, top: holderOffset.top - containerOffset.top };
                        var bottomRightPoint = { left: topLeftPoint.left + holder.outerWidth(), top: topLeftPoint.top + holder.outerHeight() };
                        showModulePanelHolder(topLeftPoint, bottomRightPoint, fromElement);
                    }
                    if (fromElement.is(".module-panel-border")) {
                        var borderParameter = fromElement.data("borderParameter");
                        var cursorPosition = getRelativePosition(container, e);

                        var newPosition = {
                            top: borderParameter.widgetPosition.top
                            , left: borderParameter.widgetPosition.left
                            , width: borderParameter.widgetPosition.width
                            , height: borderParameter.widgetPosition.height
                        };

                        switch (borderParameter.borderRole) {
                            case "top":
                                newPosition.height = newPosition.top - cursorPosition.top + newPosition.height;
                                newPosition.top = cursorPosition.top;
                                break;
                            case "bottom":
                                newPosition.height = cursorPosition.top - newPosition.top;
                                break;
                            case "left":
                                newPosition.width = newPosition.left - cursorPosition.left + newPosition.width;
                                newPosition.left = cursorPosition.left;
                                break;
                            case "right":
                                newPosition.width = cursorPosition.left - newPosition.left;
                                break;
                            case "north-west":
                                newPosition.width = newPosition.width + newPosition.left - cursorPosition.left;
                                newPosition.height = newPosition.height + newPosition.top - cursorPosition.top;
                                newPosition.left = cursorPosition.left;
                                newPosition.top = cursorPosition.top;
                                break;
                            case "south-east":
                                newPosition.width = cursorPosition.left - newPosition.left;
                                newPosition.height = cursorPosition.top - newPosition.top;
                                break;
                            case "north-east":
                                newPosition.height = newPosition.height + newPosition.top - cursorPosition.top;
                                newPosition.width = cursorPosition.left - newPosition.left;
                                newPosition.top = cursorPosition.top;
                                break;
                            case "south-west":
                                newPosition.height = cursorPosition.top - newPosition.top;
                                newPosition.width = newPosition.width + newPosition.left - cursorPosition.left;
                                newPosition.left = cursorPosition.left;
                                break;
                        }

                        if (newPosition.height > 0 && newPosition.width > 0) {
                            modulePanelSizeAdjust.css(newPosition);
                            var adjustTopLeftPoint = { left: newPosition.left, top: newPosition.top };
                            var adjustBottomRightPoint = {
                                left: adjustTopLeftPoint.left + modulePanelSizeAdjust.outerWidth()
                                , top: adjustTopLeftPoint.top + modulePanelSizeAdjust.outerHeight()
                            };
                            showModulePanelHolder(adjustTopLeftPoint, adjustBottomRightPoint, borderParameter.modulePan);
                        }
                    }
                },
                dragend: function (e) {
                    if (fromElement.is(".ei-module-cell")) {
                        container.removeClass("module-panel-drawing");
                        selectedDynamicArea.hide();
                        if (!panelHolder.is(".disabled")) {
                            showInvisibleModuleListWindow();
                        }
                    }
                    if (fromElement.is(".module-panel-border")) {
                        if (!panelHolder.is(".disabled")) {
                            var selCells = panelHolder.data("holderCells");
                            var borderParameter = fromElement.data("borderParameter");
                            var modulePan = borderParameter.modulePan;

                            scope.$broadcast("event:moduleSizeChanging", modulePan.scope().module, selCells);
                        }

                        container.removeClass("module-border-horizon-resize module-border-vertical-resize module-border-corner-nwse module-border-corner-nesw");
                        modulePanelSizeAdjust.hide();
                    }
                    panelHolder.hide();
                    if (fromElement.is(":hidden")) {
                        $(".module-panel-placeholder-temp").promise().done(function () { fromElement.show(); });
                    }
                },
                dragcancel: function () {
                    if (fromElement.is(".ei-module-cell")) {
                        container.removeClass("module-panel-drawing");
                        selectedDynamicArea.hide();
                    }
                    if (fromElement.is(".module-panel")) {
                        fromElement.show();
                    }
                    if (fromElement.is(".module-panel-border")) {
                        container.removeClass("module-border-horizon-resize module-border-vertical-resize module-border-corner-nwse module-border-corner-nesw")
                        widgetPanelSizeAdjust.hide();
                    }

                    panelHolder.hide();
                }
            });
            container.kendoDropTarget({
                group: "moduleContainer",
                drop: function (e) {
                    if (fromElement.is(".module-panel")) {
                        if (!panelHolder.is(".disabled")) {
                            var module = fromElement.scope().module;
                            var selCells = panelHolder.data("holderCells");

                            scope.$broadcast("event:modulePositionChanging", module, selCells);
                        }
                        panelHolder.hide();
                    }
                    if (fromElement.is(":hidden")) {
                        $(".module-panel-placeholder-temp").promise().done(function () { fromElement.show(); });
                    }
                }
            });
        }
    };
}]);
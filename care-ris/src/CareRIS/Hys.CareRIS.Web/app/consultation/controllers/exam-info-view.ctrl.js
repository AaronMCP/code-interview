consultationModule.controller('ExamInfoController', ['$log', '$scope', 'consultationService', '$translate', '$q', 'examService',
    function ($log, $scope, consultationService, $translate, $q, examService) {
        'use strict';
        $log.debug('ExamInfoController.ctor()...');

        examService.clear();

        $scope.invisibleModuleWinTitle = $translate.instant('SelectModule');
        $scope.disableEdit = !!$scope.disableEdit;
        $scope.showModules = true;
        $scope.viewerSource = [];

        $scope.showViewer = function (moduleType, itemClientId, detailClientId) {
            if (!$scope.patientCaseId) return;

            $scope.showModules = false;
            $scope.iniItem = {};
            moduleType || (moduleType = '');
            itemClientId || (itemClientId = '');
            detailClientId || (detailClientId = '');

            var allModulesLen = $scope.allModules.length, moduleDataLen, detailsLen;
            var tmpModule, tmpItem, tmpItem_1, tmpItem_2, tmpModuleData, tmpMdItem, tmpMdItemDetail;
            var tmpSource = [], selected;

            for (var i = 0; i < allModulesLen; ++i) {
                tmpModule = $scope.allModules[i];
                tmpItem = {
                    text: tmpModule.title,
                    moduleType: tmpModule.type,
                    expanded: true,
                    items: []
                };

                tmpModuleData = examService.modulesData[tmpModule.type] || [];
                moduleDataLen = tmpModuleData.length;

                for (var j = 0; j < moduleDataLen; ++j) {
                    tmpMdItem = tmpModuleData[j];
                    tmpItem_1 = {
                        emrItemId: tmpMdItem.emrItemId,
                        text: tmpMdItem.examDate,
                        expanded: true,
                        items: []
                    };

                    detailsLen = tmpMdItem.itemDetails.length;

                    for (var k = 0; k < detailsLen; ++k) {
                        tmpMdItemDetail = tmpMdItem.itemDetails[k];
                        selected = tmpModule.type === moduleType && tmpMdItem.clientId === itemClientId && tmpMdItemDetail.clientId === detailClientId;
                        tmpItem_2 = {
                            selected: selected,
                            clientId: tmpMdItem.clientId,
                            uniqueId: tmpMdItemDetail.file.uniqueId,
                            text: tmpMdItemDetail.file.fileName,
                            path: tmpMdItemDetail.file.path,
                            ready: tmpMdItemDetail.file.progress >= 100
                        };
                        tmpItem_1.items.push(tmpItem_2);
                        if (selected) {
                            $scope.iniItem = tmpItem_2;
                        }
                    }

                    tmpItem.items.push(tmpItem_1);
                }

                tmpSource.push(tmpItem);
            }
            $scope.viewerSource = tmpSource;
        };

        $scope.changePage = function (page) {
            if ($scope.pageIndex === page) return;
            var prevPagedModules = $scope.pagedModule[$scope.pageIndex];
            var modulesLen = prevPagedModules.length;
            var cellsLen = 0;
            $scope.pageIndex = page;
            for (var i = 0; i < modulesLen; ++i) {
                cellsLen = prevPagedModules[i].cells.length;
                for (var j = 0; j < cellsLen; ++j) {
                    prevPagedModules[i].cells[j].cell.removeData("belongToModulePannel");
                }
            }
        };
        $scope.addPage = function () {
            var pageLen = $scope.allPages.length;
            var nextPage = $scope.allPages[pageLen - 1] + 1;
            var prevPageModules = $scope.pagedModule[$scope.pageIndex];
            var modulesLen = prevPageModules.length;
            var cellsLen = 0;
            var tmpModule;

            $scope.allPages.push(nextPage);
            $scope.pagedModule[nextPage] = [];
            $scope.pageIndex = nextPage;

            for (var i = 0; i < modulesLen; ++i) {
                tmpModule = prevPageModules[i];
                cellsLen = tmpModule.cells.length;
                for (var j = 0; j < cellsLen; ++j) {
                    tmpModule.cells[j].cell.removeData("belongToModulePannel");
                }
            }
        };
        $scope.$on('event:operateContent', function (e, module, item, isNew) {
            var createTrans = $translate.instant('Create');
            var editTrans = $translate.instant('Edit');
            $scope.operateModule = module;
            $scope.operateItem = item;
            $translate('AddExam', { operation: isNew ? createTrans : editTrans }).then(function (title) {
                $scope.operateContentWin.setOptions({ title: title });
                $scope.operateContentWin.center().open();
            });

            e.stopPropagation();
        });
        $scope.$on('event:ItemAdded', function (e, patientCaseId, newItem, complete) {
            $scope.addItem({ patientCaseId: patientCaseId, newItem: newItem, complete: complete });
            e.stopPropagation();
        });
        $scope.$on('event:ItemEdited', function (e, patientCaseId, item, complete) {
            $scope.editItem({ patientCaseId: patientCaseId, item: item, complete: complete });
            e.stopPropagation();
        });
        $scope.$on('event:ItemDeleted', function (e, patientCaseId, emrItemId) {
            $scope.deleteItem({ patientCaseId: patientCaseId, emrItemId: emrItemId });
            e.stopPropagation();
        });
        $scope.$on('event:FileDeleted', function (e, patientCaseId, uniqueId) {
            $scope.deleteFile({ patientCaseId: patientCaseId, uniqueId: uniqueId });
            e.stopPropagation();
        });
        $scope.$on('event:FilenameChanged', function (e, patientCaseId, uniqueId, fileName) {
            $scope.changeFileName({ patientCaseId: patientCaseId, uniqueId: uniqueId, fileName: fileName });
            e.stopPropagation();
        });
        $scope.$on('event:FileSelected', function (e, moduleType, itemClientId, detailClientId) {
            $scope.showViewer(moduleType, itemClientId, detailClientId);
        });
        $scope.confirmOperateItem = function (item) {
            $scope.operateContentWin.close();
            $scope.$broadcast('event:operateItemComplete', $scope.operateModule, item);
        };

        $scope.updateModule = function (module) {
            var clonedModule = _.clone(module.originalObject);
            for (var attr in clonedModule) {
                clonedModule[attr] = module[attr];
            }
            clonedModule.position = clonedModule.position.join(',');

            examService.modules[clonedModule.id] = clonedModule;
            examService.updatedModules[clonedModule.id] = clonedModule;
            if (angular.isFunction($scope.moduleUpdated))
                $scope.moduleUpdated({ module: clonedModule });
        };
        consultationService.getExamModules($scope.patientCaseId).success(function (res) {
            var data = res.modules;
            var dataLen = data.length, tmpObj;
            for (var i = 0; i < dataLen; ++i) {
                tmpObj = data[i];
                examService.modules[tmpObj.id] = _.clone(tmpObj);
            }

            examService.isNew = res.isNew;

            $scope.drawModules(data);
        }).error(function (errorMsg) {
            $log.error(errorMsg);
        });
    }
]);
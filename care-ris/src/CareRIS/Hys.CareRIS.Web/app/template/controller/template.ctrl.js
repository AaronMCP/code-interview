templateModule.controller('TemplateCtrl', ['$scope', 'templateService', 'openDialog', '$translate', 'csdToaster', 'application', 'configurationService', 'dictionaryManager', 'enums', '$log', '$timeout',
    function ($scope, templateService, openDialog, $translate, csdToaster, application, configurationService, dictionaryManager, enums, $log, $timeout) {
        //性别
        var configurationData = application.configuration;
        $scope.genderList = configurationData.genderList;
        var expandIds = [];
        //获取部位分类
        configurationService.getProcedureCodeText().success(function (result) {
            for (var i = 0; i < result.length; i++) {
                if (result[i].frequency === null) {
                    result.splice(i, 1);
                }
            }
            $scope.bodyCategoryList = result;
        });

        //检查部位
        configurationService.getBodySystemMapText().success(function (result) {
            $scope.bodyPartList = result;
        });

        //设备类型
        configurationService.getModalityTypes().success(function (result) {
            $scope.modalityTypeList = result;
        });

        //阳性率
        var getPositiveList = function () {
            dictionaryManager.getDictionaries(enums.dictionaryTag.positive).then(function (data) {
                if (data) {
                    $scope.positiveList = data;
                    $scope.positiveList.sort(function (a, b) {
                        return (a.orderID > b.orderID ? 1 : -1);
                    });
                }
            });
        };

        // 处理数据
        var dataHandle = function (data) {
            for (var i = 0; i < data.length; i++) {
                data[i].text = data[i].name;
                data[i].itemName = data[i].name;
                data[i].uniqueID = data[i].id;
            }
        }
        $scope.treeData = new kendo.data.HierarchicalDataSource({
            transport: {
                read: function (options) {
                    templateService.getTemplateByParentID(options.data.id).success(function (res) {
                        if (res !== null) {
                            dataHandle(res);
                            options.success(res);
                            $timeout(function () {
                                $scope.tree.expandPath(expandIds);
                            }, 200);
                        } else {
                            options.error({});
                        }
                    }).error(function (data, status, headers, config) {
                        options.error({});
                    });
                }
            },
            messages: {
                requestFailed: '没有子目录'
            },
            schema: {
                model: {
                    id: "id",
                    hasChildren: 'hasChildren'
                }
            }
        });

        // 点击目录
        $scope.click = function (dataItem) {
            $scope.selectedItem = dataItem;
            $scope.resetName = angular.copy($scope.selectedItem.name);
            if (!dataItem.hasChildren) {
                $scope.isEdit = true;
                $scope.isSaveAnother = true;
                $scope.isNew = false;
                templateService.getTemplateByID(dataItem.uniqueID).success(function (res) {
                    res.wysText = res.wysText === '\0' ? '' : res.wysText;
                    res.wygText = res.wygText === '\0' ? '' : res.wygText;
                    $scope.template = res;
                    if ($scope.template)
                        $scope.template.isPositive = $scope.template.isPositive + "";
                })
            } else {
                $scope.isEdit = false;
                $scope.isSaveAnother = false;
                $scope.isNew = true;
                $scope.template = null;
            }
        };

        // 新增目录
        $scope.addBelow = function () {
            if ($scope.selectedItem) {
                if ($scope.selectedItem.hasChildren) {
                    $scope.addCatalogNameWindow.open();
                } else {
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '该目录下无法添加子目录');
                }
            } else {
                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '请先选择一个目录');
            }

        };

        $scope.expandNode = function (e) {
            var item = $scope.tree.dataItem(e.node);
            if (expandIds.indexOf(item.id) < 0) {
                expandIds.push(item.id);
            }
        }
        $scope.collapseNode = function (e) {
            var item = $scope.tree.dataItem(e.node);
            var id = item.id;
            var index = expandIds.indexOf(id);
            if (index >= 0) {
                expandIds.splice(index, 1);
            }
        };
        // 新增目录 点击确定
        $scope.addSure = function () {
            var templateCatalog = {
                type: $scope.selectedItem.type,
                parentId: $scope.selectedItem.uniqueID,
                itemName: $scope.catalogName
            }
            templateService.isTemplateExist({ itemName: $scope.catalogName }).success(function (res) {
                if (res) {
                    templateService.newTemplateDirec(templateCatalog).success(function (resNew) {
                        if (resNew) {
                            resNew.text = resNew.itemName;
                            $scope.tree.dataSource.read();
                            $scope.addCatalogNameWindow.close();
                            csdToaster.info('添加成功！');
                        } else {
                            csdToaster.info('添加失败！');
                        }
                    });
                } else {
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '该目录名已存在！');
                }
            });
        }

        // 点击添加模板
        $scope.addTemplate = function () {
            if ($scope.selectedItem) {
                $scope.isEdit = true;
                $scope.isNew = true;
                $scope.flag = true;
            } else {
                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '请先选择一个目录！');
            }
        }

        // 点击保存
        $scope.save = function () {
            if (!$scope.template || !$scope.template.checkItemName || !$scope.template.modalityType || !$scope.template.bodyPart) {
                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '带星号的为必填项！');
            } else {
                if ($scope.isNew) {
                    $scope.templateName = '';
                    $scope.templateNameWindow.open();
                } else {
                    if ($scope.selectedItem.type === 0) {
                        $scope.template.parentID = $scope.selectedItem.parentID;
                        templateService.updatePublicTemplate($scope.template).success(function (res) {
                            if (res) {
                                $scope.templateNameWindow.close();
                                csdToaster.info('修改成功！');
                            } else {
                                csdToaster.info('修改失败！');
                            }
                            
                        });
                    } else {
                        templateService.updateTemplate($scope.template).success(function (res) {
                            if (res) {
                                $scope.templateNameWindow.close();
                                csdToaster.info('修改成功！');
                            } else {
                                csdToaster.info('修改失败！');
                            }   
                        });
                    }
                }
            }
        }

        // 保存时确定
        $scope.saveSure = function () {
            $scope.template.templateName = $scope.templateName;
            $scope.template.type = $scope.selectedItem.type;
            // 判断模板名称是否重复
            templateService.isTemplateExist($scope.template).success(function (res) {
                if (res) {
                    //公有模板
                    if ($scope.selectedItem.type === 0 || $scope.selectedItem.type === 2) {
                        if ($scope.isSaveAnother) {
                            $scope.template.parentID = $scope.selectedItem.parentID;
                            console.log($scope.template);
                            templateService.newPublicTemplate($scope.template).success(function (resGlobal) {
                                if (resGlobal) {
                                    $scope.templateNameWindow.close();
                                    $scope.tree.dataSource.read();
                                    $scope.selectedItem = null;
                                    $scope.flag = false;
                                    $scope.isEdit = false;
                                    $scope.isSaveAnother = false;
                                    csdToaster.info('另存成功！');
                                } else {
                                    csdToaster.info('另存失败！');
                                }
                            });
                        } else {
                            $scope.template.parentID = $scope.selectedItem.uniqueID;
                            templateService.newPublicTemplate($scope.template).success(function (resPrivate) {
                                if (resPrivate) {
                                    $scope.templateNameWindow.close();
                                    $scope.flag = false;
                                    $scope.tree.dataSource.read();
                                    $scope.selectedItem = null;
                                    $scope.isEdit = false;
                                    $scope.isSaveAnother = false;
                                    csdToaster.info('新增成功！');
                                } else {
                                    csdToaster.info('新增失败！');
                                }
                                
                            });
                        }
                    } else {
                        //非私有模板
                        if ($scope.isSaveAnother) {
                            templateService.newTemplate($scope.template).success(function (res) {
                                if (res) {
                                    $scope.templateNameWindow.close();
                                    $scope.tree.dataSource.read();
                                    $scope.selectedItem = null;
                                    $scope.flag = false;
                                    $scope.isEdit = false;
                                    $scope.isSaveAnother = false;
                                    csdToaster.info('另存成功！');
                                } else {
                                    csdToaster.info('另存失败！');
                                }
                            });
                        } else {
                            $scope.template.parentID = $scope.selectedItem.uniqueID;
                            templateService.newTemplate($scope.template).success(function (res) {
                                if (res) {
                                    $scope.templateNameWindow.close();
                                    $scope.flag = false;
                                    $scope.tree.dataSource.read();
                                    $scope.selectedItem = null;
                                    $scope.isEdit = false;
                                    $scope.isSaveAnother = false;
                                    csdToaster.info('新增成功！');
                                } else {
                                    csdToaster.info('新增失败！');
                                }
                            });
                        }
                    }
                } else {
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '模板名称重复！');
                }
            });
        }

        //另存为
        $scope.saveAnother = function () {
            $scope.isSaveAnother = true;
            if (!$scope.template || !$scope.template.checkItemName || !$scope.template.modalityType || !$scope.template.bodyPart) {
                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '带星号的为必填项！');
            } else {
                $scope.templateNameWindow.open();
            }
        };

        // 点击取消
        $scope.cancel = function () {
            openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), "确定取消？", function () {
                $scope.isEdit = false;
                $scope.flag = false;
                $scope.template = null;
            });
        }

        // 重新命名
        $scope.edit = function () {
            if ($scope.selectedItem) {
                if ($scope.selectedItem.uniqueID === "GlobalTemplate" || $scope.selectedItem.uniqueID === "Site1" || $scope.selectedItem.uniqueID === "UserTemplate") {
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '不能修改系统节点！');
                } else {
                    $scope.resetNameWindow.open();
                }
            } else {
                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '请先选择一个目录！');
            }

        }

        // 重新命名确定
        $scope.resetSure = function () {
            $scope.selectedItem.name = $scope.resetName;
            var rename = {
                itemName: $scope.resetName,
                uniqueID: $scope.selectedItem.uniqueID,
                type: $scope.selectedItem.type
            }
            templateService.isTemplateExist(rename).success(function (res) {
                if (res) {
                    templateService.updateTemplateDirec(rename).success(function (resUpdate) {
                        if (resUpdate) {
                            $scope.selectedItem.text = $scope.resetName;
                            $scope.selectedItem.itemName = $scope.resetName;
                            $scope.resetNameWindow.close();
                            csdToaster.info('修改成功！');
                        } else {
                            csdToaster.info('修改失败！');
                        }
                    });
                } else {
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '目录名称重复！');
                }
            })
        }

        //删除节点
        $scope.remove = function (item) {
            if ($scope.selectedItem) {
                if (item.uniqueID === "GlobalTemplate" || item.uniqueID === "Site1" || item.uniqueID === "UserTemplate") {
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '系统节点不可删除');
                } else {
                    openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '确定删除模板？', function () {
                        var array = item.parent();
                        var index = array.indexOf(item);
                        array.splice(index, 1);
                        templateService.deleteTemplate(item.uniqueID).success(function (res) {
                            if (res) {
                                $scope.selectedItem = null;
                                $scope.template = null;
                                csdToaster.info('删除成功！');
                            } else {
                                csdToaster.info('删除失败！');
                            }
                        });
                    });
                }
            } else {
                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '请先选择一个目录！');
            }
        };

        // 节点上移
        $scope.arrowUp = function () {
            if ($scope.selectedItem) {

                if ($scope.selectedItem.uniqueID) {
                    var treeview = $scope.tree;
                    var barDataItem = treeview.dataSource.get($scope.selectedItem.uniqueID);
                    if (barDataItem) {
                        treeview.select(treeview.findByUid(barDataItem.uid));//选中节点不变  
                    }
                }
                templateService.nodeUp($scope.selectedItem.uniqueID).success(function () {
                    $scope.tree.dataSource.read();
                });
            } else {
                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '请先选择一个目录！');
            }
        }

        //节点下移
        $scope.arrowDown = function () {
            if ($scope.selectedItem) {
                templateService.nodeDown($scope.selectedItem.uniqueID).success(function () {
                    $scope.tree.dataSource.read();
                });
            } else {
                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '请先选择一个目录！');
            }
        }
        var currentSearchCriteria = {
            pagination: {},
            code: ''
        };
        var paginationContext = {
            hasNextPage: false,
            pageIndex: 1,
            pageSize: 10,

            hasNoPreviousPage: function () {
                return this.pageIndex === 1;
            },
            hasNoNextPage: function () {
                return !this.hasNextPage;
            },
            selectPreviousPage: function () {
                $scope.selectStatus = 3;
                if (this.pageIndex <= 1) {
                    return;
                }
                this.pageIndex = this.pageIndex - 1;
                currentSearchCriteria.pagination.pageIndex = this.pageIndex;
                refresh();
            },
            selectNextPage: function () {
                $scope.selectStatus = 3;
                if (!this.hasNextPage && $scope.template.acrCode.length > 2) {
                    return;
                }
                this.pageIndex = this.pageIndex + 1;
                currentSearchCriteria.pagination.pageIndex = this.pageIndex;
                refresh();
            },
            reset: function () {
                this.pageIndex = 1;
                this.hasNextPage = false;
            }
        };
        var idCodeFcous = function () {
            document.getElementById('icdCode').focus();
            $scope.selectStatus = 1;
        };
        var selectCode = function (code) {
            $scope.codeSelect = false;
            $scope.template.acrCode = code.id.trim();
        };
        var closeSelectCodes = function () {

            if ($scope.selectStatus != 3) {
                $scope.codeSelect = false;
            }
        };

        var codeChange = function () {
            if (!selectCode) {
                return;
            }
            paginationContext.pageIndex = 1;
            //获取分页数据 codes
            refresh();
        };

        var refresh = function () {

            currentSearchCriteria.pagination.pageIndex = paginationContext.pageIndex;
            currentSearchCriteria.pagination.pageSize = paginationContext.pageSize;
            if (currentSearchCriteria.pagination && currentSearchCriteria.pagination.pageIndex <= 0) {
                return;
            }
            if ($scope.template.acrCode.length < 2) {
                return;
            }
            currentSearchCriteria.code = $scope.template.acrCode;
            configurationService.selectSearch(currentSearchCriteria).success(function (data) {
                $scope.codes = data.codes;
                paginationContext.hasNextPage = data.pagination.hasNextPage;
                $scope.codeSelect = true;
            });
        };

        (function init() {

            $scope.acrCode = null;
            $scope.selectStatus = 1;
            $scope.codes = null;
            $scope.template = null;
            $scope.selectedItem = null;
            $scope.isEdit = false;
            $scope.isSaveAnother = false;
            $scope.isNew = false;
            $scope.positiveList = getPositiveList();
            $scope.codeSelect = false;
            $scope.selectCode = selectCode;//疾病列表是否显示
            $scope.codeChange = codeChange;//疾病编号输入事件
            $scope.paginationContext = paginationContext//分页
            $scope.refresh = refresh;//疾病查询
            $scope.closeSelectCodes = closeSelectCodes;
            $scope.idCodeFcous = idCodeFcous;

        })();
    }]);

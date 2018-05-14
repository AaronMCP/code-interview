configurationModule.controller('CheckCodeCtrl', ['$scope', 'risInvalidFocus', '$rootScope', 'openDialog', '$state', '$log', 'configurationService', '$timeout',
    'constants', 'csdToaster', 'loginContext', 'dictionaryManager', 'enums', '$translate', 'sortArr',
    function ($scope, risInvalidFocus, $rootScope, openDialog, $state, $log, configurationService, $timeout, constants, csdToaster, loginContext, dictionaryManager, enums, $translate, sortArr) {
        //部分分类
        $scope.bodyCategoryList = null;
        //设备类型
        $scope.modalityTypeList = null;
        //默认设备
        $scope.defaultModalityList = null;
        //检查部位
        $scope.bodyPartList = null;
        //胶片规格
        $scope.filmspecList = null;
        //持续检查
        $scope.durationList = null;
        //造影剂
        $scope.contrastNameList = null;
        //站点
        $scope.siteList = null;
        //检查项目
        $scope.checkingItemList = null;
        //检查系统
        $scope.subexamSystemList = null;
        //测试
        $scope.names = [{ Name: 'john', Country: 'jack' }];
        var indexList = ['procedureCode', 'bodyCategory', 'modalityType', 'checkingItem', 'bodyPart', 'duration'];
        // 检查代码全部字段信息
        $scope.procedureInfo = null;
        $scope.searchCriteria = {
            site: '',
            modalityType: '',
            bodyCategory: '',
            bodyPart: '',
            checkingItem: ''
        };
        //默认设备+检查部位筛选
        $scope.getDefaultModality = function () {
            configurationService.getModality($scope.procedureInfo.modalityType).success(function (result) {
                $scope.defaultModalityList = result;
            });
            $scope.getBodySystemMap(false);
            $scope.getFrequency();
            $scope.bodyPartList = _.where($scope.bodyPartListAll, { modalityType: $scope.procedureInfo.modalityType });
        };
        //获取频率
        $scope.getFrequency = function () {
            if ($scope.procedureInfo.bodyCategory !== undefined && $scope.procedureInfo.modalityType !== undefined && $scope.procedureInfo.checkingItem !== undefined && $scope.procedureInfo.bodyPart !== undefined) {
                configurationService.getProcedureFrequency($scope.procedureInfo).success(function (result) {
                    $scope.procedureInfo.frequency = result.frequency;
                    $scope.procedureInfo.bodypartFrequency = result.bodypartFrequency;
                    $scope.procedureInfo.checkingItemFrequency = result.checkingItemFrequency;
                    $scope.procedureInfo.duration = result.duration + '';
                });
            }
        };
        //获取检查系统
        $scope.getBodySystemMap = function (flag) {
            if (flag) {
                $scope.getFrequency();
            }
            if ($scope.procedureInfo.modalityType !== undefined && $scope.procedureInfo.bodyPart !== undefined) {
                var selectObj = {
                    modalityType: $scope.procedureInfo.modalityType,
                    bodyPart: $scope.procedureInfo.bodyPart
                }
                configurationService.getBodySystemMap(selectObj).success(function (result) {
                    if (result === null) {
                        $scope.procedureInfo.examSystem = undefined;
                    } else {
                        $scope.procedureInfo.examSystem = result.examSystem;
                    }
                });
            }
        };
        //搜索
        $scope.search = function () {
            $scope.searchFlag = true;
            $scope.isButtonShow = true;
            $scope.procedureInfo = null;
            $rootScope.$broadcast('event:checkCodeSearch', {
                site: $scope.searchCriteria.site,
                modalityType: $scope.searchCriteria.modalityType,
                bodyCategory: $scope.searchCriteria.bodyCategory,
                bodyPart: $scope.searchCriteria.bodyPart,
                checkingItem: $scope.searchCriteria.checkingItem
            });
        };
        $scope.$on('event:checkCodeSearch', function (e, args) {
            $scope.searchCriteria.site = args.site;
            $scope.searchCriteria.modalityType = args.modalityType;
            $scope.searchCriteria.bodyCategory = args.bodyCategory;
            $scope.searchCriteria.bodyPart = args.bodyPart;
            $scope.searchCriteria.checkingItem = args.checkingItem;
            $scope.searchCriteriaMirror = angular.copy($scope.searchCriteria);
            $scope.checkcodeGrid.dataSource.page(1);
        });
        $scope.isButtonShow = null;
        $scope.isAddSuccess = null;
        $scope.isSmallModalShow = null;
        $scope.isAdd = null;
        //small modal
        $scope.showBodyCategory = null;
        $scope.showCheckingItem = null;
        $scope.showBodyPart = null;
        $scope.closeSmallModal = function () {
            $scope.isSmallModalShow = false;
            $scope.isShowSub1ErrorMsg = false;
            $scope.isShowSub2ErrorMsg = false;
            $scope.isShowSub3ErrorMsg = false;
        };
        $scope.showSmallModal = function () {
            $scope.isSmallModalShow = true;
        }
        //small modal 显示什么内容
        $scope.showBodyCategoryActive = function () {
            $scope.showBodyCategory = true;
            $scope.showCheckingItem = false;
            $scope.showBodyPart = false;
            $scope.showSmallModal();
        }
        $scope.showCheckingItemActive = function () {
            $scope.showBodyCategory = false;
            $scope.showCheckingItem = true;
            $scope.showBodyPart = false;
            $scope.showSmallModal();
        }
        $scope.showBodyPartActive = function () {
            $scope.showBodyCategory = false;
            $scope.showCheckingItem = false;
            $scope.showBodyPart = true;
            $scope.showSmallModal();
            //获取所有检查系统
            configurationService.getBodySystemMapsText(loginContext.site).success(function (result) {
                $scope.subexamSystemList = result;
            });
        }
        // 添加
        $scope.addBodyCategory = function () {
            if (!$scope.bodyCategoryForm.$valid) {
                $scope.isShowSub1ErrorMsg = true;
                return;
            }
            for (var obj in $scope.bodyCategoryList) {
                if ($scope.bodyCategoryList[obj].text === $scope.subbodyCategory) {
                    csdToaster.info($scope.subbodyCategory + '已存在');
                    return;
                }
            }
            var obj = { text: $scope.subbodyCategory }
            $scope.bodyCategoryList.push(obj);
            $scope.closeSmallModal();
        };
        $scope.addCheckingItem = function () {
            if (!$scope.checkingItemForm.$valid) {
                $scope.isShowSub2ErrorMsg = true;
                return;
            }
            for (var obj in $scope.checkingItemList) {
                if ($scope.checkingItemList[obj].text === $scope.subcheckingItem) {
                    csdToaster.info($scope.subcheckingItem + '已存在');
                    return;
                }
            }
            var obj = { text: $scope.subcheckingItem }
            $scope.checkingItemList.push(obj);
            $scope.closeSmallModal();
        };
        $scope.addBodyPart = function () {
            if (!$scope.bodyPartForm.$valid) {
                $scope.isShowSub3ErrorMsg = true;
                return;
            } else {
                var newBodyPart = {
                    modalityType: $scope.submodalityType,
                    bodyPart: $scope.subbodyPart,
                    examSystem: $scope.subexamSystem
                };
                configurationService.isBodyPartExist(newBodyPart).success(function (result) {
                    if (result) {
                        configurationService.addBodySystemMap(newBodyPart).success(function (result) {
                            if (result) {
                                var addObj = newBodyPart;
                                $scope.bodyPartList.push(addObj);
                                $scope.bodyPartListAll.push(addObj);
                                console.log($scope.bodyPartList);
                                csdToaster.info($scope.subbodyPart + '添加成功！');
                                $scope.closeSmallModal();
                            } else {
                                csdToaster.info($scope.subbodyPart + '添加成功！');
                            }
                        });
                    } else {
                        csdToaster.info($scope.subbodyPart + '已存在！');
                    }
                });
            }
        };
        $scope.smallAddSub = function () {
            if ($scope.showBodyCategory) {
                $scope.addBodyCategory();
            } else if ($scope.showCheckingItem) {
                $scope.addCheckingItem();
            } else {
                $scope.addBodyPart();
            }
        };
        //big modal
        $scope.isShowHysBigModal = null;
        //修改频率开始
        $scope.allInfoFreList = null;
        $scope.modalityTypeArr = null;
        /**
        * 对数据进行分组
        */
        $scope.doGroup = function (arr, attr, attr2) {
            var map = {};
            var newArr = [];
            var pushObj = {};
            for (var i = 0; i < arr.length; i++) {
                var obj = arr[i];
                if (!map[obj[attr]]) {
                    pushObj = {
                        data: [obj]
                    };
                    var attrObj = {};
                    attrObj[attr] = obj[attr];
                    attrObj[attr2] = obj[attr2];
                    pushObj[attr] = attrObj;
                    newArr[obj[attr]] = pushObj;
                    map[obj[attr]] = [obj];
                } else {
                    for (var ai in newArr) {
                        var newObj = newArr[ai];
                        if (newObj[attr][attr] === obj[attr]) {
                            newObj.data.push(obj);
                            break;
                        }
                    }
                }
            }
            return newArr;
        };
        //选中
        $scope.selectModalityTypeRow = function (modalityType) {
            $scope.selectModalityType.selected = false;
            modalityType.selected = true;
            $scope.selectModalityType = modalityType;
            $scope.getbodyCategoryText($scope.selectModalityType.modalityType);
            $scope.getbodyPartText($scope.selectBodyCategory.bodyCategory);
            $scope.getcheckingItemText($scope.selectBodyPart.bodyPart);
        };
        $scope.selectbodyCategoryRow = function (bodyCategory) {
            $scope.selectBodyCategory.selected = false;
            bodyCategory.selected = true;
            $scope.selectBodyCategory = bodyCategory;
            $scope.getbodyPartText($scope.selectBodyCategory.bodyCategory);
            $scope.getcheckingItemText($scope.selectBodyPart.bodyPart);
        };
        $scope.selectbodyPartRow = function (bodyPart) {
            $scope.selectBodyPart.selected = false;
            bodyPart.selected = true;
            $scope.selectBodyPart = bodyPart;
            $scope.getcheckingItemText($scope.selectBodyPart.bodyPart);
        };
        $scope.selectCheckingItemRow = function (checkingItem) {
            $scope.selectCheckingItem.selected = false;
            checkingItem.selected = true;
            $scope.selectCheckingItem = checkingItem;
        }
        //input修改值
        $scope.putValue = [];
        $scope.onChangeFrequency = function () {
            var obj = {
                modalityType: null,
                bodyCategory: null,
                frequency: null,
                bodyPart: null,
                bodypartFrequency: null,
                checkingItem: null,
                checkingItemFrequency: null
            };
            obj.modalityType = $scope.selectModalityType.modalityType;
            obj.bodyCategory = $scope.selectBodyCategory.bodyCategory;
            obj.frequency = $scope.selectBodyCategory.frequency;
            if ($scope.putValue.length === 0) {
                $scope.putValue.push(obj);
            } else {
                var flag;
                for (var i = 0; i < $scope.putValue.length; i++) {
                    flag = i;
                    if (obj.bodyCategory === $scope.putValue[i].bodyCategory && obj.modalityType === $scope.putValue[i].modalityType) {
                        break;
                    }
                }
                if (flag === $scope.putValue.length - 1) {
                    $scope.putValue.push(obj);
                } else {
                    $scope.putValue[flag] = obj;
                }

            }
        };
        $scope.onChangeBodypartFrequency = function () {
            var obj = {
                modalityType: null,
                bodyCategory: null,
                frequency: null,
                bodyPart: null,
                bodypartFrequency: null,
                checkingItem: null,
                checkingItemFrequency: null
            };
            obj.modalityType = $scope.selectModalityType.modalityType;
            obj.bodyPart = $scope.selectBodyPart.bodyPart;
            obj.bodyCategory = $scope.selectBodyCategory.bodyCategory;
            obj.bodypartFrequency = $scope.selectBodyPart.bodypartFrequency;
            if ($scope.putValue.length === 0) {
                $scope.putValue.push(obj);
            } else {
                var flag;
                for (var i = 0; i < $scope.putValue.length; i++) {
                    flag = i;
                    if (obj.bodyPart === $scope.putValue[i].bodyPart && obj.bodyCategory === $scope.putValue[i].bodyCategory && obj.modalityType === $scope.putValue[i].modalityType) {

                        break;
                    }
                }
                if (flag === $scope.putValue.length - 1) {
                    $scope.putValue.push(obj);
                } else {
                    $scope.putValue[flag] = obj;
                }
            }
        };
        $scope.onChangeCheckingItemFrequency = function () {
            var obj = {
                modalityType: null,
                bodyCategory: null,
                frequency: null,
                bodyPart: null,
                bodypartFrequency: null,
                checkingItem: null,
                checkingItemFrequency: null
            };
            obj.modalityType = $scope.selectModalityType.modalityType;
            obj.bodyCategory = $scope.selectBodyCategory.bodyCategory;
            obj.bodyPart = $scope.selectBodyPart.bodyPart;
            obj.checkingItem = $scope.selectCheckingItem.checkingItem;
            obj.checkingItemFrequency = $scope.selectCheckingItem.checkingItemFrequency;
            if ($scope.putValue.length === 0) {
                $scope.putValue.push(obj);
            } else {
                var flag;
                for (var i = 0; i < $scope.putValue.length; i++) {
                    flag = i;
                    if (obj.checkingItem === $scope.putValue[i].checkingItem && obj.modalityType === $scope.putValue[i].modalityType && obj.bodyCategory === $scope.putValue[i].bodyCategory && obj.bodyPart === $scope.putValue[i].bodyPart) {
                        break;
                    }
                }
                if (flag === $scope.putValue.length - 1) {
                    $scope.putValue.push(obj);
                } else {
                    $scope.putValue[flag] = obj;
                }
            }
        };
        $scope.changeFrequency = function () {
            if ($scope.putValue.length === 0) {
                csdToaster.info('没有修改内容！');
            } else {
                configurationService.updateFrequency($scope.putValue).success(function (result) {
                    if (result) {
                        csdToaster.info('保存成功！');
                        $scope.modalityTypeText = [];
                        $scope.bodyPartText = [];
                        $scope.checkingItemText = [];
                        $scope.closeEditFre();
                    } else {
                        csdToaster.info('保存失败！');
                    }
                });
            }
        };
        //获取数组
        $scope.getmodalityTypeText = function (arr) {
            $scope.modalityTypeArr = $scope.doGroup(arr, 'modalityType', 'bodyCategory');
            $scope.modalityTypeText = [];
            var keyArr = [];
            for (var attr in $scope.modalityTypeArr) {
                if (attr !== 'null' && attr !== 'sortBy') {
                    $scope.modalityTypeText.push($scope.modalityTypeArr[attr].modalityType);
                }
            }
            $scope.selectModalityType = $scope.modalityTypeText[0];
            $scope.selectModalityType.selected = true;
        };
        $scope.getbodyCategoryText = function (name) {
            $scope.bodyCategoryArr = $scope.doGroup($scope.modalityTypeArr[name].data, 'bodyCategory', 'frequency');
            $scope.bodyCategoryText = [];
            for (var attr in $scope.bodyCategoryArr) {
                if (attr !== 'sortBy') {
                    if ($scope.bodyCategoryArr[attr].bodyCategory.frequency === null) {
                        $scope.bodyCategoryArr[attr].bodyCategory.frequency = 0;
                    }
                    $scope.bodyCategoryText.push($scope.bodyCategoryArr[attr].bodyCategory);
                }
            };
            $scope.bodyCategoryText.sort(function (a, b) { return b.frequency - a.frequency });
            $scope.selectBodyCategory = $scope.bodyCategoryText[0];
            $scope.selectBodyCategory.selected = true;
        };
        $scope.getbodyPartText = function (name) {
            $scope.bodyPartArr = $scope.doGroup($scope.bodyCategoryArr[name].data, 'bodyPart', 'bodypartFrequency');
            $scope.bodyPartText = [];
            for (var attr in $scope.bodyPartArr) {
                if (attr !== 'sortBy') {
                    if ($scope.bodyPartArr[attr].bodyPart.bodypartFrequency === null) {
                        $scope.bodyPartArr[attr].bodyPart.bodypartFrequency = 0;
                    }
                    $scope.bodyPartText.push($scope.bodyPartArr[attr].bodyPart);
                }
            }
            $scope.bodyPartText.sort(function (a, b) { return b.bodypartFrequency - a.bodypartFrequency });
            $scope.selectBodyPart = $scope.bodyPartText[0];
            $scope.selectBodyPart.selected = true;
        };
        $scope.getcheckingItemText = function (name) {
            $scope.checkingItemArr = $scope.doGroup($scope.bodyPartArr[name].data, 'checkingItem', 'checkingItemFrequency');
            $scope.checkingItemText = [];
            for (var attr in $scope.checkingItemArr) {
                if (attr !== 'sortBy') {
                    if ($scope.checkingItemArr[attr].checkingItem.checkingItemFrequency === null) {
                        $scope.checkingItemArr[attr].checkingItem.checkingItemFrequency = 0;
                    }
                    $scope.checkingItemText.push($scope.checkingItemArr[attr].checkingItem);
                }
            };
            //sortArr.sortArray($scope.checkingItemText, 'desc', 'checkingItemFrequency');
            $scope.checkingItemText.sort(function (a, b) { return b.checkingItemFrequency - a.checkingItemFrequency });
            $scope.selectCheckingItem = $scope.checkingItemText[0];
            $scope.selectCheckingItem.selected = true;
        };
        $scope.ediFre = function () {
            $scope.isShowHysBigModal = true;
            $scope.putValue = [];
            configurationService.getAllProcedureCode().success(function (result) {
                var firstArray = result;
                $scope.getmodalityTypeText(firstArray);
                $scope.getbodyCategoryText($scope.selectModalityType.modalityType);
                $scope.getbodyPartText($scope.selectBodyCategory.bodyCategory);
                $scope.getcheckingItemText($scope.selectBodyPart.bodyPart);
            });
        };
        $scope.closeEditFre = function () {
            $scope.isShowHysBigModal = false;
        };
        //修改频率结束
        $scope.add = function () {
            $scope.isAdd = true;
            $scope.closeSmallModal();
            $scope.procedureInfo = null;
        };
        $scope.showEdit = function () {
            configurationService.getModality($scope.currentItem.modalityType).success(function (result) {
                $scope.defaultModalityList = result;
                $scope.procedureInfo = angular.copy($scope.currentItem);
                $scope.getBodySystemMap(false);
                $scope.getFrequency();
                $scope.isAdd = false;
                $scope.closeSmallModal();
            });
        }
        $scope.quilt = function () {
            $scope.isShowErrorMsg = false;
            $scope.isAddSuccess = false;
        };
        $scope.clear = function () {
            $scope.procedureInfo = null;
        };
        //修改
        $scope.edit = function () {
            configurationService.editCheckCodeRow($scope.procedureInfo).success(function (res) {
                if (res) {
                    for (var attr in $scope.currentItem) {
                        $scope.currentItem[attr] = $scope.procedureInfo[attr];
                    };
                    csdToaster.info('修改成功！');
                    $('#checkCodeModal').modal('hide');
                } else {
                    csdToaster.info('修改失败！');
                }

            });
        };
        //添加
        $scope.saveCheckCode = function (form, successCallBack) {
            if (!form.$valid) {
                $scope.isShowErrorMsg = true;
                //risInvalidFocus(form, indexList);
                //return; 
            } else {
                if ($scope.isAdd) {
                    for (var obj in $scope.data.data) {
                        if ($scope.data.data[obj].procedureCode === $scope.procedureInfo.procedureCode) {
                            csdToaster.info('检查代码不能重复！');
                            return;
                        }
                    }
                    $scope.procedureInfo.site = $scope.searchCriteria.site;
                    configurationService.addCheckCodeRow($scope.procedureInfo).success(function (res) {
                        if (res) {
                            $('#checkCodeModal').modal('hide');
                            $scope.checkcodeGrid.dataSource.read();
                            $scope.checkcodeGrid.refresh();
                            csdToaster.info('添加成功！');
                        } else {
                            csdToaster.info('添加失败！');
                        }

                    });
                } else {
                    $scope.edit();
                }
            }
        };
        //删除
        $scope.deleteCheckRow = function () {
            $scope.closeSmallModal();
            openDialog.openIconDialogOkCancel(
                     openDialog.NotifyMessageType,
                     $translate.instant('Alert'),
                     '确定删除本条数据？', function () {
                         configurationService.deletCheckCodeRow($scope.currentItem).success(function () {
                             $scope.checkcodeGrid.dataSource.read();
                             $scope.checkcodeGrid.refresh();
                             $scope.isButtonShow = true;
                         });
                     });
        };

        (function init() {

            $scope.selectModalityType = null;
            $scope.selectBodyCategory = null;
            $scope.selectBodyPart = null;
            $scope.selectCheckingItem = null;
            $scope.isAdd = false;
            $scope.isSmallModalShow = false;
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
                $scope.bodyPartListAll = result;
                $scope.bodyPartList = result;
            });
            //设备类型
            configurationService.getModalityTypes().success(function (result) {
                $scope.modalityTypeList = result;
            });
            //胶片规格
            dictionaryManager.getDictionaries(enums.dictionaryTag.filmspecList).then(function (data) {
                $scope.filmspecList = data;
            });
            //造影剂
            dictionaryManager.getDictionaries(enums.dictionaryTag.contrastName).then(function (data) {
                $scope.contrastNameList = data;
            });
            //configurationService.getSiteDictionaries([4]).success(function (result) {
            //    $scope.filmspecList = result;
            //});
            //持续检查
            dictionaryManager.getDictionaries(enums.dictionaryTag.durationList).then(function (data) {
                $scope.durationList = data;
            });
            //configurationService.getSiteDictionaries([15]).success(function (result) {
            //    $scope.durationList = result;
            //});
            //站点
            configurationService.getAllSite().success(function (result) {
                var sites = _.map(result, function (item) {
                    return item.siteName;
                });
                //sortArr.sortArray(sites, 'asc');
                sites.sort(function (a, b) { return a > b });
                sites.unshift("");
                $scope.siteList = sites;
            });
            //检查项目
            configurationService.getCheckingItemText().success(function (result) {
                $scope.checkingItemList = result;
            });
            $scope.isButtonShow = true;
            $scope.isShowErrorMsg = false;
            $scope.checkcodeGridOption = {
                dataSource: new kendo.data.DataSource({
                    schema: {
                        data: 'data',
                        total: 'total',
                        aggregates: "aggregates",
                        model: {
                            fields: {
                                examineTime: { type: 'date' }
                            }
                        }
                    },
                    transport: {
                        read: function (options) {
                            var flag = false;
                            if ($scope.searchCriteriaMirror) {
                                flag = true;
                                for (var attr in $scope.searchOption) {
                                    if ($scope.searchOption[attr] !== $scope.searchCriteriaMirror[attr]) {
                                        flag = false;
                                        break;
                                    }
                                }
                            }
                            if ($scope.searchFlag && flag) {
                                var filter = {
                                    logic: "and",
                                    filters: []
                                };
                                for (var attr in $scope.searchCriteria) {
                                    filter.filters.push({ field: attr, operator: "contains", value: $scope.searchCriteria[attr] });
                                }
                                $scope.filter = filter;
                            }
                            options.data.filter = $scope.filter;
                            configurationService.getCheckCodeList(options.data).success(function (result) {
                                $scope.isButtonShow = true;
                                $scope.procedureInfo = null;
                                $scope.data = result;
                                options.success(result);
                            });
                        },


                    },
                    error: function (e) {
                        csdToaster.error('请求数据失败！');
                    },
                    pageSize: constants.pageSize,
                    sort: [{ field: "procedureCode", dir: "asc" }],
                    serverPaging: true,
                    serverFiltering: true,
                    serverSorting: true,
                }),
                pageable: {
                    refresh: true,
                    buttonCount: 5,
                    input: true
                },
                columns: [
                    { field: 'procedureCode', title: "检查代码" },
                    {
                        field: 'description', title: "检查"
                    },
                    { field: 'englishDescription', title: "英文描述" },
                    { field: 'modalityType', title: "设备类型" },
                    { field: 'bodyPart', title: "检查部位" },
                    { field: 'checkingItem', title: "检查项目" },
                    { field: 'charge', title: "费用" },
                    { field: 'preparation', title: "前期准备" },
                    { field: 'frequency', title: "部位分类频率" },
                    { field: 'bodyCategory', title: "部位分类" },
                    { field: 'effective', title: "是否有效", template: '{{dataItem.effective === 1 ? "是": "否"}}' },
                ],
                selectable: "row",
                sortable: {
                    allowUnsort: false
                },
                change: function (e) {
                    var selectedRows = this.select();
                    var dataItem = this.dataItem(selectedRows[0]);
                    $timeout(function () {
                        $scope.isButtonShow = false;
                    });
                    $scope.currentItem = dataItem;
                }
            };
        })();
    }]);

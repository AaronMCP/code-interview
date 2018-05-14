configurationModule.controller('DictionaryeSystemCtrl', ['$scope', '$rootScope', 'configurationService', 'csdToaster', 'openDialog', '$translate',
function ($scope, $rootScope, configurationService, csdToaster, openDialog, $translate) {

    //站点
    configurationService.getAllSite().success(function (result) {
        var sites = _.map(result, function (item) {
            return item.siteName;
        });
        //sortArr.sortArray(sites, 'asc');
        sites.sort(function (a, b) { return a > b });
        $scope.sites = sites;
    });
    //点击字典
    $scope.clickItem = function (item) {
        if ($scope.selectedItem) {
            $scope.selectedItem.selected = false;
        }
        $scope.currentSite = '';
        item.selected = true;
        $scope.selectedItem = item;
    }
    //点击字典value
    $scope.clickValueItem = function (item) {
        if ($scope.selectedValueItem) {
            $scope.selectedValueItem.selected = false;
        }
        item.selected = true;
        $scope.selectedValueItem = item;
        $scope.dicValueItem.tag = item.tag;
        $scope.dicValueItem.value = item.value;
        $scope.dicValueItem.shortcutCode = item.shortcutCode;
        $scope.dicValueItem.mapTag = item.mapTag;
    }
    //我的结果处理
    var eachResult = function (data) {
        if (!data) return;
        $scope.dicList = data;
        if ($scope.dicList && $scope.dicList.length > 0) {
            angular.forEach($scope.dicList, function (item, index) {
                item.options = [''];
                item.isChange = false;//标识默认值改变
                if (item.values) {
                    angular.forEach(item.values, function (dicvalue) {
                        if (dicvalue.value) {
                            item.options.push(dicvalue.value);
                        }
                        //默认值
                        if (dicvalue.isDefault) {
                            item.value = dicvalue.value;
                        }
                    });
                }
                //清空选项
                if (!item.value) {
                    item.value = '';
                }
            });
        }
        return data;
    }
    var tags = [116, 117, 123];
    //显示不同的快捷码lable
    var isShowText = function (tag) {
        if (!tag)
            return false;
        for (var i = 0; i < tags.length; i++) {
            if (tags[i] == $scope.selectedItem.tag) {
                return true;
            }
        }
        return false;
    }
    //刷新
    $scope.refresh = function () {
        getDicList();
    }
    $scope.optionChange = function () {
        $scope.selectedItem.isChange = true;
    }
    //修改
    $scope.updateSystem = function () {

        if (!$scope.selectedItem) {
            return;
        }
        inItalueItem();
        $scope.selectedValueItem = null;
        $scope.dicValueList = null;
        $scope.currentSite = '';
        //显示的链接
        $scope.showText = isShowText($scope.selectedItem.tag);
        getDicValues($scope.selectedItem.tag, null)
        $scope.updateSystemWindow.open();
    }
    //查询  
    var getDicList = function () {
        var site = null;
        configurationService.getSysDictionaries(site).success(function (result) {
            eachResult(result);
        });
    }
    //字典保存
    $scope.saveDics = function () {
        //form验证
        if ($scope.dicList) {
            configurationService.SaveDictionaries($scope.dicList).success(function (result) {
                if (result) {//保存成功
                    getDicList();
                    csdToaster.info('保存成功！');
                }
            });
        }

    }
    //字典值 site选择
    $scope.siteChange = function () {
        $scope.selectedValueItem = null;
        getDicValues($scope.selectedItem.tag, $scope.currentSite);
        inItalueItem();
    }
    //字典值
    var getDicValues = function (tag, site) {
        configurationService.getDictionariesList(tag, site).success(function (result) {
            if (result[0]) {//获取当前被修改字典的map 
                $scope.dicValueList = result
                $scope.mapTag = result[0].mapTag;
            }
            else {
                $scope.dicValueList = null;
            }

        });
    }
    var validate = function (str) {
        var myReg = /^[^\'\",|]+$/;
        if (myReg.test(str)) return false;
        return true;
    }
    //添加字典值
    $scope.addDicValueSure = function () {
        if (!$scope.dicValueItem.value) {
            return;
        }
        if (validate($scope.dicValueItem.value)) {
            openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant("字典描述不能包含特殊字符 {,'" + '"' + '|}'), function () {
            });
            return;
        }
        if ($scope.dicValueItem.shortcutCode) {
            if (validate($scope.dicValueItem.shortcutCode)) {
                openDialog.openIconDialogOkFun(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant("快捷方式不能包含特殊字符 {,'" + '"' + '|}'), function () {
                });
                return;
            }
        }

        var dicv = $scope.dicValueItem;
        dicv.site = $scope.currentSite;
        dicv.tag = $scope.selectedItem.tag;
        configurationService.SaveDictionaryValue(dicv).success(function (result) {
            if (result > 0) {
                csdToaster.info('添加成功！');
            } else if (result == -1) {
                csdToaster.info('描述重复！');
            } else {
                csdToaster.info('添加失败！');
            }
            getDicValues($scope.selectedItem.tag, $scope.currentSite);
        });
    }
    //上移动
    $scope.upDicValue = function () {
        if (!$scope.selectedValueItem || !$scope.dicValueList) {
            return;
        }
        var upStairsValue = null;
        var toUpIndex
        angular.forEach($scope.dicValueList, function (dicvalue, index) {
            if (dicvalue.value == $scope.selectedValueItem.value) {
                toUpIndex = index;
            }
        });
        if (toUpIndex > 0) {
            upStairsValue = $scope.dicValueList[toUpIndex - 1]
            $scope.dicValueList[toUpIndex - 1] = $scope.selectedValueItem;
            $scope.dicValueList[toUpIndex] = upStairsValue;

        } else {
            return;
        }
    }

    //下移动
    $scope.downDicValue = function () {
        if (!$scope.selectedValueItem || !$scope.dicValueList) {
            return;
        }
        var downStairsValue = null;
        var toDownIndex
        angular.forEach($scope.dicValueList, function (dicvalue, index) {
            if (dicvalue.value == $scope.selectedValueItem.value) {
                toDownIndex = index;
            }
        });
        if (toDownIndex < $scope.dicValueList.length - 1) {
            downStairsValue = $scope.dicValueList[toDownIndex + 1]
            $scope.dicValueList[toDownIndex + 1] = $scope.selectedValueItem;
            $scope.dicValueList[toDownIndex] = downStairsValue;

        } else {
            return;
        }
    }


    //修改 字典值
    $scope.updateDicValue = function () {
        if (!$scope.selectedValueItem || !$scope.dicValueList) {
            return;
        }
        openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant("此操作会影响系统其他功能！请谨慎操作。"), function () {
            var dicv = angular.copy($scope.selectedValueItem);
            dicv.value = $scope.dicValueItem.value;
            dicv.shortcutCode = $scope.dicValueItem.shortcutCode;
            dicv.values = $scope.dicValueList;
            configurationService.UpdateDictionaryValue(dicv).success(function (result) {
                if (result > 0) {
                    csdToaster.info('修改成功！');
                } else if (result == -1) {
                    csdToaster.info('描述重复！');
                } else {
                    csdToaster.info('修改失败！');
                }
                getDicValues($scope.selectedItem.tag, $scope.currentSite);
            });
        });

    }


    //删除字典值
    $scope.delDicValue = function () {
        openDialog.openIconDialogOkCancel(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant("此操作会影响系统其他功能！请谨慎操作。"), function () {
            configurationService.DelDictionaryValue($scope.selectedValueItem).success(function (result) {
                if (result > 0) {
                    csdToaster.info('删除成功！');
                }
                getDicValues($scope.selectedItem.tag, $scope.currentSite);
            });
        });


    }

    //初始化字典值对象
    var inItalueItem = function () {
        $scope.dicValueItem = {
            tag: null,
            value: null,
            shortcutCode: null,
            site: null,
            mapTag: null
        };
    }
    ; +(function init() {
        $scope.dicList = null;
        $scope.selectedItem = null;
        $scope.selectedValueItem = null;
        $scope.currentSite = '';
        $scope.mapTag = null;
        $scope.dicValueList = null;
        $scope.showText = false;
        inItalueItem();
        getDicList();

        // 默认选中第一行
        if ($scope.dicList) {
            $scope.selectedItem = $scope.dicList[0];
            $scope.selectedItem.selected = true;
        }
    })()
}])
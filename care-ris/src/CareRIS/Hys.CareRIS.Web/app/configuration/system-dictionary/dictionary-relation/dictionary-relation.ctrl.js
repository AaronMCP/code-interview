configurationModule.controller('DictionaryeRelationCtrl', ['$scope', '$rootScope', 'configurationService', 'csdToaster', 'openDialog', '$translate',
    function ($scope, $rootScope, configurationService, csdToaster, openDialog, $translate) {


        //我的结果处理
        var handleResult = function (data) {
            if (!data) return;
            $scope.dicRelationList = data;
            if ($scope.dicRelationList && $scope.dicRelationList.length > 0) {
                angular.forEach($scope.dicRelationList, function (item, index) {
                    item.options = [''];
                    item.isChange = false;//标识默认值改变
                    if (item.mapDicValues) {
                        angular.forEach(item.mapDicValues, function (dicvalue) {
                            if (dicvalue.value) {
                                if (item.mapValue == dicvalue.value) {
                                    $scope.mapDicText = dicvalue.text;
                                }
                                item.options.push(dicvalue.value);
                            }
                        });
                    }
                    //清空选项
                    if (!item.mapValue) {
                        item.mapValue = '';
                    }
                });
            }
            return data;
        }
        //字典关联查询 凹 根据tag,mapTag分组没做 
        var getDicRelationList = function () {
            configurationService.GetDicMappings($scope.mapSite).success(function (result) {
                handleResult(result)
                $scope.selectedMapItem = null;
            });
        }
        //点击一行
        $scope.clickMapItem = function (item) {
            if ($scope.selectedMapItem) {
                $scope.selectedMapItem.selected = false;
            }
            item.selected = true;
            $scope.selectedMapItem = item;
            var miadic = _.findWhere($scope.selectedMapItem.mapDicValues, { value: $scope.selectedMapItem.mapValue });
            if (miadic) {
                $scope.mapDicText = miadic.text;
            } else {
                $scope.mapDicText='无'
            }
                
        }
        //
        $scope.mapSiteChange = function () {
            getDicRelationList();
        }
        //map值改变
        $scope.mapValueChange = function () {
            $scope.selectedMapItem.isChange = true;
            var item = _.findWhere($scope.selectedMapItem.mapDicValues, { value: $scope.selectedMapItem.mapValue });
            if (item) {
                $scope.mapDicText = item.text;
            } else {
                $scope.mapDicText = '无'
            }
        }
        //保存
        $scope.saveMapDics = function () {
            configurationService.saveDicMappings($scope.dicRelationList).success(function (result) {
                if (result) {
                    csdToaster.info('保存成功！');
                    getDicRelationList();
                } else {
                    csdToaster.info('保存失败！');
                }
            });
        }
        //刷新
        $scope.mapRefresh = function () {
            getDicRelationList();
        }
        ; +(function init() {
            $scope.selectedMapItem = null;
            $scope.dicRelationList = null;
            getDicRelationList();
            $scope.mapSite = '';
        })()
    }])
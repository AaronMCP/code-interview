configurationModule.controller('SetupShareCtrl', [
 '$scope', '$state', '$log', '$modal', 'constants', 'configurationService', '$q', 'csdToaster', 'openDialog', '$translate', '$filter','$timeout',
 function ($scope, $state, $log, $modal, constants, configurationService, $q, csdToaster, openDialog, $translate, $filter, $timeout) {
     'use strict';
     $scope.onClose = function () {
         $scope.trs.length = 0;
         $scope.addOption = {
             site: null,
             shareType: null,
             count: null,
             targetType: 1
         };
         $scope.onCancel();
     };
     //点击保存
     $scope.onSave = function () {
         if ($scope.firstItem.maxNumber === 0) {
             if ($scope.isShareSliceId.length > 0) {
                 openDialog.openIconDialogOkCancel(
                 openDialog.NotifyMessageType,
                 $translate.instant('Alert'),
                 '覆盖已经设置的时间片？',
                 function () {
                     configurationService.getSharedTimeslice($scope.isShareSliceId[0]).success(function (result) {
                         $scope.isShareData = result;
                         $scope.sliceids = $scope.sliceIdsArr.concat($scope.isShareSliceId);
                         $scope.setSlice();
                     });
                 });
             } else {
                 $scope.setSlice();
             }
             
         } else {
             csdToaster.info('额度没有配完！');
         }
         
     };
    
     //设置时间片
     $scope.setSlice = function () {
         var sharers = [];
         for (var i = 0; i < $scope.trs.length; i++) {
             var shareObj = {
                 groupId: $scope.trs[i].groupId,
                 shareTarget: $scope.trs[i].site,
                 targetType: $scope.trs[i].targetType,
                 maxCount: $scope.trs[i].count
             }
             sharers.push(shareObj);
         };
         var addShareObj = {
             sliceIds: $scope.sliceids,
             sharers: sharers
         };
         if ($scope.trs.length > 0) {
             configurationService.shareTimeSlice(addShareObj).success(function (result) {
                 if (true) {
                     csdToaster.info('保存成功');
                     $scope.trs.length = 0;
                     $scope.trsLength = 0;
                     $scope.deleteGroupIdArr.length = 0;
                     $scope.addOption = {
                         site: [],
                         shareType: null,
                         count: null,
                         targetType: 1
                     };
                     $scope.onOk();
                 } else {
                     csdToaster.info('保存失败');
                 }
             });
         } else {
             csdToaster.info('请先添加共享对象');
         }
         
     }
     //添加
     $scope.index = null;
     $scope.selectRow = function (item) {
         if ($scope.selectedItem) {
             $scope.selectedItem.selected = false;
         }
         item.selected = true;
         $scope.selectedItem = item;
         $scope.index = this.$index;
     };
     //删除
     $scope.deleteShare = function () {
         if ($scope.selectedItem) {
             //私有共享
             $scope.trs.splice($scope.index, 1);
             if ($scope.selectedItem.groupId === '') {
                 $scope.firstItem.maxNumber = $scope.selectedItem.count + $scope.maxNumberCash;
             } else {
                 //公有共享             
                 if ($scope.trs.length) {
                     var index = 0;
                     for (var i = 0; i < $scope.trs.length; i++,index = i) {
                         if ($scope.trs[i].groupId === $scope.selectedItem.groupId) {
                             break;
                         }
                     }
                     //共享组内还有其他共享
                     if (index !== $scope.trs.length) {
                         $scope.firstItem.maxNumber = $scope.maxNumberCash;
                     } else {
                         //组内最后一个共享被删除
                         $scope.firstItem.maxNumber = $scope.selectedItem.count + $scope.maxNumberCash;
                     }
                 } else {
                     //组内最后一个共享被删除
                     $scope.firstItem.maxNumber = $scope.selectedItem.count + $scope.maxNumberCash;
                 } 
             }
             $scope.addOption.count = '';
             $scope.maxNumberCash = $scope.firstItem.maxNumber;
             $scope.maxInputNumber = $scope.firstItem.maxNumber / $scope.addOption.site.length;
             $scope.selectedItem = null;
         } else {
             openDialog.openIconDialog(
                 openDialog.NotifyMessageType,
                 $translate.instant('Alert'),
                 '请先选中一个共享');
         }
     };
     // 添加共享对象
     $scope.addShare = function () {
         if($scope.firstItem.maxNumber >= 0) {
             if ($scope.addOption.count > 0) {
                 //站点数大于1
                 if ($scope.addOption.site.length > 1) {
                     //分组id
                     $scope.groupId = $filter('date')(new Date(), 'yyyyMMddHHmmss') + $scope.idNumber;
                     $scope.idNumber = $scope.idNumber + 1;
                     for (var i = 0; i < $scope.addOption.site.length; i++) {
                         var showObj = {
                             site: $scope.addOption.site[i].value,
                             siteName: $scope.addOption.site[i].name,
                             shareType: $scope.addOption.shareType.value,
                             shareTypeName: $scope.addOption.shareType.name,
                             count: $scope.addOption.count,
                             targetType: 1
                         }
                         //公有共享
                         if ($scope.addOption.shareType.value === 1) {
                             showObj.groupId = 'A' + $scope.groupId;
                         } else {
                             //私有共享
                             showObj.groupId = '';
                         }
                         $scope.trs.push(showObj);
                     }
                 } else {
                     var showObj = {
                         groupId: '',
                         site: $scope.addOption.site[0].value,
                         siteName: $scope.addOption.site[0].name,
                         shareType: $scope.addOption.shareType.value,
                         shareTypeName: $scope.addOption.shareType.name,
                         count: $scope.addOption.count,
                         targetType: 1
                     }
                     $scope.trs.push(showObj);
                 }
                 $scope.addOption.count = '';
                 $scope.maxNumberCash = $scope.firstItem.maxNumber;
             } else {
                 csdToaster.info('请先输入配额！');
             }
         } else {
             csdToaster.info('没有配额了！');
         }   
     };
     //显示查询条件
     $scope.dateTypeName = '';
     switch ($scope.searchOption.dateType) {
         case 1:
             $scope.dateTypeName = '工作日';
             break;
         case 2:
             $scope.dateTypeName = '周末';
             break;
         case 3:
             $scope.dateTypeName = '节假日';
             break;
         case 4:
             $scope.dateTypeName = '检修日';
             break;
         default:
             break;
     };
     $scope.trs = [];
     //显示已经设置过的共享
     if ($scope.isShareData.length > 0) {
         for (var i = 0; i < $scope.isShareData.length; i++) {
             var shareType = null;
             var shareTypeName = null;
             if ($scope.isShareData[i].groupId === '') {
                 shareType = 0;
                 shareTypeName = '私有共享';
             } else {
                 shareType = 1;
                 shareTypeName = '公有共享';
             }
             var showObj = {
                 groupId:$scope.isShareData[i].groupId,
                 site: $scope.isShareData[i].shareTarget,
                 siteName: $scope.isShareData[i].shareTarget,
                 shareType: shareType,
                 shareTypeName: shareTypeName,
                 count: $scope.isShareData[i].maxCount,
                 targetType: $scope.isShareData[i].targetType,
                 selected: false
             }
             $scope.trs.push(showObj);
         };
     }else{
         $scope.trs = [];
     }

     //修改站点
     $scope.onChangeSite = function () {
         if ($scope.addOption.site.length > 1) {
             $scope.shareTypeabled = false;
             if ($scope.addOption.shareType !== null) {
                 if ($scope.addOption.shareType.value === 0) {
                     if ($scope.addOption.site.length * $scope.addOption.count > $scope.firstItem.maxNumber) {
                         csdToaster.info('配额不够，重新分配');
                         $scope.addOption.count = 0;
                         $scope.firstItem.maxNumber = $scope.maxNumberCash;
                         $scope.maxInputNumber = Math.floor($scope.maxNumberCash / $scope.addOption.site.length);
                     } else {
                         $scope.maxInputNumber = Math.floor($scope.maxNumberCash / $scope.addOption.site.length);
                         $scope.firstItem.maxNumber = $scope.maxNumberCash - $scope.addOption.count * $scope.addOption.site.length;
                     }
                 }
             }
         } else {
             $scope.maxInputNumber = $scope.maxNumberCash;
             $scope.shareTypeabled = true;
             $scope.firstItem.maxNumber = $scope.maxNumberCash - $scope.addOption.count * $scope.addOption.site.length;
             $scope.addOption.shareType = $scope.shareTypes[0];
         }
     };
    //修改共享类型 
     $scope.onChangeShareType = function () {
         if ($scope.addOption.shareType.value === 0) {
             if ($scope.addOption.site.length * $scope.addOption.count > $scope.firstItem.maxNumber) {
                 csdToaster.info('配额不够，重新分配');
                 $scope.addOption.count = 0;
                 $scope.firstItem.maxNumber = $scope.maxNumberCash;
                 $scope.maxInputNumber = Math.floor($scope.maxNumberCash / $scope.addOption.site.length);
             } else {
                 if ($scope.addOption.site.length === 0) {
                     $scope.maxInputNumber = $scope.maxNumberCash;
                     $scope.firstItem.maxNumber = $scope.maxNumberCash - $scope.addOption.count;
                 } else {
                     $scope.maxInputNumber = Math.floor($scope.maxNumberCash / $scope.addOption.site.length);
                     $scope.firstItem.maxNumber = $scope.maxNumberCash - $scope.addOption.count * $scope.addOption.site.length;
                 }
             }
         } else {
             if (!$scope.addOption.count) {
                 $scope.addOption.count = 0;
             }
             $scope.maxInputNumber = $scope.maxNumberCash;
             $scope.firstItem.maxNumber = $scope.maxNumberCash - $scope.addOption.count;
         }
         
     };
     //修改配额数
     $scope.onChangeCount = function () {
        if ($scope.addOption.site.length <= 1) {
             $scope.maxInputNumber = $scope.maxNumberCash;
             if ($scope.firstItem.maxNumber >= 0) {
                 if ($scope.maxNumberCash - $scope.addOption.count >= 0) {
                     $scope.firstItem.maxNumber = $scope.maxNumberCash - $scope.addOption.count;
                 } else {
                     csdToaster.info('配额不够！');
                 }
             } else {
                 csdToaster.info('没有配额！');
             }
         } else {
             if ($scope.addOption.shareType.value === 0) {
                 $scope.maxInputNumber = Math.floor($scope.maxNumberCash / $scope.addOption.site.length);
                 if ($scope.firstItem.maxNumber >= 0) {
                     if ($scope.maxNumberCash - $scope.addOption.count * $scope.addOption.site.length >= 0) {
                         $scope.firstItem.maxNumber = $scope.maxNumberCash - $scope.addOption.count * $scope.addOption.site.length;
                     } else {
                         csdToaster.info('配额不够！');
                     }
                 } else {
                     csdToaster.info('没有配额！');
                 }
             } else {
                 if ($scope.firstItem.maxNumber >= 0) {
                     $scope.maxInputNumber = $scope.maxNumberCash;
                     if ($scope.maxNumberCash - $scope.addOption.count >= 0) {
                         $scope.firstItem.maxNumber = $scope.maxNumberCash - Number($scope.addOption.count);
                     } else {
                         csdToaster.info('配额不够！');
                     }
                 } else {
                     csdToaster.info('没有配额！');
                 }
             }
        }
     }
     $scope.title = $scope.searchOption;
     $scope.title.dateTypeName = $scope.dateTypeName;
     $scope.firstItem = angular.copy($scope.items[0]);
     if ($scope.trs.length !== 0) {
         $scope.firstItem.maxNumber = 0;
     }
     $scope.maxNumberCash = $scope.firstItem.maxNumber;
     $scope.maxInputNumber = $scope.maxNumberCash;
     ; +(function init() {
         $scope.selectedItem = null;
         $scope.idNumber = 0;
         $scope.groupId = $filter('date')(new Date(), 'yyyyMMddHHmmss');
         $scope.shareTypeabled = false;
         $scope.trsLength = 0;
         $scope.deleteGroupIdArr = [];
         $scope.addOption = {
             site: [],
             shareType: null,
             count: null,
             targetType: 1
         };
         $scope.sites = [
             { name: 'site1', value: { name: 'site1', value: 'site1' } },
             { name: '临床部门', value: { name: '临床部门', value: 'ClinicalDepartment' } }
         ];
         $scope.shareTypes = [
             { name: '私有共享', value: 0 },
             { name: '公有共享', value: 1 }
         ];
         $scope.selectOptions = {
             dataTextField: "name",
             dataValueField: "value",
             valuePrimitive: true,
             autoBind: false,
             dataSource: $scope.sites,
             dataBound: function (e) {
                 if (!e.sender.dataSource.view()[0]) {
                     e.sender.ul.parent().parent().find('.k-nodata > div').html('无数据');
                 }
             }
         };
     })();
 }])
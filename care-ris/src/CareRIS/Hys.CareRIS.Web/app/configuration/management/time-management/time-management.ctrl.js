configurationModule.controller('TimeManagementCtrl', [
 '$scope', '$state', '$log', '$modal', 'constants', 'configurationService', '$q', 'csdToaster', 'openDialog', '$translate',
 function ($scope, $state, $log, $modal, constants, configurationService, $q, csdToaster, openDialog, $translate) {
     'use strict';
     //时间片id 数组
     $scope.sliceIdsArr = [];
     $scope.pushSliceIds = [];
     $scope.isShareSliceId = [];
     $scope.getModality = function (site, type, callback) {
         configurationService.
             getModalitiesByType(site, type).
             success(function (data) {
                 var modalities = _.map(data, function (item) {
                     return item.modalityName;
                 });
                 modalities.sort(function (a, b) { return a > b });
                 callback(modalities);
             });
     };

     $scope.refreshModality = function () {
         $scope.getModality($scope.searchOption.site || '', $scope.searchOption.modalityType, function (modalities) {
             $scope.isShareSliceId.length = 0;
             $scope.sliceIdsArr.length = 0;
             $scope.selectedTimeslices.length = 0;
             $scope.modalities = modalities;
             $scope.searchOption.modality = modalities.length > 0 ? modalities[0] : '';
             $scope.searchOptionChanged();
         });
     };

     // copy
     $scope.copyRefreshModality = function () {
         $scope.getModality($scope.copyOption.site || '', $scope.copyOption.modalityType, function (modalities) {
             $scope.copyModalities = modalities;
             $scope.copyOption.modality = modalities.length > 0 ? modalities[0] : '';
         });
     };
     $scope.searchOptionChanged = function () {
         $scope.getTimeSlice();
     };

     $scope.getTimeSlice = function () {
         $scope.timeslices.splice(0, $scope.timeslices.length);

         configurationService
             .getTimeslice($scope.searchOption.modality, $scope.searchOption.dateType)
             .success(function (timeSlices) {
                 var timeslice = {};
                 if (!angular.isArray(timeSlices)) {
                     return;
                 }

                 var keys = [];
                 _.each(timeSlices, function (item) {
                     item.availableDate = new Date(item.availableDate);
                     item.endDt = new Date(item.endDt);
                     item.startDt = new Date(item.startDt);
                     item.selected = false;

                     var avaDate = item.availableDate.formatDateTime("yyyy-MM-dd");

                     var curr = timeslice[avaDate];
                     if (!curr) {
                         timeslice[avaDate] = curr = [];
                         keys.push(avaDate);
                     }
                     item.belongTo = avaDate;
                     curr.push(item);
                 });
                 keys.sort(function (a, b) { return a < b; });
                 _.each(keys, function (attr) {
                     timeslice[attr].sort(function (a, b) { return a.startDt - b.startDt; });
                     $scope.timeslices.push({
                         availableDate: attr,
                         items: timeslice[attr],
                         down: false
                     });
                 });
                 if ($scope.timeslices.length > 0) {
                     $scope.timeslices[0].down = true;
                 }
             });
         $scope.selectedTimeslices = [];
     };
     //弹出设置共享
     $scope.openSetupShare = function () {
         for (var i = 0; i < $scope.selectedTimeslices.length; i++) {
             var maxNumber = $scope.selectedTimeslices[0].maxNumber
             if ($scope.selectedTimeslices[i].maxNumber !== maxNumber) {
                 csdToaster.info('最大量要相同');
                 return;
             }
         }
         configurationService.getSharedTimeslice($scope.isShareSliceId[0]).success(function (result) {
             $scope.addListsShare = result;
             $scope.pushSliceIds = $scope.sliceIdsArr.concat($scope.isShareSliceId);
             $scope.shareReady = true;
             $scope.setupShareWindow.center().open();
         });
     };
     $scope.clearShare = function () {
         var addShareObj = {
             sliceIds: $scope.isShareSliceId,
             sharers: []
         }
         if ($scope.isShareSliceId.length > 0) {
             openDialog.openIconDialogOkCancel(
             openDialog.NotifyMessageType,
             $translate.instant('Alert'),
             '清空将覆盖已经设置的时间片？',
             function () {
                 configurationService.shareTimeSlice(addShareObj).success(function (result) {
                     if (true) {
                         csdToaster.info('已清空');
                         $scope.getTimeSlice();

                     } else {
                         csdToaster.info('清空失败');
                     }
                 });
             });
         } else {
             csdToaster.info('未有已共享的');
         }
     };
     $scope.closeSetupShare = function () {
         $scope.shareReady = false;
         $scope.setupShareWindow.close();
     };
     $scope.saveSetupShare = function () {
         $scope.shareReady = false;
         $scope.setupShareWindow.close();
         $scope.getTimeSlice();
     };
     $scope.togglePan = function (slice) {
         slice.down = !slice.down;
     };
     $scope.choseTimeslice = function (e, item, items) {
         if (e.ctrlKey) {
             if (!$scope.selectedTimeslices.length || item.belongTo === $scope.selectedTimeslices[0].belongTo) {
                 if (item.selected) {
                     item.selected = false;
                     item.single = false;
                     var len = $scope.selectedTimeslices.length;
                     for (var i = 0; i < len; ++i) {
                         if ($scope.selectedTimeslices[i].uniqueID === item.uniqueID) {
                             $scope.selectedTimeslices.splice(i, 1);
                             break;
                         }
                     }
                 } else {
                     item.selected = true;
                     item.single = true;
                     if ($scope.selectedTimeslices.length > 0
                        && item.belongTo !== $scope.selectedTimeslices[0].belongTo) {
                         _.each($scope.selectedTimeslices, function (sitem) {
                             for (var i = 0; i < $scope.timeslices.length; i++) {
                                 for (var j = 0; j < $scope.timeslices[i].items.length; j++) {
                                     if (slice.uniqueID === timeslices[i].items[j].uniqueID) {
                                         $scope.timeslices[i].items[j].selected = false;
                                         $scope.timeslices[i].items[j].single = false;
                                     }
                                 }

                             }
                         });
                         $scope.selectedTimeslices.length = 0;
                     }
                     $scope.selectedTimeslices.push(item);
                     if (item.isShared) {
                         $scope.isShareSliceId.push(item.uniqueID);
                     } else {
                         $scope.sliceIdsArr.push(item.uniqueID);
                     }
                 }
             } else {
                 openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '请选择同一个日期的时间片！');
             }
         } else {
             _.each($scope.selectedTimeslices, function (slice) {
                 for (var i = 0; i < $scope.timeslices.length; i++) {
                     for (var j = 0; j < $scope.timeslices[i].items.length; j++) {
                         if (slice.uniqueID === $scope.timeslices[i].items[j].uniqueID) {
                             $scope.timeslices[i].items[j].selected = false;
                             $scope.timeslices[i].items[j].single = false;
                         }
                     }

                 }
             });
             $scope.selectedTimeslices.length = 0;
             $scope.selectedTimeslices.push(item);
             $scope.sliceIdsArr.length = 0;
             $scope.isShareSliceId.length = 0;
             if (item.isShared) {
                 $scope.isShareSliceId.push(item.uniqueID);
             } else {
                 $scope.sliceIdsArr.push(item.uniqueID);
             }
             item.selected || (item.selected = true);
             item.single = true;
         }
         if ($scope.selectedTimeslices.length === items.length) {
             items.allFlag = true;
         } else {
             items.allFlag = false;
         }

     };

     //checkbox 多选
     $scope.allFlag = false;
     $scope.selectAll = function (items) {
         if (!$scope.selectedTimeslices.length || items[0].belongTo === $scope.selectedTimeslices[0].belongTo) {
             if (items.allFlag) {
                 $scope.selectedTimeslices = angular.copy(items);
                 _.each(items, function (item) {
                     item.selected = true;
                     item.single = true;
                 });
             } else {
                 _.each(items, function (item) {
                     item.selected = false;
                     item.single = false;
                 });
                 $scope.selectedTimeslices = [];
             }
         } else {
             items.allFlag = !items.allFlag;
             openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '请选择同一个日期的时间片！');
         }

     }
     //checkbox 单选
     $scope.selectSingle = function (e, item, items) {
         e.stopPropagation();
         if (!$scope.selectedTimeslices.length || item.belongTo === $scope.selectedTimeslices[0].belongTo) {
             if (item.single) {
                 item.selected = true;
                 $scope.selectedTimeslices.push(item);
             } else {
                 item.selected = false;
                 $scope.selectedTimeslices.splice($scope.selectedTimeslices.indexOf(item), 1);
             }
             if ($scope.selectedTimeslices.length === items.length) {
                 items.allFlag = true;
             } else {
                 items.allFlag = false;
             }
         } else {
             item.single = !item.single;
             openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '请选择同一个日期的时间片！');
         }

     }
     $scope.newTimeslice = function () {
         $scope.modifyTarget = null;
         $scope.timesliceWindow.title('添加时间片');
         $scope.dataReady = true;
         $scope.timesliceWindow.center().open();
     };
     $scope.modifyTimeslice = function () {
         $scope.modifyTarget = $scope.selectedTimeslices[0];
         $scope.timesliceWindow.title('修改时间片');
         $scope.dataReady = true;
         $scope.timesliceWindow.center().open();
     };
     $scope.delTimeslice = function () {
         openDialog.openIconDialogOkCancel(
             openDialog.NotifyMessageType,
             $translate.instant('Alert'),
             '确定删除选中的时间片吗？',
             function () {
                 var timesliceIds = _.map($scope.selectedTimeslices, function (item) { return item.uniqueID; });
                 configurationService.delTimeslice(timesliceIds).success(function () {
                     csdToaster.info('删除成功!');
                     var firstSelected = $scope.selectedTimeslices[0];
                     var group = null;
                     var groupItems = null;
                     var item = null;
                     for (var i = 0; i < $scope.timeslices.length; ++i) {
                         group = $scope.timeslices[i];
                         if (firstSelected.belongTo !== group.availableDate) continue;

                         groupItems = group.items;
                         for (var j = 0; j < groupItems.length; ++j) {
                             item = groupItems[j];
                             if (item.selected) {
                                 groupItems.splice(j, 1);
                                 j--;
                             }
                         }
                         if (groupItems.length === 0) {
                             $scope.timeslices.splice(i, 1);
                             i--;
                         }
                     }
                     $scope.selectedTimeslices.length = 0;
                 });
             });
     };
     $scope.cancelTimeslice = function () {
         $scope.dataReady = false;
         $scope.timesliceWindow.close();
     };
     $scope.confirmTimeslice = function () {
         $scope.dataReady = false;
         $scope.timesliceWindow.close();
         if (!$scope.modifyTarget) {
             $scope.getTimeSlice();
         }
     };

     $scope.timesliceCopy = function () {
         var dateType = '';
         switch ($scope.searchOption.dateType) {
             case 0:
                 dateType = '工作日';
                 break;
             case 1:
                 dateType = '周末';
                 break;
             case 2:
                 dateType = '节假日';
                 break;
             case 3:
                 dateType = '检修日';
                 break;
             default:
                 break;
         }
         $scope.timesliceCopyWindow.title('复制' + $scope.searchOption.modality + '属于' + dateType + '的时间片到');
         $scope.copyOption.availableDate = $scope.selectedTimeslices[0].belongTo.replace(/-/g, '/');
         $scope.timesliceCopyWindow.open();
     }

     //cancelCopy
     $scope.cancelCopy = function () {
         $scope.timesliceCopyWindow.close();
     }

     //okCopy
     $scope.okCopy = function () {
         if ($scope.copyOption.modality === '') {
             openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), '设备不能为空');
         } else {
             var sliceIds = [];
             for (var i = 0; i < $scope.selectedTimeslices.length; i++) {
                 sliceIds.push($scope.selectedTimeslices[i].uniqueID);
             }
             var params = {
                 sliceIds: sliceIds,
                 modality: $scope.copyOption.modality,
                 availableDate: $scope.copyOption.availableDate,
                 dateType: $scope.searchOption.dateType
             }
             params.availableDate = new Date(params.availableDate)
             configurationService.copyTimeslice(params).success(function () {
                 csdToaster.info('复制成功！');
                 $scope.timesliceCopyWindow.close();
                 $scope.getTimeSlice();
             }).error(function (error) {
                 if (error.message === 'Decsription can not be duplicated in same modality and DateType!') {
                     csdToaster.info('所复制的时间片已经存在当前列表里，请重新选择复制到的列表！');
                 } else {
                     csdToaster.info('复制失败');
                 }
             });
         }
     }
     ; +(function init() {

         $scope.dataReady = false;
         $scope.timesliceTitle = '';
         $scope.timeslices = [];
         $scope.selectedTimeslices = [];
         $scope.modifyTarget = null;
         $scope.modalities = [];

         $scope.dateTypes = angular.copy(constants.dateTypes);

         $q.all({
             sites: configurationService.getAllSite(),
             modalityTypes: configurationService.getModalityTypes()
         }).then(function (data) {
             var sites = _.map(data.sites.data, function (item) {
                 return item.siteName;
             });
             sites.sort(function (a, b) { return a > b });

             $scope.sites = sites;
             var modalityTypes = data.modalityTypes.data;
             modalityTypes.sort(function (a, b) { return a > b });
             $scope.modalityTypes = modalityTypes;

             $scope.searchOption = {
                 site: null,
                 modalityType: $scope.modalityTypes[0],
                 modality: '',
                 dateType: constants.dateTypes[0].id
             };

             $scope.copyOption = {
                 site: null,
                 modalityType: $scope.modalityTypes[0],
                 modality: '',
                 availableDate: ''
             };

             $scope.refreshModality();
             $scope.copyRefreshModality();
         });
     })();
 }])
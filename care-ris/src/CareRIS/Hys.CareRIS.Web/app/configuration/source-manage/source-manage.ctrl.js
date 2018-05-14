configurationModule.controller('SourceManageCtrl', ['$scope', 'configurationService', 'loginContext', '$timeout', 'openDialog', '$translate', 'csdToaster',
    function ($scope, configurationService, loginContext, $timeout, openDialog, $translate, csdToaster) {

        //提醒框
        $scope.alert = function (info) {
            openDialog.openIconDialog(
                openDialog.NotifyMessageType.Warn,
                $translate.instant('Alert'),
                info);
        }
        //医院
        $scope.hospitalList = [];
        $scope.domain = loginContext.domain;
        $scope.hospitalList.push(loginContext.domain);
        //站点
        configurationService.getAllSite().success(function (result) {
            var sites = _.map(result, function (item) {
                return item.siteName;
            });
            sites.sort(function (a, b) { return a > b });
            $scope.sites = sites;

        });
        //设备类型
        configurationService.getModalityTypes().success(function (result) {
            $scope.modalityTypeList = result;
        });

        //提醒框
        $scope.alert = function (info) {
            openDialog.openIconDialog(
                openDialog.NotifyMessageType.Warn,
                $translate.instant('Alert'),
                info);
        }
        //点击树形结构
        $scope.click = function (dataItem) {
            $scope.selectedNode = dataItem;
            if (dataItem.type === 1) {
                $scope.modality = {
                    modalityType: dataItem.name
                };
                $scope.scanningTechModalityType = $scope.selectedNode.name;
            } else {
                var params = {
                    name: $scope.selectedNode.name,
                    type: $scope.selectedNode.modalityType
                }
                $scope.scanningTechModalityType = $scope.selectedNode.modalityType;
                configurationService.getModalitybyName(params).success(function (res) {
                    $scope.modality = res;
                    if (!$scope.modality.applyHaltPeriod) {
                        $scope.modality.startDt = '';
                        $scope.modality.endDt = '';
                    }
                });
            }
            $scope.getScanningTech();
        }
        
        //获取树形结构、刷新
        $scope.refreshModality = function () {
            configurationService.getModalityTypeNode().success(function (res) {
                $scope.res = res;
                $scope.sourceManageTree.setDataSource(new kendo.data.HierarchicalDataSource({
                    data: $scope.res
                }));
                $scope.modality = null;
                $scope.getScanningTech();
            })
        }

        //添加设备
        $scope.addModality = function () {
            if (!$scope.modality.applyHaltPeriod) {
                $scope.modality.startDt = '';
                $scope.modality.endDt = '';
            } else {
                if (!$scope.modality.startDt) {
                    $scope.alert('请填写起始时间！');
                    return;
                } else if (!$scope.modality.endDt) {
                    $scope.alert('请填写结束时间！');
                    return;
                }
                $scope.modality.startDt = new Date($scope.modality.startDt);
                $scope.modality.endDt = new Date($scope.modality.endDt + ' 23:59:59')
            }
            if ($scope.modality.startDt > $scope.modality.endDt) {
                $scope.alert('起始日不能大于结束日！');
                return;
            }
            configurationService.addModality($scope.modality).success(function (res) {
                if (res === 1) {
                    csdToaster.info('新增成功！');
                    $scope.refreshModality();
                } else {
                    csdToaster.info('名称重复！')
                }
            });
        }

        //删除设备
        $scope.deleteModality = function () {
            openDialog.openIconDialogOkCancel(
                openDialog.NotifyMessageType.Warn,
                $translate.instant('Alert'),
                '是否删除设备？',
                function () {
                    configurationService.deleteModality($scope.modality.uniqueID).success(function (res) {
                        if (res) {
                            csdToaster.info('删除成功！');
                            $scope.refreshModality();
                        }
                    });
                });
        }
        //修改设备
        $scope.updateModality = function () {
            if (!$scope.modality.applyHaltPeriod) {
                $scope.modality.startDt = '';
                $scope.modality.endDt = '';
            } else {
                if (!$scope.modality.startDt) {
                    $scope.alert('请填写起始时间！');
                    return;
                } else if (!$scope.modality.endDt) {
                    $scope.alert('请填写结束时间！');
                    return;
                }
                $scope.modality.startDt = new Date($scope.modality.startDt);
                $scope.modality.endDt = new Date($scope.modality.endDt + ' 23:59:59')
            }
            if ($scope.modality.startDt > $scope.modality.endDt) {
                $scope.alert('起始日不能大于结束日！');
                return;
            }
            configurationService.updateModality($scope.modality).success(function (res) {
                if (res) {
                    csdToaster.info('修改成功！');
                }
            });
        }
        //清空设备信息
        $scope.clearModality = function () {
            $scope.modality = null;
        }

        $scope.siteChange = function () {
            if ($scope.addScanningTechSite === '') {
                $scope.scanSite = '医院';
            } else {
                $scope.scanSite = '站点';
            }
            $scope.getScanningTech();
        }

        $scope.clickScanningTech = function (scan) {
            if ($scope.selectedScanningTech) {
                $scope.selectedScanningTech.selected = false;
            }
            scan.selected = true;
            $scope.selectedScanningTech = scan;
            $scope.scanningTechMirror = angular.copy($scope.selectedScanningTech);
        }

        //获取扫描技术
        $scope.getScanningTech = function () {
            if (!$scope.scanningTechModalityType) {
                $scope.scanningTechModalityType = $scope.res[0].name;
            }
            if (!$scope.addScanningTechSite) {
                $scope.addScanningTechSite = '';
            }
            var params = {
                site: $scope.addScanningTechSite,
                type: $scope.scanningTechModalityType
            }
            configurationService.getScanningTech(params).success(function (res) {
                $scope.scanningTechs = res;
            })
        }
       
        //增加扫描技术
        $scope.addScanningTech = function () {
            $scope.addFlag = true;
            $scope.addScanWindow.open();
        }

        //修改扫描技术
        $scope.updateScanningTech = function () {
            $scope.addFlag = false;
            $scope.addScanWindow.open();
        }

        //增加扫描技术确定
        $scope.addScanningTechOK = function () {
            $scope.scanningTechMirror.site = $scope.addScanningTechSite;
            $scope.scanningTechMirror.modalityType = $scope.scanningTechModalityType;
            if ($scope.addFlag) {
                $scope.scanningTechMirror.selected = false;
                configurationService.addScanningTech($scope.scanningTechMirror).success(function (res) {
                    if (res === 1) {
                        $scope.getScanningTech();
                        $scope.addScanWindow.close();
                        csdToaster.info('新增成功！');
                    } else {
                        csdToaster.info('名称重复！');
                    }
                });
            } else {
                configurationService.updateScanningtech($scope.scanningTechMirror).success(function (res) {
                    if (res === 1) {
                        $scope.getScanningTech();
                        $scope.addScanWindow.close();
                        csdToaster.info('修改成功！');
                    } else {
                        csdToaster.info('名称重复！');
                    }
                });
            }
            
        }

        //删除扫描技术
        $scope.deleteScanningTech = function () {
            configurationService.deleteScanningtech($scope.scanningTechMirror.uniqueID).success(function (res) {
                if (res) {
                    for (var i = 0; i < $scope.scanningTechs.length; i++) {
                        if ($scope.scanningTechs[i].uniqueID === $scope.scanningTechMirror.uniqueID) {
                            $scope.scanningTechs.splice(i, 1);
                        }
                    }
                    csdToaster.info('删除成功！');
                }
            });
        }

        $scope.addScanningTechCancel = function () {
            $scope.addScanWindow.close();
        }
        
        ; + (function init() {
            $scope.selectedScantech = null;
            $scope.scanSite = '医院';
            $scope.addScanningTechSite = '';
            $scope.modality = null;
            $scope.selectedNode = null;
            $scope.refreshModality();
            $scope.bookingShowMode = [
                { value: 0, text: '显示数量' },
                { value: 1, text: '显示检查部位' },
                { value: 2, text: '显示病人姓名' },
                { value: 3, text: '显示病人姓名和检查部位' }];
        })()
    }
])
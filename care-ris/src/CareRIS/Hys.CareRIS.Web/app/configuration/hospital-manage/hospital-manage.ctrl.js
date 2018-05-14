configurationModule.controller('HospitalManageCtrl', ['$scope', 'configurationService', '$timeout', 'openDialog', '$translate', 'csdToaster',
    function ($scope, configurationService, $timeout, openDialog, $translate, csdToaster) {


        //提醒框
        $scope.alert = function (info) {
            openDialog.openIconDialog(
                openDialog.NotifyMessageType.Warn,
                $translate.instant('Alert'),
                info);
        }
        //添加站点
        $scope.addSite = function () {
            $scope.changeSite = true;
            $scope.changeDomain = false;
            $scope.operateText = '添加站点';
            $scope.isAdd = true;
            $scope.site = '';
        }

        //点击修改
        $scope.update = function () {
            if ($scope.isDomain) {
                $scope.changeDomain = true;
                $scope.changeSite = false;
                $scope.isSite = false;
                $scope.operateText = '修改医院';
            } else {
                $scope.changeSite = true;
                $scope.changeDomain = false;
                $scope.operateText = '修改站点';
                $scope.site = angular.copy($scope.selectedSite);
            }
        }
        //点击保存
        $scope.save = function () {
            //修改医院
            if ($scope.changeDomain) {
                configurationService.updateDomain($scope.domain).success(function (res) {
                    if (res === 1) {
                        $('#domainManageModal').modal('hide');
                        csdToaster.info('修改医院成功！');
                        $scope.changeDomain = false;
                        $scope.domainGrid.dataSource.read();
                    }
                });
            } else if ($scope.isAdd) {
                configurationService.addSite($scope.site).success(function (res) {
                    if (res === 1) {
                        $('#domainManageModal').modal('hide');
                        csdToaster.info('新增站点成功！');
                        $scope.changeSite = false;
                        $scope.isAdd = false;
                        $scope.domainGrid.dataSource.read();
                    } else if (res === -1) {
                        csdToaster.info('名称重复！');
                    }
                });
            } else {
                //修改站点
                configurationService.updateSite($scope.site).success(function (res) {
                    if (res === 1) {
                        $('#domainManageModal').modal('hide');
                        csdToaster.info('修改站点成功！');
                        $scope.changeSite = false;
                        $scope.domainGrid.dataSource.read();
                    }
                })
            }
        }
        //取消
        $scope.cancel = function () {
            $scope.isAdd = false;
            $('#domainManageModal').modal('hide');
        }

        // 刷新
        $scope.refresh = function () {
            $scope.domainGrid.dataSource.read();
        }
        $scope.domainGridOption = {
            dataSource: {
                type: 'json',
                transport: {
                    read: function (options) {
                        configurationService.getDomainList().success(function (res) {
                            $scope.domains = res;
                            options.success($scope.domains);
                        });
                    }
                },
                serverPaging: true,
                sort: [{ field: "domainName", dir: "asc" }],
            },
            selectable: "row",
            dataBound: function () {
                this.expandRow(this.tbody.find("tr.k-master-row").first());
            },
            columns: [
                { field: 'domainName', title: '域名' },
                { field: 'alias', title: '域简称' },
                { field: 'domainPrefix', title: '域前缀' },
                { field: 'ftpServer', title: 'FTP服务器' },
                { field: 'ftpPort', title: 'FTP端口' },
                { field: 'ftpUser', title: 'FTP用户' },
                { field: 'ftpPassword', title: 'FTP密码' },
            ],
            change: function (e) {
                var selectedItem = this.dataItem(this.select()[0]);
                $timeout(function () {
                    $scope.selectedDomain = selectedItem;
                    $scope.domain = angular.copy($scope.selectedDomain);
                    $scope.isDomain = true;
                    $scope.isSite = false;
                })
            }
        }

        $scope.siteGridOption = function (dataItem) {
            return {
                dataSource: {
                    type: 'json',
                    transport: {
                        read: function (options) {
                            configurationService.getSiteList().success(function (res) {
                                $scope.sites = res;
                                options.success($scope.sites);
                            });
                        }
                    },
                    serverFiltering: true,
                    serverPaging: true,
                    sort: [{ field: "siteName", dir: "desc" }],
                },
                selectable: "row",
                columns: [
                    { field: 'siteName', title: '站点名' },
                    { field: 'alias', title: '站点简称' },
                    { field: 'telephone', title: '电话' },
                    { field: 'address', title: '地址' },
                    { field: 'pacsServer', title: 'PACS服务器' },
                    { field: 'pacsWebServer', title: 'PACSWeb服务器' },
                    { field: 'pacsAETitle', title: 'AE title' },
                ],
                change: function (e) {
                    var selectedItem = this.dataItem(this.select()[0]);
                    $timeout(function () {
                        $scope.selectedSite = selectedItem;
                        $scope.site = angular.copy($scope.selectedSite);
                        $scope.isSite = true;
                        $scope.isDomain = false;
                    })
                }
            }
        }

        //打开站点配置
        $scope.openSiteConfig = function () {
            var params = {
                site: $scope.selectedSite.siteName,
                domain: $scope.selectedSite.domain
            }
            configurationService.getSiteProfile(params).success(function (res) {
                res = JSON.parse(res).SiteProfiles;
                $scope.dataHandle(res, $scope.siteProfiles);
                $scope.siteProfileList = angular.copy($scope.siteProfiles);
            });
            var title = '站点配置 --- 域名: ' + $scope.selectedSite.domain + ' 站点：' + $scope.selectedSite.siteName;
            $scope.siteConfigureWindow.title(title);
            $scope.siteConfigureWindow.open();
        }

        //编辑配置
        $scope.openEditSite = function () {
            $scope.editText = '添加删除站点配置 --- 域名: ' + $scope.selectedSite.domain + ' 站点：' + $scope.selectedSite.siteName;
            configurationService.getSystemProfiles($scope.selectedSite.domain).success(function (res) {
                var res = JSON.parse(res).SystemProfiles;
                $scope.dataHandle(res, $scope.systemProfiles);
                for (var attr in $scope.siteProfiles) {
                    for (var i = 0; i < $scope.siteProfiles[attr].items.length; i++) {
                        for (var j = 0; j < $scope.systemProfiles[attr].items.length; j++) {
                            if ($scope.systemProfiles[attr].items[j].Name === $scope.siteProfiles[attr].items[i].Name) {
                                $scope.systemProfiles[attr].items.splice(j, 1);
                            }
                        }
                    }
                }
                $scope.isEditConfig = true;
            });
        }
        $scope.closeEditSite = function () {
            $scope.siteProfileList = $scope.siteProfiles;
            $scope.isEditConfig = false;
        }

        //处理数据
        $scope.dataHandle = function (list, profile) {
            profile.systemProfileGlobal = {
                items: [],
                down: true,
                name: '系统框架'
            };
            profile.systemProfileAuthorization = {
                items: [],
                down: true,
                name: '全局'
            };
            profile.systemProfileExamApplication = {
                items: [],
                down: true,
                name: '检查申请'
            };
            profile.systemProfileRegistration = {
                items: [],
                down: true,
                name: '登记检查'
            };
            profile.systemProfileReport = {
                items: [],
                down: true,
                name: '报告'
            };
            profile.systemProfileTeaching = {
                items: [],
                down: true,
                name: '教学'
            };
            profile.systemProfileExamination = {
                items: [],
                down: true,
                name: '检查'
            };
            profile.systemProfileStatistic = {
                items: [],
                down: true,
                name: '统计'
            };
            profile.systemProfileConfigure = {
                items: [],
                down: true,
                name: '配置'
            };;
            profile.systemProfileQualityControl = {
                items: [],
                down: true,
                name: '质量控制'
            };
            profile.systemProfileIntegration = {
                items: [],
                down: true,
                name: '集成'
            };
            profile.systemProfileBooking = {
                items: [],
                down: true,
                name: '预约'
            };
            for (var i = 0; i < list.length; i++) {
                // type = 0  任意输入的输入框
                // type = 1  数字框
                if (list[i].PropertyType === '1') {
                    var str = list[i].PropertyOptions.slice(1, -1);
                    var arr = str.split('-');
                    list[i].maxNumber = arr[1];
                    list[i].minNumber = arr[0];
                    list[i].Value = Number(list[i].Value);
                }

                // type = 3  下拉单选框
                if (list[i].PropertyType === '3' || list[i].PropertyType === '4') {
                    if (angular.isString(list[i].PropertyOptions)) {
                        list[i].selectOptions = list[i].PropertyOptions.split('|');
                    } else {
                        list[i].selectOptions = [];
                        for (var j = 0; j < list[i].PropertyOptions.length; j++) {
                            list[i].selectOptions.push(list[i].PropertyOptions[j].value);
                        }
                    }

                }

                // type = 4  可输入的下拉单选框
                if (list[i].PropertyType === '3' || list[i].PropertyType === '4') {
                    if (angular.isString(list[i].PropertyOptions)) {
                        list[i].selectOptions = list[i].PropertyOptions.split('|');
                    } else {
                        list[i].selectOptions = [];
                        for (var j = 0; j < list[i].PropertyOptions.length; j++) {
                            list[i].selectOptions.push(list[i].PropertyOptions[j].value);
                        }
                    }
                }

                //展示的value和多选框需要的options  type=5  下拉多选框 
                if (list[i].PropertyType === '5') {
                    list[i].selectValue = list[i].Value.split('|');
                    if (angular.isString(list[i].PropertyOptions)) {
                        list[i].selectOptions = list[i].PropertyOptions.split('|');
                    } else {
                        list[i].selectOptions = [];
                        for (var j = 0; j < list[i].PropertyOptions.length; j++) {
                            list[i].selectOptions.push(list[i].PropertyOptions[j].value);
                        }
                    }
                }

                //type = 6 颜色选择器
                if (list[i].PropertyType === '6') {
                    var arr = list[i].Value.split(',');
                    var str = arr.join(', ');
                    list[i].selectValue = 'rgba(' + str + ')';
                }

                //type = 9 folder文件路径
                if (list[i].PropertyType === '9') {
                    //console.log('floder   ' + i);
                }

                //type = 10 url文件路径
                if (list[i].PropertyType === '10') {
                    //console.log('url   ' + i);
                }
                //type = 15 密码输入框
                if (list[i].PropertyType === '15') {
                    //console.log('password   ' + i);
                }
                //type = 17 OnlineUserSetting
                if (list[i].PropertyType === '17') {
                    //console.log('count   ' + i);
                }
                var moduleId = list[i].ModuleId;

                switch (moduleId) {
                    case '0000':
                        profile.systemProfileGlobal.items.push(list[i]);
                        break;
                    case '0100':
                        profile.systemProfileAuthorization.items.push(list[i]);
                        break;
                    case '0200':
                        profile.systemProfileExamApplication.items.push(list[i]);
                        break;
                    case '0300':
                        profile.systemProfileRegistration.items.push(list[i]);
                        break;
                    case '0400':
                        profile.systemProfileReport.items.push(list[i]);
                        break;
                    case '0500':
                        profile.systemProfileTeaching.items.push(list[i]);
                        break;
                    case '0600':
                        profile.systemProfileExamination.items.push(list[i]);
                        break;
                    case '0700':
                        profile.systemProfileStatistic.items.push(list[i]);
                        break;
                    case '0800':
                        profile.systemProfileConfigure.items.push(list[i]);
                        break;
                    case '0D00':
                        profile.systemProfileQualityControl.items.push(list[i]);
                        break;
                    case '0E00':
                        profile.systemProfileIntegration.items.push(list[i]);
                        break;
                    case '0H00':
                        profile.systemProfileBooking.items.push(list[i]);
                        break;
                }
            }
        }

        $scope.togglePan = function (profile) {
            profile.down = !profile.down;
        }

        //点击域配置
        $scope.selectSystemProfile = function (systemProfile) {
            if ($scope.selectedSystemProfile) {
                $scope.selectedSystemProfile.selected = false;
            }

            systemProfile.selected = true;
            $scope.selectedSystemProfile = systemProfile;
        }

        //点击站点配置
        $scope.selectSiteProfile = function (siteProfile) {
            if ($scope.selectedSiteProfile) {
                $scope.selectedSiteProfile.selected = false;
            }

            siteProfile.selected = true;
            $scope.selectedSiteProfile = siteProfile;
        }

        //移动配置
        $scope.moveProfile = function (selectedProfile, profiles, toProfiles, flag) {
            var params = {
                moduleId: selectedProfile.ModuleId,
                domain: $scope.selectedSite.domain,
                site: $scope.selectedSite.siteName,
                name: selectedProfile.Name
            }
            if (flag) {
                configurationService.addSiteProfile(params).success(function (res) {
                    if (res) {
                        $scope.move(selectedProfile, profiles, toProfiles);
                        $scope.selectedSystemProfile = null;
                    }
                });
            } else {
                configurationService.deleteSiteProfile(params).success(function (res) {
                    if (res) {
                        $scope.move(selectedProfile, profiles, toProfiles);
                        $scope.selectedSiteProfile = null;
                    }
                });
            }

        }

        //转移配置
        $scope.move = function (selectedProfile, profiles, toProfiles) {
            var moduleId = selectedProfile.ModuleId;
            var profile = angular.copy(selectedProfile);
            var attr;
            profile.selected = false;
            switch (moduleId) {
                case '0000':
                    attr = 'systemProfileGlobal';
                    break;
                case '0100':
                    attr = 'systemProfileAuthorization';
                    break;
                case '0200':
                    attr = 'systemProfileExamApplication';

                    break;
                case '0300':
                    attr = 'systemProfileRegistration';

                    break;
                case '0400':
                    attr = 'systemProfileReport';

                    break;
                case '0500':
                    attr = 'systemProfileTeaching';

                    break;
                case '0600':
                    attr = 'systemProfileExamination';

                    break;
                case '0700':
                    attr = 'systemProfileStatistic';

                    break;
                case '0800':
                    attr = 'systemProfileConfigure';

                    break;
                case '0D00':
                    attr = 'systemProfileQualityControl';

                    break;
                case '0E00':
                    attr = 'systemProfileIntegration';

                    break;
                case '0H00':
                    attr = 'systemProfileBooking';

                    break;
            }
            for (var i = 0; i < profiles[attr].items.length; i++) {
                if (profiles[attr].items[i].Name === profile.Name) {
                    profiles[attr].items.splice(i, 1);
                }
            }
            toProfiles[attr].items.push(profile);
        }
        //保存站点配置
        $scope.saveSite = function () {
            var params = [];
            for (var attr in $scope.siteProfileList) {
                for (var i = 0; i < $scope.siteProfileList[attr].items.length; i++) {
                    $scope.siteProfileList[attr].items[i].domain = $scope.selectedSite.domain;
                    $scope.siteProfileList[attr].items[i].Site = $scope.selectedSite.siteName;
                    params.push($scope.siteProfileList[attr].items[i]);
                }
            }

            configurationService.updateSiteProfile(params).success(function (res) {
                if (res) {
                    $scope.refreshSite();
                    csdToaster.info('站点配置保存成功！');
                }
            });
        }

        //刷新
        $scope.refreshSite = function () {
            var params = {
                site: $scope.selectedSite.siteName,
                domain: $scope.selectedSite.domain
            }
            configurationService.getSiteProfile(params).success(function (res) {
                res = JSON.parse(res).SiteProfiles;
                $scope.dataHandle(res, $scope.siteProfileList);
            });
        }

        //下拉框搜索无数据
        $scope.noDataFound = function (e) {
            if (!e.sender.dataSource.view()[0]) {
                e.sender.ul.parent().parent().find('.k-nodata > div').html('无数据');
            }
        }

        //点击站点配置项
        $scope.siteProfileClick = function (siteProfile) {
            if (siteProfile.selected) {
                if ($scope.selectedSiteProfile.PropertyType === '11') {
                    if (siteProfile.Value === '0') {
                        siteProfile.Value = '1';
                    } else {
                        siteProfile.Value = '0';
                    }
                    siteProfile.isChange = true;
                }
            }
            if ($scope.selectedSiteProfile) {
                $scope.selectedSiteProfile.selected = false;
            }
            siteProfile.selected = true;
            $scope.selectedSiteProfile = siteProfile;
        }
        //下拉多选change
        $scope.siteProfileChange = function (siteProfile) {
            siteProfile.Value = siteProfile.selectValue.join('|');
            siteProfile.isChange = true;
        }
        //颜色change
        $scope.siteProfileColorChange = function (siteProfile) {
            siteProfile.isChange = true;
            var value = siteProfile.selectValue.slice(5, -1);
            siteProfile.Value = value.replace(/\s+/g, '')
        }
        ; +(function init() {
            $scope.systemProfiles = {};
            $scope.siteProfiles = {};
            $scope.isEditConfig = false;
            $scope.changeSite = false;
            $scope.changeDomain = false;
            $scope.isSite = false;
            $scope.selectedSite = '';
            $scope.selectedDomain = '';
            $scope.site = null;
            $scope.domain = null;
        })()
    }
])
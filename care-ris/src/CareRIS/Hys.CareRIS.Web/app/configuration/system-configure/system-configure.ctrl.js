configurationModule.controller('SystemConfigureCtrl', ['$scope', '$state', 'constants', 'loginContext', 'configurationService', 'csdToaster',
    function ($scope, $state, constants, loginContext, configurationService, csdToaster) {
    'use strict';
        //医院
        $scope.hospitalList = [];
        $scope.hospitalList.push(loginContext.domain);
        $scope.domain = loginContext.domain;

        $scope.refresh = function () {
            configurationService.getSystemProfiles($scope.domain).success(function (data) {
                $scope.initData();
                var list = JSON.parse(data).SystemProfiles;
                $scope.systemProfiles = list;
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
                            $scope.systemProfileGlobal.push(list[i]);
                            break;
                        case '0100':
                            $scope.systemProfileAuthorization.push(list[i]);
                            break;
                        case '0200':
                            $scope.systemProfileExamApplication.push(list[i]);
                            break;
                        case '0300':
                            $scope.systemProfileRegistration.push(list[i]);
                            break;
                        case '0400':
                            $scope.systemProfileReport.push(list[i]);
                            break;
                        case '0500':
                            $scope.systemProfileTeaching.push(list[i]);
                            break;
                        case '0600':
                            $scope.systemProfileExamination.push(list[i]);
                            break;
                        case '0700':
                            $scope.systemProfileStatistic.push(list[i]);
                            break;
                        case '0800':
                            $scope.systemProfileConfigure.push(list[i]);
                            break;
                        case '0D00':
                            $scope.systemProfileQualityControl.push(list[i]);
                            break;
                        case '0E00':
                            $scope.systemProfileIntegration.push(list[i]);
                            break;
                        case '0H00':
                            $scope.systemProfileBooking.push(list[i]);
                            break;
                    }
                }
            });
        }
        $scope.saveSystemProfile = function () {
            var params = {
                domain: $scope.domain,
                systemProfileList: $scope.systemProfiles
            }
            configurationService.updateSystemProfiles(params).success(function (res) {
                if (res) {
                    $scope.refresh();
                    csdToaster.info('修改成功！');
                } else {
                    csdToaster.info('修改失败！');
                }
            })
        }

        ; +(function init() {
            $scope.initData = function () {
                $scope.systemProfileGlobal = [];
                $scope.systemProfileAuthorization = [];
                $scope.systemProfileExamApplication = [];
                $scope.systemProfileRegistration = [];
                $scope.systemProfileReport = [];
                $scope.systemProfileTeaching = [];
                $scope.systemProfileExamination = [];
                $scope.systemProfileStatistic = [];
                $scope.systemProfileQualityControl = [];
                $scope.systemProfileIntegration = [];
                $scope.systemProfileConfigure = [];
                $scope.systemProfileBooking = [];
            } 
            $scope.refresh();
        })();
}])
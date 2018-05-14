configurationModule.controller('SystemConfigureAllController', ['$scope', 'loginContext', 'configurationService',
    function ($scope, loginContext, configurationService) {

        //下拉框搜索无数据
        $scope.noDataFound = function (e) {
            if (!e.sender.dataSource.view()[0]) {
                e.sender.ul.parent().parent().find('.k-nodata > div').html('无数据');
            }
        }
        $scope.domain = loginContext.domain;
        //点击配置项
        $scope.systemProfileClick = function (systemProfile) {
            if (systemProfile.selected) {
                if ($scope.selectedSystemProfile.PropertyType === '11') {
                    if (systemProfile.Value === '0') {
                        systemProfile.Value = '1';
                    } else {
                        systemProfile.Value = '0';
                    }
                    systemProfile.isChange = true;
                }
            }
            if ($scope.selectedSystemProfile) {
                $scope.selectedSystemProfile.selected = false;
            }
            systemProfile.selected = true;
            $scope.selectedSystemProfile = systemProfile;
        }
        //下拉多选change
        $scope.systemProfileChange = function (systemProfile) {
            systemProfile.Value = systemProfile.selectValue.join('|');
            systemProfile.isChange = true;
        }
        //颜色change
        $scope.systemProfileColorChange = function (systemProfile) {
            systemProfile.isChange = true;
            var value = systemProfile.selectValue.slice(5, -1);
            systemProfile.Value = value.replace(/\s+/g,'')
        }
        ; +(function init() {
        })();
    }
])
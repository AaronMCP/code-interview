configurationModule.controller('RoleSystemController', ['$scope','configurationService',
    function ($scope, configurationService) {
        'use strict';
        //下拉框搜索无数据
        $scope.noDataFound = function (e) {
            if (!e.sender.dataSource.view()[0]) {
                e.sender.ul.parent().parent().find('.k-nodata > div').html('无数据');
            }
        }

        //选中一行
        $scope.roleProfileClick = function (roleProfile) {
            if (roleProfile.selected) {
                if ($scope.selectedRoleProfile.PropertyType === '11') {
                    if (roleProfile.Value === '0') {
                        roleProfile.Value = '1';
                    } else {
                        roleProfile.Value = '0';
                    }
                    roleProfile.isChange = true;
                }
            }
            if ($scope.selectedRoleProfile) {
                $scope.selectedRoleProfile.selected = false;
            }
            roleProfile.selected = true;
            $scope.selectedRoleProfile = roleProfile;
        }

        $scope.inputChange = function (value, min, max) {
            if (value > max || value == max) {
                value = max;
            }
            if (value > min || value == min) {
                value = min;
            }
        }
        ; +(function init() {
            $scope.roleProfileChange = function (roleProfile) {
                roleProfile.Value = roleProfile.selectValue.join('|');
                console.log(roleProfile.selectOptions);
                roleProfile.isChange = true;
            };
        })()
}])
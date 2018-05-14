configurationModule.controller('ClientManageCtrl', ['$scope', '$state', 'constants', 'clientAgentService', 'openDialog', '$translate', 'csdToaster', 'loginContext',
    function ($scope, $state, constants, clientAgentService, openDialog, $translate, csdToaster, loginContext) {
        'use strict';

        //提醒框
        $scope.alert = function (info) {
            openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), info);
        }

        //获取配置信息
        var InitData = function () {
            clientAgentService.pacsConfig().success(function (pacs) {
                if (pacs) {
                    $scope.pacs = JSON.parse(pacs);
                    $scope.strArgs = $scope.pacs.desktopClient.args.join("|");
                }
            })
        }
        $scope.clientRefresh = function () {
            InitData();
        }
        var validate = function () {
            if (!$scope.pacs.desktopClient.disabled) {
                if (!$scope.pacs.desktopClient.path || $scope.pacs.desktopClient.path=='')
                {
                    $scope.alert("路径不能为空！");
                    return false;
                }
                if (!$scope.pacs.desktopClient.args || $scope.pacs.desktopClient.args.length<1) {
                    $scope.alert("args不能为空！");
                    return false;
                }
                if (!$scope.pacs.desktopClient.param.IP || $scope.pacs.desktopClient.param.IP == '') {
                    $scope.alert("IP不能为空！");
                    return false;
                }
                if (!$scope.pacs.desktopClient.param.port || $scope.pacs.desktopClient.param.port == '') {
                    $scope.alert("端口不能为空！");
                    return false;
                }
                if (!$scope.pacs.desktopClient.param.HeaderServicePort || $scope.pacs.desktopClient.param.HeaderServicePort == '') {
                    $scope.alert("服务端口不能为空！");
                    return false;
                }
                if (!$scope.pacs.desktopClient.param.WadoServicePort || $scope.pacs.desktopClient.param.WadoServicePort == '') {
                    $scope.alert("wado端口不能为空！");
                    return false;
                }
                if (!$scope.pacs.desktopClient.param.AETitle || $scope.pacs.desktopClient.param.AETitle == '') {
                    $scope.alert("AETitle不能为空！");
                    return false;
                }
            }
            if (!$scope.pacs.webClient.disabled) {
                if (!$scope.pacs.webClient.url || $scope.pacs.webClient.url == '') {
                    $scope.alert("URL不能为空！");
                    return false;
                }
            }
            return true;

        }
        //保存pacs 配置
        $scope.savePacsConfig = function () {
            if (($scope.pacs.desktopClient.disabled == true && $scope.pacs.webClient.disabled == true) || ($scope.pacs.desktopClient.disabled == false && $scope.pacs.webClient.disabled == false)) {
                $scope.alert("不能同时启用或停用示桌面配置和浏览器配置！");
                return;
            }
            if (validate())
            {
                if ($scope.strArgs) {
                    var argsList = $scope.strArgs.split("|");
                    $scope.pacs.desktopClient.args = argsList;
                    var jsonPacs = JSON.stringify($scope.pacs);
                    clientAgentService.editPacsConfig({ jsonPacs: $scope.pacs }).success(function (result) {
                        if (result) {
                            csdToaster.info('保存成功！');
                            InitData();
                        }
                    });
                }
            }          
        }

        ; +(function init() {
            $scope.pacs = null;
            $scope.pathPattern = /(^[a-zA-Z]:(((\\(?! )[^/:*?<>\""|\\]+)+\\?)|(\\)?)\s*$)/;
            $scope.ipPattern = /(^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$)/
            $scope.portPattern = /^([0-9]|[1-9]\d{1,3}|[1-5]\d{4}|6[0-5]{2}[0-3][0-5])$/;
            $scope.urlPattern = /^((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?)$/
            InitData();
        })()
    }])
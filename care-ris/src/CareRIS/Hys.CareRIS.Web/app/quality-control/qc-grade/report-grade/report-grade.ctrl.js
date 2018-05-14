qualitycontrolModule.controller('ReportGradeController', ['$log',
'$scope', 'qualityService', 'openDialog', '$translate', 'csdToaster', 'enums', 'clientAgentService', 'loginContext', 'busyRequestNotificationHub', '$window', 'sortArr',
function ($log, $scope, qualityService, openDialog, $translate, csdToaster, enums, clientAgentService, loginContext, busyRequestNotificationHub, $window, sortArr) {

    //初始化history.standards
    $scope.initHistory = function (history) {
        history.standards = [
            { desc: '描述合适;', selected: false },
            { desc: '无错别字或符号;', selected: false },
            { desc: '报告完整;', selected: false },
            { desc: '结论准确;', selected: false },
            { desc: '报告时完全复制;', selected: false }
        ];
    }

    $scope.reportHistory = function () {
        if ($scope.selectedItems.length < 1) {
            openDialog.openIconDialogOkFun(
                openDialog.NotifyMessageType.Warn,
                $translate.instant('Alert'),
                '请选中一行！'
            );
            return;
        }
        var firstItem = angular.copy($scope.selectedItems[0]);
        var accNo = firstItem.accNo;
        var tmpItem;
        for (var i = 1; i < $scope.selectedItems.length; ++i) {
            tmpItem = $scope.selectedItems[i];
            if (tmpItem.accNo !== accNo) {
                openDialog.openIconDialogOkFun(
                    openDialog.NotifyMessageType.Warn,
                    $translate.instant('Alert'),
                    '请选择放射编号相同的查看历史评分！');
                return;
            }
        }
        qualityService.getQcHistory(firstItem.reportID, 2).success(function (historys) {
            if (historys.length < 1) {
                openDialog.openIconDialog(
                    openDialog.NotifyMessageType.Warn,
                    $translate.instant('Alert'),
                    '没有历史评分！');
            } else {
                sortArr.sortArray(historys, 'desc', 'createDate');
                $scope.historys = historys;
                $scope.selectedHistory = historys[0];
                $scope.selectedHistory.selected = true;
                $scope.initHistory($scope.selectedHistory);
                _.each($scope.selectedHistory.standards, function (standard) {
                    standard.selected = $scope.selectedHistory.result.indexOf(standard.desc) >= 0;
                });
                for (var i = 0; i < $scope.historys.length; i++) {
                    $scope.historys[i].createDate = $scope.dateFormat($scope.historys[i].createDate);
                }
                $scope.reportHistoryWindow.open();
            }
        });

    };

    $scope.showHistory = function (history) {
        $scope.selectedHistory.selected = false;
        $scope.selectedHistory = history;
        history.selected = true;
        $scope.initHistory($scope.selectedHistory);
        _.each($scope.selectedHistory.standards, function (standard) {
            standard.selected = $scope.selectedHistory.result.indexOf(standard.desc) >= 0;
        });
    }

    $scope.setScore = function () {
        if (!$scope.selectedItems || $scope.selectedItems.length < 1) return;

        var activeItem = $scope.selectedItems[0];
        if (!activeItem.reportQuality2) {
            $scope.reportScore.reset();
            return;
        }

        var selectedStandard = ',' + activeItem.reportQuality2 + ',';
        _.each($scope.reportScore.standards, function (standard) {
            var curr = ',' + standard.id + ',';
            standard.selected = selectedStandard.indexOf(curr) >= 0;
        });
        $scope.reportScore.result.accordRate = activeItem.accordRate;
        $scope.reportScore.result.reportQualityComments = activeItem.reportQualityComments;
        $scope.reportScore.calc();
    };
    $scope.reportSoreChanged = function () {
        $scope.reportScore.calc();
    };

    $scope.viewReport = function () {
        if ($scope.selectedItems.length < 1) return;
        var firstItem = $scope.selectedItems[0];

        var params = {
            id: firstItem.procedureID,
            site: loginContext.site,
            domain: loginContext.domain,
            url: loginContext.apiHost + '/api/v1/report/getreportviewerbyprocedureid2',
            printtemplateid: firstItem.printTemplateID
        };
        busyRequestNotificationHub.requestStarted();
        var win = $window.open('', 'reportWindow', 'fullscreen=yes,location=no,menubar=no,status=no,titlebar=no,toolbar=no');
        clientAgentService.previewReport(params).success(function (data) {
            win.document.write(data);
        }).error(function (error) {
            $log.error(error);
        }).finally(function () {
            busyRequestNotificationHub.requestEnded();
        });
    };

    $scope.reportGrade = function () {
        if ($scope.selectedItems.length < 1) {
            openDialog.openIconDialogOkFun(
                openDialog.NotifyMessageType.Warn,
                $translate.instant('Alert'),
                '请选中一行！'
            );
            return;
        }
        var firstItem = angular.copy($scope.selectedItems[0]);
        var accNo = firstItem.accNo;
        var reportIDs = [firstItem.reportID];
        var tmpItem;
        for (var i = 1; i < $scope.selectedItems.length; ++i) {
            tmpItem = $scope.selectedItems[i];
            if (tmpItem.accNo !== accNo) {
                openDialog.openIconDialogOkFun(
                    openDialog.NotifyMessageType.Warn,
                    $translate.instant('Alert'),
                    '请选择放射编号相同的行进行评分！');
                return;
            }
            reportIDs.push(tmpItem.reportID);
        }

        if (!$scope.reportScore.result.selectedStandards && !$scope.reportScore.result.desc) {
            openDialog.openIconDialogOkFun(
                    openDialog.NotifyMessageType.Warn,
                    $translate.instant('Alert'),
                    '请为报告打分！');
            return;
        }

        firstItem.scoringType = 2;
        firstItem.reportIDs = reportIDs;
        firstItem.reportQuality2 = $scope.reportScore.result.selectedStandards;
        firstItem.reportQuality = $scope.reportScore.result.desc;
        firstItem.reportQualityComments = $scope.reportScore.result.reportQualityComments;
        firstItem.accordRate = $scope.reportScore.result.accordRate;
        firstItem.resultItem = $scope.reportScore.result.selectedItemXml;
        firstItem.scoringVersion = $scope.reportScore.version;

        qualityService.saveScoring(firstItem).success(function () {
            _.each($scope.selectedItems, function (item) {
                item.reportQuality2 = firstItem.reportQuality2;
                item.reportQuality = firstItem.reportQuality;
                item.reportQualityComments = firstItem.reportQualityComments;
                item.accordRate = firstItem.accordRate;
            });
            csdToaster.info('评分成功！');
        }).error(function () {
            csdToaster.warning('系统出现问题，请重新登录系统再评分！');
        });
    };

    $scope.reportScore = {
        version: 'Report1',
        standards: [
            { id: 0, desc: '描述合适;', score: 25, selected: false },
            { id: 1, desc: '无错别字或符号;', score: 25, selected: false },
            { id: 2, desc: '报告完整;', score: 25, selected: false },
            { id: 3, desc: '结论准确;', score: 25, selected: false },
            { id: 4, desc: '报告时完全复制;', score: -1000, selected: false }
        ],
        measure: {
            bad: {
                min: 0,
                max: 49,
                desc: 'D'
            },
            middle: {
                min: 50,
                max: 69,
                desc: 'C'
            },
            good: {
                min: 70,
                max: 89,
                desc: 'B'
            },
            excellent: {
                min: 90,
                max: 100,
                desc: 'A'
            },
            unkown: {
                desc: '未确定'
            }
        },
        calc: function () {
            var score = 0;
            var selectedStandards = [];
            var selectedStandardText = [];
            _.each($scope.reportScore.standards, function (item) {
                if (item.selected) {
                    score += item.score;
                    selectedStandards.push(item.id);
                    selectedStandardText.push(item.desc);
                }
            });
            score > 100 && (score = 100);
            score < 0 && (score = 0);

            var calcItem = null;
            for (var attr in $scope.reportScore.measure) {
                calcItem = $scope.reportScore.measure[attr];
                if (score <= calcItem.max && score >= calcItem.min) {
                    break;
                }
            }
            var tempXml = selectedStandardText.join('</Item><Item>');
            if (tempXml) {
                tempXml = '<Item>' + tempXml + '</Item>';
            }
            calcItem || (calcItem = $scope.reportScore.measure.unkown);
            $scope.reportScore.result.score = score;
            $scope.reportScore.result.desc = calcItem.desc;
            $scope.reportScore.result.selectedStandards = selectedStandards.join(',');
            $scope.reportScore.result.selectedItemXml = tempXml ? '<Items>' + tempXml + '</Items>' : '';
        },
        reset: function () {
            $scope.reportScore.result.score = 0;
            $scope.reportScore.result.desc = '';
            $scope.reportScore.result.accordRate = '';
            $scope.reportScore.result.reportQualityComments = '';
            $scope.reportScore.result.selectedStandards = '';
            $scope.reportScore.result.selectedItemXml = '';
            _.each($scope.reportScore.standards, function (item) {
                item.selected = false;
            });
        },
        result: {
            accordRate: '',
            score: 0,
            desc: '',
            reportQualityComments: '',
            selectedStandards: '',
            selectedItemXml: ''
        }
    };

    // 日期转换
    $scope.dateFormat = function (string) {
        var date = new Date(string);
        var year = date.getFullYear();
        var month = date.getMonth() + 1;
        var day = date.getDate();
        var hour = date.getHours();
        var min = date.getMinutes();
        if (hour < 10) {
            hour = '0' + hour;
        }
        if (min < 10) {
            min = '0' + min;
        }
        date = year + '-' + month + '-' + day + ' ' + hour + ':' + min;
        return date;
    };
    +(function init() {
        $scope.canReport = false;
        $scope.showReportEnabled = false;
        $scope.historys = [];
        $scope.history = {
            standards: [
                { desc: '患者信息、标记和检查部位正确;', selected: false },
                { desc: '体位采集范围标准;', selected: false },
                { desc: '图像质量满足诊断要求(批度、清晰度、失真度，无异物、伪影等);', selected: false },
                { desc: '造影和特检解决临床需求;', selected: false },
                { desc: '无法判断;', selected: false }
            ]
        }
        $scope.accordRateList = [{ value: '不符合' }, { value: '部分符合' }, { value: '符合' }, { value: '基本符合' }, { value: '完全符合' }]
        $scope.selectedItems = [];
        $scope.$on('event:QCSelectedItemChanged', function (e, selectedItems) {
            $scope.selectedItems = selectedItems;
            var firstItem = $scope.selectedItems[0];
            $scope.canReport = firstItem.status === 110 || firstItem.status === 120;
            $scope.showReportEnabled = firstItem.status >= enums.rpStatus.examination;
            $scope.setScore();
        });
    })();
}
]);
qualitycontrolModule.controller('ImageGradeController', [
'$scope', 'qualityService', 'openDialog', '$translate', 'csdToaster', 'enums', 'application', 'reportService', '$window', 'sortArr', 'clientAgentService',
function ($scope, qualityService, openDialog, $translate, csdToaster, enums, application, reportService, $window, sortArr, clientAgentService) {

    //初始化history
    $scope.initHistory = function (history) {
        history.standards = [
            { desc: '患者信息、标记和检查部位正确;', selected: false },
            { desc: '体位采集范围标准;', selected: false },
            { desc: '图像质量满足诊断要求(批度、清晰度、失真度，无异物、伪影等);', selected: false },
            { desc: '造影和特检解决临床需求;', selected: false },
            { desc: '无法判断;', selected: false }
        ];
    }

    //查看历史评分
    $scope.imageHistory = function () {
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
        qualityService.getQcHistory(firstItem.procedureID, 1).success(function (historys) {
            if (historys.length < 1) {
                openDialog.openIconDialog(
                    openDialog.NotifyMessageType.Warn,
                    $translate.instant('Alert'),
                    '没有历史评分！');
            } else {
                sortArr.sortArray(historys, 'desc', 'createDate');
                //historys.sort(function(a, b){return a.createDate < b.createDate})
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
                $scope.imageHistoryWindow.open();
            }
        })
    };

    //查看历史评分详细信息
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
        if (!activeItem.result3) {
            $scope.imageScore.reset();
            return;
        }

        var selectedStandard = ',' + activeItem.result3 + ',';
        _.each($scope.imageScore.standards, function (standard) {
            var curr = ',' + standard.id + ',';
            standard.selected = selectedStandard.indexOf(curr) >= 0;
        });
        $scope.imageScore.result.comment = activeItem.comment;
        $scope.imageScore.calc();
    };
    $scope.imageSoreChanged = function () {
        $scope.imageScore.calc();
    };
    $scope.imageGrade = function () {
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
        var procedIDs = [firstItem.procedureID];
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
            procedIDs.push(tmpItem.procedureID);
        }

        if (!$scope.imageScore.result.selectedStandards && !$scope.imageScore.result.desc) {
            openDialog.openIconDialogOkFun(
                    openDialog.NotifyMessageType.Warn,
                    $translate.instant('Alert'),
                    '请为图像打分！');
            return;
        }

        firstItem.scoringType = 1;
        firstItem.procedIDs = procedIDs;
        firstItem.result3 = $scope.imageScore.result.selectedStandards;
        firstItem.result = $scope.imageScore.result.desc;
        firstItem.comment = $scope.imageScore.result.comment;
        firstItem.resultItem = $scope.imageScore.result.selectedItemXml;
        firstItem.Result2 = $scope.imageScore.version;

        qualityService.saveScoring(firstItem).success(function () {
            _.each($scope.selectedItems, function (item) {
                item.result3 = firstItem.result3;
                item.result = firstItem.result;
                item.comment = firstItem.comment;
            });
            csdToaster.info('评分成功！');
        }).error(function () {
            csdToaster.warning('系统出现问题，请重新登录系统再评分！');
        });
    };

    $scope.viewImage = function () {
        if ($scope.selectedItems.length < 1) return;
        var order = $scope.selectedItems[0];

        var parameter = {
            patientId: order.patientNo,
            accNo: order.accNo,
            studyId: order.studyInstanceUID
        };
        clientAgentService.
            viewImage(parameter).
            success(function (result) {
                if (!result) {
                    csdToaster.warning('不能查看图像，请检查PACS配置！');
                }
            });
    };

    $scope.imageScore = {
        version: 'Image1',
        standards: [
            { id: 0, desc: '患者信息、标记和检查部位正确;', score: 25, selected: false },
            { id: 1, desc: '体位采集范围标准;', score: 25, selected: false },
            { id: 2, desc: '图像质量满足诊断要求(批度、清晰度、失真度，无异物、伪影等);', score: 25, selected: false },
            { id: 3, desc: '造影和特检解决临床需求;', score: 25, selected: false },
            { id: 4, desc: '无法判断;', score: -1000, selected: false }
        ],
        measure: {
            bad: {
                min: 0,
                max: 49,
                desc: '不合格片'
            },
            good: {
                min: 50,
                max: 79,
                desc: '合格片'
            },
            excellent: {
                min: 80,
                max: 100,
                desc: '优质片'
            },
            unkown: {
                desc: '未确定'
            }
        },
        calc: function () {
            var score = 0;
            var selectedStandards = [];
            var selectedStandardText = [];
            _.each($scope.imageScore.standards, function (item) {
                if (item.selected) {
                    score += item.score;
                    selectedStandards.push(item.id);
                    selectedStandardText.push(item.desc);
                }
            });
            score > 100 && (score = 100);
            score < 0 && (score = 0);

            var calcItem = null;
            for (var attr in $scope.imageScore.measure) {
                calcItem = $scope.imageScore.measure[attr];
                if (score <= calcItem.max && score >= calcItem.min) {
                    break;
                }
            }
            var tempXml = selectedStandardText.join('</Item><Item>');
            if (tempXml) {
                tempXml = '<Item>' + tempXml + '</Item>';
            }
            calcItem || (calcItem = $scope.imageScore.measure.unkown);
            $scope.imageScore.result.score = score;
            $scope.imageScore.result.desc = calcItem.desc;
            $scope.imageScore.result.selectedStandards = selectedStandards.join(',');
            $scope.imageScore.result.selectedItemXml = tempXml ? '<Items>' + tempXml + '</Items>' : '';
        },
        reset: function () {
            $scope.imageScore.result.score = 0;
            $scope.imageScore.result.desc = '';
            $scope.imageScore.result.comment = '';
            $scope.imageScore.result.selectedStandards = '';
            $scope.imageScore.result.selectedItemXml = '';
            _.each($scope.imageScore.standards, function (item) {
                item.selected = false;
            });
        },
        result: {
            score: 0,
            desc: '',
            comment: '',
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
    }

    ; +(function init() {
        $scope.historys = [];
        $scope.viewImageEnabled = false;
        $scope.history = {
            standards: [
                { desc: '患者信息、标记和检查部位正确;', selected: false },
                { desc: '体位采集范围标准;', selected: false },
                { desc: '图像质量满足诊断要求(批度、清晰度、失真度，无异物、伪影等);', selected: false },
                { desc: '造影和特检解决临床需求;', selected: false },
                { desc: '无法判断;', selected: false }
            ]
        }
        $scope.selectedItems = [];
        $scope.$on('event:QCSelectedItemChanged', function (e, selectedItems) {
            $scope.selectedItems = selectedItems;
            var firstItem = selectedItems[0];
            $scope.viewImageEnabled = !!firstItem.studyInstanceUID;
            $scope.setScore();
        });
    })()
}
]);
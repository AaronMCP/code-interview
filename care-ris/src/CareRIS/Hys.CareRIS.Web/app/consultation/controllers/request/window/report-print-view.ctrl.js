consultationModule.controller('ReportPrintViewController', ['$log', '$scope', 'consultationService',
    '$stateParams', '$state', '$timeout', '$translate', 'openDialog', '$rootScope', '$sce', 'loginContext', '$http', '$window', 'clientAgentService', 'csdToaster',
    '$filter',
    function ($log, $scope, consultationService, $stateParams, $state, $timeout,
        $translate, openDialog, $rootScope, $sce, loginContext, $http, $window, clientAgentService, csdToaster, $filter) {
        'use strict';
        $log.debug('ReportPrintViewController.ctor()...');

        $scope.printReport = function () {

            $scope.showPrintTime = true;
            $scope.currentTime = $filter('date')(Date.now(), 'yyyy/MM/dd HH:mm');

            $timeout(function () {
                var hegiht = $('#report').height();
                var printcontent = $('#report').clone();

                printcontent.css("height", hegiht);

                var params = [
                    'height=' + hegiht,
                    'width=' + screen.width,
                    'fullscreen=yes'
                    // only works in IE, but here for completeness
                ].join(',');
                var mywindow = window.open('', 'report', params);
                mywindow.document.write('<html><head><title></title>');
                /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
                mywindow.document.write('</head><body >');
                mywindow.document.write(printcontent[0].outerHTML);
                mywindow.document.write('</body></html>');

                mywindow.document.close();
                // necessary for IE >= 10
                mywindow.focus();
                // necessary for IE >= 10

                mywindow.print();
                mywindow.close();
                $scope.showPrintTime = false;
            }, 10);

            
        };

        $scope.exportWord = function () {
            $('#btnPrint').focus();
            $('#spanWriter').hide();
            var sel;
            if (window.getSelection)
            {
                sel = window.getSelection();
                if(sel.rangeCount)
                {
                    var range = sel.getRangeAt(0);
                    var insertedSpan = document.getElementById('report');
                    try {
                    range.selectNodeContents(insertedSpan);
                    //range.collapse(false);
                    sel.removeAllRanges();
                    sel.addRange(range);
                    } catch (e) {
                        csdToaster.pop('info', $translate.instant("ExportReportWordSelectError"), '');
                        return;
                    }

                    document.execCommand('copy');
                }
                
            }
            else if (document.body.createTextRange)
            {
                sel = document.body.createTextRange();
                try {
                    sel.moveToElementText(report);
                } catch (e) {
                    csdToaster.pop('info', $translate.instant("ExportReportWordSelectError"), '');
                    return;
                }
                sel.select();
                sel.execCommand("Copy");

            }
           
            clientAgentService.openConvertWord().success(function (openResult) {
                if (openResult == 2) {
                    csdToaster.pop('info', $translate.instant("ExportReportWordError"), '');
                }
            }).error(function (errorMsg) {
               
                csdToaster.pop('info', $translate.instant("ExportReportWordError"), '');
            });

            $timeout(function () {
                $('#spanWriter').show();
                if (window.getSelection) {
                    sel.removeAllRanges();
                } else if (document.body.createTextRange) {
                    sel.moveToElementText();
                    sel.select();
                }
            }, 10);
        };

        $scope.getExportPath = function () {
            var shell = new ActiveXObject("Shell.Application");
            var folder = shell.BrowseForFolder(0, '请选择存储目录', 0x0040, 0x11);
            var filePath;
            if (folder != null) {
                filePath = folder.Items().Item().Path;
            }
            return filePath;
        };

        (function initialize() {
            $scope.report = {};
            $scope.requestID = $scope.rid;
            $scope.showPrintTime = false;
            $scope.currentTime = $filter('date')(Date.now(), 'yyyy/MM/dd HH:mm');
            consultationService.getConsultationDetailAsync($scope.requestID).success(function (data) {
                $scope.report = data;
                var ageArry = $scope.report.currentAge.split(' ');
                if (ageArry.length > 1) {
                    var age = ageArry[0];
                    var ageUnit = ageArry[1];
                    var a = _.findWhere($rootScope.ageUnitList, { value: ageUnit });
                    if (a) {
                        ageUnit = a.text;
                    }
                    $scope.report.currentAge = age + ' ' + ageUnit;
                }

                $scope.report.requestPurpose = $sce.trustAsHtml($scope.report.requestPurpose);
                $scope.report.requestRequirement = $sce.trustAsHtml($scope.report.requestRequirement);
                $scope.report.consultationAdvice = $sce.trustAsHtml($scope.report.consultationAdvice);
                $scope.report.consultationRemark = $sce.trustAsHtml($scope.report.consultationRemark);
            });

            $('#btnPrint').focus();
        }());
    }
]);
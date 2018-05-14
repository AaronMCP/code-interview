reportModule.directive('reportEditView', ['$log', '$timeout', 'risDialog', '$translate', '$filter', '$sce',
function ($log, $timeout, risDialog, $translate, $filter, $sce) {
    $log.debug('reportEditView.ctor()...');
    return {
        restrict: 'E',
        templateUrl: '/app/report/views/report-edit-view.html',
        controller: 'ReportEditController',
        scope: {
            reportId: '@',
            orderId: '@',
            procedureId: '@',
            isRead: '@',
            patientCaseOrderId: '@',
            from:'@'
        },

        link: function (scope, element) {
            var $e = $(element);

            var reTemplate = $e.find('.re-template');
            var reContentOperator = $e.find('.re-content-operator');
            var reContentBody = $e.find('.re-content-body');
            var reTemplateToolbar = $e.find('.re-content-operator-toolbar .content');
            scope.reContentOperatorMinimized = false;
            scope.tipInfo = '查看登记检查信息';
            scope.flag = true;

            $e.find('.re-template-toggle,.re-template-close').on('click', function () {
                reTemplate.fadeToggle();
                $timeout(function () {
                    if (scope.flag) {
                        scope.tipInfo = '查看报告信息';
                        scope.flag = !scope.flag;
                    } else {
                        scope.tipInfo = '查看登记检查信息';
                        scope.flag = !scope.flag;
                    }

                });
            });
            $e.find('.re-operator-switcher').on('click', function () {
                reContentOperator.animate({ width: scope.reContentOperatorMinimized ? 300 : 43 }, 300);
                scope.$apply(function () {
                    scope.reContentOperatorMinimized = !scope.reContentOperatorMinimized;
                });
                reContentBody.animate({ left: scope.reContentOperatorMinimized ? 40 : 300 });
                if (scope.reContentOperatorMinimized) {
                    reTemplateToolbar.hide();
                } else {
                    reTemplateToolbar.show();
                }
            });
                
            $e.on('click', '.dropdown-menu li.disabled>a,.dropdown-menu li a.disabled', function (e) {
                e.stopPropagation();
                window.event.cancelBubble = true;
            });
            var onResize = function () {

            };

            var toggleBaseInfoHtml = function (isShowDesc) {
                if (isShowDesc) {
                    $('#tdBaseInfo').hide();
                    $('#tdBaseInfoDesc').show();
                }
                else {
                    $('#tdBaseInfoDesc').hide();
                    $('#tdBaseInfo').show();
                }
            };

            var closeAllPopover = function () {
                $('.ris-popover').popover('hide');
                $('.report-preview-content-popover').remove();
            };

            var showPopover = function (id) {
                $(document.getElementById(id)).popover('show');
            };

            var closePopover = function () {
                $(document.getElementById(scope.selectReportTemplateDirID)).popover('hide');
                scope.selectReportTemplateDirID = '';
            };

            scope.filterStatus = function (item) {
                var template = kendo.template($('#consultationStatus').html());
                var value;
                if (item.isDeleted) {
                    value = $translate.instant('Deleted');
                }
                else {
                    var status = $filter('enumValueToString')(item.status, 'consultationRequestStatus');
                    value = $translate.instant(status);
                }
                var ele = $(template(item)).attr('title', value).html('<b class="consultation-result-status">' + value + '</b>').prop('outerHTML');
                return $sce.trustAsHtml(ele);
            };

            scope.onResize = onResize;
            scope.toggleBaseInfoHtml = toggleBaseInfoHtml;
            scope.closeAllPopover = closeAllPopover;
            scope.showPopover = showPopover;
            scope.closePopover = closePopover;

            $(window).resize(onResize);
            onResize();
        }
    };
}]);

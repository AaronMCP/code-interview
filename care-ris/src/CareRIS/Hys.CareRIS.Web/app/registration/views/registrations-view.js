registrationModule.directive('registrationsView', ['$log', '$timeout', function ($log, $timeout) {
    $log.debug('registrationsView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/registration/views/registrations-view.html',
        controller: 'RegistrationsController',
        scope: {},
        link: function (scope, element) {
            var $e = $(element);
            var $orderTable = $e.find('#orderTable');
            var $mainContent = $e.find('.content-container');
            var $tableContainer = $e.find('#tableContainer');
            var registrationsOpNavbar = $e.find('.registrations-op-navbar');

            scope.closeProcedure = function () {
                $timeout(function () {
                    if (scope.flag) {
                        scope.flag = false;
                    }
                })          
            }
            var onResize = function () {
                var registrationsOpNavbarWidth = registrationsOpNavbar.width();
                var minWidth = 955;

                scope.$apply(function() {
                    scope.collapseOpNavbar = registrationsOpNavbarWidth < minWidth;
                });
                
                $timeout(function () {
                    var newHeight = $mainContent.height() - 105;
                    if (newHeight > 0) {
                        $tableContainer.height(newHeight);
                        $orderTable.floatThead('reflow');
                        // fix bug in IE
                        $timeout(function () {
                            $orderTable.floatThead('reflow');
                        }, 100);
                    }
                }, 100);
            };
            $e.on('click', '.dropdown-menu li.disabled>a', function (e) {
                e.stopPropagation();
            });
            // close all popovers when scrolling table
            $tableContainer.on('scroll', function () {
                $tableContainer.find('.ris-popover').popover('hide');
            });

            // if windows size changed, should re-calculate column size of order float table
            $(window).resize(onResize);

            // if order rows generated, should re-calculate column size of order float table
            scope.$on('orderItems:RepeatDone', function () {
                onResize();
            });

            // if procedure rows generated, should re-calculate column size of procedure float table
            scope.$on('patientProcedures:RepeatDone', function (e) {
                if (e) {
                    $(e).parents('#patientProceduresTable').floatThead('reflow');
                }
            });

            scope.$on('event:sidePanelResize', onResize);
        }
    };
}]);
/*
 * @description:
 * expand detail panel for a table row
 * 
 * @usage:
 *  <table data-template-url="app/insurance/views/patient-claims-detail-view.html" 
 *     [data-ris-table-expand="single"] [data-onload="loadClaimDetail"] [data-apply-on="PatientClaim:RepeatDone"]>
 * 
 */

commonModule.directive('risTableExpand', ['$http', '$templateCache', '$compile', '$timeout',
    function ($http, $templateCache, $compile, $timeout) {
        'use strict';

        return {
            restrict: 'A',
            link: function (scope, element, attr) {

                var scrollIntoView = function (id) {
                    if ($('#' + id).prev().length > 0) {
                        $('#' + id).prev()[0].scrollIntoView();
                    } else {
                    }
                }

                var apply = function () {
                    // gernarate uniq id for each tr element except header
                    // and remove before attach click handler
                    element.find("tbody > tr").uniqueId().off('click', expand).on('click', expand);
                }

                var expand = function () {
                    var self = this;
                    var $this = $(this);
                    var id = $this.attr('id');

                    // handle with just open one row.
                    if (attr.risTableExpand === "single") {
                        // close other opend row
                        element.find('.csd-expanded-row').not('.' + $this.attr('id')).hide();
                        element.find('.csd-selected-row').not('#' + $this.attr('id')).removeClass('csd-selected-row');
                    }

                    // find expand detail
                    var detail = element.find('.' + id);
                    // if can't find then init the detail panel
                    if (!detail.length) {
                        // find click row scope by element
                        var rowScope = angular.element(self).scope();
                        // attach hide fn
                        rowScope.hide = function () { $this.trigger('click'); }
                        // execute pre data load fn if existing
                        attr.onload && scope[attr.onload](rowScope);
                        // load and cache template
                        $http.get(attr.templateUrl, { cache: true }).success(function (template) {
                            // complie template with row scope
                            var rowDetail = $compile(template)(rowScope);
                            // insert after row
                            $this.addClass('csd-selected-row').after(rowDetail.addClass('csd-expanded-row ' + id));
                            scrollIntoView(id);
                        });
                    } else {
                        // already existing, normaly toggle open and close
                        detail.toggle('fast');
                        $this.toggleClass('csd-selected-row');

                        if (detail.is(":visible")) {
                            scrollIntoView(id);
                        }
                    }

                    scope.$emit('table-row-expanded');
                }

                // apply fn by event or default [table renderd]
                if (attr.applyOn) {
                    scope.$on(attr.applyOn, apply);
                } else {
                    $timeout(apply, 500);
                }
            }
        };
    }]);
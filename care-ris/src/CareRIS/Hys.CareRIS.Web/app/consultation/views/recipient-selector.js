consultationModule.directive('recipientSelector', ['$log','enums', function ($log,enums) {
    $log.debug('recipientSelector.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/recipient-selector.html',
        controller: 'RecipientSelectorController',
        replace: true,
        scope: {
            onSelected: '&?',
            selection: '=',
            pageSize: '=?',
            mode: '@?'
        },
        link: function (scope, element) {
            scope.hidePopover = function() {
                $('.ris-popover').popover('hide');
            };
            
            scope.expertDataBound = function () {
                if (scope.selection.length > 0) {
                    if (scope.selection[0].type === enums.consultantType.expert) {
                        var dataItems = $("#lvExpertList").data("kendoListView").dataItems();
                        _.each(scope.selection, function (item) {
                            var dataItem = _.findWhere(dataItems, { uniqueID: item.value });
                            if (dataItem && dataItem.uid) {
                                scope.selectItemInlistView(dataItem.uid, true);
                            }
                        });
                    }
                }
            };

            scope.hospitalDataBound = function () {
                if (scope.selection.length > 0) {
                    if (scope.selection[0].type === enums.consultantType.center) {
                        var listView = $("#lvHospitalList").data("kendoListView");
                        var item = _.findWhere(listView.dataItems(), { uniqueID: scope.selection[0].value });
                        item && listView.select(listView.element.children("[data-uid='" + item.uid + "']"));
                    } 
                }
            };

            scope.clearSelection = function (filter) {
                var listView = $(filter).data("kendoListView");
                _.each(listView.element.children(), function (ele) {
                    $(ele).removeClass('k-state-selected');
                });
            };
            //k-state-selected
            scope.selectItemInlistView=function(uid,selected) {
                var listView = $("#lvExpertList").data("kendoListView");
                // selects  list view item
                if (selected) {
                    listView.element.find('[data-uid="' + uid + '"]').addClass('k-state-selected');
                } else {
                    listView.element.find('[data-uid="' + uid + '"]').removeClass('k-state-selected');
                }
            }
        }
    };
}]);

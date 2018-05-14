worklistModule.directive('timeSliceView', [
function () {
    'use strict';
    return {
        restrict: 'E',
        templateUrl:'/app/worklist/booking/timeslice/views/time-slice-view.html',
        controller: 'TimeSliceController',
        scope: {
            option: '=?'
        },
        replace: true,
        link: function (scope, element) {
            element.on('click', '.time-slice-event', function () {
                var ts = $(this);
                if (ts.is('.disabled')) {
                    return;
                }
                var dataItem = ts.scope().dataItem;
                var selectedElement = element.find('.time-slice-event.selected');
                var selectedDataItem = selectedElement.size() > 0 ? selectedElement.scope().dataItem : null;

                var timeSlice = JSON.parse(dataItem.customData.timeSlice || '{}');
                scope.$apply(function () {
                    selectedDataItem && (selectedDataItem.customData.selected = false);
                    dataItem.customData.selected = true;
                    scope.timeSliceStart = dataItem.start;
                    scope.timeSliceEnd = dataItem.end;
                    scope.timeSlice = timeSlice;
                    scope.$emit('event:timesliceSelected');
                });
            });
        }
    }
}
]);
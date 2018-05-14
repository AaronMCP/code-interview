commonModule.filter('dateToImage', ['$filter', 'enums', function ($filter, enums) {
    'use strict';
    return function (input) {
        var result = '';

        if (input) {
            var dateTime = input.consultationDate ? input.consultationDate : input.expectedDate;
            var timeRage = input.consultationStartTime ? input.consultationStartTime : input.expectedTimeRange;

            if (dateTime) {
                var d = kendo.toString(kendo.parseDate(dateTime), 'yyyy/MM/dd dddd').split(' ');
                result = d[0] + '<br/>' + $filter('translate')(d[1]);
                if (timeRage) {
                    switch (timeRage) {
                        case enums.ConsultationTimeRange.Morning:
                            result += '<span class="icon-general icon-morning icon-orange" title="' + $filter('translate')('Morning') + '"></span>';
                            break;
                        case enums.ConsultationTimeRange.Afternoon:
                            result += '<span class="icon-general icon-afternoon icon-orange" title="' + $filter('translate')('Afternoon') + '"></span>';
                            break;
                        case enums.ConsultationTimeRange.Night:
                            result += '<span class="icon-general icon-night icon-blue" title="' + $filter('translate')('Night') + '"></span>';
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        return result;
    };
}]);

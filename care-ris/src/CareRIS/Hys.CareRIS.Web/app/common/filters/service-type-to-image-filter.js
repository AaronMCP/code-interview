commonModule.filter('serviceTypeToImage', [function () {
    'use strict';
    return function (input) {
        var result = '';
        if (input) {
            if (input.serviceTypeID) {
                result = '<span class="';

                //TODO '1' 
                if (input.serviceTypeID === '1') {
                    result += 'icon-generalMeeting';
                } else {
                    result += 'icon-urgentMeeting';
                }

                if (input.serviceTypeName) {
                    result += ' icon-lightgreen" title="' + input.serviceTypeName + '"></span>' + input.serviceTypeName;
                } else {
                    result += ' icon-lightgreen"></span>';
                }
            }
        }

        return result;
    };
}]);

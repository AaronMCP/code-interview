commonModule.filter('genderToImage', ['$sce', function ($sce) {
    'use strict';
    return function (input) {
        if (input) {
            var result = input + '&nbsp;&nbsp;<span href="javascript:void(0);" class="';
            if (input === '男') {
                result += 'icon-male icon-blue';
            } else if (input === '女') {
                result += 'icon-female icon-purple';
            } else {
                result += 'icon-unknown icon-orange';
            }
            result += '" style="font-size:20px"></span>';
            return $sce.trustAsHtml(result);
        }

        return '';
    };
}]);

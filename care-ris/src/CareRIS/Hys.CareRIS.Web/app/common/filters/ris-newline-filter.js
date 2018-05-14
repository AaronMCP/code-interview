commonModule.filter("newline", ["enums", function () {
    'use strict';
    return function (text) {
        return text ? text.replace(/\n|\r/g, '<br/>') : text;
    }
}]);

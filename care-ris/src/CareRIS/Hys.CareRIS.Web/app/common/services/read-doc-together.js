commonModule.factory('readDocTogether', ['$log',
    function ($log) {
        'use strict';
        return {
            item: null,
            orderID: null,
            order: null,
            patient: null,
            caller: '',
            associatedReport: false,
            getProcedure: function () {
                if (angular.isArray(this.item) && this.item.length > 0) {
                    return this.item[0];
                } else {
                    return this.item;
                }
            }
        };
    }
]);
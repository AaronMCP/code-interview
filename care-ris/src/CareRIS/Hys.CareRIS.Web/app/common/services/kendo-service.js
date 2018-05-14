commonModule.factory('kendoService', ['$log', function ($log) {
    'use strict';

    var destroyList = [];
    return {
        grid: function (grid) {
           return {
               autoResize: function () {
                   function onResize() {
                       $(grid).data("kendoGrid").resize();
                   }
                   $(window).resize(onResize).on('CUSTOME-RESIZE', onResize);

                   destroyList.push(function () {
                       $(window).off('resize', onResize).off('CUSTOME-RESIZE', onResize);
                   });
               },
           } 
        },
        destroy: function () {
            while (destroyList.length) {
                try {
                    destroyList.shift().call();
                } catch (e) {
                    $log.warn('failed on destroy kendoService -> ' + e);
                }
            }
        }
    }
}]);
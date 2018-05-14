configurationModule.controller('DoctorManagementCtrl', ['$scope', '$state', '$log', function ($scope, $state, $log) {
    'use strict';

    $log.debug('DoctorManagementCtrl.ctor()...');
    $('#doctor-management-grid').kendoGrid({
        columns: [{
            field: 'doctor',
            title: ''
        }, {
            field: 'a',
            title: '',
            temolate: "<select><option value='08:00-17:00'>08:00-17:00</option></select>"
        }, {
            field: 'b',
            title: ''
        }, {
            field: 'c',
            title: ''
        }, {
            field: 'd',
            title: ''
        }, {
            field: 'e',
            title: ''
        }, {
            field: 'f',
            title: ''
        }, {
            field: 'g',
            title: ''
        }],
        dataSource: [
            { doctor: '郑涵和', a: "" }
        ]
    });

    // work-modal配置
    $("#work-modal").kendoWindow({
        visible: false,
        width: 430,
        height: 380,
        position: { top: 150, left: '40%' },
        title: '排班时间设置'
    });

    var timeModal = $("#work-modal").data("kendoWindow");

    $scope.workTimeSet = function () {
        timeModal.open();
    }

    // workGrid配置
    $('#workGrid').kendoGrid({
        columns: [{
            field: 'startTime',
            title: '开始时间'
        }, {
            field: 'endTime',
            title: '结束时间'
        }, {
            field: 'alias',
            title: '别名'
        }, {
            field: 'morrow',
            title: '是否次日'
        }],
        dataSource: [
            { startTime: '08:00', endTime: '21:00', alias: '', morrow: '是' },
            { startTime: '17:00', endTime: '21:00', alias: '', morrow: '否' }
        ]
    });
}])
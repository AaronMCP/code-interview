commonModule.directive('risReaddocTogether', ['$log', 'readDocTogether', '$compile', 'clientAgentService','application',
    function ($log, readDocTogether, $compile, clientAgentService, application) {
        'use strict';
        return {
            restrict: 'A',
            link: function (scope, element) {
                element.on('click', function () {
                    var procedure = readDocTogether.getProcedure();
                    var pObj = {};
                    var order = readDocTogether.order;
                    if (order) {
                        pObj.patientID = order.patientID;
                        pObj.patientName = order.patientName;
                        pObj.accNo = order.accNo;
                    }
                    if (readDocTogether.patient) {
                        pObj.patientID = readDocTogether.patient.PatientID;
                        pObj.patientName = readDocTogether.patient.PatientName;
                        pObj.accNo = readDocTogether.patient.AccNo;
                        readDocTogether.patient = null;
                    }
                    var data = {
                        accNo: pObj.accNo,
                        procedureGuid: procedure.procedureID,
                        reportGuid: procedure.reportID,
                        patientID: pObj.patientID,
                        patientName: pObj.patientName,
                        modalityType: procedure.modalityType,
                        examSystem: procedure.examSystem,
                        rpStatus: procedure.status,
                        associatedReport: readDocTogether.associatedReport,
                        webMsg: JSON.stringify({
                            callMethod: readDocTogether.caller,
                            item: readDocTogether.item,
                            orderId: readDocTogether.orderID
                        })
                    };

                    data.accNo = order.accNo;
                    data.patientName = order.patientName;
                    data.patientID = order.patientNo;
                    var status = _.findWhere(application.configuration.statusList, {
                        value: procedure.status + ''
                    });
                    if (status) {
                        data.rpStatus = status.text;
                    }
                    clientAgentService.coViewInvitation(data);
                });
            }
        };
    }
]);
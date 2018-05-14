// registration web service proxy
webservices.factory('registrationService', ['$http', 'apiConfig', function ($http, apiConfig) {
    'use strict';

    return {
        searchPatient: function (patientName) {
            var config = { params: { patientName: patientName }, isBusyRequest: true };
            return $http.get('/registration/patients/search', apiConfig.create(config));
        },
        saveNewRegistration: function (registration) {
            var config = { isBusyRequest: true };
            return $http.post('/registration/orders/newRegistration', registration, apiConfig.create(config));
        },
        transferRegistration: function (registrations) {
            var config = { isBusyRequest: true };
            return $http.post('/registration/orders/transferRegistration', registrations, apiConfig.create(config));
        },
        getPatient: function (id) {
            var config = { isBusyRequest: true };
            return $http.get("/registration/patients/" + id, apiConfig.create(config));
        },
        updatePatient: function (patientEdit) {
            var config = { isBusyRequest: true };
            return $http.put("/registration/patients/" + patientEdit.patient.uniqueID, patientEdit, apiConfig.create(config));
        },
        updateOrder: function (order) {
            var config = { isBusyRequest: true };
            return $http.put("/registration/orders/" + order.uniqueID, order, apiConfig.create(config));
        },
        getPatientNO: function (site) {
            var config = { params: {}, isBusyRequest: true };
            return $http.get('/registration/orders/generate/patientNo/' + site, apiConfig.create(config));
        },
        getAccNo: function () {
            var config = { params: {}, isBusyRequest: true };
            return $http.get('/registration/orders/generate/accNo', apiConfig.create(config));
        },
        getProcedures: function (patientId, orderID) {
            var config = { params: { orderID: orderID || null } };
            return $http.get('/registration/patients/procedures/' + patientId, apiConfig.create(config));
        },
        getProcedure: function (id) {
            var config = { isBusyRequest: true };
            return $http.get('/registration/procedures/' + id, apiConfig.create());
        },
        getProceduresByOrderID: function (orderID) {
            return $http.get('/registration/orders/procedures/' + orderID, apiConfig.create());
        },
        updateProcedure: function (id, data) {
            var config = { isBusyRequest: true };
            return $http.put("/registration/procedures/" + id, data, apiConfig.create(config));
        },
        addProcedure: function (data) {
            var config = { isBusyRequest: true };
            return $http.post("/registration/procedures", data, apiConfig.create(config));
        },
        deleteProcedure: function (id) {
            return $http.delete('/registration/procedures/' + id, apiConfig.create());
        },
        getEnglishName: function (strChinese, UpperFirstLetter, SeparatePolicy, Separator) {
            var data = { LocalName: strChinese, UpperFirstLetter: UpperFirstLetter, SeparatePolicy: SeparatePolicy, Separator: Separator };
            return $http.post('/registration/orders/simplifiedToEnglish', data, apiConfig.create());
        },
        getRequisitionUrl: function (accNo, modalityType) {
            var config = { params: { accNo: accNo, modalityType: modalityType }, isBusyRequest: true };
            return $http.get('/registration/orders/requisition/url', apiConfig.create(config));
        },
        getBarCodeUrl: function (accNo, modalityType) {
            var config = { params: { accNo: accNo, modalityType: modalityType }, isBusyRequest: true };
            return $http.get('/registration/orders/barcode/url', apiConfig.create(config));
        },
        getOrder: function (orderId) {
            var config = { isBusyRequest: true };
            return $http.get('/registration/orders/' + orderId, apiConfig.create(config));
        },
        getRegistrationInfo: function (orderId) {
            var config = { isBusyRequest: true };
            return $http.get('/registration/' + orderId, apiConfig.create(config));
        },
        getProcedureCodes: function (site) {
            return $http.get('/registration/procedureCodes/' + site, apiConfig.create());
        },
        getProcedureByCode: function (code, modality) {
            var config = { params: { code: code, modality: modality } };
            return $http.get('/registration/procedureCodes/code', apiConfig.create(config));
        },
        getBodySystemMaps: function (site) {
            return $http.get('/registration/bodySystemMaps/' + site, apiConfig.create());
        },
        finishExam: function (order) {
            var config = { isBusyRequest: true };
            return $http.put("/registration/finishexam/" + order.uniqueID, order, apiConfig.create(config));
        },
        getRequestInfo: function (cardNumber, cardType) {
            var config = { params: { cardNumber: cardNumber, cardType: cardType }, isBusyRequest: true };
            return $http.get("/registration/intergration/requestInfo", apiConfig.create(config));
        },
        getIntergrationPatientInfo: function (cardNumber, cardType) {
            var config = { params: { cardNumber: cardNumber, cardType: cardType }, isBusyRequest: true };
            return $http.get("/registration/intergration/patient", apiConfig.create(config));
        },
        getSimilarPatients: function (globalID, risPatientID, hisID, patientName, matchKey, matchValue) {
            var config = { params: { globalID: globalID, risPatientID: risPatientID, hisID: hisID, patientName: patientName }, isBusyRequest: true };
            return $http.get("/registration/intergration/similarPatient", apiConfig.create(config));
        },
        rejectTransfer: function (requests) {
            var config = { isBusyRequest: true };
            return $http.post("/registration/intergration/processRequests", requests, apiConfig.create(config));
        },
        saveTempImage: function (imageData) {
            return $http.post("/registration/requisition/image", imageData, apiConfig.create());
        },
        updateTempImage: function (imageData) {
            return $http.post("/registration/requisition/image", imageData, apiConfig.create());
        },
        downLoadRequisitionFiles: function (accNo) {
            var config = { isBusyRequest: true };
            return $http.get("/registration/requisition/image/" + accNo, apiConfig.create(config));
        },
        generateErNo: function () {
            var config = { isBusyRequest: true };
            return $http.get("/registration/requisition/image/erNo", apiConfig.create(config));
        },
        deleteImage: function (fileName, relativePath, requisitionID) {
            var config = { params: { fileName: fileName, relativePath: relativePath, requisitionID: requisitionID } };
            return $http.delete("/registration/requisition/image", apiConfig.create(config));
        },
        processRequisitionInOrder: function (params) {
            var config = { isBusyRequest: true };
            return $http.post("/registration/requisition/order/image", params, apiConfig.create(config));
        },
        clearTempFile: function (erNo) {
            return $http.delete("/registration/requisition/order/image/" + erNo, apiConfig.create());
        },
        validateFtp: function () {
            return $http.get("/registration/requisition/ftp", apiConfig.create());
        },
        transferBookingToReg: function (orderId) {
            var config = { isBusyRequest: true };
            return $http.put("/registration/booking/bookingToRegistration/" + orderId, null, apiConfig.create(config));
        },
        getBookingModalities: function (modalityType, site) {
            var config = { params: { modalityType: modalityType, site: site } };
            return $http.get('/registration/booking/modalities', apiConfig.create(config));
        },
        getModalityTimeSlice: function (modality, startDate, endDate, site, userId, role) {
            var config = {
                params: {
                    modality: modality,
                    startDate: startDate,
                    endDate: endDate,
                    site: site,
                    userId: userId,
                    role: role
                }
            };
            return $http.get('/registration/booking/modalitytimeslice', apiConfig.create(config));
        },
        lockTimeSlice: function (timeSlice) {
            return $http.post('/registration/booking/locktimeslice', timeSlice, apiConfig.create());
        },
        unLockTimeSlice: function (params) {
            return $http.post('/registration/booking/unlocktimeslice', params, apiConfig.create());
        },
        updateProcedureSlice: function (orderId, slice) {
            var config = { isBusyRequest: true };
            return $http.put("/registration/procedures/slice/" + orderId, slice, apiConfig.create(config));
        }
    };
}]);
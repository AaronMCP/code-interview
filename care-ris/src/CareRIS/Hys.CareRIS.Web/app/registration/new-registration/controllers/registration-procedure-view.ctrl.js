registrationModule.controller('RegistrationProcedureController', ['$log', '$scope',
    '$modalInstance', 'registrationService', 'loginContext', 'configurationService', 'enums',
    '$translate', 'updateProcedure', 'procedures', 'risDialog', 'registrationUtil', 'modalityTypes', '$rootScope', 'application', '$q', 'bookingSlice', 'openDialog','csdToaster',
 function ($log, $scope, $modalInstance, registrationService, loginContext, configurationService, enums,
     $translate, updateProcedure, procedures, risDialog, registrationUtil, modalityTypes, $rootScope, application, $q, bookingSlice, openDialog, csdToaster) {
     'use strict';
     $log.debug('RegistrationProcedureController.ctor()...');

     var dataBymodlityType;
     var dataBybodyCategory;
     var dataByBodyPart;
     var dataByCheckingItem;
     var selectedModalityType;
     var selectedBodyCategory;
     var selectedBodyPart;
     var selectedCheckingItem;
     var selectedCheckingSystem;
     var selectedModality;
     var selectedProcedureCode;
     var bodySystemMaps;
     var modalities;
     var proceduresList;
     var addedProcedures;
     var clientConfig = application.clientConfig;

     var getProcedureCodes = function () {
         var procedurePromise = registrationService.getProcedureCodes(loginContext.site),
             bodySystemPromise = registrationService.getBodySystemMaps(loginContext.site),
             modalitiesPromise = configurationService.getModalities(loginContext.site);
         $q.all([procedurePromise, bodySystemPromise, modalitiesPromise]).then(function (result) {
             var procedureData = result[0].data;
             bodySystemMaps = result[1].data;
             modalities = result[2].data;
             //disabled modality id collection
             var disabledModalities = (clientConfig && clientConfig.disabledModalities) ? clientConfig.disabledModalities.split('|') : null;
             if (disabledModalities) {
                 modalities = _.reject(modalities, function (p) {
                     return _.contains(disabledModalities, p.uniqueID);
                 });
             }
             processProcedures(procedureData);
         });
     };

     var processProcedures = function (data) {
         var proceduresData = data;
         // update from registration edit or transfer,need to filter special modality type
         if (modalityTypes) {
             proceduresData = _.filter(proceduresData, function (p) {
                 return _.contains(modalityTypes, p.modalityType);
             });
         } else {
             // don't get disabled types data
             var disabledTypes = (clientConfig && clientConfig.disabledModalityTypes) ? clientConfig.disabledModalityTypes.split('|') : null;
             // only not update procedure need to disable modalty type
             if (disabledTypes) {
                 proceduresData = _.reject(proceduresData, function (p) {
                     return _.contains(disabledTypes, p.modalityType);
                 });
             }
         }
         var result = getDataBySelectedItem(proceduresData, "modalityType");
         dataBymodlityType = result.selectedData;
         $scope.modlityTypeList = result.listData;
         // for update procedure
         if (updateProcedure) {
             selectModalityType(updateProcedure.modalityType, true);
         } else {
             selectModalityType(_.first($scope.modlityTypeList));
         }
     };

     var selectModalityType = function (modlityType, isUpdate) {
         selectedModalityType = modlityType;
         $scope.selectedModalityType = selectedModalityType;
         // update or add booking procedure,only one modalityType and modality
         if (bookingSlice && bookingSlice.modality) {
             $scope.modalityList = [{ modalityName: bookingSlice.modality }];
         } else {
             $scope.modalityList = _.where(modalities, { modalityType: selectedModalityType });
         }
         var result = getDataBySelectedItem(dataBymodlityType, "bodyCategory", modlityType);
         dataBybodyCategory = result.selectedData;
         $scope.bodyCategoryList = result.listData;
         // get modality list
         if (isUpdate) {
             selectBodyCategory(updateProcedure.bodyCategory, true);
         }
         else {
             selectBodyCategory(_.first(result.listData), false);
         }
     };

     var selectBodyCategory = function (category, isUpdate) {
         selectedBodyCategory = category;
         $scope.selectedBodyCategory = selectedBodyCategory;
         var result = getDataBySelectedItem(dataBybodyCategory, "bodyPart", category);
         dataByBodyPart = result.selectedData;
         $scope.bodyPartList = result.listData;
         if (!isUpdate) {
             selectBodyPart(_.first(result.listData), false);
         }
         else {
             selectBodyPart(updateProcedure.bodyPart, true);
         }
     };

     var selectBodyPart = function (part, isUpdate) {
         selectedBodyPart = part;
         $scope.selectedBodyPart = selectedBodyPart;

         // the checkingItem is unique
         dataByCheckingItem = dataByBodyPart[part];
         $scope.checkingItemList = dataByCheckingItem;
         if (!bodySystemMaps) {
             registrationService.getBodySystemMaps(loginContext.site).then(function (data) {
                 bodySystemMaps = data.data;
                 checkingSystemCallBack(isUpdate);
             });
         }
         else {
             checkingSystemCallBack(isUpdate);
         }
         // for update procedure
         if (!isUpdate) {
             selectCheckingItem(_.first(dataByCheckingItem), false);
         }
         else {
             var item = _.findWhere(dataByCheckingItem, { procedureCode: updateProcedure.procedureCode });
             selectCheckingItem(item, true);
         }
     };

     var checkingSystemCallBack = function (isUpdate) {
         $scope.checkSystemList = _.where(bodySystemMaps, { bodyPart: selectedBodyPart, modalityType: selectedModalityType });
         if (!isUpdate) {
             selectCheckingSystem(_.first($scope.checkSystemList));
         }
         else {
             selectCheckingSystem({ examSystem: updateProcedure.examSystem });
         }
     };

     var selectCheckingItem = function (item, isUpdate) {
         selectedCheckingItem = item;
         $scope.selectedCheckingItem = selectedCheckingItem;
         // get modality list
         modalityCallBack(item, isUpdate);
     };
     var modalityCallBack = function (item, isUpdate) {
         if (isUpdate) {
             var modality = _.findWhere($scope.modalityList, { modalityName: updateProcedure.modality });
             selectModality(modality);
         }
         else {
             if (item.defaultModality) {
                 var modality = _.findWhere($scope.modalityList, { modalityName: item.defaultModality });
                 selectModality(modality);
             }
             else {
                 selectModality(_.first($scope.modalityList));
             }
         }
     };
     var selectCheckingSystem = function (system) {
         selectedCheckingSystem = system;
         $scope.selectedCheckingSystem = system;
     };
     var selectModality = function (modality) {
         if (modality) {
             selectedModality = modality;
             $scope.selectedModality = selectedModality;
         } else {
             selectedModality = $scope.modalityList[0];
             $scope.selectedModality = selectedModality;
         }
     };
     var getDataBySelectedItem = function (data, group, selectedItem) {
         var selectedData;
         var listData;
         //for modalityType
         if (!selectedItem) {
             selectedData = _.groupBy(data, group);
             listData = _.keys(selectedData);
         }
         else {
             selectedData = data[selectedItem];
             selectedData = _.groupBy(selectedData, group);
             listData = _.keys(selectedData);
         }
         return { selectedData: selectedData, listData: listData };
     };
     var Procedure = function () {
         this.procedureCode = selectedCheckingItem.procedureCode;
         this.checkingItem = selectedCheckingItem.checkingItem;
         this.rpDesc = selectedCheckingItem.description;
         this.modality = selectedModality.modalityName;
         this.modalityType = selectedCheckingItem.modalityType;
         this.bodyPart = selectedCheckingItem.bodyPart;
         this.bodyCategory = selectedCheckingItem.bodyCategory;
         //FilmSpec, FilmCount, ContrastName, ContrastDose, ImageCount, ExposalCount, Charge, BookingNotice,
         this.filmSpec = selectedCheckingItem.filmSpec;
         this.filmCount = selectedCheckingItem.filmCount;
         this.contrastName = selectedCheckingItem.contrastName;
         this.contrastDose = selectedCheckingItem.contrastDose;
         this.imageCount = selectedCheckingItem.imageCount;
         this.exposalCount = selectedCheckingItem.exposalCount;
         this.charge = selectedCheckingItem.charge; 
         //DE20205, bookingnotice not id
         //this.bookingNotice = selectedCheckingItem.bookingNotice;
         this.domain = loginContext.domain;
         this.examSystem = selectedCheckingSystem.examSystem;
         // for registration view
         this.reportID = null;
         // determine whether the procedure come from booking or registration order in registration-view

         if (bookingSlice) {
             this.status = enums.rpStatus.noCheck;
             this.booker = loginContext.userId;
             this.bookingBeginTime = bookingSlice.bookingBeginTime;
             this.bookingEndTime = bookingSlice.bookingEndTime;
             this.bookingTimeAlias = bookingSlice.bookingTimeAlias;
         } else {
             this.registrar = loginContext.userId;
             this.registrarName = loginContext.localName;
             this.status = enums.rpStatus.checkIn;//init status
         }
     };
     var add = function () {
         if (!selectedCheckingItem) {
             var title = $translate.instant("Tips");
             var content = $translate.instant("NotSelectCheckingItemErrorMsg");
             openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
             return false;
         }
         //procedure to be added or updated
         var procedure = new Procedure();
         // validate wheather exist the selected item
         var existItem = _.findWhere(proceduresList, { procedureCode: selectedCheckingItem.procedureCode });
         if (!existItem) {
             //add procedure
             proceduresList.push(procedure);// for validate the duplicate item.
             addedProcedures.push(procedure);
             return true;
         }
         else {
             //var title = $translate.instant("Tips");
             var content = $translate.instant("DuplicateCheckingItemErrorMsg");
             csdToaster.pop('info', content);
             //openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
             return false;
         }
     };
     var complete = function () {
         if (updateProcedure) {
             var existItem = _.findWhere(proceduresList, { procedureCode: selectedCheckingItem.procedureCode });
             var procedure = new Procedure();
             // validate wheather exist the selected item
             if (!existItem || existItem.procedureCode === updateProcedure.procedureCode) {
                 var result = { procedure: procedure, oldProcedure: updateProcedure };
                 $modalInstance.close(result);
             }
             else {
                 var title = $translate.instant("Tips");
                 var content = $translate.instant("DuplicateCheckingItemErrorMsg");
                 openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
             }
         }
         else {
             // if the added items's count is 0,then add the selected procedure and close the window.if not ,close the dialog window immediately.
             if (addedProcedures.length === 0) {
                 var isSuccess = add();
                 if (isSuccess) {
                     $modalInstance.close(addedProcedures);
                 }
             }
             else {
                 $modalInstance.close(addedProcedures);
             }
         }
     };

     var deleteProcedure = function (deleteItem) {
         registrationUtil.deleteProcedure(proceduresList, deleteItem);
         registrationUtil.deleteProcedure(addedProcedures, deleteItem);
     };

     var cancel = function () {
         $modalInstance.dismiss();
         if (modalityTypes) {
             $rootScope.$broadcast('event:refreshProcedures');
         }
     };
     var directSelectProcedure = function () {
         !$scope.isDisableAddNext && $scope.selectedModality && add();
     };
     (function initialize() {
         proceduresList = _.clone(procedures);
         addedProcedures = [];
         $scope.add = add;
         $scope.complete = complete;
         $scope.cancel = cancel;
         $scope.active = true;
         $scope.isDisableAddNext = updateProcedure ? true : false;
         if (!updateProcedure) {
             // add procedure
             $scope.addedProcedures = addedProcedures;
             $scope.isUpdate = false;
             $scope.title = $translate.instant("AddProcedure");
         }
         else {
             // udpate procedure
             $scope.addedProcedures = [updateProcedure];
             $scope.isUpdate = true;
             $scope.title = $translate.instant("UpdateProcedure");
         }
         $scope.deleteProcedure = deleteProcedure;
         getProcedureCodes();
         $scope.selectModalityType = selectModalityType;
         $scope.selectBodyCategory = selectBodyCategory;
         $scope.selectBodyPart = selectBodyPart;
         $scope.selectCheckingItem = selectCheckingItem;
         $scope.selectCheckingSystem = selectCheckingSystem;
         $scope.selectModality = selectModality;
         $scope.directSelectProcedure = directSelectProcedure;
     }());
 }
]);
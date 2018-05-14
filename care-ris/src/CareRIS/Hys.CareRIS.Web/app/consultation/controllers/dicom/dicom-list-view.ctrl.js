worklistModule.controller('DicomListController', [
    '$log', '$scope', '$state', 'loginContext', 'constants', 'enums', '$rootScope', 'consultationService',
    'kendoService', '$stateParams', 'loginUser', 'requestService', 'openDialog', '$translate', '$timeout', 'clientAgentService', 'application',
    function($log, $scope, $state, loginContext, constants, enums, $rootScope, consultationService,
        kendoService, $stateParams, loginUser, requestService, openDialog, $translate, $timeout, clientAgentService, application) {
        'use strict';

        $log.debug('DicomListController.ctor()...');
        var moduleType;
        $scope.getData = function(searchCriteria) {
            return clientAgentService.getDicoms(searchCriteria);
        };
        $scope.deleteDicom = function(accessionNo, id) {
            var data = { accessionNo: accessionNo, studyinstanceuid: id };
            openDialog.openIconDialogOkCancelParam(openDialog.NotifyMessageType.Warn, $translate.instant("Warn"),
                $translate.instant("ConfrimDeleteDICOM"), deleteDicomCallback, data);
        };

        function Case(item) {
            var modulesInitValue = {};
            if (item.PatientDOB) {
                this.birthday = new Date(item.PatientDOB);
            }
            if (item.PatientSex) {
                this.gender = _.find(application.configuration.genderList, function (p) {
                    return p.shortcutCode.toUpperCase() === item.PatientSex.toUpperCase();
                }).value;
            }
            this.patientName = item.PatientName;
            var fileName = $scope.dicomPath + '\\' + item.AccessionNo;
            var moduleVal = { fileList: [{ fileName: fileName, fileType: 'folder', path: fileName }] };
            // exam info
            moduleVal.patientNo = item.PatientID;
            moduleVal.accessionNo = item.AccessionNo;
            moduleVal.examDate = item.StudyDate;
            moduleVal.examDescription = item.StudyDescription;
            moduleVal.bodyPart = item.BodyPart;
            modulesInitValue[moduleType] = [moduleVal];
            //{'ultrasound':[{patientId:{},fileList:[{fileName:'',fileType:'folder',path:''}]},{}],'teleradiology':[]} 
            this.modulesInitValue = modulesInitValue;
        };

        $scope.loadImage = function(id) {
            var data = { studyinstanceuid: id };
            clientAgentService.loadImage(data).success(function(result) {
            });
        };

        $scope.createNewCase = function (dataItem) {
            $scope.moduleTypeWin.center().open();
            $scope.selectedDataItem = dataItem;
        };

        $scope.search = function() {
            $scope.refresh();
        };
        var deleteDicomCallback = function(data) {
            clientAgentService.deleteDicoms(data).success(function(result) {
                if (result === enums.ActionReusltStatus.Success) {
                    $scope.refresh();
                } else if (result === enums.ActionReusltStatus.AccessDenied) {
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Error, $translate.instant("Error"), $translate.instant("DeleteDicomDenied"));
                } else {
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Error, $translate.instant("Error"), $translate.instant("DeleteErrorMsg"));
                }
            });
        };
          $scope.selectModule = function(module) {
              moduleType = module.type;
              var fillCase = new Case($scope.selectedDataItem);
              $state.go('ris.consultation.newpatientcase', {
                  timestamp: Date.now(),
                  searchCriteria: $stateParams.searchCriteria,
                  autoFillCase: fillCase
              });
          };

        (function initialize() {
            $scope.ageUnitList = $rootScope.ageUnitList;
            $scope.searchCriteria = $stateParams.searchCriteria | {};
            consultationService.getExamModules().success(function(data) {
                $scope.modules = data.modules;
            });
        }());
    }
]);
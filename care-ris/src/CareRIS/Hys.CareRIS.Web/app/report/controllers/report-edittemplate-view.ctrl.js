worklistModule.controller('ReportTemplateEditController', ['$log', '$scope', '$modalInstance', 'constants', '$timeout',
    'selectTemplate', 'reportService', 'loginContext', '$translate', 'risDialog', 'dictionaryManager', 'enums', 'application', 'configurationService', '$q',
    function ($log, $scope, $modalInstance, constants, $timeout, selectTemplate, reportService, loginContext, $translate, risDialog,
        dictionaryManager, enums, application, configurationService, $q) {
        'use strict';
        $log.debug('ReportSelectUserController.ctor()...');

        //检查部位
        var bodySystemPromise = configurationService.getBodySystemMapText();

        //设备类型
        var modalityTypePromise = configurationService.getModalityTypes();

        $q.all([bodySystemPromise, modalityTypePromise]).then(function (res) {
            $scope.bodyPartList = res[0].data;
            $scope.modalityTypeList = res[1].data;
            $scope.selectTemplate = selectTemplate;
        });

        var saveTemplate = function (form) {
            if (!form.$valid) {
                $scope.isShowErrorMsg = true;
                return;
            }
            $modalInstance.close(selectTemplate);
        };

        var cancel = function () {
            $modalInstance.dismiss();
        };

        var judgeDuplicateName = function () {
            $scope.isDuplicateName = false;
            $scope.selectTemplateForm.templateName.$error.isDuplicateName = false;
            $scope.selectTemplateForm.templateName.duplicateNameError = '';

            if ($scope.selectTemplate.templateName && $scope.selectTemplate.templateName != '') {
                reportService.isDuplicatedTemplateName($scope.selectTemplate).success(function (data) {
                    if (data) {
                        $scope.selectTemplateForm.$valid = false;
                        $scope.isDuplicateName = true;
                        $scope.selectTemplateForm.templateName.$error.isDuplicateName = true;
                        $scope.selectTemplateForm.templateName.duplicateNameError = $translate.instant("CreateReportTemplateError");
                    }
                });
            }
        };

        (function initialize() {
            $log.debug('ReportTemplateEditController.initialize()...');
            $scope.isShowErrorMsg = false;

            $scope.isDuplicateName = false;
            $scope.judgeDuplicateName = judgeDuplicateName;

            $scope.title = $translate.instant("SaveUserTemplate");
            if (selectTemplate.templateName && selectTemplate.templateName != '') {
                $scope.title = $translate.instant("EditReportTemplate");
            }
            $scope.saveTemplate = saveTemplate;
            $scope.cancel = cancel;
            var configurationData = application.configuration;
            $scope.genderList = configurationData.genderList;
        }());
    }
]);



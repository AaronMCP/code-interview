consultationModule.directive('dicomListView', ['$log', 'constants', 'consultationService', 'loginContext', '$filter','dicomService',
    function ($log, constants, consultationService, loginContext, $filter,dicomService) {
    $log.debug('dicomListView.ctor()...');
    return {
        restrict: 'E',
        templateUrl:'/app/consultation/views/dicom/dicom-list-view.html',
        controller: 'DicomListController',
        replace: true,
        scope: {
            isSelectDicom: '@',
            cancelDicom: '&',
            confirmDicom:'&'
        },
        link: function (scope, element) {
            scope.dicomListOptions = {
                dataSource: new kendo.data.DataSource({
                    schema: {
                        data: 'Result',
                        total: 'Count'
                    },
                    serverPaging: true,
                    transport: {
                        read: function (options) {
                            var searchCriteria = {
                                pageIndex: options.data.page,
                                pageSize: options.data.pageSize
                            };
                            if (scope.searchCriteria) {
                                _.extend(searchCriteria, scope.searchCriteria);
                            }
                            scope.getData(searchCriteria).success(function (data) {
                                if (data.Count !== 0) {
                                    scope.dicomPath = data.DICOMPath;
                                    dicomService.lastTime = data.LastTime;
                                    var params =_.pluck(data.Result, 'StudyInstanceUID');
                                    consultationService.getDicomRelations(params, loginContext.userId).success(function (result) {
                                        data.Result = _.map(data.Result, function(p) {
                                            p.IsCreatedCase = result[p.StudyInstanceUID];
                                            return p;
                                        });
                                        options.success(data);
                                    })
                                    .error(function(error) {
                                        options.error(error);
                                        });
                                } else {
                                    options.success(data);
                                }
                            }).error(function (error) {
                                options.error(error);
                            });
                        }
                    },
                    pageSize: constants.pageSize
                }),
                pageable: {
                    refresh: true,
                    buttonCount: 5,
                    input: true
                },
                scrollable: true,
                sortable: true,
                resizable: true,
                reorderable: true,
                selectable: 'row',
                columns: [
                    {
                        title: '{{ "Edit" | translate }}',
                        template: kendo.template($('#column-menu-dicoms').html()),
                        width: 100,
                        filterable: false,
                        sortable: false,
                        resizable: false,
                        reorderable: false,
                        menu: false
                    },
                    {
                        field: 'patientBaseInfo',
                        title: '{{ "PatientBaseInfo" | translate }}',
                        template: function (dataItem) {
                            var result = dataItem.PatientName + '&nbsp;&nbsp;&nbsp;&nbsp; ';
                            if (dataItem.PatientSex === 'M') {
                                result += '<span class="icon-male icon-blue icon-gender-font">';
                            } else if (dataItem.PatientSex === 'F') {
                                result += '<span class="icon-female icon-purple icon-gender-font">';
                            } else {
                                result += '<span class="icon-unknown icon-orange icon-gender-font">';
                            }
                            result += '</span>&nbsp;&nbsp; ';
                            var age = dataItem.PatientAge.substr(0, dataItem.PatientAge.length - 1);
                            var ageUnit = dataItem.PatientAge.substr(dataItem.PatientAge.length - 1);
                            if (age.substr(0, 1) ==='0' ) {
                                age = age.substr(1);
                            }
                            var a = _.find(scope.ageUnitList, function (p) { return p.shortcutCode.toUpperCase() === ageUnit.toUpperCase() });
                            if (a) {
                                ageUnit = a.text;
                            }
                            dataItem.CurrentAge = age + ' ' + ageUnit;
                            result += dataItem.CurrentAge;
                            return result;
                        }
                    },
                    {
                        field: "PatientID ",
                        title: '{{ "PatientNo" | translate }}',
                        width: "120px"
                    }, {
                        field: "AccessionNo",
                        title: '{{ "AccessionNo" | translate }}',
                        width: "120px"
                    }, {
                        field: "BodyPart",
                        title: '{{ "BodyPart" | translate }}',
                        width: "120px"
                    }, {
                        field: "Modality",
                        title: '{{ "Modality" | translate }}',
                        width: "120px"
                    }, {
                        field: "StudyDate",
                        title: '{{ "ExamineTime" | translate }}',
                        width: "120px"
                    }, {
                        template: kendo.template($('#column-menu-createCase').html()),
                        title: '{{ "IsCreatedCase" | translate }}',
                        width: "120px"
                    }
                ],
                dataBound: function (arg) {
                    var grid = $('#dicomGrid').data('kendoGrid');
                    var data = grid.dataSource.data()[0];
                    if (data) {
                        var row = grid.table.find('tr[data-uid="' + data.uid + '"]');
                        grid.select(row);
                    }
                }
            };
            scope.refresh = function () {
                var dicomGrid = $('#dicomGrid').data('kendoGrid');
                dicomGrid.dataSource.page(1);
                dicomGrid.dataSource.read();
                dicomGrid.refresh();
            };
            scope.confirmDicomClick = function () {
                var grid = $('#dicomGrid').data('kendoGrid');
                var selectedDicom = grid.dataItem(grid.select());
                var item = {};
                item.patientNo = selectedDicom.PatientID;
                item.accessionNo = selectedDicom.AccessionNo;
                if (selectedDicom.StudyDate) {
                    item.examDate = $filter('date')(selectedDicom.StudyDate,'yyyy/MM/dd');
                }
                item.examDescription = selectedDicom.StudyDescription;
                item.bodyPart = selectedDicom.BodyPart;
                item.dicomPath = scope.dicomPath + '\\' + selectedDicom.AccessionNo;
                scope.confirmDicom({ dicom :item});
            };
        }
    };
}]);

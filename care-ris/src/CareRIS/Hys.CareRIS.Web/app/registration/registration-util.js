registrationModule.factory('registrationUtil', ['$log', 'registrationService', 'loginContext', 'configurationService', 'enums', '$translate', '$modal', '$anchorScroll', '$location',
'risDialog', '$filter', '$q', 'reportService', '$window', 'busyRequestNotificationHub', '$http', '$rootScope', 'readDocTogether', 'application', 'openDialog', 'clientAgentService', 'csdToaster', '$state',
function ($log, registrationService, loginContext, configurationService, enums, $translate, $modal, $anchorScroll, $location, risDialog,
        $filter, $q, reportService, $window, busyRequestNotificationHub, $http, $rootScope, readDocTogether, application, openDialog, clientAgentService, csdToaster, $state) {
    'use strict';

    //处理起始时间和结束时间相差的年数
    var getYearDifference = function (dBegin, dEnd) {
        var year = DateDiff.inYears(dBegin, dEnd);
        if (dEnd.getMonth() < dBegin.getMonth()) {
            year--;
        } else if (dEnd.getMonth() == dBegin.getMonth()) {
            if (dEnd.getDate() < dBegin.getDate()) {
                year--;
            }
        }

        if (year < 0) {
            year = 0;
        }
        return year;
    };
    // 处理起始时间和结束时间相差的月数
    var getMonthDifference = function (dBegin, dEnd) {
        var month = DateDiff.inMonths(dBegin, dEnd);
        if (dEnd.getDate() < dBegin.getDate()) {
            month--;
        }
        if (month < 0) {
            month = 0;
        }
        return month;
    };

    var DateDiff = {
        inDays: function (d1, d2) {
            var t2 = d2.getTime();
            var t1 = d1.getTime();

            return parseInt((t2 - t1) / (24 * 3600 * 1000));
        },

        inWeeks: function (d1, d2) {
            var t2 = d2.getTime();
            var t1 = d1.getTime();
            return Math.floor((t2 - t1) / (24 * 3600 * 1000 * 7));
        },

        inMonths: function (d1, d2) {
            var d1Y = d1.getFullYear();
            var d2Y = d2.getFullYear();
            var d1M = d1.getMonth();
            var d2M = d2.getMonth();

            return (d2M + 12 * d2Y) - (d1M + 12 * d1Y);
        },

        inYears: function (d1, d2) {
            return d2.getFullYear() - d1.getFullYear();
        },

        inHours: function (d1, d2) {
            //return d2.getHours() - d1.getHours();
            return parseInt(((d2 - d1) / 3600000));
        }
    };
    var relateReportRead = function (params) {
        var isRead = false;
        var orderID = "";
        var from = "";
        if (params && params.length == 3) {
            orderID = params[1];
            isRead = params[0];
            from = params[2];
        }
        var data = {
            orderId: orderID,
            reportId: 0,
            isPreview: false,
            from: from
        };
        if (isRead) data.isRead = true;
        $state.go('ris.report', data);
    };

    var onlyReadReport = function (item) {
        var data = {
            isPreview: false,
            isRead: true,
            procedureId: item.procedureID,
            patientCaseOrderId: item.orderID
        };
        if (item.status <= enums.rpStatus.examination) data.reportId = 0;
        $state.go('ris.report', data);
    };

    var showFTPError = function () {
        var title = $translate.instant("Tips");
        var content = $translate.instant("FTPErrorMsg");
        openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
    };
    var showRequisition = function (args, newScope) {
        var requisitionModalInstance = $modal.open({
            template: '<registration-requisition-view args="args"  modal-instance="modalInstance()" ></registration-requisition-view>',
            scope: newScope,
            backdrop: 'static',
            keyboard: false,
            size: 'lg',
        });
        newScope.modalInstance = function () {
            return requisitionModalInstance;
        };
        return requisitionModalInstance;
    };
    /*********************************************ID Card Validation*****************************************************************/
    var Wi = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1];
    var ValideCode = [1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2]; // the last number validation.10 as X   
    /**  
     * validate 18bit ID Card
     * @param ID Card
     * @return
     * -1:invalid birthday
     *-2:invalida parity bit
     *-3:invalid length 
     */
    var IdCardValidate = function IdCardValidate(idCard) {
        idCard = trim(idCard.replace(/ /g, "")); // remove the blank                  
        if (idCard.length == 15) {
            return isValidityBrithBy15IdCard(idCard); // old ID Cards:15 bit
        } else if (idCard.length == 18) {
            var a_idCard = idCard.split(""); // the array of ID Card
            if (isValidityBrithBy18IdCard(idCard) === 0) {
                return isTrueValidateCodeBy18IdCard(a_idCard);
            }
            else {
                return -1;
            }
        } else {
            return -3;
        }
    };
    /**  
     * validate 18bit ID Card
     * @param a_idCard array of id card
     * @return
     */
    function isTrueValidateCodeBy18IdCard(a_idCard) {
        var sum = 0;
        if (a_idCard[17].toLowerCase() == 'x') {
            a_idCard[17] = 10;
        }
        for (var i = 0; i < 17; i++) {
            sum += Wi[i] * a_idCard[i];
        }
        var valCodePosition = sum % 11;
        if (a_idCard[17] == ValideCode[valCodePosition]) {
            return 0;
        } else {
            return -2;
        }
    }

    /**  
     * validate 18bit ID Card
     * @param id card
     * @return
     */
    function isValidityBrithBy18IdCard(idCard18) {
        var year = idCard18.substring(6, 10);
        var month = idCard18.substring(10, 12);
        var day = idCard18.substring(12, 14);
        var temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
        if (temp_date.getFullYear() != parseFloat(year) || temp_date.getMonth() != parseFloat(month) - 1 || temp_date.getDate() != parseFloat(day)) {
            return -1;
        } else {
            return 0;
        }
    }

    /**  
     * 15bit validation
     * @param idCard15
     * @return
     */
    function isValidityBrithBy15IdCard(idCard15) {
        var year = idCard15.substring(6, 8);
        var month = idCard15.substring(8, 10);
        var day = idCard15.substring(10, 12);
        var temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
        if (temp_date.getYear() != parseFloat(year) || temp_date.getMonth() != parseFloat(month) - 1 || temp_date.getDate() != parseFloat(day)) {
            return -1;
        } else {
            return 0;
        }
    }

    function trim(str) {
        return str.replace(/(^\s*)|(\s*$)/g, "");
    }

    var $this = this;

    /****************************************************************************************************************************************/
    return {
        getPriority: function (value, patientTypeList) {
            var patientType = _.findWhere(patientTypeList, {
                value: value
            });
            return patientType ? patientType.mapValue : null;
        },
        simplifiedToEnglish: function (patientName, UpperFirstLetter, SeparatePolicy, Separator) {
            if (!patientName) {
                return "";
            }
            UpperFirstLetter = UpperFirstLetter == 1 ? true : false;
            SeparatePolicy = SeparatePolicy ? SeparatePolicy : 1;
            Separator = Separator ? Separator : "";
            return registrationService.getEnglishName(patientName, UpperFirstLetter, SeparatePolicy, Separator);
        },
        // set birthday based on age
        setBirthday: function (strAgeUnit, value) {
            if (!value) {
                return;
            }
            if (!strAgeUnit) {
                return;
            }
            var dtBase = new Date();
            var re = /^[1-9]+[0-9]*]*$/;
            var m_nPatientMaxAge = 200;
            var errorMsg = '';
            if (!re.test(value)) {
                errorMsg = $translate.instant("IntegerErrorMsg");
                return {
                    errorMsg: errorMsg,
                    newDate: dtBase
                };
            }
            var nNum = parseInt(value);
            switch (strAgeUnit.toLowerCase()) {
                case "year":
                    if (nNum > m_nPatientMaxAge || nNum < 1) {
                        errorMsg = $translate.instant("MaxAgeErrorMsg").replace("{0}", m_nPatientMaxAge);
                    } else {
                        dtBase.setYear(dtBase.getFullYear() - nNum);
                    }
                    break;
                case "month":
                    if (nNum > m_nPatientMaxAge * 12 || nNum < 1) {
                        errorMsg = $translate.instant("MaxMonthErrorMsg").replace("{0}", m_nPatientMaxAge * 12);
                    } else {
                        dtBase.setMonth(dtBase.getMonth() - nNum);
                    }
                    break;
                case "week":
                    if (nNum > m_nPatientMaxAge * 365 / 7 || nNum < 1) {
                        errorMsg = $translate.instant("MaxWeekErrorMsg").replace("{0}", parseInt(m_nPatientMaxAge * 365 / 7));
                    } else {
                        dtBase.setDate(dtBase.getDate() - 7 * nNum);
                    }

                    break;
                case "day":

                    if (nNum > m_nPatientMaxAge * 365 || nNum < 1) {
                        errorMsg = $translate.instant("MaxDayErrorMsg").replace("{0}", m_nPatientMaxAge * 365);
                    } else {
                        dtBase.setDate(dtBase.getDate() + -1 * (nNum - 1));
                    }
                    break;
                case "hour":
                    if (nNum > 200 * 365 * 24 || nNum < 1) {
                        errorMsg = $translate.instant("MaxHourErrorMsg").replace("{0}", m_nPatientMaxAge * 365 * 24);
                    } else {
                        dtBase.setHours(dtBase.getHours() - nNum);
                    }
                    break;
                default:
                    break;
            }
            return {
                errorMsg: errorMsg,
                newDate: dtBase
            };
        },
        setAge: function (yearNumber, monthNumber, dayNumber, value) {
            yearNumber = yearNumber | 2;
            monthNumber = monthNumber | 1;
            dayNumber = dayNumber | 2;
            var ageResult = {};

            var d = new Date(value);
            var errorMsg = '';
            if (!_.isDate(d) || !value) {
                errorMsg = $translate.instant("FormatErrorMsg");
            }

            var dateNow = new Date();
            if (dateNow < d) {
                errorMsg = $translate.instant("GtThanTodayErrorMsg");
            }
            if (errorMsg) {
                ageResult.errorMsg = errorMsg;
                return ageResult;
            }
            var year = getYearDifference(d, dateNow);
            var month = getMonthDifference(d, dateNow);
            var week = DateDiff.inWeeks(d, dateNow);
            var day = DateDiff.inDays(d, dateNow);
            var hour = DateDiff.inHours(d, dateNow);

            if (year >= yearNumber) {
                ageResult.ageType = "Year";
                ageResult.value = year;
            } else if (month >= monthNumber) {
                ageResult.ageType = "Month";
                ageResult.value = month;
            } else {
                if (day >= dayNumber) {
                    ageResult.ageType = "Day";
                    ageResult.value = day;
                } else {
                    ageResult.ageType = "Hour";
                    ageResult.value = hour;
                }
            }
            ageResult.errorMsg = '';
            return ageResult;
        },
        openProcedureWindow: function (callback, data) {
            var resolveData = {
                updateProcedure: function () {
                    return data.updateProcedure || null;
                },
                procedures: function () {
                    return data.procedures || null;
                },
                modalityTypes: function () {
                    return data.modalityTypes || null;
                },
                bookingSlice: function () {
                    return data.bookingSlice || null;
                }
            };
            var modalInstance = $modal.open({
                templateUrl: '/app/registration/views/registration-procedure-view.html',
                controller: 'RegistrationProcedureController',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                windowClass: "modal-registration-procedure",
                resolve: resolveData
            });
            modalInstance.result.then(function (result) {
                callback.call(result);
            });
        },
        deleteProcedure: function (procedures, id) {
            var index = _.findIndex(procedures, {
                procedureCode: id
            });
            if (index > -1) {
                procedures.splice(index, 1);
                return true;
            }
            return false;
        },
        updateProcedure: function (procedures, id, procedureNew) {
            var index = _.findIndex(procedures, {
                procedureCode: id
            });
            procedures[index] = procedureNew;
        },
        selectOrder: function (order) {
            var ts = this;
            var hasReportHTML = '';
            var hasNoReportHTML = '';

            var writeReprotCount = 0;
            var dataWriteReprot = [];
            var dataViewReprot = [];
            var dataFinishExam = [];
            var dataLoadImage = [];
            //get data for list
            angular.forEach(order.procedures, function (item, index) {
                item.isLock = false;
                item.isLockUser = '';
                item.isLockIp = '';
                item.orderId = order.orderID;

                if (item.status >= enums.rpStatus.examination && item.status <= enums.rpStatus.firstApprove) {
                    dataWriteReprot.push(item);
                }

                if (item.status < enums.rpStatus.examination && item.status != enums.rpStatus.noCheck) {
                    dataFinishExam.push(item);
                }

                if (item.status >= enums.rpStatus.examination) {
                    dataLoadImage.push(item);
                }

                if (item.status > enums.rpStatus.examination) {
                    dataViewReprot.push(item);
                } else if (item.status == enums.rpStatus.examination) {
                    writeReprotCount++;
                }

            });

            //set button
            var isDisabledWriteReport = false;
            var isDisabledPreviewReport = false;
            var isDisabledFinishExam = false;
            var isDisabledLoadImage = false;
            // transfer to registration
            var isDisabledTransferReg = false;
            if (dataViewReprot.length == 0) {
                isDisabledPreviewReport = true;
            }

            if (dataWriteReprot.length == 0) {
                isDisabledWriteReport = true;
            }

            if (dataFinishExam.length == 0) {
                isDisabledFinishExam = true;
            }

            if (dataLoadImage.length == 0) {
                isDisabledLoadImage = true;
            }
            if (order.procedures[0].status !== enums.rpStatus.noCheck) {
                isDisabledTransferReg = true;
            }

            //get all lock for order report
            reportService.getLockByOrderID(order.orderID).success(function (data) {
                if (data != null && data != '') {
                    angular.forEach(dataWriteReprot, function (writeItem, writeindex) {
                        if (data.procedureIDs == '') {
                            writeItem.isLock = true;
                        } else {
                            var startIndex = -1;
                            startIndex = data.procedureIDs.indexOf(writeItem.procedureID);
                            if (startIndex > -1) {
                                writeItem.isLock = true;
                            }
                        }
                    });
                }
            });
            readDocTogether.order = order;
            return {
                writeReprotCount: writeReprotCount,
                dataWriteReprot: dataWriteReprot,
                dataViewReprot: dataViewReprot,
                dataFinishExam: dataFinishExam,
                isDisabledWriteReport: isDisabledWriteReport,
                isDisabledPreviewReport: isDisabledPreviewReport,
                isDisabledFinishExam: isDisabledFinishExam,
                isDisabledLoadImage: isDisabledLoadImage,
                isDisabledTransferReg: isDisabledTransferReg
            };
        },
        selectLockedProcedure: function (procedureItem, orderID, fromType) {
            readDocTogether.item = procedureItem;
            readDocTogether.orderID = orderID;
            readDocTogether.caller = 'selectLockedProcedure';
            readDocTogether.associatedReport = false;
            //procedureItem.procedureID
            reportService.getProcedureByID(procedureItem.procedureID).success(function (data) {
                var item = data;
                item.isLock = false;
                item.isLockUser = '';
                item.isLockIp = '';
                item.from = fromType;
                item.procedureID = item.uniqueID;
                reportService.getLockByOrderID(orderID).success(function (lockData) {
                    if (lockData != null && lockData != '') {
                        if (lockData.procedureIDs == '') {
                            item.isLockUser = lockData.owner;
                            item.isLockIp = lockData.ownerIP;
                        } else {
                            var startIndex = -1;
                            startIndex = lockData.procedureIDs.indexOf(item.procedureID);
                            if (startIndex > -1) {
                                item.isLock = true;
                                var userStartIndex = lockData.procedureIDs.indexOf('&', startIndex + 1);
                                if (userStartIndex > -1) {
                                    var userEndIndex = lockData.procedureIDs.indexOf('&', userStartIndex + 1);
                                    if (userEndIndex > -1) {
                                        item.isLockUser = lockData.procedureIDs.substr(userStartIndex + 1, userEndIndex - userStartIndex - 1);
                                        var ipEndIndex = lockData.procedureIDs.indexOf('|', userEndIndex + 1);
                                        if (ipEndIndex > -1) {
                                            item.isLockIp = lockData.procedureIDs.substr(userEndIndex + 1, ipEndIndex - userEndIndex - 1);
                                        } else {
                                            item.isLockIp = lockData.procedureIDs.substr(userEndIndex + 1, lockData.procedureIDs.length - userEndIndex - 1);
                                        }
                                        ipEndIndex = item.isLockIp.indexOf('&');
                                        if (ipEndIndex > -1) {
                                            item.isLockIp = item.isLockIp.substr(0, ipEndIndex);
                                        }
                                    }
                                }
                            }
                        }
                    }


                    if (item.isLockUser != '' && (item.isLockUser != loginContext.userId || item.isLockIp != $rootScope.clientIP)) {
                        configurationService.getUserName(item.isLockUser).success(function (data) {
                            var tipInfo = $translate.instant("LockedBy");
                            tipInfo = tipInfo.replace('{0}', data.localName).replace('{1}', item.isLockIp);
                            openDialog.openIconDialogOkCancelParam2(openDialog.NotifyMessageType.Warn, tipInfo, '', onlyReadReport, item);
                        });
                    } else {
                        if (item.status <= enums.rpStatus.examination) {
                            $state.go('ris.report', {
                                isPreview: false,
                                procedureId: item.procedureID,
                                reportId: 0,
                                from: fromType,
                                patientCaseOrderId: orderID
                            });
                        } else {
                            if (item.status >= enums.rpStatus.firstApprove) {
                                reportService.getReportByProcedureID(item.procedureID).success(function (data) {
                                    if (loginContext.userId == data.firstApprover) {
                                        $state.go('ris.report', {
                                            isPreview: false,
                                            from: fromType,
                                            procedureId: item.procedureID,
                                            patientCaseOrderId: orderID
                                        });
                                    }
                                    else {
                                        onlyReadReport(item);
                                    }
                                });
                            }
                            else {
                                $state.go('ris.report', {
                                    isPreview: false,
                                    from: fromType,
                                    procedureId: item.procedureID,
                                    patientCaseOrderId: orderID
                                });
                            }
                        }
                    }
                });
            });
        },
        selectRelateReport: function (dataWriteReprot, orderID, fromType) {
            //get exam rocedureid
            var ts = this;
            readDocTogether.item = dataWriteReprot;
            readDocTogether.orderID = orderID;
            readDocTogether.caller = 'selectRelateReport';
            readDocTogether.associatedReport = true;
            reportService.getExamedProcedureByOrderID(orderID).success(function (data) {

                if (data != null && data != '' && data.length > 0) {
                    reportService.getLockByOrderID(orderID).success(function (lockData) {
                        if (lockData != null && lockData != '') {
                            var exit = false;
                            var isLockUser = '';
                            var isLockIp = '';
                            if (lockData.procedureIDs == '') {
                                exit = true;
                                isLockUser = lockData.owner;
                                isLockIp = lockData.ownerIP;
                            } else {

                                angular.forEach(data, function (writeItem, writeindex) {
                                    if (!exit) {
                                        var startIndex = -1;
                                        startIndex = lockData.procedureIDs.indexOf(writeItem);
                                        if (startIndex > -1 && lockData.procedureIDs.indexOf(writeItem + '&' + loginContext.userId + '&' + $rootScope.clientIP) == -1) {
                                            //

                                            var userStartIndex = lockData.procedureIDs.indexOf('&', startIndex + 1);
                                            if (userStartIndex > -1) {
                                                var userEndIndex = lockData.procedureIDs.indexOf('&', userStartIndex + 1);
                                                if (userEndIndex > -1) {
                                                    isLockUser = lockData.procedureIDs.substr(userStartIndex + 1, userEndIndex - userStartIndex - 1);
                                                    var ipEndIndex = lockData.procedureIDs.indexOf('|', userEndIndex + 1);
                                                    if (ipEndIndex > -1) {
                                                        isLockIp = lockData.procedureIDs.substr(userEndIndex + 1, ipEndIndex - userEndIndex - 1);
                                                    } else {
                                                        isLockIp = lockData.procedureIDs.substr(userEndIndex + 1, lockData.procedureIDs.length - userEndIndex - 1);
                                                    }
                                                    ipEndIndex = isLockIp.indexOf('&');
                                                    if (ipEndIndex > -1) {
                                                        isLockIp = isLockIp.substr(0, ipEndIndex);
                                                    }
                                                }
                                            }
                                            exit = true;
                                        } else {

                                        }
                                    }
                                });
                            }

                            if (exit) {
                                configurationService.getUserName(isLockUser).success(function (data) {
                                    var tipInfo = $translate.instant("LockedBy");
                                    tipInfo = tipInfo.replace('{0}', data.localName).replace('{1}', isLockIp);

                                    openDialog.openIconDialogOkCancelParam2(openDialog.NotifyMessageType.Warn, tipInfo, '', relateReportRead, [true, orderID, fromType]);
                                });
                            } else {
                                relateReportRead([false, orderID, fromType]);
                            }
                        } else {
                            relateReportRead([false, orderID, fromType]);
                        }
                    });


                } else {
                    if (dataWriteReprot.length > 0) {
                        ts.selectLockedProcedure(dataWriteReprot[0], orderID, fromType);
                    }
                }

            });
        },
        selectViewReport: function (dataViewReprot, fromType) {
            if (dataViewReprot.length > 0) {
                $state.go('ris.report', {
                    isPreview: true,
                    from: fromType,
                    procedureId: dataViewReprot[0].procedureID
                });
            }
        },
        finishExam: function (orderID, accNo, successCallBack) {
            //check need finish
            registrationService.getProceduresByOrderID(orderID).success(function (procedureData) {
                var needFinish = false;
                _.any(procedureData, function (item) {
                    if (item.status < enums.rpStatus.examination) {
                        needFinish = true;
                        return true;
                    }
                });
                if (!needFinish) {
                    var title = $translate.instant("Tips");
                    var content = $translate.instant("FinishExamFailedNoNeed");
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
                    return;
                }
                //check if it is locked, if locked, tell user about it,else finish exam 
                reportService.getLockByOrderIDByAnyLockType(orderID).success(function (lockData) {
                    if (lockData != null && lockData != '') {
                        var loginName = loginContext.userName;
                        var loginGuid = loginContext.userId;
                        var ownerGuids = [];
                        var ownerIPs = [];
                        var procedureIDs = lockData.procedureIDs;

                        if (procedureIDs != '') {
                            var procedures = procedureIDs.split('|');
                            for (var i = 0; i < procedures.length; i++) {
                                var procedure = procedures[i];
                                var procedureDetails = procedure.split('&');
                                var userGuid = procedureDetails[1];
                                var ownerIP = procedureDetails[2];
                                if (userGuid != loginGuid) {
                                    if (ownerGuids.indexOf(userGuid) == -1) {
                                        ownerGuids.push(userGuid);
                                        ownerIPs.push(ownerIP);
                                    }
                                }
                            }

                            //just show one locked info
                            if (ownerGuids.length > 0) {
                                var ownerID = ownerGuids[0];
                                configurationService.getUserName(ownerID).success(function (data) {
                                    var tipInfo = $translate.instant("RegistrationLocked");
                                    tipInfo = tipInfo.replace('{0}', data.localName).replace('{1}', ownerIPs[0]);
                                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), tipInfo);
                                    return;
                                });
                            }

                        } else {
                            var ownerID = lockData.owner;
                            var ownerIP = lockData.ownerIP;
                            configurationService.getUserName(ownerID).success(function (data) {
                                var tipInfo = $translate.instant("RegistrationLocked");
                                tipInfo = tipInfo.replace('{0}', data.localName).replace('{1}', ownerIP);
                                openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), tipInfo);
                                return;
                            });
                        }
                    } else {
                        var order = {};
                        order.examSite = loginContext.site;
                        order.examDomain = loginContext.domain;
                        order.examAccNo = accNo;
                        order.uniqueID = orderID;
                        registrationService.finishExam(order).success(function (data) {
                            var title = $translate.instant("Tips");
                            var content = $translate.instant("FinishExamSuccess");
                            csdToaster.pop('success', content, '');
                            //openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
                            //change the button state and the current order state.
                            if (successCallBack) {
                                successCallBack();
                            }
                        })
                            .error(function () {
                                var title = $translate.instant("Tips");
                                var content = $translate.instant("FinishExamFailed");
                                //openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, title, content);
                                csdToaster.pop('error', content, '');
                                console.log("Finish Exam failed.");
                            });
                    }
                });
            });
        },
        transferBookingToReg: function (orderId, successCallback) {
            registrationService.transferBookingToReg(orderId).success(function () {
                successCallback.call();
            })
            .error(function () {

            });
        },
        openPACSImageViewer: function (procedureID) {
            if (application.clientConfig && application.clientConfig.integrationType == 2) {
                reportService.getPacsUrl(procedureID).success(function (data) {
                    var pacsURL = data;
                    if (pacsURL == "") {
                        //
                        openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant("CallImageFail"));
                        return;
                    }
                    $window.open(pacsURL);
                });
            }
            else if (application.clientConfig && application.clientConfig.integrationType == 1) {
                reportService.getPacsUrlDX(procedureID).success(function (data) {
                    var pacsURL = data;
                    if (pacsURL == "") {
                        //
                        openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant("Tips"), $translate.instant("CallImageFail"));
                        return;
                    }
                    $window.open(pacsURL);
                });
            }
        },
        IdCardValidate: IdCardValidate,
        printRequisition: function (status, accNo, modalityType) {
            var templateType = status === 10 ? '1' : '2';
            reportService.getOtherReportPrintID(accNo, modalityType, templateType, loginContext.site).success(function (data) {
                if (!data) {
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant('NoPrintTemplate').replace('{0}', $translate.instant('PrintRequisition')));
                    return;
                }

                busyRequestNotificationHub.requestStarted();
                var param = {
                    accno: accNo,
                    modalityType: modalityType,
                    templateType: templateType,
                    site: loginContext.site,
                    url: loginContext.apiHost + '/api/v1/report/other/printdata',
                    printer: application.clientConfig.noticePrinter?application.clientConfig.noticePrinter:null
                };
                clientAgentService.printOther(param).success(function () {
                    busyRequestNotificationHub.requestEnded();
                }).error(function () {
                    busyRequestNotificationHub.requestEnded();
                });
            });
        },
        printBarCode: function (status, accNo, modalityType) {
            var isBooking = status === enums.rpStatus.noCheck;
            var templateType = isBooking ? '4' : '5';

            reportService.getOtherReportPrintID(accNo, modalityType, templateType, loginContext.site).success(function (data) {
                if (!data) {
                    openDialog.openIconDialog(openDialog.NotifyMessageType.Warn, $translate.instant('Alert'), $translate.instant('NoPrintTemplate').replace('{0}', $translate.instant('PrintBarCode')));
                    return;
                }

                busyRequestNotificationHub.requestStarted();

                var param = {
                    accno: accNo,
                    modalityType: modalityType,
                    templateType: templateType,
                    site: loginContext.site,
                    url: loginContext.apiHost + '/api/v1/report/other/printdata',
                    printer: application.clientConfig.barcodePrinter?application.clientConfig.barcodePrinter:null
                };
                clientAgentService.printOther(param).success(function () {
                    busyRequestNotificationHub.requestEnded();
                }).error(function () {
                    busyRequestNotificationHub.requestEnded();
                });
            });
        },
        viewRequisition: function (accNo) {
            registrationService.validateFtp().success(function (result) {
                if (!result) {
                    //ftp is not valid
                    showFTPError();
                }
                else {
                    registrationService.downLoadRequisitionFiles(accNo).success(function (result) {
                        if (result.length > 0) {
                            // requisition fileName rule:ERNO+00N+".jpg"
                            var erNo = result[0].fileName.substr(0, result[0].fileName.length - 7);
                            var args = { requisitionFiles: result, erNo: erNo, isViewRequisition: true };
                            var newScope = $rootScope.$new(true);
                            newScope.args = args;
                            showRequisition(args, newScope);
                        }
                        else {
                            openDialog.openIconDialog(openDialog.NotifyMessageType.Error, $translate.instant('Tips'), $translate.instant('NoRequisitions'));
                        }
                    });
                }
            });
        },
        showRequisition: showRequisition,
        showFTPError: function () {
            showFTPError();
        }
    };
}
])
commonModule.service('loginUser', ['$log', 'loginContext', 'consultationService', 'configurationService', 'constants', '$q',
    function ($log, loginContext, consultationService, configurationService, constants, $q) {
        'use strict';
        $log.debug('loginUser.ctor()...');
        var service = this;
        service.user = {};
        service.hasPermission = function () {
            return service.user && service.user.permissions && _.some(_.intersection(service.user.permissions, arguments));
        }
        service.initPermissions = function () {
            service.isSuperAdmin = service.user.defaultRoleID == constants.adminRoleID;
            service.isSiteAdmin = service.user.defaultRoleID == constants.siteAdminRoleID;

            service.isExpert = service.user.defaultRoleID == constants.expertRoleId;
            service.isConsAdmin = service.user.defaultRoleID == constants.consAdminRoleId;
            service.isDoctor = service.user.defaultRoleID == constants.doctorRoleId;
            if (_.findWhere(service.user.roles, { uniqueID: constants.doctorRoleId })) {
                service.hasDoctorRole = true;
            }
            service.canAccessConslution = service.isSuperAdmin || service.isSiteAdmin || service.isExpert || service.isConsAdmin || service.isDoctor;

            service.isAdmin = service.user.risRole === constants.risRole.admin;
            service.isSenior = service.user.risRole === constants.risRole.senior
            service.isIntermediate = service.user.risRole === constants.risRole.intermediate
            service.isJunior = service.user.risRole === constants.risRole.junior
            service.isTechnician = service.user.risRole === constants.risRole.technician
            service.isRegistrar = service.user.risRole === constants.risRole.registrar
            service.isNurse = service.user.risRole === constants.risRole.nurse
            service.isClinician = service.user.risRole === constants.risRole.clinician
            service.isSiteAdmin = service.user.risRole === constants.risRole.siteAdmin
            service.isGlobalAdmin = service.user.risRole === constants.risRole.globalAdmin
        }

        service.gerLicensePromise = configurationService.getLicenseData().success(function (data) {
            service.license = data;
        });

        service.getUserPromise = function (userid) {
            return consultationService.getUser(userid).success(function (data) {
                data.permissions = _.reduce(data.roles, function (a, b) { return a + ',' + b.permissions; }, '');
                if (data.permissions) {
                    data.permissions = _.uniq(data.permissions.slice(1).split(','));
                }
                service.user = data;
                service.initPermissions();
            })
        };

        consultationService.getDams().success(function (data) {
            service.damWebApiUrl = '';
            if (data && data.length > 0) {
                service.damWebApiUrl = data[0].webApiUrl;
            }
        });
    }])
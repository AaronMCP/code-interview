commonModule.factory('profileManager', ['$log', 'configurationService', 'loginContext', '$q',
    function ($log, configurationService, loginContext, $q) {

        $log.debug('profileManager.ctor()...');

        var resolve = function (profileName, profiles, deferred) {
            if (profiles != null && profiles.length > 0) {
                var profile = _.findWhere(profiles, { name: profileName });
                if (profile) {
                    deferred.resolve(profile.value);
                    return true;
                }
            }
            return false;
        };

        return {
            getProfileValue: function (name) {
                var deferred = $q.defer();
                configurationService.getProfileValues(loginContext.userId, [name]).success(function (data) {
                    deferred.resolve(data[name.toLowerCase()]?data[name.toLowerCase()].value:null);
                }).error(function () {
                    deferred.reject("Cannot find profile value!");
                });
                return deferred.promise;
            },
            getProfileValues: function (names) {
                return configurationService.getProfileValues(loginContext.userId, names);
            }
        };
    }])
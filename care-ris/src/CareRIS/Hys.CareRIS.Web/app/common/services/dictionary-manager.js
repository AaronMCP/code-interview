commonModule.factory('dictionaryManager', ['$log', 'configurationService', 'loginContext', '$q', 'enums',
    function ($log, configurationService, loginContext, $q, enums) {

        $log.debug('dictionaryManager.ctor()...');
        return {
            getDictionariesByTags: function (dictionaryTags) {
                var deferred = $q.defer();
                configurationService.getDictionariesByTags(loginContext.site, dictionaryTags).success(function (data) {
                    if (data != null && data.length > 0) {
                        deferred.resolve(data);
                    } else {
                        deferred.reject('Cannot find dictionary value by tags');
                    }
                }).error(function () {
                    deferred.reject('Cannot find dictionary value by tags');

                });
                return deferred.promise;
            },
            getValuesByTag: function (dictionaryValues, tag) {
                var dict = _.findWhere(dictionaryValues, {
                    tag: tag
                });
                if (dict) {
                    return dict.values;
                }
                return null;
            },
            getDictionaries: function (dictionaryTag) {
                var deferred = $q.defer();
                configurationService.getDictionaries(loginContext.site).success(function (data) {
                    if (data != null && data.length > 0) {
                        var dictionary = _.findWhere(data, {
                            tag: dictionaryTag
                        });
                        if (dictionary) {
                            deferred.resolve(dictionary.values);
                        }
                    }
                    deferred.reject('Cannot find dictionary value by tag ' + dictionaryTag);
                }).error(function () {
                    deferred.reject('Cannot find dictionary value by tag ' + dictionaryTag);

                });
                return deferred.promise;
            },
            getDictionariesByTags: function (tags) {
                var promise = configurationService.getDictionariesByTags(loginContext.site, tags);
                return promise;
            },
        };
    }
])
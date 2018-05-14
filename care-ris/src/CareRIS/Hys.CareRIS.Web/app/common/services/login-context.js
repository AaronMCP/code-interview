commonModule.factory('loginContext', [
    '$log', '$cookies', '$location',
    function ($log, $cookies, $location) {
        $log.debug('loginContext.ctor()...');
        var apiTokenObj = JSON.parse($('#server-app-config').val());

        var serverUrl = $('#server-app-host').val();
        var protocol = $location.protocol();
        var host = $location.host();
        var port = $location.port();
        var uriPart = [protocol, '://', host]
        port && uriPart.push(':', port);
        uriPart.push(serverUrl);

        var context = {
            apiHost: apiTokenObj.WebApiHostUrl,
            agentHost: apiTokenObj.AgentHost,
            agentVersion: apiTokenObj.AgentVersion,
            printHtml: apiTokenObj.PrintReportUrl,
            printService: apiTokenObj.PrintOtherReportUrl,
            cardService: apiTokenObj.CardReaderServiceUrl,
            serverUrl: uriPart.join(''),
            domain: '',
            site: '',
            userName: '',
            roleName: '',
            password: '',
            localName: '',
            userId: '',
            user: {}
        };

        var auth = $cookies.auth;
        if (auth) {
            context.auth = JSON.parse(auth);
            context.auth.authorized = true;
        }
        return context;
    }])
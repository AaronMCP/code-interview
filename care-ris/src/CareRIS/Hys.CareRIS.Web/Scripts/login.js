(function ($) {
    /**
     * KeyUp with delay event setup
     * 
     * @link http://stackoverflow.com/questions/1909441/jquery-keyup-delay#answer-12581187
     * @param function callback
     * @param int ms
     */
    $('#loginDHXG').hide();
    $.fn.delayKeyup = function (callback, ms) {
        $(this).keyup(function (event) {
            var srcEl = event.currentTarget;
            if (srcEl.delayTimer)
                clearTimeout(srcEl.delayTimer);
            srcEl.delayTimer = setTimeout(function () { callback($(srcEl)); }, ms);
        });

        return $(this);
    };

    $.RisProReady = function (ready) {
        $.RisProReady.readyList.push(ready);
    };

    $.RisProReady.readyList = [];
    $.languageSource = {};
    $.clientId = '';
    $.convertLangToCountry = function (lang) {
        if (lang.toLowerCase() === 'zh-cn') {
            return 'cn';
        } else if (lang.toLowerCase() === 'en-us') {
            return 'us';
        } else {
            return 'cn';
        }
    };

    $.convertCountryToLang = function (country) {
        if (country == 'cn') {
            return 'zh-cn';
        } else if (country == 'us') {
            return 'en-us';
        } else {
            return 'zh-cn';
        }
    };
})(jQuery);



$(function () {
    $.webapiUrl = $('#webapiUrl').val();

    var lang = Cookies.get('lang') || navigator.language || navigator.userLanguage || 'zh-cn';
    lang = lang.toLowerCase();

    Cookies.set('lang', lang, { expires: 7 });

    var urlPrefix = $('#urlPrefix').val();
    var absolutePath = function (relativePath) {
        return urlPrefix + relativePath;
    };
    var langSource = {
        'zh-cn': absolutePath('app-resources/i18n/zh-cn/login.json'),
        'en-us': absolutePath('app-resources/i18n/en-us/login.json')
    };
    var transElements = $('[trans]');
    var jsonReg = /^\{(['"]?)prop\1:(['"])\w+\2,(['"]?)value\3:(['"])\w+\4\}$/g;
    var langUri = langSource[lang] || langSource['zh-cn'];

    var clientIdDeffer = axios.get($.webapiUrl + '/api/v1/common/clientid', { responseType: 'text', isBusyRequest: true }).
        then(function (result) {
            $.clientId = result.data;
        });
    var langDeffer = axios.get(langUri).then(function (result) {
        var data = result.data;
        transElements.each(function () {
            var ele = $(this);
            var trans = ele.attr('trans');
            trans = trans.replace(/\s/g, '');
            var translated = '';
            if (jsonReg.test(trans)) {
                trans = (new Function('return ' + trans))();
                jsonReg.lastIndex = 0;
                var prop = trans.prop;
                var val = trans.value;
                translated = data[val] || '';
                ele.attr(prop, translated);
            } else {
                translated = data[trans] || '';
                ele.text(translated);
            }
        });

        $.languageSource = data;
    });
    axios.all([clientIdDeffer, langDeffer]).then(function () {
        var readyListLen = $.RisProReady.readyList.length;
        for (var i = 0; i < readyListLen; ++i) {
            var ready = $.RisProReady.readyList[i];
            if ($.isFunction(ready)) {
                ready();
            }
        }
    });
});

$.RisProReady(function () {

    // store login info.
    var loginModel = {};
    var checkResult = {};

    var resetLoginModel = function (email, password, realName, language) {
        var isForce = 0;
        if (loginModel && loginModel.isForce) {
            isForce = loginModel.isForce;
        }

        loginModel = {
            email: email,
            password: password,
            realName: realName,
            language: language,
            isForce: isForce
        };
    }

    var responseType = {
        Success: 0,
        EmptyEmail: 1,
        EmptyPassword: 2,
        EmailNotExist: 3,
        WrongPassword: 4,
        Inactive: 5,
        Expired: 6,
        UnKnown: 7,
        SameUserLogin: 8,
        MaxOnline: 9,
        SameLocationLogin: 10,
        IsLocked: 11,
        CannotGetLicenseData: 12,
        LicenseExpired: 13,
        EmptyRealName: 14
    };

    var resources = $.languageSource;

    var getUrlParm = function (name) {
        var searchUrl = window.location.search;
        if (searchUrl && searchUrl.length > 0) {
            var paramList = searchUrl.substr(1).split('&');
            for (var i = 0; i < paramList.length; i++) {
                var pair = paramList[i].split('=');
                if (pair && pair.length > 1 && pair[0] === name) {
                    return decodeURIComponent(pair[1]);
                }
            }
        };
        return '';
    };


    // invoke login action
    var login = function (sender, failCallBack) {
        sender.attr('disabled', true);

        var params = {
            grant_type: 'password',
            username: loginModel.email,
            password: loginModel.password,
            client_id: $.clientId
        };

        $('#loginDHXG').show();
        axios.post($.webapiUrl + '/oauth2/token', $.param(params)).then(function (result) {
            var res = result.data;
            res.clientId = $.clientId;
            Cookies.set('auth', JSON.stringify(res));
            window.location.replace('/careris/' + loginModel.language);
        }).catch(function (error) {
            $('#loginDHXG').hide();
            if (error.response) {
                if (error.response.status == 400) {
                    if (error.response.data.error_description != null) {
                        alert(error.response.data.error_description+'');
                    }
                    else {
                        alert('用户名或密码错误！');
                    }                
                }
            } else if (error.request) {
                alert('网络连接失败！')
            } else {
                alert('系统错误，请联系管理员！');
            }
            sender.attr('disabled', false);
        });
    };

    var setErrorStyle = function (error, element) {
        if (element.hasClass('has-error')) {
            //element.parent().addClass('has-error');
            error.insertAfter(element);
            //error.addClass('label');
            error.addClass('label-danger');
            error.css('font-size', '14px');

            error.css('width', '310px');
            error.css('word-break', 'break-all');
            error.css('color', 'white');
            error.css('padding', '.2em .6em .3em');
            error.css('border-radius', '.25em');
            error.css('font-weight', 'bold');

            //set text
            if (checkResult.CheckState === responseType.SameLocationLogin) {
                error[0].innerText = resources.sameLocationLogin.replace('{0}', checkResult.Message);
            }
        }
    };

    // add validation: server side 
    // Case: Email not exists
    $.validator.addMethod('emailNotExists', function () {
        return checkResult.CheckState !== responseType.EmailNotExist;
    }, resources.emailNotExists);

    // Case: Wrong password
    $.validator.addMethod('wrongPassword', function () {
        return checkResult.CheckState !== responseType.WrongPassword;
    }, resources.wrongPassword);

    // Case: Inactive account
    $.validator.addMethod('inactiveAccount', function () {
        return checkResult.CheckState !== responseType.Inactive;
    }, resources.inactiveAccount);

    // Case: Find the user if in the proper database
    $.validator.addMethod('expiredAccount', function () {
        return checkResult.CheckState !== responseType.Expired;
    }, resources.expiredAccount);

    // Case: Find the user if in the proper database
    $.validator.addMethod('unknownError', function () {
        return checkResult.CheckState !== responseType.UnKnown;
    }, resources.unknownError);

    // Case: 
    $.validator.addMethod('sameUserLogin', function (params) {

        return checkResult.CheckState !== responseType.SameUserLogin;
    }, resources.sameUserLogin);

    // Case: 
    $.validator.addMethod('maxOnline', function () {
        return checkResult.CheckState !== responseType.MaxOnline;
    }, resources.maxOnline);

    // Case: 
    $.validator.addMethod('sameLocationLogin', function () {
        return checkResult.CheckState !== responseType.SameLocationLogin;
    }, resources.sameLocationLogin);

    $.validator.addMethod('isLocked', function () {
        return checkResult.CheckState !== responseType.IsLocked;
    }, resources.isLocked);

    $.validator.addMethod('cannotGetLicenseData', function () {
        return checkResult.CheckState !== responseType.CannotGetLicenseData;
    }, resources.cannotGetLicenseData);

    $.validator.addMethod('licenseExpired', function () {
        return checkResult.CheckState !== responseType.LicenseExpired;
    }, resources.licenseExpired);


    var sameUserLoginParams = function () {
        var arrayMessage = checkResult.Message.split('&');
        if (arrayMessage.length == 2) {
            return arrayMessage;
        }

        return ['', ''];
    };

    var sameLocationLoginParams = function () {
        return checkResult.Message;
    };


    // add validation for full login
    var validatorFull = $("#loginFull").validate({
        errorClass: "has-error",
        rules: {
            loginFullEmail: {
                required: true,
                emailNotExists: true,
                inactiveAccount: true,
                expiredAccount: true,
                cannotGetLicenseData: true,
                licenseExpired: true
            },
            loginFullPwd: {
                required: true,
                wrongPassword: true,
                unknownError: true,
                sameUserLogin: sameUserLoginParams,
                maxOnline: true,
                sameLocationLogin: sameLocationLoginParams,
                isLocked: true
            },
            loginFullRealName: {
                required: true
            }
        },
        messages: {
            loginFullEmail: {
                required: resources.emailRequired,
                emailNotExists: resources.emailNotExists,
                inactiveAccount: resources.inactiveAccount,
                expiredAccount: resources.expiredAccount,
                cannotGetLicenseData: resources.cannotGetLicenseData,
                licenseExpired: resources.licenseExpired
            },
            loginFullPwd: {
                required: resources.passwordRequired,
                wrongPassword: resources.wrongPassword,
                unknownError: resources.unknownError,
                sameUserLogin: resources.sameUserLogin,
                maxOnline: resources.maxOnline,
                sameLocationLogin: resources.sameLocationLogin,
                isLocked: resources.isLocked,
            },
            loginFullRealName: {
                required: resources.realNameRequired
            }
        },
        errorPlacement: function (error, element) {
            setErrorStyle(error, element);
        },
        errorElement: 'div',
        onfocusout: function (element) {
        },
        onfocusin: function (element) { }
    });


    // full login: login with email and password
    $('#btnFullLogin').on('click', function (e) {
        e.preventDefault();

        resetLoginModel($('#loginFullEmail').val(), $('#loginFullPwd').val(), $('#loginFullRealName').val(), 'zh-cn');

        login($(this), function (checkState) {
            if (checkState === responseType.EmptyRealName) {
                $("#realNameContainer").show();
            }

            if (!$('#loginFull').valid()) {
                validatorFull.focusInvalid();
                return;
            }
            checkResult = {};
        });
    });

    // HotKey: listen to Enter/Up/Down key events
    $(document.body).keydown(function (e) {
        var btnFullLogin = $('#btnFullLogin');
        if (e.which === 13) {
            e.preventDefault();
            if (btnFullLogin.is(':visible')) {
                btnFullLogin.click();
            }
        }
    });

});
// InvalidFocus
commonModule.factory('risInvalidFocus', function () {
    return function (form, nameList) {
        // set up event handler on the form element
        // find the first invalid element
        var name = _.find(nameList, function (name) {
            return form[name].$invalid;
        });
        var firstInvalid = document.getElementsByName(name)[0];
        // if we find one, set focus
        if (firstInvalid) {
            if (!$(firstInvalid).attr("tabindex")) {
                var tabindex = $(firstInvalid).parent().attr("tabindex");
                $(firstInvalid).attr("tabindex", tabindex);
            }
            firstInvalid.focus();
        }
    };
});

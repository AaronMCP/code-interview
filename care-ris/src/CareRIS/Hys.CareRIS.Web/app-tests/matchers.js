// usage for Jasmine v2.0 (the jasmine version is configured in Chutzpah.json)
// to add a custom match rule for each spec, since we need compare some complex object
beforeEach(function () {
    jasmine.Expectation.addMatchers({
        jsonEquals: function () {
            return {
                compare: function (actual, expected) {
                    var pass = JSON.stringify(actual) === JSON.stringify(expected);
                    var message = pass ? '' : 'Expected ' + JSON.stringify(expected) + ' , but actual is ' + JSON.stringify(actual);
                    return {
                        pass: pass,
                        message: message
                    }
                }
            }
        }
    });
});

//// NOTE: this usage is for Jasmine v1.3 only, add a custom matcher, example:
//beforeEach(function () {
//    this.addMatchers({
//        jsonEquals: function (expected) {
//            return JSON.stringify(this.actual) === JSON.stringify(expected);
//        }
//    });
//});
require('../app-resources/css/site.css');
require('../app-resources/css/worklist.css');
require('../app-resources/css/registration.css');
require('../app-resources/css/exam-module-view.css');
require('../app-resources/css/exam-add-item.css');
require('../app-resources/css/exam-viewer-view.css');
require('../app-resources/css/exam-info-view.css');
require('../app-resources/css/time-slice-view.css');
require('../app-resources/css/report.css');
require('../app-resources/css/report-edit-view.css');

require('../app/app.js');
require('../app/module.js');

var requireObj = require.context('../app/', true, /.*\.(css|js)$/);
var except = /(app|module)\.js$/g;
requireObj.keys().forEach(function(path) {
    if (except.test(path)) return;
    requireObj(path);
});

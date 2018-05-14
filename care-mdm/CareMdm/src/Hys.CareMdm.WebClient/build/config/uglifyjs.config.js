let appConfig = require('./app.config.js');
let config = {
    minimize: true,
    compress: {
        drop_console: true
    },
    mangle: true,
    comments: false
}

if (appConfig.isDev || appConfig.debug) {
    config.minimize = false;
    config.beautify = { beautify: true };
    config.comments = true;
    config.sourceMap = true;
}
module.exports = config;
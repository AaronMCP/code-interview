var WebpackDevServer = require("webpack-dev-server");
var webpack = require("webpack");
var webpackConfig = require('../webpack/webpack.config.dev.js');
let packageConfig = require('../../package.json');

var compiler = webpack(webpackConfig);
var host = 'localhost';

var port = packageConfig.config.port;
new WebpackDevServer(compiler, {
    hot: true,
    quiet: false,
    noInfo: false,
    publicPath: webpackConfig.output.publicPath,
    stats: 'minimal'
}).listen(port, host, function(error, result) {
    if (error) {
        console.log(error);
    }
    console.log('webpack dev server http://%s:%s', host, port);
});
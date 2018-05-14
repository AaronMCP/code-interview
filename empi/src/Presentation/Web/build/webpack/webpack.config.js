const webpack = require('webpack');
let webpackCommonConfig = require('./common.config.js');
const appConfig = require('../config/app.config.js');
const uglifyJsConfig = require('../config/uglifyjs.config.js');

if (appConfig.isDev || appConfig.debug) {
  webpackCommonConfig.devtool = 'source-map';
}
if (appConfig.isPro) {
  if (!appConfig.debug) {
    webpackCommonConfig.plugins.push(
      new webpack.LoaderOptionsPlugin({
        minimize: true,
        debug: false
      }),
      new webpack.optimize.MinChunkSizePlugin({ minChunkSize: 100000 })
    );
  }
  webpackCommonConfig.plugins.push(new webpack.optimize.UglifyJsPlugin(uglifyJsConfig))
}

module.exports = webpackCommonConfig;
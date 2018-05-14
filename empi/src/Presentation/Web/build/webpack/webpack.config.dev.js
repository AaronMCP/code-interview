const webpack = require('webpack');
const path = require('path');

const packageConfig = require('../../package.json');
let webpackCommonConfig = require('./common.config.js');

webpackCommonConfig.entry = {
  dev: ['webpack-dev-server/client', 'webpack/hot/dev-server'],
  ...webpackCommonConfig.entry
}
webpackCommonConfig.output.publicPath = '/';
webpackCommonConfig.devtool = 'source-map';

webpackCommonConfig.plugins.push(
  new webpack.HotModuleReplacementPlugin(),
  new webpack.NoEmitOnErrorsPlugin(),
  new webpack.NamedModulesPlugin()
);

module.exports = webpackCommonConfig;
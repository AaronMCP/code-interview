const webpack = require('webpack');
const autoprefixer = require('autoprefixer');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const path = require('path');
const {
  CheckerPlugin
} = require('awesome-typescript-loader');
const appConfig = require('../config/app.config.js');
const ExtractTextPlugin = require('extract-text-webpack-plugin');

const needSourceMap = !appConfig.isPro || appConfig.debug;
const isProduction = appConfig.isPro && !appConfig.debug;

const extractCSS = new ExtractTextPlugin({
  filename: '[name]-[contenthash].css',
  allChunks: true
});
const extractScss = new ExtractTextPlugin({
  filename: '[name]-[contenthash].css',
  allChunks: true
});
let webpackConfig = {
  context: path.join(__dirname, '../../'),
  entry: {
    vendor: ['babel-polyfill', 'react', 'react-dom', 'mobx', 'mobx-react'],
    app: './build/launcher/index.tsx'
  },
  output: {
    filename: '[name]-[hash].js',
    chunkFilename: '[name]-[hash].js',
    path: path.join(__dirname, '../dist')
  },
  externals: [
    require('webpack-require-http')
  ],
  resolve: {
    extensions: ['.ts', '.tsx', '.js', '.jsx', '.scss', '.css']
  },
  plugins: [
    new webpack.LoaderOptionsPlugin({
      options: {
        context: path.join(__dirname, '../../'),
        tslint: {
          emitErrors: true,
          failOnHint: true,
          typeCheck: true
        },
        postcss: [autoprefixer({
          browsers: ['last 3 versions']
        })]
      }
    }),
    new webpack.DefinePlugin({
      'process.env': {
        'NODE_ENV': JSON.stringify(isProduction ? 'production' : 'development')
      },
      APPCONFIG: appConfig
    }),
    new CheckerPlugin(),
    new HtmlWebpackPlugin({
      minify: {
        minifyCSS: true,
        minifyJS: (appConfig.isDev || appConfig.debug) ? false : true
      },
      cache: true,
      filename: 'index.html',
      template: './build/launcher/index.html',
      //chunks: ['vendor', 'app']
    }),
    new webpack.optimize.CommonsChunkPlugin({
      names: ['common', 'vendor'] // Specify the common bundle's name.
    }),
    extractCSS,
    extractScss
  ],
  module: {
    rules: [{
        test: /\.tsx?$/,
        exclude: /node_modules/,
        use: [{
            loader: 'babel-loader'
          },
          {
            loader: 'awesome-typescript-loader',
            options: {
              configFileName: 'tsconfig.json'
            }
          }
        ]
      },
      {
        test: /\.css$/,
        use: extractCSS.extract({
          fallback: 'style-loader',
          use: ['css-loader']
        })
      }, {
        test: /\.html$/,
        use: [
          'raw-loader',
          'html-minifier-loader'
        ]
      }, {
        test: /\.scss$/,
        use: extractScss.extract({
          fallback: 'style-loader',
          use: [{
              loader: 'css-loader',
              options: {
                sourceMap: needSourceMap
              }
            },
            {
              loader: 'postcss-loader',
              options: {
                sourceMap: needSourceMap,
                plugins: function () {
                  return [
                    require('autoprefixer')({
                      broswers: ['last 2 versions']
                    })
                  ];
                }
              }
            },
            {
              loader: 'sass-loader',
              options: {
                sourceMap: needSourceMap
              }
            }
          ]
        })
      }, {
        test: /\.woff/,
        loader: 'url-loader',
        options: {
          prefix: 'font/',
          limit: '10000',
          mimetype: 'application/font-woff'
        }
      }, {
        test: /\.ttf|eot|svg/,
        loader: 'file-loader',
        options: {
          prefix: 'font/'
        }
      }, {
        test: /\.png|jpg|gif/,
        loader: 'file-loader'
      }
    ]
  }
};

process.noDeprecation = true;
module.exports = webpackConfig;
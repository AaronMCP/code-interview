const path = require('path');
const webpack = require('webpack');
const autoprefixer = require('autoprefixer');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const appConfig = require('./config.js');
const extractCSS = new ExtractTextPlugin({
    filename: '[name].css',
    allChunks: true
});

const config = {
    entry: {
        'login': './entry/login.js',
        'vendor-base': './entry/vendor-base.js',
        'vendor-extra': './entry/vendor-extra.js',
        'app': './entry/app.js'
    },
    output: {
        filename: '[name].js',
        chunkFilename: '[name].js',
        path: path.join(__dirname, './dist/script')
    },
    resolve: {
        extensions: ['.js', '.css'],
    },
    plugins: [
        new webpack.LoaderOptionsPlugin({
            options: {
                postcss: [autoprefixer({
                    browsers: ['last 3 versions', 'ie >= 9']
                })]
            }
        }),
        new webpack.IgnorePlugin(/vertx|jsdom|canvas|fs|xmldom/),
        extractCSS,
        new webpack.optimize.UglifyJsPlugin({
            minimize: true,
            compress: {
                drop_console: true
            },
            mangle: true,
            comments: false
        }),
        new webpack.DefinePlugin({
            'process.env': {
                'NODE_ENV': JSON.stringify('production')
            },
            APPCONFIG: appConfig
        }),
    ],
    module: {
        rules: [{
            test: /\.css$/,
            use: extractCSS.extract({
                fallback: 'style-loader',
                use: [{
                        loader: 'css-loader',
                        options: { minimize: true }
                    },
                    {
                        loader: 'postcss-loader',
                        options: {
                            plugins: function() {
                                return [
                                    require('autoprefixer')({
                                        broswers: ['last 3 versions', 'ie >= 9']
                                    })
                                ];
                            }
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
        }]
    }
};
module.exports = config;

let express = require('express');
let compression = require('compression');
let path = require('path');
let app = express();
let distFolder = path.join(__dirname, './dist');
let serveStatic = require('serve-static');
let packageConfig = require('./package.json');
let port = packageConfig.config.port;

app.use(compression());

app.use(serveStatic(distFolder, {
    maxAge: '7d',
    index: ['index.html'],
    etag: false,
    setHeaders: function(res, path, stat) {
        if (path.indexOf('index.html') >= 0) {
            res.setHeader('Cache-Control', 'max-age=0,no-cache,no-store,must-revalidate');
            res.setHeader('pragma', "no-cache");
        }
    }
}));

app.listen(port, () => {
    console.log('App is listening at http://localhost:%s', port);
});
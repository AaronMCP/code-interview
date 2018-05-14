const gulp = require('gulp');
const gulp_tslint = require('gulp-tslint');
const tslint = require("tslint");
const del = require('del');
const shell = require('gulp-shell');
const fs = require('fs-extra');
const path = require('path');

const DEV = 'development';
const PRO = 'production';

let env = process.env.NODE_ENV || DEV;
env = env.toLowerCase();

console.log(`Context Environment: (${env})`);

gulp.task('default', ['start']);
gulp.task('start', ['tslint'], shell.task(['node ./build/server/server.dev.js']));

gulp.task('build', ['compile'], () => {
  const serverFolder = path.join(__dirname, './build/server');
  const distFolder = path.join(__dirname, './build/dist');
  const deployFolder = path.join(__dirname, './build/deploy');
  const appPackageConfig = path.join(__dirname, './package.json');

  fs.copySync(distFolder, `${deployFolder}/dist`);
  fs.copySync(`${serverFolder}/server.js`, `${deployFolder}/server.js`);
  fs.copySync(`${serverFolder}/config.js`, `${deployFolder}/config.js`);
  const packageConfig = fs.readJsonSync(appPackageConfig);
  delete packageConfig.devDependencies;
  delete packageConfig.scripts;
  fs.outputJson(deployFolder + '/package.json', packageConfig);
});

gulp.task('tslint', () => {
  const program = tslint.Linter.createProgram("./tsconfig.json");
  return gulp.src(
    ['./app/**/*.{ts,tsx}', '!node_modules/**'], {
      base: '.'
    }
  ).pipe(gulp_tslint({
    program: program,
    formatter: "verbose"
  })).pipe(gulp_tslint.report({
    emitError: true,
    summarizeFailureOutput: true
  }));
});

gulp.task('clean', () => {
  return del('./build/dist/', {
    force: true
  }) && del('./build/deploy/', {
    force: true
  });
});

gulp.task('compile', ['clean', 'tslint'],
  shell.task(['webpack --config=./build/webpack/webpack.config.js'])
);
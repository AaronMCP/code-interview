const gulp = require('gulp');
const del = require('del');
const fs = require('fs-extra');
const shell = require('gulp-shell');

gulp.task('clean', () => {
    return del('./dist/', { force: true });
});
gulp.task('copy-html', ['clean'], () => {
    return gulp.src(['./app/**/*.html'])
        .pipe(gulp.dest('./dist/app'));
});
gulp.task('copy-json', ['copy-html'], () => {
    return gulp.src(['./app-resources/**/*.json'])
        .pipe(gulp.dest('./dist/app-resources'));
});
gulp.task('default', ['copy-json'], shell.task(['npm run webpack']));

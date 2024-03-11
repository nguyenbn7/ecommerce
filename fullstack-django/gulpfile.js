const gulp = require('gulp');

function copyBootstrapMinCSSTask() {
    return gulp.src('node_modules/bootstrap/dist/css/bootstrap.min.css')
        .pipe(gulp.dest('static/bootstrap'));
}

function copyBootstrapMinJSTask() {
    return gulp.src('node_modules/bootstrap/dist/js/bootstrap.min.js')
        .pipe(gulp.dest('static/bootstrap'));
}

function copyFontAwesomeMinCSSTask() {
    return gulp.src('node_modules/@fortawesome/fontawesome-free/css/all.min.css')
        .pipe(gulp.dest('static/fontawesome'));
}

exports.default = gulp.parallel(copyBootstrapMinCSSTask, copyBootstrapMinJSTask, copyFontAwesomeMinCSSTask);
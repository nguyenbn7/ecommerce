const gulp = require('gulp');

function copyBootstrapMinCSSTask() {
    return gulp.src('node_modules/bootstrap/dist/css/bootstrap.min.css')
        .pipe(gulp.dest('static/libs/bootstrap'));
}

function copyBootstrapMinJSTask() {
    return gulp.src('node_modules/bootstrap/dist/js/bootstrap.bundle.min.js')
        .pipe(gulp.dest('static/libs/bootstrap'));
}

function copyFontAwesomeMinCSSTask() {
    return gulp.src('node_modules/@fortawesome/fontawesome-free/css/all.min.css')
        .pipe(gulp.dest('static/libs/fontawesome/css'));
}

function copyFontAwesomeWebFontsTask() {
    return gulp.src('node_modules/@fortawesome/fontawesome-free/webfonts/*')
        .pipe(gulp.dest('static/libs/fontawesome/webfonts'));
}

function copyAnimateDotCSSTask() {
    return gulp.src('node_modules/animate.css/animate.min.css')
        .pipe(gulp.dest('static/libs/animate.css'));
}

exports.default = gulp.parallel(copyBootstrapMinCSSTask, copyBootstrapMinJSTask, copyFontAwesomeMinCSSTask, copyFontAwesomeWebFontsTask, copyAnimateDotCSSTask);
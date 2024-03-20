const navbar = document.getElementById("navbar-app");
const thresholdPixel = 56;
const backgroundClass = "bg-primary";

document.addEventListener('scroll', (ev) => {
    if (window.scrollY >= thresholdPixel) {
        navbar.classList.add(backgroundClass);
    }
    else {
        navbar.classList.remove(backgroundClass);
    }
});
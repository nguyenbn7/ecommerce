import { Carousel } from "bootstrap";
const heroSection = document.getElementById("hero-section");

const carousel = new Carousel(heroSection, {
    ride: 'carousel',
    pause: false
});


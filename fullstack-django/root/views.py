from django.http import HttpRequest
from django.shortcuts import render


# Create your views here.
def index(request: HttpRequest):
    heroes = [
        {
            "subtitle": "Mega Sale Going On!",
            "heading": "Get December Discount",
            "image": "hero1.jpg",
        },
        {
            "subtitle": "Top quality board",
            "heading": "10% off your first order",
            "image": "hero2.jpg",
        },
        {
            "subtitle": "Unique style",
            "heading": "Fashionable and Reasonable Price",
            "image": "hero3.jpg",
        },
    ]

    features = [
        {
            "iconClass": "fa-solid fa-check",
            "name": "Quality Product",
            "description": "Product with long lifespan",
        },
        {
            "iconClass": "fa-solid fa-truck-fast",
            "name": "Free Shipping",
            "description": "When order over $100",
        },
        {
            "iconClass": "fa-solid fa-rotate",
            "name": "7-Day Return",
            "description": "Get refund within 3 days!",
        },
        {
            "iconClass": "fa-solid fa-phone-volume",
            "name": "24/7 Support",
            "description": "Get support all day",
        },
    ]

    return render(request, "root/home.html", {"heroes": heroes, "features": features})


def contact(request: HttpRequest):
    return render(request, "root/contact.html", {})

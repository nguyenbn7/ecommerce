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

    return render(request, "index.html", {"heroes": heroes})


def contact(request: HttpRequest):
    return render(request, "contact.html", {})

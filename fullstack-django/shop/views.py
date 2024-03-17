from django.http import HttpRequest
from django.shortcuts import render


# Create your views here.
def index(request: HttpRequest):
    return render(request, "shop/index.html")


def detail(request: HttpRequest, product_id: int):
    return render(request, "shop/detail.html")

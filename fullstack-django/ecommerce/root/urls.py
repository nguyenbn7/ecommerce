from django.urls import path

from . import views


urlpatterns = [
    path("", views.index, name="home-page"),
    path("contact", views.contact, name="contact-page"),
]

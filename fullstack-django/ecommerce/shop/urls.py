from django.urls import path

from . import views


urlpatterns = [
    path("", views.index, name="shop-page"),
    path("<int:product_id>", views.detail, name="shop-detail-page"),
]

from django.db import models


# Create your models here.
class Product(models.Model):
    name = models.TextField(null=False)
    description = models.TextField(null=True)
    price = models.DecimalField(max_digits=18, decimal_places=2)
    picture_url = models.TextField(null=False)


class ProductBrand(models.Model):
    name = models.TextField()


class ProductType(models.Model):
    name = models.TextField()

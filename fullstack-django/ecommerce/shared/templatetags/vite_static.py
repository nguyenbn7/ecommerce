import os

from django import template
from django.conf import settings
from django.templatetags.static import static

register = template.Library()


@register.simple_tag
def get_static_file(f_name: str):
    if not f_name:
        raise Exception("file name can not be empty")

    if settings.DEBUG:
        filename, file_ext = os.path.splitext(f_name)

        if not settings.VITE_STATIC_URL:
            raise Exception(f"Can not find VITE_STATIC_URL in settings")

        if not isinstance(settings.VITE_STATIC_URL, str):
            raise Exception(f"VITE_STATIC_URL should be a string")

        if settings.VITE_STATIC_URL[-1] != "/":
            raise Exception(f"VITE_STATIC_URL should have '/' at the end")

        if not settings.VITE_PUBLIC_ASSET_URL:
            raise Exception(f"Can not find VITE_PUBLIC_ASSET_URL in settings")

        if not isinstance(settings.VITE_PUBLIC_ASSET_URL, str):
            raise Exception(f"VITE_PUBLIC_ASSET_URL should be a string")

        if settings.VITE_PUBLIC_ASSET_URL[-1] != "/":
            raise Exception(f"VITE_PUBLIC_ASSET_URL should have '/' at the end")

        if filename[0] == "/":
            filename = filename[1:]

        if file_ext in [".js", ".css"]:
            return f"{settings.VITE_STATIC_URL}{filename}{file_ext}"

        return f"{settings.VITE_PUBLIC_ASSET_URL}{filename}{file_ext}"

    return static(f_name)

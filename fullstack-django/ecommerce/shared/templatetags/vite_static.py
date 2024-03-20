from django import template
from django.conf import settings

register = template.Library()


@register.simple_tag
def get_static_file(f_name: str):
    if settings.DEBUG:
        if not settings.VITE_DEV_SERVER:
            raise Exception(f"Can not find VITE_DEV_SERVER in settings")

        if not isinstance(settings.VITE_DEV_SERVER, str):
            raise Exception(f"VITE_DEV_SERVER should be a string")

        if not f_name:
            raise Exception("file name can not be empty")

        vite_url = settings.VITE_DEV_SERVER
        file_name = f_name

        if vite_url[-1] == "/":
            vite_url = vite_url[:-1]

        if file_name[0] == "/":
            file_name = file_name[1:]

        print(f"{vite_url}/{file_name}")
        return f"{vite_url}/{file_name}"

    file_name = f_name

    if file_name[0] == "/":
        file_name = file_name[1:]

    return f"/static/{file_name}"

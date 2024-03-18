from typing import Dict
from django import template
from django.http import HttpRequest

register = template.Library()


@register.simple_tag
def build_breadcrumb(request: HttpRequest, alias_dict: Dict[str, str] = None):
    paths = request.path.split("/")[1:]

    breadcrumb = {"root": "/"}

    for path in paths:
        if path:
            prev_path = list(breadcrumb.values())[-1]

            if prev_path != "/":
                if alias_dict and alias_dict.get(path, None):
                    breadcrumb[alias_dict[path]] = f"{prev_path}/{path}"
                else:
                    breadcrumb[path] = f"{prev_path}/{path}"
            else:
                if alias_dict and alias_dict.get(path, None):
                    breadcrumb[alias_dict[path]] = f"{prev_path}{path}"
                else:
                    breadcrumb[path] = f"{prev_path}{path}"

    return breadcrumb


@register.simple_tag
def get_page_title(breadcrumb: Dict[str, str]):
    return list(breadcrumb.keys())[-1]

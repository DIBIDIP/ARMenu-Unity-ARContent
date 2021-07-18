from django.contrib import admin
from django.urls import path, include
from django.conf import settings
from django.conf.urls.static import static

from django.conf.urls import url, include

urlpatterns = [
    path("", include("core.urls", namespace="core")), 
    path("admin/", admin.site.urls),]

if settings.DEBUG:
    urlpatterns += static(settings.MENU_URL, document_root=settings.MENU_ROOT)

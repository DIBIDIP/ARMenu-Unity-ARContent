from django.urls import path
from eateries import views as eateries_views

app_name = "core"
urlpatterns = [
    path("",eateries_views.mainPage, name ="menuSelect")]
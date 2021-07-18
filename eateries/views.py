from django.shortcuts import render
from django.urls import reverse
from django.core.paginator import Paginator
from . import models


def mainPage(request):
    return render(request, 'mainpage.html')

def menus(request):
    return render(request, 'Gamachi.html')
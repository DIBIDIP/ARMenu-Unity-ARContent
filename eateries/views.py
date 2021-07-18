from django.shortcuts import render, redirect
from django.urls import reverse
from django.core.paginator import Paginator
from . import models
from .models import eateries




def mainPage(request):
    return render(request, 'mainpage.html')

def menus(request):
    return render(request, 'Gamachi.html')

def video_list(request):
    video_list = eateries.objects.all()
    return render(request, 'video/video_list.html', {'video_list':video_list})


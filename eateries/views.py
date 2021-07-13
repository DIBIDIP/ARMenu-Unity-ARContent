from django.shortcuts import render
from django.core.paginator import Paginator
from . import models

def all_eateries(request):
    return render(request, 'mainpage.html')
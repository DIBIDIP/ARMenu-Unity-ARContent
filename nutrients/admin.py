from django.contrib import admin
from django.utils.html import mark_safe
from . import models

# Register your models here.

@admin.register(models.carbohydrate , models.protein, models.fat)
class ItemAdmin(admin.ModelAdmin):
    
    """ Item Admin Definition """
    #메뉴이름과 가격
    list_display = ("name", "used_by")

    def used_by(self, obj) :
        return obj.eateries.count()

    pass
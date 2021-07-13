from django.contrib import admin
from django.utils.html import mark_safe
from . import models


# Register your models here.
@admin.register(models.MenuName)
class ItemAdmin(admin.ModelAdmin):
    
    """ Item Admin Definition """
    # 기본정보 띄우기 :: 메뉴이름, 가격, 칼로리
    list_display = ("name", "price", "kcal")

    def used_by(self, obj) :
        return obj.eateries.count()

    pass


class PhotoInline(admin.TabularInline):
    
    model = models.Photo



    
@admin.register(models.Menu)
class RoomAdmin(admin.ModelAdmin):

    """ RoomAdmin Admin Definition """

    inlines = (PhotoInline,)

    fieldsets = (
        (
            "Basic Info",
            {
                "fields": (
                    "name",
                    "price",
                    "kcal",
                    "description",
                )
            },
        ),
        (
            "More About the Space",
            {"fields": ("eatenries", "allergies", "ingrdeients")},
        ),
    )

    list_display = (
        "name",
        "price",
        "kcal",
    )

    list_filter = (
        "allergies",
        "ingredients",
    )

    filter_horizontal = ("allergies", "ingredients",)



    def count_menus(self, obj):
        return obj.menus.count()

    count_menus.short_description = "Menus Count"

    def count_photos(self, obj):
        return obj.photos.count()

    count_photos.short_description = "Photo Count"




@admin.register(models.Photo)
class PhotoAdmin(admin.ModelAdmin):
    """ Photo Admin Definition """

    list_display = ('__str__', 'get_thumbnail' )
    def get_thumbnail(self, obj):
        return mark_safe(f'<img width="50px" src="{obj.file.url}" />')
    get_thumbnail.short_description = "Thumnail"
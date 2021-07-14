from django.db import models
from core import models as core_models

# Create your models here.

# 부모메소드
class AbstractItem(core_models.TimeStampedModel):
    
    """ Abstract Item """

    name = models.CharField(max_length=20)

    class Meta:
        abstract = True

    def __str__(self):
        return self.name



# 브랜드이름
class MenuName(AbstractItem):
    """  Brand Model Definition """

    class Meta:
        verbose_name_plural = "MenuName"

class Nutrient(AbstractItem):
    """ Nutrient Model Definition """

    class Meta:
        verbose_name_plural = "Nutrient"

class Allergy(AbstractItem):
    """ Allergy Model Definition """

    class Meta:
        verbose_name_plural = "Allergy"

class Ingredient(AbstractItem):
    """ Ingredient Model Definition """

    class Meta:
        verbose_name_plural = "Ingredient"


class Price(AbstractItem):
    """ Price Model Definition """

    class Meta:
        verbose_name_plural = "Price"


class Kcal(AbstractItem):
    """ Kcal Model Definition """

    class Meta:
        verbose_name_plural = "Kcal"


# 사진 데이터 등록하기 위함
class Photo(core_models.TimeStampedModel):
    
    """ Photo Model Definition """
    caption = models.CharField(max_length=80)
    file = models.ImageField(upload_to="food_photos")
    menu = models.ForeignKey("Menu", related_name="photos", on_delete=models.CASCADE)
    def __str__(self):
        return self.caption



class Menu(core_models.TimeStampedModel):
    """ Menu Model Definition """

    #메뉴명, 설명, 가격, 칼로리
    name = models.CharField(max_length = 140) #메뉴 이름 받기
    description = models.TextField()
    price = models.IntegerField()
    kcal = models.IntegerField()

    # 영양성분 묶음, 알레르기 묶음, 재료 묶음
    nutrients = models.ManyToManyField("Nutrient", related_name="menus",blank=True)
    allergies = models.ManyToManyField("Allergy", related_name="menus",blank=True)
    ingredients = models.ManyToManyField("Ingredient", related_name="menus",blank=True)

    def __str__(self):
        return self.name

    def save(self, *args, **kwargs):
        super().save(*args, **kwargs)
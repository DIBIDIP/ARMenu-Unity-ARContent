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
    price = models.IntegerField()
    description = models.TextField()


    # 여기서 부터 영양정보
    kcal = models.IntegerField() # 1회당 칼로리
    size = models.IntegerField() # 탄수화물
    carbohydrate = models.IntegerField() # 탄수화물
    sugars = models.IntegerField() # 당
    protain = models.IntegerField() # 단백질
    fat = models.IntegerField() # 지방
    saturated_Fat = models.IntegerField() # 포화지방
    trans_Fat = models.IntegerField() # 트랜스지방
    cholesterol = models.IntegerField() # 콜레스테롤
    sodium = models.IntegerField() # 나트륨
    

    #원재료는 한 칸에 작성하기, ',' 단위로 작성할 것을 권유해야 함
    ingredients = models.TextField() #메뉴 원재료 종류


    # 알레르기 리스트에서 선택하기
    allergies = models.ManyToManyField("Allergy", related_name="menus",blank=True)

    def __str__(self):
        return self.name

    def save(self, *args, **kwargs):
        super().save(*args, **kwargs)
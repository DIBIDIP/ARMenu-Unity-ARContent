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
class brandName(AbstractItem):
    """  Brand Model Definition """

    class Meta:
        verbose_name = "Brand Name"

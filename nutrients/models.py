from django.db import models
from core import models as core_models
# Create your models here.


class AbstractItem(core_models.TimeStampedModel):

    """ Abstract Item """

    name = models.CharField(max_length=80)

    class Meta:
        abstract = True

    def __str__(self):
        return self.name


class carbohydrate(AbstractItem):
    """  Carbohydrate Model Definition """
    
    class Meta:
        verbose_name_plural = "Carbohydrate"


class protein(AbstractItem):
    """  Protein Model Definition """
    
    class Meta:
        verbose_name_plural = "Protein"


class fat(AbstractItem):
    """  fat Model Definition """
    
    class Meta:
        verbose_name_plural = "fat"
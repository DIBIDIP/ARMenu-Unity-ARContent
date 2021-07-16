from django.core.management.base import BaseCommand
from eateries.models import Allergy


class Command(BaseCommand):

    help = "This command creates allergies"
    #     def add_arguments(self, parser):
    #   parser.add_argument(
    #       "--times", help="How many times do you want me to tell you that I love you?"
    #    )
    

    def handle(self, *args, **options):
        allergies = [
            "난류",
            "닭고기",
            "쇠고기",
            "토마토",
            "아황산류",
            "우유",
            "오징어",
            "고등어",
            "게",
            "새우",
            "조개류",
            "굴",
            "전복",
            "홍합",
            "메밀",
            "땅콩",
            "대두(콩류)",
            "밀",
            "호두",
            "잣",
        ]
        for a in allergies :
            Allergy.objects.create(name=a)
        self.stdout.write(self.style.SUCCESS(f"{len(allergies)} allergies create!"))
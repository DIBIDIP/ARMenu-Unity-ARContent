# Generated by Django 2.2.5 on 2021-07-13 13:11

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('nutrients', '0001_initial'),
    ]

    operations = [
        migrations.CreateModel(
            name='fat',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('created', models.DateTimeField(auto_now_add=True)),
                ('updated', models.DateTimeField(auto_now=True)),
                ('name', models.CharField(max_length=80)),
            ],
            options={
                'verbose_name_plural': 'fat',
            },
        ),
    ]
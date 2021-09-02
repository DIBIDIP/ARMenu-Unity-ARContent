using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARContent{
    [System.Serializable]
    public class ARRestaurant {
        public string _id;
        public string restaurantName;
        public string menuName;
        public string description;
        public string allergies;
        public string ingredients;
        public string cookingTime;
        public string releaseData;
        public string fileUrl;
        public string imgUrl;

        public string calories;
        public string totalCarbohydrate;
        public string totalSugars;
        public string protein;
        public string totalFat;
        public string SaturatedFat;
        public string transFat;
        public string cholesterol;
        public string sodium;

        public ARRestaurant(string id, string resName, string menuName, string desc, string aller, string ingre, 
                            string cal, string totalCar, string totalSu, string pro, string totalFat,
                            string sature, string traFat, string chol, string sodium, string cookingTime
                            , string reldata, string file, string img)
            {
                this._id = id;
                this.restaurantName = resName;
                this.menuName = menuName;
                this.description = desc;
                this.allergies = aller;
                this.ingredients = ingre;
                this.cookingTime = cookingTime;
                this.releaseData = reldata;
                this.fileUrl = file;
                this.imgUrl = img;
                this.calories = cal;
                this.totalCarbohydrate = totalCar;
                this.totalSugars = totalSu;
                this.protein = pro;
                this.totalFat = totalFat;
                this.SaturatedFat = sature;
                this.transFat = traFat;
                this.cholesterol = chol;
                this.sodium = sodium;
            }
    }
}

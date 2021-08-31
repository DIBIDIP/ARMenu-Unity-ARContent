var mongoose = require("mongoose");

const nutrientsSchema = new mongoose.Schema({
  nt_calories: { type: Number, required: true }, //100g당 칼로리
  nt_totalCarbohydrate: { type: Number, required: true }, //탄
  nt_totalSugars: { type: Number, required: true }, //당
  nt_protein: { type: Number, required: true }, //단
  nt_totalFat: { type: Number, required: true }, //지방
  nt_SaturatedFat: { type: Number, required: true }, //포화지방
  nt_transFat: { type: Number, required: true }, //트랜스지방
  nt_cholesterol: { type: Number, required: true }, //콜레스테롤
  nt_sodium: { type: Number, required: true }, //나트륨
});

const menuSchema = new mongoose.Schema({
  restaurantName: { type: String, required: true }, //음식점
  menuName: { type: String, required: true, trim: true, maxLength: 20 }, //메뉴이름
  description: { type: String, required: true, trim: true, maxLength: 40 }, //한 줄 간략 메뉴 설명
  allergies: [{ type: String, trim: true }], // 알레르기 (배열)
  ingredients: { type: String, required: true }, //원재료
  nutrients: nutrientsSchema, //영양성분

  cookingTime: { type: Number, required: true }, // 조리시간
  realeaseData: { type: Date, required: true }, // 출시일

  fileUrl: { type: String, required: true }, // 메뉴 동영상 URL
  imgUrl: { type: String, required: false }, // 메뉴 사진 URL
});

const Menu = mongoose.model("Menu", menuSchema);
export default Menu;

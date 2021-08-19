import mongoose from "mongoose";

const menuSchema = new mongoose.Schema({
  fileUrl: { type: String, required: true },
  imgUrl: { type: String, required: false },

  title: { type: String, required: true, trim: true, maxLength: 20 },
  description: { type: String, required: true, trim: true, maxLength: 40 },
  ingredients: [{ type: String, required: true }],
  hashtags: [{ type: String, trim: true }],

  nt_calories: { type: String, required: true },
  nt_totalCarbohydrate: { type: String, required: true },
  nt_totalSugars: { type: String, required: true },
  nt_protein: { type: String, required: true },
  nt_totalFat: { type: String, required: true },
  nt_SaturatedFat: { type: String, required: true },
  nt_transFat: { type: String, required: true },
  nt_cholesterol: { type: String, required: true },
  nt_sodium: { type: String, required: true },
});

const Menu = mongoose.model("Menu", menuSchema);

export default Menu;

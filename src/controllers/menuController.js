import Menu from "../models/Menu";

export const home = async (req, res) => {
  const menus = await Menu.find({}).sort({ title: "asc" });
  return res.render("home", { pageTitle: "Home", menus });
};

export const watch = async (req, res) => {
  const { id } = req.params;
  const menu = await Menu.findById(id);
  if (!menu) {
    return res.render("404", { pageTitle: "메뉴가 없어요." });
  }
  return res.render("watch", { pageTitle: menu.title, menu });
};

export const getEdit = async (req, res) => {
  const { id } = req.params;
  const menu = await Menu.findById(id);
  if (!menu) {
    return res.status(404).render("404", { pageTitle: "메뉴가 없어요." });
  }
  return res.render("edit", { pageTitle: `Edit: ${menu.title}`, menu });
};

export const postEdit = async (req, res) => {
  const { id } = req.params;
  const { title, description, allergies } = req.body;
  const menu = await Menu.exists({ _id: id });
  if (!menu) {
    return res.status(404).render("404", { pageTitle: "메뉴가 없어요." });
  }
  await Menu.findByIdAndUpdate(id, {
    title,
    description,
    hashtags: Menu.formatHashtags(hashtags),
  });
  return res.redirect(`/menus/${id}`);
};

export const getUpload = (req, res) => {
  return res.render("upload", { pageTitle: "메뉴 등록" });
};

/* 이미지등록 해야 함 */
export const postUpload = async (req, res) => {
  const { path: fileUrl } = req.file;
  const {
    title,
    description,
    ingredients,
    allergies,
    hashtags,
    nt_calories,
    nt_totalCarbohydrate,
    nt_totalSugars,
    nt_protein,
    nt_totalFat,
    nt_SaturatedFat,
    nt_transFat,
    nt_cholesterol,
    nt_sodium,
  } = req.body;
  try {
    await Menu.create({
      title,
      description,
      ingredients,
      fileUrl,
      ingredients,
      allergies,
      hashtags: Menu.formatHashtags(hashtags),
      nt_calories,
      nt_totalCarbohydrate,
      nt_totalSugars,
      nt_protein,
      nt_totalFat,
      nt_SaturatedFat,
      nt_transFat,
      nt_cholesterol,
      nt_sodium,
    });
    return res.redirect("/");
  } catch (error) {
    console.log(error);
    return res.status(400).render("upload", {
      pageTitle: "메뉴 등록",
      errorMessage: error._message,
    });
  }
};

export const deleteMenu = async (req, res) => {
  const { id } = req.params;
  await Menu.findByIdAndDelete(id);
  return res.redirect("/");
};

/* 제목이 아니라 알레르기로 서치할 수 있도록 고쳐야함 */
export const search = async (req, res) => {
  const { keyword } = req.query;
  let menus = [];
  if (keyword) {
    menus = await Menu.find({
      title: {
        $regex: new RegExp(`${keyword}$`, "i"),
      },
    });
  }
  return res.render("search", { pageTitle: "Search", menus });
};

/* 밑에는 테스트용임 */
export const gettestUpload = (req, res) => {
  return res.render("iU", { pageTitle: "image Test" });
};

export const posttestUpload = async (req, res) => {
  const { path: menuImageUrl } = req.file;
  try {
    await Menu.create({
      menuImageUrl,
    });
    return res.redirect("/");
  } catch (error) {
    console.log(error);
    return res.status(400).render("iU", {
      pageTitle: "image test",
      errorMessage: error._message,
    });
  }
};

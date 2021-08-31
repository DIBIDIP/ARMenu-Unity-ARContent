import express from "express";
import {
  searchRestaurant,
  index,
  searchAllergy,
  searchMenuName,
} from "./menuController";

const menuRouter = express.Router();

menuRouter.get("/all", index);
menuRouter.get("/allergyserch/:allergies", searchAllergy);
menuRouter.get("/menusearch/:menuname", searchMenuName);

module.exports = menuRouter;

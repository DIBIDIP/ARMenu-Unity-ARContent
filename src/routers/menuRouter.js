import express from "express";
import {
  watch,
  getUpload,
  getEdit,
  postEdit,
  postUpload,
  deleteMenu,
  gettestUpload,
  posttestUpload,
} from "../controllers/menuController";
import {
  protectorMiddleware,
  videoUpload,
  menuImageUpload,
} from "../middlewares";

const menuRouter = express.Router();

menuRouter.get("/:id([0-9a-f]{24})", watch);
menuRouter
  .route("/:id([0-9a-f]{24})/edit")
  .all(protectorMiddleware)
  .get(getEdit)
  .post(postEdit);
menuRouter
  .route("/:id([0-9a-f]{24})/delete")
  .all(protectorMiddleware)
  .get(deleteMenu);
menuRouter
  .route("/upload")
  .all(protectorMiddleware)
  .get(getUpload)
  .post(videoUpload.single("video"), postUpload);
menuRouter
  .route("/upload")
  .all(protectorMiddleware)
  .get(getUpload)
  .post(menuImageUpload.single("menuImage"), postUpload);

menuRouter
  .route("/iU")
  .all(protectorMiddleware)
  .get(gettestUpload)
  .post(menuImageUpload.single("menuImage"), posttestUpload);
export default menuRouter;

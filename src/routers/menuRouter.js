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
import { protectorMiddleware, fileUpload } from "../middlewares";

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
  .post(
    fileUpload.fields([{ name: "video" }, { name: "menuImage" }]),
    postUpload
  );

export default menuRouter;

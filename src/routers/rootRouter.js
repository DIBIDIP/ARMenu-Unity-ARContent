import express from "express";
import {
  t_postRegister,
  t_postLogin,
  t_getLogout,
  t_getAuth,
  t_getJoin,
  getJoin,
  postJoin,
} from "../controllers/userController";
import { home, search } from "../controllers/menuController";
import { publicOnlyMiddleware } from "../middlewares";

const rootRouter = express.Router();

rootRouter.get("/", home);
rootRouter.route("/api/users/register").post(t_postRegister);
rootRouter.route("/api/users/login").post(t_postLogin);
rootRouter.get("/search", search);

rootRouter.route("/join").get(t_getJoin).post(t_postRegister);
export default rootRouter;

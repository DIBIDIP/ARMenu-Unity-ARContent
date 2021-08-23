import express from "express";
import {
  t_postRegister,
  t_postLogin,
  t_getLogout,
  t_getAuth,
} from "../controllers/userController";
import {
  protectorMiddleware,
  publicOnlyMiddleware,
  avatarUpload,
} from "../middlewares";
const { auth } = require("../auth");

/* 아래는 test용 Router */
const t_userRouter = express.Router();

//localhost:5000 접속화면
t_userRouter.get("/", (req, res) => {
  res.send("Hello, Here is DBLAB with React");
});
t_userRouter.get("/api/hello", (req, res) => {
  res.send("Hello, Here is AR-Dish with React");
});

t_userRouter.route("api/users/auth").all(auth).get(t_getAuth);
t_userRouter.route("/api/users/logout").all(auth).get(t_getLogout);

export default t_userRouter;

import express from "express";
import morgan from "morgan";
import session from "express-session";
import MongoStroe from "connect-mongo";
import rootRouter from "./routers/rootRouter";
import menuRouter from "./routers/menuRouter";
import userRouter from "./routers/userRouter";
import { localsMiddleware } from "./middlewares";
import cookieParser from "cookie-parser";

const app = express();
const logger = morgan("dev");

app.set("view engine", "pug");
app.set("views", process.cwd() + "/src/views");
app.use(logger);
app.use(express.urlencoded({ extended: true }));

app.use(
  session({
    secret: process.env.COOKIE_SECRET,
    resave: false,
    saveUninitialized: false,
    store: MongoStroe.create({ mongoUrl: process.env.DB_URL }),
  })
);

app.use(localsMiddleware);
app.use("/uploads", express.static("uploads"));
app.use("/", rootRouter);
app.use("/menus", menuRouter);
app.use("/users", userRouter);

const t_app = express();
t_app.set("view engine", "pug");
t_app.set("views", process.cwd() + "/src/views");
t_app.use(localsMiddleware);
t_app.use("/", rootRouter);
t_app.use("/users", userRouter);
t_app.use("/menus", menuRouter);
t_app.use("/uploads", express.static("uploads"));
t_app.use(logger);
/* 밑에는 test_app  */
//application/x-www-form-urlencoded 로 넘어오는 정보를 분석해주기 위해 필요함
t_app.use(express.urlencoded({ extended: true }));
//application/json --> json 형태로 넘어오는걸 받기위해서 필요함
t_app.use(express.json());
t_app.use(cookieParser());
/* 여기까지 가져온 것 */

export default t_app;

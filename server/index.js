require("dotenv").config();
const express = require("express");
const cors = require("cors");
const bodyParser = require("body-parser");
const cookieParser = require("cookie-parser");

const app = express();
const path = require("path");
const config = require("./config/key");
const port = process.env.PORT || 3001;

const mongoose = require("mongoose");
mongoose.Promise = global.Promise;
const connect = mongoose
  .connect(config.mongoURI)
  .then(() => console.log("✔️ MongoDB 연결했어요 "))
  .catch((err) => console.log(err));

/* 패키지 적용 */
// 서버 차이 해결
app.use(cors());
// 가공된 형태로 보내고-받고-접근할 수 있도록 bodyParser 적용 => 디폴트값(Undefind)오류 해결
app.use(bodyParser.urlencoded({ extended: true }));
app.use(express.urlencoded({ extended: true }));
app.use(bodyParser.json());
//요청된 쿠키를 쉽게 추출하도록 함
app.use(cookieParser());

/* 기존 api */
app.use("/api/users", require("./routes/users"));
app.use("/api/favorite", require("./routes/favorite"));
app.use("/api/comment", require("./routes/comment"));
app.use("/api/like", require("./routes/like"));

/* 새로 만든 MENU api */
app.use("/api/menus", require("./api/menuRouter")); // api폴더 > menusRouter.js 내용 참고

app.use("/uploads", express.static("uploads"));

// Serve static assets if in production
if (process.env.NODE_ENV === "production") {
  // Set static folder
  app.use(express.static("client/build"));

  // index.html for all page routes
  app.get("*", (req, res) => {
    res.sendFile(path.resolve(__dirname, "../client", "build", "index.html"));
  });
}

app.listen(port, () => {
  console.log(
    `✔️ http://localhost:` + port + `, ${port}port에서 대기하고 있습니다.`
  );
});

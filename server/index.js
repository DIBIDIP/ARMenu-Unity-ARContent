require("dotenv").config(); // .env 파일 적용
const express = require("express");
const cors = require("cors"); // B-F 서버 차이(CORS)해결을 위한 미들웨어
const bodyParser = require("body-parser");
const cookieParser = require("cookie-parser");
const fs = require("fs-extra");

const app = express();
const path = require("path");
const config = require("./config/key");
const mongoose = require("mongoose");
const { PassThrough } = require("stream");
const os = require("os");

// const mongoose = require("mongoose");
// mongoose
//   .connect(config.mongoURI, { useNewUrlParser: true })
//   .then(() => console.log("DB connected"))
//   .catch(err => console.error(err));

const port = process.env.PORT;

mongoose.Promise = global.Promise;
const connect = mongoose
  .connect(config.mongoURI)
  .then(() => console.log("✔️ MongoDB 연결했어요 "))
  .catch((err) => console.log(err));

app.use(cors());

//to not get any deprecation warning or error
//support parsing of application/x-www-form-urlencoded post data
app.use(bodyParser.urlencoded({ extended: true }));
app.use(express.urlencoded({ extended: true }));
//to get json data
// support parsing of application/json type post data
app.use(bodyParser.json());
app.use(cookieParser());

app.use("/api/users", require("./routes/users"));
app.use("/api/favorite", require("./routes/favorite"));
app.use("/api/comment", require("./routes/comment"));
app.use("/api/like", require("./routes/like"));

/* api 생성 */
app.use("/api/menus", require("./api/menuRouter")); // api폴더 > menusRouter.js 내용 참고

//use this to show the image you have in node js server to client (react js)
//https://stackoverflow.com/questions/48914987/send-image-path-from-node-js-express-server-to-react-client
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

const port = process.env.PORT || 5000;

/* 서버 연결 */
app.listen(port, () => {
  console.log(
    `✔️ http://localhost:` + port + `, ${port}port에서 대기하고 있습니다.`
  );
});

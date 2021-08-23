import User from "../models/User";
import fetch from "node-fetch";
import bcrypt from "bcrypt";
import bodyParser from "body-parser";
import config from "../config/key";
import auth from "../auth";

/* test용 함수 */
export const t_postRegister = (req, res) => {
  const user = new User(req.body);
  user.save((err, userInfo) => {
    if (err) return res.json({ success: false, err });
    return res.status(200).json({
      success: true,
    });
  });
};

export const t_postLogin = (req, res) => {
  User.findOne({ email: req.body.email }, (err, user) => {
    if (!user) {
      return res.json({
        loginSuccess: false,
        message: "제공된 이메일에 해당하는 유저가 없습니다.",
      });
    }
    user.comparePassword(req.body.password, (err, isMatch) => {
      if (!isMatch)
        return res.json({
          loginSuccess: false,
          message: "비밀번호가 틀렸습니다.",
        });
    });
    user.generateToken((err, user) => {
      if (err) return res.status(400).send(err);
      res
        .cookie("x_auth", user.token)
        .status(200)
        .json({ loginSuccess: true, userId: user._id });
    });
  });
};

export const t_getAuth = (req, res) => {
  //여기까지 미들웨어를 통과해 왔다는 말은 Auth 가 True 라는 말
  res.status(200).json({
    _id: req.user._id,
    isAdmin: req.user.role === 0 ? false : true, //role 이 0 -> 일반유저 , role 이 0이 아니면 관리자
    isAuth: true,
    email: req.user.email,
    name: req.user.name,
    lastname: req.user.lastname,
    role: req.user.role,
    image: req.user.image,
  });
};

export const t_getLogout = (req, res) => {
  User.findOneAndUpdate({ _id: req.user._id }, { token: "" }, (err, user) => {
    if (err) return res.json({ success: false, err });
    return res.status(200).send({
      success: true,
    });
  });
};

/* 위까지 test용 함수 */

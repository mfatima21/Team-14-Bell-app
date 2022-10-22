const express = require("express");
const app = express();
const cors = require("cors");
const url = require("url");
const { application } = require("express");
const index = require("./routes/index.routes")

app.use(cors());

app.use(function (req, res, next) {
  res.header("Access-Control-Allow-Origin");
  res.header(
    "Access-Control-Allow-Headers",
    "Origin, X-Requested-With, Content-Type, Accept"
  );
  next();
});

app.use("/", index)

module.exports = app
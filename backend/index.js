const express = require("express");
const app = express();
const cors = require("cors");
const url = require("url");
const index = require("./routes/index.routes")
const bodyParser = require('body-parser');

app.use(cors());

app.use(function (req, res, next) {
  res.header("Access-Control-Allow-Origin");
  res.header(
    "Access-Control-Allow-Headers",
    "Origin, X-Requested-With, Content-Type, Accept"
  );
  next();
});

app.use(bodyParser.json())
app.use(bodyParser.urlencoded({ extended: true }));

app.use("/", index)

module.exports = app
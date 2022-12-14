const express = require("express");
const recipieController = require("../controller/recipieCreator.controller");
const recognizeImageController = require("../controller/imageRecognition.controller")
const upload = require("../middleware/upload.middleware")
const router = express.Router();

/**
 * GET /heatlh-check - Check health service
 */
router.get("/health-check", (req, res) => {
    return res.send("Ok");
});

router.route("/create-recipie/").post(recipieController.CreateRecipie);
router.route("/recognize-image/").post(upload.single("fridge_image"), recognizeImageController.extractIngredients);

module.exports = router;
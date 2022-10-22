const Recipe = require("../models/models.js");

const CreateRecipe = async (req, res) => {
    // Parse the input here

    // Process the inputs here

    // Create the list of recipes here
    const mock_recipe = new Recipe("Garlic soup", ["Garlic","Apples"]);
    let recipes = [mock_recipe];
    return res.json(recipes);
};

module.exports = {
    CreateRecipe: CreateRecipe,
};
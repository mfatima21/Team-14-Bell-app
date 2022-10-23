const { promisify } = require('util');
var exec =  promisify(require('child_process').exec)

/**
 * @function CreateRecipie
 * @param req
 * @param res
 */

/**
 * Frontend Developer Notes: Pass foodItems in the body.
 * Example: body: { foodItem: ["lettuce", "beef", "tomatoes", "beans"]}
 */
 const CreateRecipie = async (req, res) => {
    try {
        const ingredients = req.body.foodItems ? req.body.foodItems.join(" ") : "tomatoes pasta meat"
        exec("../backend/Archive/CreateSearchIndex " + `${ingredients}`, function(error, stdout, stderr) { 
            if (error || stderr) {
                return res.status(500).send({error: "server glitch" + error + stderr})
            }
            return res.status(200).send(JSON.parse(stdout)) 
        });
    } catch (err) {
        return res.status(500).send({error: "server glitch"})
    }
 }
module.exports = {
    CreateRecipie,
};
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
        const ingredients = req.body.join(" ");
        exec("../backend/Archive/CreateSearchIndex " + `${ingredients}`, function(error, stdout, stderr) { 
            if (error || stderr) {
                console.log(error + stderr)
                return res.status(500).send({error: "server glitch" + error + stderr})
            }
            return res.status(200).send(JSON.parse(stdout)) 
        });
    } catch (err) {
        console.log(err);
        return res.status(500).send({error: "server glitch"})
    }
 }
module.exports = {
    CreateRecipie,
};
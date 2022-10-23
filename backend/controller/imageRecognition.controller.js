var request = require('request');
const fs = require("fs")

const token = "6122abe151eedca9654ddbf1189f42e2b8e710fd"

/**
 * @function extractIngredients
 * @param req
 * @param res
 */

/**
 * Frontend Developer Notes: Pass the image and enctype as form data
 * Example:  'fridge_image': {
             'value': fs.createReadStream('/image.jpeg'),
             'options': {
                'filename': 'image.jpeg',
                'contentType': null
              }
             },
             'enctype': 'multipart/form-data'
 */

const extractIngredients = async (req, res) => {


    try {
        var options = {
            'method': 'POST',
            'url': 'https://api.logmeal.es/v2/image/segmentation/complete',
            'headers': {
                'Authorization': `Bearer ${token}`
            },
            formData: {
                'image': {
                'value': fs.createReadStream(req.file.path),
                'options': {
                    'filename': 'image.jpeg',
                    'contentType': null
                }
                }
            }
        };
    
        request(options, function (error, response) {
            if (response.statusCode != 200) {
                return res.status(response.statusCode).send({error: response.body});
            }
    
            var options = {
                'method': 'POST',
                'url': 'https://api.logmeal.es/v2/recipe/ingredients',
                'headers': {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    "imageId": JSON.parse(response.body).imageId
                })
            };
        
            request(options, (error, response) => {
                parsedData = JSON.parse(response.body).foodName
                return res.status(200).send({foodItems: parsedData});
            });
        });    
    } catch(err) {
        return res.status(500).send({error: "Server glitch" + err});
    }
};

module.exports = {
    extractIngredients
};
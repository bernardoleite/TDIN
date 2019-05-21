module.exports = async (newRequest) => {

    const Request = require ("./models/Request");

    const errHandler = (err) => {
        console.error("Error: ", err);
    }

    const request = await Request.create(newRequest).catch(errHandler);



}
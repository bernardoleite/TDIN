module.exports = async (newRequest) => {

    var amqp = require('amqplib/callback_api');

    amqp.connect('amqp://localhost', function(error0, connection) {
        if (error0) {
            throw error0;
        }
        connection.createChannel(function(error1, channel) {
            if (error1) {
                throw error1;
            }
    
            var queue = 'store_warehouse2';
            var msg = newRequest;
    
            channel.assertQueue(queue, {
                durable: false
            });
            channel.sendToQueue(queue, Buffer.from(msg));
    
            console.log(" [x] Sent %s", msg);
        });
        setTimeout(function() {
            console.log("it will close...");
            connection.close();
            process.exit(0);
        }, 500);
    });

}
var cfg = require("./cfg");
var cmd = require("./cmd");
var WebSocketClient = require('websocket').client;

var client = new WebSocketClient();

client.on('connectFailed', function(error) {
    console.log('Connect Error: ' + error.toString());
});

client.on('connect', function(connection) {
    console.log('WebSocket client connected');
    connection.on('error', function(error) {
        console.log("Connection Error: " + error.toString());
    });
    connection.on('close', function() {
        console.log('echo-protocol Connection Closed');
    });
    connection.on('message', function(message) {
        if (message.type === 'utf8') {
            console.log("Received: '" + message.utf8Data + "'");
        }
    });

    function sendNumber() {
        if (connection.connected) {

			//开通
			//var data = {
				//"cmd": cmd.SERV_OPEN_CLIENT,
				//"data": "192.168.1.105"
			//};
			
			//归档
			//var data = {
				//"cmd": cmd.SERV_CLOSE_CLIENT,
				//"data": "192.168.1.105"
			//};

			//sql注入测试
			var data = {
				"cmd": cmd.SERV_MENU_LIST,
				"data": "select * from client"
			};

            connection.sendUTF(JSON.stringify(data));
            //setTimeout(sendNumber, 1000);
        }
    }
    sendNumber();
});

client.connect('ws://localhost:8765/', 'echo-protocol');

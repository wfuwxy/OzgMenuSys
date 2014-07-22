
var http = require("http");
var WebSocketServer = require("websocket").server;
var cfg = require("./cfg");
var cmd = require("./cmd");
var strings = require("./strings");
var commons = require("./commons");
var serverdb = require("./serverdb");

var clientList = new Array(); //客户端列表
serverdb.setClientList(clientList);

var server = http.createServer(function(request, response) {
    console.log((new Date()) + " Received request for " + request.url);
    response.writeHead(404);
    response.end();
});

server.listen(cfg.SERVER_PORT, function() {
	console.log((new Date()) + " Server is listening");
});

var wsServer = new WebSocketServer({
    httpServer: server,
    autoAcceptConnections: false
});

function originIsAllowed(origin) {
	// put logic here to detect whether the specified origin is allowed.
	return true;
}

wsServer.on("request", function(request) {
    if (!originIsAllowed(request.origin)) {
		// Make sure we only accept requests from an allowed origin
		request.reject();
		console.log((new Date()) + " Connection from origin " + request.origin + " rejected.");
		return;
    }

    var connection = request.accept("echo-protocol", request.origin);
    console.log((new Date()) + " Connection accepted.");
	
	//添加客户端列表
	var existList = false;
	for(var i = 0; i < clientList.length; i++) {
		if(connection.remoteAddress == clientList[i].remoteAddress) {
			existList = true;
			break;
		}			
	}
	if(!existList)
		clientList.push(connection);
	
    connection.on("message", function(message) {
        if (message.type === "utf8") {
            //console.log("Received Message: " + message.utf8Data);
            //connection.sendUTF(message.utf8Data);

			var inputObj = null;
			try {
				inputObj = JSON.parse(message.utf8Data);
			}
			catch (err) {
				console.log("data error, data: " + message.utf8Data);
				connection.close();
				return;
			}

			if(inputObj.cmd == cmd.SERV_CHK_CLIENT) {
				serverdb.checkClient(connection); //检测客户端是否匹配服务器的数据

			}
			else if(inputObj.cmd == cmd.SERV_MENU_CLASS_LIST) {
				//菜单分类列表
				serverdb.getMenuClassList(connection);

			}
			else if(inputObj.cmd == cmd.SERV_MENU_LIST) {
				//菜单列表
				serverdb.getMenuList(connection, inputObj.data);

			}
			else if(inputObj.cmd == cmd.SERV_ORDER_LIST) {
				//订单列表
				serverdb.getOrderList(connection);

			}
			else if(inputObj.cmd == cmd.SERV_SMALL_IMAGE) {
				//获取小图
				serverdb.getMenuSmallImage(connection, inputObj.data);

			}
			else if(inputObj.cmd == cmd.SERV_BIG_IMAGE) {
				//获取大图
				serverdb.getMenuBigImage(connection, inputObj.data);

			}
			else if(inputObj.cmd == cmd.SERV_ADD_ORDER) {
				//下单
				serverdb.addOrderDetail(connection, inputObj.data.menu_id, inputObj.data.quantity);

			}
			else if(inputObj.cmd == cmd.SERV_PAYMENT) {
				//结账
				serverdb.orderPayment(connection);

			}
			else if(inputObj.cmd == cmd.SERV_OPEN_CLIENT) {
				//服务台开通一个客户端
				var clientIp = inputObj.data;
				serverdb.openClient(connection, clientIp);				

			}
			else if(inputObj.cmd == cmd.SERV_CLOSE_CLIENT) {
				//服务台归档一个客户端
				var clientIp = inputObj.data;
				serverdb.closeClient(connection, clientIp);				

			}
			else if(inputObj.cmd == cmd.SERV_ISEND_CLIENT) {
				//检测订单是否已归档
				serverdb.isEndClient(connection);				

			}
			else {
				//命令参数错误
				console.log(strings.COMMONS_MSG1);
			
				var outputStr = commons.outputJsonStr(0, strings.COMMONS_MSG1);
			
				connection.sendUTF(outputStr);
				connection.close();
			}
        }
        /*else if (message.type === 'binary') {
            console.log('Received Binary Message of ' + message.binaryData.length + ' bytes');
            connection.sendBytes(message.binaryData);
        }*/
    });

    connection.on("close", function(reasonCode, description) {
		
		//删除客户端列表
		var deleteIndex = -1;
		for(var i = 0; i < clientList.length; i++) {
			if(connection.remoteAddress == clientList[i].remoteAddress) {
				deleteIndex = i;
				break;
			}			
		}
		if(deleteIndex != -1)
			clientList.splice(deleteIndex, 1);
		//console.log(clientList.length);

        console.log((new Date()) + " Peer " + connection.remoteAddress + " disconnected.");
    });

});

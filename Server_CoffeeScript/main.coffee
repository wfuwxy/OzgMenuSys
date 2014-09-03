
http = require("http")
WebSocketServer = require("websocket").server
cfg = require("./cfg")
cmd = require("./cmd")
strings = require("./strings")
commons = require("./commons")
serverdb = require("./serverdb")

clientList = serverdb.clientList #客户端列表

server = http.createServer((request, response) ->
	console.log((new Date()) + " Received request for " + request.url)
	response.writeHead(404)
	response.end()
)

server.listen(cfg.SERVER_PORT, ->
	console.log((new Date()) + " Server is listening")
)

wsServer = new WebSocketServer({
	httpServer: server,
	autoAcceptConnections: false,
	maxReceivedFrameSize: 1024 * 1024,
	maxReceivedMessageSize: 1024 * 1024
})

originIsAllowed = (origin) ->
	#put logic here to detect whether the specified origin is allowed.
	return true

wsServer.on "request", (request) ->
	if !originIsAllowed(request.origin)
		#Make sure we only accept requests from an allowed origin
		request.reject()
		console.log((new Date()) + " Connection from origin " + request.origin + " rejected.")
		return

	connection = request.accept("echo-protocol", request.origin)
	console.log((new Date()) + " " + connection.remoteAddress + " Connection accepted.")
	
	#添加客户端列表
	existList = false
	for item in clientList
		if connection.remoteAddress == item.remoteAddress
			existList = true
			break
	
	if !existList
		clientList.push(connection)

	#向服务台发出响应数据
	#刷新服务台的数据
	outputStr = commons.outputJsonStr(1, null, cmd.CLIENT_GET_ONLINE_LIST)
	serverdb.writeInformationDeskData(outputStr)
	
	connection.on "message", (message) ->
		if message.type == "utf8"
			#console.log("Received Message: " + message.utf8Data)
			#connection.sendUTF(message.utf8Data)

			inputObj = null
			try
				inputObj = JSON.parse(message.utf8Data)
			catch err
				console.log("data error, data: " + message.utf8Data)
				connection.close()
				return
			
			if inputObj.cmd == cmd.SERV_CHK_CLIENT
				serverdb.checkClient(connection) #检测客户端是否匹配服务器的数据
			else if inputObj.cmd == cmd.SERV_MENU_CLASS_LIST
				#菜单分类列表
				serverdb.getMenuClassList(connection)
			else if inputObj.cmd == cmd.SERV_MENU_LIST
				#菜单列表
				serverdb.getMenuList(connection, inputObj.data)
			else if inputObj.cmd == cmd.SERV_ORDER_LIST
				#订单列表
				serverdb.getOrderList(connection)
			else if inputObj.cmd == cmd.SERV_SMALL_IMAGE
				#获取小图
				serverdb.getMenuSmallImage(connection, inputObj.data)
			else if inputObj.cmd == cmd.SERV_BIG_IMAGE
				#获取大图
				serverdb.getMenuBigImage(connection, inputObj.data)
			else if inputObj.cmd == cmd.SERV_ADD_ORDER
				#下单
				serverdb.addOrderDetail(connection, inputObj.data.menu_id, inputObj.data.quantity)
			else if inputObj.cmd == cmd.SERV_PAYMENT
				#结账
				serverdb.orderPayment(connection)
			else if inputObj.cmd == cmd.SERV_OPEN_CLIENT
				#服务台开通一个客户端
				clientIp = inputObj.data
				serverdb.openClient(connection, clientIp)
			else if inputObj.cmd == cmd.SERV_CLOSE_CLIENT
				#服务台归档一个客户端
				clientIp = inputObj.data
				serverdb.closeClient(connection, clientIp)
			else if inputObj.cmd == cmd.SERV_ONLINE_LIST
				#在线列表
				serverdb.onlineList(connection)
			else if inputObj.cmd == cmd.SERV_ORDER_DETAIL
				#下单明细列表
				serverdb.orderDetailList(connection)
			else if inputObj.cmd == cmd.SERV_ORDER_DETAIL_CHANGE_STATUS
				#更新一个订单明细的状态（将状态改为1）
				serverdb.orderDetailChangeStatus(connection, inputObj.data)
			else if inputObj.cmd == cmd.SERV_REPORT_DAY
				#日报表数据
				serverdb.reportDay(connection, inputObj.data.time_from, inputObj.data.time_to)
			else if inputObj.cmd == cmd.SERV_REPORT_MONTH
				#月报表数据
				serverdb.reportMonth(connection, inputObj.data.time_from, inputObj.data.time_to)
			else if inputObj.cmd == cmd.SERV_CLIENT_LIST
				#获取客户端列表数据
				serverdb.clientDataList(connection)
			else if inputObj.cmd == cmd.SERV_CLIENT_ADD
				#增加或更新客户端数据
				serverdb.clientDataAdd(connection, inputObj.data)
			else if inputObj.cmd == cmd.SERV_CLIENT_DELETE
				#删除客户端数据
				serverdb.clientDataDelete(connection, inputObj.data)
			else if inputObj.cmd == cmd.SERV_MENU_CLASS_ADD
				#增加或更新菜单分类数据
				serverdb.menuClassAdd(connection, inputObj.data)
			else if inputObj.cmd == cmd.SERV_MENU_CLASS_DELETE
				#删除菜单分类数据
				serverdb.menuClassDelete(connection, inputObj.data)
			else if inputObj.cmd == cmd.SERV_MENU_ADD
				#增加或更新菜单数据
				serverdb.menuAdd(connection, inputObj.data)
			else if inputObj.cmd == cmd.SERV_MENU_DELETE
				#删除菜单数据
				serverdb.menuDelete(connection, inputObj.data)
			else
				#命令参数错误
				console.log(strings.COMMONS_MSG1)
				outputStr = commons.outputJsonStr(0, strings.COMMONS_MSG1)
				connection.sendUTF(outputStr)
				connection.close()
	
	connection.on "close", (reasonCode, description) ->
		
		#删除客户端列表
		deleteIndex = -1
		i = 0
		for item in clientList
			if connection.remoteAddress == item.remoteAddress
				deleteIndex = i
				break
			
			i++
		
		if deleteIndex != -1
			clientList.splice(deleteIndex, 1)
		#console.log(clientList.length)

		#向服务台发出响应数据
		#刷新服务台的数据
		outputStr = commons.outputJsonStr(1, null, cmd.CLIENT_GET_ONLINE_LIST)
		serverdb.writeInformationDeskData(outputStr)

		console.log((new Date()) + " Peer " + connection.remoteAddress + " disconnected.")

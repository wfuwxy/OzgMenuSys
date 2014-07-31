
var cfg = require("./cfg");
var cmd = require("./cmd");
var strings = require("./strings");
var commons = require("./commons");
var fs = require('fs');
var sqlite3 = require("sqlite3").verbose();

var db = new sqlite3.Database(cfg.DB_PATH);   

var chkClientSql = "select * from client where ip = '{0}' and status = 1";
var chkClientSql2 = "select * from client where ip = '{0}'"; //检测订单是否归档的时候用到

var clientList = new Array()
exports.clientList = clientList; //客户端列表，里面的元素是connection对象

//公用方法不用exports
function writeErrorIp(connection) {
	console.log("查询不到ip为" + connection.remoteAddress + "的数据");
			
	var outputStr = commons.outputJsonStr(0, strings.CHECK_MSG1);
			
	connection.sendUTF(outputStr);
	connection.close();
}

//公用方法不用exports
function writeDbData(connection, sql) {
	db.get(commons.format(chkClientSql, connection.remoteAddress), function(err, row) {
		if(row != undefined && row) {
			
			db.all(sql, function(err, rows) {

				var outputStr = commons.outputJsonStr(1, "", "", rows);			
				connection.sendUTF(outputStr);
			});
		}
		else {
			//查询不到数据
			
			writeErrorIp(connection);
		}

	});
}

//有链接时调用
exports.checkClient = function(connection) {
	
	db.get("select * from client where ip = '" + connection.remoteAddress + "'", function(err, row) {
		if(row != undefined && row) {
			
			//console.log("已接收ip " + row.ip + "发来的数据:" + data);
			
			if(row.is_admin == 1) {
				//服务台
				var data = {
					"client": row
				};
				var outputStr = commons.outputJsonStr(1, commons.format(strings.CHECK_MSG5, row.ip), cmd.CLIENT_WANT_TOMAIN, data);
				connection.sendUTF(outputStr);
			}
			else {
				//一般客户端

				if(row.status == 0) {
					//未开通
					var outputStr = commons.outputJsonStr(0, commons.format(strings.CHECK_MSG2, row.name));
					connection.sendUTF(outputStr);
				}
				else if(row.status == 1) {
					//已开通的逻辑
					
					var data = {
						"client": row
					};
					var outputStr = commons.outputJsonStr(1, commons.format(strings.CHECK_MSG3, row.name), cmd.CLIENT_WANT_TOMENU, data);
					connection.sendUTF(outputStr);
				}
			}			

		}
		else {
			//查询不到数据
			
			writeErrorIp(connection);
		}

	});	
};

//获取菜单分类列表
exports.getMenuClassList = function(connection) {
	var sql = "select * from menu_class order by id desc, sort desc";
	writeDbData(connection, sql);
};

//获取一个菜单分类下面的菜单列表
exports.getMenuList = function(connection, dataId) {
	dataId = commons.sqlValid(connection, dataId, strings.COMMONS_MSG2);
	if(!dataId)
		return;

	var sql = "select m.*, mc.name as mc_name from menu as m inner join menu_class as mc on m.class_id = mc.id where m.class_id = " + dataId + " order by m.id desc, m.sort desc";
	writeDbData(connection, sql);	
};

//获取一个菜单的详细数据
/*exports.getMenuDetail = function(connection, dataId) {	
	db.get(commons.format(chkClientSql, connection.remoteAddress), function(err, row) {
		if(row != undefined && row) {
			dataId = commons.sqlValid(connection, dataId, strings.COMMONS_MSG2);
			if(!dataId)
				return;

			db.get("select m.*, mc.name as mc_name from menu as m inner join menu_class as mc on m.class_id = mc.id where m.id = " + dataId, function(err, row) {

				var outputStr = commons.outputJsonStr(1, "", "", row);			
				connection.sendUTF(outputStr);
			});
		}
		else {
			//查询不到数据
			
			writeErrorIp(connection);
		}

	});

}; */

//获取一个图片（公用方法不用exports）
function getMenuImage(connection, dataId, isSmall) {
	
	db.get(commons.format(chkClientSql, connection.remoteAddress), function(err, row) {
		if(row != undefined && row) {
			dataId = commons.sqlValid(connection, dataId, strings.COMMONS_MSG2);
			if(!dataId)
				return;

			db.get("select m.*, mc.name as mc_name from menu as m inner join menu_class as mc on m.class_id = mc.id where m.id = " + dataId, function(err, row2) {
				var imgStr = null;
				if(isSmall)
					imgStr = commons.fileBase64Encode("./" + row2.small_img);
				else 
					imgStr = commons.fileBase64Encode("./" + row2.big_img);

				var data = {};
				data.img_base64str = imgStr;
				data.menu_data = row2;

				var outputStr = commons.outputJsonStr(1, "", "", data);
				connection.sendUTF(outputStr);
			});
			
		}
		else {
			//查询不到数据
			
			writeErrorIp(connection);
		}

	});

};

//获取一个小图
exports.getMenuSmallImage = function(connection, dataId) {
	
	getMenuImage(connection, dataId, true);

};

//获取一个大图
exports.getMenuBigImage = function(connection, dataId) {
	
	getMenuImage(connection, dataId, false);

};

//点菜
exports.addOrderDetail = function(connection, menuId, quantity) {
	
	if(quantity == undefined || quantity == 0)
		quantity = 1;
	
	db.get(commons.format(chkClientSql, connection.remoteAddress), function(err, row) {
		if(row != undefined && row) {
			menuId = commons.sqlValid(connection, menuId, strings.COMMONS_MSG2);
			if(!menuId)
				return;

			db.get("select * from menu where id = " + menuId, function(err, row2) {
				
				db.get("select id from `order` where client_id = " + row.id + " and status = 0 order by id desc, add_time desc limit 1", function(err, row3) {
					
					var addTime = Date.parse(new Date()) / 1000;
					
					quantity = commons.sqlValid(connection, quantity, strings.COMMONS_MSG2);
					if(!quantity)
						return;

					db.run("insert into order_detail (add_time, menu_id, price, quantity, menu_name, order_id) values(" + addTime + ", " + row2.id + ", " + row2.price + ", " + quantity + ", '" + row2.name + "', " + row3.id + ")");
					
					var updateTime = addTime;
					db.run("update `order` set update_time = " + updateTime + " where id = " + row3.id);

					var outputStr = commons.outputJsonStr(1, commons.format(strings.MENU_ADD_MSG, row2.name));
					connection.sendUTF(outputStr);
				});
			});
		}
		else {
			//查询不到数据
			
			writeErrorIp(connection);
		}

	});

};

//获取当前客户端的订单列表
exports.getOrderList = function(connection) {
	
	var sql = "select od.*, o.update_time as o_update_time from order_detail as od left join `order` as o on od.order_id = o.id left join client as c on o.client_id = c.id where c.ip = '" + connection.remoteAddress + "' and o.status = 0";
	writeDbData(connection, sql);
};

//当前客户端结帐
exports.orderPayment = function(connection) {

	//订单状态：0正在消费，1结帐中，2完成订单
	
	db.get(commons.format(chkClientSql, connection.remoteAddress), function(err, row) {
		if(row != undefined && row) {
									
			var sql = "select sum(od.price * od.quantity) as total_price from order_detail as od left join `order` as o on od.order_id = o.id left join client as c on o.client_id = c.id where c.ip = '" + connection.remoteAddress + "' and o.status = 0";
			db.get(sql, function(err, row2) {
				
				if(row2.total_price != null && row2.total_price > 0) {
					//改为结账中，完成订单是服务台操作的
					var updateTime = (new Date()).getTime() / 1000;

					var sql = "update `order` set status = 1, update_time = " + parseInt(updateTime) + " where status = 0 and client_id = " + row.id;
					db.run(sql);

					var outputStr = commons.outputJsonStr(1, commons.format(strings.MENU_PAYMENT_MSG1, row2.total_price));
					connection.sendUTF(outputStr);
				}
				else {
					var outputStr = commons.outputJsonStr(0, strings.MENU_PAYMENT_MSG2);
					connection.sendUTF(outputStr);
				}
				
			});
			
		}
		else {
			//查询不到数据
			
			writeErrorIp(connection);
		}

	});

};

exports.isEndClient = function(connection) {
	//检测订单是否已归档
	db.get(commons.format(chkClientSql2, connection.remoteAddress), function(err, row) {
		if(row != undefined && row) {
			
			db.get("select count(id) as total from `order` where status < 2 and client_id = " + row.id, function(err, row2) {
				if(row2.total > 0) {
					//未归档
					var outputStr = commons.outputJsonStr(0);
					connection.sendUTF(outputStr);
				}
				else {
					//已归档
					var outputStr = commons.outputJsonStr(1);
					connection.sendUTF(outputStr);
				}
			});
						
		}
		else {
			//查询不到数据
			
			writeErrorIp(connection);
		}

	});
};

exports.openClient = function(connection, targetClientIp) {
	//服务台开通一个客户端
	db.get(commons.format(chkClientSql, connection.remoteAddress), function(err, row) {
		if(row != undefined && row) {
									
			if(row.is_admin == 1) {
				
				targetClientIp = commons.sqlValid(connection, targetClientIp, strings.COMMONS_MSG2);
				if(!targetClientIp)
					return;

				db.get("select * from client where ip = '" + targetClientIp + "'", function(err, row2) {
					
					var sql = "update client set status = 1 where ip = '" + targetClientIp + "'";
					db.run(sql);

					var addTime = (new Date()).getTime() / 1000;
					var updateTime = addTime;
					sql = "insert into `order` (add_time, update_time, client_id) values(" + parseInt(addTime) + ", " + parseInt(updateTime) + ", " + row2.id + ")";
					db.run(sql);
					
					//对目标客户端发送跳转命令
					var data = {
						"client": row2
					};
					var outputStr = commons.outputJsonStr(1, commons.format(strings.NOTICE_MSG1, row2.name), cmd.CLIENT_WANT_TOMENU, data);
					for(var i = 0; i < clientList.length; i++) {
						if(targetClientIp == clientList[i].remoteAddress) {
							//console.log(outputStr);
							clientList[i].sendUTF(outputStr);
							break;
						}
					}

					//返回服务台的信息
					outputStr = commons.outputJsonStr(1, commons.format(strings.NOTICE_MSG1, row2.name));
					connection.sendUTF(outputStr);
				});
				
			}
			else {
				var outputStr = commons.outputJsonStr(0, commons.format(strings.CHECK_MSG4, connection.remoteAddress));
				connection.sendUTF(outputStr);
			}
			
		}
		else {
			//查询不到数据
			
			writeErrorIp(connection);
		}

	});
};

exports.closeClient = function(connection, targetClientIp) {
	//服务台归档一个客户端
	db.get(commons.format(chkClientSql, connection.remoteAddress), function(err, row) {
		if(row != undefined && row) {
									
			if(row.is_admin == 1) {
				targetClientIp = commons.sqlValid(connection, targetClientIp, strings.COMMONS_MSG2);
				if(!targetClientIp)
					return;

				db.get("select * from client where ip = '" + targetClientIp + "'", function(err, row2) {
					var sql = "update client set status = 0 where ip = '" + targetClientIp + "'";
					db.run(sql);

					var updateTime = (new Date()).getTime() / 1000;
					sql = "update `order` set status = 2, update_time = " + parseInt(updateTime) + " where status = 1 and client_id = " + row2.id;
					db.run(sql);

					var outputStr = commons.outputJsonStr(1, commons.format(strings.NOTICE_MSG2, row2.name), cmd.CLIENT_WANT_TOMAIN);
					for(var i = 0; i < clientList.length; i++) {
						if(targetClientIp == clientList[i].remoteAddress) {
							clientList[i].sendUTF(outputStr);
							break;
						}
					}

					//返回服务台的信息
					var outputStr = commons.outputJsonStr(1, commons.format(strings.NOTICE_MSG2, row2.name));
					connection.sendUTF(outputStr);
				});
				
			}
			else {
				var outputStr = commons.outputJsonStr(0, commons.format(strings.CHECK_MSG4, connection.remoteAddress));
				connection.sendUTF(outputStr);
			}
			
		}
		else {
			//查询不到数据
			
			writeErrorIp(connection);
		}

	});
};

//在线列表用到
var statClientData = null;
function statClient(connection, rows, i) {
	
	if(statClientData == null)
		statClientData = new Array();

	db.get("select o.status as o_status, sum(od.price) as total_price from `order` as o left join order_detail as od on o.id = od.order_id where o.client_id = " + rows[i].id + " and o.status < 2", function(err, row) {
		
		var statClientDataItem = {};
		statClientDataItem.o_status = row.o_status;
		statClientDataItem.total_price = row.total_price;
		statClientDataItem.ip = rows[i].ip;
		statClientDataItem.name = rows[i].name;
		statClientDataItem.status = rows[i].status;
		statClientDataItem.is_admin = rows[i].is_admin;
		statClientData.push(statClientDataItem);

		i++;
		
		if(i < rows.length) {
			statClient(connection, rows, i);
		}
		else {
			var outputStr = commons.outputJsonStr(1, null, cmd.CLIENT_WANT_ONLINE_LIST, statClientData);
			connection.sendUTF(outputStr);

			statClientData = null;
		}
		
	});
}

exports.onlineList = function(connection) {
	//在线列表
	db.get(commons.format(chkClientSql, connection.remoteAddress), function(err, row) {
		if(row != undefined && row) {
									
			if(row.is_admin == 1) {
				var ipList = new Array();
				for(var i = 0; i < clientList.length; i++)
					ipList.push("'" + clientList[i].remoteAddress + "'");

				var sql = "select * from client where ip in(" + ipList.toString() + ") order by id desc";
				db.all(sql, function(err, rows) {
					if(rows.length > 0)
						statClient(connection, rows, 0);
					
				});
			}
			else {
				var outputStr = commons.outputJsonStr(0, commons.format(strings.CHECK_MSG4, connection.remoteAddress));
				connection.sendUTF(outputStr);
			}
			
		}
		else {
			//查询不到数据
			
			writeErrorIp(connection);
		}

	});
};

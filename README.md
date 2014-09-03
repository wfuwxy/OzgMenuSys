ozg 点餐系统
================

本系统只适用于局域网（平板或相关设备替代纸质菜单）的场合，客户端需要设置固定IP，每一个客户端的IP对应服务端数据库client表的一条数据。

================

Server目录为服务器端：

使用nodejs + sqlite3 + websocket，需要安装的库请查看node_modules目录的描述文件，服务器启动：cd OzgMenuSys/Server，node main.js。

upload目录下的图片均来自互联网。

上传服务端的大图为600*400的jpg。

================

Server_CoffeeScript目录为服务器端：

功能同上面的一样，只是用CoffeeScript实现了同样的功能。

需要安装coffee-script、sqlite3、websocket、imagemagick，然后将Server/upload、Server/db.sys复制到这个目录。

服务器启动：cd OzgMenuSys/Server_CoffeeScript，coffee main.coffee。

================

Android目录为客户端：

density为1.0、1.5、2.5的真机测试通过（没有使用模拟器测试）

AppConfig.java里面的服务器IP和端口必须跟服务端相对应。

默认链接服务器：ws://192.168.1.101:8765

需要自行设置服务器数据库client表的对应数据（该数据对应该客户端的IP），也可以用服务台来进行相应操作

================

InformationDesk目录为服务台端：

AppConfig.cs里面的服务器IP和端口必须跟服务端相对应，client表的对应数据，is_admin字段必须为1。环境为VS2013 + .Net4.5.1。

默认链接服务器：ws://192.168.1.101:8765

默认服务台的IP为192.168.1.103，也可以自行修改服务器数据库client表的对应数据来对应正确的服务台IP

================

运行状况：
![](https://raw.github.com/ouzhigang/OzgMenuSys/master/screenshot1.jpg)

![](https://raw.github.com/ouzhigang/OzgMenuSys/master/screenshot2.jpg)

![](https://raw.github.com/ouzhigang/OzgMenuSys/master/screenshot3.jpg)

ozg 点餐系统
================

本系统只适用于局域网和平板客户端或相关设备（平板或相关设备替代纸质菜单）的场合，客户端需要设置固定IP，每一个客户端的IP对应服务端数据库client表的一条数据。

================

Server目录为服务器端：

使用nodejs + sqlite3 + websocket，需要安装的库请查看node_modules目录的描述文件，服务器启动：cd OzgMenuSys/Server，node main.js。

upload目录下的图片均来自互联网。

上传服务端的大图为600*400的jpg。

================

Android目录为客户端：

AppConfig.java里面的服务器IP和端口必须跟服务端相对应。

默认链接服务器：ws://192.168.1.10:8765

================

InformationDesk目录为服务台端：

AppConfig.cs里面的服务器IP和端口必须跟服务端相对应，client表的对应数据，is_admin字段必须为1。环境为VS2012 + .Net4.5。

默认链接服务器：ws://192.168.1.10:8765

================

运行状况：
![](https://raw.github.com/ouzhigang/OzgMenuSys/master/screenshot1.jpg)

![](https://raw.github.com/ouzhigang/OzgMenuSys/master/screenshot2.jpg)

![](https://raw.github.com/ouzhigang/OzgMenuSys/master/screenshot3.jpg)

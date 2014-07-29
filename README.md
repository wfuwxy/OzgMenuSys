ozg 点餐系统
================

本系统只适用于局域网和平板客户端（平板替代纸质菜单）的场合，客户端需要设置固定IP，每一个客户端的IP对应服务端数据库client表的一条数据。

================

Server目录为服务器端：

使用nodejs + sqlite3 + websocket，详情请查看node_modules目录，服务器启动：cd OzgMenuSys/Server，node main.js。

node test.js是暂时代替没有做完的服务台端，主要测试服务台端的各种功能。

服务端存放的大图为1366*908，小图为360*233。

================

Android目录为客户端：

AppConfig.java里面的服务器IP和端口必须跟服务端相对应。

================

InformationDesk目录为服务台端（未完成）：

AppConfig.cs里面的服务器IP和端口必须跟服务端相对应。环境为VS2012 + .NET4.5。

================

计划完成部分：未做自适应测试（只在魅族MX3上面测试通过），心跳检测机制。

================

运行状况：
![](https://raw.github.com/ouzhigang/OzgMenuSys/master/screenshot1.jpg)

![](https://raw.github.com/ouzhigang/OzgMenuSys/master/screenshot2.jpg)

![](https://raw.github.com/ouzhigang/OzgMenuSys/master/screenshot3.jpg)

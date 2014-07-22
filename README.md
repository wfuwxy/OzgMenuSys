ozg 点餐系统
================

本系统只适用于局域网，客户端需要设置固定IP，每一个客户端的IP对应服务端数据库client表的一条数据。

服务器端使用nodejs + sqlite3 + websocket，详情请查看node_modules目录，服务器启动：cd OzgMenuSys/Server， node main.js。

node test.js是暂时代替没有做的服务台端的，主要测试服务台端的各种功能。

服务端存放的大图为1366*908，小图为360*233。

客户端使用android，AppConfig.java里面的服务器IP和端口必须跟服务端相对应。

计划完成部分：未做自适应测试（只在魅族MX3上面测试通过），防sql注入，心跳检测机制，图片本地缓存机制，在线状态(服务台端)。


运行状况：
![](https://raw.github.com/ouzhigang/OzgMenuSys/master/screenshot1.jpg)

![](https://raw.github.com/ouzhigang/OzgMenuSys/master/screenshot2.jpg)

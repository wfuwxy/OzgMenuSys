ozg 点餐系统

================

本系统主要用于技术的研究和积累

================

客户端需要设置固定IP，每一个客户端的IP对应服务端数据库client表的一条数据。

================

Server目录为服务器端：

先安装imagemagick：http://www.imagemagick.org/ 

cd到OzgMenuSys/Server，运行npm install

服务器启动：node main.js。

upload目录下的图片均来自互联网。

上传服务端的大图为600*400的jpg。


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

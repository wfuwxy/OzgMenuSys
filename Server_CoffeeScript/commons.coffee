
fs = require('fs')

Date.prototype.format = (formatStr) ->
	o = {
		"M+" : this.getMonth() + 1, #month
		"d+" : this.getDate(), #day
		"h+" : this.getHours(), #hour
		"m+" : this.getMinutes(), #minute
		"s+" : this.getSeconds(), #second
		"q+" : Math.floor((this.getMonth() + 3) / 3), #quarter
		"S" : this.getMilliseconds() #millisecond
	}

	if /(y+)/.test(formatStr)
		formatStr = formatStr.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length))
	
	for k, v of o
		if new RegExp("(" + k + ")").test(formatStr)
			formatStr = formatStr.replace(RegExp.$1, if RegExp.$1.length == 1 then v else ("00" + v).substr(("" + v).length))
	return formatStr

exports.fileBase64Encode = (filePath) ->
	bitmap = fs.readFileSync(filePath)
	return new Buffer(bitmap).toString("base64")

exports.fileBase64Decode = (base64str, filePath) ->
	bitmap = new Buffer(base64str, "base64")
	fs.writeFileSync(filePath, bitmap)
	#console.log('******** File created from base64 encoded string ********')

#格式化一个字符串
exports.format = (str, args...) ->
	i = 0
	for item in args
		str = str.replace("{" + i.toString() + "}", item)
		i++

	return str

#防注入检测
exports.sqlStrValid = (str) ->
	str = str + ""
	re = /select|update|delete|exec|count|'|"|=|;|>|<|%/i
	if re.test(str)
		return false	
	return str

#下面是这个项目自有的

#生成一个输出数据的对象
exports.outputJsonStr = (ok, message, cmd, data) ->
	
	outputStrObj = {}
	outputStrObj.ok = ok

	if message != undefined && message != null
		outputStrObj.message = message
	
	if cmd != undefined && cmd != null
		outputStrObj.cmd = cmd

	if data != undefined && data != null
		outputStrObj.data = data
	
	return JSON.stringify(outputStrObj)

exports.sqlValid = (connection, str, errorStr) ->
	str = this.sqlStrValid(str)
	if !str
		outputStr = this.outputJsonStr(0, errorStr)
		connection.sendUTF(outputStr)
		return false
	
	return str


var fs = require('fs');

exports.fileBase64Encode = function(filePath) {
    var bitmap = fs.readFileSync(filePath);
    return new Buffer(bitmap).toString("base64");
}

exports.fileBase64Decode = function(base64str, filePath) {
    var bitmap = new Buffer(base64str, "base64");
    fs.writeFileSync(filePath, bitmap);
    //console.log('******** File created from base64 encoded string ********');
}

//生成一个输出数据的对象
exports.outputJsonStr = function(ok, message, cmd, data) {
	
	var outputStrObj = {};
	outputStrObj.ok = ok;

	if(message != undefined && message != null)
		outputStrObj.message = message;
	
	if(cmd != undefined && cmd != null)
		outputStrObj.cmd = cmd;

	if(data != undefined && data != null)
		outputStrObj.data = data;
	
	return JSON.stringify(outputStrObj);
}

//格式化一个字符串
exports.format = function() {
	var str = arguments[0];

	for(var i = 1; i < arguments.length; i++) {
		str = str.replace("{" + (i - 1).toString() + "}", arguments[i]);
	}
	
	return str;
}

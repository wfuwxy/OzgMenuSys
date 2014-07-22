
var fs = require('fs');

function fileBase64Encode(filePath) {
    var bitmap = fs.readFileSync(filePath);
    return new Buffer(bitmap).toString("base64");
}

function fileBase64Decode(base64str, filePath) {
    var bitmap = new Buffer(base64str, "base64");
    fs.writeFileSync(filePath, bitmap);
    //console.log('******** File created from base64 encoded string ********');
}

//生成一个输出数据的对象
function outputJsonStr(ok, message, cmd, data) {
	
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
function format() {
	var str = arguments[0];

	for(var i = 1; i < arguments.length; i++) {
		str = str.replace("{" + (i - 1).toString() + "}", arguments[i]);
	}
	
	return str;
}

exports.fileBase64Encode = fileBase64Encode;
exports.fileBase64Decode = fileBase64Decode;

exports.outputJsonStr = outputJsonStr;
exports.format = format;

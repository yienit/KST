// Util method for javascript

/*只能输入数字和*,不能以0开头 onkeypress='inputNumberOnly(this)' */
function inputNumberOnly(obj) {
    var e = window.event || arguments.callee.caller.arguments[0];
    var keyCode = e.keyCode;
    var reg = /^[1-9]\d{1,4}$/
    if ((obj.value.length == 0) && keyCode == 42) {
        event.returnValue = true;
        return;
    }
    if (obj.value.length == 0 && keyCode == 48) {
        event.returnValue = false;
        return;
    }
    if ((keyCode > 47 && keyCode < 58)) {
        event.returnValue = true;
    }
    else {
        event.returnValue = false;
    }
}

/* 校验手机号码 */
function isMobile(s) {
    //var patrn = /^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$/;
    var patrn = /^(13[0-9]{9})|(14[0-9])|(18[0-9])|(15[0-9][0-9]{8})$/;
    if (!patrn.exec(s)) return false
    return true
}

/* string.format 方法实现 */
String.prototype.format = function () {
    var args = arguments;
    return this.replace(/\{(\d+)\}/g,
        function (m, i) {
            return args[i];
        });
}

/*   dateURL格式的图片(data:image/png;base64,dW5kZWZpbmVk) 转换为Blob对象 */
function dataURItoBlob(dataURI) {
    // convert base64/URLEncoded data component to raw binary data held in a string
    var byteString;
    if (dataURI.split(',')[0].indexOf('base64') >= 0)
        byteString = atob(dataURI.split(',')[1]);
    else
        byteString = unescape(dataURI.split(',')[1]);

    // separate out the mime component
    var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];

    // write the bytes of the string to a typed array
    var ia = new Uint8Array(byteString.length);
    for (var i = 0; i < byteString.length; i++) {
        ia[i] = byteString.charCodeAt(i);
    }

    return new Blob([ia], { type: mimeString });
}

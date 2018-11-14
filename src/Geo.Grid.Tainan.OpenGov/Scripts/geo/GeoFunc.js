// 錯誤處理
function errorHandler(err) {
    if (err != null) {
        if (err.statusText != null) {
            console.log(err.statusText);
            alert(err.statusText);
        } else {
            console.log(err);
            alert(err);
        }
    }
}

//
function DoAjaxPost(url, model, success) {
    $.ajax({
        url: url,
        type: "POST",
        data: model,
        dataType: "json",
        async: false,
        cache: false,
        success: success
    });
}

///顯示 Ajax Partial View
function ShowPartial(url, containerId, success, data) {
    $.ajax({
        url: url,
        data: data,
        type: "GET",
    })
    .done(function (result) {
        $("#" + containerId).html(result);
        if (success) {
            success();
        }
    });
}

///顯示bootstrap popup
function ShowModal(url, containerId, data) {
    $.ajax({
        url: url,
        type: "GET",
        data: data
    })
    .done(function (result) {
        $("#" + containerId).html(result).modal('show');
    });
}

//四捨五入
function Round(val, pow) {
    pow = pow || 0;
    var p = Math.pow(10, pow);
    return Math.round(val * p) / p;
}

//取得網址列參數
function GetUrlPara(name) {
    GetUrlPara(name, window.location.href);
}
function GetUrlPara(name, href) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(href);
    if (results == null) {
        return "";
    } else {
        return results[1];
    }
}

function GetDocWidthHeight() {
    return {
        w: (window.innerWidth || document.documentElement && document.documentElement.clientWidth || document.body.clientWidth),
        h: (window.innerHeight || document.documentElement && document.documentElement.clientHeight || document.body.clientHeight)
    }
}

////str_pad方法 i=字串,l=位數,s=補齊的元素, 用在地號補"0"
$.strPad = function (i, l, s) {
    var o = i.toString();
    s = (s) || '0';
    while (o.length < l) {
        o = s + o;
    }
    return o;
};

//擴充.NET的String.Format
String.format = function () {
    var s = arguments[0];
    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        s = s.replace(reg, arguments[i + 1]);
    }
    return s;
}

if (typeof String.prototype.trim !== 'function') {
    String.prototype.trim = function () {
        return this.replace(/^\s+|\s+$/g, '');
    }
}

//顯示驗證文字
document.addEventListener("DOMContentLoaded", function () {
    var elements = document.getElementsByTagName("INPUT");
    for (var i = 0; i < elements.length; i++) {
        elements[i].oninvalid = function (e) {
            e.target.setCustomValidity("");
            if (!e.target.validity.valid) {
                e.target.setCustomValidity(e.target.getAttribute('data-require'));
            }
        };
        elements[i].oninput = function (e) {
            e.target.setCustomValidity("");
        };
    }

    var selects = document.getElementsByTagName("select");
    for (var i = 0; i < selects.length; i++) {
        selects[i].oninvalid = function (e) {
            e.target.setCustomValidity("");
            if (!e.target.validity.valid) {
                e.target.setCustomValidity(e.target.getAttribute('data-require'));
            }
        };
        selects[i].oninput = function (e) {
            e.target.setCustomValidity("");
        };
    }
})

//驗證文字
document.addEventListener("DOMContentLoaded", function() {
    var elements = document.getElementsByTagName("INPUT");
    for (var i = 0; i < elements.length; i++) {
        elements[i].oninvalid = function(e) {
            e.target.setCustomValidity("");
            if (!e.target.validity.valid) {
                e.target.setCustomValidity(e.target.getAttribute('data-require'));
            }
        };
        elements[i].oninput = function(e) {
            e.target.setCustomValidity("");
        };
    }

    var selects = document.getElementsByTagName("select");
    for (var i = 0; i < selects.length; i++) {
        selects[i].oninvalid = function(e) {
            e.target.setCustomValidity("");
            if (!e.target.validity.valid) {
                e.target.setCustomValidity(e.target.getAttribute('data-require'));
            }
        };
        selects[i].oninput = function(e) {
            e.target.setCustomValidity("");
        };
    }
})
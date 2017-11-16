
$(function () {
    headNavigation.init();
    showLeftTime();
});
document.body.onresize = function () {
    headNavigation.divChange();
}
function clearCookie() {
    var keysCookie = parent.document.cookie.match(/[^ =;]+(?=\=)/g);
    if (keysCookie) {
        for (var i = 0; i < keysCookie.length; i++)
            parent.document.cookie = keysCookie[i] + '=0;expires=' + new Date(0).toUTCString()
    }
}
//头部事件
var headNavigation = {
    commandCentreOnClick: function () {
        parent.main.changeWUA();
    }
    , reportCentreClick: function () {
        parent.main.changeReport();
    },
    logout: function () {
        $("#Div1 img").click(function () {
            clearCookie();
            parent.window.location.href = parent.globalConfig.loginUrl;
           
        })
    },
    init: function () { this.divChange(); this.centreChange(); this.logout(); },
    divChange: function () {
        var vw = document.body.clientWidth;
        var rw = $("#Div1").width();

        $("#content").css("width", vw - rw - 50 - 35);
        var leftWidth = $("#content").width();
        var imgMargin = 0;
        if (leftWidth > 729) {
            var offsetM = (leftWidth - 729) / 7 + imgMargin;
            $(".imgMar").css("margin-left", offsetM);
        }
        $("#left").css("width", leftWidth);
        $(".centerImg").css("width", leftWidth - 500);
    },
    centreChange: function () {
        $(".centreDiv").each(function (i) {
            $(this).click(function () {
                if (i == 0) {
                    //$('.container').css("display", "block");
                    //$('#Draggable').css("display", "block");
                    parent.head.hideMessWarm(false);
                }
                else {
                    parent.head.hideMessWarm(true);
                    //$('.container').css("display", "none");
                    //$('#Draggable').css("display", "none");
                }
                $(".labelNavigation").find("img")[0].src = $(".labelNavigation").find("img")[0].src.replace('_s', "");
                $(this).removeClass("labelNavigationFalse").addClass("labelNavigation").siblings().removeClass("labelNavigation").addClass("labelNavigationFalse");
                $(this).find("img")[0].src = $(this).find("img")[0].src.replace('.png', "") + "_s.png";
                if (i != 0 && i != 1) {
                    parent.document.cookie = "FUNCTIONID=" + headNavigation.FUNCTIONID(i);
                    parent.location.href = parent.globalConfig.managerIndexPath + '?FUNCTIONID=' + headNavigation.FUNCTIONID(i);
                }
            });
        })       
    },
    FUNCTIONID: function (j) {
        switch(j){
            case 2: return 101;
            case 3: return 6;
            case 4: return 8;
            case 5: return 7;
            case 6: return 9;
            case 7: return 10;
        }
    }
}
//获得当前时间,刻度为一千分一秒
var initializationTime = (new Date()).getTime();
function showLeftTime() {
    var now = new Date();
    var year = now.getYear();
    var month = now.getMonth();
    var day = now.getDate();
    var hours = now.getHours() < 10 ? "0" + now.getHours() : now.getHours();
    var minutes = now.getMinutes() < 10 ? "0" + now.getMinutes() : now.getMinutes();
    var seconds = now.getSeconds() < 10 ? "0" + now.getSeconds() : now.getSeconds();
    if (month + 1 < 10) {
        month = "0" + (month + 1);
    } else {
        month = month + 1;
    }
    if (day < 10) {
        day = "0" + day;
    }
    $(".Date")[0].innerHTML = "" + (1900 + year) + "-" + month + "-" + day;
    $(".Time")[0].innerHTML = "  " + hours + ":" + minutes + ":" + seconds;
    //一秒刷新一次显示时间
    var timeID = setTimeout(showLeftTime, 1000);
}

////---------------------------------------------------  
//// 日期格式化  
//// 格式 YYYY/yyyy/YY/yy 表示年份  
//// MM/M 月份  
//// W/w 星期  
//// dd/DD/d/D 日期  
//// hh/HH/h/H 时间  
//// mm/m 分钟  
//// ss/SS/s/S 秒  
////---------------------------------------------------  
//Date.prototype.Format = function (formatStr) {
//    var str = formatStr;
//    var Week = ['日', '一', '二', '三', '四', '五', '六'];

//    str = str.replace(/yyyy|YYYY/, this.getFullYear());
//    str = str.replace(/yy|YY/, (this.getYear() % 100) > 9 ? (this.getYear() % 100).toString() : '0' + (this.getYear() % 100));

//    str = str.replace(/MM/, this.getMonth() > 9 ? this.getMonth().toString() : '0' + this.getMonth());
//    str = str.replace(/M/g, this.getMonth());

//    str = str.replace(/w|W/g, Week[this.getDay()]);

//    str = str.replace(/dd|DD/, this.getDate() > 9 ? this.getDate().toString() : '0' + this.getDate());
//    str = str.replace(/d|D/g, this.getDate());

//    str = str.replace(/hh|HH/, this.getHours() > 9 ? this.getHours().toString() : '0' + this.getHours());
//    str = str.replace(/h|H/g, this.getHours());
//    str = str.replace(/mm/, this.getMinutes() > 9 ? this.getMinutes().toString() : '0' + this.getMinutes());
//    str = str.replace(/m/g, this.getMinutes());

//    str = str.replace(/ss|SS/, this.getSeconds() > 9 ? this.getSeconds().toString() : '0' + this.getSeconds());
//    str = str.replace(/s|S/g, this.getSeconds());

//    return str;
//}
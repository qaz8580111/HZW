//把rgb格式颜色转换成16位颜色
var common = {
    changeRGBToColor: function changeRGBToColor(rgb) {
        var that = rgb;
        if (/^(rgb|RGB)/.test(that)) {
            var aColor = that.replace(/(?:\(|\)|rgb|RGB)*/g, "").split(",");
            var strHex = "#";
            for (var i = 0; i < aColor.length; i++) {
                var hex = Number(aColor[i]).toString(16);
                if (hex === "0") {
                    hex += hex;
                }
                strHex += hex;
            }
            if (strHex.length !== 7) {
                strHex = that;
            }
            return strHex;
        } else if (reg.test(that)) {
            var aNum = that.replace(/#/, "").split("");
            if (aNum.length === 6) {
                return that;
            } else if (aNum.length === 3) {
                var numHex = "#";
                for (var i = 0; i < aNum.length; i += 1) {
                    numHex += (aNum + aNum);
                }
                return numHex;
            }
        } else {
            return that;
        }
    },
    //自动关闭的提示框
    selfAlert: function (str) {
        if (document.getElementById("alertmsgDiv")!=null) {
        return;
        }
        var msgw, msgh, bordercolor;
        msgw = 350;//提示窗口的宽度    
        msgh = 80;//提示窗口的高度    
        titleheight = 25 //提示窗口标题高度    
        bordercolor = "#336699";//提示窗口的边框颜色    
        titlecolor = "#99CCFF";//提示窗口的标题颜色    
        var sWidth, sHeight;
        //获取当前窗口尺寸    
        sWidth = document.body.offsetWidth;
        sHeight = document.body.offsetHeight;
        //    //背景div    
        var bgObj = document.createElement("div");
        bgObj.setAttribute('id', 'alertbgDiv');
        bgObj.style.position = "absolute";
        bgObj.style.top = "0";
        bgObj.style.background = "#E8E8E8";
        bgObj.style.filter = "progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75";
        bgObj.style.opacity = "0.6";
        bgObj.style.left = "0";
        bgObj.style.width = sWidth + "px";
        bgObj.style.height = sHeight + "px";
        bgObj.style.zIndex = "10000";
        document.body.appendChild(bgObj);
        //创建提示窗口的div    
        var msgObj = document.createElement("div")
        msgObj.setAttribute("id", "alertmsgDiv");
        msgObj.setAttribute("align", "center");
        msgObj.style.background = "white";
        msgObj.style.border = "1px solid " + bordercolor;
        msgObj.style.position = "absolute";
        msgObj.style.left = "50%";
        msgObj.style.font = "12px/1.6em Verdana, Geneva, Arial, Helvetica, sans-serif";
        //窗口距离左侧和顶端的距离     
        msgObj.style.marginLeft = "-225px";
        //窗口被卷去的高+（屏幕可用工作区高/2）-150    
        msgObj.style.top = document.body.scrollTop + (window.screen.availHeight / 2) - 150 + "px";
        msgObj.style.width = msgw + "px";
        msgObj.style.height = msgh + "px";
        msgObj.style.textAlign = "center";
        msgObj.style.lineHeight = "25px";
        msgObj.style.zIndex = "10001";
        document.body.appendChild(msgObj);
        //提示信息标题    
        var title = document.createElement("h4");
        title.setAttribute("id", "alertmsgTitle");
        title.setAttribute("align", "left");
        title.style.margin = "0";
        title.style.padding = "3px";
        title.style.background = bordercolor;
        title.style.filter = "progid:DXImageTransform.Microsoft.Alpha(startX=20, startY=20, finishX=100, finishY=100,style=1,opacity=75,finishOpacity=100);";
        title.style.opacity = "0.75";
        title.style.border = "1px solid " + bordercolor;
        title.style.height = "18px";
        title.style.font = "12px Verdana, Geneva, Arial, Helvetica, sans-serif";
        title.style.color = "white";
        title.innerHTML = "提示信息";
        document.getElementById("alertmsgDiv").appendChild(title);
        //提示信息    
        var txt = document.createElement("p");
        txt.setAttribute("id", "msgTxt");
        txt.style.margin = "16px 0";
        txt.innerHTML = str;
        document.getElementById("alertmsgDiv").appendChild(txt);
        //设置关闭时间    
        window.setTimeout("common.closewin()", 2000);
    },

    closewin: function () {
        document.body.removeChild(document.getElementById("alertbgDiv"));
        //document.getElementById("alertmsgDiv").removeChild(document.getElementById("alertmsgTitle"));
        document.body.removeChild(document.getElementById("alertmsgDiv"));
    }
}

function selfAlert(str) {
    var msgw, msgh, bordercolor;
    msgw = 350;//提示窗口的宽度    
    msgh = 80;//提示窗口的高度    
    titleheight = 25 //提示窗口标题高度    
    bordercolor = "#336699";//提示窗口的边框颜色    
    titlecolor = "#99CCFF";//提示窗口的标题颜色    
    var sWidth, sHeight;
    //获取当前窗口尺寸    
    sWidth = document.body.offsetWidth;
    sHeight = document.body.offsetHeight;
    //    //背景div    
    var bgObj = document.createElement("div");
    bgObj.setAttribute('id', 'alertbgDiv');
    bgObj.style.position = "absolute";
    bgObj.style.top = "0";
    bgObj.style.background = "#E8E8E8";
    bgObj.style.filter = "progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75";
    bgObj.style.opacity = "0.6";
    bgObj.style.left = "0";
    bgObj.style.width = sWidth + "px";
    bgObj.style.height = sHeight + "px";
    bgObj.style.zIndex = "10000";
    document.body.appendChild(bgObj);
    //创建提示窗口的div    
    var msgObj = document.createElement("div")
    msgObj.setAttribute("id", "alertmsgDiv");
    msgObj.setAttribute("align", "center");
    msgObj.style.background = "white";
    msgObj.style.border = "1px solid " + bordercolor;
    msgObj.style.position = "absolute";
    msgObj.style.left = "50%";
    msgObj.style.font = "12px/1.6em Verdana, Geneva, Arial, Helvetica, sans-serif";
    //窗口距离左侧和顶端的距离     
    msgObj.style.marginLeft = "-225px";
    //窗口被卷去的高+（屏幕可用工作区高/2）-150    
    msgObj.style.top = document.body.scrollTop + (window.screen.availHeight / 2) - 150 + "px";
    msgObj.style.width = msgw + "px";
    msgObj.style.height = msgh + "px";
    msgObj.style.textAlign = "center";
    msgObj.style.lineHeight = "25px";
    msgObj.style.zIndex = "10001";
    document.body.appendChild(msgObj);
    //提示信息标题    
    var title = document.createElement("h4");
    title.setAttribute("id", "alertmsgTitle");
    title.setAttribute("align", "left");
    title.style.margin = "0";
    title.style.padding = "3px";
    title.style.background = bordercolor;
    title.style.filter = "progid:DXImageTransform.Microsoft.Alpha(startX=20, startY=20, finishX=100, finishY=100,style=1,opacity=75,finishOpacity=100);";
    title.style.opacity = "0.75";
    title.style.border = "1px solid " + bordercolor;
    title.style.height = "18px";
    title.style.font = "12px Verdana, Geneva, Arial, Helvetica, sans-serif";
    title.style.color = "white";
    title.innerHTML = "提示信息";
    document.getElementById("alertmsgDiv").appendChild(title);
    //提示信息    
    var txt = document.createElement("p");
    txt.setAttribute("id", "msgTxt");
    txt.style.margin = "16px 0";
    txt.innerHTML = str;
    document.getElementById("alertmsgDiv").appendChild(txt);
    //设置关闭时间    
    window.setTimeout("closewin()", 1500);
};

function closewin() {
    document.body.removeChild(document.getElementById("alertbgDiv"));
    document.getElementById("alertmsgDiv").removeChild(document.getElementById("alertmsgTitle"));
    document.body.removeChild(document.getElementById("alertmsgDiv"));
}
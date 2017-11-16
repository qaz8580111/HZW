var g_iWndIndex = 0; //可以不用设置这个变量，有窗口参数的接口中，不用传值，开发包会默认使用当前选择窗口
$(function () {
    // 检查插件是否已经安装过
    var iRet = WebVideoCtrl.I_CheckPluginInstall();
    if (-2 == iRet) {
        alert("您的Chrome浏览器版本过高，不支持NPAPI插件！");
        return;
    } else if (-1 == iRet) {
        document.getElementById("VideoVersion").style.display = "block";
        document.getElementById("divPlugin").style.display = "none";
        return;
    }

    // 初始化插件参数及插入插件
    WebVideoCtrl.I_InitPlugin(500, 300, {
        bWndFull: true,//是否支持单窗口双击全屏，默认支持 true:支持 false:不支持
        iWndowType: 2,
        cbSelWnd: function (xmlDoc) {
            g_iWndIndex = $(xmlDoc).find("SelectWnd").eq(0).text();
            var szInfo = "当前选择的窗口编号：" + g_iWndIndex;
            //showCBInfo(szInfo);
        }
    });
    WebVideoCtrl.I_InsertOBJECTPlugin("divPlugin");

    // 检查插件是否最新
    if (-1 == WebVideoCtrl.I_CheckPluginVersion()) {
        alert("检测到新的插件版本，双击开发包目录里的WebComponentsKit.exe升级！");
        return;
    }

    // 窗口事件绑定
    $(window).bind({
        resize: function () {
            var $Restart = $("#restartDiv");
            if ($Restart.length > 0) {
                var oSize = getWindowSize();
                $Restart.css({
                    width: oSize.width + "px",
                    height: oSize.height + "px"
                });
            }
        }
    });
    WebVideoCtrl.I_ChangeWndNum(1);
    clickLogin();
    //clickStartRealPlay("60.190.179.50");
});

// 登录
function clickLogin() {
    var szIP = "'192.168.20.10",
		szPort = "554",
		szUsername = "admin",
		szPassword = "admin123";

    if ("" == szIP || "" == szPort) {
        return;
    }

    var iRet = WebVideoCtrl.I_Login(szIP, 1, szPort, szUsername, szPassword, {
        success: function (xmlDoc) {
            //alert("mession seccess");
            ////showOPInfo(szIP + " 登录成功！");

            ////$("#ip").prepend("<option value='" + szIP + "'>" + szIP + "</option>");
            //setTimeout(function () {
            //    //$("#ip").val(szIP);
            //    //getChannelInfo(szIP);
            //}, 10);
            clickStartRealPlay(szIP);
        },
        error: function () {
            alert(szIP + " mession failed");
        }
    });

    if (-1 == iRet) {
        alert(szIP + " 已登录过！");
    }
}

// 获取通道
function getChannelInfo(szIP) {
    //var szIP = $("#ip").val(),
	//	oSel = $("#channels").empty();

    //if ("" == szIP) {
    //    return;
    //}

    // 模拟通道
    WebVideoCtrl.I_GetAnalogChannelInfo(szIP, {
        async: false,
        success: function (xmlDoc) {
            var oChannels = $(xmlDoc).find("VideoInputChannel");

            $.each(oChannels, function (i) {
                var id = $(this).find("id").eq(0).text(),
					name = $(this).find("name").eq(0).text();
                if ("" == name) {
                    name = "Camera " + (i < 9 ? "0" + (i + 1) : (i + 1));
                }
                oSel.append("<option value='" + id + "' bZero='false'>" + name + "</option>");
            });
            alert(szIP + " 获取模拟通道成功！");
        },
        error: function () {
            alert(szIP + " 获取模拟通道失败！");
        }
    });
    // 数字通道
    WebVideoCtrl.I_GetDigitalChannelInfo(szIP, {
        async: false,
        success: function (xmlDoc) {
            var oChannels = $(xmlDoc).find("InputProxyChannelStatus");

            $.each(oChannels, function (i) {
                var id = $(this).find("id").eq(0).text(),
					name = $(this).find("name").eq(0).text(),
					online = $(this).find("online").eq(0).text();
                if ("false" == online) {// 过滤禁用的数字通道
                    return true;
                }
                if ("" == name) {
                    name = "IPCamera " + (i < 9 ? "0" + (i + 1) : (i + 1));
                }
                oSel.append("<option value='" + id + "' bZero='false'>" + name + "</option>");
            });
            alert(szIP + " 获取数字通道成功！");
        },
        error: function () {
            alert(szIP + " 获取数字通道失败！");
        }
    });
    // 零通道
    WebVideoCtrl.I_GetZeroChannelInfo(szIP, {
        async: false,
        success: function (xmlDoc) {
            var oChannels = $(xmlDoc).find("ZeroVideoChannel");

            $.each(oChannels, function (i) {
                var id = $(this).find("id").eq(0).text(),
					name = $(this).find("name").eq(0).text();
                if ("" == name) {
                    name = "Zero Channel " + (i < 9 ? "0" + (i + 1) : (i + 1));
                }
                if ("true" == $(this).find("enabled").eq(0).text()) {// 过滤禁用的零通道
                    oSel.append("<option value='" + id + "' bZero='true'>" + name + "</option>");
                }
            });
            alert(szIP + " 获取零通道成功！");
        },
        error: function () {
            alert(szIP + " 获取零通道失败！");
        }
    });
}

function clickStartRealPlay(szIP) {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex),
		szIP = szIP,
		iStreamType = 1,//parseInt($("#streamtype").val(), 10),
		iChannelID =1 ,//parseInt($("#channels").val(), 10),
		bZeroChannel =  false,
		szInfo = "";

    if ("" == szIP) {
        return;
    }

    //if (oWndInfo != null) {// 已经在播放了，先停止
    //    WebVideoCtrl.I_Stop();
    //}

    var iRet = WebVideoCtrl.I_StartRealPlay(szIP, {
        iStreamType: iStreamType,
        iChannelID: iChannelID,
        bZeroChannel: bZeroChannel
    });

    if (0 == iRet) {
        szInfo = "review seccess";
    } else {
        szInfo = "review failed";
    }

    //alert(szIP + " " + szInfo);
}
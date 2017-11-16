//初始化SL对象
function initSL(sender) {
    main.slContent = sender.getHost();
}
//初始化三维控件
function initWJH() {
    initAxControl();
    //return;
   // con = __g.new_ConnectionInfo;
   // con.connectionType = 101;
    //con.database = "yzmx";
    //con.server = "172.16.2.126";
   // con.database = "122402";
   // con.server = "172.172.100.20";
   // con.port = 8040;
    //加载服务数据
    //loadServer(con);
    //加载本地数据，路径为"C:\\WJH\\3D数字鄞州150605A.FDB"
    loadFdb("", "", "");

    var x = 358994.199503869;
    var y = 3302067.63111812;
    var scale = __g.new_Vector3;

    scale.set(x, y, 0);
    var ang = __g.new_EulerAngle;
    ang.heading = 10;
    ang.tilt = -45;
    __g.camera.flyTime = 0;
    __g.camera.lookAt(scale, 200, ang);
    // 改为漫游
    __g.interactMode = gviInteractMode.gviInteractNormal;
    __g.oncameraflyfinished = function () {
      //  $("#loadingDiv").css("visibility", "collapse");
        if (globalConfig.isFirst && globalConfig.isLogin) {
            globalConfig.isFirst = false;
          
        }
    };
}
//初始化和重置面板
//初始化和重置菜单
function initMenu() {
    var vh = document.body.clientHeight;
    var vw = document.body.clientWidth;
    var mh = $("#menuDiv").height();
    var mw = $("#menuDiv").width();
    //$("#menuDiv").html("高度：" + vh + "；宽度" + vw + "\n;菜单高度：" + mh + "菜单宽度" + mw);
    $("#menuDiv").css("top", vh - mh);
    $("#menuContent").css("width", mw - 210);
    //设置菜单栏的案件数量bar
    var offset = (vw - 1024) / 9;
    offset = offset > 0 ? offset : 0;
    $(".contentMenu").css("margin-right", 20 + offset);
    $(".self .contentMenu:last").css("margin-right", 0);

    $('#picBottow').css("width", $("#menuContent").width() - 200).css("left", 100);
    $("#menuContent").css("background-size", $("#menuContent").width() + "px 100px");
}
//初始化和重置二三维地图大小
function initMap() {
    var vh = document.body.clientHeight;
    var hh = $("#headDiv").height();
    var mh = $("#menuDiv").height();
    $("#swDiv").css("top", hh).css("height", vh - hh - mh);
    $("#ewDiv").css("top", hh).css("height", vh - hh - mh);
}
//初始化和重置列表
function initList() {
    var vh = document.body.clientHeight;
    var vw = document.body.clientWidth;
    var hh = $("#headDiv").height();
    var mh = $("#menuDiv").height();
    var lh = $("#listDiv").height();
    var lw = $("#listDiv").width();
    //$("#listDiv").html("高度：" + vh + "；宽度" + vw + "\n;列表高度：" + lh + "列表宽度" + lw);
    $("#listDiv").css("top", hh + 30).css("left", vw - lw - 42);
}
//初始化和重置详情
function initDetail() {
    var vw = document.body.clientWidth;
    var hh = $("#headDiv").height();
    var dw = $("#detailDiv").width();
    //var lw = $("#listDiv").width();
    //$("#listDiv").html("高度：" + vh + "；宽度" + vw + "\n;详情高度：" + dh + "列表宽度" + dw);
    $("#detailDiv").css("top", hh + 30).css("left", (vw - 60 - dw) / 2);
}
//初始化和重置概要面板
function initDetailMin() {
    var vh = document.body.clientHeight;
    var vw = document.body.clientWidth;
    var hh = $("#headDiv").height();
    var mh = $("#menuDiv").height();
    var dh = $("#listDiv-min").height();
    var dw = $("#listDiv-min").width();
    //$("#listDiv-min").html("高度：" + vh + "；宽度" + vw + "\n;详情高度：" + dh + "列表宽度" + dw);
    $("#detail-minDiv").css("top", hh + 30).css("left", 50);//(vh - mh - dh - hh) / 3,(vw - dw) / 3
}
//初始化和重置报表面板
function initReport() {
    var vh = document.body.clientHeight;
    var vw = document.body.clientWidth;
    var hh = $("#headDiv").height();
    $("#reportDiv").css("top", hh).css("height", vh - hh).css("width", vw);
}
//初始化加载界面
function initLoading() {

    var roleNameCookie = decodeURI(getCookie("RoleID"));
    var roles = roleNameCookie.split(',');
    for (var i = 0; i < roles.length; i++) {
        if (roles[i] == "30") {
            globalConfig.isViewer = 1;
        }
        if (roles[i] == "29") {
            parent.globalConfig.isLeader = 1;
        }
    }
    if (globalConfig.isViewer == 0) {
        //window.history.go(-1);
        //window.location.href=globalConfig.loginUrl;
    }


    var vh = document.body.clientHeight;
    $("#loadingDiv > img").css("top", (vh - 400) / 2);
    var acc = getCookie("account");
    var pwd = getCookie("password");
    //var acc = sessionStorage.getItem("account");
    //var pwd = sessionStorage.getItem("password");

    if (acc != null && pwd != null) {
        globalConfig.isLogin = true;
        $("#loginSpan").html("登录成功");
        $("#loadingSpan").html("地图数据加载中。。。");
        main.init();
        $("#loadingDiv").css("visibility", "collapse");
    } else {
        $("#loginSpan").html("登录失败");
        $("#loadingSpan > button").css("visibility", "visible");
    }

    //$.ajax({
    //    type: "GET",
    //    async: true,
    //    url: globalConfig.apiconfig + "/api/User/UserLogin",
    //    data: { account: acc, password: pwd },
    //    dataType: "json",
    //    success: function (result) {
    //        switch (result) {
    //            case "登入成功":
    //                globalConfig.isLogin = true;
    //                $("#loginSpan").html("登入成功");
    //                $("#loadingSpan").html("地图数据加载中。。。");
    //                main.init();
    //                break;
    //            default:
    //                $("#loginSpan").html(result);
    //                $("#loadingSpan > button").css("visibility", "visible");
    //                break;
    //        }
    //    },
    //    error: function (errorMsg) {
    //        console.log(errorMsg);
    //    }
    //});
}
//初始化消息面板
function initMessage() {
    var vw = document.body.clientWidth;
    $("#messageDiv").css("left", (vw - 580) / 2);
}

function login() {
    window.location = "login.aspx";
}

function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return unescape(arr[2]);
    else
        return null;
}
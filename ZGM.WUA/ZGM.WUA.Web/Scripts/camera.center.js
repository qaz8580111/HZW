var globalConfig = {//全局变量
    //apiconfig: "http://localhost:5618",
    apiconfig: "",
};

$(function () {
    console.log("准备 init");
    initConfig();
    cam.init();
    console.log("完成 init");
});

document.body.onresize = function () {
    cam.resize();
}

var slContent = null;
function initSL(sender) {
    slContent = sender.getHost();
    cam.personTraceReplay();
    intervalFunc();
    setInterval("intervalFunc()", 15000);
    console.log("personTraceReplay");

}

function intervalFunc() {
    $.ajax({
        type: "GET",
        async: true,
        url: globalConfig.apiconfig + "/api/User/GetUserByUserId",
        data: { userId: cam.User.UserId },
        dataType: "json",
        success: function (data) {
            cam.personTraceReplay();
            if (cam.User != null) {
                if (cam.User.X2000 == data.X2000 && cam.User.Y2000 == data.Y2000) {
                    return;
                }
            }
            cam.User.Name = data.UserName;
            cam.User.X2000 = data.X2000;
            cam.User.Y2000 = data.Y2000;
            //cam.User.UserName = data.UserName;
           
        },
        error: function (errorMsg) {
            console.log(errorMsg);
        }
    });
}
function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return unescape(arr[2]);
    else
        return null;
}
var cam = {
    OCXobj: null,
    User: null,
    init: function () {
        this.User = new Object();
        //var queryStr = GetQueryString("User");
        //this.User.X = GetQueryString("X");
        //this.User.Y = GetQueryString("Y");
        var queryStr =decodeURI(decodeURI( getCookie("User")));
        this.User = eval("(" + queryStr + ")");
        console.log("GetQueryString");

        this.initDiv();
        console.log("initDiv");

        this.initCam();
        console.log("initCam");


    },
    resize: function () {
        this.initDiv();
    },
    initDiv: function () {
        var vh = document.body.clientHeight;
        var vw = document.body.clientWidth;
        $("#erDiv").css("width", vw / 2).css("height", vh);
        $("#cam").css("width", vw / 2).css("left", vw / 2).css("height", vh);
        //$("#list").css("height", vh - 200).css("left", vw - 200);
    },
    initCam: function () {
        var OCXobj1 = document.getElementById("PlayViewOCX");
        this.OCXobj = OCXobj1;
        this.OCXobj.SetOcxMode(0);
        this.OCXobj.SetWndNum(1);
    },
    initList: function () {

    },
    palyView: function (obj) {

    },
    personTraceReplay: function () {
        mapElementModel.Id = this.User.UserId;
        mapElementModel.Type = "UserModel";
        mapElementModel.Name = this.User.Name;
        mapElementModel.X = this.User.X2000;
        mapElementModel.Y = this.User.Y2000;
        mapElementModel.IsOnline = this.User.IsOnline;
        mapElementModel.IsAlarm = this.User.IsAlarm;
        var jsonstr = JSON.stringify(mapElementModel);
        slContent.Content.MainPage.PersonCameraCentreHistory(jsonstr);
    }
}

var jsonCamera = null;

//监控Maker点击播放视频
function CameraPalyClicked(obj) {

    var previewXml = eval("(" + obj + ")").Note;;
    if (previewXml == "")
        return;
    cam.OCXobj.StartTask_Preview_FreeWnd(previewXml);

}

//监控列表
function CameraList(obj) {
    jsonCamera = eval("(" + obj + ")");;
    //$("#list").html("");
    //var cameraList = "";
    //for (var i = 0; i < jsonCamera.length; i++) {
    //    if (i == 0) {
    //        cameraList += "<div class='cameraList cameraSelected'>" + jsonCamera[i].Name + "</div>";
    //    } else {
    //        cameraList += "<div class='cameraList'>" + jsonCamera[i].Name + "</div>";
    //    }
    //}
    //$("#list").html(cameraList);
    if (jsonCamera.length != 0) {
        cam.OCXobj.StopAllPreview();
        for (var i = 0; i < jsonCamera.length; i++) {
            cam.OCXobj.StartTask_Preview_InWnd(jsonCamera[i].Note, (i + 1));
        }
        //cam.OCXobj.StartTask_Preview_FreeWnd(jsonCamera[0].Note);
        //cam.OCXobj.StartTask_Preview_FreeWnd(jsonCamera[1].Note);
        //cam.OCXobj.StartTask_Preview_FreeWnd(jsonCamera[2].Note);
        //cam.OCXobj.StartTask_Preview_FreeWnd(jsonCamera[3].Note);
        //$(".cameraList").each(function (i) {
        //    $(this).click(function () {
        //        $(this).addClass("cameraSelected").siblings().removeClass("cameraSelected");
        //        var camera = jsonCamera[i];
        //        var previewXml = jsonCamera[i].Note;
        //        if (previewXml == "")
        //            return;
        //        cam.OCXobj.StartTask_Preview_FreeWnd(previewXml);
        //    });
        //})
    }
}

//地图元素模型
var mapElementModel = {
    Id: null,
    Type: null,
    PartsType: null,
    Name: null,
    Circum: null,
    X: null,
    Y: null,
    Lines: null,
    Areas: null,
    IsOnline: null,
    IsAlarm: null
}

function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
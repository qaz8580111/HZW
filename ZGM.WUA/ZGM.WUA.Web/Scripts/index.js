var pathUrl = "http://218.108.93.246:15212";// "http://后台地址";
var globalConfig = {//全局变量
    picurl: "http://172.172.100.22:8081",
    apiconfig: "http://localhost:5618",
    managerIndexPath: "http://172.172.100.22:8081/Home",
    //imgPath: "http://218.108.93.246:15212/GetZFSJPicByPath.ashx?PicPath=",
    listTakeNum: 8,
    mapFlag: 3,//3:三维；2:二维
    isFirst: true,
    isLogin: false,
    Account: null,
    Password: null,
    isFirst: true,
    UserID: 0,
    UserName: "",
    password: "",
    UserID: null,
    imgPath: "http://218.108.93.246:15212/GetZFSJPicByPath.ashx?PicPath=",
    refreshPosition: 0,
    isLeader: 0,
    isViewer: 0,
    loginUrl: "http://localhost:18279",
    //头像地址
    smallHeadImgPath: pathUrl + "/GetPictureFile.ashx?PicPath=C:\\ZGMImage\\UserImage\\small\\",
    //消息小图
    messImgSmallPath: pathUrl + "/GetPictureFile.ashx?PicPath=E:\\ZGMImage\\XXFilesPath\\small\\",
    //消息压缩图
    messImgFilesPath: pathUrl + "/GetPictureFile.ashx?PicPath=E:\\ZGMImage\\XXFilesPath\\destnation\\",
    //消息原图
    messImgOriginalPath: pathUrl + "/GetPictureFile.ashx?PicPath=E:\\ZGMImage\\XXFilesPath\\sourse\\",
    //简易工程附件   E:\\ZGMFile\SimpleEngineeringFilesPath\\
    constrSimplePath: pathUrl + "/GetPictureFile.ashx?PicPath=E:\\ZGMFile\\SimpleEngineeringFilesPath\\",
    //重大工程附件   E:\\ZGMFile\ZDGCFile\\
    constrImportantPath: pathUrl + "/GetPictureFile.ashx?PicPath=E:\\ZGMFile\\ZDGCFile\\",
    //拆迁附件  E:\\ZGMFile\ProjectFilesPath\\
    removeBuildPath: pathUrl + "/GetPictureFile.ashx?PicPath=E:\\ZGMFile\\ProjectFilesPath\\",
    //违建附件   E:\\ZGMImage\WFJZFilesPath\sourse\\
    nonConforBuildPath: pathUrl + "/GetPictureFile.ashx?PicPath=E:\\ZGMImage\\WFJZFilesPath\\sourse\\",
};

var warmMessage = {
    roleMessage: function () {
        $.messager.show({
            title: '提示',
            msg: '没有对应权限！',
            timeout: 2000,
            showType: 'slide',

        });
    }
}

$(function () {
    //main.init();
    initConfig();
    initLoading();
    messageWarm();
    globalConfig.password = getCookie("password");
    globalConfig.UserName = getCookie("account");
    globalConfig.UserID = getCookie("UserID");
});
function getCookie(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=")
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1
            c_end = document.cookie.indexOf(";", c_start)
            if (c_end == -1) c_end = document.cookie.length
            return unescape(document.cookie.substring(c_start, c_end))
        }
    }
    return ""
}
function messageWarm() {
    $('.container').css('left', document.body.clientWidth - 92 + "px");
    $('.container').css('top', document.body.clientHeight - 205 + "px");
}
var main = {
    slContent: null,
    init: function () {
        //initLoading();
        initMessage();
        initMenu();
        initList();
        initDetailMin();
        initDetail();
        initReport();
        initMap();
        initWJH();
        this.initCount();
    },
    resize: function () {
        initMessage();
        initMenu();
        initList();
        initDetailMin();
        initDetail();
        initReport();
        initMap();
       
    },
    initCount: function () {
        this.initEvent();
        this.initOnlineStaff();
        this.initOnlineCar();
        this.initCameraCount();
    },
    initEvent: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/Task/GetTasksStat?startTime=&endTime=",
            dataType: "json",
            success: function (result) {

                if ($("label:contains('今日上报数')+label").length > 0) {
                    $("label:contains('今日上报数')+label")[0].textContent = result[0];
                }
                if ($("label:contains('紧急事件')+label").length > 0) {
                    $("label:contains('紧急事件')+label")[0].textContent = result[1];
                }
                if ($("label:contains('结案数')+label").length > 0) {
                    $("label:contains('结案数')+label")[0].textContent = result[2];
                }
                if ($("label:contains('超期数')+label").length > 0) {
                    $("label:contains('超期数')+label")[0].textContent = result[3];
                }

            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    initOnlineStaff: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/User/GetUnitOnline?parentId=&seconds=",
            dataType: "json",
            success: function (result) {
                var count = 0;
                for (var i = 0; i < result.length; i++) {
                    count += result[i].Sum == null ? 0 : result[i].Sum;
                }
                if ($("label:contains('在线人员')+label").length > 0) {
                    $("label:contains('在线人员')+label")[0].textContent = count;
                }
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    initOnlineCar: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/Car/GetOnlineCount?seconds=",
            dataType: "json",
            success: function (result) {
                if ($("label:contains('在线车辆')+label").length > 0) {
                    $("label:contains('在线车辆')+label")[0].textContent = result;
                }
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    initCameraCount: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/Camera/GetCamerasCount?seconds=",
            dataType: "json",
            success: function (result) {
                if ($("label:contains('监控数量')+label").length > 0) {
                    $("label:contains('监控数量')+label")[0].textContent = result;
                }
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    changeWUA: function () {
        report.close();
    },
    changeReport: function () {
        report.show();
    },
}

//当页面改变大小的时候，重置菜单大小
document.body.onresize = function () {
    main.resize();
    messageWarm();
}

//二维轨迹回放
function historyPalyForPerson(person) {
    //console.log(person);
}



function GetPersonDetail() {
    alert("GetPersonDetail");
}
function GetCarDetail() {
    alert("GetCarDetail");
}

function GetCameraDetail() {
    alert("GetCameraDetail");
}

function GetEventDetail() {
    alert("GetEventDetail");
}

function GetNonConformingBuildingDetail() {
    alert("GetNonConformingBuildingDetail");
}

function GetEngineerDetail() {
    alert("GetEngineerDetail");
}

function GetDemolitionDetail() {
    alert("GetDemolitionDetail");
}

function GetPartsDetail() {
    alert("GetPartsDetail");
}
function GetWhiteListDetail() {
    alert("GetWhiteListDetail");
}

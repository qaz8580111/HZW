function initPersonList() {
    parent.initPersonList();
};
$(function () {
    init();
    initPersonList();
});
var param = {
    bottomDiv_Width: 0,
    showTableID: ""
};
document.body.onresize = function () {
    init();
}
function init() {
    param.bottomDiv_Width = document.body.clientWidth;
    //alert(param.bottomDiv_Width);
    $('#opera')[0].clientWidth = param.bottomDiv_Width;
    //$('#opera')[0].clientheight =95;
    ////alert($("#backgroundPic").clientWidth);
    //$('#backgroundPic').css("width", param.bottomDiv_Width - 210);
    //$('#backgroundPic').css("height", 105);
    //$('#picBottow').css("width", param.bottomDiv_Width - 200).css("left", 100);
    //$('#backgroundPic').css("width", param.bottomDiv_Width);
    $('.tb').css("width", param.bottomDiv_Width);
    $('.fullTb').css("width", param.bottomDiv_Width - 20);
    //alert($('#backgroundPic').width());
    parts.init();
};

//菜单页事件
var menu = {
    //事件点击
    eventOnClick: function eventOnClick() {
        alert("eventOnClick");
    },
    //人员点击
    staffOnClick: function staffOnClick() {
        alert("staffOnClick");
    },
    //监控点击
    cameraOnClick: function cameraOnClick() {
        alert("cameraOnClick");
    },
    //车辆点击
    carOnClick: function carOnClick() {
        alert("carOnClick");
    },
    //部件点击
    partsOnClick: function partsOnClick() {
        parent.changeMenu("Views/Bottow/Parts.aspx");
    },
    //白名单点击
    whiteListOnClick: function whiteListOnClick() {
        alert("whiteListOnClick");
    },
    //工程点击
    engineeringOnClick: function engineeringOnClick() {
        alert("engineeringOnClick");
    },
    //拆迁点击
    demolitionOnClick: function demolitionOnClick() {
        alert("demolitionOnClick");
    },
    //违建点击
    conformingBuildingOnClick: function conformingBuildingOnClick() {
        alert("conformingBuildingOnClick");
    }
}

//部件页面事件
var parts = {
    init: function () {
        //市政
        this.loadMunicipal();
        //排水
        this.loadDrainage();
        //园林绿化
        this.loadLandscaping();
        //市容环卫
        this.loadSanitation();
        //城市内河
        this.loadCityRiver();
    },
    loadMunicipal: function () {
        $("#roadNum")[0].innerText = "1111";
        $("#bridgeNum")[0].innerText = "2222";
        $("#streetLightNum")[0].innerText = "3333";
    },
    loadDrainage: function () {
        $("#pumpNum")[0].innerText = "1111";
        $("#wellCoverNum")[0].innerText = "2222";
    },
    loadLandscaping: function () {
        $("#publicGreenNum")[0].innerText = "1111";
        $("#roadGreenNum")[0].innerText = "2222";
        $("#protectGreenNum")[0].innerText = "3333";
    },
    loadSanitation: function () {
        $("#sanitationCarNum")[0].innerText = "1111";
        $("#cleaningSectionNum")[0].innerText = "2222";
        $("#publicWashroomNum")[0].innerText = "3333";
    },
    loadCityRiver: function () {
        $("#riverNum")[0].innerText = "1111";
    }
}

var firstPage = {
    init: function () {
        this.loadEvent();
        this.loadEventCountByTime();
        this.loadStaffInfo();
    },
    loadEvent: function () { },
    loadEventCountByTime: function () { },
    loadStaffInfo: function () { }
};

//$('.hoverImg').bind('onkeydown', function (e) {
//    this.style.opacity = 0.8;
//});
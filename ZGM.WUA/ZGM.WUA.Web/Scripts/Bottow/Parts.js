$(function () {
    init();
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
        $("#roadNum").html("147");
        $("#bridgeNum").html("207");
        $("#streetLightNum").html("162");
        $("#redLightNum").html("31");
    },
    loadDrainage: function () {
        $("#pumpNum").html("12");
        $("#wellCoverNum").html("198");
    },
    loadLandscaping: function () {
        $("#publicGreenNum").html("160");
        $("#roadGreenNum").html("97");
        $("#protectGreenNum").html("2");
    },
    loadSanitation: function () {
        //$("#sanitationCarNum")[0].innerText = "0";
        //$("#cleaningSectionNum")[0].innerText = "0";
        $("#publicWashroomNum").html("21");
    },
    loadCityRiver: function () {
        $("#riverNum").html("49");
    }
};
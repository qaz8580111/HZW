$(function () {
    init();
    
        $('td.hoverImg').each(function (i) {
            var obj=$(this);
            var str = $(this).find("label")[0].innerHTML;
            var index = isLeaderContains(str);
            if (parent.globalConfig.isLeader != 1) {
                if (index >= 6) {
                    $(this).addClass("grayImg");
                    $(this).click(function () {
                        parent.warmMessage.roleMessage();
                    });
                }
                else {
                    $(this).click(function () {
                        catalog.indexClick(index);
                    })
                }
            } else {
                $(this).click(function () {
                    catalog.indexClick(index);
                })
            }
        })
    
});

function isLeaderContains(str) {
    if (str.indexOf("事件") != -1) {
        return 1;
    }
    else if (str.indexOf("人员") != -1)
    {
        return 2;
    }
    else if (str.indexOf("监控") != -1) {
        return 3;
    }
    else if (str.indexOf("车辆") != -1) {
        return 4;
    }
    else if (str.indexOf("部件") != -1) {
        return 5;
    }
    else if (str.indexOf("工程") != -1) {
        return 6;
    }
    else if (str.indexOf("拆迁") != -1) {
        return 7;
    }
    else if (str.indexOf("违建") != -1) {
        return 8;
    }
    else if (str.indexOf("白名单") != -1) {
        return 9;
    }
    else {
        return 0;
    }
}

var param = {
    bottomDiv_Width: 0,
    showTableID: ""
};
document.body.onresize = function () {
    init();
}
function init() {
    param.bottomDiv_Width = document.body.clientWidth;
   
    $('#opera')[0].clientWidth = param.bottomDiv_Width;
   
    $('.tb').css("width", param.bottomDiv_Width);
    $('.tb td').css("width", param.bottomDiv_Width/9);
    $('.fullTb').css("width", param.bottomDiv_Width - 20);
 

};

//菜单页事件
var catalog = {
    indexClick:function(index){
        switch (index) {
            case 1: catalog.eventOnClick(); break;
            case 2: catalog.staffOnClick(); break;
            case 3: catalog.cameraOnClick(); break;
            case 4: catalog.carOnClick(); break;
            case 5: catalog.partsOnClick(); break;
            case 6: catalog.engineeringOnClick(); break;
            case 7: catalog.demolitionOnClick(); break;
            case 8: catalog.conformingBuildingOnClick(); break;
            case 9: catalog.whiteListOnClick(); break;
        }
    },
    //事件点击
    eventOnClick: function eventOnClick() {
        parent.changeMenu("Views/Bottow/Event.aspx");
    },
    //人员点击
    staffOnClick: function staffOnClick() {
        parent.changeMenu("Views/Bottow/Staff.aspx");
    },
    //监控点击
    cameraOnClick: function cameraOnClick() {
        parent.Camera.initCameraList();
    },
    //车辆点击
    carOnClick: function carOnClick() {
        parent.changeMenu("Views/Bottow/Car.aspx");
    },
    //部件点击
    partsOnClick: function partsOnClick() {
        parent.changeMenu("Views/Bottow/Parts.aspx");
    },
    //白名单点击
    whiteListOnClick: function whiteListOnClick() {
        //alert("whiteListOnClick");
        parent.BMD.initBMDList();
    },
    //工程点击
    engineeringOnClick: function engineeringOnClick() {
        parent.Constr.initConstrList();
    },
    //拆迁点击
    demolitionOnClick: function demolitionOnClick() {
        parent.RemoveBuilding.initRemoveBuildingList();
    },
    //违建点击
    conformingBuildingOnClick: function conformingBuildingOnClick() {
        parent.NonConforBuild.initNonConforBuildList();
    }
}
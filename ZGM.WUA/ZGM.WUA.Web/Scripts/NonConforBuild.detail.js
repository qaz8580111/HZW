$(function () {
    detail.init(parent.NonConforBuild.NonConforBuildInfo);
    parent.mapSW.deepView();
});
var detail = {
    NonConforBuildInfo: null,
    init: function (NonConforBuildInfo) {
        $(".minbtn").toggle(function () {
            detail.expand();
        }, function () {
            detail.collapse();
        });
        this.NonConforBuildInfo = NonConforBuildInfo;
        if ($("div:contains('违建单位（个人）')+div").length > 0) {
            $("div:contains('违建单位（个人）')+div")[0].textContent = NonConforBuildInfo.IBUnitName == null ? "" : NonConforBuildInfo.IBUnitName;
        }
        if ($("div:contains('所在片区')+div").length > 0) {
            $("div:contains('所在片区')+div")[0].textContent = NonConforBuildInfo.ZoneName == null ? "" : NonConforBuildInfo.ZoneName;
        }
        if ($("div:contains('违建用途')+div").length > 0) {
            $("div:contains('违建用途')+div")[0].textContent = NonConforBuildInfo.WJUse == null ? "" : NonConforBuildInfo.WJUse;
        }
        if ($("div:contains('当前状态')+div").length > 0) {
            $("div:contains('当前状态')+div")[0].textContent = NonConforBuildInfo.State == null ? "" : NonConforBuildInfo.State;
        }
        if ($("div:contains('个人或法人代表身份')+div").length > 0) {
            $("div:contains('个人或法人代表身份')+div")[0].textContent = NonConforBuildInfo.IdentityName == null ? "" : NonConforBuildInfo.IdentityName;
        }
        if ($("div:contains('联系电话')+div").length > 0) {
            $("div:contains('联系电话')+div")[0].textContent = NonConforBuildInfo.Tel == null ? "" : NonConforBuildInfo.Tel;
        }
        if ($("div:contains('违建地点')+div").length > 0) {
            $("div:contains('违建地点')+div")[0].textContent = NonConforBuildInfo.Address == null ? "" : NonConforBuildInfo.Address;
        }
        if ($("div:contains('拆除时间')+div").length > 0) {
            $("div:contains('拆除时间')+div")[0].textContent = NonConforBuildInfo.RemoveTime == null ? "" : NonConforBuildInfo.RemoveTime.substr(0, 10);
        }
        if ($("div:contains('建筑面积')+div").length > 0) {
            $("div:contains('建筑面积')+div")[0].textContent = NonConforBuildInfo.BuildingArea == null ? "" : NonConforBuildInfo.BuildingArea;
        }
        if ($("div:contains('拆除面积')+div").length > 0) {
            $("div:contains('拆除面积')+div")[0].textContent = NonConforBuildInfo.RemoveArea == null ? "" : NonConforBuildInfo.RemoveArea;
        }
        this.getFiles();
    },
    getFiles: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: parent.globalConfig.apiconfig + "/api/IllegalBuilding/GetIllegalBuildingFiles",
            data: { IBId: detail.NonConforBuildInfo.IBId },
            dataType: "json",
            success: function (data) {
                var selfData = [{ FileId: 1, FileName: "测试图片", FilePath: "aaa.jpg" }]
                //测试
                //data = selfData;
                var str = "";
                for (var i = 0; i < data.length; i++) {
                    //str += "<tr onclick='parent.zoom(this, \"" + parent.globalConfig.removeBuildPath + data[i].FilePath + "\")'><td class='tbHide'>" + data[i].FileName + "</td> </tr>";
                    str += "<tr><td class='tbHide'><a href='" + parent.globalConfig.nonConforBuildPath + data[i].FilePath + "'  target=\"_blank\">" + data[i].FileName + "</a></td> </tr>";
                }
                $("#tableFile").html(str);              
                //$("#Files").html(str);             
            }
        });
    }, 
    collapse: function () {
        parent.detail.collapse();
        setTimeout(function () {
            $("body").css("background-image", "url('/images/ppolice/ppolicebg1.png')");
        }, 300);
    },
    expand: function () {
        parent.detail.expand();
        $("body").css("background-image", "url('/images/ppolice/ppolicebg.png')");
    },
    close: function () {
        parent.detail.close();
    }
}
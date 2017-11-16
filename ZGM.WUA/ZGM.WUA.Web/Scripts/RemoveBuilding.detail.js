$(function () {
    detail.init(parent.RemoveBuilding.RemoveBuildingInfo, parent.RemoveBuilding.Type);
    parent.mapSW.deepView();
});
var selfData = [
    {
        DrawId: 1,
        HouseId: 2,
        //抽签时间
        DrawTime: '2016-06-27',
        //套数
        HouseCount: 2,
        //实际剩余面积
        OverArea: 10,
        CreateTime: '2016-06-27',
        CreateUserId: 2,
        Houses: [{
            DrawHouseId: 1,
            DrawId: 2,
            //小区
            Residential: "社区名1",
            //房号
            HouseNumber: '房间号-101',
            //面积
            Area: '100',
            Remarks: '备注',
            FileName: '文件名',
            FilePath: '文件路径',
            CreateTime: '2016-06-27'
        },
        {
            DrawHouseId: 2,
            DrawId: 2,
            //小区
            Residential: "社区名1",
            //房号
            HouseNumber: '房间号-102',
            //面积
            Area: '100',
            Remarks: '备注',
            FileName: '文件名',
            FilePath: '文件路径',
            CreateTime: '2016-06-27'
        }
        ]
    }
,
      {
          DrawId: 2,
          HouseId: 2,
          //抽签时间
          DrawTime: '2016-06-27',
          //套数
          HouseCount: 3,
          //实际剩余面积
          OverArea: 11,
          CreateTime: '2016-06-27',
          CreateUserId: 2,
          Houses: [{
              DrawHouseId: 1,
              DrawId: 2,
              //小区
              Residential: "社区名2",
              //房号
              HouseNumber: '房间号-101',
              //面积
              Area: '100',
              Remarks: '备注',
              FileName: '文件名',
              FilePath: '文件路径',
              CreateTime: '2016-06-27'
          },
        {
            DrawHouseId: 2,
            DrawId: 2,
            //小区
            Residential: "社区名2",
            //房号
            HouseNumber: '房间号-102',
            //面积
            Area: '100',
            Remarks: '备注',
            FileName: '文件名',
            FilePath: '文件路径',
            CreateTime: '2016-06-27'
        }
          ]
      }
];

var detail = {
    RemoveBuildingInfo: null,
    detailHouse: null,
    Type: 0,
    searchType: 0,
    init: function (RemoveBuildingInfo, Type) {
        $(".minbtn").toggle(function () {
            detail.expand();
        }, function () {
            detail.collapse();
        });
        this.optionClick();
        this.RemoveBuildingInfo = RemoveBuildingInfo;
        this.Type = Type;
        if (Type == 0) {
            $('#Files').css("display", "none");
            this.getDetails_first();
        }
        else {
            $('#personal').css("display", "none");
            $('#personalDetail').css("display", "none");
            this.getComDetails();
            this.getDetails();
        }
    },
    getDetails_first: function () {
        if ($("div:contains('项目负责人')+div").length > 0) {
            $("div:contains('项目负责人')+div")[0].textContent = detail.RemoveBuildingInfo.ProjectUser == null ? " " : detail.RemoveBuildingInfo.ProjectUser;
        }
        if ($("div:contains('项目状态')+div").length > 0) {
            $("div:contains('项目状态')+div")[0].textContent = detail.RemoveBuildingInfo.StateName == null ? " " : detail.RemoveBuildingInfo.StateName;
        }
        if ($("div:contains('项目名称')+div").length > 0) {
            $("div:contains('项目名称')+div")[0].textContent = detail.RemoveBuildingInfo.ProjectName == null ? " " : detail.RemoveBuildingInfo.ProjectName;
        }
        if ($("div:contains('权证记载面积')+div").length > 0) {
            $("div:contains('权证记载面积')+div")[0].textContent = detail.RemoveBuildingInfo.WarrantArea == null ? " " : detail.RemoveBuildingInfo.WarrantArea;
        }
        if ($("div:contains('户主姓名')+div").length > 0) {
            $("div:contains('户主姓名')+div")[0].textContent = detail.RemoveBuildingInfo.HouseHolder == null ? " " : detail.RemoveBuildingInfo.HouseHolder;
        }
        if ($("div:contains('联系方式')+div").length > 0) {
            $("div:contains('联系方式')+div")[0].textContent = detail.RemoveBuildingInfo.HolderPhone == null ? "" : detail.RemoveBuildingInfo.HolderPhone;
        }
        if ($("div:contains('丈量面积')+div").length > 0) {
            $("div:contains('丈量面积')+div")[0].textContent = detail.RemoveBuildingInfo.MeasurementArea == null ? " " : detail.RemoveBuildingInfo.MeasurementArea;
        }
        if ($("div:contains('无证面积')+div").length > 0) {
            $("div:contains('无证面积')+div")[0].textContent = detail.RemoveBuildingInfo.WarrantArea == null ? " " : detail.RemoveBuildingInfo.WarrantArea;
        }
     
        //获取附件
        $.ajax({
            type: "GET",
            async: true,
            url: parent.globalConfig.apiconfig + "/api/RemoveBuilding/GetFiles",
            data: { id: detail.RemoveBuildingInfo.HouseId },
            dataType: "json",
            success: function (data) {
                var selfData = [{ FileId: 1, FileName: "测试图片", FilePath: "aaa.jpg" }]
                //测试
                //data = selfData;
                var str = "";
                for (var i = 0; i < data.length; i++) {
                    //str += "<tr onclick='parent.zoom(this, \"" + parent.globalConfig.removeBuildPath + data[i].FilePath + "\")'><td class='tbHide'>" + data[i].FileName + "</td> </tr>";
                    str += "<tr><td class='tbHide'><a href='" + parent.globalConfig.removeBuildPath + data[i].FilePath + "'  target=\"view_window\">" + data[i].FileName + "</a></td> </tr>";
                }
                $("#tableFile").html(str);
            }
        });
    },
    getDetails_second: function (obj) {
        $.ajax({
            type: "GET",
            async: true,
            url: parent.globalConfig.apiconfig + "/api/RemoveBuilding/GetRemoveBuildingHouseSign",//获取居民拆迁签协
            data: { houseId: detail.RemoveBuildingInfo.HouseId },
            dataType: "json",
            success: function (data) {
                var html = "";
                if (data != null) {
                    html += '<div class="personnum_BMD" style="height:45px">'
                        + '<div class="columm1" style="margin-left: 40px;">签协时间:</div>'
                        + '<div class="columm2" id="xm">' + (data.SignTime == null ? "" : data.SignTime.substr(0, 10)) + '</div>'
                        + '<div class="columm3"  style="">可调产房屋面积:</div>'
                        + '<div class="columm2" id="name2">' + (data.HouseArea == null ? "" : data.HouseArea) + '</div>'
                    + '</div>';
                    html += '<div class="personnum_BMD" style="height:45px">'
                     + '<div class="columm1" style="margin-left: 40px;">仓库面积:</div>'
                     + '<div class="columm2" id="xm">' + (data.WareHouseArea == null ? "" : data.WareHouseArea) + '</div>'
                     + '<div class="columm3"  style="">扩户面积:</div>'
                     + '<div class="columm2" id="name2">' + (data.ExpansionArea == null ? "" : data.ExpansionArea) + '</div>'
                 + '</div>';
                    html += '<div class="personnum_BMD" style="height:45px">'
                    + '<div class="columm1" style="margin-left: 40px;">划入:</div>'
                    + '<div class="columm2" id="xm">' + (data.WipeIn == null ? "" : data.WipeIn) + '</div>'
                    + '<div class="columm3"  style="">划出:</div>'
                    + '<div class="columm2" id="name2">' + (data.WipeOut == null ? "" : data.WipeOut) + '</div>'
                + '</div>';

                    html += '<div class="personnum_BMD" style="height:45px">'
                    + '<div class="columm1" style="margin-left: 40px;">分户扩户面积:</div>'
                    + '<div class="columm2" id="xm">' + (data.HouseHoldExpansionArea == null ? "" : data.HouseHoldExpansionArea) + '</div>'
                    + '<div class="columm3"  style="">可奖励面积:</div>'
                    + '<div class="columm2" id="name2">' + (data.RewardArea == null ? "" : data.RewardArea) + '</div>'
                + '</div>';
                    html += '<div class="personnum_BMD" style="height:45px">'
               + '<div class="columm1" style="margin-left: 40px;">婚领优购面积:</div>'
               + '<div class="columm2" id="xm">' + (data.MarriageArea == null ? "" : data.MarriageArea) + '</div>'
               + '<div class="columm3"  style="">原房补偿费:</div>'
               + '<div class="columm2" id="name2">' + (data.HouseMoney == null ? "" : data.HouseMoney) + '</div>'
           + '</div>';
                    html += '<div class="personnum_BMD" style="height:45px">'
               + '<div class="columm1" style="margin-left: 40px;">奖励费:</div>'
               + '<div class="columm2" id="xm">' + (data.RewardMoney == null ? "" : data.RewardMoney) + '</div>'
               + '<div class="columm3"  style="">腾空时间:</div>'
               + '<div class="columm2" id="name2">' + (data.EmptyTime == null ? "" : data.EmptyTime.substr(0, 10)) + '</div>'
           + '</div>';
                } else {
                    html += '<div class="personnum_BMD" style="height:45px;margin-top:10px;">'
                + '<div class="columm1" style="margin-left: 40px;">签协时间:</div>'
                + '<div class="columm2" id="xm">' + '</div>'
                + '<div class="columm3"  style="">可调产房屋面积:</div>'
                + '<div class="columm2" id="name2">' + '</div>'
            + '</div>';
                    html += '<div class="personnum_BMD" style="height:45px">'
                     + '<div class="columm1" style="margin-left: 40px;">仓库面积:</div>'
                     + '<div class="columm2" id="xm">' + '</div>'
                     + '<div class="columm3"  style="">扩户面积:</div>'
                     + '<div class="columm2" id="name2">' + '</div>'
                 + '</div>';
                    html += '<div class="personnum_BMD" style="height:45px">'
                    + '<div class="columm1" style="margin-left: 40px;">划入:</div>'
                    + '<div class="columm2" id="xm">' + '</div>'
                    + '<div class="columm3"  style="">划出:</div>'
                    + '<div class="columm2" id="name2">' + '</div>'
                + '</div>';

                    html += '<div class="personnum_BMD" style="height:45px">'
                    + '<div class="columm1" style="margin-left: 40px;">分户扩户面积:</div>'
                    + '<div class="columm2" id="xm">' + '</div>'
                    + '<div class="columm3"  style="">可奖励面积:</div>'
                    + '<div class="columm2" id="name2">' + '</div>'
                + '</div>';
                    html += '<div class="personnum_BMD" style="height:45px">'
               + '<div class="columm1" style="margin-left: 40px;">婚领优购面积:</div>'
               + '<div class="columm2" id="xm">' + '</div>'
               + '<div class="columm3"  style="">原房补偿费:</div>'
               + '<div class="columm2" id="name2">' + '</div>'
           + '</div>';
                    html += '<div class="personnum_BMD" style="height:45px">'
               + '<div class="columm1" style="margin-left: 40px;">奖励费:</div>'
               + '<div class="columm2" id="xm">' + '</div>'
               + '<div class="columm3"  style="">腾空时间:</div>'
               + '<div class="columm2" id="name2">' + '</div>'
           + '</div>';
                }
                obj.html(html);
            }
        });
    },
    getDetails_third: function (obj) {
        $.ajax({
            type: "GET",
            async: true,
            url: parent.globalConfig.apiconfig + "/api/RemoveBuilding/GetRemoveBuildingHouseTranstions",
            data: { houseId: detail.RemoveBuildingInfo.HouseId },
            dataType: "json",
            success: function (data) {
                var str = "";
                str += ' <div class="fileClass" style="width: 100%; margin-top: 10px;color:#619ae2">' +
                                '<span class="columm textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin-left:25px">发放时间</span>' +
                                '<span class="columm textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">期限（月）</span>' +
                                '<span class="columm textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">月过渡费标准</span>' +
                                '<span class="columm textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">过渡费</span>' +
                                '<span class="columm textAlign" style="width:155px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">不计发过渡费可安置面积</span>' +
                            '</div>';
                for (var i = 0; i < data.length; i++) {
                    str += ' <div class="fileClass" style="width: 100%; margin-top: 10px;">' +
                                 '<span class="columm textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin-top:10px;margin-left:25px">' + data[i].StartTime.substr(0, 10) + '</span>' +
                                 '<span class="columm textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin-top:10px;">' + data[i].Term + '</span>' +
                                 '<span class="columm textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin-top:10px;">' + data[i].StandardMoney + '</span>' +
                                  '<span class="columm textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin-top:10px;">' + data[i].Money + '</span>' +
                                   '<span class="columm textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin-top:10px;">' + data[i].PlaceArea + '</span>' +
                             '</div>';

                }
                obj.html(str)
            }
        });
    },
    getDetails_fourth: function (obj) {
        $.ajax({
            type: "GET",
            async: true,//detail.RemoveBuildingInfo.HouseId
            url: parent.globalConfig.apiconfig + "/api/RemoveBuilding/GetRemoveBuildingHouseDraw",
            data: { houseId: detail.RemoveBuildingInfo.HouseId },
            dataType: "json",
            success: function (data) {
                //data = selfData;
                detail.detailHouse = data;
                var html = "";
                if (data != null && data.length > 0) {
                    html += '<div class="personnum_BMD" style="height:45px">'
                        + '<div class="columm1" style="margin-left: 50px;width: 60px;">抽签时间:</div>'
                        + '<div class="columm2" id="xm" style="width: 80px;min-width: 80px;">' + (data[0].DrawTime == null ? "" : data[0].DrawTime.substr(0, 10)) + '</div>'
                        + '<div class="columm3"  style="width: 90px;min-width: 90px;margin-left: 20px;">实际剩余面积:</div>'
                        + '<div class="columm2" id="name2" style="width: 50px;min-width: 50px;">' + (data[0].OverArea == null ? "" : data[0].OverArea) + '</div>'
                        + '<div class="columm1" style="margin-left: 0px;width: 60px;min-width: 60px;">套数:</div>'
                        + '<div class="columm2" id="xm" style="width: 80px;min-width: 80px;">' + (data[0].HouseCount == null ? "" : data[0].HouseCount) + '</div>'
                    + '</div>';
                    //   html += '<div class="personnum_BMD" style="height:45px">'
                    //    + '<div class="columm1" style="margin-left: 40px;">套数:</div>'
                    //    + '<div class="columm2" id="xm">' + (data[0].HouseCount == null ? "" : data[0].HouseCount) + '</div>'
                    //+ '</div>';
                }
                else {
                    html += '<div class="personnum_BMD" style="height:45px">'
                      + '<div class="columm1" style="margin-left: 40px;">抽签时间:</div>'
                      + '<div class="columm2" id="xm">' + '</div>'
                      + '<div class="columm3"  style="">实际剩余面积:</div>'
                      + '<div class="columm2" id="name2">' + '</div>'
                  + '</div>';
                    html += '<div class="personnum_BMD" style="height:45px;width:100px">'
                     + '<div class="columm1" style="margin-left: 40px;">套数:</div>'
                     + '<div class="columm2" id="xm">' + '</div>'
                 + '</div>';
                }

                html += ' <div class="fileClass" style="width:  58%; margin-top: 10px;color:#619ae2">' +
                                '<span class="columm textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin-left:25px">小区</span>' +
                                '<span class="columm textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">房号</span>' +
                                '<span class="columm textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">面积</span>' +
                            '</div>';
                html += '<div class="ppolice_line" style="width: 562px; height: 1px; display: block; margin-left: 4px;margin-top:35px;"></div>';
                if (data != null && data.length > 0) {
                    for (var i = 0; i < data[0].Houses.length; i++) {
                        html += ' <div class="fileClass house" style="width: 58%; margin-top: 10px;">' +
                                     '<span class="columm textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin-top:10px;margin-left:25px">' + data[0].Houses[i].Residential + '</span>' +
                                     '<span class="columm textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin-top:10px;">' + data[0].Houses[i].HouseNumber + '</span>' +
                                     '<span class="columm textAlign" style=" overflow: hidden; white-space: nowrap; text-overflow: ellipsis;margin-top:10px;">' + data[0].Houses[i].Area + '</span>' +
                                 '</div>';
                    }
                }
                obj.html(html);
                detail.getDetails_House();
            }
        });
    },
    getDetails_House: function () {
        //$('.house').each(function (j) {
        //    $(this).click(function () {
        //        var titleDialog = "抽签详情";
        //        if (detail.detailHouse[j] != null) {
        //            if (detail.detailHouse[j].Houses != null) {
        //                if (detail.detailHouse[j].Houses.length == 0) {
        //                    parent.Dialog.alert("提示：本次抽签没有房子信息！");
        //                }
        //                else {
        //                    var str = "";
        //                    str += ' <div class="fileClass" style="width:  100%; margin-top: 10px;color:#619ae2">' +
        //                                    '<span class="columm textAlign" style="  width: 100px;min-width:100px;float: left; min-height :14px;text-align:left;text-align:center; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">小区</span>' +
        //                                    '<span class="columm textAlign" style="  width: 100px;min-width:100px;float: left; min-height :14px;text-align:left;text-align:center; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">房号</span>' +
        //                                    '<span class="columm textAlign" style="  width: 100px;min-width:100px;float: left; min-height :14px;text-align:left;text-align:center; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">面积</span>' +
        //                                '</div>';
        //                    for (var i = 0; i < detail.detailHouse[j].Houses.length;i++){
        //                        str += ' <div class="fileClass" style="width: 100%; margin-top: 10px;">' +
        //                        '<span class="columm textAlign " style="   width: 100px;min-width:100px;float: left; min-height :14px;text-align:left;text-align:center; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + detail.detailHouse[j].Houses[i].Residential + '</span>' +
        //                        '<span class="columm textAlign" style="   width: 100px;min-width:100px;float: left; min-height :14px;text-align:left;text-align:center; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + detail.detailHouse[j].Houses[i].HouseNumber + '</span>' +
        //                        '<span class="columm textAlign" style="  width: 100px;min-width:100px;float: left; min-height :14px;text-align:left;text-align:center; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + detail.detailHouse[j].Houses[i].Area + '</span>' +
        //                    '</div>';
        //                    }
        //                    parent.dialog_self.htmlDialog(titleDialog, str);
        //                }
        //            }
        //            else {
        //                parent.Dialog.alert("提示：本次抽签没有房子信息！");
        //            }
        //        }               
        //    })
        //})
    },
    getDetails_fifth: function (obj) {
        $.ajax({
            type: "GET",
            async: true,
            url: parent.globalConfig.apiconfig + "/api/RemoveBuilding/GetRemoveBuildingHouseCheckout",
            data: { houseId: detail.RemoveBuildingInfo.HouseId },
            dataType: "json",
            success: function (data) {
                var html = "";
                if (data != null) {
                    html += '<div class="personnum_BMD" style="height:45px;clear: both;">'
                        + '<div class="columm1" style="margin-left: 40px;">核算人:</div>'
                        + '<div class="columm2" id="xm">' + (data.AccountUserName == null ? "" : data.AccountUserName) + '</div>'
                        + '<div class="columm3"  style="">资金结算:</div>'
                        + '<div class="columm2" id="name2">' + (data.Money == null ? "" : data.Money) + '</div>'
                    + '</div>';
                    html += '<div class="personnum_BMD" style="height:45px;clear: both;">'
                     + '<div class="columm1" style="margin-left: 40px;">结账人:</div>'
                     + '<div class="columm2" id="xm">' + (data.CheckoutUserName == null ? "" : data.CheckoutUserName) + '</div>'
                     + '<div class="columm3"  style="">结账时间:</div>'
                     + '<div class="columm2" id="name2">' + (data.CheckoutTime == null ? "" : data.CheckoutTime.substr(0, 10)) + '</div>'
                 + '</div>';
                } else {
                    html += '<div class="personnum_BMD" style="height:45px;clear: both;">'
                        + '<div class="columm1" style="margin-left: 40px;">合算负责人:</div>'
                        + '<div class="columm2" id="xm"></div>'
                        + '<div class="columm3"  style="">合算金额:</div>'
                        + '<div class="columm2" id="name2"></div>'
                    + '</div>';
                    html += '<div class="personnum_BMD" style="height:45px;clear: both;">'
                     + '<div class="columm1" style="margin-left: 40px;">结算人:</div>'
                     + '<div class="columm2" id="xm"></div>'
                     + '<div class="columm3"  style="">结算金额:</div>'
                     + '<div class="columm2" id="name2"></div>'
                 + '</div>"';
                }
                obj.html(html);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });

    },
    getDetails: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: parent.globalConfig.apiconfig + "/api/RemoveBuilding/GetRemoveBuildingEntMoneys",
            data: { EntId: detail.RemoveBuildingInfo.EnterpriseId },
            dataType: "json",
            success: function (data) {
                var str = "<tr class='' style='color:#619ae2'> <th >支付时间</th><th>支付金额</th><th >备注</th><th >附件</th></tr>";
                //str += ' <div class="fileClass" style="width: 100%; margin-top: 10px;color:#619ae2">' +
                //                '<span class="columm2 textAlign" style="margin-left:20px; width:200px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">支付时间</span>' +
                //                '<span class="columm2 textAlign" style="margin-left:20px;width:150px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">支付金额</span>' +
                //                '<span class="columm2 textAlign" style="margin-left:20px;width:150px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">备注</span>' +
                            //'</div>';
                            for (var i = 0; i < data.length; i++) {
                                str += '<tr>' +
                                '<td>' + '<span class="columm2 textAlign" style="margin:0px; width:150px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + (data[i].PaypalTime == null ? "" : data[i].PaypalTime.substr(0, 10)) + '</span>' + '</td>' +
                                            '<td>' + '<span class="columm2 textAlign" style="margin:0px;width:120px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + data[i].ParpalMoney + '</span>' + '</td>' +
                                            '<td>' + '<span class="columm2 textAlign" style="margin:0px;width:120px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + data[i].Remarks + '</span>' + '</td>' +
                                             '<td>';
                                try {
                                    if (data[i].FileName != null) {
                                        try {
                                            var obj = data[i].FileName.split('|');
                                            for (var j = 0; j < obj.length; j++) {
                                                var paths = data[i].FilePath.split('|');
                                                str += '<div ' + (paths[j] == null ? '' : 'onclick="detail.fileClick(\'' + paths[j] + '\')" ') + 'class="columm2 textAlign" style="margin:0px;width:110px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + obj[j] + '</div>';
                                            }
                                        } catch (e) {
                                            console.log(e);
                                        }
                                    }
                                }
                                catch (e)
                                {
                                    console.log(e);
                                    continue;
                                }
                               
                                //str += '<div ' + (data[i].FilePath == null ? '' : 'onclick="detail.fileClick(\'' + data[i].FilePath + '\')" ') + 'class="columm2 textAlign" style="margin:0px;width:110px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + data[i].FileName + '</div>';
                                str += '</td>';
                                str += '</tr>';

                            }
                $("#table1").append(str);

            }
        });
    },
    fileClick: function (path) {
        parent.window.open(parent.globalConfig.removeBuildPath+path);
    },
    optionClick: function () {
        $('.option').each(function (i) {
            $(this).click(function () {
                $(this).addClass("current").siblings().removeClass("current");
                detail.searchType = i;
                detail.personbasedetail();

            })
        })
    },
    getComDetails: function () {
        if ($("#Files div:contains('法人代表')+div").length > 0) {
            $("#Files div:contains('法人代表')+div")[0].textContent = detail.RemoveBuildingInfo.LegalName == null ? " " : detail.RemoveBuildingInfo.LegalName;
        }
        if ($("#Files div:contains('法人代表联系方式')+div").length > 0) {
            $("#Files div:contains('法人代表联系方式')+div")[0].textContent = detail.RemoveBuildingInfo.LegalPhone == null ? " " : detail.RemoveBuildingInfo.LegalPhone;
        }
        if ($("#Files div:contains('土地面积')+div").length > 0) {
            $("#Files div:contains('土地面积')+div")[0].textContent = detail.RemoveBuildingInfo.LandArea == null ? " " : detail.RemoveBuildingInfo.LandArea;
        }
        if ($("#Files div:contains('有证建筑面积')+div").length > 0) {
            var WarrantArea= detail.RemoveBuildingInfo.WarrantArea == null ? " " : detail.RemoveBuildingInfo.WarrantArea;
            //var WarrantArea= detail.RemoveBuildingInfo.WarrantArea == null ? " " : detail.RemoveBuildingInfo.WarrantArea;
            var MeasurementArea = detail.RemoveBuildingInfo.MeasurementArea == null ? " " : detail.RemoveBuildingInfo.MeasurementArea;
            var text = "权证面积：" + WarrantArea + " 丈量面积:" + MeasurementArea;
            $("#Files div:contains('有证建筑面积')+div")[0].textContent = text;
            $("#Files div:contains('有证建筑面积')+div").attr("title", text);
        }
        if ($("#Files div:contains('无证建筑面积')+div").length > 0) {
            $("#Files div:contains('无证建筑面积')+div")[0].textContent = detail.RemoveBuildingInfo.WithoutArea == null ? " " : detail.RemoveBuildingInfo.WithoutArea;
        }
        if ($("#Files div:contains('备注')+div").length > 0) {
            $("#Files div:contains('备注')+div")[0].textContent = detail.RemoveBuildingInfo.Remarks == null ? " " : detail.RemoveBuildingInfo.Remarks;
        }
      
        if ($("#Files div:contains('签约时间')+div").length > 0) {
            $("#Files div:contains('签约时间')+div")[0].textContent = detail.RemoveBuildingInfo.SignTime == null ? " " : detail.RemoveBuildingInfo.SignTime.substr(0, 10);
        }
        if ($("#Files div:contains('腾空时间')+div").length > 0) {
            $("#Files div:contains('腾空时间')+div")[0].textContent = detail.RemoveBuildingInfo.EmptyTime == null ? " " : detail.RemoveBuildingInfo.EmptyTime.substr(0, 10);
        }
        if ($("#Files div:contains('总补偿金额')+div").length > 0) {
            $("#Files div:contains('总补偿金额')+div")[0].textContent = detail.RemoveBuildingInfo.SumMoney == null ? " " : detail.RemoveBuildingInfo.SumMoney;
        }
        if ($("#Files div:contains('所得税补偿金额')+div").length > 0) {
            $("#Files div:contains('所得税补偿金额')+div")[0].textContent = detail.RemoveBuildingInfo.Tax == null ? " " : detail.RemoveBuildingInfo.Tax;
        }
        if ($("#Files div:contains('附件')+div").length > 0) {
            //$("#Files div:contains('附件')+div")[0].textContent = detail.RemoveBuildingInfo.FileName == null ? " " : detail.RemoveBuildingInfo.FileName;
            var html = "";
            if (detail.RemoveBuildingInfo.FileName != null) {
                try{
                    var obj = detail.RemoveBuildingInfo.FileName.split('|');
                    for (var i = 0; i < obj.length; i++) {
                        var paths = detail.RemoveBuildingInfo.FilePath.split('|');
                        if (i != 0) {
                            html += "&nbsp;&nbsp;&nbsp;";
                        }
                        html +=  "<span class='removeFile' " + (paths[i] == null ? '' : 'onclick="detail.fileClick(\'' + paths[i] + '\')" ') + ">" + obj[i] + "</span>";
                    }
                }catch(e){
                    console.log(e);
                }
            }           
            $($("#Files div:contains('附件')+div")[0]).html(html);
        }
    },
    personbasedetail: function () {
        $('.personbasedetail').each(function (j) {
            if (detail.searchType == j) {
                $(this).css("display", "block").siblings().css("display", "none");
                switch (detail.searchType) {
                    case 0:
                        if (detail.Type != 0) {
                            detail.getComDetails();
                        }
                        detail.getDetails_first($(this)); break;
                    case 1: detail.getDetails_second($(this)); break;
                    case 2: detail.getDetails_third($(this)); break;
                    case 3: detail.getDetails_fourth($(this)); break;
                    case 4: detail.getDetails_fifth($(this)); break;
                }
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
$(function () {
    list.init();
});

var list = {
    searchType: 0,
    sumCount: null,
    apiconfig: null,
    takeNum: null,
    skipNum: 0,
    isFirstInit: 0,
    name: null,
    RemoveBuildingList: null,
    init: function () {
        this.apiconfig = parent.globalConfig.apiconfig;
        this.takeNum = parent.globalConfig.listTakeNum;
        this.searchRB();
        $(".minbtn").toggle(function () {
            list.collapse();
        }, function () {
            list.expand();
        });
        list.tabClick();
        this.positionAll();
    },
    tabClick: function () {
        $('.tab div').each(function (i) {
            $(this).click(function () {
                $(this).addClass("tabChecked").siblings().removeClass("tabChecked");
                list.searchType = i;
                list.searchRB();
            })
        })
    },
    searchRB: function () {
        if (list.searchType == 0) {
            list.searchRemoveBuildingPerson();
        } else {
            list.searchRemoveBuilding();
        }
    },
    searchRemoveBuildingPerson: function () {
        this.name = this.getSearchContent;
        this.setLoading();
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/RemoveBuilding/GetRemoveBuildingsCountHouse",
            data: { houseHolder: list.name, startTime: null, endTime: null },
            dataType: "json",
            success: function (result) {
                list.setPageCount(result);
                list.initPage();
                list.getRemoveBuildingPersonList();

            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getRemoveBuildingPersonList: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: list.apiconfig + "/api/RemoveBuilding/GetRemoveBuildingsByPageHouse",
            data: { houseHolder: list.name, startTime: null, endTime: null, takeNum: list.takeNum, skipNum: list.skipNum },
            dataType: "json",
            success: function (data) {
                list.RemoveBuildingList = data;
                var str = "";
                $(".personlist").html("");
                if (data.length == 0) {
                    $(".personlist").html('<p style="margin:0px;font-size:14px;color:#fff;text-align: center;line-height:200px">暂无数据</p>');
                }
                for (var i = 0; i < data.length; i++) {
                    //var icon = data[i].isOnline == 1 ? data[i].isAlarm == 1 ? "Alarm" : "Online" : "Offline";
                    //var msg = data[i].isMessage == 1 ? "1" : "0";
                    var str = '<div class="personlist_list">'
                        + '<a href="javascript:void(0)"  onclick="list.userClicked(' + data[i].HouseId + ');">'
                        + '<div class="statusiconCQ" style="display:block; width:38px;height:30px; line-height:30px;"></div>'
                        + '<div  style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:160px;">' + data[i].HouseHolder + '</div>'
                        //+ '<div style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:71px;">' + data[i].TypeName + '</div>'
                        //+ '<div class="message' + msg + '" style="float:right; display:block;width:33px; height:30px;  line-height:30px;cursor: pointer;"></div>'
                        + '<div class="topline" style="width:190px; height:1px; margin-left:8px;"></div>'
                        + '</a>'
                   + '</div>'
                    $(".personlist").append(str);
                }
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    searchRemoveBuilding: function () {
        this.name = this.getSearchContent;
        this.setLoading();
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/RemoveBuilding/GetRemoveBuildingsCountEnt",
            data: { projectName: list.name, startTime: null, endTime: null },
            dataType: "json",
            success: function (result) {
                list.setPageCount(result);
                list.getRemoveBuildingList();
                list.initPage();
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getRemoveBuildingList: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: list.apiconfig + "/api/RemoveBuilding/GetRemoveBuildingsByPageEnt",
            data: { projectName: list.name, startTime: null, endTime: null, takeNum: list.takeNum, skipNum: list.skipNum },
            dataType: "json",
            success: function (data) {
                list.RemoveBuildingList = data;
                var str = "";
                $(".personlist").html("");
                for (var i = 0; i < data.length; i++) {
                    //var icon = data[i].isOnline == 1 ? data[i].isAlarm == 1 ? "Alarm" : "Online" : "Offline";
                    //var msg = data[i].isMessage == 1 ? "1" : "0";
                    var str = '<div class="personlist_list">'
                        + '<a href="javascript:void(0)" onclick="list.userClicked(' + data[i].EnterpriseId + ');">'
                        + '<div class="statusiconCQ" style="display:block; width:38px;height:30px; line-height:30px;"></div>'
                        + '<div  style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:160px;">' + data[i].ProjectName + '</div>'
                        //+ '<div style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:71px;">' + data[i].TypeName + '</div>'
                        //+ '<div class="message' + msg + '" style="float:right; display:block;width:33px; height:30px;  line-height:30px;cursor: pointer;"></div>'
                        + '<div class="topline" style="width:190px; height:1px; margin-left:8px;"></div>'
                        + '</a>'
                   + '</div>'
                    $(".personlist").append(str);
                }
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    initPage: function () {
        if (this.isFirstInit == 1) {
            //list.takeNum=8;
            list.skipNum = 0;
            $('.pagination').jqPagination('destroy');
        }
        list.isFirstInit = 1;
        $('.pagination').jqPagination({
            current_page: 1,
            link_string: '/?page={page_number}',
            max_page: Math.ceil(this.sumCount / this.takeNum),
            paged: function (page) {
                list.skipNum = (page - 1) * 8;
                list.setLoading();
                if (list.searchType == 0) {
                    list.getRemoveBuildingPersonList();
                } else {
                    list.getRemoveBuildingList();
                }
            }
        });
    },
    getSearchContent: function () {
        return $("#search").val() == "请输入搜索内容" ? "" : $("#search").val();
    },
    setLoading: function () {
        $(".personlist").html("");
        $(".personlist").html('<div id="allbg"><div id="showLoading" class="showLoading" style="top:60px;left:45px;"><img src="/images/list/loading2.gif" style="width: 20px; height: 20px" />' +
        '<br /><p style="font-size: 17px;">正在加载中...</></div></div>');
    },
    setPageCount: function (sumCount) {
        this.sumCount = sumCount;
        $("#pcounts").html("");
        $("#pcounts").html(sumCount);
    },
    userClicked: function (HouseId) {
        for (var i = 0; i < list.RemoveBuildingList.length; i++) {
            if (list.searchType == 0) {
                if (this.RemoveBuildingList[i].HouseId == HouseId) {
                    //显示概要面板
                    this.showDetailMin(this.RemoveBuildingList[i]);
                    //地图定位
                    this.positionRemoveBuilding(this.RemoveBuildingList[i]);
                }
            } else {
                if (this.RemoveBuildingList[i].EnterpriseId == HouseId) {
                    //显示概要面板
                    this.showDetailMin(this.RemoveBuildingList[i]);
                    //地图定位
                    this.positionRemoveBuilding(this.RemoveBuildingList[i]);
                }
            }
        }
    },
    positionRemoveBuilding: function (RemoveBuilding) {
        parent.RemoveBuilding.positionRemoveBuilding(RemoveBuilding, list.searchType);
    },
    positionAll: function () {
        var url = "";
        if (list.searchType == 0) {
            url = '/api/RemoveBuilding/GetRemoveBuildingsByPageHouse';
        } else {
            url = '/api/RemoveBuilding/GetRemoveBuildingsByPageEnt';
        }
        $('.allLocate').click(function () {
            $.ajax({
                type: "GET",
                async: true,
                url: list.apiconfig + url,
                data: { houseHolder: list.name, startTime: null, endTime: null, takeNum: null, skipNum: null },
                dataType: "json",
                success: function (data) {
                    if (data != null && data.length != 0) {
                        parent.RemoveBuilding.positionAll(data);
                    }
                },
                error: function (errorMsg) {
                    console.log(errorMsg);
                }
            });
        });
    },
    showDetailMin: function (RemoveBuilding) {
        parent.RemoveBuilding.initRemoveBuildingDeatilMin(RemoveBuilding, list.searchType);
    },
    collapse: function () {
        parent.list.collapse();
        setTimeout(function () {
            $("body").css("background-image", "url('/images/list/listdivbg5.png')");
        }, 300);
    },
    expand: function () {
        parent.list.expand();
        $("body").css("background-image", "url('/images/list/listdivbg4.png')");
    },
    close: function () {
        parent.list.close();
    },
}
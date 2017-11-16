$(function () {
    list.init(parent.parts.Type);
});

var list = {
    type: null,
    sumCount: null,
    apiconfig: null,
    takeNum: null,
    skipNum: 0,
    partsName: null,
    parts: null,
    UrlCount: "",
    isFirstInit:0,
    UrlPage: "",
    data: {
        parkGreenName:"",
        loadGreenName: "",
        protectionGreenName: "",
        toiltName: "",
        riverName: "",
        takeNum: 8,
        skipNum:0,
    },
    dataReturn: {
        id: "",
        name: "",
        X: null,
        Y:null,
    },
    dataReturnList:[],
    init: function (type) {
        this.type = type;
        this.apiconfig = parent.globalConfig.apiconfig;
        this.takeNum = parent.globalConfig.listTakeNum;
        $(".minbtn").toggle(function () {
            list.collapse();
        }, function () {
            list.expand();
        });
        switch (this.type) {
            case "Road":
                this.searchRoads();
                break;
            case "Bridge":
                this.searchBridges();
                break;
            case "StreetLamp":
                this.searchStreetLamps();
                break;
            case "LandscapeLamp":
                this.searchLandscapeLamps();
                break;
            case "Pump":
                this.searchPumps();
                break;
            case "CoverLoad":
                $('.type').css("display", "inline");
                this.searchCoverLoads();
               
                break;
            case "ParkGreen":
                this.UrlCount = "/api/Parts/GetParkGreensCount";
                this.UrlPage = "/api/Parts/GetParkGreensByPage";
                this.searchParts(); break;
            case "LoadGreen":
                this.UrlCount = "/api/Parts/GetLoadGreensCount";
                this.UrlPage = "/api/Parts/GetLoadGreensByPage";
                this.searchParts(); break;
            case "ProtectionGreen":
                this.UrlCount = "/api/Parts/GetProtectionGreensCount";
                this.UrlPage = "/api/Parts/GetProtectionGreensByPage";
                this.searchParts(); break;
            case "Toilt":
                this.UrlCount = "/api/Parts/GetToiltsCount";
                this.UrlPage = "/api/Parts/GetToiltsByPage";
                this.searchParts(); break;
            case "River":
                this.UrlCount = "/api/Parts/GetRiversCount";
                this.UrlPage = "/api/Parts/GetRiversByPage";
                this.searchParts(); break;
          
        }
    },
    searchParts: function () {
        this.partsName = this.getSearchContent;
        this.setLoading();
        this.data.parkGreenName = this.partsName;
        this.data.loadGreenName = this.partsName;
        this.data.protectionGreenName = this.partsName;
        this.data.toiltName = this.partsName;
        this.data.riverName = this.partsName;
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + this.UrlCount,
            data: this.data,
            dataType: "json",
            success: function (result) {
                list.setPageCount(result);
                list.initPage();
                list.getPartsList();
               
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getPartsList: function () {
        this.data.takeNum = this.takeNum;
        this.data.skipNum = this.skipNum;
        $.ajax({
            type: "GET",
            async: true,
            url: list.apiconfig + list.UrlPage,
            data: list.data,
            dataType: "json",
            success: function (data) {
                list.parts = data;
                list.changeData(data);
               
                var str = "";
                $(".personlist").html("");
                if (data.length == 0) {
                    $(".personlist").html('<p style="margin:0px;font-size:14px;color:#fff;text-align: center;line-height:200px">暂无数据</p>');
                }
                for (var i = 0; i < list.dataReturnList.length; i++) {
                    var str = '<div class="personlist_list">'
                        + '<a href="javascript:void(0)"  onclick="list.partsClicked(' + list.dataReturnList[i].id + ');">'
                        + '<div class="statusicon_parts" style="display:block; width:38px;height:30px; line-height:30px;"></div>'
                        + '<div  style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:160px;">' + list.dataReturnList[i].name + '</div>'
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
    changeData: function (data) {
        this.dataReturnList = [];
        switch (this.type) {
            case "ParkGreen":
                for (var i = 0; i < data.length; i++) {
                    //this.dataReturn.id = data[i].ParkGreenId;
                    //this.dataReturn.name = data[i].PartGreenName;
                    //this.dataReturn.X = data[i].X;
                    //this.dataReturn.Y = data[i].Y;
                    this.dataReturnList.push({
                        id: data[i].ParkGreenId,
                        name: data[i].PartGreenName,
                        X: data[i].X,
                        Y: data[i].Y
                    });
                }
                break;
            case "LoadGreen":
                for (var i = 0; i < data.length; i++) {
                    //this.dataReturn.id = data[i].LoadGreenId;
                    //this.dataReturn.name = data[i].LoadGreenName;
                    //this.dataReturn.X = data[i].X;
                    //this.dataReturn.Y = data[i].Y;
                    //this.dataReturnList.push(this.dataReturn);
                    this.dataReturnList.push({
                        id: data[i].LoadGreenId,
                        name: data[i].LoadGreenName,
                        X: data[i].X,
                        Y: data[i].Y
                    });
                }
                break;
            case "ProtectionGreen":
                for (var i = 0; i < data.length; i++) {
                    //this.dataReturn.id = data[i].ProtectionGreenId;
                    //this.dataReturn.name = data[i].ProtectGreenName;
                    //this.dataReturn.X = data[i].X;
                    //this.dataReturn.Y = data[i].Y;
                    //this.dataReturnList.push(this.dataReturn);
                    this.dataReturnList.push({
                        id: data[i].ProtectionGreenId,
                        name: data[i].ProtectGreenName,
                        X: data[i].X,
                        Y: data[i].Y
                    });
                }
                break;
            case "Toilt":
                for (var i = 0; i < data.length; i++) {
                    //this.dataReturn.id = data[i].ToiltId;
                    //this.dataReturn.name = data[i].ToiltName;
                    //this.dataReturn.X = data[i].X;
                    //this.dataReturn.Y = data[i].Y;
                    //this.dataReturnList.push(this.dataReturn);
                    this.dataReturnList.push({
                        id: data[i].ToiltId,
                        name: data[i].ToiltName,
                        X: data[i].X,
                        Y: data[i].Y
                    });
                }
                break;
            case "River":
                for (var i = 0; i < data.length; i++) {
                    //this.dataReturn.id = data.RiverId;
                    //this.dataReturn.name = data.RiverName;
                    //this.dataReturn.X = data.X;
                    //this.dataReturn.Y = data.Y;
                    //this.dataReturnList.push(this.dataReturn);
                    this.dataReturnList.push({
                        id: data[i].RiverId,
                        name: data[i].RiverName,
                        X: data[i].X,
                        Y: data[i].Y
                    });
                }
                break;
        }
       
    },
    searchCoverLoads: function () {
        this.partsName = this.getSearchContent;
        this.setLoading();
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/Parts/GetCoverLoadsCount",
            data: { coverLoadName: list.partsName },
            dataType: "json",
            success: function (result) {
                list.setPageCount(result);
                list.initPage();
                list.getCoverLoadsList();
                
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getCoverLoadsList: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: list.apiconfig + "/api/Parts/GetCoverLoadsByPage",
            data: { coverLoadName: this.partsName, takeNum: this.takeNum, skipNum: this.skipNum },
            dataType: "json",
            success: function (data) {
                list.parts = data;
                var str = "";
                $(".personlist").html("");
                if (data.length == 0) {
                    $(".personlist").html('<p style="margin:0px;font-size:14px;color:#fff;text-align: center;line-height:200px">暂无数据</p>');
                }
                for (var i = 0; i < data.length; i++) {
                    var str = '<div class="personlist_list">'
                        + '<a href="javascript:void(0)"  onclick="list.partsClicked(' + data[i].CoverLoadId + ');">'
                        + '<div class="statusicon_parts" style="display:block; width:38px;height:30px; line-height:30px;"></div>'

                        + '<div  style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:65px;">' + data[i].CoverLoadName + '</div>'
                         + '<div  style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:65px;">' + data[i].JGXZ_TYPE + '</div>'
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
    searchPumps: function () {
        this.partsName = this.getSearchContent;
        this.setLoading();
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/Parts/GetPumpsCount",
            data: { pumpName: list.partsName },
            dataType: "json",
            success: function (result) {
                list.setPageCount(result);
                list.initPage();
                list.getPumpsList();
               
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getPumpsList: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: list.apiconfig + "/api/Parts/GetPumpsByPage",
            data: { pumpName: this.partsName, takeNum: this.takeNum, skipNum: this.skipNum },
            dataType: "json",
            success: function (data) {
                list.parts = data;
                var str = "";
                $(".personlist").html("");
                if (data.length == 0) {
                    $(".personlist").html('<p style="margin:0px;font-size:14px;color:#fff;text-align: center;line-height:200px">暂无数据</p>');
                }
                for (var i = 0; i < data.length; i++) {
                    var str = '<div class="personlist_list">'
                        + '<a href="javascript:void(0)"  onclick="list.partsClicked(' + data[i].PumpId + ');">'
                        + '<div class="statusicon_parts" style="display:block; width:38px;height:30px; line-height:30px;"></div>'
                        + '<div  style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:160px;">' + data[i].PumpName + '</div>'
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
    searchLandscapeLamps: function () {
        this.partsName = this.getSearchContent;
        this.setLoading();
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/Parts/GetLandscapeLampsCount",
            data: { lampName: list.partsName },
            dataType: "json",
            success: function (result) {
                list.setPageCount(result);
                list.initPage();
                list.getLandscapeLampsList();
                
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getLandscapeLampsList: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: list.apiconfig + "/api/Parts/GetLandscapeLampsByPage",
            data: { lampName: this.partsName, takeNum: this.takeNum, skipNum: this.skipNum },
            dataType: "json",
            success: function (data) {
                list.parts = data;
                var str = "";
                $(".personlist").html("");
                if (data.length == 0) {
                    $(".personlist").html('<p style="margin:0px;font-size:14px;color:#fff;text-align: center;line-height:200px">暂无数据</p>');
                }
                for (var i = 0; i < data.length; i++) {
                    var str = '<div class="personlist_list">'
                        + '<a href="javascript:void(0)"  onclick="list.partsClicked(' + data[i].LLId + ');">'
                        + '<div class="statusicon_parts" style="display:block; width:38px;height:30px; line-height:30px;"></div>'
                        + '<div  style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:160px;">' + data[i].LLName + '</div>'
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
    searchStreetLamps: function () {
        this.partsName = this.getSearchContent;
        this.setLoading();
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/Parts/GetStreetLampLoadsCount",
            data: { loadName: list.partsName },
            dataType: "json",
            success: function (result) {
                list.setPageCount(result);
                list.initPage();
                list.getStreetLampsList();
                
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getStreetLampsList: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: list.apiconfig + "/api/Parts/GetStreetLampLoadsByPage",
            data: { loadName: this.partsName, takeNum: this.takeNum, skipNum: this.skipNum },
            dataType: "json",
            success: function (data) {
                list.parts = data;
                var str = "";
                $(".personlist").html("");
                if (data.length == 0) {
                    $(".personlist").html('<p style="margin:0px;font-size:14px;color:#fff;text-align: center;line-height:200px">暂无数据</p>');
                }
                for (var i = 0; i < data.length; i++) {
                    var str = '<div class="personlist_list">'
                        + '<a href="javascript:void(0)" style="color:#87a7c8;" onclick="list.partsClicked(' + data[i].SLLId + ');">'
                        + '<div class="statusicon_parts" style="display:block; width:38px;height:30px; line-height:30px;"></div>'
                        + '<div  style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:160px;">' + data[i].SLLName + '</div>'
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
    searchBridges:function(){
        this.partsName = this.getSearchContent;
        this.setLoading();
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/Parts/GetBirdgesCount",
            data: { bridgeName: list.partsName },
            dataType: "json",
            success: function (result) {
                list.setPageCount(result);
                list.initPage();
                list.getBridgesList();
                
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getBridgesList: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: list.apiconfig + "/api/Parts/GetBirdgesByPage",
            data: { bridgeName: this.partsName, takeNum: this.takeNum, skipNum: this.skipNum },
            dataType: "json",
            success: function (data) {
                list.parts = data;
                var str = "";
                $(".personlist").html("");
                if (data.length == 0) {
                    $(".personlist").html('<p style="margin:0px;font-size:14px;color:#fff;text-align: center;line-height:200px">暂无数据</p>');
                }
                for (var i = 0; i < data.length; i++) {
                    var str = '<div class="personlist_list">'
                        + '<a href="javascript:void(0)" style="color:#87a7c8;" onclick="list.partsClicked(' + data[i].BridgeId + ');">'
                        + '<div class="statusicon_parts" style="display:block; width:38px;height:30px; line-height:30px;"></div>'
                        + '<div  style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:64px;">' + data[i].BridgeName + '</div>'
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
    searchRoads: function () {
        this.partsName = this.getSearchContent;
        this.setLoading();
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/Parts/GetRoadsCount",
            data: { roadName: list.partsName },
            dataType: "json",
            success: function (result) {
                list.setPageCount(result);
                list.initPage();
                list.getRoadsList();
                
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getRoadsList: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: list.apiconfig + "/api/Parts/GetRoadsByPage",
            data: { roadName: this.partsName, takeNum: this.takeNum, skipNum: this.skipNum },
            dataType: "json",
            success: function (data) {
                list.parts = data;
                var str = "";
                $(".personlist").html("");
                if (data.length == 0) {
                    $(".personlist").html('<p style="margin:0px;font-size:14px;color:#fff;text-align: center;line-height:200px">暂无数据</p>');
                }
                for (var i = 0; i < data.length; i++) {
                    var str = '<div class="personlist_list">'
                        + '<a href="javascript:void(0)" style="color:#87a7c8;" onclick="list.partsClicked(' + data[i].RoadId + ');">'
                        + '<div class="statusicon_parts" style="display:block; width:38px;height:30px; line-height:30px;"></div>'
                        + '<div  style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:64px;">' + data[i].RoadName + '</div>'
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

                switch (list.type) {
                    case "Road":
                        list.getRoadsList();
                        break;
                    case "Bridge":
                        list.getBridgesList();
                        break;
                    case "StreetLamp":
                        list.getStreetLampsList();
                        break;
                    case "LandscapeLamp":
                        list.getLandscapeLampsList();
                        break;
                    case "Pump":
                        list.getPumpsList();
                        break;
                    case "CoverLoad":
                        list.getCoverLoadsList();
                        break;
                    default: list.getPartsList(); break;
                }
            }
        });
    },
    partsClicked: function (partId) {
        for (var i = 0; i < list.parts.length; i++) {
            switch (this.type) {
                case "Road":
                    if (this.parts[i].RoadId == partId) {
                        //显示概要面板
                        this.showDetailMin(this.parts[i]);
                        //地图定位
                        this.positionRoad(this.parts[i]);
                    }
                    break;
                case "Bridge":
                    if (this.parts[i].BridgeId == partId) {
                        //显示概要面板
                        this.showDetailMin(this.parts[i]);
                        //地图定位
                        this.positionBridge(this.parts[i]);
                    }
                    break;
                case "StreetLamp":
                    if (this.parts[i].SLLId == partId) {
                        //显示概要面板
                        this.showDetailMin(this.parts[i]);
                        //地图定位
                        this.positionStreetLamp(this.parts[i]);
                    }
                    break;
                case "LandscapeLamp":
                    if (this.parts[i].LLId == partId) {
                        //显示概要面板
                        this.showDetailMin(this.parts[i]);
                        //地图定位
                        this.positionLandscapeLamp(this.parts[i]);
                    }
                    break;
                case "Pump":
                    if (this.parts[i].PumpId == partId) {
                        //显示概要面板
                        this.showDetailMin(this.parts[i]);
                        //地图定位
                        this.positionPump(this.parts[i]);
                    }
                    break;
                case "CoverLoad":
                    if (this.parts[i].CoverLoadId == partId) {
                        //显示概要面板
                        this.showDetailMin(this.parts[i]);
                        //地图定位
                        this.positionCoverLoad(this.parts[i]);
                    }
                    break;
                default:
                    if (this.dataReturnList[i].id == partId) {
                        //显示概要面板
                        this.showDetailMin(this.dataReturnList[i]);
                        //地图定位
                        this.positionPart(this.dataReturnList[i]);
                    }
                    break;
                //case "LoadGreen":
                //    this.UrlCount = "/api/Parts/GetLoadGreensCount";
                //    this.UrlPage = "/api/Parts/GetLoadGreensByPage";
                //    this.searchParts(); break;
                //case "ProtectionGreen":
                //    this.UrlCount = "/api/Parts/GetProtectionGreensCount";
                //    this.UrlPage = "/api/Parts/GetProtectionGreensByPage";
                //    this.searchParts(); break;
                //case "Toilt":
                //    this.UrlCount = "/api/Parts/GetToiltsCount";
                //    this.UrlPage = "/api/Parts/GetToiltsByPage";
                //    this.searchParts(); break;
                //case "River":
                //    this.UrlCount = "/api/Parts/GetRiversCount";
                //    this.UrlPage = "api/Parts/GetRiversByPage";
                //    this.searchParts(); break;
            }
          
        }
    },
    positionPart: function (part) {
        parent.parts.positionPart(part);
    },
    positionCoverLoad: function (part) {
        parent.parts.positionCoverLoad(part);
    },
    positionPump: function (part) {
        parent.parts.positionPump(part);
    },
    positionLandscapeLamp: function (part) {
        parent.parts.positionLandscapeLamp(part);
    },
    positionStreetLamp: function (part) {
        parent.parts.positionStreetLamp(part);
    },
    positionRoad: function (part) {
        parent.parts.positionRoad(part);
    },
    positionBridge: function (part) {
        parent.parts.positionBridge(part);
    },
    showDetailMin: function (part) {
        parent.parts.intiDetailMin(part);
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
    searchBtnClick: function () {
        switch (this.type) {
            case "Road":
                this.searchRoads();
                break;
            case "Bridge":
                this.searchBridges();
                break;
            case "StreetLamp":
                this.searchStreetLamps();
                break;
            case "LandscapeLamp":
                this.searchLandscapeLamps();
                break;
            case "Pump":
                this.searchPumps();
                break;
            case "CoverLoad":
                $('.type').css("display", "inline");
                this.searchCoverLoads();

                break;
          default:
              this.searchParts(); break;

        }
    }
}
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
    IsAlarm: null,
    Node: null,
}
var head = {
    hideMessWarm: function (isHide) {
        if (!isHide) {
            $('.container').css("display", "block");

        }
        else {
            $('.container').css("display", "none");
        }
    }
}

//拆迁
var RemoveBuilding = {
    RemoveBuildingInfo: null,
    Type: 0,
    initRemoveBuildingList: function () {
        list.show("Views/RemoveBuilding/RemoveBuildingList.aspx");
    },
    initRemoveBuildingDeatilMin: function (Info, Type) {
        this.RemoveBuildingInfo = Info;
        this.Type = Type;
        detailMin.show("/Views/RemoveBuilding/RemoveBuildingMin.aspx");
        this.initDetail(Info);
    },
    initDetail: function (Info) {
        this.RemoveBuildingInfo = Info;
        detail.close();
        detail.show("Views/RemoveBuilding/RemoveBuildingDetail.aspx");
    },
    positionRemoveBuilding: function (Info, Type) {

        //var listMapElementModel = [];
        mapElementModel.Id = Info.ProjectId;
        mapElementModel.Type = "RemoveBuildingModel";
        mapElementModel.Name = Info.ProjectName;
        //mapElementModel.Circum = "UserModel,CarModel,TaskModel";
        mapElementModel.X = null;
        mapElementModel.Y = null;
        mapElementModel.Areas = Info.Geometry;
        mapElementModel.IsOnline = 1;
        mapElementModel.IsAlarm = 0;
        map.position(mapElementModel);
    },
    positionAll: function (data) {
        var listModel = [];
        for (var i = 0; i < data.length; i++) {
            if (data[i].Geometry == null) {
                continue;
            }
            listModel.push({
                Id: data[i].ProjectId,
                Type: "RemoveBuildingModel",
                Name: (data[i].ProjectName == null ? "" : data[i].ProjectName),
                Areas: data[i].Geometry,
                //Areas:  areaPosition[i%2],
                IsOnline: 1,
                IsAlarm: 0
            });
        }
        map.positions(listModel);
    }
}
//违建
var NonConforBuild = {
    NonConforBuildInfo: null,
    initNonConforBuildList: function () {
        list.show("Views/nonconformingBuilding/NonConforBuildList.aspx");
    },
    initNonConforBuildDeatilMin: function (Info) {
        this.NonConforBuildInfo = Info;
        detailMin.show("Views/nonconformingBuilding/NonConforBuildDetailMin.aspx");
        this.initDetail(Info);
    },
    initDetail: function (Info) {
        this.NonConforBuildInfo = Info;
        detail.close();
        detail.show("Views/nonconformingBuilding/NonConforBuildDetail.aspx");
    },
    positionNonConforBuild: function (Info) {

        //var listMapElementModel = [];
        mapElementModel.Id = Info.IBId;
        mapElementModel.Type = "IllegalBuildingModel";
        mapElementModel.Name = Info.Address;
        //mapElementModel.Circum = "UserModel,CarModel,TaskModel";
        mapElementModel.X = Info.X;
        mapElementModel.Y = Info.Y;
        mapElementModel.IsOnline = 1;
        mapElementModel.IsAlarm = 0;
        map.position(mapElementModel);
    },
    positionAll: function (data) {
        var listModel = [];
        for (var i = 0; i < data.length; i++) {
            //mapElementModel.Id = data[i].IBId;
            //mapElementModel.Type = "IllegalBuildingModel";
            //mapElementModel.Name = data[i].Address;

            ////mapElementModel.X = data[i].X;
            ////mapElementModel.Y = data[i].Y;
            //var j = i % 9;
            //mapElementModel.X = testPosition[j].x;
            //mapElementModel.Y = testPosition[j].y;

            //mapElementModel.IsOnline = 1;
            //mapElementModel.IsAlarm = 0;
            //添加
            var j = i % 9;
            listModel.push({
                Id: data[i].IBId,
                Type: "IllegalBuildingModel",
                Name: data[i].Address,
                X: data[i].X,
                Y: data[i].Y,
                //X : testPosition[j].x,
                //Y : testPosition[j].y,
                IsOnline: 1,
                IsAlarm: 0
            });
        }
        try {
            map.positions(listModel);
        } catch (e) {
            console.log(e.toString());
        }
    }
}
//361214.176911319,3302337.10827024;361207.107108668,3302199.24711854;361207.107108668,3302114.40948672;361369.712569654,3301962.40872971;361677.248984997,3301672.546821;361861.063853937,3301506.40645869;361892.877965869,3301492.26685338;362044.878722878,3301937.66442043;362299.391618334,3302347.71297422;362140.321058674,3302450.22511267;361945.901485756,3302581.01646172;361786.830926096,3302322.96866494;361454.550201472,3302330.03846759;361440.410596169,3302337.10827024;361207.107108668,3302333.57336892
var testPosition = [{ x: 361214.176911319, y: 3302337.10827024 },
{ x: 361207.107108668, y: 3302199.24711854 },
{ x: 361207, y: 3302114.4094867 },
{ x: 361369.712569654, y: 3301962.40872971 },
{ x: 361677.248984997, y: 3301672.546821 },
{ x: 361861.063853937, y: 3302337.10827024 },
{ x: 362044.8787228, y: 3301937.66442043 },
{ x: 362299.391618334, y: 3302347.7129742 },
{ x: 362140.321058674, y: 3302450.22511267 }]
var areaPosition = ["361214.176911319,3302337.10827024;361207.107108668,3302199.24711854;361207.107108668,3302114.40948672;361369.712569654,3301962.40872971;361677.248984997,3301672.546821;361861.063853937,3301506.40645869;361892.877965869,3301492.26685338;362044.878722878,3301937.66442043;362299.391618334,3302347.71297422;362140.321058674,3302450.22511267;361945.901485756,3302581.01646172;361786.830926096,3302322.96866494;361454.550201472,3302330.03846759;361440.410596169,3302337.10827024;361207.107108668,3302333.57336892", "362299.391618334,3302351.24787555;362730.64958008,3302057.85106551;362571.579020419,3301559.42997857;362522.090401859,3301382.68491228;362444.322572691,3301142.31162213;362363.019842198,3300937.28734524;362274.647309054,3300976.17125982;362126.181453371,3301046.86928633;361793.900728747,3301205.93984599;361889.343064544,3301495.80175471;361974.180696362,3301732.64014354;362044.878722878,3301937.66442043;362225.158690493,3302213.38672384;362306.461420986,3302351.24787555"];
//监控
var Camera = {
    CameraInfo: null,
    initCameraList: function () {
        list.show("Views/Camera/CameraList.aspx");
    },
    initDetail: function () {
        //detail.show("Views/Camera/CameraDetail.aspx");
        //window.open('http://localhost:18279/Views/Camera/CameraDetail.aspx', 'newwindow', 'height=450,width=750,top=60,left=300,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no')
        var param = "?CameraId=" + Camera.CameraInfo.CameraId
            + "&IndexCode=" + Camera.CameraInfo.IndexCode
            + "&CameraName=" + Camera.CameraInfo.CameraName
            + "&DeviceId=" + Camera.CameraInfo.DeviceId;
        //+ "&Parameter=" + Camera.CameraInfo.Parameter;
        //layer.open({
        //    type: 2,
        //    area: ['750px', '450px'],
        //    fix: true, //不固定
        //    maxmin: true,
        //    content: 'http://localhost:18279/Views/Camera/CameraDetail.aspx' + param
        //});
        window.showModalDialog("CameraDetail.aspx" + param, null, "dialogHeight:450px;dialogWidth:750px;dialogTop=60;dialogLeft=300");
        //window.showModelessDialog("http://localhost:18279/Views/Camera/CameraDetail.aspx", window, "dialogHeight:450px;dialogWidth:750px;status=no;top=60,left=300");
    },
    initDetailBMD: function () {

        var param = "?CameraId=" + Camera.CameraInfo.CameraId
            + "&IndexCode=" + Camera.CameraInfo.IndexCode
            + "&CameraName=" + Camera.CameraInfo.CameraName
            + "&DeviceId=" + Camera.CameraInfo.DeviceId;

        window.showModalDialog("/Views/Camera/CameraDetail.aspx" + param, null, "dialogHeight:450px;dialogWidth:750px;dialogTop=60;dialogLeft=300");

    },
    initCameraDeatilMin: function (node) {
        //$.ajax({
        //    type: "GET",
        //    async: true,
        //    url: globalConfig.apiconfig + "/api/Camera/GetCameraInfo",
        //    data: { cameraId: node.id },
        //    dataType: "json",
        //    success: function (data) {
        Camera.CameraInfo = node.attributes;

        detailMin.show("Views/Camera/CameraDetailMin.aspx");

        this.positionCamera(Camera.CameraInfo);
        this.initDetail(Camera.CameraInfo);

        //    }
        //});
    },
    initCameraDeatilMin2: function (node) {
        //$.ajax({
        //    type: "GET",
        //    async: true,
        //    url: globalConfig.apiconfig + "/api/Camera/GetCameraInfo",
        //    data: { cameraId: node.id },
        //    dataType: "json",
        //    success: function (data) {
        Camera.CameraInfo = node;
        detailMin.show("Views/Camera/CameraDetailMin.aspx");
        this.initDetailBMD(Camera.CameraInfo);
        //this.positionCamera(Camera.CameraInfo);

        //    }
        //});
    },
    mapElementClicked: function (CameraInfo) {
        detailMin.close();
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/Camera/GetCameraInfo",
            data: { cameraId: CameraInfo.Id },
            dataType: "json",
            success: function (data) {
                Camera.CameraInfo = data;
                detailMin.show("Views/Camera/CameraDetailMin.aspx");
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
        //Camera.CameraInfo = {
        //    Id: CameraInfo.Id, CameraName: CameraInfo.Name,
        //    X: CameraInfo.X, Y: CameraInfo.Y, CameraTypeName: CameraInfo.Node
        //};
        //Camera.initCameraDeatilMin2(Camera.CameraInfo);

    },
    mapElementClickedByTHC: function (CameraInfo) {
        detailMin.close();
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/Camera/GetCameraInfoByTHCId",
            data: { tchId: CameraInfo.Id },
            dataType: "json",
            success: function (data) {
                Camera.CameraInfo = data;
                detailMin.show("Views/Camera/CameraDetailMin.aspx");
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
        //Camera.CameraInfo = {
        //    Id: CameraInfo.Id, CameraName: CameraInfo.Name,
        //    X: CameraInfo.X, Y: CameraInfo.Y, CameraTypeName: CameraInfo.Node
        //};
        //Camera.initCameraDeatilMin2(Camera.CameraInfo);

    },
    positionCamera: function (Info) {
        mapElementModel.Id = Info.CameraId;
        mapElementModel.Type = "CameraModel";
        mapElementModel.Name = Info.CameraName;
        mapElementModel.Circum = "UserModel,CarModel,TaskModel";
        mapElementModel.X = Info.X;
        mapElementModel.Y = Info.Y;
        mapElementModel.IsOnline = 1;
        mapElementModel.IsAlarm = 0;
        mapElementModel.Node = Info.CameraTypeName;
        map.position(mapElementModel);

    },
    positionCameras: function (Info) {
        var mapElementModels = [];
        for (var i = 0; i < Info.length; i++) {
            //mapElementModel.Id = Info[i].attributes.CameraId;
            //mapElementModel.Type = "CameraModel";
            //mapElementModel.Name = Info[i].attributes.CameraName;
            //mapElementModel.Circum = "UserModel,CarModel,TaskModel";
            //mapElementModel.X = Info[i].attributes.X;
            //mapElementModel.Y = Info[i].attributes.Y;
            //mapElementModel.IsOnline = 1;
            //mapElementModel.IsAlarm = 0;
            mapElementModels.push({
                Id: Info[i].attributes.THCId, Type: "CameraDrawLine", Name: Info[i].attributes.CameraName, Circum: "UserModel,CarModel,TaskModel",
                X: Info[i].attributes.X, Y: Info[i].attributes.Y, IsOnline: 1, IsAlarm: 0, Node: Info[i].CameraTypeName
            });
        }
        map.positions(mapElementModels);

    },
    positionCameras2: function (Info) {
        var mapElementModels = [];
        for (var i = 0; i < Info.length; i++) {
            mapElementModels.push({
                Id: Info[i].attributes.CameraId, Type: "CameraModel", Name: Info[i].attributes.CameraName, Circum: "UserModel,CarModel,TaskModel",
                X: Info[i].attributes.X, Y: Info[i].attributes.Y, IsOnline: 1, IsAlarm: 0, Node: Info[i].attributes.CameraTypeName
            });
        }
        map.positions(mapElementModels);

    },
    foundCircumCamera: function (obj) {
        mapElementModel.Id = obj.CameraId;
        mapElementModel.Type = "CameraModel";
        mapElementModel.Name = obj.CameraName;
        mapElementModel.Circum = "CameraModel";
        mapElementModel.X = obj.X;
        mapElementModel.Y = obj.Y;
        mapElementModel.IsOnline = 1;
        mapElementModel.IsAlarm = 0;
        map.foundCircumCamera(mapElementModel);
    }
}
var cameraCenter = {
    init: function (user) {
        //var url = "http://" + window.location.host + "/CameraCenter.aspx?UserId=" + user.UserId
        //+ "&X=" + user.X2000 + "&Y=" + user.Y2000;
        var temp = new Object();
        temp.UserId = user.UserId;
        temp.Type = "UserModel";
        temp.Name = encodeURI(encodeURI(user.UserName));
        temp.X = user.X2000;
        temp.Y = user.Y2000;
        temp.IsOnline = user.IsOnline;
        temp.IsAlarm = user.IsAlarm;
        var url = "http://" + window.location.host + "/CameraCenter.aspx?User=" + JSON.stringify(temp);
        document.cookie = "User=" + JSON.stringify(temp);
        window.open(url);
    }
}

var Constr = {
    ConstrInfo: null,
    initConstrList: function () {
        list.show("Views/Constr/ConstrList.aspx");
    },
    initConstrDeatilMin: function (Info) {
        this.ConstrInfo = Info;
        detailMin.show("Views/Constr/ConstrDetailMin.aspx");
        this.initDetail(Info);
    },
    initDetail: function (Info) {
        this.ConstrInfo = Info;
        detail.close();
        detail.show("Views/Constr/ConstrDetail.aspx");
    },
    positionConstr: function (Info) {
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/Constr/GetAreas",
            data: { constrId: Info.ConstrId },
            dataType: "json",
            success: function (data) {
                //var listMapElementModel = [];
                mapElementModel.Id = Info.ConstrId;
                mapElementModel.Type = "ConstructionModel";
                mapElementModel.Name = Info.ConstrName;
                //mapElementModel.Circum = "UserModel,CarModel,TaskModel";
                mapElementModel.Areas = "";
                mapElementModel.IsOnline = 1;
                mapElementModel.IsAlarm = 0;
                mapElementModel.X = null;
                mapElementModel.Y = null;


                for (var i = 0; i < data.length; i++) {
                    if (i != 0) {
                        mapElementModel.Areas += "|" + data[i].Geometry;
                    }
                    else {
                        mapElementModel.Areas += data[i].Geometry;
                    }

                }
                //listMapElementModel.push(mapElementModel);
                if (mapElementModel.Areas != "") {
                    map.position(mapElementModel);
                }
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    positionAll: function (data) {
        var listModel = [];
        for (var i = 0; i < data.length; i++) {
            if (data[i].Geometry == null) {
                continue;
            }
            listModel.push({
                Id: data[i].ConstrId,
                Type: "ConstructionModel",
                Name: (data[i].ConstrName==null?"":data[i].ConstrName),
                Areas: data[i].Geometry,
                IsOnline: 1,
                IsAlarm: 0
            });
        }
        map.positions(listModel);
    }
}


//白名单
var BMD = {
    BMDInfo: null,
    initBMDList: function () {
        list.show("Views/BMD/BMDList.aspx");
    },
    initBMDDeatilMin: function (Info) {
        this.BMDInfo = Info;
        detailMin.show("Views/BMD/BMDDetailMin.aspx");
        if (globalConfig.mapFlag == 3) {
            map.mapChange();
        }
        this.initDetail(Info);
    },
    initDetail: function (Info) {
        this.BMDInfo = Info;
        detail.close();
        detail.show("Views/BMD/BMDDetail.aspx");
    },
    positionBMD: function (Info) {
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/BMD/GetBMDAreas",
            data: { BMDId: Info.BMDId },
            dataType: "json",
            success: function (data) {
                //var listMapElementModel = [];
                mapElementModel.Id = Info.BMDId;
                mapElementModel.Type = "BMDAreaModel";
                mapElementModel.Name = Info.Name;
                //mapElementModel.Circum = "UserModel,CarModel,TaskModel";
                mapElementModel.Areas = "";
                mapElementModel.IsOnline = 1;
                mapElementModel.IsAlarm = 0;

                for (var i = 0; i < data.length; i++) {
                    if (i != 0) {
                        mapElementModel.Areas += "|" + data[i].Geometry;
                    }
                    else {
                        mapElementModel.Areas += data[i].Geometry;
                    }

                }
                //listMapElementModel.push(mapElementModel);
                if (mapElementModel.Areas != "") {
                    map.position(mapElementModel);
                }
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    }
};

//人员
var person = {
    UserId: null,
    UnitId: null,
    User: null,
    //初始化人员列表
    initPersonList: function (unitId) {
        this.UnitId = unitId == null ? "" : unitId;
        list.show("Views/Person/PersonList.aspx");
    },
    initPersonDeatilMin: function (user) {
        this.User = user;
        detailMin.show("Views/Person/PersonDetailMin.aspx");
        this.initDetail(user);
    },
    initDetail: function (user) {
        this.User = user;
        detail.close();
        detail.show("Views/Person/PersonDetail.aspx");
    },
    positionEventSB: function (eventInfo) {//定位人员上报事件
        mapElementModel.Id = eventInfo.ZFSJId;
        mapElementModel.Type = "TaskSBModel";
        mapElementModel.Name = eventInfo.EventTitle;
        mapElementModel.Circum = "UserModel,CarModel,TaskModel";
        mapElementModel.X = eventInfo.X2000;
        mapElementModel.Y = eventInfo.Y2000;
        mapElementModel.IsOnline = 1;
        mapElementModel.IsAlarm = 0;
        map.position(mapElementModel);
    },
    eventSBDetail: function (eventId) {
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/Task/GetTaskByTaskId",
            data: { taskId: eventId },
            dataType: "json",
            success: function (data) {
                event.Event = data;
                event.initDetail(data);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getUserAlarmArea: function (user, startTime, endTime) {
        //this.positionUser(user);
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/Area/GetUserAreas",
            data: { userId: user.UserId },
            dataType: "json",
            success: function (dataSelf) {
                var selfSearchArea = dataSelf;
                var DrawModel = [];
                for (var i = 0; i < selfSearchArea.length; i++) {
                    DrawModel.push({
                        ID: selfSearchArea[i].AreaId,
                        Type: "Polygon",
                        Points: selfSearchArea[i].Geometry,
                        Style: 2,
                        UserName: selfSearchArea[i].AreaName
                    });
                }

                $.ajax({
                    type: "GET",
                    async: true,
                    url: globalConfig.apiconfig + "/api/User/GetUserPositions",
                    data: { userId: user.UserId, startTime: startTime, endTime: endTime },
                    dataType: "json",
                    success: function (data) {
                        var AreaId = 0;
                        var Points = "";
                        var UserName = user.UserName;
                        for (var j = 0; j < data.length; j++) {
                            AreaId = data[j].UPId;
                            if (data[j].X2000 != null && data[j].Y2000 != null) {
                                Points += ";" + data[j].X2000 + "," + data[j].Y2000;
                            }
                        }
                        Points = Points.substr(1, Points.length - 1);
                        if (Points.length > 0) {
                            DrawModel.push({
                                ID: AreaId,
                                Type: "Polyline",
                                Points: Points,
                                Style: 1,
                                UserName: UserName
                            });
                        }
                        map.getSearchArea(DrawModel);
                    },
                    error: function (errorMsg1) {
                        console.log(errorMsg1);
                    }
                });
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getSearchArea: function (user) {
        //clearInterval(globalConfig.refreshPosition);
        var DrawModel = [];
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/Area/GetAllAreas",
            data: { typeId: 1 },
            dataType: "json",
            success: function (data) {
                var allSearchArea = data;
                $.ajax({
                    type: "GET",
                    async: true,
                    url: globalConfig.apiconfig + "/api/Area/GetUserAreas",
                    data: { userId: user.UserId },
                    dataType: "json",
                    success: function (dataSelf) {
                        var selfSearchArea = dataSelf;
                        for (var i = 0; i < allSearchArea.length; i++) {
                            var isSelf = 0;
                            for (var j = 0; j < selfSearchArea.length; j++) {
                                if (allSearchArea[i].AreaId == selfSearchArea[j].AreaId) {
                                    isSelf = 1;
                                    break;
                                }
                            }
                            //  Red-1 Blue-2 Green-3 Yellow-4 Borrow(棕色)-5
                            if (isSelf == 1) {
                                DrawModel.push({
                                    ID: allSearchArea[i].AreaId,
                                    Type: "Polygon",
                                    Points: allSearchArea[i].Geometry,
                                    Style: 2,
                                    UserName: allSearchArea[i].AreaName
                                });
                            } else {
                                var styleFlag = 1;
                                if (allSearchArea[i].Color == "Green") {
                                    styleFlag = 3;
                                }
                                DrawModel.push({
                                    ID: allSearchArea[i].AreaId,
                                    Type: "Polygon",
                                    Points: allSearchArea[i].Geometry,
                                    Style: styleFlag,
                                    UserName: allSearchArea[i].AreaName
                                });
                            }
                        }
                        map.getSearchArea(DrawModel);
                    },
                    error: function (errorMsg1) {
                        console.log(errorMsg1);
                    }
                });
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    positionUser: function (user) {
        this.User = user;
        mapElementModel.Id = user.UserId;
        mapElementModel.Type = "UserModel";
        mapElementModel.Name = user.UserName;
        mapElementModel.Circum = "UserModel,CarModel,TaskModel";
        mapElementModel.X = user.X2000;
        mapElementModel.Y = user.Y2000;
        mapElementModel.IsOnline = user.IsOnline;
        mapElementModel.IsAlarm = user.IsAlarm;
        map.position(mapElementModel);
        clearInterval(globalConfig.refreshPosition);
        //person.getUserPositionById(user);
        globalConfig.refreshPosition = setInterval("person.getUserPositionById()", 15000);
    },
    getUserPositionById: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/User/GetUserByUserId",
            data: { userId: this.User.UserId },
            dataType: "json",
            success: function (data) {
                var models = [];
                mapElementModel.Id = data.UserId;
                mapElementModel.Type = "UserModel";
                mapElementModel.Name = data.UserName;
                mapElementModel.Circum = "UserModel,CarModel,TaskModel";
                mapElementModel.X = data.X2000;
                mapElementModel.Y = data.Y2000;
                mapElementModel.IsOnline = data.IsOnline;
                mapElementModel.IsAlarm = data.IsAlarm;
                models.push(mapElementModel);
                //map.updatePosition(mapElementModel);
                var jsonstr = JSON.stringify(models);
                mapSW.position(mem);
                main.slContent.Content.MainPage.UpdateUserPosition(jsonstr);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    positionAllUser: function (user) {
        var listUser = [];
        for (var i = 0; i < user.length; i++) {
            if (user[i].X2000 == null || user[i].Y2000 == null) {
                continue;
            }
            listUser.push({ Id: user[i].UserId, Type: "UserModel", Name: user[i].UserName, Circum: "UserModel,CarModel,TaskModel", X: user[i].X2000, Y: user[i].Y2000, IsOnline: user[i].IsOnline, IsAlarm: user[i].IsAlarm });
        }
        map.positions(listUser);
    },
    positionAllUserInTime: function (username, unitId) {
        clearInterval(globalConfig.refreshPosition);
        //person.getUserPositionById(user);
        globalConfig.refreshPosition = setInterval("person.updateAllUserInTime(\"" + username + "\"," + unitId + ")", 15000);
    },
    updateAllUserInTime: function (username, unitId) {
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/User/GetUsersByPage",
            data: { userName: username, unitId: unitId, takeNum: null, skipNum: null },
            dataType: "json",
            success: function (data) {
                //parent.person.positionAllUser(data);
                var listUser = [];
                for (var i = 0; i < data.length; i++) {
                    if (data[i].X2000 == null || data[i].Y2000 == null) {
                        continue;
                    }
                    listUser.push({ Id: data[i].UserId, Type: "UserModel", Name: data[i].UserName, Circum: "UserModel,CarModel,TaskModel", X: data[i].X2000, Y: data[i].Y2000, IsOnline: data[i].IsOnline, IsAlarm: data[i].IsAlarm });
                }
                var jsonstr = JSON.stringify(listUser);
                main.slContent.Content.MainPage.UpdateUserPosition(jsonstr);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    traceReplay: function (user) {
        mapElementModel.Id = user.UserId;
        mapElementModel.Type = "UserModel";
        mapElementModel.Name = user.UserName;
        mapElementModel.X = user.X2000;
        mapElementModel.Y = user.Y2000;
        mapElementModel.IsOnline = user.IsOnline;
        mapElementModel.IsAlarm = user.IsAlarm;
        map.traceReplay(user);
       // clearInterval(globalConfig.refreshPosition);
    },
    foundCircum: function (user) {
        mapElementModel.Id = user.UserId;
        mapElementModel.Type = "UserModel";
        mapElementModel.Name = user.UserName;
        mapElementModel.Circum = "UserModel,CarModel,TaskModel";
        mapElementModel.X = user.X2000;
        mapElementModel.Y = user.Y2000;
        mapElementModel.IsOnline = user.IsOnline;
        mapElementModel.IsAlarm = user.IsAlarm;
        map.foundCircum(mapElementModel);
    },
    positionUsers: function (users) {
    },
    showMessageContent: function (user) {
        if (user != null) {
            Message.showContent(user);
        }
    },
    mapElementClicked: function (userId) {
        detailMin.close();
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/User/GetUserByUserId",
            data: { userId: userId },
            dataType: "json",
            success: function (data) {
                person.User = data;
                person.initPersonDeatilMin(data);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    }
}


var car = {
    Car: null,
    initCarList: function () {
        list.show("Views/Car/CarList.aspx");
    },
    initDetailMin: function (car) {
        this.Car = car;
        detailMin.show("Views/Car/CarDetailMin.aspx");
    },
    initDetail: function (car) {
        //this.Car = car;
        //detail.show("Views/Car/CarDetail.aspx");
    },
    positionCar: function (car) {
        mapElementModel.Id = car.CarId;
        mapElementModel.Type = "CarModel";
        mapElementModel.Name = car.CarNumber;
        mapElementModel.Circum = "UserModel,CarModel,TaskModel";
        mapElementModel.X = car.X2000;
        mapElementModel.Y = car.Y2000;
        mapElementModel.IsOnline = car.IsOnline;
        mapElementModel.IsAlarm = car.IsOverArea;
        map.position(mapElementModel);
    },
    positionAllCar: function (cars) {
        var listCar = [];
        for (var i = 0; i < cars.length; i++) {
            if (cars[i].X2000 == null || cars[i].Y2000 == null) {
                continue;
            }
            listCar.push({ Id: cars[i].CarId, Type: "CarModel", Name: cars[i].CarNumber, Circum: "UserModel,CarModel,TaskModel", X: cars[i].X2000, Y: cars[i].Y2000, IsOnline: cars[i].IsOnline, IsAlarm: cars[i].IsOverArea });
        }
        map.positions(listCar);
    },
    traceReplay: function (car) {
        mapElementModel.Id = car.CarId;
        mapElementModel.Type = "CarModel";
        mapElementModel.Name = car.CarNumber;
        mapElementModel.Circum = "UserModel,CarModel,TaskModel";
        mapElementModel.X = car.X2000;
        mapElementModel.Y = car.Y2000;
        mapElementModel.IsOnline = car.IsOnline;
        mapElementModel.IsAlarm = car.IsOverArea;
        map.traceReplay(car);
    },
    getSearchArea: function (car) {
        var DrawModel = [];
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/Area/GetAllAreas",
            data: { typeId: 2 },
            dataType: "json",
            success: function (data) {
                var allSearchArea = data;
                if (allSearchArea.length == 0) {
                    return;
                }
                $.ajax({
                    type: "GET",
                    async: true,
                    url: globalConfig.apiconfig + "/api/Area/GetCarAreas",
                    data: { carId: car.CarId },
                    dataType: "json",
                    success: function (dataSelf) {
                        var selfSearchArea = dataSelf;
                        for (var i = 0; i < allSearchArea.length; i++) {
                            var isSelf = 0;
                            for (var j = 0; j < selfSearchArea.length; j++) {
                                if (allSearchArea[i].AreaId == selfSearchArea[j].AreaId) {
                                    isSelf = 1;
                                    break;
                                }
                            }
                            if (isSelf == 1) {
                                DrawModel.push({
                                    ID: allSearchArea[i].AreaId,
                                    Type: "Polygon",
                                    Points: allSearchArea[i].Geometry,
                                    Style: 2,
                                    UserName: allSearchArea[i].AreaName
                                });
                            } else {
                                DrawModel.push({
                                    ID: allSearchArea[i].AreaId,
                                    Type: "Polygon",
                                    Points: allSearchArea[i].Geometry,
                                    Style: 1,
                                    UserName: allSearchArea[i].AreaName
                                });
                            }
                        }
                        map.getSearchArea(DrawModel);
                    },
                    error: function (errorMsg1) {
                        console.log(errorMsg1);
                    }
                });
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    foundCircum: function (car) {
        mapElementModel.Id = car.CarId;
        mapElementModel.Type = "CarModel";
        mapElementModel.Name = car.CarNumber;
        mapElementModel.Circum = "UserModel,CarModel,TaskModel";
        mapElementModel.X = car.X2000;
        mapElementModel.Y = car.Y2000;
        mapElementModel.IsOnline = car.IsOnline;
        mapElementModel.IsAlarm = car.IsOverArea;
        map.foundCircum(mapElementModel);
    },
    mapElementClicked: function (carId) {
        detailMin.close();
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/Car/GetCarByCarId",
            data: { carId: carId },
            dataType: "json",
            success: function (data) {
                car.Car = data;
                car.initDetailMin(data);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
}
//案件
var event = {
    SourceId: null,
    Event: null,
    initEventList: function (sourceId) {
        this.SourceId = sourceId == null ? "" : sourceId;
        list.show("Views/Events/EventsList.aspx");
    },
    initEventDetailMin: function (event) {
        this.Event = event;
        detailMin.show("Views/Events/EventsDetailMin.aspx");
        this.initDetail(event);
    },
    initDetail: function (event) {
        this.Event = event;
        detail.close();
        detail.show("Views/Events/EventsDetail.aspx");
    },
    positionEvent: function (event) {
        mapElementModel.Id = event.ZFSJId;
        mapElementModel.Type = "TaskModel";
        mapElementModel.Name = event.EventTitle;
        mapElementModel.Circum = "UserModel,CarModel,TaskModel";
        mapElementModel.X = event.X2000;
        mapElementModel.Y = event.Y2000;
        mapElementModel.IsOnline = 1;
        mapElementModel.IsAlarm = event.LevelNum == 1 ? 0 : 1;
        map.position(mapElementModel);
    },
    positionAllEvent: function (events) {
        var listEvent = [];
        for (var i = 0; i < events.length; i++) {
            if (events[i].X2000 == null || events[i].Y2000 == null) {
                continue;
            }
            listEvent.push({ Id: events[i].ZFSJId, Type: "TaskModel", Name: events[i].EventTitle, Circum: "UserModel,CarModel,TaskModel", X: events[i].X2000, Y: events[i].Y2000, IsOnline: 1, IsAlarm: event.LevelNum == 1 ? 0 : 1 });
        }
        map.positions(listEvent);
    },
    foundCircum: function (event) {
        mapElementModel.Id = event.ZFSJId;
        mapElementModel.Type = "TaskModel";
        mapElementModel.Name = event.EventTitle;
        mapElementModel.Circum = "UserModel,CarModel,TaskModel";
        mapElementModel.X = event.X2000;
        mapElementModel.Y = event.Y2000;
        map.foundCircum(mapElementModel);
    },
    mapElementClicked: function (eventId) {
        detailMin.close();
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/Task/GetTaskByTaskId",
            data: { taskId: eventId },
            dataType: "json",
            success: function (data) {
                event.Event = data;
                event.initEventDetailMin(data);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
}

var parts = {
    Type: null,
    Part: null,
    RountURL: "",
    RountData: {
        parkGreenId: "",
        loadGreenId: "",
        protectionGreenId: "",
        toiltId: "",
        riverId: "",
    },
    RountModel: {
        id: "",
        name: "",
        X: null,
        Y: null,
    },
    initPartsList: function (type) {
        this.Type = type;
        list.show("Views/Parts/PartsList.aspx");
    },
    intiDetailMin: function (part) {
        this.Part = part;
        detailMin.show("Views/Parts/PartsDetailMin.aspx");
        this.initDetail(part);
    },
    initDetail: function (part) {
        this.Part = part;
        detail.close();
        detail.show("Views/Parts/PartsDetail.aspx");
    },
    positionPart: function (road) {
        mapElementModel.Id = road.id;
        mapElementModel.Type = "PartsModel";
        mapElementModel.PartsType = this.Type;
        mapElementModel.Name = road.name;
        mapElementModel.X = road.X;
        mapElementModel.Y = road.Y;
        mapElementModel.IsOnline = 1;
        mapElementModel.IsAlarm = 0;
        map.position(mapElementModel);
    },
    positionCoverLoad: function (road) {
        mapElementModel.Id = road.CoverLoadId;
        mapElementModel.Type = "PartsModel";
        mapElementModel.PartsType = "CoverLoad";
        mapElementModel.Name = road.CoverLoadName;
        mapElementModel.X = road.X;
        mapElementModel.Y = road.Y;
        mapElementModel.IsOnline = 1;
        mapElementModel.IsAlarm = 0;
        map.position(mapElementModel);
    },
    positionPump: function (road) {
        mapElementModel.Id = road.PumpId;
        mapElementModel.Type = "PartsModel";
        mapElementModel.PartsType = "Pump";
        mapElementModel.Name = road.PumpName;
        mapElementModel.X = road.X;
        mapElementModel.Y = road.Y;
        mapElementModel.IsOnline = 1;
        mapElementModel.IsAlarm = 0;
        map.position(mapElementModel);
    },
    positionLandscapeLamp: function (road) {
        mapElementModel.Id = road.LLId;
        mapElementModel.Type = "PartsModel";
        mapElementModel.PartsType = "LandscapeLamp";
        mapElementModel.Name = road.LLName;
        mapElementModel.X = road.X;
        mapElementModel.Y = road.Y;
        mapElementModel.IsOnline = 1;
        mapElementModel.IsAlarm = 0;
        map.position(mapElementModel);
    },
    positionRoad: function (road) {
        mapElementModel.Id = road.RoadId;
        mapElementModel.Type = "PartsModel";
        mapElementModel.PartsType = "Road";
        mapElementModel.Name = road.RoadName;
        mapElementModel.X = road.X;
        mapElementModel.Y = road.Y;
        mapElementModel.IsOnline = 1;
        mapElementModel.IsAlarm = 0;
        map.position(mapElementModel);
    },
    positionBridge: function (part) {
        mapElementModel.Id = part.BridgeId;
        mapElementModel.Type = "PartsModel";
        mapElementModel.PartsType = "Bridge";
        mapElementModel.Name = part.BridgeName;
        mapElementModel.X = part.X;
        mapElementModel.Y = part.Y;
        mapElementModel.IsOnline = 1;
        mapElementModel.IsAlarm = 0;
        map.position(mapElementModel);
    },
    positionStreetLamp: function (part) {
        mapElementModel.Id = part.SLLId;
        mapElementModel.Type = "PartsModel";
        mapElementModel.PartsType = "StreetLamp";
        mapElementModel.Name = part.SLLName;
        mapElementModel.X = part.X;
        mapElementModel.Y = part.Y;
        mapElementModel.IsOnline = 1;
        mapElementModel.IsAlarm = 0;
        map.position(mapElementModel);
    },
    mapElementClicked: function (mem) {
        detailMin.close();
        switch (mem.PartsType) {
            case "Road":
                this.roadClicked(mem.Id);
                break;
            case "Bridge":
                this.BridgeClicked(mem.Id);
                break;
            case "StreetLamp":
                this.StreetLampClicked(mem.Id);
                break;
            case "LandscapeLamp":
                this.LandscapeLampClicked(mem.Id);
                break;
            case "Pump":
                this.PumpClicked(mem.Id);
                break;
            case "CoverLoad":
                this.CoverLoadClicked(mem.Id);
                break;
            case "ParkGreen":
                this.RountURL = "/api/Parts/GetParkGreenByParkGreenId";
                this.RountData.parkGreenId = mem.Id;
                this.PartClicked(); break;
            case "LoadGreen":
                this.RountURL = "/api/Parts/GetLoadGreenByLoadGreenId";
                this.RountData.loadGreenId = mem.Id;
                this.PartClicked(); break;
            case "ProtectionGreen":
                this.RountURL = "/api/Parts/GetProtectionGreen";
                this.RountData.protectionGreenId = mem.Id;
                this.PartClicked(); break;
            case "Toilt":
                this.RountURL = "/api/Parts/GetToilt";
                this.RountData.toiltId = mem.Id;
                this.PartClicked(); break;
            case "River":
                this.RountURL = "/api/Parts/GetRiver";
                this.RountData.riverId = mem.Id;
                this.PartClicked(); break;

        }
    },
    PartClicked: function (id) {
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + this.RountURL,
            data: this.RountData,
            dataType: "json",
            success: function (data) {
                parts.changeData(data);
                parts.Part = parts.RountModel;
                parts.intiDetailMin(parts.RountModel);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    changeData: function (data) {
        switch (this.Type) {
            case "ParkGreen":
                this.RountModel.id = data.ParkGreenId;
                this.RountModel.name = data.PartGreenName;
                this.RountModel.X = data.X;
                this.RountModel.Y = data.Y;
                break;
            case "LoadGreen":
                this.RountModel.id = data.LoadGreenId;
                this.RountModel.name = data.LoadGreenName;
                this.RountModel.X = data.X;
                this.RountModel.Y = data.Y;

                break;
            case "ProtectionGreen":
                this.RountModel.id = data.ProtectionGreenId;
                this.RountModel.name = data.ProtectGreenName;
                this.RountModel.X = data.X;
                this.RountModel.Y = data.Y;

                break;
            case "Toilt":
                this.RountModel.id = data.ToiltId;
                this.RountModel.name = data.ToiltName;
                this.RountModel.X = data.X;
                this.RountModel.Y = data.Y;

                break;
            case "River":
                this.RountModel.id = data.RiverId;
                this.RountModel.name = data.RiverName;
                this.RountModel.X = data.X;
                this.RountModel.Y = data.Y;
                break;
        }
    },
    CoverLoadClicked: function (id) {
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/Parts/GetCoverLoadByCoverLoadId",
            data: { coverLoadId: id },
            dataType: "json",
            success: function (data) {
                parts.Part = data;
                parts.intiDetailMin(data);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    PumpClicked: function (id) {
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/Parts/GetPumpByPumpId",
            data: { pumpId: id },
            dataType: "json",
            success: function (data) {
                parts.Part = data;
                parts.intiDetailMin(data);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    StreetLampClicked: function (id) {
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/Parts/GetStreetLampLoad",
            data: { SLLId: id },
            dataType: "json",
            success: function (data) {
                parts.Part = data;
                parts.intiDetailMin(data);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    LandscapeLampClicked: function (id) {
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/Parts/GetLandscapeLampByLLId",
            data: { LLId: id },
            dataType: "json",
            success: function (data) {
                parts.Part = data;
                parts.intiDetailMin(data);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    roadClicked: function (id) {
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/Parts/GetRoadByRoadId",
            data: { roadId: id },
            dataType: "json",
            success: function (data) {
                parts.Part = data;
                parts.intiDetailMin(data);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    BridgeClicked: function (id) {
        $.ajax({
            type: "GET",
            async: true,
            url: globalConfig.apiconfig + "/api/Parts/GetBridgeByBridgeId",
            data: { bridgeId: id },
            dataType: "json",
            success: function (data) {
                parts.Part = data;
                parts.intiDetailMin(data);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    selectModel: function (type, id) {
        switch (type) {
            case "道路":
                $.ajax({
                    type: "GET",
                    async: true,
                    url: globalConfig.apiconfig + "/api/Parts/GetRoadAndRoadPartsBySWMXId",
                    data: { SWMXId: id },
                    dataType: "json",
                    success: function (data) {
                        var road = data[0];
                        var roadways = data[1];
                        var x = data[0].X;
                        var y = data[0].Y;
                        mapSW.curPoint.x = x;
                        mapSW.curPoint.y = y;
                        parts.Type = "Road";
                        parts.intiDetailMin(road);
                    },
                    error: function (errorMsg) {
                        console.log(errorMsg);
                    }
                });
                break;
            default:
        }
    },
}

var constr = {
    positionConstr: function () {
        mapElementModel.Id = 1;
        mapElementModel.Name = "测试工程";
        mapElementModel.Type = "ConstructionModel";
        mapElementModel.Areas = "361214.176911319,3302337.10827024;361207.107108668,3302199.24711854;361207.107108668,3302114.40948672;361369.712569654,3301962.40872971;361677.248984997,3301672.546821;361861.063853937,3301506.40645869;361892.877965869,3301492.26685338;362044.878722878,3301937.66442043;362299.391618334,3302347.71297422;362140.321058674,3302450.22511267;361945.901485756,3302581.01646172;361786.830926096,3302322.96866494;361454.550201472,3302330.03846759;361440.410596169,3302337.10827024;361207.107108668,3302333.57336892|362299.391618334,3302351.24787555;362730.64958008,3302057.85106551;362571.579020419,3301559.42997857;362522.090401859,3301382.68491228;362444.322572691,3301142.31162213;362363.019842198,3300937.28734524;362274.647309054,3300976.17125982;362126.181453371,3301046.86928633;361793.900728747,3301205.93984599;361889.343064544,3301495.80175471;361974.180696362,3301732.64014354;362044.878722878,3301937.66442043;362225.158690493,3302213.38672384;362306.461420986,3302351.24787555";
        map.position(mapElementModel);
    },
}
//二三维操作 


var map = {
    init: function () {
        if (globalConfig.mapFlag == 2) {
            map.mapChange();
        };

        mapSW.zoomToPoint({ X: 358994.199503869, Y: 3302067.63111812 });
    },
    changeMapToEW: function () {
        if (globalConfig.mapFlag == 3) {
            this.mapChange();
        }
    },
    mapChange: function () {
        if (globalConfig.mapFlag == 2) {
            globalConfig.mapFlag = 3;

            //获取二维位置
            var str = main.slContent.Content.MainPage.GetMapExtent();
            var strs = str.split(',');
            var x1 = strs[0];
            var y1 = strs[1];
            var x2 = strs[2];
            var y2 = strs[3];

            //设置三维位置
            map.changeSW(x1, y1, x2, y2);
        } else {
            globalConfig.mapFlag = 2;
            //获取三维坐标
            var cam = __g.camera.getCamera();
            var pos = cam.position;
            var x1 = pos.x;
            var x2 = pos.x;
            var y1 = pos.y;
            var y2 = pos.y;
            //设置二维位置
            map.changeEW(x1, y1, x2, y2);
            //ContainerManager.mapcontrol2d.MapExtent(double.Parse(strs[0]) - 1198, double.Parse(strs[1]) - 354, double.Parse(strs[2]) + 1198, double.Parse(strs[3]) + 354);
        }
    },
    changeEW: function (x1, y1, x2, y2) {
        x1 = x1 - 1198;
        y1 = y1 - 354;
        x2 = x2 + 1198;
        y2 = y2 + 354;
        var str = main.slContent.Content.MainPage.SetMapExtent(x1, y1, x2, y2);
        $("#swDiv").css("visibility", "hidden");
        $("#ewDiv").css("visibility", "visible");

        $("#ewDiv").css("height", "100%");
        $("#ewDiv").css("width", "100%");
        $("#swDiv").css("height", "100%");
        $("#swDiv").css("width", "100%");
    },
    changeSW: function (x1, y1, x2, y2) {
        var x = parseFloat(x1) + parseFloat(x2);
        var y = parseFloat(y1) + parseFloat(y2);

        var scale = __g.new_Vector3;
        scale.set(x / 2, y / 2, 0);
        var ang = __g.new_EulerAngle;
        ang.heading = 0;
        ang.tilt = -90;
        __g.camera.flyTime = 0;
        __g.camera.lookAt(scale, 200, ang);

        $("#ewDiv").css("visibility", "hidden");
        $("#swDiv").css("visibility", "visible");

        $("#swDiv").css("height", "100%");
        $("#swDiv").css("width", "100%");
        $("#ewDiv").css("height", "100%");
        $("#ewDiv").css("width", "100%");
    },
    position: function (mem) {
        if ((mem.X == null || mem.Y == null || mem.X == 0 || mem.Y == 0) && (mem.Areas == null || mem.Areas == "")) {
            //alert("无定位信息");
            return;
        };
        map.clears();
        if (globalConfig.mapFlag == 2 && mem.Type != "BMDAreaModel") {
            this.mapChange();
        }
        mapEW.position(mem);
        mapSW.position(mem);
    },
    updatePosition: function (mem) {
        if ((mem.X == null || mem.Y == null || mem.X == 0 || mem.Y == 0) && (mem.Areas == null || mem.Areas == "")) {
            //alert("无定位信息");
            return;
        };
        //map.clears();      
        mapEW.position(mem);
        mapSW.position(mem);
    },
    getSearchArea: function (mems) {
        if (globalConfig.mapFlag == 3) {
            this.mapChange();
        }
        mapEW.getSearchArea(mems);
    },
    positions: function (mems) {
        mapEW.positions(mems);
        if (globalConfig.mapFlag == 3) {
            this.mapChange();
        }
        //mapSW.positions(mems);
    },
    foundCircum: function (mem) {
        if (globalConfig.mapFlag == 3) {
            this.mapChange();
        }
        mapEW.foundCircum(mem);
    },
    foundCircumCamera: function (mem) {
        if (globalConfig.mapFlag == 3) {
            this.mapChange();
        }
        mapEW.foundCircumCamera(mem);
    },
    traceReplay: function (mem) {
        if (globalConfig.mapFlag == 3) {
            this.mapChange();
        }
        $("#menuDiv").css("visibility", "hidden");
        $("#listDiv").css("visibility", "hidden");
        $("#detailDiv").css("visibility", "hidden");
        $("#detail-minDiv").css("visibility", "hidden");
        //mapEW.traceReplay(mem);
        mapEW.traceReplay(mem);
    },
    traceReplay_CameraCentre: function (mem) {
        if (globalConfig.mapFlag == 3) {
            this.mapChange();
        }
        $("#menuDiv").css("visibility", "hidden");
        $("#listDiv").css("visibility", "hidden");
        $("#detailDiv").css("visibility", "hidden");
        $("#detail-minDiv").css("visibility", "hidden");
        mapEW.traceReplay_CameraCentre(mem);
    },
    traceReplayClosed: function () {
        $("#menuDiv").css("visibility", "visible");
        $("#detail-minDiv").css("visibility", "visible");
        if ($("#ifmList").attr("src") != "") {
            $("#listDiv").css("visibility", "visible");
        }
        if ($("#ifmDetail").attr("src") != "") {
            $("#detailDiv").css("visibility", "visible");
        }
    },
    memClicked: function (mem) {
        switch (mem.Type) {
            case "UserModel":
                person.mapElementClicked(mem.Id);
                break;
            case "CarModel":
                car.mapElementClicked(mem.Id);
                break;
            case "TaskModel":
                event.mapElementClicked(mem.Id);
                break;
            case "PartsModel":
                parts.mapElementClicked(mem);
                break;
            case "CameraModel":
                Camera.mapElementClicked(mem);
                break;
            case "CameraDrawLine":
                Camera.mapElementClickedByTHC(mem);
                break;
            default:
        }
    },
    tools: function (type) {
        if (globalConfig.mapFlag == 2) {
            mapEW.tools(type);
        } else {
            mapSW.tools(type);
        }
    },
    recSearch: function () {
        mapEW.recSearch();
    },
    clears: function () {
        map.clearLocate();
        map.clearCircum();
        mapSW.clears();
    },
    clearLocate: function () {
        mapEW.clearLocate();
    },
    clearCircum: function () {
        mapEW.clearCircum();
    },
    selectModel: function () {
        if (globalConfig.mapFlag == 2) {
            this.mapChange();
        }
        mapSW.selectModel();
    },
}

var mapEW = {
    position: function (mem) {
        var jsonstr = JSON.stringify(mem);
        main.slContent.Content.MainPage.DrawElement(jsonstr);
    },
    positions: function (mems) {
        var jsonstr = JSON.stringify(mems);
        main.slContent.Content.MainPage.DrawElements(jsonstr);
    },
    foundCircum: function (mem) {
        var jsonstr = JSON.stringify(mem);
        main.slContent.Content.MainPage.RoundResource(jsonstr);
    },
    foundCircumCamera: function (mem) {
        var jsonstr = JSON.stringify(mem);
        main.slContent.Content.MainPage.RoundCamera(jsonstr);
    },
    traceReplay: function (mem) {
        var jsonstr = JSON.stringify(mem);
        main.slContent.Content.MainPage.PersonHistory(jsonstr);
    },
    traceReplay_CameraCentre: function (mem) {
        var jsonstr = JSON.stringify(mem);
        main.slContent.Content.MainPage.PersonCameraCentreHistory(jsonstr);
    },
    memClicked: function (mem) {
        map.memClicked(mem);
    },
    tools: function (type) {
        main.slContent.Content.MainPage.Draw(type);
    },
    recSearch: function () {
        main.slContent.Content.MainPage.SelectArea_rec();
    },
    clearLocate: function () {
        main.slContent.Content.MainPage.ClearLocateElement();
    },
    clearCircum: function () {
        main.slContent.Content.MainPage.ClearRoundElement();
    },
    GraphicID: null,
    showToolsDetailMin: function (id) {
        this.GraphicID = id;
        detailMin.show("Views/Tools/ToolsDetailMin.aspx");
    },
    deleteGraphic: function (id) {
        main.slContent.Content.MainPage.DeleteGraphic(id);
    },
    getSearchArea: function (obj) {
        main.slContent.Content.MainPage.DrawSearchArea(JSON.stringify(obj));
    }
}

var mapSW = {
    curPoint: { x: 0, y: 0, z: 0 },
    Map3Objs: {},
    position: function (mem) {
        mapSW.clears();
        if ((mem.X == null || mem.Y == null || mem.X == 0 || mem.Y == 0) && mem.Area != "") {
            var strP = mem.Areas.split(';')[0];
            mem.X = strP.split(',')[0];
            mem.Y = strP.split(',')[1];
        }
        mapSW.zoomToPoint(mem);
        mapSW.addMap3Label(mem);
        //mapSW.addMap3Image(mem);
        //mapSW.deepView();
    },
    positions: function (mems) {
        mapSW.clears();
        mapSW.zoomToPoint(mems[0]);
        for (var i = 0; i < mems.length; i++) {
            mapSW.addMap3Label(mems[i]);
        }
    },
    zoomToPoint: function (mem) {
        var x = mem.X;
        var y = mem.Y;
        var z = 5;
        mapSW.curPoint = { x: x, y: y, z: z };

        var ang = __g.new_EulerAngle;
        ang.heading = 0;
        ang.tilt = -56;

        var v3 = __g.new_Vector3;
        v3.set(x, y, z);
        __g.camera.flyTime = 0;
        __g.camera.lookAt(v3, 120, ang);
    },
    //视角拉近
    deepView: function () {
        var x = mapSW.curPoint.x;
        var y = mapSW.curPoint.y;
        var z = mapSW.curPoint.z;
        if (x == 0 || y == 0)
            return;
        var ang = __g.new_EulerAngle;
        ang.heading = -56;
        ang.tilt = -34;

        var v3 = __g.new_Vector3;
        v3.set(x, y, z);
        __g.camera.flyTime = 5;
        __g.camera.lookAt(v3, 50, ang);
    },
    //添加三维标签
    addMap3Label: function (mem) {
        var x = mem.X;
        var y = mem.Y;
        var z = 5;
        var type = mem.Type;
        var id = mem.Id;
        var context = mem.Name;
        var titleText;
        switch (type) {
            case "TaskModel":
                titleText = "事件";
                break;
            case "UserModel":
                titleText = "执法人员";
                break;
            case "CarModel":
                titleText = "执法车辆";
                break;
            case "PartsModel":
                titleText = "部件";
                break;
            case "CameraModel":
                titleText = "监控";
                break;
            default:
                titleText = "";
                break;
        }


        if ((x == null || x == 0) || (y == null || y == 0))
            return;
        var key = type + "," + id;

        if (mapSW.Map3Objs[key] != null) {
            //console.log("已存在。");
            return;
        }

        // 创建一个有3行2列的TableLabel
        var tableLabel = __g.objectManager.createTableLabel(1, 1);
        //var tableLabel = __g.objectManager.createTableLabel(1, 1, "");
        tableLabel.clientData = key;
        //加入到临时Map
        mapSW.Map3Objs[key] = tableLabel;

        var title = titleText;

        // 设定表头文字
        tableLabel.titleText = title;
        // 设定表格中第1行，第1列的显示文字
        tableLabel.setRecord(0, 0, context);
        //// 第1行，第2列
        //tableLabel.setRecord(0, 1, "2");

        //标牌的位置
        var position = __g.new_Vector3;
        position.set(x, y, z);
        var point = null;
        point = __g.geometryFactory.createPoint(gviVertexAttribute.gviVertexAttributeZ);
        point.position = position;
        tableLabel.position = point;

        // 列宽度
        tableLabel.setColumnWidth(0, 0);
        //tableLabel.setRowHeight(0, 0);
        //tableLabel.setColumnWidth(1, 80);

        // 表的边框颜色
        //tableLabel.borderColor = 0xffffffff;
        // 表的边框的宽度
        //tableLabel.borderWidth = 2;
        // 表的背景色 #  
        tableLabel.tableBackgroundColor = 0xff397DD7;

        // 标题背景色
        tableLabel.titleBackgroundColor = 0xff79A9D9;

        // 第一列文本样式
        var headerTextAttribute = __g.new_TextAttribute;
        headerTextAttribute.textColor = 0xffffffff;
        headerTextAttribute.outlineColor = 0xff000000;
        headerTextAttribute.font = "黑体";
        headerTextAttribute.bold = true;
        headerTextAttribute.multilineJustification = gviMultilineJustification.gviMultilineLeft;
        tableLabel.setColumnTextAttribute(0, headerTextAttribute);

        // 标题文本样式
        var capitalTextAttribute = __g.new_TextAttribute;
        capitalTextAttribute.textColor = 0xffffffff;
        capitalTextAttribute.outlineColor = 4279834905;
        //capitalTextAttribute.font = "华文新魏";
        capitalTextAttribute.font = "黑体";
        capitalTextAttribute.textSize = 14;
        capitalTextAttribute.multilineJustification = gviMultilineJustification.gviMultilineCenter;
        capitalTextAttribute.bold = true;
        tableLabel.titleTextAttribute = capitalTextAttribute;

        //var angle = __g.new_EulerAngle;
        //angle.heading = 0;
        //angle.titl = -90;
        //__g.camera.lookAt(position, 50, angle);
    },
    addMap3Image: function (mem) {
        var x = mem.X;
        var y = mem.Y;
        var z = 5;
        var type = mem.Type;
        var id = mem.Id;

        if ((x == null || x == 0) || (y == null || y == 0))
            return;
        var key = type + "PosImage" + "," + id;
        if (mapSW.Map3Objs[key] != null) {
            //console.log("已存在。");
            return;
        }

        var geoSymbol = __g.new_ImagePointSymbol;   //将点以图片的形式显示出来
        var imguri = "http:\/\/" + location.host + "\/Images\/3Dperson.png";
        geoSymbol.imageName = imguri;  //使用ImageClass里存在的图片
        geoSymbol.size = 25;

        var gfactory = __g.geometryFactory;
        var fde_point = gfactory.createGeometry(gviGeometryType.gviGeometryPoint,
                        gviVertexAttribute.gviVertexAttributeZ);
        fde_point.setCoords(x, y, z, 0, 0);

        var rpoint = __g.objectManager.createRenderPoint(fde_point, geoSymbol);
        //var rpoint = __g.objectManager.createRenderPoint(fde_point, geoSymbol, "");
        //绑定对象
        rpoint.clientData = key;
        //加入到临时Map
        mapSW.Map3Objs[key] = rpoint;
    },
    selectModel: function () {
        __g.interactMode = gviInteractMode.gviInteractSelect;
        __g.mouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectAll;
        __g.mouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;

        __g.onmouseclickselect = mapSW.fn_MouseClickSelect;
    },
    fn_MouseClickSelect: function (pickResult, intersectPoint, mask, eventSender) {
        if (pickResult == null)
            return;
        __g.featureManager.unhighlightAll();
        for (var obj in mapSW.Map3Objs) {
            mapSW.Map3Objs[obj].unhighlight();
        }

        if (pickResult != null) {
            if (pickResult.type == gviObjectType.gviObjectFeatureLayer) {
                var fid = pickResult.featureId;
                var fl = pickResult.featureLayer;

                for (var fcGuid in __fcMap) {
                    var flag = false;
                    var fc = __fcMap[fcGuid];

                    if (fcGuid == fl.featureClassId) {
                        __g.featureManager.highlightFeature(fc, fid, 0xffff0000);
                        //console.log("fid:" + fid);
                        //建筑的时候，消防展示
                        //if (fc.name == "建筑" && fid == 5712) {
                        //    xfbuilding();
                        //    __g.interactMode = gviInteractMode.gviInteractNormal;
                        //}

                        for (var i = 0; i < visFCMap.length; i++) {
                            if (visFCMap[i] === fc.name) {
                                flag = true;
                                var filter = __g.new_QueryFilter;
                                filter.whereClause = 'oid =' + fid;

                                var cursor = fc.search(filter, true);
                                if (cursor != null) {
                                    var fdeRow = null;
                                    while ((fdeRow = cursor.nextRow()) != null) {
                                        var swmx = "workid";
                                        var nPos = fdeRow.fieldIndex(swmx);

                                        if (nPos != -1) {
                                            var workid = fdeRow.getValue(nPos);
                                            ///通过fc.name（图层）和WordID查询部件
                                            //console.log(fc.name);
                                            //console.log("workid:" + workid);
                                            parts.selectModel(fc.name, workid);
                                        } else {

                                        }
                                        __g.interactMode = gviInteractMode.gviInteractNormal;
                                    }
                                }
                                break;
                            }
                        }

                        if (flag) {
                            break;
                        }
                    }
                    //console.log(fc.name);
                }
            }
            //var x = intersectPoint.x;
            //var y = intersectPoint.y;
            //var z = 5;
            //mapSW.curPoint = { x: x, y: y, z: z };

            //var ang = __g.new_EulerAngle;
            //ang.heading = 0;
            //ang.tilt = -56;

            //var v3 = __g.new_Vector3;
            //v3.set(x, y, z);
            //__g.camera.flyTime = 2;
            //__g.camera.lookAt(v3, 120, ang);
            var x = intersectPoint.x;
            var y = intersectPoint.y;
            var z = 5;
            if (x == 0 || y == 0)
                return;
            var ang = __g.new_EulerAngle;
            ang.heading = -56;
            ang.tilt = -34;

            var v3 = __g.new_Vector3;
            v3.set(x, y, z);
            __g.camera.flyTime = 5;
            __g.camera.lookAt(v3, 50, ang);
        }
    },
    tools: function (type) {
        switch (type) {
            case "PolylineWithLen":
                this.MeasureAerialDistance();
                break;
            case "PolygonWithArea":
                this.MeasureArea();
                break;
        }
    },
    MeasureAerialDistance: function () {
        //任意测距
        __g.interactMode = gviInteractMode.gviInteractMeasurement;
        __g.measurementMode = gviMeasurementMode.gviMeasureAerialDistance;
    },
    MeasureArea: function () {
        //面积
        __g.interactMode = gviInteractMode.gviInteractMeasurement;
        __g.measurementMode = gviMeasurementMode.gviMeasureArea;
    },
    clears: function () {
        __g.featureManager.unhighlightAll();
        __g.interactMode = gviInteractMode.gviInteractNormal;

        for (var obj in mapSW.Map3Objs) {
            __g.objectManager.deleteObject(mapSW.Map3Objs[obj].guid);
        }
        mapSW.Map3Objs = {};
    },
}

function memClicked(mem) {
    var mapm = eval("(" + mem + ")");
    mapEW.memClicked(mapm);
}


function taskDetailClicked(taskInfo) {
    var mapm = eval("(" + taskInfo + ")");
    person.eventSBDetail(mapm.Id);
}

function traceReplayClosed() {
    map.traceReplayClosed();
}

function ToolsDetailMin(id) {
    mapEW.showToolsDetailMin(id);
}

//监控Maker点击播放视频
function CameraBMDClicked(obj) {
    var model = eval("(" + obj + ")");;
    Camera.CameraInfo = model.Content;

    //Camera.initDetailBMD();

    Camera.initCameraDeatilMin2(Camera.CameraInfo);
}
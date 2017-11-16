﻿window.onload = function loadWJHMap() {
    //初始化三维控件
    initAxControl();

    //var con = __g.new_ConnectionInfo;
    //con.connectionType = 101;
    //con.database = "cixi0330";
    //con.server = "192.168.20.32";
    //con.port = 8040;
    ////加载服务数据
    //loadServer(con);

    loadFdb("", "", "");

    //设置选择对象类型
    __g.mouseSelectObjectMask = gviMouseSelectObjectMask.gviSelectFeatureLayer;

    //绑定拾取事件
    __g.onmouseclickselect = fnMouseClickSelect;
    __g.onmousedragselect = fnMouseDragSelect;

    var x = 328672.20;
    var y = 3358725.40;
    var scale = __g.new_Vector3;

    scale.set(x, y, 0);
    var ang = __g.new_EulerAngle;
    ang.heading = 10;
    ang.tilt = -45;
    __g.camera.flyTime = 0;
    __g.camera.lookAt(scale, 70, ang);
}
var tableLabel = null;
var position = null;
var point = null;
var fcGuid = {};
//模型选取
function Select() {
    __g.interactMode = gviInteractMode.gviInteractSelect;
    __g.mouseSelectMode = gviMouseSelectMode.gviMouseSelectClick;
}

function fnMouseClickSelect(pickResult, intersectPoint, mask, eventSender) {
    //先清除所有选中
    __g.featureManager.unhighlightAll();
    if (pickResult == null) {
        if (tableLabel != null)
            __g.objectManager.deleteObject(tableLabel.guid);
        return;
    }
    var featureId = pickResult.featureId;
    var featureClass = null;
    var fl = pickResult.featureLayer;
    var fc = __fcMap[fl.featureClassId];
    __g.featureManager.highlightFeature(fc, featureId, 0xffff0000);
    if (fc) {
        var filter = __g.new_QueryFilter;
        filter.whereClause = "oid=" + featureId;
        var nCount = fc.getCount(filter);
        if (nCount == 0)
            return;
        var cursor = fc.search(filter, true);
        if (cursor != null) {
            var fdeRow = null;
            if (tableLabel) {
                __g.objectManager.deleteObject(tableLabel.guid);
                tableLabel = null;
            }
            var fInfoColl = fc.getFields();
            tableLabel = __g.objectManager.createTableLabel(4, 2);
            tableLabel.visibleMask = gviViewportMask.gviViewNone;
            while ((fdeRow = cursor.nextRow()) != null) {
                //for (var i = 0; i < fInfoColl.count; ++i) {
                //    var fieleInfo = fInfoColl.get(i);
                //    var strColName = fieleInfo.name;
                //    var nPos = fdeRow.fieldIndex(strColName);
                //    if (fieleInfo.fieldType == gviFieldType.gviFieldGeometry) {
                //        tableLabel.setRecord(i, 0, strColName);
                //        tableLabel.setRecord(i, 1, "Geometry");
                //        continue;
                //    } if (fieleInfo.fieldType == gviFieldType.gviFieldBlob) {
                //        tableLabel.setRecord(i, 0, strColName);
                //        tableLabel.setRecord(i, 1, "Blob");
                //        continue;
                //    }
                //    if (nPos == -1 || fdeRow.isNull(nPos))
                //        continue;
                //    var v = fdeRow.getValue(nPos);  // 从库中读取值
                //    tableLabel.setRecord(i, 0, strColName);
                //    tableLabel.setRecord(i, 1, v);
                //}
                var fieleInfo = fInfoColl.get(1);
                var strColName = fieleInfo.name;
                var nPos = fdeRow.fieldIndex(strColName);
                var number = fdeRow.getValue(nPos);  // 从库中读取值
                tableLabel.setRecord(0, 0, "编号");
                tableLabel.setRecord(0, 1, number);

                var fieleInfo = fInfoColl.get(2);
                var strColName = fieleInfo.name;
                var nPos = fdeRow.fieldIndex(strColName);
                var name = fdeRow.getValue(nPos);  // 从库中读取值
                tableLabel.setRecord(1, 0, "名称");
                tableLabel.setRecord(1, 1, name);

                var fieleInfo = fInfoColl.get(4);
                var strColName = fieleInfo.name;
                var nPos = fdeRow.fieldIndex(strColName);
                var unitName = fdeRow.getValue(nPos);  // 从库中读取值
                tableLabel.setRecord(2, 0, "所属单位");
                tableLabel.setRecord(2, 1, unitName);

                var fieleInfo = fInfoColl.get(15);
                var strColName = fieleInfo.name;
                var nPos = fdeRow.fieldIndex(strColName);
                var textureName = fdeRow.getValue(nPos);  // 从库中读取值
                tableLabel.setRecord(3, 0, "材质");
                tableLabel.setRecord(3, 1, textureName);
            }
            // 设定表位置
            tableLabel.position = intersectPoint;

            //表头
            tableLabel.titleText = "拾取模型信息";
            //表头背景色
            tableLabel.titleBackgroundColor = colorFromARGB(255, 255, 255, 200);

            tableLabel.setColumnWidth(0, 80);

            tableLabel.setColumnWidth(1, 350);

            // 表的边框颜色
            tableLabel.borderColor = 0xffffffff;
            // 表的边框的宽度
            tableLabel.borderWidth = 2;
            // 表的背景色
            tableLabel.tableBackgroundColor = 0xffffffff;

            // 标题背景色
            tableLabel.titleBackgroundColor = 0xff2A6CC6;

            // 第一列文本样式
            var headerTextAttribute = __g.new_TextAttribute;
            headerTextAttribute.textColor = 0xffffffff;
            headerTextAttribute.outlineColor = 0xff000000;
            headerTextAttribute.font = "黑体";
            headerTextAttribute.bold = true;
            headerTextAttribute.multilineJustification = gviMultilineJustification.gviMultilineLeft;
            tableLabel.setColumnTextAttribute(0, headerTextAttribute);

            // 第二列文本样式
            var contentTextAttribute = __g.new_TextAttribute;
            contentTextAttribute.textColor = 4293256677;
            contentTextAttribute.outlineColor = 0xff000000;
            contentTextAttribute.font = "黑体";
            contentTextAttribute.bold = false;
            contentTextAttribute.multilineJustification = gviMultilineJustification.gviMultilineLeft;
            tableLabel.setColumnTextAttribute(1, contentTextAttribute);

            // 标题文本样式
            var capitalTextAttribute = __g.new_TextAttribute;
            capitalTextAttribute.textColor = 0xffffffff;
            capitalTextAttribute.outlineColor = 4279834905;
            capitalTextAttribute.font = "华文新魏";
            capitalTextAttribute.textSize = 14;
            capitalTextAttribute.multilineJustification = gviMultilineJustification.gviMultilineCenter;
            capitalTextAttribute.bold = true;
            tableLabel.titleTextAttribute = capitalTextAttribute;

            tableLabel.mouseSelectMask = 0;
            tableLabel.visibleMask = gviViewportMask.gviViewAllNormalView;
            __g.interactMode = gviInteractMode.gviInteractNormal;
        }
    }

}

function fnMouseDragSelect(pickResult, mask) {
    if (pickResult == null)
        return;
    var ctrl = $("CtrlSel").checked;
    if (!ctrl || (ctrl == true && mask != 12))	  //ctrl键
    {
        __g.featureManager.unhighlightAll();
    }

    if (pickResult != null) {
        for (i = 0; i < pickResult.count; i++) {
            var pr = pickResult.get(i);
            if (pr.type == gviObjectType.gviObjectFeatureLayer) {
                var fid = pr.featureId;
                var fl = pr.featureLayer;
                for (var fcGuid in __fcGeoMap) {
                    var fc = __fcMap[fcGuid];
                    if (fcGuid == fl.featureClassId) {
                        __g.featureManager.highlightFeature(fc, fid, 0xffff0000);
                    }
                }
            }
        }
    }
}



//跳转至中心点
function screentoword(x, y) {
    var x = x;
    var y = y;
    var scale = __g.new_Vector3;

    scale.set(x, y, 0);
    var ang = __g.new_EulerAngle;
    ang.heading = 10;
    ang.tilt = -45;
    __g.camera.flyTime = 5;
    __g.camera.lookAt(scale, 70, ang);
}

//清除选择
function StopCheck() {
    try {
        clearMulch();
        __g.interactMode = gviInteractMode.gviInteractNormal;
        __g.featureManager.unhighlightAll();
        deletetemplate(tableLabel);
    } catch (e) {
        console.log(e.toString());
    }
  
}

//拾取坐标
function Coordinate() {
    __g.interactMode = gviInteractMode.gviInteractMeasurement;
    __g.measurementMode = gviMeasurementMode.gviMeasureCoordinate;
}

//直线测量
function AerialDistance() {
    __g.interactMode = gviInteractMode.gviInteractMeasurement;
    __g.measurementMode = gviMeasurementMode.gviMeasureAerialDistance;
}
//水平测量
function HorizontalDistance() {
    __g.interactMode = gviInteractMode.gviInteractMeasurement;
    __g.measurementMode = gviMeasurementMode.gviMeasureHorizontalDistance;
}
//垂直测量
function VerticalDistance() {
    __g.interactMode = gviInteractMode.gviInteractMeasurement;
    __g.measurementMode = gviMeasurementMode.gviMeasureVerticalDistance;
}

/// <summary>
/// //飞行到点位并且显示模板信息
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
/// <param name="titleName">标题名称</param>
/// <param name="rownum">创建表格行数</param>
/// <param name="colnum">创建表格列数</param>
/// <param name="fifthX">x坐标</param>
/// <param name="fifthY">y坐标</param>
/// <param name="fifthY">y坐标</param>
/// <param name="fifthZ">level离地距离</param>
/// <param name="Array">数据对象</param>
function flytoPonitShowTemplate(titleName, rownum, colnum, colwidth1, colwidth2, fifthX, fifthY, fifthZ, data) {
    //var __g = document.getElementById("__g");
    if (tableLabel != null) {
        deletetemplate(tableLabel);
    }
    var rootId = '11111111-1111-1111-1111-111111111111';
    // 创建一个有3行2列的TableLabel(服务器用)
    // tableLabel = __g.objectManager.createTableLabel(rownum, colnum, rootId);
    //(本地用)
    tableLabel = __g.objectManager.createTableLabel(rownum, colnum);
    // 设定表头文字，
    tableLabel.titleText = titleName;
    //var point = __g.geometryFactory.createGeometry(gviGeometryType.gviGeometryPoint, gviVertexAttribute.gviVertexAttributeZ);
    point = __g.geometryFactory.createPoint(gviVertexAttribute.gviVertexAttributeZ);
    point.spatialCRS = fds.spatialReference;

    for (var i = 0; i < data.length; i++) {
        tableLabel.setRecord(i, 0, data[i].name);
        if (data[i].value != null) {
            tableLabel.setRecord(i, 1, data[i].value);
        }
    }
    //point = __g.geometryFactory.createPoint(gviVertexAttribute.gviVertexAttributeZ);
    //point.spatialCRS = fds.spatialReference;
    //标牌的位置
    position = __g.new_Vector3;
    position.set(fifthX, fifthY, fifthZ);
    point.position = position;
    tableLabel.position = point;

    // 列宽度
    tableLabel.setColumnWidth(0, colwidth1);
    tableLabel.setColumnWidth(1, colwidth2);


    // 表的边框颜色
    tableLabel.borderColor = 0xffffffff;
    // 表的边框的宽度
    tableLabel.borderWidth = 2;
    // 表的背景色
    tableLabel.tableBackgroundColor = 0xffffffff;

    // 标题背景色
    tableLabel.titleBackgroundColor = 0xff2A6CC6;

    // 第一列文本样式
    var headerTextAttribute = __g.new_TextAttribute;
    headerTextAttribute.textColor = 0xffffffff;
    headerTextAttribute.outlineColor = 0xff000000;
    headerTextAttribute.font = "黑体";
    headerTextAttribute.bold = true;
    headerTextAttribute.multilineJustification = gviMultilineJustification.gviMultilineLeft;
    tableLabel.setColumnTextAttribute(0, headerTextAttribute);

    // 第二列文本样式
    var contentTextAttribute = __g.new_TextAttribute;
    contentTextAttribute.textColor = 4293256677;
    contentTextAttribute.outlineColor = 0xff000000;
    contentTextAttribute.font = "黑体";
    contentTextAttribute.bold = false;
    contentTextAttribute.multilineJustification = gviMultilineJustification.gviMultilineLeft;
    tableLabel.setColumnTextAttribute(1, contentTextAttribute);

    // 标题文本样式
    var capitalTextAttribute = __g.new_TextAttribute;
    capitalTextAttribute.textColor = 0xffffffff;
    capitalTextAttribute.outlineColor = 4279834905;
    capitalTextAttribute.font = "华文新魏";
    capitalTextAttribute.textSize = 14;
    capitalTextAttribute.multilineJustification = gviMultilineJustification.gviMultilineCenter;
    capitalTextAttribute.bold = true;
    tableLabel.titleTextAttribute = capitalTextAttribute;

    var angle = __g.new_EulerAngle;
    angle.heading = 0;
    angle.tilt = -20;
    __g.camera.flyTime = 5;
    __g.camera.lookAt(position, 30, angle);
}
//删除模板
function deletetemplate(obj) {
    try {
        __g.objectManager.deleteObject(obj.guid);
    } catch (e) {
        console.log(e.toString());
    }
}
var url = config.Mapurl;
var layers = [];
var tile1 = new ol.layer.Tile({
    source: new ol.source.MapQuest({ layer: 'sat' })
});
var extent = [178757.297670768, 3196155.06694196, 550040.662492606, 3369186.01895213];
var center = [338280.149511882, 3354010.4205832];
var tile2 = new ol.layer.Tile({
    extent: extent,
    source: new ol.source.TileArcGISRest({
        url: url
    })
});

//地图默认中心点和放大级别
var view = new ol.View({
    projection: new ol.proj.Projection({
        code: 'EPSG:2437',
        units: 'pixels',
        extent: extent
    }),
    center: center,
    zoom: 9
});

var source = new ol.source.Vector();
var vector = new ol.layer.Vector({
    source: source,
    style: new ol.style.Style({
        fill: new ol.style.Fill({
            color: 'rgba(249, 24, 24, 0.7)'
        }),
        stroke: new ol.style.Stroke({
            color: '#18D8F9',
            width: 2
        }),
        image: new ol.style.Circle({
            radius: 7,
            fill: new ol.style.Fill({
                color: '#18D8F9'
            })
        })
    })
});

layers.push(tile1);
layers.push(tile2);
layers.push(vector);

//创建arcgis地图对象
var map = new ol.Map({
    layers: layers,
    target: 'map',
    view: view
});

var point_div = document.getElementById("css_animation");
var point_overlay = new ol.Overlay({
    element: point_div,
    positioning: 'bottom-right',
    stopEvent: false
});
map.addOverlay(point_overlay);

var mytemplate_div = document.getElementById("mytemplate");
var template_overlay = new ol.Overlay({
    element: mytemplate_div,
    positioning: 'center-left',
    stopEvent: false
})
map.addOverlay(template_overlay);
//添加定位点
function addpoint() {
    var points = document.getElementById("locationPoint").value;
    var point = points.split(';');
    for (var i = 0; i < point.length; ++i) {
        var newpoint = point[i].split(",");
        var x = newpoint[0];
        var y = newpoint[1];
        if (x == null || y == null || x == 'undefined' || y == 'undefined' || x == '' || y == '') {
            return;
        } else { addMark(i, x, y, 'Images/peoplelocation.png', 1); }
    }
}

//单击事件
map.on('click', function (evt) {
    var feature = map.forEachFeatureAtPixel(evt.pixel,
    function (feature, layer) {
        return feature;
    });
    if (feature) {
        //获取点位Id
        var name = feature.get('name');
        var typeName = feature.get('type');

        if (typeName != 'undefind' && name != 'undefind' && typeName != null && name != null) {
            switch (typeName) {
                case 'point':
                    var geometry = feature.get('geometry').v;
                    var x = geometry[0];
                    var y = geometry[1];
                    var value = feature.get('name');
                    var obj = $("#eventId");
                    obj.text(value);
                    break;
                case 'circle':
                    var id = feature.get('name')
                    break;
                case 'line':
                    var id = feature.get('name')
                    break;
                case 'polygon':
                    var id = feature.get('name')
                    break;
            }
        }
    }
    else {
        $(container).popover('destroy');
    }
});

/// <summary>
/// 添加定位点
/// </summary>
/// <param name="Id">数据唯一标识</param>
/// <param name="X">经度</param>
/// <param name="Y">纬度</param>
/// <param name="iconurl">图标Icon地址(或者http地址)</param>
/// <param name="opacity">图标透明度</param>
var vectorLayer = [];
function addMark(Id, X, Y, iconurl, opacity) {
    map.addOverlay(point_overlay);
    point_overlay.setPosition([X, Y]);
    var coordinate = [];
    coordinate.push(X);
    coordinate.push(Y);

    //创建一个Icon定位图标图层
    var iconFeature = new ol.Feature({
        //坐标
        geometry: new ol.geom.Point([X, Y]),
        type: 'point',
        name: Id,
        population: 4000,
        rainfall: 500,
    });

    //Icon定位图标设置
    var iconStyle = new ol.style.Style({
        image: new ol.style.Icon(({
            anchor: [1, 46],
            anchorXUnits: 'fraction',
            anchorYUnits: 'pixels',
            opacity: opacity,
            src: iconurl
        }))
    });

    //图层Icon设置
    iconFeature.setStyle(iconStyle);

    var vectorSource = new ol.source.Vector({
        features: [iconFeature]
    });

    var vectorLayers = new ol.layer.Vector({
        source: vectorSource
    });
    map.addLayer(vectorLayers);
    vectorLayer.push(vectorLayers);
    flytocoordinate(coordinate);
    return { feature: iconFeature, layer: vectorLayers, sourceVector: vectorSource, point: coordinate };
}

/// <summary>
/// maker鼠标样式
/// </summary>
$(map.getViewport()).on('mousemove', function (e) {
    var pixel = map.getEventPixel(e.originalEvent);
    var hit = map.forEachFeatureAtPixel(pixel, function (feature, layer) {
        return true;
    });
    targetStr = map.getTarget();
    targetEle = typeof targetStr === "string" ? $('#' + targetStr) : $(targetStr);
    if (hit) {
        targetEle.css('cursor', 'pointer');
    } else {
        targetEle.css('cursor', '');
    }
});


//飞到定位点(地图跟随移动)
function flytocoordinate(coordinate) {
    //var duration = 2000;
    //var start = +new Date();
    //var pan = ol.animation.pan({
    //    duration: duration,
    //    source: /** @type {ol.Coordinate} */ (view.getCenter()),
    //    start: start
    //});
    //var bounce = ol.animation.bounce({
    //    duration: duration,
    //    resolution: 4 * view.getResolution(),
    //    start: start
    //});
    //map.beforeRender(pan, bounce);
    view.setCenter(coordinate);
}

//绘线
function drawlinestring(obj) {
    var value = obj.value;
    addInteraction(value);
}

//绘面
function drawPolygon(obj) {
    var value = obj.value;
    //添加绘制
    addInteraction(value);
}

//绘圆
function drawCircle(obj) {
    var value = obj.value;
    //添加绘制
    addInteraction(value);
}

//回调获取绘线 绘范围 绘圆坐标
var pointerMoveHandler = function (evt) {
    var ponits = '';
    var type = draws.T;
    if (type != null && type != 'undefined' && type != '') {
        switch (type) {
            case 'LineString':
                if (draws != null && draws.a != null) {
                    for (var i = 0; i < draws.a.length; i++) {
                        ponits += draws.a[i][0] + "," + draws.a[i][1] + ";";
                    }
                }
                break;
            case 'Polygon':
                if (draws != null && draws.S != null) {
                    for (var i = 0; i < draws.S.length; i++) {
                        ponits += draws.S[i][0] + "," + draws.S[i][1] + ";";
                    }
                }
                break;
            case 'Circle':
                if (draws != null && draws.S != null) {
                    for (var i = 0; i < draws.S.length; i++) {
                        ponits += draws.S[i][0] + "," + draws.S[i][1] + ";";
                    }
                }
                break;
        }
        document.getElementById("locationPoint").value = ponits;
    }
}

//获取绘画图层的地理信息
var draws;
//绘制点.线.面
function addInteraction(value) {
    //指针移动监听
    map.on("pointermove", pointerMoveHandler);
    map.removeInteraction(draws);
    var value = value;
    if (value !== 'None') {
        var geometryFunction, maxPoints;
        if (value === 'Square') {
            value = 'Circle';
            geometryFunction = ol.interaction.Draw.createRegularPolygon(4);
        } else if (value === 'Box') {
            value = 'LineString';
            maxPoints = 2;
            geometryFunction = function (coordinates, geometry) {
                if (!geometry) {
                    geometry = new ol.geom.Polygon(null);
                }
                var start = coordinates[0];
                var end = coordinates[1];
                geometry.setCoordinates([
                  [start, [start[0], end[1]], end, [end[0], start[1]], start]
                ]);
                return geometry;
            };
        }
        draws = new ol.interaction.Draw({
            source: source,
            type: /** @type {ol.geom.GeometryType} */ (value),
            geometryFunction: geometryFunction,
            maxPoints: maxPoints
        });
        map.addInteraction(draws);
    }
}

//加线
function addlines() {
    var latandlont = [];
    var pointsvalue = document.getElementById("locationPoint").value;
    var point = pointsvalue.split(';');
    for (var i = 0; i < (point.length - 1) ; i++) {
        var lastpoint = point[i].split(',');
        var x = parseFloat(lastpoint[0]);
        var y = parseFloat(lastpoint[1]);
        var points = [x, y];
        latandlont.push(points);
    }
    addDrawLine(1, 'line', latandlont, '#18D8F9', 2);
}

//加面
function addNoodlescall() {
    var points = [];
    var strpoints = document.getElementById("locationPoint").value;
    var point = strpoints.split(';');
    for (var i = 0; i < (point.length - 1) ; i++) {
        var lastpoint = point[i].split(',');
        var x = lastpoint[0];
        var y = lastpoint[1];
        var latandlont = [x, y];
        points.push(latandlont);
    }
    var pointslist = [];

    pointslist.push(points);
    addDrawNoodles(2, 'polygon', pointslist, 'rgba(240, 19, 23, 0.7)', '#18D8F9', 2);
}

//加圆
function addCircle() {
    var start = [], end = [];
    var strpointsvalue = document.getElementById("locationPoint").value;
    var point = strpointsvalue.split(";");
    var startpoint, endpoint;
    startpoint = point[0].split(',');
    endpoint = point[1].split(',');
    var startx, starty;
    startx = startpoint[0];
    starty = startpoint[1];
    start.push(startx);
    start.push(starty);

    var x = parseFloat(startx);
    var y = parseFloat(starty);
    //中心点
    var centerpoint = [x, y];
    var endx, endy;
    endx = endpoint[0];
    endy = endpoint[1];
    end.push(endx);
    end.push(endy);

    //计算半径
    var line = new ol.geom.LineString([start, end]);
    var radius = line.getLength();
    //获取
    addDrawCircle(3, 'circle', centerpoint, radius, 'rgba(240,19,23,0.7)', '#18D8F9', 2);
}

//轨迹回放
function paly() {
    var longpointlist = "362080.883286335,3306732.1863086;362135.624518341,3306720.02159037;362086.965645447,3306659.19799926;362062.636209,3306589.25086947;362059.595029444,3306519.30373969;362010.936156551,3306491.93312368;362010.936156551,3306431.10953257;361998.771438327,3306406.78009612;361989.64789966,3306400.69773701;361983.565540548,3306391.57419834;361968.359642769,3306373.327121;361965.318463213,3306361.16240278;361950.112565433,3306327.70942767;361947.071385878,3306306.42117078;361925.783128986,3306276.00937522;361925.783128986,3306266.88583655;361916.659590319,3306245.59757966;361895.371333428,3306209.10342499;361886.24779476,3306184.77398854;361871.041896981,3306166.52691121;361855.835999202,3306142.19747476;361843.671280978,3306117.86803831;361837.588921867,3306096.57978142;361825.424203643,3306072.25034497;361819.341844531,3306044.87972897;361791.971228529,3306029.67383119;361798.05358764,3306005.34439475;361855.835999202,3305996.22085608;361883.206615204,3305977.97377874;361922.741949431,3305968.85024008;361980.524360992,3305947.56198318;362020.059695218,3305932.3560854;362056.553849888,3305904.9854694;362096.089184115,3305883.69721251;362135.624518341,3305871.53249429;362162.995134344,3305853.28541695;362199.489289014,3305841.12069873;362229.901084572,3305831.99716006;362263.354059687,3305813.75008273;362308.971753025,3305789.42064628;362339.383548583,3305774.2147485;362403.248319256,3305755.96767116;362424.536576147,3305749.88531205;362448.866012594,3305737.72059383;362400.207139701,3305692.10290049;362378.91888281,3305652.56756627;362354.589446363,3305616.0734116;362336.342369028,3305570.45571826;362321.136471248,3305537.00274314;362302.889393913,3305482.26151114;362278.559957466,3305406.23202224;362266.395239243,3305366.69668802;362232.942264128,3305321.07899468;362220.777545905,3305284.58484001;362220.777545905,3305254.17304445;362214.695186793,3305226.80242845;362187.32457079,3305217.67888978;362111.295081894,3305254.17304445;362035.265592997,3305269.37894223;361953.153744989,3305299.79073779;361934.906667654,3305333.2437129;361861.918358313,3305357.57314935;361791.971228529,3305378.86140624;361746.353535191,3305409.2732018;361709.85938052,3305433.60263824;361649.035789403,3305457.93207469;361588.212198286,3305479.22033158;361569.965120951,3305488.34387025;361542.594504948,3305460.97325425;361539.553325392,3305412.31438135;361521.306248057,3305372.77904713;361484.812093387,3305308.91427645;361475.688554719,3305260.25540356;361457.441477384,3305229.843608;361341.876654261,3305269.37894223;361305.382499591,3305257.214224;361229.353010695,3305278.50248089;361168.529419577,3305287.62601956;361089.458751125,3305299.79073779;361077.294032902,3305314.99663557;360998.223364449,3305330.20253334;360955.646850667,3305342.36725157;360876.576182215,3305363.65550846;360849.205566212,3305372.77904713;360827.917309321,3305375.82022668;360809.670231986,3305375.82022668;360818.793770654,3305403.19084269;360843.1232071,3305445.76735647;360897.864439106,3305488.34387025;360934.358593776,3305540.0439227;360967.811568891,3305588.70279559;360976.935107558,3305616.0734116;361016.470441784,3305658.64992538;361034.71751912,3305704.26761872;361065.129314678,3305749.88531205;361068.170494234,3305792.46182584;361110.747008016,3305831.99716006;361116.829367128,3305871.53249429;361147.241162686,3305898.90311029;361089.458751125,3305944.52080363;361068.170494234,3305965.80906052;361056.005776011,3305984.05613785;361052.964596455,3305996.22085608;361086.417571569,3306017.50911297;361092.499930681,3306020.55029252;361116.829367128,3306032.71501075;361153.323521798,3306017.50911297;361183.735317357,3306005.34439475;361198.941215136,3305993.17967652;361226.311831139,3306060.08562675;361232.394190251,3306105.70332009;361235.435369806,3306126.99157698;361281.053063144,3306154.36219298;361278.011883588,3306196.93870676;361293.217781368,3306242.5564001;361311.464858703,3306263.84465699;361329.711936038,3306300.33881166;361347.959013373,3306348.99768456;361366.206090708,3306400.69773701;361384.453168044,3306434.15071212;361427.029681826,3306510.18020102;361451.359118272,3306537.55081702;361500.017991166,3306470.64486679;361524.347427613,3306458.48014857;361539.553325392,3306449.3566099;361560.841582283,3306437.19189168;361585.17101873,3306431.10953257;361594.294557398,3306431.10953257;361545.635684504,3306361.16240278;361536.512145836,3306333.79178678;361536.512145836,3306321.62706856;361557.800402728,3306309.46235033;361582.129839174,3306309.46235033;361600.37691651,3306300.33881166;361609.500455177,3306294.25645255;361588.212198286,3306260.80347744;361579.088659619,3306242.5564001;361554.759223172,3306206.06224543;361554.759223172,3306193.89752721;361548.67686406,3306175.65044987;361539.553325392,3306151.32101343;361536.512145836,3306142.19747476;361539.553325392,3306120.90921787;361569.965120951,3306108.74449964;361594.294557398,3306096.57978142;361606.459275621,3306093.53860187;361624.706352956,3306087.45624275;361642.953430292,3306126.99157698;361673.36522585,3306175.65044987;361682.488764518,3306199.97988632;361691.612303185,3306227.35050232;361718.982919188,3306251.67993877;361728.106457856,3306279.05055477;361749.394714747,3306318.585889;361767.641792082,3306358.12122323;361791.971228529,3306400.69773701;361807.177126308,3306437.19189168;361843.671280978,3306507.13902146;361858.877178758,3306543.63317613;";

    playTrack(longpointlist, 'rgba(0,59,137,1)', 10, 'Images/peoplelocation.png', 'rgba(240,244,249,1)', 10, 5, 9, 200);
}

/// <summary>
/// 根据坐标点加线
/// </summary>
/// <param name="Id">线的唯一标识</param>
/// <param name="type">类型</param>
/// <param name="listPoints">线的点位集合</param>
/// <param name="strokeColor">线颜色</param>
/// <param name="listPoints">线宽度</param>
var rmblinelayer = [];
function addDrawLine(Id, type, listPoints, strokeColor, strokeWidth) {
    //创建一个绘线图层
    var linefeature = new ol.Feature({
        type: type,
        name: Id,
        geometry: new ol.geom.LineString(listPoints)
    });
    var lineStyle = new ol.style.Style({
        stroke: new ol.style.Stroke({
            color: strokeColor,
            width: strokeWidth
        })
    });
    linefeature.setStyle(lineStyle);
    var lineSource = new ol.source.Vector({
        features: [linefeature]
    });
    var lineLayer = new ol.layer.Vector({
        source: lineSource,
    });
    map.addLayer(lineLayer);
    rmblinelayer.push(lineLayer);
}

/// <summary>
/// 根据坐标点加画面
/// </summary>
/// <param name="pointslist">面的点位集合</param>
/// <param name="fillColor">面填充颜色'rgba(240, 19, 23, 0.7)'</param>
/// <param name="strokeColor">面边框颜色'#18D8F9'</param>
/// <param name="strokeWidth">面边框宽度2</param>
var rmbNoodleslayer = [];
function addDrawNoodles(Id, type, pointslist, fillColor, strokeColor, strokeWidth) {
    var vectorSource = new ol.source.Vector();
    var polygon = new ol.geom.Polygon(pointslist);
    vectorSource.addFeature(
        new ol.Feature({
            type: type,
            name: Id,
            geometry: polygon,
        })
    )
    var vectorLayer = new ol.layer.Vector({
        source: vectorSource,
        style: function (feature) {
            return [
                new ol.style.Style({
                    fill: new ol.style.Fill({
                        color: fillColor
                    }),
                    stroke: new ol.style.Stroke({
                        width: strokeWidth,
                        color: strokeColor
                    })
                })
            ]
        }
    })
    map.addLayer(vectorLayer);
    rmbNoodleslayer.push(vectorLayer);
}

/// <summary>
/// 根据坐标点加画圆
/// </summary>
/// <param name="Id">圆的唯一标识</param>
/// <param name="type">类型</param>
/// <param name="startPoints">中心点位</param>
/// <param name="radius">半径</param>
/// <param name="fillColor">圆填充颜色'rgba(240,19,23,0.7)'</param>
/// <param name="strokeColor">圆边颜色'#18D8F9'</param>
/// <param name="strokeWidth">圆边宽度2</param>
var rmbCirclelayer = [];
function addDrawCircle(Id, type, startPoints, radius, fillColor, strokeColor, strokeWidth) {
    var vectorSource = new ol.source.Vector();
    var circle = new ol.geom.Circle(startPoints, radius, 0);
    vectorSource.addFeature(
        new ol.Feature({
            type: type,
            name: Id,
            geometry: circle,
        })
    )
    var vectorLayer = new ol.layer.Vector({
        source: vectorSource,
        style: function (feature) {
            return [
                new ol.style.Style({
                    fill: new ol.style.Fill({
                        color: fillColor
                    }),
                    stroke: new ol.style.Stroke({
                        color: strokeColor,
                        width: strokeWidth
                    })
                })
            ]
        }
    })
    map.addLayer(vectorLayer);
    rmbCirclelayer.push(vectorLayer);
}

/// <summary>
/// 轨迹回放
/// </summary>
/// <param name="longpointlist">坐标数据</param>
/// <param name="lineColor">轨迹颜色</param>
/// <param name="lineWidth">轨迹宽度</param>
/// <param name="iconurl">图标Icon地址(或者http地址)</param>
/// <param name="linePointColor">轨迹线点颜色</param>
/// <param name="linePointWidth">轨迹线点宽度</param>
/// <param name="linePointRadius">轨迹线点圆角</param>
/// <param name="speed">速度</param>
var positions = [];
var goIconUrl;
var pointslength;
var goTrackObj;
var againspeed;
var j;
function playTrack(longpointlist, lineColor, lineWidth, iconurl, linePointColor, linePointWidth, linePointRadius, zoom, speed) {
    view.setZoom(zoom);
    window.clearInterval(goTrackObj);
    positions.splice(0, positions.length);
    iconurl = iconurl;
    var startx, starty;
    var newpoint = longpointlist.split(';');
    var startlatandlnt = newpoint[0].split(',');
    startx = parseFloat(startlatandlnt[0]);
    starty = parseFloat(startlatandlnt[1]);
    for (var i = 0; i < (newpoint.length - 1) ; ++i) {
        var points = newpoint[i].split(',');
        var x = parseFloat(points[0]);
        var y = parseFloat(points[1]);
        var latandlot = [x, y];
        positions.push(latandlot);
    }
    j = 0;
    goIconUrl = iconurl;
    againspeed = speed;
    //清除轨迹线
    removeTrackLine();
    //清除轨迹线点
    removeTrackLinePoint();
    //绘制轨迹
    drawTrackLine(positions, lineColor, lineWidth, linePointColor, linePointWidth, linePointRadius);
    //绘制启始点位
    drawTrackPoint(iconurl, startx, starty);
    pointslength = positions.length;
    goTrackObj = setInterval("goPlay()", speed);
}

/// <summary>
/// 轨迹奔跑
/// </summary>
function goPlay() {
    if (j < (pointslength - 1)) {
        j++;
        var points = positions[j];
        var x = points[0];
        var y = points[1];
        removeTrackPoint();
        drawTrackPoint(goIconUrl, x, y);
    } else {
        return;
    }
}

/// <summary>
/// 绘制轨迹线
/// </summary>
/// <param name="listPoints">坐标数据</param>
/// <param name="linecolor">轨迹线颜色</param>
/// <param name="linewidth">轨迹线宽度</param>
/// <param name="linePointColor">轨迹线点颜色</param>
/// <param name="linePointWidth">轨迹线点宽度</param>
/// <param name="linePointRadius">轨迹线点圆角</param>
var TrackLine = [];
function drawTrackLine(listPoints, linecolor, linewidth, linePointColor, linePointWidth, linePointRadius) {
    //创建一个绘线图层
    var linefeature = new ol.Feature({
        geometry: new ol.geom.LineString(listPoints)
    });
    var lineStyle = new ol.style.Style({
        stroke: new ol.style.Stroke({
            color: linecolor,
            width: linewidth
        })
    });
    linefeature.setStyle(lineStyle);
    var lineSource = new ol.source.Vector({
        features: [linefeature]
    });
    var lineLayer = new ol.layer.Vector({
        source: lineSource,
    });
    map.addLayer(lineLayer);
    TrackLine.push(lineLayer);
    for (var i = 0; i < listPoints.length; ++i) {
        var x = listPoints[i][0];
        var y = listPoints[i][1];
        //绘制轨迹线点
        drawTrackLinePoint(x, y, linePointColor, linePointWidth, linePointRadius);
    }
}

/// <summary>
/// 绘制轨迹线点位
/// </summary>
/// <param name="x">经度</param>
/// <param name="y">纬度</param>
/// <param name="linePointcolor">线点颜色</param>
/// <param name="linePointWidth">点宽度</param>
/// <param name="linePointRadius">轨迹线点圆角</param>
var drawTrackLinePoints = [];
function drawTrackLinePoint(x, y, linePointcolor, linePointWidth, linePointRadius) {
    //创建一个无图片的点位层
    var pointFeature = new ol.Feature({
        //坐标
        geometry: new ol.geom.Point([x, y]),
        name: null,
        population: 4000,
        rainfall: 500,
    });

    var pointStyle = new ol.style.Style({
        fill: new ol.style.Fill({
            width: linePointWidth,
            color: linePointcolor
        }),
        stroke: new ol.style.Stroke({
            color: linePointcolor,
            width: linePointWidth
        }),
        image: new ol.style.Circle({
            radius: linePointRadius,
            fill: new ol.style.Fill({
                color: linePointcolor,
            })
        })

    });

    pointFeature.setStyle(pointStyle);
    var vectorSource = new ol.source.Vector({
        features: [pointFeature]
    });
    var vectorLayers = new ol.layer.Vector({
        source: vectorSource
    });
    map.addLayer(vectorLayers);
    drawTrackLinePoints.push(vectorLayers);
}

/// <summary>
/// 绘制轨迹点位
/// </summary>
/// <param name="iconurl">图标地址</param>
/// <param name="X">经度</param>
/// <param name="Y">纬度</param>
var drawTrackPoints = [];
//绘制轨迹点
function drawTrackPoint(iconurl, X, Y) {
    var coordinate = [];
    coordinate.push(X);
    coordinate.push(Y);

    //创建一个Icon定位图标图层
    var iconFeature = new ol.Feature({
        //坐标
        geometry: new ol.geom.Point([X, Y]),
        name: null,
        population: 4000,
        rainfall: 500,
    });

    //Icon定位图标设置
    var iconStyle = new ol.style.Style({
        image: new ol.style.Icon(({
            anchor: [0.5, 46],
            anchorXUnits: 'fraction',
            anchorYUnits: 'pixels',
            opacity: 1,
            src: iconurl
        }))
    });

    //图层Icon设置
    iconFeature.setStyle(iconStyle);

    var vectorSource = new ol.source.Vector({
        features: [iconFeature]
    });

    var vectorLayers = new ol.layer.Vector({
        source: vectorSource,
    });
    map.addLayer(vectorLayers);
    drawTrackPoints.push(vectorLayers);
    flytocoordinate(coordinate);
    return { feature: iconFeature, layer: vectorLayers, sourceVector: vectorSource, point: coordinate };
}

/// <summary>
/// 清除轨迹线
/// </summary>
function removeTrackLine() {
    if (TrackLine.length > 0) {
        for (var i = 0; i < TrackLine.length; i++) {
            map.removeLayer(TrackLine[i]);
        }
    }
}

//清除轨迹线点
function removeTrackLinePoint() {
    if (drawTrackLinePoints.length > 0) {
        for (var i = 0; i < drawTrackLinePoints.length; ++i) {
            map.removeLayer(drawTrackLinePoints[i]);
        }
    }
}

/// <summary>
/// 清除轨迹点
/// </summary>
function removeTrackPoint() {
    if (drawTrackPoints.length > 0) {
        for (var i = 0; i < drawTrackPoints.length; i++) {
            map.removeLayer(drawTrackPoints[i]);
        }
    }
}

/// <summary>
/// 暂停
/// </summary>
function stopPlayTrack() {
    window.clearInterval(goTrackObj);
}

/// <summary>
/// 重启
/// </summary>
function againPalyTrack() {
    goTrackObj = setInterval("goPlay()", againspeed)//重新开始定时器
}

/// <summary>
/// 清除整个轨迹
/// </summary>
function clearTrack() {
    window.clearInterval(goTrackObj);
    positions.splice(0, positions.length);
    if (drawTrackPoints.length > 0) {
        for (var i = 0; i < drawTrackPoints.length; i++) {
            map.removeLayer(drawTrackPoints[i]);
        }
    }
    if (TrackLine.length > 0) {
        for (var i = 0; i < TrackLine.length; i++) {
            map.removeLayer(TrackLine[i]);
        }
    }
    if (drawTrackLinePoints.length > 0) {
        for (var i = 0; i < drawTrackLinePoints.length; ++i) {
            map.removeLayer(drawTrackLinePoints[i]);
        }
    }
}

//清除覆盖物
function clearMulch() {
    map.removeInteraction(draws);
    source.clear();
    map.removeOverlay(point_overlay);
    map.removeOverlay(template_overlay);
    if (vectorLayer.length > 0) {
        for (var i = 0; i < vectorLayer.length; i++) {
            map.removeLayer(vectorLayer[i]);
        }
    }
    if (rmblinelayer.length > 0) {
        for (var i = 0; i < rmblinelayer.length; i++) {
            map.removeLayer(rmblinelayer[i]);
        }
    }
    if (rmbNoodleslayer.length > 0) {
        for (var i = 0; i < rmbNoodleslayer.length; i++) {
            map.removeLayer(rmbNoodleslayer[i]);
        }
    }
    if (rmbCirclelayer.length > 0) {
        for (var i = 0; i < rmbCirclelayer.length; i++) {
            map.removeLayer(rmbCirclelayer[i]);
        }
    }
}

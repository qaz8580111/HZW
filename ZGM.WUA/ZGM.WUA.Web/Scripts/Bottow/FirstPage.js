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

    //$('#opera')[0].clientWidth = param.bottomDiv_Width;

    $('.tb').css("width", param.bottomDiv_Width);
    $('.fullTb').css("width", param.bottomDiv_Width);
    //$("#chart_1").css("width", ($("body").width() - 510) / 3);
    $("#chart_2").css("width", ($("body").width() - 850));
    firstPage.init();

};
var colors = [];
colors.push("#FF0000");
colors.push("#FFCC00");
colors.push("#99FF00");
colors.push("#00FFFF");
colors.push("#008B00");
colors.push("#FF00CC");
colors.push("#F1A7B6");
var firstPage = {
    apiconfig: null,

    init: function () {

        //this.colors.push(this.changeRGBToColor("rgb( 199,238,22 )"));
        this.apiconfig = parent.globalConfig.apiconfig;
        this.loadEvent();
        //this.loadEventCountByTime();
        this.loadStaffInfo();
        this.tabClick();
    },
    loadEvent: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/Task/GetSourceStatSum?startTime=&endTime=",
            dataType: "json",
            success: function (result) {
                for (var i = 0; i < result.length; i++) {
                    var sourceId = result[i].SourceId;
                    if ($("label:contains('" + result[i].SourceName + "')+label").length > 0) {
                        $("label:contains('" + result[i].SourceName + "')+label")[0].textContent = result[i].Sum;
                        $("label:contains('" + result[i].SourceName + "')+label").css('color', colors[i]);
                        $("label:contains('" + result[i].SourceName + "')+label")[0].parentElement.setAttribute("onclick", "firstPage.showEventList(" + result[i].SourceId + ");");
                    }
                }
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    showEventList: function (sourceId) {
        parent.event.initEventList(sourceId);
    },
    loadEventCountByTime: function () {
        var zhcgModel = [];
        var qzrxtsModel = [];
        var sqsbModel = [];
        var dyxcfxModel = [];
        var jkfxModel = [];
        var ldxcfxModel = [];

        var times = [];


        //document.getElementById('chart_1').html("");
        $('#chart_1').html('<div id="allbg"><div id="showLoading" class="showLoading" style="text-align:center;"><img src="/images/list/loading2.gif" style="width: 20px;margin-top:10px; height: 20px" />' +
       '<br /><p style="font-size: 12px;color:white">正在加载中...</></div></div>');

        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/Task/GetSourceStatSub?startTime=&several=14&hours=1",
            dataType: "json",
            success: function (result) {
                var s = result;
                if (result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        if (result[i].SourceName == "智慧城管") {
                            zhcgModel.push(result[i].Sum);
                            times.push(result[i].StatTime.substr(11, 5));
                            //zhcgSum += result[i].Sum;
                        }
                        if (result[i].SourceName == "群众热线投诉") {
                            qzrxtsModel.push(result[i].Sum);
                            //qzrxtsSum += result[i].Sum;
                        }
                        if (result[i].SourceName == "社区上报") {
                            sqsbModel.push(result[i].Sum);
                            //sqsbSum += result[i].Sum;
                        }
                        if (result[i].SourceName == "队员巡查发现") {
                            dyxcfxModel.push(result[i].Sum);
                            //dyxcfxSum += result[i].Sum;
                        }
                        if (result[i].SourceName == "监控发现") {
                            jkfxModel.push(result[i].Sum);
                            //jkfxgSum += result[i].Sum;
                        }
                        if (result[i].SourceName == "领导巡查发现") {
                            ldxcfxModel.push(result[i].Sum);
                            //ldxcfxSum += result[i].Sum;
                        }
                    }
                    var totalFive = [];
                    for (var i = 0; i < dyxcfxModel.length; i++) {
                        if (qzrxtsModel[i] == null) {
                            totalFive.push(null);
                        } else {
                            totalFive.push(firstPage.dataFormat(qzrxtsModel[i]) + firstPage.dataFormat(sqsbModel[i]) + firstPage.dataFormat(dyxcfxModel[i]) + firstPage.dataFormat(jkfxModel[i]) + firstPage.dataFormat(ldxcfxModel[i]));
                        }
                    }
                    myChart_1 = echarts.init(document.getElementById('chart_1'));
                    option.series[0].data = zhcgModel;
                    option.series[1].data = totalFive;

                    //y轴间距控制
                    var temp = [];
                    temp.push(Math.max.apply(Math, zhcgModel));
                    temp.push(Math.max.apply(Math, totalFive));
                    //temp.push(Math.max.apply(Math, jrbj));
                    var maxNum = Math.max.apply(Math, temp);
                    if (maxNum == 0) {
                        maxNum = 1;
                    }
                    option.yAxis.min = 0;
                    option.yAxis.interval = Math.ceil(maxNum / 2);
                    option.yAxis.max = option.yAxis.interval * 2;
                    //option.series[2].data =jkfxModel ;
                    //option.series[3].data =qzrxtsModel ;
                    //option.series[4].data =sqsbModel ;
                    //option.series[5].data = ldxcfxModel;
                    option.xAxis.data = times;
                    myChart_1.setOption(option);
                }
            },
            error: function (errorMsg) {
                $('#carChartData').html('<div id="allbg"><div id="showLoading" class="showLoading" style="text-align:center;vertical-align: middle;">' +
 '<br /><p style="font-size: 12px;color:white">加载失败</></div></div>');
            }
        });

        //myChart_2 = echarts.init(document.getElementById('chart_2'));
        //myChart_2.setOption(option);
    },
    dataFormat: function (data) {
        if (data == undefined) {
            return 0;
        }
        else {
            return data;
        }
    },
    loadStaffInfo: function () {
        var zhcgModel = [];
        var qzrxtsModel = [];
        var sqsbModel = [];
        var dyxcfxModel = [];
        var jkfxModel = [];
        var ldxcfxModel = [];

        var times = [];

        var chart2_x = [];
        var data_chart2 = [];
        var zhcgSum = 0;
        var qzrxtsSum = 0;
        var sqsbSum = 0;
        var dyxcfxSum = 0;
        var jkfxgSum = 0;
        var ldxcfxSum = 0;
        $('#chart_2').html('<div id="allbg"><div id="showLoading" class="showLoading" style="text-align:center;"><img src="/images/list/loading2.gif" style="width: 20px;margin-top:10px; height: 20px" />' +
       '<br /><p style="font-size: 12px;color:white">正在加载中...</></div></div>');
        var re = [11, 22, 33, 44, 10, 2, 10]
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/User/GetUnitStat?parentId=&seconds=",
            dataType: "json",
            success: function (result) {

                if (result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        chart2_x.push({
                            value: result[i].UnitName.replace("分队", ""),
                            textStyle:
                            {
                                //color: colors[i],
                                color: "#17CBFC",
                                fontSize: 10
                            }
                        });
                        var Percert = result[i].Sum;
                        //var Percert = re[i];
                        data_chart2.push({
                            value: Percert, label: {
                                normal: {
                                    textStyle: {
                                        //   color: firstPage.changeRGBToColor("rgb( 53,238,175 )")
                                    }
                                }
                            }, itemStyle: {
                                normal: {
                                    color: colors[i]
                                }
                            }
                        });
                    }
                }

                myChart_2 = echarts.init(document.getElementById('chart_2'));
                option2.series[0].type = "bar";
                option2.series[0].data = data_chart2;
                option2.xAxis.data = chart2_x;

                //y轴间距控制              
                option2.yAxis.min = 0;
                option2.yAxis.interval = 0.5;
                option2.yAxis.max = option.yAxis.interval * 2;

                myChart_2.setOption(option2);

            },
            error: function (errorMsg) {
                $('#carChartData').html('<div id="allbg"><div id="showLoading" class="showLoading" style="text-align:center;vertical-align: middle;">' +
 '<br /><p style="font-size: 12px;color:white">加载失败</></div></div>');
            }
        });
    },
    changeRGBToColor: function (rgb) {
        var that = rgb;
        if (/^(rgb|RGB)/.test(that)) {
            var aColor = that.replace(/(?:\(|\)|rgb|RGB)*/g, "").split(",");
            var strHex = "#";
            for (var i = 0; i < aColor.length; i++) {
                var hex = Number(aColor[i]).toString(16);
                if (hex === "0") {
                    hex += hex;
                }
                strHex += hex;
            }
            if (strHex.length !== 7) {
                strHex = that;
            }
            return strHex;
        } else if (reg.test(that)) {
            var aNum = that.replace(/#/, "").split("");
            if (aNum.length === 6) {
                return that;
            } else if (aNum.length === 3) {
                var numHex = "#";
                for (var i = 0; i < aNum.length; i += 1) {
                    numHex += (aNum + aNum);
                }
                return numHex;
            }
        } else {
            return that;
        }
    },
    tabClick: function () {
        $('.td2Font').each(function (i) {
            $(this).click(function () {
                $(this).addClass("td2Selected").siblings().removeClass("td2Selected");
                switch (i) {
                    case 0: firstPage.cameraOnClick(); break;
                    case 1: firstPage.staffOnClick(); break;
                    case 2: firstPage.eventOnClick(); break;
                    case 3: firstPage.carOnClick(); break;
                }
            });
        });
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
    }
};
seriersModel = {
    name: "",
    symbol: "circle",
    type: 'line',
    data: []
}
var myChart_1 = null;
var myChart_2 = null; //echarts.init(document.getElementById('chart_2'));
option = {
    tooltip: {
        trigger: 'axis'
    },
    toolbox: {
        feature: {
            saveAsImage: {}
        }
    },
    grid: {
        left: '0%',
        right: '4%',
        bottom: '3%',
        top: '7%',
        containLabel: true,
        backgroundColor: "transparent",
        borderColor: "transparent",
        borderWidth: 1,
        show: true,
        borderColor: "#137496"
    },
    title: {
        show: false,
        padding: 0
    },
    legend: {
        show: false
    },
    xAxis: {
        nameTextStyle: { color: "#f1efe9", fontSize: 8 },
        type: 'category',
        boundaryGap: false,
        data: [],
        splitLine: {
            show: false
        },
        axisLabel: { textStyle: { color: "#18bfee" } }
    },
    yAxis: {
        nameTextStyle: { color: "#f1efe9", fontSize: 8 },
        type: 'value',
        splitLine: {
            show: false
        },
        axisLabel: { textStyle: { color: "#6fdcf5" } }
    },
    //textStyle: {
    //    color: "#f1efe9",
    //    fontSize:8
    //},
    color: colors,
    series: [
        {
            name: "智慧城管",
            symbol: "circle",
            type: 'line',
            //stack: '总量',
            data: []
        }
        ,
        {
            name: "队员巡查发现",
            symbol: "rect",
            type: 'line',
            //stack: 'aaa',
            data: []
        }
        //,
        //{
        //    name: "监控发现",
        //    symbol: "circle",
        //    type: 'line',
        //    // stack: '总量',
        //    data: []
        //},
        //{
        //    name: "群众热线投诉",
        //    symbol: "circle",
        //    type: 'line',
        //    //stack: '总量',
        //    data: []
        //},
        //{
        //    name: "社区上报",
        //    symbol: "circle",
        //    type: 'line',
        //    //stack: '总量',
        //    data: []
        //},
        //{
        //    name: "领导巡查发现",
        //    symbol: "circle",
        //    type: 'line',
        //    //stack: '总量',
        //    data: []
        //}
    ]
};

option2 = {
    tooltip: {
        trigger: 'axis'
    },
    toolbox: {
        feature: {
            saveAsImage: {}
        }
    },
    grid: {
        left: '0%',
        right: '4%',
        bottom: '3%',
        top: '7%',
        containLabel: true,
        backgroundColor: "transparent",
        borderColor: "transparent",
        // borderWidth: 1,
        show: true,
        //borderColor: "#137496"
    },
    title: {
        show: false,
        padding: 0
    },
    legend: {
        show: false
    },
    xAxis: {
        type: 'category',
        splitLine: {
            show: false
        },
        axisLabel: { textStyle: { color: "#18bfee" } },
        data: [],
        axisLabel: {
            interval: 0,
            margin: 5
        }
    },
    yAxis: {
        splitLine: {
            show: false
        },
        type: 'value',
        axisLabel: { textStyle: { color: "#6fdcf5" } }
        //,
        //min: 0,
        //max: 100
    },
    series: [
        {
            name: '汇总',
            type: 'bar',
            stack: '总量',
            barCategoryGap: '80%',
            label: {
                normal: {
                    show: true,
                    position: 'inside'
                }
            },
            data: []
        }
    ]
};
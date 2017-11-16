
$(function () {
    carZGM.init();
});
document.body.onresize = function () {
    carZGM.changeSize();
}

var carZGM = {
    apiconfig: null,
    init: function () {
        this.apiconfig = parent.globalConfig.apiconfig;
        this.changeSize();
        this.loadCarChartData();
    },
    changeSize: function () {
        var tbWidth = document.body.clientWidth;
        $("#carChartData").css("width", tbWidth - 250);
    },
    loadZFCarCount: function (jrzxCount, jrlxCount, jrbjCount) {
        $(".onlineCar").html(jrzxCount + "/");
        $("#onlineCarNum").html(jrzxCount);
        $("#offLineCarNum").html(jrlxCount);
        $("#reportCarNum").html(jrbjCount);
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/Car/GetCarsCount?carNumber=&isOnline=",
            dataType: "json",
            success: function (result) {
                $(".totalCar").html(result);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    loadCarClassify: function () { },
    loadCarChartData: function () {
        var jrzxCount = 0;
        var jrlxCount = 0;
        var jrbjCount = 0;
        var jrzx = [];
        var jrlx = [];
        var jrbj = [];
        var times = [];
        $('#carChartData').html('<div id="allbg"><div id="showLoading" class="showLoading" style="text-align:center;vertical-align: middle;"><img src="/images/list/loading2.gif" style="width: 20px; height: 20px;margin-top:10px" />' +
 '<br /><p style="font-size: 12px;color:white">正在加载中...</></div></div>');
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/Car/GetCarStatSub?startTime=&endTime=",
            dataType: "json",
            success: function (result) {
                var s = result;
                var time = new Date();
                if (result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        if (result[i].TypeName == "今日在线") {
                            times.push(result[i].StatTime.substr(11, 5));
                            if (result[i].StatTime.substr(11, 2) <= time.getHours()) {
                                jrzx.push(result[i].Sum);
                                jrzxCount = result[i].Sum;
                            } else {
                                jrzx.push(null);
                            }
                        }
                        if (result[i].TypeName == "今日离线") {
                            if (result[i].StatTime.substr(11, 2) <= time.getHours()) {
                                jrlx.push(result[i].Sum);
                                jrlxCount = result[i].Sum;
                            } else {
                                jrlx.push(null);
                            }
                        }
                        if (result[i].TypeName == "今日报警") {
                            if (result[i].StatTime.substr(11, 2) <= time.getHours()) {
                                jrbj.push(result[i].Sum);
                                jrbjCount = result[i].Sum;
                            } else {
                                jrbj.push(null);
                            }
                        }
                    }
                    myChart_1 = echarts.init(document.getElementById('carChartData'));
                    option.series[0].data = jrzx;
                    option.series[1].data = jrlx;
                    option.series[2].data = jrbj;
                    option.xAxis.data = times;
                    myChart_1.setOption(option);
                }
                else {
                    $('#carChartData').html('<div id="allbg"><div id="showLoading" class="showLoading" style="text-align:center;vertical-align: middle;">' +
'<br /><p style="font-size: 12px;color:white">没有相关数据！</></div></div>');
                }
                carZGM.loadZFCarCount(jrzxCount, jrlxCount, jrbjCount);
            },
            error: function (errorMsg) {
                $('#carChartData').html('<div id="allbg"><div id="showLoading" class="showLoading" style="text-align:center;vertical-align: middle;">' +
 '<br /><p style="font-size: 12px;color:white">加载失败</></div></div>');
            }
        });
    },
    showCarList: function (status) {
        parent.car.initCarList();
    }
};

//图表
var myChart_1 = null;
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
        //data: ['07:00', '08:00', '09:00', '10:00', '11:00', '12:00', '13:00', '14:00', '15:00', '16:00', '17:00', '18:00', '19:00', '20:00'],
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
    color: ["#29f02e", "#a5a5a5", "#ed2929", common.changeRGBToColor("rgb(254,125,20)")
    , common.changeRGBToColor("rgb(232,230,38)"), common.changeRGBToColor("rgb(243,33,53)")],
    series: [
        {
            name: "今日在线",
            symbol: "emptyCircle",
            type: 'line',
            //stack: '总量',
            //data: [10, 11, 12, 13, 14, 15, 16, 11, 11, 11, 11, 11, 11, 11]
        }
        ,
        {
            name: "今日离线",
            symbol: "emptyCircle",
            type: 'line',
            //stack: 'aaa',
            //data: [13, 14, 15, 16, 17, 18, 19, 11, 11, 11, 11, 11, 11, 11]
        }
        ,
        {
            name: "今日报警",
            symbol: "emptyCircle",
            type: 'line',
            // stack: '总量',
            //data: [15, 16, 17, 18, 19, 20, 21, 11, 11, 11, 11, 11, 11, 11]
        }
    ]
};
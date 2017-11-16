
$(function () {
    eventZGM.init();
});
document.body.onresize = function () {
    eventZGM.init();
}

var eventZGM = {
    apiconfig: null,
    changeBodySize: function () {

        var tbWidth = document.body.clientWidth;
        // $("#eventChartTD").css("width", tbWidth - 400);
        $("#eventChart").css("width", tbWidth - 435);
    },
    init: function () {
        this.apiconfig = parent.globalConfig.apiconfig;
        this.changeBodySize();
        this.loadCharData();
        this.loadEventClassify();
        //this.loadTodayEventCount();

    }, //获取今日案件来源统计总数
    loadEventClassify: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: parent.globalConfig.apiconfig + "/api/Task/GetSourceStatSum?startTime=&endTime=",
            data: {},
            dataType: "json",
            success: function (result) {
                for (var i = 0; i < result.length; i++) {
                    var sourceId = result[i].SourceId;
                    if ($("label:contains('" + result[i].SourceName + "')+label").length > 0) {
                        $("label:contains('" + result[i].SourceName + "')+label")[0].textContent = result[i].Sum;
                        $("label:contains('" + result[i].SourceName + "')+label")[0].parentElement.setAttribute("onclick", "eventZGM.showEventList(" + result[i].SourceId + ");");
                    }
                }
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    loadCharData: function () {
        var jrsbCount = 0;
        var jrbjCount = 0;
        var cswblCount = 0;
        var jrsb = [];
        var jrbj = [];
        var cswbl = [];
        var times = [];
        $('#eventChart').html('<div id="allbg"><div id="showLoading" class="showLoading" style="text-align:center;"><img src="/images/list/loading2.gif" style="width: 20px;margin-top:10px; height: 20px" />' +
     '<br /><p style="font-size: 12px;color:white">正在加载中...</></div></div>');

        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/Task/GetTasksStatSub?startTime=&several=&hours=",
            dataType: "json",
            success: function (result) {
               
                if (result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        times.push(result[i].StatTime.substr(11, 5));

                        jrsb.push(result[i].ReportCount);
                        if (result[i].ReportCount != null) {
                            jrsbCount = result[i].ReportCount == null ? 0 : result[i].ReportCount;
                        }

                        jrbj.push(result[i].ClosedCount);
                        if (result[i].ClosedCount != null) {
                            jrbjCount = result[i].ClosedCount == null ? 0 : result[i].ClosedCount;
                        }
                        cswbl.push(result[i].OverdueCount);
                        if (result[i].OverdueCount != null) {
                            cswblCount = result[i].OverdueCount == null ? 0 : result[i].OverdueCount;
                        }
                    }
                    myChart_1 = echarts.init(document.getElementById('eventChart'));
                    option.series[0].data = jrsb;
                    option.series[1].data = jrbj;
                    option.series[2].data = cswbl;
                    option.xAxis.data = times;
                    //y轴间距控制
                    var temp = [];
                    temp.push(Math.max.apply(Math, jrsb));
                    temp.push(Math.max.apply(Math, jrbj));
                    temp.push(Math.max.apply(Math, cswbl));
                    var maxNum = Math.max.apply(Math, temp);
                    if (maxNum == 0) {
                        maxNum = 1;
                    }
                    option.yAxis.min = 0;                    
                    option.yAxis.interval = Math.ceil(maxNum / 2);
                    option.yAxis.max = option.yAxis.interval*2;
                    myChart_1.setOption(option);
                }
                else {
                    $('#eventChart').html('<div id="allbg"><div id="showLoading" class="showLoading" style="text-align:center;vertical-align: middle;">' +
'<br /><p style="font-size: 12px;color:white">没有相关数据！</></div></div>');
                }
                eventZGM.loadTodayEventCount(jrsbCount, jrbjCount, cswblCount);
            },
            error: function (errorMsg) {
                $('#carChartData').html('<div id="allbg"><div id="showLoading" class="showLoading" style="text-align:center;vertical-align: middle;">' +
 '<br /><p style="font-size: 12px;color:white">加载失败</></div></div>');
            }
        });
    },
    loadTodayEventCount: function (jrsbCount, jrbjCount, cswblCount) {
        $("#jrsbNum").html(jrsbCount);
        $("#jrjaNum").html(jrbjCount);
        $("#cswblNum").html(cswblCount);
    },
    showEventList: function (sourceId) {
        parent.event.initEventList(sourceId);
    }
}
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
        //data: ['07:00', '08:00', '09:00', '10:00', '11:00', '12:00', '13:00'],
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
    color: ["#0ce239", "#f39822", "#eb3434", common.changeRGBToColor("rgb(254,125,20)")
    , common.changeRGBToColor("rgb(232,230,38)"), common.changeRGBToColor("rgb(243,33,53)")],
    series: [
        {
            name: "今日上报",
            symbol: "circle",
            type: 'line',
            //stack: '总量',
            //data: [10, 11, 12, 13, 14, 15]
        }
        ,
        {
            name: "今日结案",
            symbol: "rect",
            type: 'line',
            //stack: 'aaa',
            //data: [13, 14, 15, 16, 17, 18]
        }
        ,
        {
            name: "超时未办理",
            symbol: "circle",
            type: 'line',
            // stack: '总量',
            // data: [15, 16, 17, 18, 19, 20, 21]
        }
        //,
        //{
        //    name: "44",
        //    symbol: "circle",
        //    type: 'line',
        //    //stack: '总量',
        //    //data: [17, 18, 19, 20, 21, 22, 23]
        //}
    ]
};
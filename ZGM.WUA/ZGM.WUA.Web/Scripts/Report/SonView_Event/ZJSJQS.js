$(function () {
    ZJSJQS.init()
});
//当页面改变大小的时候，重置菜单大小
document.body.onresize = function () {
    ZJSJQS.resize();
}

var ZJSJQS = {
    apiconfig:"",
    dateType:1,
    init: function () { this.tabClick(); this.resize(); },
    resize: function () {
        //ZJSJQS.init();
        this.apiconfig = parent.parent.globalConfig.apiconfig;
        var vh = document.body.clientHeight;
        var bh = $(".top").height();

        $(".content").css("height", vh - bh);
        this.chartLoad();
    },
    tabClick: function () {
        $(".tab").each(function (i) {
            $(this).click(function () {
                ZJSJQS.dateType = i + 1;
                if (i == 0) {
                    $(this).addClass("tab_left_selected").siblings().removeClass("tab_center_selected").removeClass("tab_right_selected");
                }
                else if (i == 1) {
                    $(this).addClass("tab_center_selected").siblings().removeClass("tab_left_selected").removeClass("tab_right_selected");
                }
                else if (i == 2) {
                    $(this).addClass("tab_right_selected").siblings().removeClass("tab_left_selected").removeClass("tab_center_selected");
                }
                ZJSJQS.chartLoad();
            });
        })
    },
    changeXAxis: function () {
       
        var XAxisData = [];
        switch (this.dateType) {
            case 1:
                for (var i = 6; i >= 0; i--) {
                    var today = new Date();
                    today.setDate(today.getDate() - i);
                    var month = today.getMonth() + 1;
                    var day = today.getDate();
                    if (month < 10) {
                        month = "0" + month;
                    }
                    if (day < 10) {
                        day = "0" + day;
                    }
                    XAxisData.push(month + "-" + day);
                    console.log(month + "-" + day );
                }
                break;
            case 2:
                for (var i = 6; i >= 0; i--) {
                    var today = new Date();
                    today.setMonth(today.getMonth() - i);
                    var year = today.getFullYear();
                    var month = today.getMonth() + 1;
                    if (month < 10) {
                        month = "0" + month;
                    }                  
                    XAxisData.push(year + "-" + month);
                    console.log(year + "-" + month);
                }
                break;
            case 3:
                for (var i = 6; i >= 0; i--) {
                    var today = new Date();
                    today.setFullYear(today.getFullYear() - i);
                    var year = today.getFullYear();
                    //var day = today.getDate();
                    XAxisData.push(year + "年");
                    console.log(year + "年");
                }
                break;
        }
        return XAxisData;
    },
    chartLoad: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/EventReport/GetEventTrendsList",
            data: { NYR: ZJSJQS.dateType },
            dataType: "json",
            success: function (result) {
                option.xAxis[0].data = [];
                for (var k = 0; k < option.series.length;k++){
                    option.series[k].data = [0,0,0,0,0,0,0];
                }
                if (result != null && result.length > 0) {
                    option.xAxis[0].data = ZJSJQS.changeXAxis();
                    for (var i = 0; i < result.length; i++) {
                        for (var j = 0; j < option.xAxis[0].data.length;j++){                           
                            if (option.xAxis[0].data[j] == result[i].DAYS) {
                                option.series[result[i].sourceid - 1].data[j] = result[i].counts;
                            }                                
                        }
                        //option.series[result[i].sourceid-1].data[i] = result[i].OverBorderCount;
                        //option.series[1].data[i] = result[i].OverLineCount;
                        //option.series[2].data[i] = result[i].OverTimeCount;
                    }                   
                }
                myChart_1 = echarts.init(document.getElementById('chartDiv'));
                myChart_1.setOption(option);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    
       
    }
}
var colors = [];
colors.push("#FF0000");
colors.push("#FFCC00");
colors.push("#99FF00");
colors.push("#00FFFF");
colors.push("#008B00");
colors.push("#FF00CC");
colors.push("#F1A7B6");
var myChart_1 = null;
var myChart_2 = null;
option = {
    tooltip: {
        trigger: 'axis',
        axisPointer: {
            lineStyle: {
                color: '#3cf5f3',
                width: 2
            }
        }
    },
    grid: {
        left: '1%',
        right: '4%',
        bottom: '0%',
        top: '15%',
        containLabel: true,
        backgroundColor: "transparent",
        borderColor: "transparent",
        show: true,
    },
    color: colors,//['#bf4754', '#93a235', '#f4cc20'],
    legend: {
        selectedMode: true,
        show: true,
        textStyle: {
            fontFamily: 'Microsoft YaHei',
            fontSize: 16,
            color: '#c6c7c6',
            fontStyle: 'normal',
            fontWeight: 'bolder',
        },
        itemWidth: 55,
        data: ['智慧城管', '队员巡查发现', '监控发现', '群众热线投诉', '社区上报', '领导巡查发现']
    },
    xAxis: [
        {
            nameTextStyle: { color: "#a4a6ac", fontSize: 8 },
            type: 'category',
            boundaryGap: false,
            splitLine: {
                show: false
            },
            axisLabel: {
                textStyle: {
                    fontFamily: 'Microsoft YaHei',
                    fontSize: 14,
                    color: '#fff',
                },
            },
            axisTick: {
                lineStyle: {
                    color: '#32a1a2'
                }
            },
            axisLine: {
                lineStyle: {
                    color: '#32a1a2'
                }
            },
            data: []
        }
    ],
    yAxis: {
        nameTextStyle: { color: "#a4a6ac", fontSize: 8 },
        type: 'value',
        splitLine: {
            show: true,
            lineStyle: {
                color: '#3b4459',
                type: 'dashed',
                width: 0.5,
                shadowColor: 'rgba(0,0,0,0)',
                shadowBlur: 5,
                shadowOffsetX: 3,
                shadowOffsetY: 3,
            },
        },
        axisTick: {
            lineStyle: {
                color: '#32a1a2'
            }
        },
        axisLine: {
            lineStyle: {
                color: '#32a1a2'
            }
        },
        axisLabel: {
            textStyle: {
                fontFamily: 'Microsoft YaHei',
                fontSize: 14,
                color: '#fff',
            }
        }
    },//['智慧城管', '队员巡查发现', '监控发现', '群众热线投诉', '社区上报', '领导巡查发现']
    series: [
        {
            name: '智慧城管',
            type: 'line',
            //stack: '总量',
            symbol: 'emptyCircle',
            symbolSize: 8,
            data: [
                0, 0, 0, 0, 0,0, 0
            ],
            markPoint: {
                data: [
                    { type: 'max', name: '最大值' },
                    { type: 'min', name: '最小值' },
                ]
            },
            markLine: {
                data: [
                    { type: 'average', name: '平均值' }
                ]
            }
        },
         {
             name: '队员巡查发现',
             type: 'line',
             //stack: '总量',
             symbol: 'emptyCircle',
             symbolSize: 8,
             data: [
                 0, 0, 0, 0, 0, 0, 0
             ],
             markPoint: {
                 data: [
                     { type: 'max', name: '最大值' },
                     { type: 'min', name: '最小值' },
                 ]
             },
             markLine: {
                 data: [
                     { type: 'average', name: '平均值' }
                 ]
             }
         },       
        {
            name: '监控发现',
            type: 'line',
            symbol: 'emptyCircle',
            symbolSize: 8,
            data: [
                0, 0, 0, 0, 0, 0, 0
            ], markPoint: {
                data: [
                    { type: 'max', name: '最大值' },
                    { type: 'min', name: '最小值' },
                ]
            },
            markLine: {
                data: [
                    { type: 'average', name: '平均值' }
                ]
            }
        },
        {
            name: '群众热线投诉',
            type: 'line',
            symbol: 'emptyCircle',
            symbolSize: 8,
            data: [
                0, 0, 0, 0, 0, 0, 0
            ], markPoint: {
                data: [
                    { type: 'max', name: '最大值' },
                    { type: 'min', name: '最小值' },
                ]
            },
            markLine: {
                data: [
                    { type: 'average', name: '平均值' }
                ]
            }
        },
        {
            name: '社区上报',
            type: 'line',
            symbol: 'emptyCircle',
            symbolSize: 8,
            data: [
                0, 0, 0, 0, 0, 0, 0
            ], markPoint: {
                data: [
                    { type: 'max', name: '最大值' },
                    { type: 'min', name: '最小值' },
                ]
            },
            markLine: {
                data: [
                    { type: 'average', name: '平均值' }
                ]
            }
        },
        {
            name: '领导巡查发现',
            type: 'line',
            symbol: 'emptyCircle',
            symbolSize: 8,
            data: [
                0, 0, 0, 0, 0, 0, 0
            ], markPoint: {
                data: [
                    { type: 'max', name: '最大值' },
                    { type: 'min', name: '最小值' },
                ]
            },
            markLine: {
                data: [
                    { type: 'average', name: '平均值' }
                ]
            }
        }
    ]
};
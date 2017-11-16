$(function () {
    GLSJQS.init();
});
//当页面改变大小的时候，重置菜单大小
document.body.onresize = function () {
    GLSJQS.resize();
}

var GLSJQS = {
    dataType: 1,
    apiconfig: "",
    init: function () { this.tabClick(); this.resize(); },
    resize: function () {
        //var vh = document.body.clientHeight;
        //var bh = $(".top").height();

        //$(".content").css("height", vh - bh);
        //this.apiconfig = parent.parent.globalConfig.apiconfig;
        //this.chartLoad();

        var vh = document.body.clientHeight;
        var bh = $(".top_RYBJ").height();

        $(".content_1").css("height", (vh - bh) * 0.4);
        $(".content_2").css("height", (vh - bh) * 0.6);

        this.apiconfig = parent.parent.globalConfig.apiconfig;
        this.chartLoad();
    },
    tabClick: function () {
        $(".tab").each(function (i) {
            $(this).click(function () {
                if (i == 0) {
                    $(this).addClass("tab_left_selected").siblings().removeClass("tab_center_selected").removeClass("tab_right_selected");
                }
                else if (i == 1) {
                    $(this).addClass("tab_center_selected").siblings().removeClass("tab_left_selected").removeClass("tab_right_selected");
                }
                else if (i == 2) {
                    $(this).addClass("tab_right_selected").siblings().removeClass("tab_left_selected").removeClass("tab_center_selected");
                }
                GLSJQS.dataType = i + 1;
                GLSJQS.chartLoad();
            });
        })
    },
    chartLoad: function () {
        //myChart_1 = null;
        //myChart_1 = echarts.init(document.getElementById('chartDiv'));
        //myChart_1.setOption(option);
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/EventReport/GetTrendList",
            data: { nyr: GLSJQS.dataType },
            dataType: "json",
            success: function (result) {
                // 1	智慧城管  2	队员巡查发现  3	监控发现  4	群众热线投诉  5	社区上报  6	领导巡查发现
                option.series[0].data = [0, 0, 0, 0, 0, 0];
                option.series[1].data = [0, 0, 0, 0, 0, 0];
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        option.series[0].data[result[i].Source - 1] = result[i].NumberOfReported;
                        option.series[1].data[result[i].Source - 1] = result[i].CasesSettled;
                    }
                }
                myChart_1 = echarts.init(document.getElementById('bar_chartDiv'));
                myChart_1.setOption(option);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });

        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/EventReport/GetSourceAnalysisList",
            data: { nyr: GLSJQS.dataType },
            dataType: "json",
            success: function (result) {
                // 1	智慧城管  2	队员巡查发现  3	监控发现  4	群众热线投诉  5	社区上报  6	领导巡查发现
                option2.series[0].data = [
                { value: 0, name: '智慧城管  (0)' },
                { value: 0, name: '队员巡查发现  (0)' },
                { value: 0, name: '监控发现  (0)' },
                { value: 0, name: '群众热线投诉  (0)' },
                { value: 0, name: '社区上报  (0)' },
                { value: 0, name: '领导巡查发现  (0)' }
                ];
                option2.legend.data = ['智慧城管  (0)', '队员巡查发现  (0)', '监控发现  (0)', '群众热线投诉  (0)', '社区上报  (0)', '领导巡查发现  (0)'];
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        var format = result[i].SourceName + "  (" + result[i].zfsj_Count + ")";
                        option2.series[0].data[result[i].Source - 1].value = result[i].zfsj_Count;
                        option2.series[0].data[result[i].Source - 1].name = format;
                        option2.legend.data[result[i].Source - 1] = format;
                    }
                }
                myChart_2 = echarts.init(document.getElementById('pie_chartDiv'));
                myChart_2.setOption(option2);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    }
}

var myChart_1 = null;
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
        top: '10%',
        containLabel: true,
        backgroundColor: "transparent",
        borderColor: "transparent",
        show: true,
    },
    legend: {
        selectedMode: true,
        show: true,
        textStyle: {
            fontFamily: 'Microsoft YaHei',
            fontSize: 12,
            color: '#a4a7ae',
            fontStyle: 'normal',
            fontWeight: 'normal',
        },
        x: 'right',
        padding: [0, 50, 0, 0],
        y: 'top',
        data: ['上报数', '结案数']
    },
    xAxis: [
        {
            type: 'category',
            name: '来源',
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
            //1	智慧城管  2	队员巡查发现  3	监控发现  4	群众热线投诉  5	社区上报  6	领导巡查发现
            data: ['智慧城管', '队员巡查发现', '监控发现', '群众热线投诉', '社区上报', '领导巡查发现']
        }
    ],
    yAxis: [
        {
            type: 'value',
            name: '个数',
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
                textStyle: {     fontFamily: 'Microsoft YaHei',
                    fontSize: 14,
                    color: '#fff', }
            }
        }
    ],

    series: [
        {
            name: '上报数',
            type: 'bar',
            itemStyle: {
                normal: {
                    color: "#697be6",
                    barBorderColor: '#697be6',
                    barBorderWidth: 5,
                    barBorderRadius: 2
                }
            },
            barWidth: 20,
            data: [
                0, 0,0,0, 0, 0
            ]

        },
        {
            name: '结案数',
            type: 'bar',
            barWidth: 40,                   // 系列级个性化，柱形宽度
            itemStyle: {
                normal: {
                    color: "#ff0000",
                    barBorderColor: '#ff6347',
                    barBorderWidth: 5,
                    barBorderRadius: 2
                }
            },
            barWidth: 20,
            data: [0, 0, 0, 0, 0, 0]
        }
    ]
};

var myChart_2 = null;
option2 = {
    tooltip: {
        trigger: 'item',
        formatter: function (param) {
            return param.seriesName + " <br/>" + param.name.split(' ')[0] + " : " + param.value + " (" + param.percent + "%)";
        }
    },
    legend: {
        orient: 'vertical',
        x: 'right',
        y: 'center',
        padding: [0, 250, 0, 0],
        selectedMode: true,
        itemGap: 35,
        textStyle: {
            fontFamily: 'Microsoft YaHei',
            fontSize: 16,
            color: '#ffffff',
            fontStyle: 'normal',
            fontWeight: 'normal',
        },
        data: ['智慧城管  (0)', '队员巡查发现  (0)', '监控发现  (0)', '群众热线投诉  (0)', '社区上报  (0)', '领导巡查发现  (0)']
    },
    color: ['#e90b00', '#eb6100', '#feba4b', '#0a7c25', '#00a0ea', '#af000d'],
    series: [
        {
            name: '事件来源',
            type: 'pie',
            //radius: '75%',
            center: ['30%', '52%'],
            radius: ['40%', '75%'],
            //center: ['40%', '55%'],
            label: {
                normal: {
                    //formatter: function (param) {
                    //    return param.name.split(' ')[0];
                    //}
                }
            },
            itemStyle: {
                normal: {
                    label: {
                        textStyle: {
                            color: '#ffffff',
                            fontFamily: 'Microsoft YaHei',
                            fontSize: 16,
                            fontStyle: 'normal',
                            fontWeight: 'normal',
                        }
                    }
                }
            },
            data: [
                //'智慧城管', '队员巡查发现', '监控发现', '群众热线投诉', '社区上报', '领导巡查发现'
                { value: 0, name: '智慧城管  (0)' },
                { value: 0, name: '队员巡查发现  (0)' },
                { value: 0, name: '监控发现  (0)' },
                { value: 0, name: '群众热线投诉  (0)' },
                { value: 0, name: '社区上报  (0)' },
                { value: 0, name: '领导巡查发现  (0)' }
            ]
        }
    ]
};
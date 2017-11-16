$(function () {
    SJLYFX.init();
    SJNRDWT.init();
    GLSJQS.init();
});
//当页面改变大小的时候，重置菜单大小
document.body.onresize = function () {
    SJLYFX.resize();
    SJNRDWT.resize();
    GLSJQS.resize();
}

//事件来源分析
var SJLYFX = {
    dataType: 3,
    init: function () { this.tabClick(); this.resize(); },
    resize: function () {
        var vh = document.getElementById('aaa').clientHeight;
        var bh = $(".top").height();
        $(".content1").css("height", vh - bh - 40);
        this.chartLoad(1);
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
                SJLYFX.dataType = i + 1;
                SJLYFX.chartLoad();
            });
        })
    },
    chartLoad: function (i) {
        //myChart_1 = echarts.init(document.getElementById('chartDiv1'));
        //myChart_1.setOption(option1);
        $.ajax({
            type: "post",
            async: true,
            url: "/HomeSystem/GetSourceAnalysisList",
            data: { NYR: SJLYFX.dataType },
            dataType: "json",
            success: function (result) {
                // 1	智慧城管  2	队员巡查发现  3	监控发现  4	群众热线投诉  5	社区上报  6	领导巡查发现
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        option1.series[0].data[result[i].Source - 1].value = result[i].zfsj_Count;
                    }
                }
                myChart_1 = echarts.init(document.getElementById('chartDiv1'));
                myChart_1.setOption(option1);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    }
}

var myChart_1 = null;
option1 = {
    tooltip: {
        trigger: 'item',
        formatter: "{a} <br/>{b} : {c} ({d}%)"
    },
    legend: {
        orient: 'vertical',
        x: 'right',
        y: 'center',
        padding: [0, 50, 0, 0],
        selectedMode: true,
        itemGap: 20,
        textStyle: {
            fontFamily: 'Microsoft YaHei',
            fontSize: 12,
            color: '#333',
            fontStyle: 'normal',
            fontWeight: 'normal',
        },
        data: ['智慧城管', '队员巡查发现', '监控发现', '群众热线投诉', '社区上报', '领导巡查发现']
    },
    color: ['#e90b00', '#eb6100', '#feba4b', '#0a7c25', '#00a0ea', '#af000d'],
    series: [
        {
            name: '事件来源',
            type: 'pie',
            radius: ['40%', '70%'],
            center: ['30%', '55%'],

            itemStyle: {
                normal: {
                    label: {
                        textStyle: {
                            color: '#333',
                            fontFamily: 'Microsoft YaHei',
                            fontSize: 12,
                            fontStyle: 'normal',
                            fontWeight: 'normal',
                        }
                    }
                }
            },
            data: [

                //'智慧城管', '队员巡查发现', '监控发现', '群众热线投诉', '社区上报', '领导巡查发现'
                { value: 0, name: '智慧城管' },
                { value: 0, name: '队员巡查发现' },
                { value: 0, name: '监控发现' },
                { value: 0, name: '群众热线投诉' },
                { value: 0, name: '社区上报' },
                { value: 0, name: '领导巡查发现' }
            ],
            itemStyle: {
                normal: {
                    label: {
                        show: true,
                        formatter: '{c} ({d}%)'
                    },
                    labelLine: { show: true }
                }
            }
        }
    ]
};






//事件难热点统计图
var SJNRDWT = {
    dataType: 3,
    init: function () { this.tabClick(); this.resize(); },
    resize: function () {
        var vh = document.getElementById("aaa").clientHeight;
        var bh = $(".top").height();

        $(".content").css("height", vh - bh - 40);
        this.chartLoad(1);
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
            });
        })
    },
    chartLoad: function (i) {
        //myChart_1 = null;
        //myChart_1 = echarts.init(document.getElementById('chartDiv'));
        //myChart_1.setOption(option);
        $.ajax({
            type: "GET",
            async: true,
            url: "/HomeSystem/GetHardHeatMapList",
            data: { nyr: SJNRDWT.dataType },
            dataType: "json",
            success: function (result) {
                // 1	智慧城管  2	队员巡查发现  3	监控发现  4	群众热线投诉  5	社区上报  6	领导巡查发现
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        option.series[result[i].Source - 1].data[SJNRDWT.changeToIndex(result[i].BClassName)] = result[i].zfsj_Count;
                    }
                }
                myChart_1 = echarts.init(document.getElementById('chartDiv'));
                myChart_1.setOption(option);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    changeToIndex: function (ClassName) {
        //'市容','道路交通', '工商行政', '城乡规划' ,'内河管理' ,'宣传广告' ,'环境保护', '其他'
        switch (ClassName) {
            case "市容": return 0;
            case "道路交通": return 1;
            case "工商行政": return 2;
            case "城乡规划": return 3;
            case "内河管理": return 4;
            case "宣传广告": return 5;
            case "环境保护": return 6;
            case "其他": return 7;
        }
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
        left: '2%',
        right: '8%',
        bottom: '4%',
        top: '23%',
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
            color: '#333',
            fontStyle: 'normal',
            fontWeight: 'normal',
        },
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
                textStyle: { color: "#333" },
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
            data: ['市容', '道路交通', '工商行政', '城乡规划', '内河管理', '宣传广告', '环境保护', '其他']
        }
    ],
    //['#bf4754', '#93a235', '#f4cc20']
    color: colors,
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
        axisLabel: { textStyle: { color: "#a4a6ac" } }
    },
    series: [
        {
            name: '智慧城管',
            type: 'line',
           // stack: '总量',
            symbol: 'emptyCircle',
            symbolSize: 8,
            data: [
               0, 0, 0, 0, 0, 0, 0, 0
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
        //'智慧城管', '队员巡查发现', '监控发现', '群众热线投诉', '社区上报', '领导巡查发现'
        {
            name: '队员巡查发现',
            type: 'line',
            // stack: '总量',
            symbol: 'emptyCircle',     // 系列级个性化拐点图形
            symbolSize: 8,
            data: [
                 0, 0, 0, 0, 0, 0, 0, 0
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
 0, 0, 0, 0, 0, 0, 0, 0
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
                 0, 0, 0, 0, 0, 0, 0, 0
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
                  0, 0, 0, 0, 0, 0, 0, 0
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
                   0, 0, 0, 0, 0, 0, 0, 0
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



//各类事件趋势图
var GLSJQS = {
    dataType: 3,
    init: function () { this.tabClick(); this.resize(); },
    resize: function () {
        var vh = document.getElementById('aaa').clientHeight;
        var bh = $(".top").height();

        $(".content2").css("height", vh - bh - 40);
        this.chartLoad(1);
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
            });
        })
    },
    chartLoad: function (i) {
        //myChart_2 = null;
        //myChart_2 = echarts.init(document.getElementById('chartDiv2'));
        //myChart_2.setOption(option2);
        $.ajax({
            type: "GET",
            async: true,
            url: "/HomeSystem/GetTrendList",
            data: { nyr: GLSJQS.dataType },
            dataType: "json",
            success: function (result) {
                // 1	智慧城管  2	队员巡查发现  3	监控发现  4	群众热线投诉  5	社区上报  6	领导巡查发现
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        option2.series[0].data[result[i].Source - 1] = result[i].NumberOfReported;
                        option2.series[1].data[result[i].Source - 1] = result[i].CasesSettled;
                    }
                }
                myChart_1 = echarts.init(document.getElementById('chartDiv2'));
                myChart_1.setOption(option2);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });

    }
}

//var myChart_1 = null;
option2 = {
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
        left: '2%',
        right: '4%',
        bottom: '3%',
        top: '23%',
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
            color: '#333',
            fontStyle: 'normal',
            fontWeight: 'normal',
        },
        x: 'right',
        padding: [10, 50, 0, 0],
        y: 'top',
        data: ['上报数', '结案数']
    },
    xAxis: [
        {
            type: 'category',
            splitLine: {
                show: false
            },
            axisLabel: {
                textStyle: { color: "#333" },
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
                textStyle: { color: "#a4a6ac" }
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
                0, 0, 0, 0, 0, 0
            ], itemStyle: {
                normal: {
                    label: {
                        show: true,
                        formatter: '{c}'
                    },
                    labelLine: { show: true }
                }
            }

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
            data: [0, 0, 0, 0, 0, 0],
            itemStyle: {
                normal: {
                    label: {
                        show: true,
                        formatter: '{c}'
                    },
                    labelLine: { show: true }
                }
            }

        }
    ]
};
$(function () {
    RYBSXL.init();
});
//当页面改变大小的时候，重置菜单大小
document.body.onresize = function () {
    RYBSXL.resize();
}

var RYBSXL = {
    init: function () { this.tabClick(); this.resize(); },
    resize: function () {
        var vh = document.body.clientHeight;
        var bh = $(".top").height();

        $(".content").css("height", vh - bh);
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
        myChart_1 = echarts.init(document.getElementById('chartDiv'));
        myChart_1.setOption(option);

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
        data: ['智慧城管', '监控发现', '队员巡查发现']
    },
    color: ['#ff854c', '#fc492c', '#f3ac3b'],
    xAxis: [
        {
            nameTextStyle: { color: "#a4a6ac", fontSize: 14 },
            type: 'category',
            name:'日期',
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
            data: ['5月1号', '5月2号', '5月3号', '5月4号', '5月5号', '5月6号', '5月7号']
        }
    ],
    yAxis: {
        nameTextStyle: { color: "#a4a6ac", fontSize: 14 },
        type: 'value',
        name: '次数',
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
    },
    series: [
        {
            name: '智慧城管',
            type: 'line',
            stack: '总量',
            symbol: 'emptyCircle',
            symbolSize: 8,
            data: [
                120, 132, 301, 134, 90, 230, 210
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
            stack: '总量',
            symbol: 'emptyCircle',     // 系列级个性化拐点图形
            symbolSize: 8,
            data: [
                120, 82, 201, 134, 190, 230, 110
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
            symbol: 'emptyCircle',
            symbolSize: 8,
            data: [
                620, 732, 791, 743, 890, 930, 820
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
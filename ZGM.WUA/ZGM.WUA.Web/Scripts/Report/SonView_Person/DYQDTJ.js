$(function () {
    DYQDTJ.init();
});
//当页面改变大小的时候，重置菜单大小
document.body.onresize = function () {
    DYQDTJ.resize();
}

var DYQDTJ = {
    dataType: 0,
    apiconfig: "",
    init: function () { this.tabClick(); this.resize();},
    resize: function () {
        var vh = document.body.clientHeight;
        var bh = $(".top").height();

        $(".content").css("height", vh - bh);
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
                DYQDTJ.dataType = i;
                DYQDTJ.chartLoad();
            });
        })
    },
    chartLoad: function () {
        //myChart_1 = echarts.init(document.getElementById('chartDiv'));
        //myChart_1.setOption(option);
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/TJReport/GetUnitSign",
            data: { DateType: DYQDTJ.dataType },
            dataType: "json",
            success: function (result) {
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        option.xAxis[0].data[i] = result[i].UnitName;
                      
                        option.series[0].data[i] = result[i].SignCount
                        option.series[1].data[i] = result[i].UnSignCount
                    }
                    myChart_1 = echarts.init(document.getElementById('chartDiv'));
                    myChart_1.setOption(option);
                }
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
        padding: [0,50,0,0],
        y: 'top',
        data: ['正常', '不正常']
    },
    xAxis: [
        {
            type: 'category',
            name: '中队名',
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
            //'万达分队', '鄞南分队', '智慧分队', '长丰分队', '钟公庙分队', '机动分队', '中队部'
        }
    ],
    yAxis: [
        {
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
        }
    ],

    series: [
        {
            name: '正常',
            type: 'bar',
            itemStyle: {
                normal: {
                    color: "#4dcfd1",
                    barBorderColor: '#4dcfd1',
                    barBorderWidth: 5,
                    barBorderRadius: 2
                }
            },
            barWidth: 20,
            data: [
               
            ]
            // 420, 432,301,534, 390, 430, 520
        },
        {
            name: '不正常',
            type: 'bar',
            barWidth: 40,                   // 系列级个性化，柱形宽度
            itemStyle: {
                normal: {
                    color: "#fefe26",
                    barBorderColor: '#ff2626',
                    barBorderWidth: 5,
                    barBorderRadius: 2
                }
            },
            barWidth: 20,
            data: []
            //220, 232, 101, 234, 190, 330, 210
        }
    ]
};
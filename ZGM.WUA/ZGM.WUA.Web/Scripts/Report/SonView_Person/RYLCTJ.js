$(function () {
    RYLCTJ.init();
});
//当页面改变大小的时候，重置菜单大小
document.body.onresize = function () {
    RYLCTJ.resize();
}

var RYLCTJ = {
    dataType: 0,
    apiconfig:"",
    init: function () {
        this.apiconfig = parent.parent.globalConfig.apiconfig;
        this.tabClick();
        this.resize();
    },
    resize: function () {
        var vh = document.body.clientHeight;
        var bh = $(".top").height();

        $(".content").css("height", vh - bh);
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
                RYLCTJ.dataType = i ;
                RYLCTJ.chartLoad();
            });
        })
    },
    chartLoad: function () {
        
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/TJReport/GetPersonWalk",
            data: { DateType: RYLCTJ.dataType },
            dataType: "json",
            success: function (result) {
                if (result != null && result.length == 10) {
                    for (var i = 0; i < result.length; i++) {
                        option.xAxis[0].data[i] = result[i].UserName;
                        option.series[0].data[i].value = result[i].WalkCount;
                        myChart_1 = echarts.init(document.getElementById('chartDiv'));
                        myChart_1.setOption(option);
                    }
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
   
    xAxis: [
        {
            type: 'category',
            name: '队员名',
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
            data: ['1号队员', '2号队员', '3号队员', '4号队员', '5号队员', '6号队员', '7号队员', '8号队员', '9号队员', '10号队员']
        }
    ],
    yAxis: [
        {
            type: 'value',
            name: '里程',
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
            name: '汇总',
            type: 'bar',
            barWidth:28,
            stack: '总量',
            //barCategoryGap: '60%',
            label: {
                normal: {
                    show: true,
                    position: 'inside'
                }
            },
            data: [
                {
                    value: 100,itemStyle: {
                        normal: {
                            color: '#ff4754',
                            barBorderColor: '#ff4754',
                            barBorderWidth: 5,
                            barBorderRadius: 2
                        }
                    }
                },
                {
                    value: 95, itemStyle: {
                        normal: {
                            color: '#ff477b',
                            barBorderColor: '#ff477b',
                            barBorderWidth: 5,
                            barBorderRadius: 2
                        }
                    }
                },
                {
                    value: 90, itemStyle: {
                        normal: {
                            color: '#ff4799',
                            barBorderColor: '#ff4799',
                            barBorderWidth: 5,
                            barBorderRadius: 2
                        }
                    }
                },
                {
                    value: 85, itemStyle: {
                        normal: {
                            color: '#ff47d6',
                            barBorderColor: '#ff47d6',
                            barBorderWidth: 5,
                            barBorderRadius: 2
                        }
                    }
                },
                {
                    value: 80, itemStyle: {
                        normal: {
                            color: '#df47ff',
                            barBorderColor: '#df47ff',
                            barBorderWidth: 5,
                            barBorderRadius: 2
                        }
                    }
                },
                {
                    value: 70, itemStyle: {
                        normal: {
                            color: '#b847ff',
                            barBorderColor: '#b847ff',
                            barBorderWidth: 5,
                            barBorderRadius: 2
                        }
                    }
                },
                {
                    value: 60, itemStyle: {
                        normal: {
                            color: '#a247ff',
                            barBorderColor: '#a247ff',
                            barBorderWidth: 5,
                            barBorderRadius: 2
                        }
                    }
                },
                {
                    value: 55, itemStyle: {
                        normal: {
                            color: '#8847ff',
                            barBorderColor: '#8847ff',
                            barBorderWidth: 5,
                            barBorderRadius: 2
                        }
                    }
                },
                {
                    value: 45, itemStyle: {
                        normal: {
                            color: '#6547ff',
                            barBorderColor: '#6547ff',
                            barBorderWidth: 5,
                            barBorderRadius: 2
                        }
                    }
                },
                {
                    value: 40, itemStyle: {
                        normal: {
                            color: '#4765ff',
                            barBorderColor: '#4765ff',
                            barBorderWidth: 5,
                            barBorderRadius: 2
                        }
                    }
                }]
        }
    ]
};
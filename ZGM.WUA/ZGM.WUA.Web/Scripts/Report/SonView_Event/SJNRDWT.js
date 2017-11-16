$(function () {
    SJNRDWT.init();
});
//当页面改变大小的时候，重置菜单大小
document.body.onresize = function () {
    SJNRDWT.resize();
}

var SJNRDWT = {
    dataType: 1,
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
                if(i==0){
                    $(this).addClass("tab_left_selected").siblings().removeClass("tab_center_selected").removeClass("tab_right_selected");
                }
                else if (i == 1) {
                    $(this).addClass("tab_center_selected").siblings().removeClass("tab_left_selected").removeClass("tab_right_selected");
                }
                else if (i == 2) {
                    $(this).addClass("tab_right_selected").siblings().removeClass("tab_left_selected").removeClass("tab_center_selected");
                }
                SJNRDWT.dataType = i + 1;
                SJNRDWT.chartLoad();
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
            url: this.apiconfig + "/api/EventReport/GetHardHeatMapList",
            data: { nyr: SJNRDWT.dataType },
            dataType: "json",
            success: function (result) {
                // 1	智慧城管  2	队员巡查发现  3	监控发现  4	群众热线投诉  5	社区上报  6	领导巡查发现
                option.series[0].data = [0, 0, 0, 0, 0, 0, 0, 0];
                option.series[1].data = [0, 0, 0, 0, 0, 0, 0, 0];
                option.series[2].data = [0, 0, 0, 0, 0, 0, 0, 0];
                option.series[3].data = [0, 0, 0, 0, 0, 0, 0, 0];
                option.series[4].data = [0, 0, 0, 0, 0, 0, 0, 0];
                option.series[5].data = [0, 0, 0, 0, 0, 0, 0, 0];
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
                width:2
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
    legend: {
        selectedMode:true,
        show:true,
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
            data: ['市容','道路交通', '工商行政', '城乡规划' ,'内河管理' ,'宣传广告' ,'环境保护', '其他']
        }
    ],
    //['#bf4754', '#93a235', '#f4cc20']
    color: colors,
    yAxis:{
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
    },
    series: [
        {
            name: '智慧城管',
            type: 'line',
            //stack: '总量',
            symbol: 'emptyCircle',
            symbolSize: 8,
            data: [
               0,0,0,0,0,0,0,0
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
            //stack: '总量',            
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
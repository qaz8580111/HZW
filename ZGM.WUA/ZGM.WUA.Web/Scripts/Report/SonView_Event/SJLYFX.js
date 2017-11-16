$(function () {
    SJLYFX.init();
});
//当页面改变大小的时候，重置菜单大小
document.body.onresize = function () {
    SJLYFX.resize();
}

var SJLYFX = {
    dataType: 1,
    apiconfig: "",
    init: function () { this.tabClick(); this.resize();  },
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
                SJLYFX.dataType = i + 1;
                SJLYFX.chartLoad();
            });
        })
    },
    chartLoad: function (i) {
        //myChart_1 = echarts.init(document.getElementById('chartDiv'));
        //myChart_1.setOption(option);
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/EventReport/GetSourceAnalysisList",
            data: { nyr: SJLYFX.dataType },
            dataType: "json",
            success: function (result) {
                // 1	智慧城管  2	队员巡查发现  3	监控发现  4	群众热线投诉  5	社区上报  6	领导巡查发现
                option.series[0].data = [
                { value: 0, name: '智慧城管  (0)' },
                { value: 0, name: '队员巡查发现  (0)' },
                { value: 0, name: '监控发现  (0)' },
                { value: 0, name: '群众热线投诉  (0)' },
                { value: 0, name: '社区上报  (0)' },
                { value: 0, name: '领导巡查发现  (0)' }
                ];
                option.legend.data = ['智慧城管  (0)', '队员巡查发现  (0)', '监控发现  (0)', '群众热线投诉  (0)', '社区上报  (0)', '领导巡查发现  (0)'];
                if (result != null && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        var format = result[i].SourceName + "  (" + result[i].zfsj_Count+")";
                        option.series[0].data[result[i].Source - 1].value = result[i].zfsj_Count;
                        option.series[0].data[result[i].Source - 1].name = format;
                        option.legend.data[result[i].Source - 1] = format;
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

var myChart_1 = null;
option = {
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
        itemGap: 25,
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
            radius: ['40%', '70%'],
            center: ['40%', '55%'],
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

var legenddata;
var option = null;

function getcaseLegend(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async: true,
        timeout: 10000,
        dataType: 'json',
        success: function (data, jqxhr, textStatus) {
            legenddata = data;
        }, error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    })
}
function getcaseLine(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async: true,
        timeout: 10000,
        dataType: 'json',
        success: function (data, jqxhr, textStatus) {
            // 指定图表的配置项和数据
            option = {
                tooltip: {
                    trigger: 'axis',
                    axisPointer: {
                        lineStyle: {
                            color: '#16B2B5',
                        }
                    }
                },
                legend: {
                    data: ['7天案件趋势'],
                    textStyle:
                    {
                        color: '#ffffff',
                    },
                    top: 30,
                    left: '20%',
                },
                grid: {
                    left: '3%',
                    right: '4%',
                    bottom: '3%',
                    containLabel: true
                },
                color: ['#ECA513', '#67EC13'],
                xAxis: [
                    {
                        type: 'category',
                        boundaryGap: false,
                        data: legenddata,
                        axisLine: {
                            lineStyle: {
                                color: '#16B2B5',
                            },
                        },
                        axisLabel: {
                            textStyle: {
                                color: '#fff',
                            }
                        }
                    }
                ],
                yAxis: [
                    {
                        type: 'value',
                        axisLine: {
                            lineStyle: {
                                color: '#16B2B5',
                            },
                        },
                        axisLabel: {
                            textStyle: {
                                color: '#fff',
                            }
                        }
                    }
                ],
                series: [
                    {
                        name: '7天案件趋势',
                        type: 'line',
                        stack: '总量',
                        data: data,
                    },
                ]
            };
            reloadajLinedata(option);
        }, error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    })
}

function reloadajLinedata(option) {
    // 基于准备好的dom，初始化echarts实例
    var myTrendChart = echarts.init(document.getElementById('rightTrendchart'));
    // 使用刚指定的配置项和数据显示图表。
    myTrendChart.setOption(option);
    // 基于准备好的dom，初始化echarts实例
    var myTrendChartaj = echarts.init(document.getElementById('rightTrendchartaj'));
    // 使用刚指定的配置项和数据显示图表。
    myTrendChartaj.setOption(option);
}


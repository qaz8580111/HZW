var option = null;
var xAxisdata = null;

function inityjdjLegend(ajaxurl) {
    $.ajax({
        url:ajaxurl,
        type: 'get',
        async: false,
        timeout:10000,
        dataType: 'json',
        success: function (data, jqxhr, textStatus) {
            xAxisdata = data;
        }, error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    })
}

function inityjdjdata(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async: false,
        timeout: 10000,
        dataType: 'json',
        success: function (data, jqxhr, textStatus) {
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
                    top: '25px',
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
                        data: xAxisdata,
                        axisLine: {
                            lineStyle: {
                                color: '#16B2B5',
                            },
                        },
                        axisLabel: {
                            textStyle: {
                                color: '#fff',
                            }
                        },
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
                        },
                        max: '100',
                    }
                ],
                series: [
                    {
                        name: '7天案件趋势',
                        type: 'line',
                        data: data,
                    }
                ]
            };
            reloadyjdjaj(option);
        }, error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    })
}

function reloadyjdjaj(option) {
    // 基于准备好的dom，初始化echarts实例
    var yjdjaj = echarts.init(document.getElementById('yjdjaj'));
    // 使用刚指定的配置项和数据显示图表。
    yjdjaj.setOption(option);
    // 基于准备好的dom，初始化echarts实例
    var yjdjajsy = echarts.init(document.getElementById('shouyeyjdjaj'));
    // 使用刚指定的配置项和数据显示图表。
    yjdjajsy.setOption(option);
}

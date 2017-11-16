var option = null;
var barLengend;

function casebarLegend(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async: true,
        timeout: 10000,
        dataType: 'json',
        success: function (data, jqxhr, textStatus) {
            barLengend = data;
        }, error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    })
}

function casebardata(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async: true,
        timeout: 10000,
        dataType: 'json',
        success: function (data, jqxhr, textStatus) {
            option = {
                tooltip: {},
                grid: {
                    left: '3%',
                    right: '4%',
                    bottom: '1%',
                    height: '118px',
                    containLabel: true
                },
                xAxis: {
                    color: ['#fffff'],
                    splitLine: {
                        show: false
                    },
                    axisLine: {
                        lineStyle: {
                            color: '#16B2B5'
                        },
                    },
                },

                yAxis: {
                    data: barLengend,
                    axisLine: {
                        lineStyle: {
                            color: '#16B2B5',
                        },
                    },
                    boundaryGap: ['5%', '5%']
                },

                textStyle: {
                    color: '#ffffff',
                    fontSize: 12
                },

                series: [
                     {
                         type: 'bar',
                         barWidth: 12,
                         itemStyle: {
                             normal: {
                                 color: new echarts.graphic.LinearGradient(
                                     0, 0, 0, 1,
                                     [
                                         { offset: 0, color: '#70F466' },
                                         { offset: 0.5, color: '#65F65A' },
                                         { offset: 1, color: '#4CF43F' }
                                     ]
                                 )
                             },
                             emphasis: {
                                 color: new echarts.graphic.LinearGradient(
                                     0, 0, 0, 1,
                                     [
                                         { offset: 0, color: '#70F466' },
                                         { offset: 0.7, color: '#65F65A' },
                                         { offset: 1, color: '#4CF43F' }
                                     ]
                                 )
                             }
                         },
                         data: data
                     }
                ]
            };
            reloadcasebar(option);
        }, error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    })
}

function reloadcasebar(option) {
    // 基于准备好的dom，初始化echarts实例
    var myChart = echarts.init(document.getElementById('ajthistogram'));
    myChart.setOption(option);
}

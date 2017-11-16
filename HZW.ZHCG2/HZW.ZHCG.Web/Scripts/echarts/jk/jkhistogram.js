var option = null;
var datavalue = null;
var dataName = null;

function getjkhisgramsName(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async: true,
        timeout: 10000,    //超时时间
        dataType: 'json',
        success: function (data, textStatus, jqxhr) {
            dataName = data;
        }, error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    });
}

function getjkhisgrams(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async: true,
        timeout: 10000,    //超时时间
        dataType: 'json',
        success: function (data, textStatus, jqxhr) {
            datavalue = data;
            // 指定图表的配置项和数据
            option = {
                tooltip:{},
                grid: {
                    left:'3%',
                    right: '4%',
                    bottom: '3%',
                    containLabel: true
                },
                xAxis: {
                    data: dataName,
                    axisLine: {
                        lineStyle: {
                            color: '#16B2B5',
                        },
                    },
                    axisLabel: {
                        textStyle: {
                            color: '#fff',
                        },
                        interval: 0,
                        margin: 2,
                    },
                    boundaryGap:['5%', '5%']

                },

                yAxis: {
                    color: ['#fffff'],
                    axisLine: {
                        lineStyle: {
                            color: '#16B2B5',
                        },
                    },
                    splitLine: {
                        show:true,
                        lineStyle: {
                            type: 'dotted',
                        }
                    },
                    axisLabel: {
                        textStyle: {
                            color: '#fff',
                        }

                    },
                },

                textStyle: {
                    color: '#ffffff',
                    fontSize:12
                },

                series: [
                     {
                         type:'bar',
                         barWidth:20,
                         itemStyle: {
                             normal: {
                                 color: new echarts.graphic.LinearGradient(
                                     0, 0, 0, 1,
                                     [
                                         { offset: 0, color: '#19A3E6' },
                                         { offset: 0.5, color: '#1CCBF1' },
                                         { offset: 1, color: '#28F0FA' }
                                     ]
                                 )
                             },
                             emphasis: {
                                 color: new echarts.graphic.LinearGradient(
                                     0, 0, 0, 1,
                                     [
                                         { offset: 0, color: '#19A3E6' },
                                         { offset: 0.7, color:'#1CCBF1' },
                                         { offset: 1, color: '#28F0FA' }
                                     ]
                                 )
                             }
                         },
                         data: datavalue,
                     }
                ]
            };
            reloadjkhistogram(option);
        }, error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    });
}

function reloadjkhistogram(option) {
    // 基于准备好的dom，初始化echarts实例
    var jkhistogram = echarts.init(document.getElementById('jkhistogram'));
    jkhistogram.setOption(option);
}
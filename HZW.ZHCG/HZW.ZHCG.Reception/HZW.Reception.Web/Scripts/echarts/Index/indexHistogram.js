// 基于准备好的dom，初始化echarts实例
var myChart = echarts.init(document.getElementById('lefthistogram'));
var data = [0, 20,30, 40, 20, 30, 80, 95];

// 指定图表的配置项和数据
var option = {
    tooltip: {},
    grid: {
        top: 50,
        height: 90
    },
    xAxis: {
        data: ["天数","1", "2", "3", "4", "5", "6", "7"],
        axisLine: {
            lineStyle: {
                color: '#ffffff',
            },
        },
        boundaryGap: ['5%', '5%']
    },

    yAxis: {
        color: ['#fffff'],
        axisLine: {
            lineStyle: {
                color: '#ffffff'
            },
        },
    },

    textStyle: {
        color: '#ffffff',
        fontSize: 12
    },

    series: [
         {
             type: 'bar',
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
                             { offset: 0.7, color: '#1CCBF1' },
                             { offset: 1, color: '#28F0FA' }
                         ]
                     )
                 }
             },
             data: data
         }
    ]
};
myChart.setOption(option);
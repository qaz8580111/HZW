// 基于准备好的dom，初始化echarts实例
var myTrendChart = echarts.init(document.getElementById('rightTrendchart'));

// 指定图表的配置项和数据
var optionOne = {
    tooltip: {
        trigger: 'axis'
    },
    legend: {
        data: ['门前环境', '越门经营'],
        textStyle:
        {
            color:'#ffffff',
        }
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
            data: ['天数', '1', '2', '3', '4', '5', '6', '7'],
            axisLine: {
                lineStyle: {
                    color: '#ffffff',
                },
            }
        }
    ],
    yAxis: [
        {
            type: 'value',
            axisLine: {
                lineStyle: {
                    color: '#ffffff',
                },
            }
        }
    ],
    series: [
        {
            name: '门前环境',
            type: 'line',
            stack: '总量',
            data: [0, 120, 132, 101, 134, 90, 230, 210]
        },
        {
            name: '越门经营',
            type: 'line',
            stack: '总量',
            data: [0, 220, 182, 191, 234, 290, 330, 310]
        }
    ]
};


// 使用刚指定的配置项和数据显示图表。
myTrendChart.setOption(optionOne);
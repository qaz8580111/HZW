// 基于准备好的dom，初始化echarts实例
var wrjPiecharts = echarts.init(document.getElementById('wrjPiecharts'));

// 指定图表的配置项和数据
var option = {
    tooltip: {
        trigger: 'item',
        formatter: "{a} <br/>{b}: {c} ({d}%)"
    },
    title: {
        subtext: '2',
        subtextStyle: {
            color: '#ffffff',
            fontStyle: 'normal',
            fontWeight: 'normal',
            fontFamily: 'digitalTableFont',
            fontSize: 25,
        },
        x: 'center',
        top: '10%',
    },
    series: [
        {
            name: '无人机数量',
            type: 'pie',
            radius: ['60%', '85%'],
            avoidLabelOverlap: true,
            label: {
                normal: {
                    show: false,
                    position: 'center'
                },
                emphasis: {
                    show: false,
                    textStyle: {
                        fontSize: '30',
                        fontWeight: 'bold',
                    }
                }
            },
            color: ["#69BF03", "#16DFE8", "#FE6721"],
            labelLine: {
                normal: {
                    show: false
                }
            },
            data: [
                {
                    value: 0, name: '行驶中',
                    itemStyle: {
                        normal: {
                            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                offset: 0, color: '#7BCD1C' // 0% 处的颜色
                            }, {
                                offset: 1, color: '#68BF03' // 100% 处的颜色
                            }], false)
                        },
                        emphasis: {
                            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                offset: 0, color: '#7BCD1C' // 0% 处的颜色
                            }, {
                                offset: 1, color: '#68BF03' // 100% 处的颜色
                            }], false)
                        }
                    }
                },
                {
                    value: 1, name: '检修中',
                    itemStyle: {
                        normal: {
                            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                offset: 0, color: '#16DFE8' // 0% 处的颜色
                            }, {
                                offset: 1, color: '#07D4DD' // 100% 处的颜色
                            }], false)
                        },
                        emphasis: {
                            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                offset: 0, color: '#16DFE8' // 0% 处的颜色
                            }, {
                                offset: 1, color: '#07D4DD' // 100% 处的颜色
                            }], false)
                        }
                    }
                },
                {
                    value: 1, name: '轮休中',
                    itemStyle: {
                        normal: {
                            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                offset: 0, color: '#FE6C23' // 0% 处的颜色
                            }, {
                                offset: 1, color: '#FE9F34' // 100% 处的颜色
                            }], false)
                        },
                        emphasis: {
                            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                offset: 0, color: '#FE6C23' // 0% 处的颜色
                            }, {
                                offset: 1, color: '#FE9F34' // 100% 处的颜色
                            }], false)
                        }
                    }
                }
            ]
        }
    ]
};

// 使用刚指定的配置项和数据显示图表。
wrjPiecharts.setOption(option);
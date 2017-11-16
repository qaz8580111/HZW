var option = null;
var subtext = null;
function getcameracount(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type:'get',
        async: false,
        timeout: 10000,
        dataType: 'json',
        success: function (data, textStatus, jqxhr) {
            subtext = data;
        }, error: function (textStatus, xhr) {
            console.log(textStatus);
        }
    });
}
// 指定图表的配置项和数据
function cameraPieData(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async: false,
        timeout: 10000, //超时时间
        dataType: 'json',
        success: function (data, textStatus, jqxhr) {
            var html = null;
            for (var i = 0; i < data.length; i++) {
                html += "<tr><td class='tdFontSizeColor'>" + data[i].name + "</td><td class='centerrightTabTd GreenColor'>" + data[i].value + "</td></tr>";
            }
            $("#centerrightTabCamera").html(html);
            option = {
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b}: {c} ({d}%)"
                },
                title: {
                    subtext: subtext,
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
                        name: '监控数量',
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
                        color: ["#FE9731", "#F6584E", "#B056F5", "#6DC208"],
                        labelLine: {
                            normal: {
                                show: false
                            }
                        },
                        data: data
                    }
                ]
            };
            reloadCameraPie(option);
        }, error: function (textStatus, jqxhr) {
            console.log(textStatus);
        }
    });
}

function reloadCameraPie(option) {
    // 基于准备好的dom，初始化echarts实例
    var jkPiecharts = echarts.init(document.getElementById('jkPiecharts'));
    // 使用刚指定的配置项和数据显示图表。
    jkPiecharts.setOption(option);
}
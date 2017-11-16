var option = null;
var count = null;
function gethwggcount(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async:false,
        timeout: 10000,
        dataType: 'json',
        success: function (data, jqxhr, textStatus) {
            count = data;
        }, error: function (xhr, textStatus) {
            console.log(textStatus)
        }
    });
}
function gethwggtypebypie(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async: false,
        timeout: 10000,
        dataType: 'json',
        success: function (data, jqxhr, textStatus) {
            var html = null;
            for (var i = 0; i < data.length; i++) {
                if (i == 0) {
                    html += "<tr><td class='tdFontSizeColor'>" + data[i].name + "</td><td class='centerrightTabTd orangeColor'>" + data[i].value + "</td></tr>";
                }
                if (i == 1) {
                    html += "<tr><td class='tdFontSizeColor'>" + data[i].name + "</td><td class='centerrightTabTd OxbloodRed'>"+data[i].value+"</td></tr>"
                }
                if (i == 2) {
                    html += "<tr><td class='tdFontSizeColor'>" + data[i].name + "</td><td class='centerrightTabTd Violet'>" + data[i].value + "</td></tr>";
                }
                if (i == 3) {
                    html += "<tr><td class='tdFontSizeColor'>" + data[i].name + "</td><td class='centerrightTabTd GreenColor'>" + data[i].value + "</td></tr>";
                }
            }
            $("#tabhwggtype").html(html);
            option = {
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b}: {c} ({d}%)"
                },
                title: {
                    subtext: count,
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
                        name: '广告数量',
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
                        data:data,
                    }
                ]
            };
            reloadhwggpie(option);
        }, error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    });
}


function reloadhwggpie(option) {
    // 基于准备好的dom，初始化echarts实例
    var hwgg = echarts.init(document.getElementById('hwggPie'));
    // 使用刚指定的配置项和数据显示图表。
    hwgg.setOption(option);
}

 

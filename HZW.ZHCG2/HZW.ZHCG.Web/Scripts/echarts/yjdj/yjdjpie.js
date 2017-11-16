var option = null;
function getyjdjpiedata(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async: true,
        timeout: 10000,    //超时时间
        dataType: 'json',
        success: function (data, textStatus, jqxhr) {
            var html = null;
            for (var i = 0; i < data.length; i++) {
                if (i == 0) {
                    html += "<tr><td class='tdFontSizeColor'>" + data[i].name + "</td><td class='tdFontSizeFamily tdColor'>" + data[i].value + "</td>";
                }
                if (i == 1) {
                    html += "<td class='tdFontSizeColor'>" + data[i].name + "</td><td class='tdFontSizeFamily tdColor'>" + data[i].value + "</td></tr>";
                }
                if (i == 2) {
                    html += "<tr><td class='tdFontSizeColor'>" + data[i].name + "</td><td class='tdFontSizeFamily tdColor'>" + data[i].value + "</td>";
                }
                if (i == 3) {
                    html += "<td class='tdFontSizeColor'>" + data[i].name + "</td><td class='tdFontSizeFamily tdColor'>" + data[i].value + "</td></tr>";
                }
                if (i == 4) {
                    html += "<tr><td class='tdFontSizeColor'>" + data[i].name + "</td><td class='tdFontSizeFamily tdColor'>" + data[i].value + "</td>";
                }
            }
           $("#yjdjtype").html(html);
           option = {
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b} : {c} ({d}%)"
                },
                series: [
                    {
                        name: '沿街店家类型',
                        type: 'pie',
                        radius: '55%',
                        center: ['50%', '60%'],
                        color: ['#EF2E25', '#6FC30B', '#2488BD', '#019B17', '#FE842B'],
                        data: data,
                        itemStyle: {
                            emphasis: {
                                shadowBlur: 10,
                                shadowOffsetX: 0,
                                shadowColor: 'rgba(0, 0, 0, 0.5)'
                            }
                        }
                    }
                ]
            };
           reloadyjdjPie(option);
        }, error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    });
}

function reloadyjdjPie(option) {
    // 基于准备好的dom，初始化echarts实例
    var yjdjpie = echarts.init(document.getElementById('yjdjpie'));
    // 使用刚指定的配置项和数据显示图表。
    yjdjpie.setOption(option);
}


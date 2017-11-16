function getCameraGrid(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async: true,
        timeout: 10000,//超时时间
        dataType: 'json',
        success: function (data, textStatus, jqxhr) {
            var html = null;
            var val = parseFloat(data.length);
            if (val % 2 == 0)
            {
                if (val == 2)
                {
                    for (var i = 0; i < data.length; i++)
                    {
                        html += "<tr><td class='tdFontSizeColor'>" + data[i].text + "<span class='lefttabTd'>"+data[i].value+"</span></td></tr>";
                    }
                }
                else
                {
                    var i = val / 2;
                    html += "<tr>";
                    for (var j = 0; j < i; j++)
                    {
                        html += "<td class='tdFontSizeColor'>" + data[j].text + "<span class='lefttabTd'>" + data[j].value + "</span></td>";
                    }
                    html += "</tr>";

                    html += "<tr>"
                    for (i; i < val; i++)
                    {
                        html += "<td class='tdFontSizeColor'>" + data[i].text + "<span class='lefttabTd'>" + data[i].value + "</span></td>";
                    }
                    html += "</tr>";
                }
            }
            else
            {
                if (val > 1)
                {
                    var numberone = parseFloat(val);
                    var numbertwo = parseFloat(1);
                    var sum = numberone + numbertwo;
                    var j = sum / 2;

                    html += "<tr>";
                    for (var m = 0; m < j; m++)
                    {
                        html += "<td class='tdFontSizeColor'>" + data[m].text + "<span class='lefttabTd'>" + data[m].value + "</span></td>";
                    }
                    html += "</tr>";

                    html += "<tr>"
                    for (j; j < data.length; j++)
                    {
                        html += "<td class='tdFontSizeColor'>" + data[j].text + "<span class='lefttabTd'>" + data[j].value + "</span></td>";
                    }
                    html += "</tr>";
                }
                else
                {
                    html += "<tr>";
                    for (var h = 0; h < val; h++)
                    {
                        html += "<td class='tdFontSizeColor'>" + data[h].text + "<span class='lefttabTd'>" + data[h].value + "</span></td>";
                    }
                    html += "</tr>";
                }
            }
            $("#camaraGridData").html(html);
        }, error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    });
}
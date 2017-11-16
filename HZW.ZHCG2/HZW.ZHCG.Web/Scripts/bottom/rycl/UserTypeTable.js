function getusertypeTable(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async: true,
        timeout: 10000,
        dataType: 'json',
        success: function (data, jqxhr, textStatus) {
            var html = null;
            var val = parseFloat(data.length);
            if (val % 2 == 0) {
                if (val == 2) {
                    for (var i = 0; i < data.length; i++) {
                        html += "<tr><td class='ryclFontSizeColor'>" + data[i].name + "<span class='centerrightTabTd orangeColor'>" + data[i].value + "</span></td></tr>";
                    }
                }
                else {
                    var i = val / 2;
                    html += "<tr>";
                    for (var j = 0; j < i; j++) {
                        html += "<td class='ryclFontSizeColor'>" + data[j].name + "<span class='centerrightTabTd orangeColor'>" + data[j].value + "</span></td>";
                    }
                    html += "</tr>";

                    html += "<tr>"
                    for (i; i < val; i++) {
                        html += "<td class='ryclFontSizeColor'>" + data[i].name + "<span class='centerrightTabTd orangeColor'>" + data[i].value + "</span></td>";
                    }
                    html += "</tr>";
                }
            }
            else {
                if (val > 1) {
                    var numberone = parseFloat(val);
                    var numbertwo = parseFloat(1);
                    var sum = numberone + numbertwo;
                    var j = sum / 2;

                    html += "<tr>";
                    for (var m = 0; m < j; m++) {
                        html += "<td class='ryclFontSizeColor'>" + data[m].name + "<span class='centerrightTabTd orangeColor'>" + data[m].value + "</span></td>";
                    }
                    html += "</tr>";

                    html += "<tr>"
                    for (j; j < data.length; j++) {
                        html += "<td class='ryclFontSizeColor'>" + data[j].name + "<span class='centerrightTabTd orangeColor'>" + data[j].value + "</span></td>";
                    }
                    html += "</tr>";
                }
                else {
                    html += "<tr>";
                    for (var h = 0; h < val; h++) {
                        html += "<td class='ryclFontSizeColor'>" + data[h].name + "<span class='centerrightTabTd orangeColor'>" + data[h].value + "</span></td>";
                    }
                    html += "</tr>";
                }
            }
            $("#rytypedate").html(html);
        }, error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    })
}
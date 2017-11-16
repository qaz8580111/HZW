function assemblyunit(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async: true,
        timeout: 10000,    //超时时间
        dataType: "json",
        success: function (data, textStatus, jqxhr) {
            var htmlone = null;
            var htmltwo = null;
            for (var i = 0; i < 6; i++) {
                if (i % 2 == 0) {
                    htmlone += "<tr>";
                    htmlone += "<td>" + data[i].name + "</td><td>" + data[i].num + "</td>";
                } else {
                    htmlone += "<td>" + data[i].name + "</td><td>" + data[i].num + "</td>";
                    htmlone += "</tr>";
                }
            }
            htmltwo += "<tr>";
            for (var j = 6; j < 9; j++) {
                htmltwo += "<td>" + data[j].name + "</td><td>" + data[j].num + "</td>";
            }
            htmltwo += "</tr><tr>";
            for (var j = 9; j < 12; j++) {
                htmltwo += "<td>" + data[j].name + "</td><td>" + data[j].num + "</td>";
            }
            htmltwo += "</tr><tr>";
            for (var j = 12; j < data.length; j++) {
                htmltwo += "<td>" + data[j].name + "</td><td>" + data[j].num + "</td>";
            }
            htmltwo += "</tr>";
            $("#bjlefttable").html(htmlone);
            $("#bjrighttable").html(htmltwo);
        }
        , error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    });
}
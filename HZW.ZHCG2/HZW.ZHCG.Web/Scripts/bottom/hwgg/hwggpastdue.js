function hwggpastdue(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async: true,
        timeout: 10000,
        dataType: 'json',
        success: function (data, jqxhr, textStatus) {
            var html;
            for (var i = 0; i < data.length; i++) {
                html += "<tr><td>" + data[i].AdName + "</td><td>" + subStringvalue(data[i].Address,10) + "</td><td>" + data[i].EndDate.toString().substring(0, data[i].EndDate.toString().indexOf(":") - 2) + "</td></tr>";
            }
            $("#hwggpastdue").html(html);
        }, error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    });
}
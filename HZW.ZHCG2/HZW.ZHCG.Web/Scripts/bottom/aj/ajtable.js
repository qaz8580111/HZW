//获取案件hot(4)条信息
function getajtable(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async: true,
        timeout: 10000,
        dataType: 'json',
        success: function (data, jqxhr, textStatus) {
            var html;
            $.each(data, function (i, n) {
                html += "<tr><td>" + n.tasknum + "</td><td>" + n.address + "</td><td>" + n.createtime + "</td></tr>";
            });
            $("#ajtable").html(html);
        }, error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    })
}
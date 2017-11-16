function yjdjcount(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async: true,
        timeout: 10000,
        dataType: 'json',
        success: function (data, textStatus, jqxhr) {
            $("#yjdjcount").text(data);
        }, error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    });
}
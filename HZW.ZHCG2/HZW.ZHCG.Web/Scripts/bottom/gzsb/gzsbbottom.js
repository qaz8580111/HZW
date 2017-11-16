function getcenterDevice(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type:'get',
        async: true,
        timeout: 10000,
        dataType: 'json',
        success: function (data, jqxhr, textStatus) {
            var zhxfjcnumber = data[0].count;
            $("#zhxfjc").text(zhxfjcnumber);
            var zhxfjcOnlineNo = data[0].bjcount;
            $("#zhxfjcOnline").text("(" + zhxfjcOnlineNo + ")");
            var djgjcnumber = data[1].count;
            $("#djgjc").text(djgjcnumber);
            var djgjcOnlinenumber = data[1].bjcount;
            $("#djgjcOnline").text("(" + djgjcOnlinenumber + ")");
            var pmfcjcnumber = data[2].count;
            $("#pmfcjc").text(pmfcjcnumber);
            var pmfcjcOnlinenumber = data[2].bjcount;
            $("#pmfcjcOnline").text("(" + pmfcjcOnlinenumber + ")");

        }, error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    });
}

function getDeviceRight(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async: true,
        timeout: 10000,
        dataType: 'json',
        success: function (data, jqxhr, textStatus) {
            var html;
            $.each(data, function (i, n) {
                html += "<tr><td>" + n.msgId + "</td><td>" + n.projectName + "</td><td>" + n.formattedCreated + "</td></tr>";
            })
            $("#gzsbtableData").html(html);
        }, error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    });
}

function getDeviceNumber(ajaxurl) {
    $.ajax({
        url: ajaxurl,
        type: 'get',
        async: true,
        timeout: 10000,
        dataType: 'json',
        success: function (data, jqxhr, textStatus) {
            var numberone = data[0].sumcount;
            var numbertwo = data[1].sumcount;
            var sumcount = parseFloat(numberone) + parseFloat(numbertwo);
            $("#DeviceCount").text(sumcount);
            $.each(data, function (i, n) {
                switch (n.flag) {
                    case 1:
                        $("#normalDevice").text(n.sumcount);
                        break;
                    case 2:
                        $("#nonormalDevice").text(n.sumcount);
                        break;
                }
            })
        }, error: function (xhr, textStatus) {
            console.log(textStatus);
        }
    });
}
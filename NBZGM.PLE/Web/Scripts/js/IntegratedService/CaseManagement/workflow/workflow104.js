

; (function ($, window, document, undefined) {

    $(document).ready(function () {

        //改变短信提醒人名称和电话
        var SMSNameAndNum = "";
        var zbdyId = $("#ZBDYID").val();
        var zbdyName = $("#ZBDYNAME").val();
        $.ajax({
            type: "POST",
            url: "/Workflow/GetSMSNumber",
            data: "userId=" + zbdyId + "",
            cache: false,
            async: false,
            success: function (data) {
                if (data != "") {
                    SMSNameAndNum = zbdyName + " 手机号：" + data;
                    $("#FSDX").val("");
                    $("#FSDX").val(data);
                }
                else {
                    $("#FSDX").attr("disabled", "disabled");
                    SMSNameAndNum = "(无号码)";
                }
            }
        });
        $("#SMSUserNameAndNum").html(SMSNameAndNum);


        // 如果回退时改变短信提醒联系人
        $("input[name=Approved]").click(function () {
            var IsBack = $(this).val();

            //如果不同意
            if (IsBack == "False") {
                var backProcessUserId = $("#BackProcessUserId").val();
                var backProcessUserName = $("#BackProcessUserName").val();
                var SMSNameAndNum = "";
                $.ajax({
                    type: "post",
                    url: "/Workflow/GetSMSNumber",
                    data: "userId=" + backProcessUserId + "",
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != "") {
                            SMSNameAndNum = backProcessUserName + " 手机号:" + data;
                            $("#FSDX").val("");
                            $("#FSDX").val(data);
                            $("#FSDX").removeAttr("disabled");
                        }
                        else {
                            $("#FSDX").attr("disabled", "disabled");
                            SMSNameAndNum = backProcessUserName + " 手机号： (无号码)";
                        }

                    }
        });
                $("#SMSUserNameAndNum").text(SMSNameAndNum);
                //同意
            } else {

        //改变短信提醒人名称和电话
        var SMSNameAndNum = "";
        var zbdyId = $("#ZBDYID").val();
                var zbdyName = $("#ZBDYNAME").val();
        $.ajax({
            type: "POST",
            url: "/Workflow/GetSMSNumber",
            data: "userId=" + zbdyId + "",
            cache: false,
            async: false,
            success: function (data) {
                if (data != "") {
                            SMSNameAndNum = zbdyName + " 手机号：" + data;
                            $("#FSDX").val("");
                    $("#FSDX").val(data);
                }
                else {
                    $("#FSDX").attr("disabled", "disabled");
                    SMSNameAndNum = "(无号码)";
                }
            }
        });
        $("#SMSUserNameAndNum").html(SMSNameAndNum);
            }

    });
    });

})(jQuery, window, document);

function ValidateWorkflowForm() {
    var flag = true;
    return flag;
}
; (function ($, window, document, undefined) {
    $(document).ready(function () {
        $("#tzsj").datepicker();

        $("input[name=tzjg]").change(function () {
            $("#TZJGID").val($(this).val());
            $("#TZJGName").val($(this).attr('tzjgName'));
        });

        $("#upload").click(function () {
            $("#sccl").css("display", "inherit");
        });

        //改变短信提醒人名称和电话
        var SMSNameAndNum = "";
        var fgldId = $("#FGLDID").val();
        var fgldName = $("#FGLDNAME").val();
        $.ajax({
            type: "POST",
            url: "/Workflow/GetSMSNumber",
            data: "userId=" + fgldId + "",
            cache: false,
            async: false,
            success: function (data) {
                if (data != "") {
                    SMSNameAndNum = fgldName + " 手机号：" + data;
                    $("#FSDX").val("");
                    $("#FSDX").val(data);
                }
                else {
                    $("#FSDX").attr("disabled", "disabled");
                    SMSNameAndNum = fgldName + "(无号码)";
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
                var fgldId = $("#FGLDID").val();
                var fgldName = $("#FGLDNAME").val();
                $.ajax({
                    type: "POST",
                    url: "/Workflow/GetSMSNumber",
                    data: "userId=" + fgldId + "",
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != "") {
                            SMSNameAndNum = fgldName + " 手机号：" + data;
                            $("#FSDX").val("");
                            $("#FSDX").val(data);
                        }
                        else {
                            $("#FSDX").attr("disabled", "disabled");
                            SMSNameAndNum = fgldName + "(无号码)";
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
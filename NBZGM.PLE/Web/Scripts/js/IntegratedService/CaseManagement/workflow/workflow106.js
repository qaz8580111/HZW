; (function ($, window, document, undefined) {
    $(document).ready(function () {
        $("input[category=dw], select[category=dw]").attr("disabled", "disabled");
        $("input[category=gr], select[category=gr]").attr("disabled", "disabled");

        $("#upload").click(function () {
            $("#sccl").css("display", "inherit");
        });

        $("#veto").click(function () {
            $("#Approved").val(false);
            $("#workflow6Form")[0].submit()
        });

        //改变短信提醒人名称和电话
        var SMSNameAndNum = "";
        var zbdyId = $("#ZBDYID").val();
        $.ajax({
            type: "POST",
            url: "/Workflow/GetSMSNumber",
            data: "userId=" + zbdyId + "",
            cache: false,
            async: false,
            success: function (data) {
                if (data != "") {
                    SMSNameAndNum = data;
                    $("#FSDX").val(data);
                }
                else {
                    $("#FSDX").attr("disabled", "disabled");
                    SMSNameAndNum = "(无号码)";
                }
            }
        });
        $("#SMSUserNameAndNum").html(SMSNameAndNum);
    });

   
})(jQuery, window, document);

function ValidateWorkflowForm() {
    var flag = true;
    return flag;
}
; (function ($, window, document, undefined) {
    $(document).ready(function () {

        $("#upload").click(function () {
            $("#sccl").css("display", "inherit");
        });

        $("input[name=dsryj]").change(function () {
            $("#DSRYJID").val($(this).val());
            $("#DSRYJName").val($(this).attr('dsryjName'));
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
    //待实现
    return flag;
}
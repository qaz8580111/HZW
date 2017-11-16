; (function ($, window, document, undefined) {

    $(document).ready(function () {

        //改变短信提醒人名称和电话
        var SMSNameAndNum = "";
        var cbldId = $("#CBLDID").val();
        $.ajax({
            type: "POST",
            url: "/Workflow/GetSMSNumber",
            data: "userId=" + cbldId + "",
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

//表单验证
function ValidateWorkflowForm() {
    var flag = true;

    return flag;
}
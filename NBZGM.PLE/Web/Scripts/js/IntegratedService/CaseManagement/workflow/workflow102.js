; (function ($, window, document, undefined) {
    $(document).ready(function () {

        $("#upload").click(function () {
            $("#sccl").css("display", "inherit");
        });

        //绑定分管领导选项改变事件
        $("select[name=FGLDID]").change(function () {
            $("input[name=FGLDName]").val("");
            $("input[name=FGLDName]").val($(this).find("option:selected").html());
        });

        //绑定法制处审核人改变事件
        $("#FZCSHRBH").change(function () {
            $("input[name=FZCSHR]").val("");
            $("input[name=FZCSHR]").val($(this).find("option:selected").html());

            //改变短信提醒人名称和电话
            var fzcId = $(this).val();
            var cbldName = $(this).find("option:selected").html();
            var SMSNameAndNum = "";
            if ($("input[name=Approved]:checked").val() == "True") {
                $.ajax({
                    type: "POST",
                    url: "/Workflow/GetSMSNumber",
                    data: "userId=" + fzcId + "",
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != "") {
                            SMSNameAndNum = cbldName + data;
                            $("#FSDX").val(data);
                        }
                        else {
                            $("#FSDX").attr("disabled", "disabled");
                            SMSNameAndNum = cbldName + "(无号码)";
                        }
                    }
                });
                $("#SMSUserNameAndNum").html(SMSNameAndNum);
            }
        });

        //如果回退时改变短信提醒联系人
        $("input[name=Approved]").click(function () {
            //不同意
            var IsBack = $(this).val();
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
            }
                //同意
            else {
                $("#FZCSHRBH").change();
                $("#FSDX").removeAttr("checked");
            }
        });
    });
})(jQuery, window, document);

function ValidateWorkflowForm() {
    var flag = true;

    //验证主办人员和协办人员是否为空
    var zbry = $("#ZBDY").val();
    var xbry = $("#XBDY").val();
    var fgld = $("#FGLDID").val();
    var fzcshr = $("#FZCSHRBH").val();

    //分管领导
    if (fgld == null || fgld == "") {
        flag = false;
        $("#error_FGLDID").show();
        $("#error_FGLDID").text("请选择分管领导");
    } else {
        $("#error_FGLDID").text("");
    }

    //法制处
    if (fzcshr == null || fzcshr == "") {
        flag = false;
        $("#error_FZCSHRBH").show();
        $("#error_FZCSHRBH").text("请选择法制处审核人");
    } else {
        $("#error_FZCSHRBH").text("");
    }

    //主办人
    if (zbry == null || zbry == "") {
        flag = false;
        $("#error_ZBDY").show();
        $("#error_ZBDY").text("请选择主办人员");
    }
    else {
        $("#error_ZBDY").text("");
    }

    //协办人
    if (xbry == "" || xbry == null) {
        flag = false;
        $("#error_XBDY").show();
        $("#error_XBDY").text("请选择协办人员");
    }
    else {
        $("#error_XBDY").text("");
    }

    return flag
}
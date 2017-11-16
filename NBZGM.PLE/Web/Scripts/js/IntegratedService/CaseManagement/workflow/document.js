; (function ($, window, document, undefined) {
    $(document).ready(function () {
        //绑定新增文书页面和修改文书页面的提交按钮点击事件
        $("#addDocContainer,#editDocContainer").delegate("#btnDocFormSubmit", "click", function () {
            var $thisButton = $(this);
            $thisButton.attr("disabled", "disabled");
            //表单验证
            var isPassValidate = ValidateDocForm();
            if (isPassValidate) {
                //生成新的工作流
                var adid = $("#ADID").val();
                var wiid = $("#WIID").val();
                if (adid == "101" && (wiid == "" || wiid == null)) {
                    $("#btn_workflowdDelete").show();
                    $.ajax({
                        type: "get",
                        url: "/GeneralCase/createWorkflow",
                        cache: false,
                        async: false,
                        success: function (data) {
                            var arr = new Array();
                            arr = data.split(",");
                            $("input[name='WIID']").val(arr[0]);
                            $("input[name='AIID']").val(arr[1]);
                            $("input[name='WICode']").val(arr[2]);

                            $("#WIIDa").val(arr[0]);
                            $("#AIIDa").val(arr[1]);
                            $("#WICodea").val(arr[2]);
                        }
                    });
                }
                $("form[class=docForm]").submit();
            } else {
                $thisButton.removeAttr("disabled");
            }
        });
    });
})(jQuery, window, document);
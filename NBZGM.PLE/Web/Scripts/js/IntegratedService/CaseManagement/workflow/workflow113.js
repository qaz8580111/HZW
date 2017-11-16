; (function ($, window, document, undefined) {
    $(document).ready(function () {
        $("#zxsj").datepicker();//执行时间

        $("#ZXJGID").change(function () {
            $("#ZXJGName").val($(this).find("option:selected").html());
        });

        $("#upload").click(function () {
            $("#sccl").css("display", "inherit");
        });
    });
    
})(jQuery, window, document);

function ValidateWorkflowForm() {
    var flag = true;
    //待实现
    return flag;
}
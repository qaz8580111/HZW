; (function ($, window, document, undefined) {

    $(document).ready(function () {
        //表单暂存
        $("#btnSaveForm").click(function () {
            $("#workflow101Form").attr("action", "/Workflow101/SaveForm");
            $("#workflow101Form").submit();
            $("#btn_workflowdDelete").show();
        });
    });
    //定义页面的警告提示信息面板
})(jQuery, window, document);
var alertPanel = {
    id: "#alertPanel",
    show: function (msg) {
        var $alertDiv = $("<div class=\"alert alert-error\" style=\"margin-left: 5px; margin-right: 5px;\">"
            + "<button class=\"close\" data-dismiss=\"alert\">×</button>"
            + "<strong>必填提醒：</strong>" + msg + ""
            + "</div>");
        $(this.id).html($alertDiv);
        //将滚动条置顶
        $("#btn-scrollup").click();
    }
};
//验证必填文书
function ValidateRequiredDocs() {
    if (!ValidateWorkflowForm()) {
        var $requiredDocBtns = $("#docBtnContainer a[IsRequired=1][count='']");
        alertPanel.show("流程表单内容填写不完整");
        return false;
    }
    var $requiredDocBtns = $("#docBtnContainer a[IsRequired=1][count='']");
    if ($requiredDocBtns.length > 0) {
        alertPanel.show("操作面板中存在橙色背景的必填文书还未填写");
        return false;
    }
    return true;
}
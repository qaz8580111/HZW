$(document).ready(function () {
    $("#LARQ").datepicker();

    //绑定承办单位选项改变事件
    $("select[name=CBDWID]").change(function () {
        $("input[name=CBDWName]").val($(this).find("option:selected").html());

        var CBDWID = $(this).find("option:selected").val();
        var url = "/Workflow101/GetCBLDs?UnitID=" + CBDWID;
        $("#CBLDID").empty();
        $.getJSON(url, function (data) {
            if (data != null) {
                $.each(data, function (i, n) {
                    $("#CBLDID").append($("<option value=\"" + n.ID + "\">" + n.Name + "</option>"));
                });
            }
        });
    });

    //绑定承办领导选项改变事件
    $("select[name=CBLDID]").change(function () {
        $("input[name=CBLDName]").val("");
        $("input[name=CBLDName]").val($(this).find("option:selected").html());
    });

    //新增相关事项审批下拉按钮数据绑定
    (function () {
        //绑定新增相关事项审批事项按钮项数据加载事件
        $("#relevantItemBtnsContainer").bind("loadRelevantItemBtns", function () {
            var $relevantBtnContainer = $(this).empty();
            $.getJSON("/Document/GetRelevantBtns", function (data) {
                $.each(data, function (i, n) {
                    var $relevantBtn = $("<li DDID='" + n.DDID + "' DDName='" + n.DDName + "'><a href=\"#\"><i class=\"icon-plus\"></i>" + n.DDName + "</a></li>");
                    $relevantBtnContainer.append($relevantBtn);
                });
                var $otherRelevantBtn = $("<li DDID='' DDName='其他事项内部审批表'><a href=\"#\"><i class=\"icon-plus\"></i>其他事项内部审批表</a></li>");
                $relevantBtnContainer.append($otherRelevantBtn);
            });
        });

        //触发相关事项审批按钮列表事件
        $("#relevantItemBtnsContainer").trigger("loadRelevantItemBtns");
    })();

    //绑定相关事项审批按钮单击事件
    $("#relevantItemBtnsContainer").delegate("li", "click", function () {
        var $thisRelevantItem = $(this);
        var ddid = $thisRelevantItem.attr("DDID");
        $("#DDID").val(ddid);

        //如果相关审批事项类型为其他类型，则隐藏文书 Tab 页
        if (ddid == "") {
            $("#SQSX").val("");
            //清空文书 Tab 页
            $("#hrefQTSXNBSPBDoc").html("");
            $("#hrefQTSXNBSPBDoc").hide();
            $("#hrefQTSXNBSPForm").click();
            return;
        } else {
            $("#SQSX").val($thisRelevantItem.attr("DDName"));
            $("#hrefQTSXNBSPBDoc").html($thisRelevantItem.attr("DDName"));
            $("#hrefQTSXNBSPBDoc").show();
            $("#hrefQTSXNBSPBDoc").click();
        }
       
        var addParams = {
            "WIID": $("#ParentWIID").val(),
            "AIID": $("#ParentAIID").val(),
            "DDID": ddid
        };

        $.get("/Document/AddDocumentGo?rad=" + Math.random(),
            addParams, function (data) {
                $("#divQTSXNBSPBDocContainer").html(data);
            }, "html");
        $("form").submitFlag = 0;
    });

    //相关事项审批表单 Tab 页、文书表单 Tab 页、上一步、下一步按钮的点击事件绑定
    (function () {
        //绑定点击相关事项审批表单 tab 页事件
        $("#hrefQTSXNBSPForm").click(function () {
            if ($("#DDID").val() == "") {
                $("#btnBackStep").hide();
            } else {
                $("#btnBackStep").show();
            }
            $("#btnNextStep").hide();
            $("#btnSubmit").show();
        });

        //绑定点击文书表单 tab 页事件
        $("#hrefQTSXNBSPBDoc").click(function () {
            $("#btnBackStep").hide();
            $("#btnSubmit").hide();
            $("#btnNextStep").show();
        });

        //下一步按钮点击事件
        $("#btnNextStep").click(function () {
            $("#hrefQTSXNBSPForm").click();
            $("#btn-scrollup").click();
        });

        //上一步按钮点击事件
        $("#btnBackStep").click(function () {
            $("#hrefQTSXNBSPBDoc").click();
            $("#btn-scrollup").click();
        });
    })();

    $("#btnSubmit").click(function () {
        $(".formErrorMsg").hide();
        $(".formErrorMsg").text("");
        //处理要提交的文书表单数据
        var strFun1 = $("#hidPackingDataFunc").val();

        if (strFun1 && strFun1 != "") {
            eval("(" + strFun1 + ")()");
        }

        //验证文书表单()
        var isPass1 = true;

        var strFun2 = $("#hidValidateDocFormFunc").val();
        if (strFun2 && strFun2 != "") {
            isPass1 = eval("(" + strFun2 + ")()");
        }

        if (!isPass1) {
            $("#btnBackStep").click();
        }

        //验证审批表表单
        var isPass2 = validateXGSXSPForm();

        if (isPass1 && isPass2) {
            if (this.submitFlag == 1) {
                alert('数据已经提交，请勿再次提交。');
            }
            else {
                this.submitFlag = 1;
                $("form").submit();
            }
        } else {
            alertPanel.show("表单验证未通过");
            setTimeout(function () {
                alertPanel.hide();
            }, 2000)
        }
    });

    //定义页面的警告提示信息面板
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
        },
        hide: function () {
            $(this.id).find("button").click();
            $(this.id).html("");
        }
    };

    function validateXGSXSPForm() {
        var flag = true;

        //申请事项
        var sqsx = $("#SQSX").val();
        if ($.trim(sqsx) == "") {
            $("#error_SQSX").show();
            $("#error_SQSX").html("申请事项不能为空");
            flag = false;
        }

        //文书编号
        var docBH = $("#WSBH").val();
        if ($.trim(docBH) == "") {
            $("#error_WSBH").show();
            $("#error_WSBH").html("文书编号不能为空");
            flag = false;
        } else {
            $.ajax({
                type: "POST",
                data: "DDID=12&WSBH=" + docBH + "",
                url: "/Document/ValidateWSBH",
                async: false,
                cache: false,
                success: function (data) {
                    if (data != "True") {
                        $("#error_WSBH").css({ "display": "block" });
                        $("#error_WSBH").html("文书编号已存在");
                        flag = false;
                    }
                }
            })
        }
        //承办领导
        if ($.trim($("#CBLDID").val()) == "") {
            $("#error_CBLDID").show();
            $("#error_CBLDID").html("请选择承办领导");
            flag = false;
        }
        return flag;
    };
});
; (function ($, window, document, undefined) {

    $(document).ready(function () {
        //初始化案情摘要
        GetAQZY();
        //改变当事人
        $("#OrgForm_MC,#PersonForm_XM,#FASJ").change(function () {
            GetAQZY();
        })

        //拟办意见
        GetNBYJ();

        //绑定案发时间选择控件
        $("input[name=FASJ]").datetimepicker();
        $("input[name=FASJ]").blur(function () {
            var datetime = $(this).val();
            if (datetime.substr(datetime.length - 5, datetime.length - 1) == "00:00") {
                $(this).val(datetime.substr(0, datetime.length - 6));
            }
        });

        //绑定出生年月日期选择控件
        $("input[name=CSNY]").datepicker();

        //绑定当事人类型单选按钮的选择事件
        if ($(".dsrlxgr").attr("checked")) {
            $(".gr").removeAttr("disabled");
            $(".dw").attr("disabled", "disabled");
        } else {
            $(".dw").removeAttr("disabled");
            $(".gr").attr("disabled", "disabled");
        }

        $(".dsrlxdw").click(function () {
            $(".dw").removeAttr("disabled");
            $(".gr").attr("disabled", "disabled");
            $(".gr").val("");
        });

        $(".dsrlxgr").click(function () {
            $(".gr").removeAttr("disabled");
            $(".dw").attr("disabled", "disabled");
            $(".dw").val("");
        });

        //保存选中的案件来源名称
        $("select[name=AJLYID]").change(function () {
            $("input[name=AJLYName]").val($(this).find("option:selected").html());
        });
        $("select[name=AJLYID]").change();

        //绑定承办单位选项改变事件
        $("select[name=CBDWID]").change();
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
                $("select[name=CBLDID]").change();
            });

        });

        //绑定承办领导选项改变事件
        $("select[name=CBLDID]").change(function () {
            $("input[name=CBLDName]").val("");
            $("input[name=CBLDName]").val($(this).find("option:selected").html());

            //改变短信提醒人名称和电话
            var cbldId = $(this).val();
            if ($.trim(cbldId) == "") {
                cbldId = 0;
            }
            var cbldName = $(this).find("option:selected").html();
            var SMSNameAndNum = "";
            $.ajax({
                type: "POST",
                url: "/Workflow/GetSMSNumber",
                data: "userId=" + cbldId + "",
                cache: false,
                async: false,
                success: function (data) {
                    if (data != null) {
                        SMSNameAndNum = cbldName + " 手机号：" + data;
                        $("#FSDX").val(data);
                    }
                    else {
                        $("#FSDX").attr("disabled", "disabled");
                        SMSNameAndNum = cbldName + "(无号码)"
                    }
                }
            });
            $("#SMSUserNameAndNum").html(SMSNameAndNum);

        });
        $("select[name=CBLDID]").change();


        $("#IllegalClass1ID").change(function () {
            var illegaClassID = $(this).find("option:selected").val();
            var url = "/Workflow101/GetIllegalClasses?IllegaClassID=" + illegaClassID;
            $("#IllegalClass2ID").empty();
            $("#IllegalClass2ID").append($("<option value=''>请选择小类</option>"));
            BindSelect($("#IllegalClass2ID"), url);
        });

        $("#IllegalClass2ID").change(function () {
            var illegaClassID = $(this).find("option:selected").val();
            var url = "/Workflow101/GetIllegalClasses?IllegaClassID=" + illegaClassID;
            $("#IllegalClass3ID").empty();
            $("#IllegalClass3ID").append($("<option value=''>请选择子类</option>"));
            BindSelect($("#IllegalClass3ID"), url);
        });

        $("#IllegalClass3ID").change(function () {
            var illegaClassID = $(this).find("option:selected").val();
            var url = "/Workflow101/GetIllegalItems?IllegaClassID=" + illegaClassID;
            $("#IllegalForm_ID").empty();
            $("#IllegalForm_ID").append($("<option value=''>请选择违法行为</option>"));
            BindSelect($("#IllegalForm_ID"), url);
        });

        function BindSelect($select, url) {
            $.getJSON(url, function (data) {
                if (data != null) {
                    $.each(data, function (i, n) {
                        $select.append($("<option value=\"" + n.ID + "\">" + n.Name + "</option>"));
                    });
                }
            });
        }

        $("#IllegalForm_ID").change(function () {
            var IllegalItemID = $(this).find("option:selected").val();
            var url = "/Workflow101/GetIllegalInfomation?IllegalItemID=" + IllegalItemID;
            $.getJSON(url, function (data) {
                if (data != null) {
                    $("#div1").html("适用的违则：" + (data.WEIZE == null ? " " : data.WEIZE));
                    $("#div2").html("适用的罚则：" + (data.FZZE == null ? "" : data.FZZE));
                    $("#div3").html("适用的处罚：" + (data.PENALTYCONTENT != null ? data.PENALTYCONTENT : ""));
                    $("#IllegalForm_Name").val(data.ILLEGALITEMNAME);
                    $("#IllegalForm_WeiZe").val(data.WEIZE);
                    $("#IllegalForm_FaZe").val(data.FZZE);
                    $("#IllegalForm_Code").val(data.ILLEGALCODE);
                    $("#IllegalForm_ChuFa").val(data.PENALTYCONTENT);
                    //获取立案理由
                    GetLALY();
                }
            });
        });

        $("#IllegalForm_Code").keyup(function () {
            var wfxwdm = $("#IllegalForm_Code").val();
            if (wfxwdm.length == 1) {
                var Class1_ID = wfxwdm.substring(0, 1);
                var OptionNum = $("#IllegalClass1ID").find("option").length;
                for (var i = 0; i < OptionNum; i++) {
                    var option = document.getElementById("IllegalClass1ID").options[i];
                    if ($(option).text().substring(0, 1) == Class1_ID) {
                        $("#IllegalClass1ID").val($(option).val());
                        $("#IllegalClass2ID").empty();
                        BindSelect($("#IllegalClass2ID"), "/Workflow101/GetIllegalClasses?IllegaClassID=" + $(option).val());
                        $("#IllegalClass3ID").empty();
                        $("#IllegalClass3ID").append($("<option value=''>请选择子类</option>"));
                        $("#IllegalForm_ID").empty();
                        $("#IllegalForm_ID").append($("<option value=''>请选择违法行为</option>"));
                    }
                }
            }

            if (wfxwdm.length == 2) {
                var 小类 = wfxwdm.substring(1, 2);
                var OptionNum = $("#IllegalClass2ID").find("option").length;
                for (var i = 0; i < OptionNum; i++) {
                    var option = document.getElementById("IllegalClass2ID").options[i];
                    if ($(option).text().substring(1, 2) == 小类) {
                        $("#IllegalClass2ID").get(0).selectedIndex = i;
                        $("#IllegalClass3ID").empty();
                        BindSelect($("#IllegalClass3ID"), "/Workflow101/GetIllegalClasses?IllegaClassID=" + $("#IllegalClass2ID").val());
                        $("#IllegalForm_ID").empty();
                        $("#IllegalForm_ID").append($("<option value=''>请选择违法行为</option>"));
                        return;
                    }
                }

            }

            if (wfxwdm.length == 3) {
                var 子类 = wfxwdm.substring(2, 3);
                var OptionNum = $("#IllegalClass3ID").find("option").length;
                for (var i = 0; i < OptionNum; i++) {
                    var option = document.getElementById("IllegalClass3ID").options[i];
                    if ($(option).text().substring(2, 3) == 子类) {
                        $("#IllegalClass3ID").get(0).selectedIndex = i;
                        $("#IllegalForm_ID").empty();
                        BindSelect($("#IllegalForm_ID"), "/Workflow101/GetIllegalItems?IllegaClassID=" + $(option).val());
                        return;
                    }
                }
            }

            if (wfxwdm.length == 5) {
                var 违法行为 = wfxwdm.substring(3, 5);
                var OptionNum = $("#IllegalForm_ID").find("option").length;
                for (var i = 0; i < OptionNum; i++) {
                    var option = document.getElementById("IllegalForm_ID").options[i];
                    if ($(option).text().substring(3, 5) == 违法行为) {
                        $("#IllegalForm_ID").val($(option).val());

                        var IllegalItemID = $("#IllegalForm_ID").val();
                        var url = "/Workflow101/GetIllegalInfomation?IllegalItemID=" + IllegalItemID;
                        $.getJSON(url, function (data) {
                            if (data != null) {
                                $("#div1").html("适用的违则：" + (data.WEIZE == null ? " " : data.WEIZE));
                                $("#div2").html("适用的罚则：" + (data.FZZE == null ? "" : data.FZZE));
                                $("#div3").html("适用的处罚：" + (data.PENALTYCONTENT != null ? data.PENALTYCONTENT : ""));
                                $("#IllegalForm_Name").val(data.ILLEGALITEMNAME);
                                $("#IllegalForm_WeiZe").val(data.WEIZE);
                                $("#IllegalForm_FaZe").val(data.FZZE);
                                $("#IllegalForm_Code").val(data.ILLEGALCODE);
                                $("#IllegalForm_ChuFa").val(data.PENALTYCONTENT);
                                GetLALY();
                            }

                        });

                    }
                }
            }
            if (wfxwdm.length > 5) {
                $("#IllegalForm_Code").val($("#IllegalForm_Code").val().substring(0, 5));
            }
        });

        //上传控件
        $("#upload").click(function () {
            $("#sccl").css("display", "inherit");
        });

        //拟办队员1
        $("#NBDYID1").change(function () {
            $("#NBDYNAME1").val($("#NBDYID1 option:selected").text());
        });
        //拟办队员2
        $("#NBDYID2").change(function () {
            $("#NBDYNAME2").val($("#NBDYID2 option:selected").text());
        })
    });
})(jQuery, window, document);

//表单验证
function ValidateWorkflowForm() {
    var flag = true;
    $("label.formErrorMsg").hide();
    $("label.formErrorMsg").text("");

    //验证案由是否为空
    var ay = $("#AY").val();
    if ($.trim(ay) == "") {
        flag = false;
        $("#error_AY").show();
        $("#error_AY").text("案由不能为空");
    }

    //先验证发案时间是否为空
    var fasj = $("#FASJ").val();
    if ($.trim(fasj) == "") {
        flag = false;
        $("#error_FASJ").show();
        $("#error_FASJ").text("案发时间不能为空");
    }

    //验证案件来源是否为空
    var ajlyid = $("#AJLYID").val();
    if ($.trim(ajlyid) == "") {
        flag = false;
        $("#error_AJLYID").show();
        $("#error_AJLYID").text("请选择案件来源");
    }

    //验证承办单位是否为空
    var cbdwid = $("#CBDWID").val();
    if ($.trim(cbdwid) == "") {
        flag = false;
        $("#error_CBDWID").show();
        $("#error_CBDWID").text("请选择承办单位");
    }

    //验证承办领导是否为空
    var cbldid = $("#CBLDID").val();
    if ($.trim(cbldid) == "") {
        flag = false;
        $("#error_CBLDID").show();
        $("#error_CBLDID").text("请选择承办领导");
    }

    //验证违法行为是否为空
    var illegalItemID = $("#IllegalForm_ID").val();
    if ($.trim(illegalItemID) == "") {
        flag = false;
        $("#error_IllegalItemID").show()
        $("#error_IllegalItemID").text("请选择违法行为");
    }

    //验证拟办队员
    var nbdy1 = $("#NBDYID1").val();
    if (nbdy1=="-1") {
        $("#error_NBDY1").show();
        $("#error_NBDY1").text("请选择拟办队员");
        flag = false;
    }

    var nbdy2 = $("#NBDYID2").val();
    if (nbdy2 == "-1") {
        $("#error_NBDY2").show();
        $("#error_NBDY2").text("请选择拟办队员");
        flag = false;
    }

    //验证文书编号
    var docBH = $("#WSBH").val();
    if ($.trim(docBH) == "") {
        $("#error_WSBH").show();
        $("#error_WSBH").text("文书编号不能为空");
        flag = false;
    } else {
        //判断表单是否退回0为首次，1为退回
        if ($("#IsRollback").val() == "0") {
            $.ajax({
                type: "POST",
                url: "/Document/ValidateWSBH",
                data: "DDID=13&WSBH=" + docBH + "",
                cache: false,
                async: false,
                success: function (data) {
                    if (data != "True") {
                        $("#error_WSBH").show();
                        $("#error_WSBH").text("文书编号已存在");
                        flag = false;
                    }
                }
            });
        }
        else {
            var wiid = $("#WIID").val();
            $.ajax({
                type: "POST",
                url: "/Workflow101/ValidateRollbackWSBH",
                data: "DDID=13&WSBH=" + docBH + "&WIID=" + wiid + "",
                cache: false,
                async: false,
                success: function (data) {
                    if (data != "True") {
                        $("#error_WSBH").show();
                        $("#error_WSBH").text("文书编号已存在");
                        flag = false;
                    }
                }
            });
        }
    }
    return flag;
}

//立案理由
function GetLALY() {
    if ($("#LALY").val() != "当事人的行为违反了") {
        var wz = $("#IllegalForm_WeiZe").val();
        var laly = "当事人的行为违反了" + wz + "";
        $("#LALY").val(laly);
    }
    else {
        var laly = "当事人的行为违反了";
        $("#LALY").val(laly);
    }
}

//案情摘要
function GetAQZY() {
    if ($("#AQZY").val() != "　　当事人 于,") {
        //当事人或单位
        var dsr = "";
        //发案时间
        var fasj = GetFASJ();
        if ($("input[name='DSRLX']:checked").val() == "dw") {
            dsr = $("#OrgForm_MC").val();
        } else if ($("input[name='DSRLX']:checked").val() == "gr") {
            dsr = $("#PersonForm_XM").val();
        }
        var aqzy = "　　当事人" + dsr + "于" + fasj + ",";
        $("#AQZY").val(aqzy);
    }else{
        var aqzy = "　　当事人 于,";
        $("#AQZY").val(aqzy);
    }
}

//处理时间函数
function GetFASJ() {
    var sj = $("#FASJ").val();
    if ($.trim(sj) != "") {
        sj = sj.split(" ");
        sj = sj[0].split("-");
        sj = sj[0] + "年" + sj[1] + "月" + sj[2] + "日";
        return sj;
    } else {
        return "";
    }
}


//拟办意见
function GetNBYJ() {
    if ($("#NBYJ").val() == "") {
        var nbyj = "建议立案查处";
        $("#NBYJ").val(nbyj);
    }
}
//初始化
function GetExpandInforForm101(APID) {
    $.get("/Approval/GetExpandInforForm101?random=" + Math.random(), { apid: APID, wiid: $("#WIID").val() },
         function (data) {
             if (data != null) {
                 $.each(data, function (i, item) {
                     $tr = $("<tr class='beforeTR'></tr>");
                     $th = $("<th style='text-align: center;'>" + item.NAME + "</th>");
                     $td = $("<td colspan='3'></td>");
                     $div1 = $("<div class='form-group' style='margin-bottom: 5px;'></div>");
                     $div2 = $("<div class='col-sm-9 col-lg-10 controls'></div>");

                     $input = null;

                     if (item.TYPE == "text") {
                         $input = $("<input name='" + item.NAME + "' value='" + item.VALUE + "' type='text' id='" + item.KEY + "' class='large' seqno='" + item.ID + "' placeholder='" + item.NAME + "' LX='" + item.TYPE + "'/>");
                     }
                     else {
                         $input = $("<input name='" + item.NAME + "' value='" + item.VALUE + "' type='text' id='" + item.KEY + "' class='large dateText' seqno='" + item.ID + "' placeholder='" + item.NAME + "' LX='" + item.TYPE + "'/>");
                     }

                     $div2.append($input);
                     $div1.append($div2);
                     $td.append($div1);
                     $tr.append($th);
                     $tr.append($td);
                     $("#brothorid").after($tr);
                 });

                 $(".dateText").datepicker();
                 $("#DZMC").val($("#APName").val());
                 $("#DZMC").attr("disabled", "disabled");
                 $("#XMMC").val($("#APName").val());
                 $("#XMMC").attr("disabled", "disabled");

                 $("#KSSZQX").bind("change", (function () {
                     var ksszqx = $("#KSSZQX").val();
                     var jsszqx = "";
                     var str = ksszqx.split("-");
                     if (str.length == 3) {
                         if (str[0] != "") {
                             jsszqx = parseInt(str[0]) + 1;
                         }
                         if (str[1] != "") {
                             jsszqx += "-" + str[1];
                         }
                         if (str[2] != "") {
                             jsszqx += "-" + str[2];
                         }
                     }
                     $("#JSSZQX").val(jsszqx);
                 }))
             }
         });
};
function GetLocateCkeckInform103(APID) {
    $.get("/Approval/GetLocateCkeckInform103?Ran=" + Math.random(), { apid: APID, wiid: $("#WIID").val() }, function (data) {
        var tr = "<tr><th style='text-align: center;'>现场核查情况</th><td colspan='3'><div class='form-group' style='margin-bottom: 5px;'><div class='col-sm-9 col-lg-10 controls'>";
        if (data != null) {
            $.each(data, function (i, item) {
                if (item.CHECK == "false") {
                    tr += "<input class='xchcTR' text='" + item.NAME + "' type='radio' seqno='" + item.ID + "' name='xchcqk'  LX='" + item.TYPE + "' id='" + item.KEY + "'/>";
                }
                else {
                    tr += "<input class='xchcTR' text='" + item.NAME + "' type='radio' seqno='" + item.ID + "' name='xchcqk' LX='" + item.TYPE + "' id='" + item.KEY + "' checked/>";
                }
                tr += "<span>" + item.NAME + "</span>&nbsp;&nbsp;&nbsp;";
            });
        }
        tr += "</div></div></td></tr>";
        $("#brothor2").after(tr);
    });
}

//禁止修改
function GetExpandInforForm101disabled(APID) {
    $.get("/Approval/GetExpandInforForm101?random=" + Math.random(), { apid: APID, wiid: $("#WIID").val() },
         function (data) {
             if (data != null) {
                 $.each(data, function (i, item) {
                     $tr = $("<tr class='beforeTR'></tr>");
                     $th = $("<th style='text-align: center;line-height:30px'>" + item.NAME + "</th>");
                     $td = $("<td colspan='3'></td>");
                     $div1 = $("<div class='form-group' style='margin-bottom: 5px;' ></div>");
                     $div2 = $("<div class='col-sm-9 col-lg-10 controls'></div>");

                     $input = null;

                     if (item.TYPE == "text") {
                         $input = $("<input style='border:none;background-color:white' class='beforeinput' name='" + item.NAME + "' value='" + item.VALUE + "' type='text' id='" + item.KEY + "' class='large' seqno='" + item.ID + "' placeholder='" + item.NAME + "' LX='" + item.TYPE + "' disabled='disabled'/>");
                     }
                     else {
                         $input = $("<input style='border:none;background-color:white' class='beforeinput' name='" + item.NAME + "' value='" + item.VALUE + "' type='text' id='" + item.KEY + "' class='large' seqno='" + item.ID + "' placeholder='" + item.NAME + "' LX='" + item.TYPE + "' disabled='disabled'/>");
                     }

                     $div2.append($input);
                     $div1.append($div2);
                     $td.append($div1);
                     $tr.append($th);
                     $tr.append($td);
                     $("#brothorid").after($tr);
                 });
                 $("#DZMC").val($("#APName").val());
                 $("#DZMC").attr("disabled", "disabled");
                 $("#XMMC").val($("#APName").val());
                 $("#XMMC").attr("disabled", "disabled");
             }
         });
};

function GetLocateCkeckInform103disabled(APID) {
    $.get("/Approval/GetLocateCkeckInform103?Ran=" + Math.random(), { apid: APID, wiid: $("#WIID").val() }, function (data) {
        var tr = "<tr><th style='text-align: center;line-height:30px'>现场核查情况</th><td colspan='3'><div class='form-group' style='margin-bottom: 5px;'><div class='col-sm-9 col-lg-10 controls'>";
        if (data != null) {
            $.each(data, function (i, item) {
                if (item.CHECK == "false") {
                    tr += "<input class='xchcTR' type='radio' seqno='" + item.ID + "' name='xchcqk'  LX='" + item.TYPE + "' id='" + item.KEY + "' disabled='disabled'/>";
                }
                else {
                    tr += "<input class='xchcTR' type='radio' seqno='" + item.ID + "' name='xchcqk' LX='" + item.TYPE + "' id='" + item.KEY + "' checked disabled='disabled'/>";
                }
                tr += "<span>" + item.NAME + "</span>&nbsp;&nbsp;&nbsp;";
            });
        }
        tr += "</div></div></td></tr>";
        $("#brothor2").after(tr);
    });
}

function GetExplainInfo(APID) {
    $.get("/Approval/GetExplainInfo", { APID: APID }, function (data) {
        $("#explainInfo").html(data);
    });
}

$(document).ready(function () {
    //提交
    $("#btnSubmit").click(function () {
        if (FormSubmit()) {
            $("form").submit();
        }
    })
    //回退
    $("#refuse").click(function () {
        $("#bc").val("2");
        $("form").submit();
    });

})
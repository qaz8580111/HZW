var ip;
$(function () {
    //待办导航
    var TopLi = {
        ID: "#div_Cace",
        Click: function (ID, TableID) {
            $(this.ID).find("li").removeClass();
            ID = "#" + ID;
            TableID = "#" + TableID;
            $(ID).addClass("aahover");
            if (TableID == ZFSJWorkFlowTable.ID) {
                ZFSJWorkFlowTable.show();
            }
            if (TableID == CaseWorkFlowTable.ID) {
                CaseWorkFlowTable.show();
            }
            if (TableID == ComplaintReportingWorkTable.ID) {
                ComplaintReportingWorkTable.show();
            }
            if (TableID == GGFWEvetTable.ID) {
                GGFWEvetTable.show();
            }
            if (TableID == XZSPWorkFlowTable.ID) {
                XZSPWorkFlowTable.show();
            }
            if (TableID == GETDBGZTable.ID) {
                GETDBGZTable.show();
            }
        }
    };




    //执法事件列表
    var ZFSJWorkFlowTable = {
        ID: "#ZFSJWorkFlowTable",
        Class: ".datagrid",
        show: function () {
            $(this.Class).hide();
            $(this.ID).show();
        },
        load: function () {
            $.ajax({
                url: "/XTBG/AllEventsTableList",
                type: "POST",
                cache: false,
                success: function (data) {
                    if (data.length == 0) {
                        $tr = $("<tr></tr>");
                        $td = $("<td colspan='5'>没有数据</td>");
                        $tr.append($td);
                        $(ZFSJWorkFlowTable.ID).append($tr);
                    }
                    $.each(data, function (i, t) {
                        $tr = $("<tr></tr>");
                        $td_EventCode = $("<td>" + t.EventCode + "</td>");
                        if (t.EventTitle != null && t.EventTitle != "") {
                            $td_EventTitle = $("<td>" + t.EventTitle.substring(0, 40) + "</td>");
                        } else {
                            $td_EventTitle = $("<td>" + t.EventTitle + "</td>");
                        }
                        $td_adName = $("<td>" + t.ADName + "</td>");
                        $td_CreateTime = $("<td>" + t.CreateTime + "</td>");
                        $tb_cl = $("<td><a style='color:#08c' href='/ZFSJWorkflow/ZFSJWorkflowProcess?WIID=" + t.WIID + "'>处理 </a></td>");
                        $tr.append($td_EventCode);
                        $tr.append($td_EventTitle);
                        $tr.append($td_adName);
                        $tr.append($td_CreateTime);
                        $tr.append($tb_cl);
                        $(ZFSJWorkFlowTable.ID).append($tr);
                    })
                }
            });
        }
    }

    //一般案件列表
    var CaseWorkFlowTable = {
        ID: "#CaseWorkFlowTable",
        Class: ".datagrid",
        show: function () {
            $(this.Class).hide();
            $(this.ID).show();
        },
        load: function () {
            $.ajax({
                url: "/PersonalWorkbench/GetCaseWorkFlow",
                type: "POST",
                cache: false,
                success: function (data) {
                    if (data.length == 0) {
                        var html = "";
                        html += "<tr>";
                        html += "<td colspan='5'>没有数据</td>";
                        html += "</tr>";
                        $(CaseWorkFlowTable.ID).append(html);
                    }
                    $.each(data, function (i, t) {
                        var html = "";
                        html += "<tr>";
                        html += "<td>" + t.WICode + "</td>";
                        if (t.AY != null && t.AY != "") {
                            html += "<td>" + t.AY.substring(0, 34) + "</td>";
                        } else {
                            html += "<td>" + t.AY + "</td>";
                        }
                        html += "<td>" + t.ADName + "</td>";
                        html += "<td>" + t.DeliveryTime + "</td>";
                        if (t.WDID == 1) {
                            html += "<td><a style='color:#08c' href='/Workflow/WorkflowProcess?WIID=" + t.WIID + "&AIID=" + t.AIID + "'>处理</a></td>";
                        }
                        else {
                            html += "<td><a style='color:#08c' href='/RelevantItemWorkflow/RelevantItemWorkflowProcess?ParentWIID=" + t.ParentWIID + "&WIID=" + t.WIID + "&AIID=" + t.AIID + "'>处理</a></td>";
                        }
                        html += "</tr>";
                        $(CaseWorkFlowTable.ID).append(html);
                    });
                }
            });
        }
    }

    //信访待办列表
    var ComplaintReportingWorkTable = {
        ID: "#ComplaintReportingWorkTable",
        Class: ".datagrid",
        show: function () {
            $(this.Class).hide();
            $(this.ID).show();
        },
        load: function () {
            $.ajax({
                url: "/PersonalWorkbench/GetPublicobeayTodoEvent",
                type: "POST",
                cache: false,
                success: function (data) {
                    if (data.length == 0) {
                        var html = "";
                        html += "<tr>";
                        html += "<td colspan='5'>没有数据</td>"
                        html += "</tr>";
                        $(ComplaintReportingWorkTable.ID).append(html);
                    } else {
                        var html = "";
                        $.each(data, function (i, t) {
                            html += "<tr>";
                            if (t.EVENTTITLE != null && t.EVENTTITLE != "") {
                                html += "<td>" + t.EVENTTITLE.substring(0, 83) + "</td>";
                            } else {
                                html += "<td>" + t.EVENTTITLE + "</td>";
                            }
                            html += "<td>" + t.SOURCENAME + "</td>";
                            html += "<td>" + t.UserName + "</td>";
                            html += "<td>" + t.CREATETIME + "</td>";
                            html += "<td><a style='color:#08c' href='/EventRegister/GGFWWorkflowProcess?ID=" + t.EVENTID + "&type=sel&WIID=" + t.WIID + "'>处理</a></td>";
                            html += "</tr>";
                        });
                        $(ComplaintReportingWorkTable.ID).append(html);
                    }
                }
            });
        }
    }

    //公共服务列表
    var GGFWEvetTable = {
        ID: "#GGFWEvetTable",
        Class: ".datagrid",
        show: function () {
            $(this.Class).hide();
            $(this.ID).show();
        },
        load: function () {
            $.ajax({
                url: "/GGFWEvent/GGFWProcessTask",
                type: "POST",
                cache: false,
                success: function (data) {
                    if (data.length == 0) {
                        $tr = $("<tr></tr>");
                        $td = $("<td colspan='5'>没有数据</td>");
                        $tr.append($td);
                        $(GGFWEvetTable.ID).append($tr);
                    }
                    $.each(data, function (i, t) {
                        $tr = $("<tr></tr>");
                        if (t.EventTitle != null && t.EventTitle != "") {
                            $td_EventID = $("<td>" + t.EventTitle.substring(0, 35) + "</td>");
                        } else {
                            $td_EventID = $("<td>" + t.EventTitle + "</td>");
                        }
                        $td_EventTitle = $("<td>" + t.ADName + "</td>");
                        $td_ADName = $("<td>" + t.EventSource + "</td>");
                        $td_CreateTime = $("<td>" + t.OverTime + "</td>");
                        $tb_cl = $("<td><a style='color:#08c'  href='/GGFWEvent/DealEvent?ID=" + t.ID + "'>处理 </a></td>")
                        $tr.append($td_EventID);
                        $tr.append($td_EventTitle);
                        $tr.append($td_ADName);
                        $tr.append($td_CreateTime);
                        $tr.append($tb_cl);
                        $(GGFWEvetTable.ID).append($tr);
                    })
                }
            });
        }
    }

    //获取办公代办
    var GETDBGZTable = {
        ID: "#GETDBGZTable",
        Class: ".datagrid",
        show: function () {
            $(this.Class).hide();
            $(this.ID).show();
        },
        load: function () {
            $.ajax({
                url: "/PersonalWorkbench/GetBGDBByUserId",
                type: "POST",
                cache: false,
                success: function (data) {
                    if (data.length == 0) {
                        var html = "";
                        html += "<tr>";
                        html += "<td colspan='5'>没有数据</td>"
                        html += "</tr>";
                        $(GETDBGZTable.ID).append(html);
                    } else {
                        var html = "";
                        $.each(data, function (i, t) {
                            html += "<tr>";
                            html += "<td style='text-align:left'>" + t.wfname + "</td>";
                            if (t.wfsname.length > 40) {
                                html += "<td style='text-align:left' title=" + t.wfsname.substring(39, t.wfsname.length) + ">" + t.wfsname.substring(0, 39) + "</td>";
                            } else {
                                html += "<td style='text-align:left'>" + t.wfsname + "</td>";
                            }
                            html += "<td style='text-align:left'>" + t.username + "</td>";
                            html += "<td style='text-align:left'>" + t.createtime + "</td>";
                            html += "<td style='text-align:left'><a style='color:#08c' href='/OAWorkFlowManager/ProcessIndex/?WFID=" + t.wfid + "&WFDID=" + t.wfdid + "&WFSID=" + t.wfsid + "&WFSAID=" + t.wfsaid + "&IsMainWF=" + t.ISMAINWF + "&TYPE=deal'>处理</a></td>";
                            html += "</tr>";
                        });
                        $(GETDBGZTable.ID).append(html);
                    }
                }
            });
        }
    }
    //行政审批列表
    var XZSPWorkFlowTable = {
        ID: "#XZSPWorkFlowTable",
        Class: ".datagrid",
        show: function () {
            $(this.Class).hide();
            $(this.ID).show();
        },
        load: function () {
            $.ajax({
                url: "/PersonalWorkbench/GetXZSPWorkflow",
                type: "POST",
                cache: false,
                success: function (data) {
                    if (data.length == 0) {
                        $tr = $("<tr></tr>");
                        $td = $("<td colspan='5'>没有数据</td>");
                        $tr.append($td);
                        $(XZSPWorkFlowTable.ID).append($tr);
                    }
                    $.each(data, function (i, t) {
                        $tr = $("<tr></tr>");
                        $td_Id = $("<td>" + t.Id + "</td>");
                        $td_PROJECTNAME = $("<td>" + t.PROJECTNAME + "</td>");
                        $td_PROJECTNAME = $("<td>" + t.PROJECTNAME + "</td>");
                        $td_CreateTime = $("<td>" + t.CreateTime + "</td>");
                        $tr.append($td_Id);
                        $tr.append($td_PROJECTNAME);
                        $tr.append($td_PROJECTNAME);
                        $tr.append($td_CreateTime);
                        $(XZSPWorkFlowTable.ID).append($tr);
                    })
                }
            });
        }
    }

    //公示公告
    var TZUL = {
        ID: "#TZ_ul",
        load: function () {
            $.ajax({
                url: "/PersonalWorkbench/GetTZ",
                type: "POST",
                cache: false,
                success: function (data) {
                    $.each(data, function (i, t) {
                        var alltitle = t.title.substring(25, t.title.length);
                        if (t.title.length > 35) {
                            var titleone = t.title.substring(0, 25) + "...";
                            $ol = $("<ol></ol>");
                            //$a_title = $("<a href='/ArticleManagement/ShowArticle?articleID=" + t.id + "&random=" + Math.random() + "'  target='_blank'>" + titleone + "</a>" + "<p style='float:right; color:gray'>" + t.CreteTime + "</p>");
                            $a_title = $("<a href='" + ip + " /Article/ArticleDetail?articleID=" + t.id + "&random=" + Math.random() + "'  target='_blank'>" + titleone + "</a>" + "<p style='float:right; color:gray'>" + t.CreteTime + "</p>");
                            $a_title.bind("click", function () {
                                NewsShow(t.id);
                            });
                            $ol.append($a_title);
                            $(TZUL.ID).append($ol);
                        } else {
                            $ol = $("<ol></ol>");
                            $a_title = $("<a href='" + ip + "/Article/ArticleDetail?articleID=" + t.id + "&random=" + Math.random() + "'  target='_blank'>" + t.title + "</a>" + "<p style='float:right; color:gray'>" + t.CreteTime + "</p>");
                            $a_title.bind("click", function () {
                                NewsShow(t.id);
                            });
                            $ol.append($a_title);
                            $(TZUL.ID).append($ol);
                        }
                    });
                }
            })
        }
    }

    var NewUL = {
        ID: "#News_ul",
        load: function () {
            $.ajax({
                url: "/PersonalWorkbench/GetNEWS",
                type: "POST",
                cache: false,
                success: function (data) {
                    $.each(data, function (i, t) {
                        var titleall = t.title.substring(25, t.title.length);
                        if (t.title.length > 35) {
                            var titleone = t.title.substring(0, 25) + "...";
                            $ol = $("<ol></ol>");
                            $a_title = $("<a href='" + ip + "/Article/ArticleDetail?articleID=" + t.id + "&random=" + Math.random() + "'  target='_blank'>" + titleone + "</a>" + "<p style='float:right; color:gray'>" + t.CreteTime + "</p>");
                            $a_title.bind("click", function () {
                                NewsShow(t.id);
                            });
                            $ol.append($a_title);
                            $(NewUL.ID).append($ol);
                        } else {
                            $ol = $("<ol></ol>");
                            $a_title = $("<a href='" + ip + "/Article/ArticleDetail?articleID=" + t.id + "&random=" + Math.random() + "'  target='_blank'>" + t.title + "</a>" + "<p style='float:right; color:gray'>" + t.CreteTime + "</p>");
                            $a_title.bind("click", function () {
                                NewsShow(t.id);
                            });
                            $ol.append($a_title);
                            $(NewUL.ID).append($ol);
                        }
                    });
                }
            })
        }
    }




    //列表导航样式切换
    $(TopLi.ID).find("li").bind("click", function () {
        TopLi.Click($(this).attr("id"), $(this).attr("tableid"));
    });
    loadXMLDoc();
    //首次加载待办列表
    ZFSJWorkFlowTable.show();
    CaseWorkFlowTable.load();
    GGFWEvetTable.load();
    ComplaintReportingWorkTable.load();
    //ZFSJWorkFlowTable.load();
    //加载办公待办
    GETDBGZTable.load();
    //公示公告
    TZUL.load();

})
function NewsShow(articleID) {
    $.ajax({
        type: "GET",
        url: "/ArticleManagement/ShowArticle?articleID=" + articleID + "&random=" + Math.random(),
        success: function (data) {
            $("#span90").hide();
            //隐藏编辑表单
            $("#span92").hide();
            //显示添加表单
            $('#article-content').empty();
            $("#span91").show();
            $("#article-content").html(data);
        }
    });
}
function loadXMLDoc() {
    var url = "/IntranetIpPort.txt";
    var xmlhttp;
    if (window.XMLHttpRequest) {
        // code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    }
    else {
        // code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            var xmldoc = xmlhttp.responseText;
            ip = xmldoc;
        }
    }
    xmlhttp.open("GET", url, true);
    xmlhttp.send();
}

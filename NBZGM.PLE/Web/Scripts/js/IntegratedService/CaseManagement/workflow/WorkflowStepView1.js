$(document).ready(function () {
    var StepView = {
        //文书列表页面
        docItemsContainer: {
            id: "#docItemsContainer",
            remove: function () {
                $(this.id).html("");
            },
        },

        //处理过程详细页面
        workFlowPanel: {
            id: "#workFlowPanel",
            show: function (aiid) {
                StepView.docPanel.hide();
                $(this.id).show();
                $(".historyFormDiv").hide();
                $("#historyFormDiv" + aiid).show();

            },
            hide: function () {
                $(this.id).hide();
            }
        },

        //文书显示标签
        ifrmViewDoc: {
            id: "#ifrmViewDoc",
            show: function (path) {
                docSrc = "/Document/GetPDFFile?DocPath=" + encodeURI(path);
                $(this.id).attr("src", docSrc);
            }
        },
        //文书详细页面
        docPanel: {
            id: "#docPanel",
            show: function (path) {
                $(this.id).show();
                StepView.workFlowPanel.hide();
                $(StepView.ifrmViewDoc.id).trigger("LoadInstance", path);
            },
            hide: function () {
                $(this.id).hide();
            }
        }
    }

    $(StepView.ifrmViewDoc.id).bind("LoadInstance", LoadInstance);

    var WIID = GetWIID();
    var load_aiid = $("#table_workflow tbody tr:first-child td:first-child a:first-child").attr("aiid");
    $("#docItemsContainer").bind("loadDocItem", GetDocInstances);
    $("#docItemsContainer").trigger("loadDocItem", [load_aiid]);

    //环节按钮点击事件
    $(".historyForm").click(function () {
        var aiid = $(this).attr("aiid");
        $("#docItemsContainer").trigger("loadDocItem", [aiid]);
        StepView.workFlowPanel.show(aiid);
    })

    //获取页面WIID
    function GetWIID() {
        var rel = "";
        var url = window.location.search.split("?");
        url = url[1].split("&");
        for (var i = 0; i < url.length; i++) {
            var wiid = url[i].split("=");
            if (wiid[0] = "WIID") {
                rel = wiid[1];
                break;
            }
        }
        return rel;
    }
    //根据活动标识查询文书
    function GetDocInstances(event, aiid) {
        StepView.docItemsContainer.remove();
        $.ajax({
            type: "POST",
            url: "/Document/GetDocInstance",
            data: "WIID=" + WIID + "&AIID=" + aiid + "",
            cache: false,
            dataType: "JSON",
            success: function (data) {
                $grid = "该节点没有生成文书";
                if (data.length != 0) {
                    $.each(data, function (i, item) {
                        $grid = "<div class='grid docItemTitle' docsrc=" + item.DOCPATH + " diname=" + item.DOCNAME + " ddid=" + item.DDID + ">";
                        $grid += " <div class='grid-content'>";
                        $grid += "<div class='c_avatar'>";
                        $grid += "<img alt='PDF' src='/Images/PDF.png'>";
                        $grid += "</div>";
                        $grid += "<div class='conversation-info'>";
                        $grid += "<div class='conversation-title'><span>" + item.DOCNAME + "</span></div>";
                        $grid += "<div class='conversation-date-status'>" + item.CREATEDTIME + "</div>";
                        $grid += "</div>";
                        $grid += "<div class='clear'></div>";
                        $grid += "</div>";
                        $grid += "</div>";
                        $(StepView.docItemsContainer.id).prepend($grid);
                    });
                    $(".docItemTitle").bind("click", function () {
                        StepView.docPanel.show($(this).attr("docsrc"));
                    });
                } else {
                    $(StepView.docItemsContainer.id).prepend($grid);
                }
            }
        })
    }
    //加载文书
    function LoadInstance(event, path) {
        docSrc = "/Document/GetPDFFile?DocPath=" + encodeURI(path);
        $(StepView.ifrmViewDoc.id).attr("src", docSrc);
    }
})




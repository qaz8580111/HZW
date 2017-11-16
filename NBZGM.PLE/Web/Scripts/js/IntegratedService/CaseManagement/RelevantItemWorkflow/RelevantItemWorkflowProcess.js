$(document).ready(function () {
    //定义并设置相关变量
    var constParams = {
        ParentWIID: $("#ParentWIID").val(),//父流程标识(一般案件流程标识)
        ParentAIID: $("#ParentAIID").val(),//父流程的当前环节活动标识
        WIID: $("#WIID").val(), //流程标识
        AIID: $("#AIID").val(), //活动标识
    };

    //控制页面右侧模块之间的显示与隐藏逻辑
    var rightPanel = {
        relevantItemsPanel: {
            myseft: $("#relevantItemsListPanel"),
            show: function () {
                this.myseft.show();
            },
            hide: function () {
                this.myseft.hide();
            },
            expand: function () {
                var t = $("#relevantItemsContent");

                if (t.is(":hidden")) {
                    $("#relevantItemsCollapse").click();
                }
            },
            collapse: function () {
                var t = $("#relevantItemsContent");

                if (!t.is(":hidden")) {
                    $("#relevantItemsCollapse").click();
                }
            }
        },
        relevantItemFormPanel: {
            myseft: $("#RelevantItemFormPanel"),
            show: function () {
                rightPanel.viewDocPanel.hide();
                rightPanel.viewRelevantItemPanel.hide();
                rightPanel.relevantItemsPanel.show();
                this.myseft.show();
            },
            hide: function () {
                this.myseft.hide();
            }
        },
        viewDocPanel: {
            myseft: $("#ViewDocPanel"),
            show: function (docSrc) {
                rightPanel.relevantItemsPanel.hide();
                rightPanel.viewRelevantItemPanel.hide();
                rightPanel.relevantItemFormPanel.hide();
                docSrc = "/Document/GetPDFFile?DocPath=" + encodeURI(docSrc);
                $("#ifrmViewDoc").attr("src", docSrc + "#scrollbar=0&statusbar=0");
                this.myseft.show();
            },

            hide: function () {
                $("#ifrmViewDoc").attr("src", "");
                this.myseft.hide();
            }
        },
        viewRelevantItemPanel: {
            myseft: $("#ViewRelevantItemPanel"),
            show: function () {
                rightPanel.relevantItemFormPanel.hide();
                this.myseft.show();
            },
            hide: function () {
                this.myseft.hide();
            }
        },
    };


    (function () {
        //绑定文书树控件数据加载事件
        $("ul#docTree").bind("loadDocTreeData", function () {
            //文书树配置
            var setting = {
                data: {
                    simpleData: {
                        enable: true
                    }
                },
                view: {
                    showIcon: false,
                    showLine: true,
                    fontCss: {
                        size: "14px"
                    }
                },
                callback: {
                    onClick: function (event, treeId, treeNode) {
                        if (treeNode.type == "doc") {
                            $("ul#docTree").trigger("clickDocTreeNode", treeNode.value);
                        }
                    }
                },
            };

            var queryParams = {
                "WIID": constParams.ParentWIID
            };

            $.ajax({
                type: "GET",
                cache: false,
                url: ("/Document/GetDocTree?rad=" + Math.random()),
                data: queryParams,
                success: function (data) {
                    $.fn.zTree.init($("ul#docTree"), setting, data);
                }
            });
        });

        //绑定点击文书树节点事件
        $("ul#docTree").bind("clickDocTreeNode", function (e, docSrc) {
            rightPanel.viewDocPanel.show(docSrc);
        });

        //绑定相关事项审批列表数据加载事件
        $("#relevantItemsTable").bind("loadRelevantItems", function () {
            var $tBody = $("#relevantItemsTable").find("tbody").empty();

            $.get("/RelevantItemWorkflow/GetRelevantItems",
                { ParentWIID: constParams.ParentWIID },
                function (data) {
                    if (data == null || data.length == 0) {
                        var $emptyTR = $("<tr><td colspan=\"4\">无相关事项审批</td></tr>");
                        $tBody.append($emptyTR);
                        return;
                    }
                    $.each(data, function (i, n) {
                        var $TR = $("<tr>" +
                            "<td>" + n.WINAME + "</td>" +
                            "<td>" + n.ADNAME + "</td>" +
                            "<td>" + n.USERNAME + "</td>" +
                            "<td><a ParentWIID='" + n.PARENTWIID + "' WIID='" + n.WIID + "' class='viewRelevantItem' href='javascript:void(0);'>查看</a></td>" +
                            "</tr>");
                        $tBody.append($TR);
                    });
                });
        });

        $("#relevantItemsTable").delegate("a.viewRelevantItem", "click", function () {
            var parentWIID = $(this).attr("ParentWIID");
            var WIID = $(this).attr("WIID");

            $.get("/RelevantItemWorkflow/ViewRelevantItem", {
                ParentWIID: parentWIID,
                WIID: WIID
            }, function (data) {
                $("#ViewRelevantItemPanel").html(data);
                rightPanel.viewRelevantItemPanel.show();
            });
        });

        $("#btnRelevantItemForm").click(function () {
            rightPanel.relevantItemFormPanel.show();
        });
        //触发文书树控件数据加载事件
        $("ul#docTree").trigger("loadDocTreeData");

        //触发相关事项审批列表加载事件
        $("#relevantItemsTable").trigger("loadRelevantItems");
    })();
});
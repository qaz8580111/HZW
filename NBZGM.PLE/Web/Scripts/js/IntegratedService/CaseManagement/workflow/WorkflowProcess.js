$(document).ready(function () {

    //定义并设置相关变量
    var constParams = {
        WIID: $("#WIID").val(), //流程标识
        ADID: $("#ADID").val(), //活动定义标识
        AIID: $("#AIID").val(), //活动标识
        DDID: $("#DDID").val(), //文书定义标识
        DIID: $("#DIID").val(), //文书标识
        IsNewWorkflow: $("#IsNewWorkflow").val()
    };
    
    //控制页面右侧模块之间的显示与隐藏逻辑
    var rightPanel = {
        hide: function () {
            rightPanel.addDocPanel.hide();
            rightPanel.editDocPanel.hide();
            rightPanel.viewDocPanel.hide();
            rightPanel.workflowFormPanel.hide();
        },
        addDocPanel: {
            id: "#addDocPanel",
            show: function () {
                rightPanel.hide();
                $(this.id).show();
                rightPanel.docOperationPanel.hide();
            },
            hide: function () {
                $(this.id).hide();
                $("#addDocContainer").html("");
            }
        },
        editDocPanel: {
            id: "#editDocPanel",
            show: function () {
                rightPanel.hide();
                $(this.id).show();
            },
            hide: function () {
                $(this.id).hide();
                $("#editDocContainer").html("");
            }
        },
        viewDocPanel: {
            id: "#viewDocPanel",
            show: function (docSrc) {
                rightPanel.hide();
                $(this.id).show();
                docSrc = "/Document/GetPDFFile?DocPath=" + encodeURI(docSrc);
                $("#ifrmViewDoc").attr("src", docSrc + "#scrollbar=0&statusbar=0");
                rightPanel.docOperationPanel.show();
            },
            showOnlySelf: function (docSrc) {
                rightPanel.hide();
                $(this.id).show();
                docSrc = "/Document/GetPDFFile?DocPath=" + encodeURI(docSrc);
                $("#ifrmViewDoc").attr("src", docSrc + "#scrollbar=0&statusbar=0");
            },
            hide: function () {
                $("#ifrmViewDoc").attr("src", "");
                $(this.id).hide();
            }
        },
        workflowFormPanel: {
            id: "#workflowFormPanel",
            show: function () {
                rightPanel.hide();
                rightPanel.docItemsPanel.hide();
                rightPanel.docOperationPanel.hide();
                $(this.id).show();
            },
            hide: function () {
                $(this.id).hide();
            }
        },
        docItemsPanel: {
            id: "#docItemsPanel",
            show: function () {
                rightPanel.hide();
                rightPanel.docOperationPanel.hideOperationAdd();
                $(this.id).show();

            },
            hide: function () {
                $(this.id).hide();
                rightPanel.docOperationPanel.hide();
            }
        },
        docOperationPanel: {
            id: "#docOperationPanel",
            show: function () {
                $(this.id).show();
            },
            hide: function () {
                $(this.id).hide();
            },
            hideOperationAdd: function () {
                $("#operationAdd").hide();
            },
            showOperationAdd: function () {
                $("#operationAdd").show();
            },
            hideOperationEdit: function () {
                $("#operationEdit").hide();
            },
            showOperationEdit: function () {
                $("#operationEdit").show();
            }
        }
    };
    //绑定一般案件流程相关的数据加载和元素点击事件
    (function () {

        //如果不是流程的第一个环节，则需要展示已有文书树形列表
        if (constParams.IsNewWorkflow != "True") {
            //绑定文书树控件数据加载事件
            $("ul#docTree").bind("loadDocTreeData", function (e, callback) {
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
                    "WIID": constParams.WIID,
                    "AIID": constParams.AIID
                };

                $.ajax({
                    type: "GET",
                    cache: false,
                    url: ("/Document/GetDocTree?rad=" + Math.random()),
                    data: queryParams,
                    success: function (data) {
                        $.fn.zTree.init($("ul#docTree"), setting, data);

                        if (callback instanceof Function) {
                            callback();
                        }
                    }
                });
            });

            //绑定点击文书树节点事件
            $("ul#docTree").bind("clickDocTreeNode", function (e, docSrc) {
                rightPanel.viewDocPanel.showOnlySelf(docSrc);
            });
        }

        //绑定左侧操作面板里面的文书按钮加载事件
        $("#docBtnContainer").bind("loadDocBtns", function (e, parameter, callback) {
            var $docBtnContainer = $(this);
            $docBtnContainer.empty();

            var queryParams = {
                "WIID": constParams.WIID,
                "ADID": constParams.ADID
            };
            $.ajax({
                type: "GET",
                cache: false,
                url: ("/Document/GetDocBtns?rad=" + Math.random()),
                data: queryParams,
                success: function (data) {

                    $.each(data, function (i, item) {
                        var $clearDiv = $("<div class='clear'></div>");
                        $docBtnContainer.append($clearDiv);

                        var $btnHTML = $("<a class='btn docBtn' style='width:92%' href='javascript:void(0);' count='" + item.Count + "'"
                            + " isUnique='" + item.IsUnique + "' IsRequired='" + item.IsRequired + "' DDID='" + item.DDID + "'>"
                            + item.DDName
                            + "</a>");
                        if (item.IsRequired == "1") {
                            $btnHTML.html("*" + item.DDName).css("color", "red");
                        }

                        //必填文书背景色
                        //if (item.IsRequired == "1" && item.Count == "") {
                        //    $btnHTML.addClass("btn-warning");
                        //} else if (item.IsRequired == "1" && item.Count != "") {
                        //    $btnHTML.addClass("btn-success");
                        //}
                        $docBtnContainer.append($btnHTML);

                        if (item.Count != "") {
                            var $tagHTML = $("<span class='inf_link'>" + item.Count + "</span>");
                            $docBtnContainer.append($tagHTML);
                        }
                    });

                    if (callback instanceof Function) {
                        callback(parameter);
                    }
                }
            });
        });

        //绑定左侧操作面板里面的文书按钮 Click 事件
        $("#docBtnContainer").delegate(".docBtn", "click", function () {
            var parameter = {
                DDID: $(this).attr("DDID"),
                IsUnique: $(this).attr("isUnique"),
                Count: $(this).attr("count")
            };
            //触发右侧文书列表数据加载事件
            $("#docItemsContainer").trigger("loadDocItems", [parameter, function (parameter) {
                if (parameter.Count != "" && parameter.IsUnique == 1) {
                    $("#docItemsContainer .grid")[0].click();
                }
                else {
                    $("#docOperationPanel").trigger("AddDoc", parameter.DDID);
                }
            }]);
        });

        //绑定右侧文书列表数据加载事件
        $("#docItemsContainer").bind("loadDocItems", function (e, parameter, callback) {
            var $docItemsContainer = $(this);
            $docItemsContainer.empty();

            var queryParams = {
                "WIID": constParams.WIID,
                "DDID": parameter.DDID,
                "ADID": constParams.ADID
            };

            $.ajax({
                type: "GET",
                cache: false,
                url: ("/Document/GetDocItems?rad=" + Math.random()),
                data: queryParams,
                success: function (data) {
                    //判断 docItemsPanel 显示或隐藏
                    if (data != null && data.length > 0) {
                        rightPanel.docItemsPanel.show();
                    } else {
                        rightPanel.docItemsPanel.hide();
                    }

                    //遍历添加文书项
                    $.each(data, function (i, item) {
                        //定义
                        var $grid = $("<div class='grid' DDID='" + parameter.DDID
                            + "' IsUnique='" + parameter.IsUnique
                            + "' DocTypeID='" + item.DocTypeID
                            + "' DIName='" + item.DIName
                            + "' DIID='" + item.DIID
                            + "' DocSrc='" + item.DocSrc
                            + "'></div>");
                        var $content = $("<div class='grid-content'>");
                        var $avatar = $("<div class='c_avatar'><img alt='PDF' src=\"/Images/PDF.png\"></div>");
                        var $info = $("<div class='conversation-info'></div>");
                        var $title = $("<div class='conversation-title'><span>" + item.DIName + "</span></div>");
                        var $time = $("<div class='conversation-date-status'>" + item.CreatedTime + "</div>");
                        var $clear = $("<div class='clear'></div>");
                        //结构
                        $title.appendTo($info);
                        $time.appendTo($info);
                        $avatar.appendTo($content);
                        $info.appendTo($content);
                        $clear.appendTo($content);
                        $content.appendTo($grid);
                        //插入到列表中
                        $docItemsContainer.prepend($grid);
                    });

                    if (callback instanceof Function) {
                        callback(parameter);
                    }
                }
            });
        });

        //绑定右侧文书列表中文书项的点击事件
        $("#docItemsContainer").delegate(".grid", "click", function () {
            $item = $(this);

            //获取当前选中文书的相关参数
            var DIID = $item.attr("DIID");
            var docSrc = $item.attr("DocSrc");
            var isUnique = $item.attr("IsUnique");
            var DDID = $item.attr("DDID");
            var docTypeID = $item.attr("DocTypeID");

            //移除选中文书的样式，并设置当前选中文书的样式
            $("#docItemsContainer .grid").removeClass("selectItem");
            $(this).addClass("selectItem");

            rightPanel.viewDocPanel.show(docSrc);

            //控制新增按钮的显示与隐藏
            if (isUnique == "1") {
                rightPanel.docOperationPanel.hideOperationAdd();
            } else {
                rightPanel.docOperationPanel.showOperationAdd();
            }

            //控制修改该按钮的显示与隐藏
            if (docTypeID == "2") {
                rightPanel.docOperationPanel.hideOperationEdit();
            } else {
                rightPanel.docOperationPanel.showOperationEdit();
            }

            //绑定新增按钮事件
            $("#operationAdd").unbind("click");
            $("#operationAdd").click(function () {
                $("#docItemsContainer .grid").removeClass("selectItem");
                $("#docOperationPanel").trigger("AddDoc", DDID);
            });

            //绑定修改按钮事件
            $("#operationEdit").unbind("click");
            $("#operationEdit").click(function () {
                $("#docOperationPanel").trigger("EditDoc", [DIID, DDID]);
            });

            //绑定删除按钮事件
            $("#operationDelete").unbind("click");
            $("#operationDelete").click(function () {
                $("#docOperationPanel").trigger("DeleteDoc", [DIID, DDID]);
            });
        });

        //提交流程
        $("#btn_workflowSubmit").unbind("click");
        $("#btn_workflowSubmit").click(function () {
            $("#btn_workflowSubmit").attr("disabled", "disabled");
            var Docs = ValidateRequiredDocs()
            if (Docs && confirm("是否确认提交流程至下一环节？")) {
                $(".workflowForm").submit();
            } else {
                $("#btn_workflowSubmit").removeAttr("disabled");
            }
        })

        //删除流程
        $("#btn_workflowdDelete").click(function () {
            if (confirm("是否确认删除此工作流程？")) {
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "/Workflow/DeleteWorkflowByWIID",
                    data: { WIID: $("#WIID").val() },
                    cache: false,
                    success: function (date) {
                        if (date == "True") {
                            alert("删除成功！");
                            window.history.go(-1);
                        }
                        else {
                            alert("删除失败！");
                        }
                    }
                });
            }
        })


        //绑定添加文书事件
        $("#docOperationPanel").bind("AddDoc", function (e, DDID) {
            var addParams = {
                "WIID": constParams.WIID,
                "AIID": constParams.AIID,
                "ADID": constParams.ADID,
                "DDID": DDID
            };

            $.get("/Document/AddDocumentGo?rad=" + Math.random(),
                addParams,
                function (data) {
                    rightPanel.addDocPanel.show();
                    $("#addDocContainer").html(data);

                    //将滚动条置顶
                    $("#btn-scrollup").click();
                }, "html");
        });

        //绑定修改文书事件
        $("#docOperationPanel").bind("EditDoc", function (e, DIID, DDID) {
            var editParams = {
                "WIID": constParams.WIID,
                "AIID": constParams.AIID,
                "ADID": constParams.ADID,
                "DIID": DIID,
                "DDID": DDID
            };
            $.get("/Document/EditDocumentGo",
                editParams,
                function (data) {
                    rightPanel.editDocPanel.show();
                    $("#editDocContainer").html(data);
                }, "html");
        });

        //绑定删除文书事件
        $("#docOperationPanel").bind("DeleteDoc", function (e, DIID, DDID) {
            var delParams = {
                "DIID": DIID
            };
            $.get("/Document/RemoveDocument",
                delParams,
                function () {
                    $("ul#docTree").trigger("loadDocTreeData");
                    $("#docBtnContainer").trigger("loadDocBtns", [DDID, function (DDID) {
                        $(".docBtn[DDID='" + DDID + "']").click();
                    }]);
                });
        });

        //绑定操作面板流程环节按钮点击事件
        $("#btnWorkflowForm").click(function () {
            rightPanel.workflowFormPanel.show();
        });
    })();

    //触发文书树控件数据加载事件
    if (constParams.IsNewWorkflow != "True") {
        $("ul#docTree").trigger("loadDocTreeData", function () {
            var treeObj = $.fn.zTree.getZTreeObj("docTree");
            var treeNode = treeObj.getNodeByParam("name", '立案审批表', null);
            treeObj.selectNode(treeNode);
            $("ul#docTree").trigger("clickDocTreeNode", treeNode.value);
        });
    }

    //触发数据加载事件
    $("#docBtnContainer").trigger("loadDocBtns", [constParams.DDID, function (DDID) {
        $(".docBtn[DDID='" + DDID + "']").click();
    }]);
});
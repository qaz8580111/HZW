$(document).ready(function () {

    //定义并设置相关变量
    var WIID = $("#WIID").val();

    //绑定数据加载和元素点击事件
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
                    showLine: true
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
                "WIID": WIID
            };

            $.ajax({
                type: "GET",
                cache: false,
                url: ("/Document/GetDocTree?rad=" + Math.random()),
                data: queryParams,
                success: function (data) {
                    $.fn.zTree.init($("ul#docTree"), setting, data);
                    var treeObj = $.fn.zTree.getZTreeObj("docTree");
                    var treeNode = treeObj.getNodeByParam("name", '立案审批表', null);
                    if (treeNode == null) {
                        treeNode = treeObj.getNodeByParam("name", '现场检查（勘验）笔录', null);
                    }
                    treeObj.selectNode(treeNode);
                    $("ul#docTree").trigger("clickDocTreeNode", treeNode.value);
                }
            });
        });

        //绑定点击文书树节点事件
        $("ul#docTree").bind("clickDocTreeNode", function (e, docSrc) {
            docSrc = "/Document/GetPDFFile?DocPath=" + encodeURI(docSrc);
            $("#ifrmViewDoc").attr("src", docSrc + "#scrollbar=0&statusbar=0");
        });
    })();
    //触发文书树控件数据加载事件
    $("ul#docTree").trigger("loadDocTreeData");
});
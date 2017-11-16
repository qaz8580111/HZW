<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchGW.aspx.cs" Inherits="WebService.SearchGW" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Cache-control" content="max-age=1700" />
    <meta name="viewport" content="width=device-width; initial-scale=1.3; minimum-scale=1.0; maximum-scale=2.0" />
    <meta name="MobileOptimized" content="240" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="format-detection" content="email=no" />
    <title></title>

    <script src="Jquery/jquery.min.js" type="text/javascript"></script>

    <style>
        .bluebuttoncss {
            font-family: "tahoma", "宋体";
            font-size: 12pt;
            color: #ffffff;
            border: 0px #93bee2 solid;
            border-bottom: #93bee2 1px solid;
            border-left: #93bee2 1px solid;
            border-right: #93bee2 1px solid;
            border-top: #93bee2 1px solid;
            background-color: #00c8ff;
            cursor: pointer;
            font-style: normal;
        }
    </style>

    <script type="text/javascript">

        function AddPage(type) {
            var curpage = $("#curpage").val();
            var searchName = $("#searchName").val();
            var GWType = $("#GWType").val();
            var account = "<%= account%>";
            //点击查询按钮
            if (type == 0) {
                $("#content").html("");
                curpage = 1
            }
            $.ajax({
                url: "SearchGWHandler.ashx",
                type: "get",
                data: {
                    curpage: curpage, searchName: searchName,
                    account: account, GWType: GWType
                },
                success: function (jsonData) {
                    if (jsonData) {
                        $("#nextPage").css("display", "");
                        var newJsonData = eval('(' + jsonData + ')');
                        var content = $("#content").html();
                        var newContent = content + newJsonData["text"];
                        $("#content").html(newContent);
                        $("#curpage").val(newJsonData["curpage"]);
                        if (newJsonData["curpage"] == 0) {
                            $("#nextPage").css("display", "none");
                        }
                    }
                },
                error: function () { }
            });
        }

        function Details(id) {
            var div = document.getElementById(id);
            var flow_id = div.getAttribute("flow_id");

            window.open("GWDetails.aspx?RUN_ID="
                + id + "&FLOW_ID=" + flow_id
                + "&Account=" + "<%= account%>");
        }

    </script>
</head>
<body>
    <div style="text-align: center; margin: 0 auto;">
        <input type="hidden" id="curpage" value="1" />
        <div style="text-align: left;">
            <select id="GWType" style="width: 25%;">
                <option value="102">发文</option>
                <option value="103">收文</option>
            </select>
            <input type="text" id="searchName" style="width: 45%;" />
            <input type="button" class="bluebuttoncss" value="查 询" onclick="AddPage(0)" style="width: 25%;" />
        </div>
        <div id="content" style="text-align: left;">
        </div>
        <div style="text-align: center; margin-top: 10px;">
            <input type="button" id="nextPage" class="bluebuttoncss" style="display: none; width: 100%; height: 30px;" value="下一页" onclick="AddPage(1)" />
        </div>
    </div>
</body>
</html>

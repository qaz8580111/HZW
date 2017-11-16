<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FLDetails.aspx.cs" Inherits="WebService.FLDetails" %>

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
        function GoBack() {
            window.location.href = "SearchFL.aspx";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: left;">
            适用的违则：<%=  wz %>
            <br />
            适用的罚则：<%=  fz %>
        </div>
        <div style="text-align: center; margin-top: 10px;">
            <input type="button" class="bluebuttoncss" style="width: 100%; height: 30px;" value="返 回" onclick="GoBack()" />
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GWDetails.aspx.cs" Inherits="WebService.GWDetails" %>

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
        .div {
            text-align: center;
            margin: auto;
        }

            .div table {
                width: 100%;
                border-collapse: collapse;
                border: 3px solid #F00;
            }

                .div table td {
                    border: 1px solid #F00;
                }

        .td-a {
            text-align: center;
            color: red;
        }

        .td-b {
            text-align: left;
        }

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
            window.location.href = "SearchGW.aspx?account=" + "<%= account%>";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" visible="false">
        <div style="text-align: center; font-size: 30px; padding-bottom: 30px;">
            <b style="color: red;">收文单</b>
        </div>
        <div class="div">
            <table>
                <tr>
                    <td class="td-a" style="width: 15%">收文人</td>
                    <td class="td-b" style="width: 35%"><%= result.Data_1 %></td>
                    <td class="td-a" style="width: 15%">处理时间</td>
                    <td class="td-b" style="width: 35%"><%= result.Data_3 %></td>
                </tr>
                <tr>
                    <td class="td-a">来文单位</td>
                    <td colspan="3" class="td-b"><%= result.Data_32 %></td>
                </tr>
                <tr>
                    <td class="td-a">文件标题</td>
                    <td colspan="3"><%= result.Run_Name %></td>
                </tr>
                <tr>
                    <td class="td-a">文号</td>
                    <td colspan="3" class="td-b"><%= result.Data_5 %> (<%= result.Data_28 %>)<%= result.Data_29 %> 号（期）</td>
                </tr>
                <tr>
                    <td class="td-a" style="width: 15%">收文编号</td>
                    <td class="td-b"><%= result.Data_33 %></td>
                    <td class="td-a" style="width: 15%">收文时间</td>
                    <td class="td-b"><%= result.Data_34 %></td>
                </tr>
                <tr>
                    <td class="td-a">文件类别</td>
                    <td colspan="3" class="td-b"><%= result.Data_27 %></td>
                </tr>
                <tr>
                    <td class="td-a">保留期限</td>
                    <td colspan="3" class="td-b"><%= result.Data_10 %></td>
                </tr>
                <tr>
                    <td class="td-a">签发单位</td>
                    <td colspan="3" class="td-b"><%= result.Data_15 %></td>
                </tr>
                <tr>
                    <td colspan="4" class="td-a">文件内容或其他说明</td>
                </tr>
                <tr>
                    <td colspan="4"><%= result.Data_18 %></td>
                </tr>
                <tr>
                    <td colspan="4" class="td-a">局办公室意见</td>
                </tr>
                <tr>
                    <td colspan="4"><%= result.Data_19 %><br />
                        <strong>签名：<%= result.Data_30 %> 时间：<%= result.Data_31 %></strong></td>
                </tr>
                <tr>
                    <td colspan="4" class="td-a">局领导意见</td>
                </tr>
                <tr>
                    <td colspan="4"><%= JLDYJ %></td>
                </tr>
                <tr>
                    <td colspan="4" class="td-a">相关科室意见</td>
                </tr>
                <tr>
                    <td colspan="4"><%= XGKSYJ %></td>
                </tr>
                <tr>
                    <td colspan="4" class="td-a">相关单位情况确认</td>
                </tr>
                <tr>
                    <td colspan="4"><%= XGDWQKQR %></td>
                </tr>
                <tr>
                    <td colspan="4" class="td-a">相关科室情况确认</td>
                </tr>
                <tr>
                    <td colspan="4"><%= XGKSQKQR %></td>
                </tr>
                <tr>
                    <td colspan="4" class="td-a">相关单位办理结果反馈</td>
                </tr>
                <tr>
                    <td colspan="4"><%= XGDWBLJGFK %></td>
                </tr>
                <tr>
                    <td colspan="4" class="td-a">相关科室办理结果反馈</td>
                </tr>
                <tr>
                    <td colspan="4"><%= XGKSBLJGFK %></td>
                </tr>
            </table>
        </div>
    </form>
    <form id="form2" runat="server" visible="false">
        <div style="text-align: center; font-size: 30px; padding-bottom: 30px;">
            <b style="color: red;">发文单</b>
        </div>
        <div class="div">
            <table>
                <tr>
                    <td class="td-a" style="width: 15%">拟稿人</td>
                    <td class="td-b" style="width: 35%"><%= result.Data_1 %></td>
                    <td class="td-a" style="width: 15%">拟稿时间</td>
                    <td class="td-b" style="width: 35%"><%= result.Data_3 %></td>
                </tr>
                <tr>
                    <td class="td-a">发文单位</td>
                    <td colspan="3" class="td-b"><%= result.Data_4 %></td>
                </tr>
                <tr>
                    <td class="td-a">文件标题</td>
                    <td colspan="3"><%= result.Run_Name %></td>
                </tr>
                <tr>
                    <td class="td-a">文号</td>
                    <td colspan="3" class="td-b"><%= result.Data_5 %> (<%= result.Data_28 %>)<%= result.Data_29 %> 号（期）</td>
                </tr>
                <tr>
                    <td class="td-a">文件类别</td>
                    <td colspan="3" class="td-b"><%= result.Data_27 %></td>
                </tr>
                <tr>
                    <td class="td-a">保留期限</td>
                    <td colspan="3" class="td-b"><%= result.Data_10 %></td>
                </tr>
                <tr>
                    <td class="td-a">签发单位</td>
                    <td colspan="3" class="td-b"><%= result.Data_15 %></td>
                </tr>
                <tr>
                    <td colspan="4" class="td-a">文件内容或其他说明</td>
                </tr>
                <tr>
                    <td colspan="4"><%= result.Data_18 %></td>
                </tr>
                <tr>
                    <td colspan="4" class="td-a">局办公室意见</td>
                </tr>
                <tr>
                    <td colspan="4"><%= result.Data_19 %><br />
                        <strong>签名：<%= result.Data_30 %> 时间：<%= result.Data_31 %></strong></td>
                </tr>
                <tr>
                    <td colspan="4" class="td-a">局领导意见</td>
                </tr>
                <tr>
                    <td colspan="4"><%= JLDYJ %></td>
                </tr>
                <tr>
                    <td colspan="4" class="td-a">相关科室意见</td>
                </tr>
                <tr>
                    <td colspan="4"><%= XGKSYJ %></td>
                </tr>
                <tr>
                    <td colspan="4" class="td-a">相关单位情况确认</td>
                </tr>
                <tr>
                    <td colspan="4"><%= XGDWQKQR %></td>
                </tr>
                <tr>
                    <td colspan="4" class="td-a">相关科室情况确认</td>
                </tr>
                <tr>
                    <td colspan="4"><%= XGKSQKQR %></td>
                </tr>
                <tr>
                    <td colspan="4" class="td-a">相关单位办理结果反馈</td>
                </tr>
                <tr>
                    <td colspan="4"><%= XGDWBLJGFK %></td>
                </tr>
                <tr>
                    <td colspan="4" class="td-a">相关科室办理结果反馈</td>
                </tr>
                <tr>
                    <td colspan="4"><%= XGKSBLJGFK %></td>
                </tr>
            </table>
        </div>
    </form>
    <div style="text-align: center; margin-top: 10px;">
        <input type="button" class="bluebuttoncss" style="width: 100%; height: 30px;" value="返 回" onclick="GoBack()" />
    </div>
</body>
</html>

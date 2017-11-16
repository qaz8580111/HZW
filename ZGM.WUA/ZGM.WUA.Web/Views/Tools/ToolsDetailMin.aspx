<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToolsDetailMin.aspx.cs" Inherits="ZGM.WUA.Web.Views.Tools.ToolsDetailMin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>绘图信息概要</title>
    <link href="../../Styles/listmin.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <%--<div>
            <button id="minbtn1">详情</button>
            <button id="minbtn2">关闭</button>
            <button id="minbtn3">轨迹回放</button>
        </div>--%>
        <div class="main">
            <div class="top">
                <div class="t_title">绘图信息</div>
                <div class="btnclose" onclick="detailMin.close();"><span></span></div>
                <div class="t_left">
                    <div class="eventsminicoin"></div>
                </div>
                <div class="m_spilit"></div>
                <div class="t_right">
                  <%--  <span class="eventsminname"></span>--%>
                    <%--<span class="pposition"></span>--%>
                    <textarea style="resize: none;background: transparent none repeat scroll 0% 0%; border-style: none; height: 78px; width: 112px;" class="pposition"></textarea>
                </div>
            </div>

            <div class="bottom">
                <div class="proute del" title="删除" onclick="detailMin.deleteGraphic();"></div>
                <%--<div class="proute"></div>--%>
                <%--<div class="pmessage"></div>--%>
                <%--<div class="pcase"></div>--%>
               <%-- <div class="pother" title="查询周边" onclick="detailMin.foundCircum();"></div>--%>
            </div>
            <%--<div class="policecase">
                <div></div>
                <span>0</span>
            </div>--%>
        </div>
    </form>
</body>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/Tools.detailmin.js"></script>
</html>

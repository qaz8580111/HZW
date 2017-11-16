<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventsDetailMin.aspx.cs" Inherits="ZGM.WUA.Web.Views.Events.EventsDetailMin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>事件概要</title>
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
                <div class="t_title">事件信息</div>
                <div class="btnclose" onclick="detailMin.close();"><span></span></div>
                <div class="t_left">
                   <%-- <div class="eventsminicoin"></div>--%>
                    <img class="picoin" src="/images/概要图标/事件.png" />
                </div>
                <div class="m_spilit"></div>
                <div class="t_right">
                    <span class="eventsminname"></span>
                    <span class="pposition"></span>
                </div>
            </div>

            <div class="bottom">
                <div class="pdetail" title="详情" onclick="detailMin.initDetail();"></div>
                <%--<div class="proute"></div>--%>
                <%--<div class="pmessage"></div>--%>
                <%--<div class="pcase"></div>--%>
                <div class="pother" title="查询周边" onclick="detailMin.foundCircum();"></div>
                  <div id="review" class="pdetail pdetailCamera" title="回放" onclick="detailMin.initHistoryDetail();" style="display:none"></div>
            </div>
            <div class="policecase">
                <div></div>
            </div>
        </div>
    </form>
</body>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/events.detailmin.js"></script>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarDetailMin.aspx.cs" Inherits="ZGM.WUA.Web.Views.Car.CarDetailMin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>车辆信息</title>
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
                <div class="t_title">车辆信息</div>
                <div class="btnclose" onclick="detailMin.close();"><span></span></div>
                <div class="t_left">
                    <div class="cicoin"></div>
                </div>
                <div class="m_spilit"></div>
                <div class="t_right">
                    <%--<span class="cname"></span>--%>
                    <span class="cnumber" style="margin-top:22px;width:80px"></span>
                    <div class="pposition">执法车辆</div>
                </div>
            </div>

            <div class="bottom">
                <%--<div class="pdetail" title="详情" onclick="detailMin.initDetail();"></div>--%>
                <div class="proute" title="轨迹回放" onclick="detailMin.traceReplay();"></div>
                <%--<div class="pmessage"></div>--%>
                <div class="pcase" title="巡查区域" onclick="detailMin.getSearchArea();"></div>
                <div class="pother" title="查询周边" onclick="detailMin.foundCircum();"></div>
            </div>
            <div class="policecase">
                <div></div>
                <span>0</span>
            </div>
        </div>
    </form>
</body>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/car.detailmin.js"></script>
</html>


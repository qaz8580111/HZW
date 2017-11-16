<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PartsDetailMin.aspx.cs" Inherits="ZGM.WUA.Web.Views.Parts.PartsDetailMin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>部件详细</title>
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
                <div class="t_title">部件信息</div>
                <div class="btnclose"><span onclick="detailMin.close();"></span></div>
                <div class="t_left">
                    <div class="partsminicoin"></div>
                </div>
                <div class="m_spilit"></div>
                <div class="t_right">
                   
                    <div class="partsminposition"></div>
                </div>
            </div>

            <div class="bottom">
                <div class="pdetail" title="详情" onclick="detailMin.initDetail();"></div>
            </div>
        </div>
    </form>
</body>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/parts.detailmin.js"></script>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BMDDetailMin.aspx.cs" Inherits="ZGM.WUA.Web.Views.BMD.BMDDetailMin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
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
                <div class="t_title">白名单人员信息</div>
                <div class="btnclose"><span onclick="detailMin.close();"></span></div>
                <div class="t_left">
                    <img class="picoin" src="/images/pmin/pminicoin_03.png" />
                </div>
                <div class="m_spilit"></div>
                <div class="t_right">
                    <span class="pname" style="width:auto"></span>
                    <div class="pposition"></div>
                </div>
            </div>

            <div class="bottom">
                <div class="pdetail" title="详情" onclick="detailMin.initDetail();"></div>
                <%--<div class="proute" title="轨迹回放" onclick="detailMin.traceReplay();"></div>
                <div class="pmessage" title="消息"></div>
                <div class="pcase" title="巡查区域"></div>
                <div class="pother" title="查询周边" onclick="detailMin.foundCircum();"></div>--%>
            </div>
          <%--  <div class="policecase">
                <div></div>
                <span>0</span>
            </div>--%>
        </div>
    </form>
</body>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/BMD.detailmin.js"></script>
</html>

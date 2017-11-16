·<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Head.aspx.cs" Inherits="ZGM.WUA.Web.Views.Head" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../Styles/Head.css" rel="stylesheet" />
    <title></title>
    <style>
        #Div1 img:hover{
        opacity:0.6;
        cursor: pointer   ;
        }

    </style>
</head>
<body style="background-color: #0e1f2b">
    <div id="content" style="height: 50px; min-width: 729px; position: absolute; left: 35px; text-align: left;top:0px;">
        <%--<span style="vertical-align: middle; height: 100%; display: inline-block"></span>--%>
        <div style="float: left" class="centreDiv labelNavigation">
             <a class="commandCentre " onclick="headNavigation.commandCentreOnClick()">
            <img class="commandCentre" src="../Images/Head/staff_first_s.png" style="vertical-align:sub;" onclick="headNavigation.commandCentreOnClick()" />
            <span>指挥中心</span> </a>
        </div>
        <div style="float: left" class="centreDiv labelNavigationFalse">
            <a class="reportCentre " onclick="headNavigation.reportCentreClick()">
            <img class="reportCentre imgMar" src="../Images/Head/Navigation_report.png" style="vertical-align: sub;" onclick="headNavigation.reportCentreClick()" />
           <span> 报表中心</span></a>
        </div>
        <div style="float: left" class="centreDiv labelNavigationFalse">
            <a class="bussnessCentre " onclick="headNavigation.bussnessCentreClick()">
            <img class="bussnessCentre imgMar" src="../Images/Head/Navigation_bussness.png" style="vertical-align: sub;" onclick="headNavigation.bussnessCentreClick()" />
            业务中心</a>
        </div>
        <div style="float: left" class="centreDiv  labelNavigationFalse">
             <a class="officeCentre " onclick="headNavigation.officeCentreClick()">
            <img class="officeCentre imgMar" src="../Images/Head/Navigation_office.png" style="vertical-align: sub;" onclick="headNavigation.officeCentreClick()" />
           协同办公</a>
        </div>
        <div style="float: left" class="centreDiv labelNavigationFalse">
             <a class="engineeringCentre " onclick="headNavigation.engineeringCentreClick()">
            <img class="engineeringCentre imgMar" src="../Images/Head/Navigation_engineering.png" style="vertical-align: sub;" onclick="headNavigation.engineeringCentreClick()" />
           工程管理</a>
        </div>

        <div style="float: left" class="centreDiv labelNavigationFalse">
            <a class="cqCentre " onclick="headNavigation.cqCentreClick()">
            <img class="cqCentre imgMar" src="../Images/Head/Navigation_cq.png" style="vertical-align: sub;" onclick="headNavigation.cqCentreClick()" />
            拆迁管理</a>
        </div>
        <div style="float: left" class="centreDiv labelNavigationFalse">
            <a class="nonconformingBuildingCentre " onclick="headNavigation.nonconformingBuildingCentreClick()">
            <img class="nonconformingBuildingCentre imgMar" src="../Images/Head/Navigation_non-conformingBuilding.png" style="vertical-align: sub;margin-top:-6px" onclick="headNavigation.nonconformingBuildingCentreClick()" />
            违建管理</a>
        </div>
        <div style="float: left" class="centreDiv labelNavigationFalse">
                 <a class="systemCentre " onclick="headNavigation.systemCentreClick()">
            <img class="systemCentre imgMar" src="../Images/Head/Navigation_1system.png" style="vertical-align: sub;" onclick="headNavigation.systemCentreClick()" />
       系统管理</a>
        </div>

  </div>

    <div id="Div1" style="height: 50px; width: 200px; position: absolute; right: 25px; text-align: right;top:0px">
        <%--<span style="vertical-align: middle; height: 100%; display: inline-block"></span>--%>

        <label class="Date"></label>
        <label class="Time"></label>
        <img src="../Images/Head/Navigation_logout.png" style="margin-left:5px; height: 14px; width: 14px" />
    </div>

    <%--左侧背景图--%>
    <div id="left" style="height: 50px; float: left">
        <div style="height: 50px; width: 250px; float: left" class="leftImg"></div>
        <div style="height: 50px; float: left" class="centerImg"></div>
        <div style="height: 50px; width: 245px; float: left;" class="rightImg"></div>
    </div>
    <%--右侧背景图--%>
    <div id="right" style="height: 50px; width: 230px; float: right;">
        <%-- <div style="height:50px; width:60px ;float:left" class="right_leftImg"></div>
        <div style="height:50px; float:left;width:80px" class="right_centerImg"></div>
        <div style="height:50px;width:60px; float:left;" class="right_rightImg"></div>--%>
    </div>
</body>
</html>
<script src="../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../Scripts/head.js"></script>

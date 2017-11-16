<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FirstPage.aspx.cs" Inherits="ZGM.WUA.Web.Views.Bottow.FirstPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../Styles/Menu.css" rel="stylesheet" />
    <style>
        .td2Font {
           color:#b7d2ed;
        }
        .td2Font:hover {
           color:#dded0e;
          cursor:pointer;
          background:url('/images/border.png') no-repeat scroll 52% 0%;
        }
         .td2Selected{
           color:#dded0e;
          background:url('/images/border.png') no-repeat scroll 52% 0%;
        }
    </style>
</head>
<body>
    <%-- 首页 start--%>

    <table id="firstPage" class="fullTb" style="display: normal;">
        <tr>
            <td style="width: 14px; height: 90%">
                <h1 class="vertical" style="float: left; margin: 0px 10px 5px 10px">事件信息</h1>
            </td>
            <td style="width: 470px; min-width: 400px; height: 90%">
                <div style="height: 38%; margin-top: 10px">
                    <div style=" float: left" class="hoverImg" onclick="firstPage.showEventList(1);">
                        <img src="../../Images/Menu/Police_Woman_24px_1178418_easyicon.png" class="firstImg" style="float: left" />

                        <label class="lableFont labelMargin">智慧城管</label>
                        <label class="lableFont2 eventCount" id="zhcg">0</label>

                    </div>
                    <div style="float: left;" class="hoverImg" onclick="firstPage.showEventList(2);">
                        <img src="../../Images/Menu/leaderHead.png" class="firstImg" style="float: left" />

                        <label class="lableFont4 labelMargin" style="width: auto">群众热线投诉</label>
                        <label class="lableFont2 eventCount" id="qzrxts">0</label>

                    </div>

                     <div style=" float: left;" class="hoverImg" onclick="firstPage.showEventList(5);">
                        <img src="../../Images/Menu/VIDEO_CAMERA_29.png" style="float: left;width:16px;height:16px" />

                        <label class="lableFont labelMargin">监控发现</label>
                        <label class="lableFont2 eventCount" id="jkfx">0</label>
                    </div>
                </div>
                <div style="height: 38%">
                    <div style="float: left;" class="hoverImg" onclick="firstPage.showEventList(3);">
                        <img src="../../Images/Menu/petition.png" class="firstImg" style="float: left" />

                        <label class="lableFont labelMargin">社区上报</label>
                        <label class="lableFont2 eventCount" id="sqsb">0</label>

                    </div>
                    <div style=" float: left;" class="hoverImg" onclick="firstPage.showEventList(4);">
                        <img src="../../Images/Menu/staff_first.png" class="firstImg" style="float: left" />

                        <label class="lableFont4 labelMargin" style="width: auto">队员巡查发现</label>
                        <label class="lableFont2 eventCount" id="dyxcfx">0</label>

                    </div>
                    
                     <div style="float: left;" class="hoverImg" onclick="firstPage.showEventList(6);">
                        <img src="../../Images/Menu/Police_Man_24px_1178416_easyicon.png" class="firstImg" style="float: left" />

                        <label class="lableFont4 labelMargin" style="width: auto">领导巡查发现</label>
                        <label class="lableFont2 eventCount" id="ldxcfx">0</label>

                    </div>
                </div>
               <%-- <div style="height: 30%">
                    <div style=" float: left;" class="hoverImg" onclick="firstPage.showEventList(5);">
                        <img src="../../Images/Menu/VIDEO_CAMERA_29.png" style="float: left;width:16px;height:16px" />

                        <label class="lableFont labelMargin">监控发现</label>
                        <label class="lableFont2 eventCount" id="jkfx">0</label>

                    </div>
                    <div style="float: left;" class="hoverImg" onclick="firstPage.showEventList(6);">
                        <img src="../../Images/Menu/Police_Man_24px_1178416_easyicon.png" class="firstImg" style="float: left" />

                        <label class="lableFont4 labelMargin" style="width: auto">领导巡查发现</label>
                        <label class="lableFont2 eventCount" id="ldxcfx">0</label>

                    </div>
                </div>  --%>             
            </td>
            <td id="chartParent_1">
                 <div class="borderImg" style="float:left">
                </div>
                <div id="chart_1" style="height: 90px;margin-left:10px; float: left;">
                    <table style="width:270px;margin-left:0px">
                        <tr  style="width:100%;height:35px">
                            <td style="width:50%;text-align:center" class="td2Font"><div style="margin-left:0px"><img src="/Images/entrance/监控.png" style="width:16px;height:16px;vertical-align:middle;"/><span  style="margin-left:10px">监控</span></div></td>
                            <td style="width:50%;text-align:center" class="td2Font"><div style="margin-left:0px"><img src="/Images/entrance/人员.png" style="width:16px;height:16px;vertical-align:middle;"/><span style="margin-left:10px">人员</span></div</td>
                        </tr>
                         <tr  style="width:100%;height:35px">
                            <td style="width:50%;text-align:center" class="td2Font"><div style="margin-left:0px"><img src="/Images/entrance/事件.png" style="width:16px;height:16px;vertical-align:middle;"/><span style="margin-left:10px">事件</span></div></td>
                            <td style="width:50%;text-align:center" class="td2Font"><div style="margin-left:0px"><img src="/Images/entrance/车辆.png" style="width:16px;height:16px;vertical-align:middle;"/><span style="margin-left:10px">车辆</span></div</td>
                        </tr>
                      
                    </table>
                </div>
                <div class="borderImg">
                </div>
            </td>
            <td style="width: 14px; height: 90%">
                <h1 class="vertical" style="float: left; margin: 0px 10px 5px 10px">人员信息</h1>
            </td>
            <td id="chartParent_2">
                <div id="chart_2" style="height: 90px;"></div>
            </td>
        </tr>
    </table>
    <%--<div id="chart_1" style="position:absolute;width:300px;height:150px;top:0px;left:300px;z-index:100"></div>--%>
    <%-- 首页 End--%>
    <script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
    <%--echart图表--%>
    <script src="../../Scripts/Chart/echarts.simple.min.js"></script>
    <%--自定义js--%>
    <script src="../../Scripts/Bottow/FirstPage.js"></script>

</body>
</html>

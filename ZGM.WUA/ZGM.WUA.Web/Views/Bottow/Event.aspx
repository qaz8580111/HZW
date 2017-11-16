<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Event.aspx.cs" Inherits="ZGM.WUA.Web.Views.Bottow.Event" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../Styles/Bottow/Event.css" rel="stylesheet" />
</head>
<body>
    <table id="events" class="fullTb" style="display: normal;">
        <tr>
            <%--            <td class="centreAlign" style="width: 120px">
                <div onclick="eventZGM.showEventList(1);">
                    <label class="lableFont labelMargin">智慧城管:</label>
                    <span class="fewEvent" id="zhcgFew">11/</span>
                </div>
                <div onclick="eventZGM.showEventList(2);">
                    <label class="lableFont labelMargin">社区上报:</label>
                    <span class="fewEvent" id="sqsbFew">11/</span>
                </div>
                <div>
                    <label class="lableFont labelMargin">监控发现:</label>
                    <span class="fewEvent" id="jkfxFew">11/</span>
                </div>
            </td>
            <td class="centreAlign" style="width: 150px">
                <div>
                    <div style="float: left; margin: 0px 10px">
                        <label class="lableFont labelMargin">群众热线投诉:</label>
                        <span class="fewEvent" id="qzrxtsFew">11/</span>
                        <br />
                        <label class="lableFont labelMargin">队员巡查发现:</label>
                        <span class="fewEvent" id="dyxcfxFew">11/</span>
                        <br />
                        <label class="lableFont labelMargin">领导巡查发现:</label>
                        <span class="fewEvent" id="ldxcfxFew">11/</span>
                    </div>
                    <div class="borderImg">
                    </div>
                </div>
            </td>--%>
            <td style="width: 280px; height: 90%">
                <div style="height: 30%; margin-top: 10px;margin-left: 20px ">
                    <div onclick="eventZGM.showEventList(1);" style="color: rgb( 254, 137, 28 ); float: left" class="hoverImg">
                        <img src="../../Images/Menu/Police_Woman_24px_1178418_easyicon.png" class="firstImg" style="float: left" />

                        <label class="lableFont4 labelMargin" style="width: auto">智慧城管</label>
                        <label class="lableFont2" id="zhcg">0</label>

                    </div>
                    <div onclick="eventZGM.showEventList(2);" style="color: rgb( 224, 220, 60 ); float: left;" class="hoverImg">
                        <img src="../../Images/Menu/leaderHead.png" class="firstImg" style="float: left" />

                        <label class="lableFont4 labelMargin" style="width: auto">群众热线投诉</label>
                        <label class="lableFont2" id="qzrxts">0</label>

                    </div>
                </div>
                <div style="height: 30%;margin-left: 20px">
                    <div onclick="eventZGM.showEventList(3);" style="color: rgb( 254, 137, 28 ); float: left;" class="hoverImg">
                        <img src="../../Images/Menu/petition.png" class="firstImg" style="float: left" />

                        <label class="lableFont4 labelMargin" style="width: auto">社区上报</label>
                        <label class="lableFont2" id="sqsb">0</label>

                    </div>
                    <div onclick="eventZGM.showEventList(4);" style="color: rgb( 224, 220, 60 ); float: left;" class="hoverImg">
                        <img src="../../Images/Menu/staff_first.png" class="firstImg" style="float: left" />

                        <label class="lableFont4 labelMargin" style="width: auto">队员巡查发现</label>
                        <label class="lableFont2" id="dyxcfx">0</label>

                    </div>
                </div>
                <div style="height: 30%;margin-left: 20px ">
                    <div onclick="eventZGM.showEventList(5);" style="color: rgb( 254, 137, 28 ); float: left;" class="hoverImg">
                        <img src="../../Images/Menu/VIDEO_CAMERA_29.png" style="float: left;width:16px;height:16px" />

                        <label class="lableFont4 labelMargin" style="width: auto">监控发现</label>
                        <label class="lableFont2" id="jkfx">0</label>

                    </div>
                    <div onclick="eventZGM.showEventList(6);" style="color: rgb( 224, 220, 60 ); float: left;" class="hoverImg">
                        <img src="../../Images/Menu/Police_Man_24px_1178416_easyicon.png" class="firstImg" style="float: left" />

                        <label class="lableFont4 labelMargin" style="width: auto">领导巡查发现</label>
                        <label class="lableFont2" id="ldxcfx">0</label>

                    </div>
                </div>

            </td>
            <td >
                <div id="eventChart" style=" height:90px;"></div>
            </td>
            <td style="width: 130px; height: 90%">
                <div  class="" style="height: 30%">
                    <img src="../../Images/Menu/Event/rectangle_green.png" class="eventImg" />
                    <label class="eventCount labelMargin" style="width: auto">今日上报:</label>
                    <label class="eventCount" style="color: #0ce239; margin-left: 12px" id="jrsbNum">0</label>
                </div>
             <%--   <div onclick="eventZGM.showEventList(6);" class="hoverImg">
                    <img src="../../Images/Menu/Event/rectangle_org2.png" class="eventImg" />
                    <label class="eventCount labelMargin" style="width: auto">今日办理:</label>
                    <label class="eventCount" style="color: #ecd53d; margin-left: 12px" id="jrblNum">41</label>
                </div>--%>
                <div style="height: 30%" class="">
                    <img src="../../Images/Menu/Event/rectangle_org.png" class="eventImg" />
                    <label class="eventCount labelMargin" style="width: auto">今日结案:</label>
                    <label class="eventCount" style="color: #f39822; margin-left: 12px" id="jrjaNum">0</label>
                </div>
                <div  style="height: 30%" class="">
                    <img src="../../Images/Menu/Event/rectangle_red.png" class="eventImg" />
                    <label class="eventCount labelMargin" style="width: auto">超时未办理:</label>
                    <label class="eventCount" style="color: #eb3434" id="cswblNum">0</label>
                </div>
            </td>

        </tr>
    </table>
</body>
</html>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/base/Common.js"></script>
<script src="../../Scripts/Chart/echarts.simple.min.js"></script>
<script src="../../Scripts/Bottow/Event.js"></script>

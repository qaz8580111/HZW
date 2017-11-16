<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="ZGM.WUA.Web.Views.Bottow.Staff" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../Styles/Bottow/Staff.css" rel="stylesheet" />
</head>
<body>
    <%-- 人员页 start--%>
    <table id="staffs" class="fullTb" style="display: normal;">
        <tr>
            <td class="centreAlign" style="min-width:150px;margin-left:30px">
                <div class="hoverImg" onclick="staffZGM.showStaffList(22)">
                    <label class="lableFont labelMargin" style="cursor: pointer;text-align:right;">万达分队:</label>
                    <span class="fewEvent" id="firstFew">0/</span><span class="totalEvent" id="firstTotal">0</span>
                </div>
                <div class="hoverImg" onclick="staffZGM.showStaffList(23)">
                    <label class="lableFont labelMargin" style="text-align:right;">鄞南分队:</label>
                    <span class="fewEvent" id="secondFew">0/</span><span class="totalEvent" id="secondTotal">0</span>
                </div>
                <div class="hoverImg" onclick="staffZGM.showStaffList(26)">
                    <label class="lableFont labelMargin" style="text-align:right;">智慧分队:</label>
                    <span class="fewEvent" id="thirdFew">0/</span><span class="totalEvent" id="thirdTotal">0</span>
                </div>
            </td>
            <td class="leftAlign" style="min-width: 130px">
                <div style="margin: 0px 0px 0px 10px" class="hoverImg" onclick="staffZGM.showStaffList(24)">
                    <label class="lableFont labelMargin">长丰分队:</label>
                    <span style="" class="fewEvent" id="fourthFew">0/</span><span class="totalEvent" id="fourthTotal">0</span>
                </div>
                <div style="margin: 0px 0px 0px 10px" class="hoverImg" onclick="staffZGM.showStaffList(25)">
                    <label class="lableFont labelMargin">钟公庙分队:</label>
                    <span class="fewEvent" id="fiveFew">0/</span><span class="totalEvent" id="fiveTotal">0</span>
                </div>
                <div style="margin: 0px 0px 0px 10px" class="hoverImg" onclick="staffZGM.showStaffList(27)">
                    <label class="lableFont labelMargin">机动分队:</label>
                    <span style="" class="fewEvent" id="sixFew">0/</span><span class="totalEvent" id="sixTotal">0</span>
                </div>
            </td>
        <%--    <td class="centreAlign" style="min-width: 90px;">
                <div class="hoverImg" style="float: left; text-align: center; vertical-align: middle; width: 88px; padding-top: 15px" onclick="staffZGM.showStaffList(0)">

                    <label class="lableFont labelMargin" style="text-align:center;">中队部:</label>
                  <div style="text-align: center;">
                    <span class="fewEvent" id="Span1">0/</span><span class="totalEvent" id="Span2">0</span>
                      </div>

                </div>
                <div class="borderImg">
                </div>

            </td>--%>
              <td class="leftAlign" style="min-width: 130px;">
                  <div style="float: left;">
                      <div style="position:relative;top:15px">
                <div style="margin: 0px 0px 0px 10px" class="hoverImg" onclick="staffZGM.showStaffList(0)">
                    <label class="lableFont labelMargin">繁裕分队:</label>
                    <span style="" class="fewEvent" id="Span1">0/</span><span class="totalEvent" id="Span2">0</span>
                </div>
                <div style="margin: 0px 0px 0px 10px" class="hoverImg" onclick="staffZGM.showStaffList(93)">
                    <label class="lableFont labelMargin">中队部:</label>
                    <span class="fewEvent" id="Span3">0/</span><span class="totalEvent" id="Span4">0</span>
                </div>
                          </div>
                      </div>
               <div class="borderImg">
                </div>
            </td>

            <td class="centreAlign" style="height: 90px;" id="staffChart">
                <div id="chart_1" style="height: 90px; float: left; margin-left: 25px"></div>
                <div class="borderImg">
                </div>
            </td>
            <td style="min-width: 130px;text-align:right">
                <div style="margin: 0px 10px" >
                    <img src="../../Images/Menu/Staff/running_man_green.png" class="eventImg" />
                    <label class="lableFont labelMargin" style="width:60px;color:#2be338 ">人员在线</label>
                    <%--<label class="lableFont" style="color: #29f02e;width:35px" id="jrzxNum">0</label>--%>
                </div>
                <div style="margin: 0px 10px">
                    <img src="../../Images/Menu/Staff/running_man_black.png" class="eventImg" />
                    <label class="lableFont labelMargin" style="width:60px ;color:#a5a5a5"> 人员离线</label>
                    <%--<label class="lableFont" style="color: #a5a5a5;width:35px " id="jrlxNum">0</label>--%>
                </div>
                <div style="margin: 0px 10px"  >
                    <img src="../../Images/Menu/Staff/running_man_red.png" class="eventImg" />
                    <label class="lableFont labelMargin" style="width:60px ;color:#e41436">人员报警</label>
                    <%--<label class="lableFont" style="color: #ed2929;width:35px" id="jrbjNum">0</label>--%>
                </div>
            </td>

        </tr>
    </table>
    <%-- 人员页 End--%>
</body>
</html>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/base/Common.js"></script>
<script src="../../Scripts/Chart/echarts.simple.min.js"></script>
<script src="../../Scripts/Bottow/Staff.js"></script>

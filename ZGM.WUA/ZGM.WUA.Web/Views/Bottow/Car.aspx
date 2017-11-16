<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Car.aspx.cs" Inherits="ZGM.WUA.Web.Views.Bottow.Car" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../Styles/Bottow/Car.css" rel="stylesheet" />
</head>
<body>
    <table id="parts" class="fullTb" style="display: normal;">
        <tr>
            <td class="centreAlign" style="min-width: 90px">

                <div style="float: left; margin: 0px 20px" class="hoverImg" onclick="carZGM.showCarList(0)">
                    <img src="../../Images/Menu/Car/car_blue.png" />
                    <br />
                    <span class="ZFCar">执法车辆</span>
                    <br />
                    <span class="onlineCar">0/</span><span class="totalCar">0</span>
                </div>
                <div class="borderImg">
                </div>

            </td>
            <td class="centreAlign" style="min-width: 130px">

                <div style="float: left; margin: 0px 10px">
                    <div>
                        <img src="../../Images/Menu/Car/car_green.png" class="carImg" />
                        <label class="lableFont labelMargin">当前在线:</label>
                        <label class="lableFont" style="color: #0ce239" id="onlineCarNum">0</label>
                    </div>
                    <div >
                        <img src="../../Images/Menu/Car/car_black.png" class="carImg" />
                        <label class="lableFont labelMargin">当前离线:</label>
                        <label class="lableFont" style="color: #939495" id="offLineCarNum">0</label>
                    </div>
                    <div >
                        <img src="../../Images/Menu/Car/car_red.png" class="carImg" />
                        <label class="lableFont labelMargin">当前报警:</label>
                        <label class="lableFont" style="color: #d93131" id="reportCarNum">0</label>
                    </div>
                </div>
                <div class="borderImg">
                </div>

            </td>
            <td style="height: 80px;">
                <div id="carChartData" style="height: 90px;"></div>
            </td>
        </tr>
    </table>
</body>
</html>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/base/Common.js"></script>
<script src="../../Scripts/Chart/echarts.simple.min.js"></script>
<script src="../../Scripts/Bottow/Car.js"></script>

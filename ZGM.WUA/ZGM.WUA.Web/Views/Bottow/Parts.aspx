<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Parts.aspx.cs" Inherits="ZGM.WUA.Web.Views.Bottow.Parts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../Styles/Menu.css" rel="stylesheet" />
    <link href="../../Styles/Bottow/Parts.css" rel="stylesheet" />
</head>
<body>
    <%-- 部件页 End--%>
    <div id="opera">
        <table id="parts" class="fullTb" style="display: normal;">
            <tr>
                <td class="leftalign" style="width: 19%">

                    <h1 class="vertical" style="float: left; margin: 0px 10px 0px 10px; font-size: 14px">市政类</h1>
                    <div style="float: left; margin-left: 10px">
                        <div style="color: rgb( 254, 137, 28 )" class="hoverDiv" onclick="parent.parts.initPartsList('Road');">
                            <label class="partLabel">道路</label>
                            <label class="partLabel" id="roadNum">0</label>
                        </div>
                        <div style="color: rgb( 224, 220, 60 )" class="hoverDiv"  onclick="parent.parts.initPartsList('Bridge');">
                            <label class="partLabel">桥梁</label>
                            <label class="partLabel" id="bridgeNum">0</label>
                        </div>
                        <div style="color: rgb( 28, 241, 58 )" class="hoverDiv"  onclick="parent.parts.initPartsList('StreetLamp');">
                            <label class="partLabel">路灯</label>
                            <label class="partLabel" id="streetLightNum">0</label>
                        </div>
                        <div style="color: rgb( 34, 212, 244 )" class="hoverDiv" onclick="parent.parts.initPartsList('LandscapeLamp');">
                            <label class="partLabel">景观灯</label>
                            <label class="partLabel" id="redLightNum">0</label>
                        </div>
                    </div>
                    <div class="borderImg">
                    </div>

                </td>
                <td class="leftalign" style="width: 19%">

                    <h1 class="vertical" style="float: left; margin: 0px 10px; font-size: 14px">排水类</h1>
                    <div style="float: left; margin-left: 10px">
                        <div style="color: rgb( 254, 137, 28 )" class="hoverDiv" onclick="parent.parts.initPartsList('Pump');">
                            <label class="partlableFont2">泵站</label>
                            <label class="partLabel" id="pumpNum">0</label>
                        </div>
                        <div style="color: rgb( 224, 220, 60 )" class="hoverDiv"  onclick="parent.parts.initPartsList('CoverLoad');">
                            <label class="partlableFont2">井盖</label>
                            <label class="partLabel" id="wellCoverNum">0</label>
                        </div>
                    </div>
                    <div class="borderImg">
                    </div>


                </td>
                <td class="leftalign" style="width: 19%">

                    <h1 class="vertical" style="float: left; margin: 0px 10px; font-size: 14px">园林绿化</h1>
                    <div style="float: left; margin-left: 10px">
                        <div style="color: rgb( 254, 137, 28 )" class="hoverDiv" onclick="parent.parts.initPartsList('ParkGreen');">
                            <label class="partlableFont4">公园绿地</label>
                            <label class="partLabel" id="publicGreenNum">0</label>
                        </div>
                        <div style="color: rgb( 224, 220, 60 )" class="hoverDiv" onclick="parent.parts.initPartsList('LoadGreen');">
                            <label class="partlableFont4">道路绿地</label>
                            <label class="partLabel" id="roadGreenNum">0</label>
                        </div>
                        <div style="color: rgb( 28, 241, 58 )" class="hoverDiv" onclick="parent.parts.initPartsList('ProtectionGreen');">
                            <label class="partlableFont4">防护绿地</label>
                            <label class="partLabel" id="protectGreenNum">0</label>
                        </div>

                    </div>
                    <div class="borderImg">
                    </div>

                </td>
                <td class="leftalign" style="width: 19%">

                    <h1 class="vertical" style="float: left; margin: 0px 10px; font-size: 14px">市容环卫</h1>
                    <div style="float: left; margin-left: 10px">
                        <%--<div style="color: rgb( 254, 137, 28 )" class="hoverDiv">
                            <label class="partlableFont4">环卫车辆</label>
                            <label class="partLabel" id="sanitationCarNum">0</label>
                        </div>
                        <div style="color: rgb( 224, 220, 60 )" class="hoverDiv">
                            <label class="partlableFont4">保洁路段</label>
                            <label class="partLabel" id="cleaningSectionNum">0</label>
                        </div>--%>
                        <div style="color: rgb( 28, 241, 58 )" class="hoverDiv" onclick="parent.parts.initPartsList('Toilt');">
                            <label class="partlableFont4">公厕</label>
                            <label class="partLabel" id="publicWashroomNum">0</label>
                        </div>

                    </div>
                    <div class="borderImg">
                    </div>

                </td>
                <td class="leftalign" style="width: 19%">

                    <h1 class="vertical" style="float: left; margin: 0px 10px; font-size: 14px; height: 80px">城内河</h1>
                    <div style="float: left; margin-left: 10px">
                        <div style="color: rgb( 254, 137, 28 )" class="hoverDiv" onclick="parent.parts.initPartsList('River');">
                            <label class="partLabel">河道</label>
                            <label class="partLabel" id="riverNum">0</label>
                        </div>

                    </div>



                </td>
            </tr>
        </table>
    </div>
    <%-- 部件页 start--%>
    <script src="../../Scripts/base/jquery-1.8.2.min.js"></script>

    <script src="../../Scripts/Bottow/Parts.js"></script>
</body>
</html>

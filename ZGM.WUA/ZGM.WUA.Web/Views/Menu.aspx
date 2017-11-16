<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="ZGM.WUA.Web.Views.Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../Styles/Menu.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <%--  中间内容start --%>
        <div id="opera">
            <%-- 背景图--%>
            <%--<img src="../Images/Menu/picBottow.png" id="picBottow" />
            <img src="../Images/Menu/1.png" id="backgroundPic" />--%>

            <%-- 菜单页 start--%>
            <table class="tb" id="catalog" style="display: none;">
                <tr class="tr">
                    <td class="hoverImg" onclick="menu.eventOnClick()">
                        <div>
                            <img src="/Images/Menu/eventPic.png" />
                        </div>
                        <div>
                            <label class="contentMenu">111</label>
                        </div>
                        <div>
                            <label class="titleMenu">事件</label>
                        </div>
                    </td>
                    <td class="hoverImg" onclick="menu.staffOnClick()">
                        <div>
                            <img src="/Images/Menu/staff.png" class="hoverImg" />
                        </div>
                        <div>
                            <label class="contentMenu">111</label>
                        </div>
                        <div>
                            <label class="titleMenu">人员</label>
                        </div>
                    </td>
                    <td class="hoverImg" onclick="menu.cameraOnClick()">
                        <div>
                            <img src="/Images/Menu/camera.png" class="hoverImg" />
                        </div>
                        <div>
                            <label class="contentMenu">111</label>
                        </div>
                        <div>
                            <label class="titleMenu">监控</label>
                        </div>
                    </td>
                    <td class="hoverImg" onclick="menu.carOnClick()">
                        <div>
                            <img src="/Images/Menu/Car_48px_1177303_easyicon.png" class="hoverImg" />
                        </div>
                        <div>
                            <label class="contentMenu">111</label>
                        </div>
                        <div>
                            <label class="titleMenu">车辆</label>
                        </div>
                    </td>
                    <td class="hoverImg" onclick="menu.partsOnClick()">
                        <div>
                            <img src="/Images/Menu/parts.png" class="hoverImg" />
                        </div>
                        <div>
                            <label class="contentMenu">111</label>
                        </div>
                        <div>
                            <label class="titleMenu">部件</label>
                        </div>
                    </td>
                    <td class="hoverImg" onclick="menu.whiteListOnClick()">
                        <div>
                            <img src="/Images/Menu/whiteList.png" class="hoverImg" />
                        </div>
                        <div>
                            <label class="contentMenu">111</label>
                        </div>
                        <div>
                            <label class="titleMenu">白名单</label>
                        </div>
                    </td>
                    <td class="hoverImg" onclick="menu.engineeringOnClick()">
                        <div>
                            <img src="/Images/Menu/engineering.png" class="hoverImg" />
                        </div>
                        <div>
                            <label class="contentMenu">111</label>
                        </div>
                        <div>
                            <label class="titleMenu">工程</label>
                        </div>
                    </td>
                    <td class="hoverImg" onclick="menu.demolitionOnClick()">
                        <div>
                            <img src="/Images/Menu/Demolition.png" class="hoverImg" />
                        </div>
                        <div>
                            <label class="contentMenu">111</label>
                        </div>
                        <div>
                            <label class="titleMenu">拆迁</label>
                        </div>
                    </td>
                    <td class="hoverImg" onclick="menu.conformingBuildingOnClick()">
                        <div>
                            <img src="/Images/Menu/non-conformingBuilding.png" class="hoverImg" />
                        </div>
                        <div>
                            <label class="contentMenu">111</label>
                        </div>
                        <div>
                            <label class="titleMenu">违建</label>
                        </div>
                    </td>
                </tr>
            </table>
            <%-- 菜单页 End--%>

            <%-- 人员页 start--%>
            <table id="staff" class="fullTb" style="display: none">
                <tr>
                    <td style="width: 260px;">
                        <table>
                            <tr>
                                <td class="titleMenu">万达分队1</td>
                                <td class="contentMenu">11/11</td>
                                <td class="titleMenu">万达分队2</td>
                                <td class="contentMenu">22/22</td>
                            </tr>
                            <tr>
                                <td class="titleMenu">万达分队3</td>
                                <td class="contentMenu">3333</td>
                                <td class="titleMenu">万达分队4</td>
                                <td class="contentMenu">44/44</td>
                            </tr>
                            <tr>
                                <td class="titleMenu">万达分队5</td>
                                <td class="contentMenu">55/55</td>
                                <td class="titleMenu">万达分队6</td>
                                <td class="contentMenu">66/66</td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 120px; background-color: wheat"></td>
                    <td style="background-color: red"></td>
                    <td style="width: 120px; background-color: wheat"></td>
                </tr>
            </table>
            <%-- 人员页 End--%>

            <%-- 部件页 End--%>
            <table id="parts" class="fullTb" style="display: normal;">
                <tr>
                    <td class="leftalign" style="width: 19%">
                        <div>
                            <h1 class="vertical" style="float: left; margin: 10px">市政类</h1>
                            <div style="float: left; margin: 0px 20px">
                                <div style="color: rgb( 254, 137, 28 )">
                                    <label class="lableFont2">道路</label>
                                    <label class="lableFont" id="roadNum">41</label>
                                </div>
                                <div style="color: rgb( 224, 220, 60 )">
                                    <label class="lableFont2">桥梁</label>
                                    <label class="lableFont" id="bridgeNum">41</label>
                                </div>
                                <div style="color: rgb( 28, 241, 58 )">
                                    <label class="lableFont2">路灯</label>
                                    <label class="lableFont" id="streetLightNum">41</label>
                                </div>
                                <%--   <div style="color:rgb( 34, 212, 244 )">
                                 <label class="lableFont2">路灯</label>
                                 <label class="lableFont" id="roadNum">41</label>
                              </div>--%>
                            </div>
                            <div class="borderImg">
                            </div>
                        </div>
                    </td>
                    <td class="leftalign" style="width: 19%">
                        <div>
                            <h1 class="vertical" style="float: left; margin: 10px">排水类</h1>
                            <div style="float: left; margin: 0px 20px">
                                <div style="color: rgb( 254, 137, 28 )">
                                    <label class="lableFont2">泵站</label>
                                    <label class="lableFont" id="pumpNum">41</label>
                                </div>
                                <div style="color: rgb( 224, 220, 60 )">
                                    <label class="lableFont2">井盖</label>
                                    <label class="lableFont" id="wellCoverNum">41</label>
                                </div>
                            </div>
                            <div class="borderImg">
                            </div>
                        </div>

                    </td>
                    <td class="leftalign" style="width: 19%">
                        <div>
                            <h1 class="vertical" style="float: left; margin: 10px">园林绿化</h1>
                            <div style="float: left; margin: 0px 20px">
                                <div style="color: rgb( 254, 137, 28 )">
                                    <label class="lableFont">公共绿地</label>
                                    <label class="lableFont" id="publicGreenNum">41</label>
                                </div>
                                <div style="color: rgb( 224, 220, 60 )">
                                    <label class="lableFont">道路绿地</label>
                                    <label class="lableFont" id="roadGreenNum">41</label>
                                </div>
                                <div style="color: rgb( 28, 241, 58 )">
                                    <label class="lableFont">防护绿地</label>
                                    <label class="lableFont" id="protectGreenNum">41</label>
                                </div>

                            </div>
                            <div class="borderImg">
                            </div>
                        </div>
                    </td>
                    <td class="leftalign" style="width: 19%">
                        <div>
                            <h1 class="vertical" style="float: left; margin: 10px">市容环卫</h1>
                            <div style="float: left; margin: 0px 20px">
                                <div style="color: rgb( 254, 137, 28 )">
                                    <label class="lableFont">环卫车辆</label>
                                    <label class="lableFont" id="sanitationCarNum">41</label>
                                </div>
                                <div style="color: rgb( 224, 220, 60 )">
                                    <label class="lableFont">保洁路段</label>
                                    <label class="lableFont" id="cleaningSectionNum">41</label>
                                </div>
                                <div style="color: rgb( 28, 241, 58 )">
                                    <label class="lableFont">公厕</label>
                                    <label class="lableFont" id="publicWashroomNum">41</label>
                                </div>

                            </div>
                            <div class="borderImg">
                            </div>
                        </div>
                    </td>
                    <td class="leftalign" style="width: 19%">
                        <div>
                            <h1 class="vertical" style="float: left; margin: 10px">城内河</h1>
                            <div style="float: left; margin: 0px 20px">
                                <div style="color: rgb( 254, 137, 28 )">
                                    <label class="lableFont2">河道</label>
                                    <label class="lableFont" id="riverNum">41</label>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <%-- 部件页 start--%>


            <%-- 首页 start--%>
            <table id="firstPage" class="fullTb" style="display: none">
                <tr>
                    <td style="width: 14px; height: 90%">
                        <h1 class="vertical" style="float: left; margin: 10px">事件信息</h1>
                    </td>
                    <td style="width: 250px; height: 90%">
                        <div style="height: 30%; margin-top: 10px">
                            <div style="color: rgb( 254, 137, 28 ); float: left">
                                <img src="../Images/Menu/Police_Woman_24px_1178418_easyicon.png" class="firstImg" style="float: left" />

                                <label class="lableFont labelMargin">智慧城管</label>
                                <label class="lableFont2" id="Label1">41</label>

                            </div>
                            <div style="color: rgb( 224, 220, 60 ); float: left;">
                                <img src="../Images/Menu/leaderHead.png" class="firstImg" style="float: left" />

                                <label class="lableFont labelMargin">领导督办</label>
                                <label class="lableFont2" id="Label2">41</label>

                            </div>
                        </div>
                        <div style="height: 30%">
                            <div style="color: rgb( 254, 137, 28 ); float: left;">
                                <img src="../Images/Menu/VIDEO_CAMERA_29.png" class="firstImg" style="float: left" />

                                <label class="lableFont labelMargin">监控发现</label>
                                <label class="lableFont2" id="Label3">41</label>

                            </div>
                            <div style="color: rgb( 224, 220, 60 ); float: left;">
                                <img src="../Images/Menu/staff_first.png" class="firstImg" style="float: left" />

                                <label class="lableFont labelMargin">群众举报</label>
                                <label class="lableFont2" id="Label4">41</label>

                            </div>
                        </div>
                        <div style="height: 30%">
                            <div style="color: rgb( 254, 137, 28 ); float: left;">
                                <img src="../Images/Menu/petition.png" style="float: left" />

                                <label class="lableFont labelMargin">信访件</label>
                                <label class="lableFont2" id="Label5">41</label>

                            </div>
                            <div style="color: rgb( 224, 220, 60 ); float: left;">
                                <img src="../Images/Menu/Police_Man_24px_1178416_easyicon.png" class="firstImg" style="float: left" />

                                <label class="lableFont labelMargin">队员巡查</label>
                                <label class="lableFont2" id="Label6">41</label>

                            </div>
                        </div>

                    </td>
                    <td style="background-color: white"></td>
                    <td style="width: 14px; height: 90%">
                        <h1 class="vertical" style="float: left; margin: 10px">人员信息</h1>
                    </td>
                    <td style="background-color: white"></td>
                </tr>
            </table>
            <%-- 首页 End--%>
        </div>
        <%--  中间内容End --%>
    </form>
</body>
<script src="../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../Scripts/menu.js"></script>
</html>

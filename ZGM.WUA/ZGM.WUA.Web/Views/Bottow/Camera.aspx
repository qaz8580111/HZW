<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Camera.aspx.cs" Inherits="ZGM.WUA.Web.Views.Bottow.Camera" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../../Styles/Menu.css" rel="stylesheet" />
</head>
<body>
     <%-- 菜单页 start--%>
    <div id="opera">
        <table class="tb" id="catalog" style="display: normal;margin-top:10px">
            <tr class="tr">
                <td class="hoverImg" onclick="catalog.eventOnClick()">
                    <div>
                        <img src="/Images/Menu/eventPic.png" />
                    </div>
                  
                    <div>
                        <label class="titleMenu">事件</label>
                    </div>
                </td>
                <td class="hoverImg" onclick="catalog.staffOnClick()">
                    <div>
                        <img src="/Images/Menu/staff.png" class="hoverImg" />
                    </div>
                    
                    <div>
                        <label class="titleMenu">人员</label>
                    </div>
                </td>
                <td class="hoverImg" onclick="catalog.cameraOnClick()">
                    <div>
                        <img src="/Images/Menu/camera.png" class="hoverImg" />
                    </div>
                   
                    <div>
                        <label class="titleMenu">监控</label>
                    </div>
                </td>
                <td class="hoverImg" onclick="catalog.carOnClick()">
                    <div>
                        <img src="/Images/Menu/Car_48px_1177303_easyicon1.png" class="hoverImg" />
                    </div>
                    
                    <div>
                        <label class="titleMenu">车辆</label>
                    </div>
                </td>
                <td class="hoverImg" onclick="catalog.partsOnClick()">
                    <div>
                        <img src="/Images/Menu/parts.png" class="hoverImg" />
                    </div>
                  
                    <div>
                        <label class="titleMenu">部件</label>
                    </div>
                </td>
                <td class="hoverImg" onclick="catalog.whiteListOnClick()">
                    <div>
                        <img src="/Images/Menu/whiteList.png" class="hoverImg" />
                    </div>
                   
                    <div>
                        <label class="titleMenu">白名单</label>
                    </div>
                </td>
                <td class="hoverImg" onclick="catalog.engineeringOnClick()">
                    <div>
                        <img src="/Images/Menu/engineering.png" class="hoverImg" />
                    </div>
                  
                    <div>
                        <label class="titleMenu">工程</label>
                    </div>
                </td>
                <td class="hoverImg" onclick="catalog.demolitionOnClick()">
                    <div>
                        <img src="/Images/Menu/Demolition.png" class="hoverImg" />
                    </div>
                   
                    <div>
                        <label class="titleMenu">拆迁</label>
                    </div>
                </td>
                <td class="hoverImg" onclick="catalog.conformingBuildingOnClick()">
                    <div>
                        <img src="/Images/Menu/non-conformingBuilding.png" class="hoverImg" />
                    </div>
                   
                    <div>
                        <label class="titleMenu">违建</label>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <%-- 菜单页 End--%>
</body>
</html>

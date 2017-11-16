<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tools.aspx.cs" Inherits="ZGM.WUA.Web.Views.Bottow.Tools" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../../Styles/Menu.css" rel="stylesheet" />
</head>
<body>
     <%-- 菜单页 start--%>
    <div id="opera">
        <table class="tb" id="catalog" style="display: normal;margin-top:20px">
            <tr class="tr">
                <td style="width:12%" class="hoverImg" onclick="tools.bdOnClick()">
                    <div>
                        <img src="../../Images/Menu/Tools/标点.png" />
                    </div>
                  
                    <div>
                        <label class="titleMenu">标点</label>
                    </div>
                </td>
                <td  style="width:12%" class="hoverImg" onclick="tools.hxOnClick()">
                    <div>
                        <img src="../../Images/Menu/Tools/画线.png" class="hoverImg" />
                    </div>
                    
                    <div>
                        <label class="titleMenu">画线</label>
                    </div>
                </td>
                <td  style="width:12%" class="hoverImg" onclick="tools.hmOnClick()">
                    <div>
                        <img src="../../Images/Menu/Tools/画面2.png" class="hoverImg" />
                    </div>
                   
                    <div>
                        <label class="titleMenu">画面</label>
                    </div>
                </td>
                <td  style="width:12%" class="hoverImg" onclick="tools.cjOnClick()">
                    <div>
                        <img src="../../Images/Menu/Tools/测距.png" class="hoverImg" />
                    </div>
                    
                    <div>
                        <label class="titleMenu">测距</label>
                    </div>
                </td>
                <td  style="width:12%" class="hoverImg" onclick="tools.cmOnClick()">
                    <div>
                        <img src="../../Images/Menu/Tools/测面.png" class="hoverImg" />
                    </div>
                  
                    <div>
                        <label class="titleMenu">测面</label>
                    </div>
                </td>
                <td  style="width:12%" class="hoverImg" onclick="tools.kxOnClick()">
                    <div>
                        <img src="../../Images/Menu/Tools/框选2.png" class="hoverImg" />
                    </div>
                   
                    <div>
                        <label class="titleMenu">框选</label>
                    </div>
                </td>
                <td  style="width:12%" class="hoverImg" onclick="tools.swOnClick()">
                    <div>
                        <img src="../../Images/Menu/Tools/三维.png" class="hoverImg" />
                    </div>
                  
                    <div>
                        <label class="titleMenu">三维选择</label>
                    </div>
                </td>
               
                 <td  style="width:12%" class="hoverImg" onclick="tools.clears();">
                    <div>
                        <img src="../../Images/Menu/Tools/删除2.png" class="hoverImg" />
                    </div>
                  
                    <div>
                        <label class="titleMenu">清除</label>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <%-- 菜单页 End--%>
</body>
</html>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/Bottow/Tools.js"></script>

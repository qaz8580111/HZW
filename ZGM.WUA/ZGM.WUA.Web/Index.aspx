<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ZGM.WUA.Web.Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>智慧钟公庙管控平台</title>
    <link rel="icon" href="Images/ielogo.ico" type="image/x-ico">
    <link href="Styles/index.css" rel="stylesheet" />
    <link href="Styles/style.css" rel="stylesheet" />
    <link href="Styles/messageWarm.css" rel="stylesheet" />
    <link href="css/css.css" rel="stylesheet" />
    <link href="Styles/dialog.css" rel="stylesheet" />
</head>
<body>
    <%-- 消息通知面板 --%>
    <div class="container">
        <ul class="nav-list">
            <li><span style="margin-left: 10px" class="nav-link">消息<span class="nav-counter nav-counter-green">0</span></span></li>
        </ul>
        <div class="messageList" style="margin-left: 20px">
            <%--   <div class="messageSonList">张三<span class="num">4</span></div>
          <div class="messageSonList lastmessage">李四<span class="num">4</span></div>--%>
        </div>
    </div>

    <%-- 聊天框 --%>
    <div id="Draggable" style="display: none; left: 450px; top: 55px; padding: 0px; background-color: #3176cd; background-image: url('/Images/Communicate/backgroundImg.png')">
        <div id="aa" style="width: 100%; height: 35px; line-height: 35px; color: #fff; cursor: pointer; border-bottom: 1px solid #12dffc;">
            <span style="margin-left: 15px" id="name"></span>
            <span id="Span1">(</span>
            <span style="color: #f99029" id="level">指挥长</span>
            <span>）</span>

            <div class="closeImg" style="margin-top: 10px; float: right; width: 15px; height: 15px; background: url('/images/plus_icon.png') no-repeat"></div>
            <div class="minImg" style="margin-top: 10px; float: right; width: 15px; height: 15px;"></div>
            <div class="maxImg" style="margin-top: 10px; float: right; width: 15px; height: 15px; display: none"></div>
        </div>
        <div style="width: 100%; height: 350px" class="displayDiv">
            <div id="convo">
                <ul class="chat-thread" id="scrollContent">
                </ul>

            </div>
            <div style="text-align: center; clear: both">
            </div>
        </div>
        <div class="tools displayDiv">
            <style>
                /*a {
                    display: inline-block;width: 20px;height: 20px; position: relative;overflow: hidden;
                }*/
                /*a:hover{background:green;}*/
                /*input {
                    position: absolute;right: 0;top: 0;font-size: 100px;opacity: 0;filter: alpha(opacity=0);
                }*/
            </style>
            <a href="#" style="display: inline-block; width: 20px; height: 20px; position: relative; overflow: hidden;" class="item pic uploadify">
                <input style="position: absolute; right: 0; top: 0; font-size: 100px; opacity: 0; filter: alpha(opacity=0);" type="file" value="浏览" onchange="preview(this)" id="Img" accept="image/png,image/gif,image/jpeg,image/jpe" />
            </a>
            <%--<div class="item file"></div>--%>
            <div class="item position"></div>
        </div>
        <div class="displayDiv" id="preview" contenteditable="true" style="color: #fff; BORDER-RIGHT: #3176cd 1px solid; PADDING-RIGHT: 3px; BORDER-TOP: #3176cd 1px solid; OVERFLOW-Y: auto; PADDING-LEFT: 3px; FONT-WEIGHT: normal; FONT-SIZE: 14px; OVERFLOW-X: hidden; PADDING-BOTTOM: 3px; BORDER-LEFT: #3176cd 1px solid; WIDTH: 502px; WORD-BREAK: break-all; PADDING-TOP: 3px; BORDER-BOTTOM: #3176cd 1px solid; FONT-STYLE: normal; FONT-FAMILY: SIMSUN; HEIGHT: 60px"></div>
        <%--<div class="displayDiv" id="preview" contenteditable="true" style="color: #fff; BORDER-RIGHT: #3176cd 1px solid; PADDING-RIGHT: 3px; BORDER-TOP: #3176cd 1px solid; OVERFLOW-Y: auto; PADDING-LEFT: 3px; FONT-WEIGHT: normal; FONT-SIZE: 14px; OVERFLOW-X: hidden; PADDING-BOTTOM: 3px; BORDER-LEFT: #3176cd 1px solid; WIDTH: 502px; COLOR: #000066; WORD-BREAK: break-all; PADDING-TOP: 3px; BORDER-BOTTOM: #3176cd 1px solid; FONT-STYLE: normal; FONT-FAMILY: SIMSUN; HEIGHT: 60px"></div>--%>
        <%-- <div style="text-align: center; float: right; width: 50px; height: 20px; background-color: #808080; margin: 5px; line-height: 20px; border-radius: 8px; cursor: pointer" id="close" onclick="closeClick()">关闭</div>--%>
        <div class="displayDiv" style="font-size: 12px; text-align: center; float: right; width: 68px; height: 20px; background-color: #0064ce; margin: 5px; line-height: 20px; border-radius: 8px; cursor: pointer" id="sent" onclick="sentClick()">发送</div>
    </div>

    <div id="headDiv">
        <iframe id="ifmHead" src="Views/Head.aspx" style="height: 100%; width: 100%; border: 0px"></iframe>
    </div>
    <div id="swDiv">
        <object id="__g" type="application/x-cm-3d"></object>
    </div>
    <div id="ewDiv">
        <form id="form1" runat="server" style="height: 100%">
            <div id="silverlightControlHost">
                <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="100%" height="100%">
                    <param name="source" value="ClientBin/ZGM.WUA.xap" />
                    <param name="onError" value="onSilverlightError" />
                    <param name="background" value="white" />
                    <param name="minRuntimeVersion" value="5.0.61118.0" />
                    <param name="autoUpgrade" value="true" />
                    <param name="onLoad" value="initSL" />
                    <param name="initParams" value="arg=<%= System.Configuration.ConfigurationManager.AppSettings["arg"]%>" />
                    <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=5.0.61118.0" style="text-decoration: none">
                        <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="获取 Microsoft Silverlight" style="border-style: none" />
                    </a>
                </object>
                <iframe id="_sl_historyFrame" style="visibility: hidden; height: 0px; width: 0px; border: 0px"></iframe>
            </div>
        </form>
    </div>
    <div id="reportDiv">
        <iframe id="ifmReport" style="height: 100%; width: 100%; border: 0px"></iframe>
    </div>
    <div id="menuDiv">
        <img src="../Images/Menu/frameWithPic.png" style="height: 35px; width: 100%" />
        <div class="self">
            <label class="titleMenu">今日上报数：</label>
            <label class="contentMenu" style="" id="jrsbNum">0</label>
            <label class="titleMenu">紧急事件：</label>
            <label class="contentMenu" style="color: #df4f41" id="jjsjNum">0</label>
            <label class="titleMenu">结案数：</label>
            <label class="contentMenu" style="color: #ef8120" id="jaNum">0</label>
            <label class="titleMenu">超期数：</label>
            <label class="contentMenu" style="color: #20ef5a" id="cqNum">0</label>
            <label class="titleMenu">在线人员：</label>
            <label class="contentMenu" style="color: #35ff20" id="zxryNum">0</label>
            <label class="titleMenu">在线车辆：</label>
            <label class="contentMenu" style="color: #04b28b" id="zxclNum">0</label>
            <label class="titleMenu">监控数量：</label>
            <label class="contentMenu" style="color: #a7dd16" id="jkNum">187</label>
            <label class="titleMenu">部件数量：</label>
            <label class="contentMenu" style="color: #b44cf7" id="bjNum">1086</label>
        </div>
        <div class="bottomDiv">
            <div class="bottomSon bottomMargin">
                <img id="menuHome" class="hoverImg" title="首页" src="../Images/Menu/Currency_Exchange_24px_1190753_easyicon_left.png" />
                <img id="menuMapChange" title="地图切换" src="../Images/Menu/gloable.png" class="hoverImg" />
            </div>
            <div class="bottomSon ">
                <img src="../Images/Menu/rectangle.png" />
                <img src="../Images/Menu/rectangle.png" />
            </div>
            <%--  中间内容start --%>
            <div id="menuContent">
                <img id="picBottow" src="../Images/Menu/picBottow.png" />
                <%--<img id="backgroundPic" src="../Images/Menu/backgroundPic.png" />--%>
                <iframe id="ifmMenu" src="Views/Bottow/FirstPage.aspx" style="height: 100%; width: 100%; border: 0px"></iframe>
            </div>
            <%--  中间内容End --%>
            <div class="bottomSon " style="margin-left: 10px">
                <img src="../Images/Menu/rectangle.png" />
                <img src="../Images/Menu/rectangle.png" />
            </div>
            <div class="bottomSon bottomMargin">
                <img id="menuCatalog" class="hoverImg" title="菜单" src="../Images/Menu/Currency_Exchange_24px_1190753_easyicon_right.png" />
                <img id="toolsImg" title="工具" src="../Images/Menu/Tools/tools.png" class="hoverImg" />
            </div>
        </div>
    </div>
    <div id="listDiv">
        <iframe id="ifmList" src="" style="height: 100%; width: 100%; border: 0px"></iframe>
    </div>
    <div id="detail-minDiv">
        <iframe id="ifmDetailMin" src="" style="height: 100%; width: 100%; border: 0px"></iframe>
    </div>
    <div id="detailDiv">
        <iframe id="ifmDetail" src="" style="height: 100%; width: 100%; border: 0px"></iframe>
    </div>
    <%--   <div id="messageDiv">
        <iframe id="ifmMessage" src="Views/Message/Message.aspx" style="height: 100%; width: 100%; border: 0px"></iframe>
    </div>--%>
    <div id="loadingDiv">
        <img src="Images/loading.gif" height="200" width="200" />
        <span id="loginSpan">登录验证中。。。</span>
        <span id="loadingSpan">
            <button type="button" onclick="login();" style="visibility: hidden">返回登录</button></span>
    </div>
    <div id="UserID"><%=param%></div>
    <div id="append_parent"></div>
    <div id="ajaxwaitid"></div>
</body>
<script src="Scripts/base/jquery-1.8.2.min.js"></script>
<script src="Scripts/easyui/jquery.easyui.min.js"></script>
<script src="Scripts/layer-v2.2/layer/layer.js"></script>
<script src="Scripts/base/cm7.js"></script>
<script src="Scripts/base/cm7_sample_util.js"></script>
<script>

</script>
<script src="Scripts/index.js"></script>
<script src="Scripts/index.init.js"></script>
<script src="Scripts/index.map.js"></script>
<script src="Scripts/index.func.js"></script>
<script src="Scripts/index.event.js"></script>
<script src="Scripts/index.business.js"></script>
<script src="Scripts/base/Common.js"></script>
<script src="Scripts/Communicate/Draggle.js" charset="gb2312"></script>
<script src="Scripts/Communicate/jquery.uploadify.min.js"></script>
<script type="text/javascript">
    var IMGDIR = 'images';
</script>
<script src="Scripts/Communicate/common.js"></script>
<script src="Scripts/DragDialog/zDrag.js"></script>
<script src="Scripts/DragDialog/zDialog.js"></script>
<script src="Scripts/Communicate/jquery.nicescroll.js"></script>
<script>
   
    function initConfig() {
        globalConfig.apiconfig = '<%=System.Web.Configuration.WebConfigurationManager.AppSettings["apiconfig"] %>';
        globalConfig.picurl = '<%=System.Web.Configuration.WebConfigurationManager.AppSettings["picurl"] %>';
        globalConfig.managerIndexPath = '<%=System.Web.Configuration.WebConfigurationManager.AppSettings["managerIndexPath"] %>';
        globalConfig.imgPath = '<%=System.Web.Configuration.WebConfigurationManager.AppSettings["imgPath"] %>';
        globalConfig.loginUrl = '<%=System.Web.Configuration.WebConfigurationManager.AppSettings["loginUrl"] %>';
    };
</script>
</html>

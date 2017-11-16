<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CameraCenter.aspx.cs" Inherits="ZGM.WUA.Web.CameraCenter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>监控中心</title>
    <link href="Styles/camera.center.css" rel="stylesheet" />

</head>
<body>
    <div id="cam">
        <object classid="clsid:04DE0049-8359-486e-9BE7-1144BA270F6A" id="PlayViewOCX" height="100%" width="100%" name="ocx"></object>
    </div>
    <div id="list">
        <%-- <select id="cameras">
            <option value="">请选择</option>
            <option value="<?xml version='1.0' encoding='UTF-8'?><Message><Camera><Id>168</Id><IndexCode>33000002001310014219</IndexCode><Name>政府职能带1</Name><ChanNo>0</ChanNo><Matrix Code='' Id='0' /></Camera><Dev regtype='0' devtype='10070'><Id>301</Id><IndexCode>33000002001310014219</IndexCode><Addr IP='172.16.7.101' Port='8000' /><Auth User='admin' Pwd='12345' /></Dev><Vag IP='172.16.2.36' Port='7302' /><Voice><Encode>2</Encode></Voice><Media Protocol='0' Stream='1'><Vtdu IP='172.16.2.37' Port='554' /></Media><Privilege Priority='50' Code='7' /><Option><Talk>1</Talk><PreviewType>0</PreviewType></Option></Message>">政府职能带1</option>
            <option value="<?xml version='1.0' encoding='UTF-8'?><Message><Camera><Id>169</Id><IndexCode>33000002001310014268</IndexCode><Name>政府职能带2</Name><ChanNo>0</ChanNo><Matrix Code='' Id='0' /></Camera><Dev regtype='0' devtype='10070'><Id>301</Id><IndexCode>33000002001310014268</IndexCode><Addr IP='172.16.7.101' Port='8000' /><Auth User='admin' Pwd='12345' /></Dev><Vag IP='172.16.2.36' Port='7302' /><Voice><Encode>2</Encode></Voice><Media Protocol='0' Stream='1'><Vtdu IP='172.16.2.37' Port='554' /></Media><Privilege Priority='50' Code='7' /><Option><Talk>1</Talk><PreviewType>0</PreviewType></Option></Message>">政府职能带1</option>
            <option value="<?xml version='1.0' encoding='UTF-8'?><Message><Camera><Id>188</Id><IndexCode>33000001001310017541</IndexCode><Name>西区泵站1</Name><ChanNo>0</ChanNo><Matrix Code='' Id='0' /></Camera><Dev regtype='0' devtype='10070'><Id>330</Id><IndexCode>33000001001310017541</IndexCode><Addr IP='172.16.7.101' Port='8000' /><Auth User='admin' Pwd='12345' /></Dev><Vag IP='172.16.2.36' Port='7302' /><Voice><Encode>2</Encode></Voice><Media Protocol='0' Stream='1'><Vtdu IP='172.16.2.37' Port='554' /></Media><Privilege Priority='50' Code='7' /><Option><Talk>1</Talk><PreviewType>0</PreviewType></Option></Message>">西区泵站1</option>
            <option value="<?xml version='1.0' encoding='UTF-8'?><Message><Camera><Id>178</Id><IndexCode>33000001001310013082</IndexCode><Name>文化广场中央</Name><ChanNo>0</ChanNo><Matrix Code='' Id='0' /></Camera><Dev regtype='0' devtype='10070'><Id>313</Id><IndexCode>33000001001310013082</IndexCode><Addr IP='172.16.7.101' Port='8000' /><Auth User='admin' Pwd='12345' /></Dev><Vag IP='172.16.2.36' Port='7302' /><Voice><Encode>2</Encode></Voice><Media Protocol='0' Stream='1'><Vtdu IP='172.16.2.37' Port='554' /></Media><Privilege Priority='50' Code='7' /><Option><Talk>1</Talk><PreviewType>0</PreviewType></Option></Message>">文化广场中央</option>
        </select>--%>
        <%--  <div class="cameraList cameraSelected">政府职能带1</div>
        <div class="cameraList">政府职能带1政府职能带1政府职能带1政府职能带1</div>
        <div class="cameraList">政府职能带1</div>
         <div class="cameraList">政府职能带1</div>
         <div class="cameraList">政府职能带1</div>
        <div class="cameraList">政府职能带1政府职能带1政府职能带1政府职能带1</div>
        <div class="cameraList">政府职能带1</div>
         <div class="cameraList">政府职能带1</div>
         <div class="cameraList">政府职能带1</div>
         <div class="cameraList">政府职能带1</div>
        <div class="cameraList">政府职能带1</div>
        <div class="cameraList">政府职能带1</div>
         <div class="cameraList">政府职能带1</div>
         <div class="cameraList">政府职能带1</div>--%>
    </div>
    <div id="erDiv">
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
</body>
</html>
<script src="Scripts/base/jquery-1.8.2.min.js"></script>
<script src="Scripts/camera.center.js"></script>
<script>
    function initConfig() {
        globalConfig.apiconfig = '<%=System.Web.Configuration.WebConfigurationManager.AppSettings["apiconfig"] %>';
    }
</script>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CameraDetail.aspx.cs" Inherits="ZGM.WUA.Web.Views.Camera.CameraDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>监控播放</title>
    <link href="/Styles/listdetail.css" rel="stylesheet" />
    
    <script>
        function init() {
            var camera = new Object();
            camera.CameraId = GetQueryString("CameraId");
            camera.IndexCode = GetQueryString("IndexCode");
            camera.CameraName = GetQueryString("CameraName");
            camera.DeviceId = GetQueryString("DeviceId");
            //var xml = GetQueryString("Parameter");
            var xml = "<?xml version='1.0' encoding='UTF-8'?><Message><Camera><Id>" + camera.CameraId
                    + "</Id><IndexCode>" + camera.IndexCode
                    + "</IndexCode><Name>" + ""
                    + "</Name><ChanNo>1</ChanNo><Matrix Code='0' Id='0' /></Camera><Dev regtype='0' devtype='10017'><Id>" + camera.DeviceId
                    + "</Id><IndexCode>" + camera.IndexCode
                    + "</IndexCode><Addr IP='172.172.50.9' Port='11100' /><Auth User='admin' Pwd='hik12345' /></Dev><Vag IP='172.172.100.11' Port='7302' /><Voice><Encode>2</Encode></Voice><Media Protocol='0' Stream='0'><Privilege Priority='50' Code='31' />  <Option>    <Talk>1</Talk>    <PreviewType>1</PreviewType>  </Option></Message>";

            var OCXobj = document.getElementById("PlayViewOCX");
            OCXobj.SetOcxMode(0);
            OCXobj.SetWndNum(1);
            OCXobj.StartTask_Preview_FreeWnd(xml);
        }
        function GetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }

    </script>
</head>
<body onload="init();">
    <form id="form1" runat="server">
        <%--<div>
            <button id="dbtn1">展开</button>
            <button id="dbtn2">收缩</button>
            <button id="dbtn3">关闭</button>
        </div>--%>
        <div class="main">
            <div class="closebtn" id="closebtn" onclick="detail.close();"></div>
            <div class="minbtn" style=""></div>
            <%--部件详情--%>
            <div class="personbase" style="display: block; width: 750px; height: 450px;">
                <object classid="clsid:04DE0049-8359-486e-9BE7-1144BA270F6A" id="PlayViewOCX" width="750" height="450" name="ocx" />
            </div>
        </div>

    </form>
</body>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<%--<script src="../../Scripts/Camera.detailmin.js"></script>--%>
</html>


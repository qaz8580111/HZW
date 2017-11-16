<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SLGis.aspx.cs" Inherits="ZGM.WUA.Web.SLGis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="charset=utf-8;application/x-www-form-urlencoded" />
    <title>测试页面</title>
</head>
<body>
    <button onclick="test();">测试</button>
</body>
<script src="Scripts/base/jquery-1.8.2.min.js"></script>
<script>
    function test() {
        var arg = '<%=System.Web.Configuration.WebConfigurationManager.AppSettings["arg"] %>';
        var apiconfig = arg.split('#')[0].split('|')[1];
        var CameraId = 422;
        $.ajax({
            type: "GET",
            async: true,
            url: apiconfig + "/api/Camera/GetCameraInfo",
            data: { cameraId: CameraId },
            dataType: "json",
            success: function (result) {
                //console.log(result);
                document.cookie = "Camera=" + result.PlayBack;
                //var cam = getCookie("Camera");
                //console.log(cam);
                //alert(cam);

                //var windowObjectReference;
                //var strWindowFeatures = "menubar=yes,location=yes,resizable=yes,scrollbars=yes,status=yes";
                //windowObjectReference = window.open("Views/Camera/CameraPlayBack.aspx", "CNN_WindowName", strWindowFeatures);
                var CameraId = result.CameraId;
                var CameraName = encodeURI(encodeURI(result.CameraName));
                var IndexCode = result.IndexCode;
                //var StartTime = "2016-07-15 12:00:00";
                //var EndTime = "2016-07-15 13:00:00";

                var StartTime = "";
                var EndTime = "";


                var param = "?CameraId=" + CameraId + "&CameraName=" + CameraName + "&IndexCode=" + IndexCode + "&StartTime=" + StartTime + "&EndTime=" + EndTime;
                //window.showModalDialog("Views/Camera/CameraPlayBack.aspx" + param, null, "dialogHeight:580px;dialogWidth:818px;dialogTop=60;dialogLeft=300");
                //param = "?param=" + JSON.stringify(result);
                document.open("Views/Camera/CameraPlayBack.aspx" + param, "_blank", "top=60,left=300,width=818, height=580");

            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    }
</script>
</html>

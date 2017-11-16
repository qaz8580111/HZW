<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CameraPlayBack.aspx.cs" Inherits="ZGM.WUA.Web.Views.Camera.CameraPlayBack" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>监控回放</title>
    <style>
        html, body {
            height: 100%;
            overflow: hidden;
        }
    </style>
    <link href="../../Styles/easyui/easyui.css" rel="stylesheet" />
</head>
<body onload="init();">
    <form id="form1" runat="server">
        <div style="float:left">
            <object classid="clsid:5DEF5889-AD46-4fc0-AEBE-B54E6CD71C96" id="OcxObject" width="800" height="500" name="OcxObject"></object>
        </div>
     <%--   <div id="searchTime" style="float:left;width:180px;height:100%;margin:5px" class="easyui-panel" title="Basic Panel">
           <label  class="label-top">开始时间</label> 
                       <input id="startTime" class="easyui-datetimebox" data-options="formatter:ww3,parser:w3" style="width:100%;height:26px"></input>
                    <label class="label-top" style="margin-top"> 结束时间</label> 
                       <input id="endTime" class="easyui-datetimebox" data-options="formatter:ww3,parser:w3" style="width:100%;height:26px"></input>
                     
                        <input type="button" value="开始搜索" onclick="StartQueryRecord()" />
        </div>--%>
            <div id="searchTime" class="easyui-panel" style="float:left;width:180px;height:100%;margin:5px" title="回放时间设置">
        <div style="margin-top:10px">
            <label class="label-top">开始时间:</label>
             <input id="TextStartTime" class="easyui-datetimebox" data-options="formatter:ww3,parser:w3" style="width:175px;height:26px"><%-- yyyy/mm/dd hh-mm-ss--%></input>
                     </div>
        <div style="margin-top:10px">
            <label class="label-top">结束时间:</label>
           <input id="TextEndTime" class="easyui-datetimebox" data-options="formatter:ww3,parser:w3" style="width:175px;height:26px"> <%--yyyy/mm/dd hh-mm-ss--%></input>
                     
        </div>
                 <input style="margin-top:10px;width:170px;height:35px" type="button" value="开始搜索" onclick="StartQueryRecord()" />
    </div>
    </form>
</body>

<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
    <script src="../../Scripts/easyui/jquery.easyui.min.js"></script>
<script src="../../Scripts/easyui/easyui-lang-zh_CN.js"></script>
<script>
    function init() {
        ocxObject = document.getElementById("OcxObject");
        ocxObject.SetPlayWndOne();

        console.log(document.cookie);
        console.log(GetQueryString("param"));
        
        var CameraId = GetQueryString("CameraId");
        var CameraName = decodeURI(decodeURI(GetQueryString("CameraName")));
        var IndexCode = GetQueryString("IndexCode");
        var StartTime = GetQueryString("StartTime");
        var EndTime = GetQueryString("EndTime");

        var xml = "<?xml version='1.0' encoding='UTF-8'?><Message> <Window index='0' /> <Camera> <Id>" + CameraId + "</Id> "
            + "<IndexCode>" + IndexCode + "</IndexCode> <Name>" + CameraName + "</Name> <DevType>0</DevType>"
            + " <DetailDevType>10016</DetailDevType> <RecLocation>2</RecLocation> </Camera> <Query> <ZoneId>100001</ZoneId>"
            + " <Vrm ip='172.172.100.11' port='6300' /> </Query> <Intelligence> <NCG ip='172.172.100.11' port='7099' /> "
            + "<IvsSvr ip='127.0.0.1' port='6060' /> <Kms ip='127.0.0.1' port='8080' />"
            + " <Imp ip='172.172.100.10' port='80' userName='admin' password='b213ceac2f1022023ef2699aa62599cf'/> "
            + "</Intelligence> <DSNVR_Info> <Addr /> <Port /> <UserName /> <PassWord /> <indexcode /> </DSNVR_Info> "
            + "<VAG_Info> <Addr /> <Port /> <UserName /> <PassWord /> </VAG_Info> <Privilege>7</Privilege>"
            + " <StorageLocation>1</StorageLocation><AutoPlay>0</AutoPlay></Message>";
        ocxObject.SetPlayBackParam(xml);

        if (StartTime != null && StartTime != "" && EndTime != null && EndTime != "") {
            $('#searchTime').css("display", "none");
            strXML = "<?xml version='1.0'?><Message><Time><!--录像开始时间--><Begin>" + StartTime + "</Begin><!--录像结束时间--><End>" + EndTime + "</End></Time><!--录像类型1-计划录像2-移动录像4-报警录像16-手动录像23-全部录像--><RecType>" + "23" + "</RecType></Message>";
            ocxObject.StartQueryRecord(strXML);
        }        
    }

    function StartQueryRecord() {
        var StartTime = $('#TextStartTime').datetimebox('getValue');;
        var EndTime = $('#TextEndTime').datetimebox('getValue');
        if (StartTime == null || StartTime == "" || EndTime == null || EndTime == "") {
            $.messager.show({
                title: '提示',
                msg: '请输入时间！',
                timeout: 2000,
                showType: 'slide'
            });
            return;
        }
        if (StartTime >ww3(new Date())) {
            $.messager.show({
                title: '提示',
                msg: '开始时间不能大与当前时间！',
                timeout: 2000,
                showType: 'slide'
            });
            return;
        }
        if (StartTime > EndTime) {
            $.messager.show({
                title: '提示',
                msg: '开始时间不能大与结束时间！',
                timeout: 2000,
                showType: 'slide'
            });
            return;
        }

        strXML = "<?xml version='1.0'?><Message><Time><!--录像开始时间--><Begin>" + StartTime + "</Begin><!--录像结束时间--><End>" + EndTime + "</End></Time><!--录像类型1-计划录像2-移动录像4-报警录像16-手动录像23-全部录像--><RecType>" + "23" + "</RecType></Message>";
        ocxObject.StartQueryRecord(strXML);
    }

    function GetQueryString(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }
</script>

    <script>
        function ww3(date) {
            var y = date.getFullYear();
            var m = date.getMonth() + 1;
            var d = date.getDate();
            var h = date.getHours();
            var min = date.getMinutes();
            var sec = date.getSeconds();
            var str = y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d) + '' + ' ' + (h < 10 ? ('0' + h) : h) + ':' + (min < 10 ? ('0' + min) : min) + ':' + (sec < 10 ? ('0' + sec) : sec);
            return str;
        }
        function w3(s) {
            if (!s) return new Date();
            var y = s.substring(0, 4);
            var m = s.substring(5, 7);
            var d = s.substring(8, 10);
            var h = s.substring(11, 13);
            var min = s.substring(14, 16);
            var sec = s.substring(17, 19);
            if (!isNaN(y) && !isNaN(m) && !isNaN(d) && !isNaN(h) && !isNaN(min) && !isNaN(sec)) {
                return new Date(y, m - 1, d, h, min, sec);
            } else {
                return new Date();
            }
        }
    </script>
</html>

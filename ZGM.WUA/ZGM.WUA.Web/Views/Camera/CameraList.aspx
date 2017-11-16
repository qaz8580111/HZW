<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CameraList.aspx.cs" Inherits="ZGM.WUA.Web.Views.Camera.CameraList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Access-Control-Allow-Origin" content="*" />
    <title>部件列表</title>
    <link href="../../Styles/list.css" rel="stylesheet" />

    <link href="../../Styles/easyui/easyui.css" rel="stylesheet" />
    <link href="../../Styles/CameraList.css" rel="stylesheet" />
    <style>
        /*p {
            font-size: 9px;
            font-family: 宋体;
            font-weight: bold;
            color: gray;
        }

        .showLoading {
            text-align: center;
            position: absolute;
            top: 50%;
            left: 80px;
        }

        #allbg {
            background: #87a7c8;
            width: 206px;
            height: 256px;
            top: 78px;
            left: 0px;
            position: absolute;
            opacity: 0.6;
            z-index: 100;
        }*/
    </style>
</head>
<body>


    <form id="form1" runat="server">
        <div class="top">
            <div class="ticon_parts_camera"></div>
            <div class="title" id="detailbtn">监控信息</div>
            <div class="counts"><span id="pcounts"></span></div>
            <div class="minbtn"></div>
            <div class="closebtn" id="closebtn" onclick="list.close();"></div>
            <div class="topline"></div>
        </div>

        <div class="search">
            <input id="search" value="请输入搜索内容" onfocus="if (value =='请输入搜索内容'){value =''}"
                onblur="if(this.value===''){this.value='请输入搜索内容'}" />
            <div class="search_btn" onclick="list.searchClick();"></div>
        </div>
        <div class="tab">
            <div class="tabChecked" style="float: left; text-align: center; width: 60px">监控</div>
            <div style="float: left; text-align: center; width: 60px">专题</div>
        </div>
        <div class="easyui-panel personlist" style="height: 256px; width: 200px;" id="scrollDiv">
           <%-- <div id="allbg">
                <div id="showLoading" class="showLoading" style="top: 60px; left: 45px;">
                    <img src="/images/list/loading2.gif" style="width: 20px; height: 20px" /><br />
                    <p style="font-size: 17px;">
                        正在加载中...
                    </p>
                </div>
            </div>--%>
           <div id="allbg"><div id="showLoading" class="showLoading" style="top:60px;left:45px;"><img src="/images/list/loading2.gif" style="width: 20px; height: 20px" />' +
        '<br /><p style="font-size: 17px;">正在加载中...</></div></div>
            <ul class="easyui-tree" style="display: none;" id="searchTree">
            </ul>
            <ul class="easyui-tree" style="display: none;" id="ztCameraTree">
            </ul>
            <ul class="easyui-tree" style="" id="cameraTree">
            </ul>

        </div>
        <div style="width: 100%; height: 31px;">
            <div style="float: right" onclick="list.positionAllCamera()">
                <img class="allLocate" style="margin-right: 25px; margin-top: 5px; cursor: pointer" src="/Images/AllLocate.png" alt="全部定位" title="全部定位" />
            </div>
        </div>
        <input id="pagecounts" type="hidden" value="0" />
        <%-- <div class="page">
            <div class="wrapper">
                <div class="gigantic pagination">
                    <a href="#" class="first" data-action="first">&laquo;</a>
                    <a href="#" class="previous" data-action="previous">&lsaquo;</a>
                    <input type="text" readonly="readonly" disabled="true" />

                    <a href="#" class="next" data-action="next">&rsaquo;</a>
                    <a href="#" class="last" data-action="last">&raquo;</a>
                </div>
            </div>
        </div>--%>
        <%--<button id="detailbtn">详情</button>--%>

        <%-- <button id="expbtn">展开</button>
            <button id="collbtn">收缩</button>--%>
    </form>
</body>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/page/jquery.jqpagination.js"></script>
<script src="../../Scripts/easyui/jquery.easyui.min.js"></script>
<%--<script src="../../Scripts/Communicate/jquery.nicescroll.js"></script>--%>
<script src="../../Scripts/Camera.list.js"></script>

<script>

 
      
</script>
<%--<script src="../../Scripts/parts.list.js"></script>--%>
</html>

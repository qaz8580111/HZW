<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RemoveBuildingList.aspx.cs" Inherits="ZGM.WUA.Web.Views.RemoveBuilding.RemoveBuildingList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Access-Control-Allow-Origin" content="*" />
    <title></title>
    <link href="../../Styles/list.css" rel="stylesheet" />
    <link href="../../Styles/CameraList.css" rel="stylesheet" />
</head>
<body>


    <form id="form1" runat="server">
        <div class="top">
            <div class="ticon_Remove"></div>
            <div class="title">拆迁</div>
            <div class="counts"><span id="pcounts"></span>个</div>
            <div class="minbtn" style="margin-left:25px"></div>
            <div class="closebtn" id="closebtn" onclick="list.close();"></div>
            <div class="topline"></div>
        </div>

        <div class="search">
            <input id="search" value="请输入搜索内容" onfocus="if (value =='请输入搜索内容'){value =''}"
                onblur="if(this.value===''){this.value='请输入搜索内容'}" />
            <div class="search_btn" onclick="list.searchRB();"></div>
        </div>
       <%-- <div class="cloumnames">
            <div>姓名</div>
         
         
        </div>--%>
          <div class="tab">
            <div class="tabChecked" style="float: left; text-align: center; width: 60px">住宅</div>
            <div style="float: left; text-align: center; width: 60px">企业</div>
        </div>
        <div class="personlist" style="height:259px">
            <div class="personlist_list">
                        <%-- <a href="javascript:void(0)" style="color:#87a7c8;" onclick="list.userClicked();">
                         <div class="statusicon  icon  " style="display:block; width:38px;height:30px; line-height:30px;"></div>
                         <div  style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:64px;"> 名字  </div>
                         <div style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:71px;">  爱爱爱  </div>
                         <div class="message  msg  " style="float:right; display:block;width:33px; height:30px;  line-height:30px;cursor: pointer;"></div>
                         <div class="topline" style="width:190px; height:1px; margin-left:8px;"></div>
                         </a>--%>
                    </div>
        </div>
        <input id="pagecounts" type="hidden" value="0" />
        <div class="page">
              <div class="wrapper">
                <div style="text-align:left;margin-left:20px;float:left;width:120px" class="gigantic pagination">
                    <a href="#" class="first" data-action="first">«</a>
                    <a href="#" class="previous" data-action="previous">‹</a>
                    <input style="width:60px" readonly="readonly" disabled="true" type="text">

                    <a href="#" class="next" data-action="next">›</a>
                    <a href="#" class="last" data-action="last">»</a>
   
                </div>
                <img class="allLocate" style="margin-top:5px;cursor:pointer" src="/Images/AllLocate.png" alt="全部定位" title="全部定位"/>
            </div>
        </div>
        <%--<button id="detailbtn">详情</button>--%>

        <%-- <button id="expbtn">展开</button>
            <button id="collbtn">收缩</button>--%>
    </form>
</body>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/page/jquery.jqpagination.js"></script>
<script src="../../Scripts/RemoveBuilding.list.js"></script>

</html>

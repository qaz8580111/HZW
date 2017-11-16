<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConstrList.aspx.cs" Inherits="ZGM.WUA.Web.Views.Constr.ConstrList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Access-Control-Allow-Origin" content="*" />
    <title></title>
    <link href="../../Styles/list.css" rel="stylesheet" />
</head>
<body>


    <form id="form1" runat="server">
        <div class="top">
            <div class="ticon_Constr"></div>
            <div class="title">工程</div>
            <div class="counts"><span id="pcounts"></span>个</div>
            <div class="minbtn" style="margin-left:25px"></div>
            <div class="closebtn" id="closebtn" onclick="list.close();"></div>
            <div class="topline"></div>
        </div>

        <div class="search">
            <input id="search" value="请输入搜索内容" onfocus="if (value =='请输入搜索内容'){value =''}"
                onblur="if(this.value===''){this.value='请输入搜索内容'}" />
            <div class="search_btn" onclick="list.searchConstr();"></div>
        </div>
        <div class="cloumnames">
            <div style="width:80px">工程名</div>
            <%--<div>类型</div>--%>
           <%-- <div>   </div>--%>
        </div>

        <div class="personlist">
            <div class="personlist_list">
                         <a href="javascript:void(0)" style="color:#87a7c8;" onclick="list.ConstrClicked(1);">
                         <div class="statusiconConstr  icon  " style="display:block; width:38px;height:30px; line-height:30px;"></div>
                         <div  style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:160px;"> 工程名字工程名字工程名字工程名字工程名字工程名字工程名字  </div>
                        
                         <div class="topline" style="width:190px; height:1px; margin-left:8px;"></div>
                         </a>
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
<script src="../../Scripts/Constr.list.js"></script>

</html>

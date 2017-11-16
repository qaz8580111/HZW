<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarList.aspx.cs" Inherits="ZGM.WUA.Web.Views.Car.CarList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>车辆列表</title>
    <link href="../../Styles/list.css" rel="stylesheet" />
   
</head>
<body>
    <form id="form1" runat="server">
        <div class="top">
            <div class="ticon_c"></div>
            <div class="title" id="detailbtn">执法车辆</div>
            <div class="counts"><span id="pcounts">0</span>辆</div>
            <div class="minbtn"></div>
            <div class="closebtn" id="closebtn" onclick="list.close();"></div>
            <div class="topline"></div>
        </div>

        <div class="search">
            <input id="search" value="请输入搜索内容" onfocus="if (value =='请输入搜索内容'){value =''}"
                onblur="if(this.value===''){this.value='请输入搜索内容'}" />
            <div class="search_btn" onclick="list.searchCars();"></div>
        </div>
        <div class="cloumnames">
            <div style="width: 78px;">车牌号</div>
            <div></div>
            <div></div>
        </div>
        <div class="personlist">
            <div id="allbg">
                <div id="showLoading" class="showLoading">
                    <img src="/images/list/loading2.gif" style="width: 20px; height: 20px" />
                    <br />
                    <p>正在加载中...</p>
                </div>
            </div>
            <div class="personlist_list">
                <div class="statusicon_c" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                <div style="width: 16px;">浙</div>
                <div style="width: 70px">A888888</div>
                <div class="topline" style="width: 99%; height: 1px;"></div>
            </div>
            <div class="personlist_list">
                <div class="statusicon1_c" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                <div style="width: 16px;">浙</div>
                <div style="width: 70px">A888888</div>
                <div class="topline" style="width: 99%; height: 1px;"></div>
            </div>
            <div class="personlist_list">
                <div class="statusicon2_c" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                <div style="width: 16px;">浙</div>
                <div style="width: 70px">A888888</div>
                <div class="topline" style="width: 99%; height: 1px;"></div>
            </div>
            <div class="personlist_list">
                <div class="statusicon_c" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                <div style="width: 16px;">浙</div>
                <div style="width: 70px">A888888</div>
                <div class="topline" style="width: 99%; height: 1px;"></div>
            </div>
            <div class="personlist_list">
                <div class="statusicon_c" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                <div style="width: 16px;">浙</div>
                <div style="width: 70px">A888888</div>
                <div class="topline" style="width: 99%; height: 1px;"></div>
            </div>
            <div class="personlist_list">
                <div class="statusicon_c" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                <div style="width: 16px;">浙</div>
                <div style="width: 70px">A888888</div>
                <div class="topline" style="width: 99%; height: 1px;"></div>
            </div>
            <div class="personlist_list">
                <div class="statusicon_c" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                <div style="width: 16px;">浙</div>
                <div style="width: 70px">A888888</div>
                <div class="topline" style="width: 99%; height: 1px;"></div>
            </div>
            <div class="personlist_list">
                <div class="statusicon_c" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                <div style="width: 16px;">浙</div>
                <div style="width: 70px">A888888</div>
                <div class="topline" style="width: 99%; height: 1px;"></div>
            </div>
        </div>
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
<script src="../../Scripts/car.list.js"></script>
</html>

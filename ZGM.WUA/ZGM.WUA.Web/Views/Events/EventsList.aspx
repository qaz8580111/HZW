<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventsList.aspx.cs" Inherits="ZGM.WUA.Web.Views.Events.EventsList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Access-Control-Allow-Origin" content="*">
    <title>事件列表</title>
    <link href="../../Styles/list.css" rel="stylesheet" />
    <style>
        p {
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
        }
    </style>
</head>
<body>


    <form id="form1" runat="server">
        <div class="top">
            <div class="ticon_events"></div>
            <div class="title" id="detailbtn">事件信息</div>
            <div class="counts"><span id="pcounts">0</span>件</div>
            <div class="minbtn"></div>
            <div class="closebtn" id="closebtn" onclick="list.close();"></div>
            <div class="topline"></div>
        </div>

        <div class="search">
            <input id="search" value="按事件地址查询" onfocus="if (value =='按事件地址查询'){value =''}"
                onblur="if(this.value===''){this.value='按事件地址查询'}" />
            <div class="search_btn" onclick="list.searchEvents();"></div>
        </div>
        <div class="cloumnames">
            <div style="width: 90px; text-align: right;">案卷编号</div>
            <div style="width: 94px; text-align: left; float: right;">事件小类</div>
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
                <a href="javascript:void(0)" style="color: #87a7c8;">
                    <div class="statusicon_events" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                    <div>076212</div>
                    <div style="float: right; text-align: left; width: 95px;">中公庙街街道</div>
                    <div class="topline" style="width: 100%; height: 1px;"></div>
                </a>
            </div>
            <div class="personlist_list">
                <a href="javascript:void(0)" style="color: #87a7c8;">
                    <div class="statusicon_events" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                    <div>076212</div>
                    <div style="float: right; text-align: left; width: 95px;">中公庙街街道</div>
                    <div class="topline" style="width: 100%; height: 1px;"></div>
                </a>
            </div>
            <div class="personlist_list">
                <a href="javascript:void(0)" style="color: #87a7c8;">
                    <div class="statusicon1_events" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                    <div>076212</div>
                    <div style="float: right; text-align: left; width: 95px;">中公庙街街道</div>
                    <div class="topline" style="width: 100%; height: 1px;"></div>
                </a>
            </div>
            <div class="personlist_list">
                <a href="javascript:void(0)" style="color: #87a7c8;">
                    <div class="statusicon1_events" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                    <div>076212</div>
                    <div style="float: right; text-align: left; width: 95px;">中公庙街街道</div>
                    <div class="topline" style="width: 100%; height: 1px;"></div>
                </a>
            </div>
            <div class="personlist_list">
                <a href="javascript:void(0)" style="color: #87a7c8;">
                    <div class="statusicon1_events" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                    <div>076212</div>
                    <div style="float: right; text-align: left; width: 95px;">中公庙街街道</div>
                    <div class="topline" style="width: 100%; height: 1px;"></div>
                </a>
            </div>
            <div class="personlist_list">
                <a href="javascript:void(0)" style="color: #87a7c8;">
                    <div class="statusicon_events" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                    <div>076212</div>
                    <div style="float: right; text-align: left; width: 95px;">中公庙街街道</div>
                    <div class="topline" style="width: 100%; height: 1px;"></div>
                </a>
            </div>
            <div class="personlist_list">
                <a href="javascript:void(0)" style="color: #87a7c8;">
                    <div class="statusicon_events" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                    <div>076212</div>
                    <div style="float: right; text-align: left; width: 95px;">中公庙街街道</div>
                    <div class="topline" style="width: 100%; height: 1px;"></div>
                </a>
            </div>
            <div class="personlist_list">
                <a href="javascript:void(0)" style="color: #87a7c8;">
                    <div class="statusicon_events" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                    <div>076212</div>
                    <div style="float: right; text-align: left; width: 95px;">中公庙街街道</div>
                    <div class="topline" style="width: 100%; height: 1px;"></div>
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
<script src="../../Scripts/events.list.js"></script>
</html>

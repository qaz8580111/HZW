<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PartsList.aspx.cs" Inherits="ZGM.WUA.Web.Views.Parts.PartsList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Access-Control-Allow-Origin" content="*">
    <title>部件列表</title>
    <link href="../../Styles/list.css" rel="stylesheet" />
    </style>
</head>
<body>


    <form id="form1" runat="server">
        <div class="top">
            <div class="ticon_parts"></div>
            <div class="title" id="detailbtn">部件信息</div>
            <div class="counts"><span id="pcounts"></span>个</div>
            <div class="minbtn"></div>
            <div class="closebtn" id="closebtn" onclick="list.close();"></div>
            <div class="topline"></div>
        </div>

        <div class="search">
            <input id="search" value="请输入搜索内容" onfocus="if (value =='请输入搜索内容'){value =''}"
                onblur="if(this.value===''){this.value='请输入搜索内容'}" />
            <div class="search_btn" onclick="list.searchBtnClick();"></div>
        </div>
        <div class="cloumnames">
            <div>名称</div>
            <div class="type" style="display:none;text-align:center;width:120px">类型</div>
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
                    <div class="statusicon_parts" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                    <div style="width: 160px;">钟公庙街道</div>
                    <div class="topline" style="width: 100%; height: 1px;"></div>
                </a>
            </div>
            <div class="personlist_list">
                <a href="javascript:void(0)" style="color: #87a7c8;">
                    <div class="statusicon_parts" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                    <div style="width: 160px;">钟公庙街道</div>
                    <div class="topline" style="width: 100%; height: 1px;"></div>
                </a>
            </div>
            <div class="personlist_list">
                <a href="javascript:void(0)" style="color: #87a7c8;">
                    <div class="statusicon_parts" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                    <div style="width: 160px;">钟公庙街道</div>
                    <div class="topline" style="width: 100%; height: 1px;"></div>
                </a>
            </div>
            <div class="personlist_list">
                <a href="javascript:void(0)" style="color: #87a7c8;">
                    <div class="statusicon_parts" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                    <div style="width: 160px;">钟公庙街道</div>
                    <div class="topline" style="width: 100%; height: 1px;"></div>
                </a>
            </div>
            <div class="personlist_list">
                <a href="javascript:void(0)" style="color: #87a7c8;">
                    <div class="statusicon_parts" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                    <div style="width: 160px;">钟公庙街道</div>
                    <div class="topline" style="width: 100%; height: 1px;"></div>
                </a>
            </div>
            <div class="personlist_list">
                <a href="javascript:void(0)" style="color: #87a7c8;">
                    <div class="statusicon_parts" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                    <div style="width: 160px;">钟公庙街道</div>
                    <div class="topline" style="width: 100%; height: 1px;"></div>
                </a>
            </div>
            <div class="personlist_list">
                <a href="javascript:void(0)" style="color: #87a7c8;">
                    <div class="statusicon_parts" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                    <div style="width: 160px;">钟公庙街道</div>
                    <div class="topline" style="width: 100%; height: 1px;"></div>
                </a>
            </div>
            <div class="personlist_list">
                <a href="javascript:void(0)" style="color: #87a7c8;">
                    <div class="statusicon_parts" style="display: block; width: 30px; height: 30px; line-height: 30px;"></div>
                    <div style="width: 160px;">钟公庙街道</div>
                    <div class="topline" style="width: 100%; height: 1px;"></div>
                </a>
            </div>
        </div>
        <input id="pagecounts" type="hidden" value="0" />
        <div class="page">
            <div class="wrapper">
                <div class="gigantic pagination">
                    <a href="#" class="first" data-action="first">&laquo;</a>
                    <a href="#" class="previous" data-action="previous">&lsaquo;</a>
                    <input type="text" readonly="readonly" disabled="true" />

                    <a href="#" class="next" data-action="next">&rsaquo;</a>
                    <a href="#" class="last" data-action="last">&raquo;</a>
                </div>
            </div>
        </div>
        <%--<button id="detailbtn">详情</button>--%>

        <%-- <button id="expbtn">展开</button>
            <button id="collbtn">收缩</button>--%>
    </form>
</body>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/page/jquery.jqpagination.js"></script>
<script src="../../Scripts/parts.list.js"></script>
</html>

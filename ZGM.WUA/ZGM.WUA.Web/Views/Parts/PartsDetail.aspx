<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PartsDetail.aspx.cs" Inherits="ZGM.WUA.Web.Views.Parts.PartsDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>部件详情</title>
    <link href="../../Styles/list.css" rel="stylesheet" />
    <link href="/Styles/listdetail.css" rel="stylesheet" />
</head>
<body>
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
            <div class="personbase" style="display: block;">
                <div class="ptitle">
                    部件详情
                </div>
                <div class="ptitle_space"></div>
                <div class="ptitle_option">
                    <div class="personbase1">基本信息</div>
                    <div class="ppolice_line" style="width: 562px; height: 1px; display: block; margin-left: 4px;"></div>
                </div>
                <div class="ptitle_space"></div>
                <div class="partsbaseinfo" style="color: #fff">
                    <div id="allbg_part" style="">
                        <div id="showLoading" class="showLoading_part">
                            <img src="/images/list/loading2.gif" style="width: 20px; height: 20px; margin-left: -30px" />
                            <br />
                            <p style="font-size: 16px; color: white">正在加载中...</p>
                        </div>
                    </div>
                    <%--      <div class="partsbaseinfo_left">
                        <div>
                            <span>路段名称:</span>
                            <span style="color: #3cd93c;" id="roadName"></span>
                        </div>
                        <div>
                            <span>总宽度(m):</span>
                            <span></span>
                        </div>
                        <div>
                            <span>车行道宽度(m):</span>
                            <span></span>
                        </div>
                        <div>
                            <span>车行道面积(㎡):</span>
                            <span style="color: #ff9211;"></span>
                        </div>
                    </div>
                    <div class="partsbaseinfo_right">
                        <div>
                            <span>总长度(m):</span>
                            <span></span>
                        </div>
                        <div>
                            <span>总面积(㎡):</span>
                            <span style="color: #f6ef37;"></span>
                        </div>
                        <div>
                            <span>人行道宽度(m):</span>
                            <span></span>
                        </div>
                        <div>
                            <span>人行道面积(㎡):</span>
                            <span style="color: #ff3030;"></span>
                        </div>
                    </div>--%>
                </div>

            </div>
        </div>

    </form>
</body>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/parts.detail.js"></script>
</html>


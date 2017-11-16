<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BMDDetail.aspx.cs" Inherits="ZGM.WUA.Web.Views.BMD.BMDDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>白名单详细</title>
    <link href="/Styles/listdetail.css" rel="stylesheet" />
    <style>
        span {
            width: 100px;
        }

        .columm1 {
            width: 100px;
            margin-left: 10px;
            float: left;
        }

        .columm2 {
            width: 150px;
            height: 14px;
            float: left;
        }
    </style>
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
            <%--基本消息--%>
            <div class="personbase" style="display: block;">
                <div class="ptitle">
                    白名单详情
                </div>
                <div class="ptitle_space"></div>
                <div class="ptitle_option">
                    <div class="personbase1 ">基本信息</div>
                    <%--<div class="preport1">人员上报</div>
                    <div class="personbasedetail1">人员报警</div>--%>
                    <div class="ppolice_line" style="width: 562px; height: 1px; display: block; margin-left: 4px;"></div>
                </div>
                <div class="personbasedetail">
                    <div class="personnum_BMD">
                        <div class="columm1" style="margin-left: 40px;">姓名:</div>
                        <div class="columm2 colorRed" id="xm" ></div>

                        <div class="columm1" style="width: 100px;">别名:</div>
                        <div class="columm2" id="name2"></div>

                    </div>

                    <div class="personnum_BMD">

                        <div class="columm1" style="margin-left: 40px;">性别:</div>
                        <div class="columm2" id="Div1" ></div>

                        <div class="columm1" style="width: 100px;">民族:</div>
                        <div class="columm2" id="Div2"></div>

                    </div>
                    <div class="personnum_BMD">
                        <div class="columm1" style="margin-left: 40px;">出生日期:</div>
                        <div class="columm2" id="Div3" ></div>

                        <div class="columm1" style="width: 100px;">文化程度:</div>
                        <div class="columm2" id="Div4"></div>


                    </div>
                    <div class="personnum_BMD">
                        <div class="columm1" style="margin-left: 40px;">职业:</div>
                        <div class="columm2" id="Div5"></div>

                        <div class="columm1" style="width: 100px;">原政治面貌:</div>
                        <div class="columm2" id="Div6"></div>


                    </div>
                    <div class="personnum_BMD">
                        <div class="columm1" style="margin-left: 40px;">类型:</div>
                        <div class="columm2" id="Div7" ></div>

                        <div class="columm1" style="width: 100px;">身份证:</div>
                        <div class="columm2 colorYellow" id="Div8"></div>

                    </div>
                    <div class="personnum_BMD">
                        <div class="columm1" style="margin-left: 40px;">籍贯:</div>
                        <div class="columm2 colorYellow" id="Div9"></div>

                        <div class="columm1" style="width: 100px;">户籍所在地:</div>
                        <div class="columm2 colorYellow" id="Div10"></div>

                    </div>
                    <div class="personnum_BMD">
                        <div class="columm1" style="margin-left: 40px;">固定居住地:</div>
                        <div class="columm2 colorYellow" id="Div11"></div>

                        <%--<div class="columm1" style="width: 100px;">常用居住地:</div>
                        <div class="columm2" id="Div12"></div>--%>

                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/page/jquery.jqpagination.js"></script>
<script src="/Scripts/BMD.detail.js"></script>
<%--<script src="../../Scripts/events.list.js"></script>--%>
</html>

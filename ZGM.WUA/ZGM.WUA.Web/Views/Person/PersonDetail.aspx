<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonDetail.aspx.cs" Inherits="ZGM.WUA.Web.Views.Person.PersonDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>人员报警</title>
    <link href="/Styles/listdetail.css" rel="stylesheet" />
    <style>
        .ptitle_option {
            /*width: 300px;*/
            height: 21px;
            display: block;
            color: #ffffff;
            font-size: 13px;
            font-family: 'Microsoft YaHei';
           /*margin: 0 auto;*/
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
                <div class="ptitle" style="color: #ffffff; font-size: 14px; font-family: 'Microsoft YaHei';">
                    人员详情
                </div>
                <div class="ptitle_space"></div>
                <div class="ptitle_option">
                    <div class="personbase1  current" style="margin-left:130px">基本信息</div>
                    <div class="preport1">人员上报</div>
                    <div class="personbasedetail1">人员报警</div>
                    <div class="ppolice_line" style="width: 562px; height: 1px; display: block; margin-left: 4px;"></div>
                </div>
                <div class="personbasetop">
                    <div class="personbasepic"></div>
                    <div class="personbasename"></div>

                </div>
                <div class="personbasedetail">
                    <div class="personnum">
                        <span style="margin-left: 20px;">人员编号:</span><span id="personcode" style="color: #44f740;"></span>
                        <div class="personaddress">
                            <span style="width: 100px;">部门:</span><span id="unit" style="color: #fff"></span>
                        </div>
                    </div>

                    <div class="personnum">
                        <span style="margin-left: 20px;">性别:</span><span id="gender" style="color: #fff"></span>
                        <div class="personaddress">
                            <span style="width: 100px;">职务:</span>
                            <span id="position" style="color: #fff; overflow: hidden; white-space: nowrap; text-overflow: ellipsis; width: 140px;"></span>
                        </div>
                        <div class="ptitle_space"></div>
                    </div>
                    <div class="personnum">
                        <span style="margin-left: 20px;">短号:</span><span id="shotnum" style="color: #fff"></span>
                        <div class="personaddress">
                            <span style="width: 100px;"></span><span></span>
                        </div>
                    </div>
                    <div class="personnum">
                        <span style="margin-left: 20px;">电话:</span><span id="phone" style="color: #ff9211"></span>
                        <div class="personaddress">
                            <span style="width: 100px;"></span><span></span>
                        </div>
                    </div>
                </div>
            </div>
            <%--人员上报--%>
            <div class="preport" style="display: none;">
                <div class="ptitle" style="">人员上报</div>
                <div class="ptitle_space"></div>
                <div class="ptitle_option">
                    <div class="personbase1" style="margin-left:130px">基本信息</div>
                    <div class="preport1">人员上报</div>
                    <div class="personbasedetail1">人员报警</div>
                    <div class="ppolice_line" style="width: 562px; height: 1px; display: block; margin-left: 4px;"></div>
                </div>
                <div class="ptitle_space"></div>
                <div class="plist_name">
                    <div style="width: 50px; height: 20px; line-height: 20px;">紧急</div>
                    <div style="width: 130px; height: 20px; line-height: 20px;">时间</div>
                    <div style="width: 150px; height: 20px; line-height: 20px;">事件编号</div>
                    <div style="width: 70px; height: 20px; line-height: 20px;">事件小类</div>
                    <div style="width: 100px; height: 20px; line-height: 20px;">地址</div>
                    <div style="width: 70px; height: 20px; line-height: 20px;">状态</div>
                    <div class="ppolice_line01" style="width: 562px; height: 1px; margin-left: 10px;"></div>
                </div>
                <div class="PersonReportinfo" style="color: #fff">
                    <%--<div class="ppolist_list" style="color: #56e15c;">
                        <div style="width: 50px; height: 30px; line-height: 30px;" class="preportsta"></div>
                        <div style="width: 130px; height: 30px; line-height: 30px;">2016-04-13 15:55</div>
                        <div style="width: 150px; height: 30px; line-height: 30px;">XC2016322</div>
                        <div style="width: 70px; height: 30px; line-height: 30px;">事件小类</div>
                        <div style="width: 100px; height: 30px; line-height: 30px;">钟公庙街道</div>
                        <div style="width: 70px; height: 30px; line-height: 30px;">已处理</div>
                        <div class="ppolice_line02" style="width: 562px; height: 1px; margin-left: 10px;"></div>
                    </div>
                     <div class="ppolist_list" style="color: #56e15c;">
                        <div style="width: 50px; height: 30px; line-height: 30px;" class="preportsta"></div>
                        <div style="width: 130px; height: 30px; line-height: 30px;">2016-04-13 15:55</div>
                        <div style="width: 150px; height: 30px; line-height: 30px;">XC2016322</div>
                        <div style="width: 70px; height: 30px; line-height: 30px;">事件小类</div>
                        <div style="width: 100px; height: 30px; line-height: 30px;">钟公庙街道</div>
                        <div style="width: 70px; height: 30px; line-height: 30px;">已处理</div>
                        <div class="ppolice_line02" style="width: 562px; height: 1px; margin-left: 10px;"></div>
                    </div>
                     <div class="ppolist_list" style="color: #56e15c;">
                        <div style="width: 50px; height: 30px; line-height: 30px;" class="preportsta"></div>
                        <div style="width: 130px; height: 30px; line-height: 30px;">2016-04-13 15:55</div>
                        <div style="width: 150px; height: 30px; line-height: 30px;">XC2016322</div>
                        <div style="width: 70px; height: 30px; line-height: 30px;">事件小类</div>
                        <div style="width: 100px; height: 30px; line-height: 30px;">钟公庙街道</div>
                        <div style="width: 70px; height: 30px; line-height: 30px;">已处理</div>
                        <div class="ppolice_line02" style="width: 562px; height: 1px; margin-left: 10px;"></div>
                    </div>
                     <div class="ppolist_list" style="color: #56e15c;">
                        <div style="width: 50px; height: 30px; line-height: 30px;" class="preportsta"></div>
                        <div style="width: 130px; height: 30px; line-height: 30px;">2016-04-13 15:55</div>
                        <div style="width: 150px; height: 30px; line-height: 30px;">XC2016322</div>
                        <div style="width: 70px; height: 30px; line-height: 30px;">事件小类</div>
                        <div style="width: 100px; height: 30px; line-height: 30px;">钟公庙街道</div>
                        <div style="width: 70px; height: 30px; line-height: 30px;">已处理</div>
                        <div class="ppolice_line02" style="width: 562px; height: 1px; margin-left: 10px;"></div>
                    </div>--%>
                </div>
                <div class="page">
                    <%--<div class="page_left">
                        <span style="color: #5d83ab;">您所在当前第1页还有</span>
                        <span style="color: #ada823;">4页未读</span>
                        <span style="color: #5d83ab;">,共12页</span>
                    </div>
                    <div class="page_right">
                        <a class="last"><</a>
                        <a>1</a>
                        <a>2</a>
                        <a>3</a>
                        <a class="next">></a>
                        <div class="endpage">尾页</div>
                    </div>--%>
                    <div class="wrapper">
                        <div class="gigantic pagination">
                            <a href="#" class="first" data-action="first"><<</a>
                            <a href="#" class="previous" data-action="previous"><</a>
                            <input type="text" readonly="readonly" disabled="true" />

                            <a href="#" class="next" data-action="next">></a>
                            <a href="#" class="last" data-action="last">>></a>
                        </div>
                    </div>
                </div>
            </div>
            <%--人员报警--%>
            <div class="ppolice" style="display: none;">
                <div class="ptitle" style="">人员报警</div>
                <div class="ptitle_space"></div>
                <div class="ptitle_option">
                    <div class="personbase1" style="margin-left:130px">基本信息</div>
                    <div class="preport1">人员上报</div>
                    <div class="personbasedetail1">人员报警</div>
                    <div class="ppolice_line" style="width: 562px; height: 1px; display: block; margin-left: 4px;"></div>
                </div>
                <div class="ptitle_space"></div>
                <div class="plist_name">
                    <div style="width: 70px; height: 20px; line-height: 20px;">状态</div>
                    <div style="width: 100px; height: 20px; line-height: 20px;">名称</div>
                    <div style="width: 140px; height: 20px; line-height: 20px;">发现时间</div>
                    <div style="width: 160px; height: 20px; line-height: 20px;">报警原因</div>
                    <div style="width: 110px; height: 20px; line-height: 20px;">处理状态</div>
                    <div class="ppolice_line01" style="width: 562px; height: 1px; margin-left: 10px;"></div>
                </div>
                <div class="ppoliceinfo" style="color: #fff">
                    <%-- <div class="ppolist_list" style="color: #56e15c;">
                        <div style="width: 70px; height: 30px; line-height: 30px;" class="ppolicesta"></div>
                        <div style="width: 100px; height: 30px; line-height: 30px;">管理员</div>
                        <div style="width: 140px; height: 30px; line-height: 30px;">2016-04-13 15:55:16</div>
                        <div style="width: 160px; height: 30px; line-height: 30px;">后院起火了</div>
                        <div style="width: 110px; height: 30px; line-height: 30px;">已生效</div>
                        <div class="ppolice_line02" style="width: 562px; height: 1px; margin-left: 10px;"></div>
                    </div>
                    <div class="ppolist_list" style="color: #989898;">
                        <div style="width: 70px; height: 30px; line-height: 30px;" class="ppolicesta"></div>
                        <div style="width: 100px; height: 30px; line-height: 30px;">管理员</div>
                        <div style="width: 140px; height: 30px; line-height: 30px;">2016-04-13 15:55:16</div>
                        <div style="width: 160px; height: 30px; line-height: 30px;">后院起火了</div>
                        <div style="width: 110px; height: 30px; line-height: 30px;">已作废</div>
                        <div class="ppolice_line02" style="width: 562px; height: 1px; margin-left: 10px;"></div>
                    </div>
                    <div class="ppolist_list" style="color: #56e15c;">
                        <div style="width: 70px; height: 30px; line-height: 30px;" class="ppolicesta01"></div>
                        <div style="width: 100px; height: 30px; line-height: 30px;">管理员</div>
                        <div style="width: 140px; height: 30px; line-height: 30px;">2016-04-13 15:55:16</div>
                        <div style="width: 160px; height: 30px; line-height: 30px;">后院起火了</div>
                        <div style="width: 110px; height: 30px; line-height: 30px;">已生效</div>
                        <div class="ppolice_line02" style="width: 562px; height: 1px; margin-left: 10px;"></div>
                    </div>
                    <div class="ppolist_list" style="color: #989898;">
                        <div style="width: 70px; height: 30px; line-height: 30px;" class="ppolicesta01"></div>
                        <div style="width: 100px; height: 30px; line-height: 30px;">管理员</div>
                        <div style="width: 140px; height: 30px; line-height: 30px;">2016-04-13 15:55:16</div>
                        <div style="width: 160px; height: 30px; line-height: 30px;">后院起火了</div>
                        <div style="width: 110px; height: 30px; line-height: 30px;">已作废</div>
                        <div class="ppolice_line02" style="width: 562px; height: 1px; margin-left: 10px;"></div>
                    </div>
                    <div class="ppolist_list" style="color: #ada823;">
                        <div style="width: 70px; height: 30px; line-height: 30px;" class="ppolicesta01"></div>
                        <div style="width: 100px; height: 30px; line-height: 30px;">管理员</div>
                        <div style="width: 140px; height: 30px; line-height: 30px;">2016-04-13 15:55:16</div>
                        <div style="width: 160px; height: 30px; line-height: 30px;">后院起火了</div>
                        <div style="width: 110px; height: 30px; line-height: 30px;">处理中 </div>
                        <div class="ppolice_line02" style="width: 562px; height: 1px; margin-left: 10px;"></div>
                    </div>
                    <div class="ppolist_list" style="color: #56e15c;">
                        <div style="width: 70px; height: 30px; line-height: 30px;" class="ppolicesta"></div>
                        <div style="width: 100px; height: 30px; line-height: 30px;">管理员</div>
                        <div style="width: 140px; height: 30px; line-height: 30px;">2016-04-13 15:55:16</div>
                        <div style="width: 160px; height: 30px; line-height: 30px;">后院起火了</div>
                        <div style="width: 110px; height: 30px; line-height: 30px;">已生效</div>
                        <div class="ppolice_line02" style="width: 562px; height: 1px; margin-left: 10px;"></div>
                    </div>
                    <div class="ppolist_list" style="color: #989898;">
                        <div style="width: 70px; height: 30px; line-height: 30px;" class="ppolicesta"></div>
                        <div style="width: 100px; height: 30px; line-height: 30px;">管理员</div>
                        <div style="width: 140px; height: 30px; line-height: 30px;">2016-04-13 15:55:16</div>
                        <div style="width: 160px; height: 30px; line-height: 30px;">后院起火了</div>
                        <div style="width: 110px; height: 30px; line-height: 30px;">已作废</div>
                        <div class="ppolice_line02" style="width: 562px; height: 1px; margin-left: 10px;"></div>
                    </div>
                    <div class="ppolist_list" style="color: #989898;">
                        <div style="width: 70px; height: 30px; line-height: 30px;" class="ppolicesta01"></div>
                        <div style="width: 100px; height: 30px; line-height: 30px;">管理员</div>
                        <div style="width: 140px; height: 30px; line-height: 30px;">2016-04-13 15:55:16</div>
                        <div style="width: 160px; height: 30px; line-height: 30px;">后院起火了</div>
                        <div style="width: 110px; height: 30px; line-height: 30px;">已作废</div>
                        <div class="ppolice_line02" style="width: 562px; height: 1px; margin-left: 10px;"></div>
                    </div>--%>
                </div>
                <div class="page">
                    <%--<div class="page_left">
                        <span style="color: #5d83ab;">您所在当前第1页还有</span>
                        <span style="color: #ada823;">4页未读</span>
                        <span style="color: #5d83ab;">,共12页</span>
                    </div>
                    <div class="page_right">
                        <a class="last"><</a>
                        <a>1</a>
                        <a>2</a>
                        <a>3</a>
                        <a class="next">></a>
                        <div class="endpage">尾页</div>
                    </div>--%>
                    <div class="wrapper">
                        <div class="gigantic pagination ppoliceinfopage">
                            <a href="#" class="first" data-action="first"><<</a>
                            <a href="#" class="previous" data-action="previous"><</a>
                            <input type="text" readonly="readonly" disabled="true" />

                            <a href="#" class="next" data-action="next">></a>
                            <a href="#" class="last" data-action="last">>></a>
                        </div>
                    </div>
                </div>

            </div>
        </div>

    </form>
</body>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/page/jquery.jqpagination.js"></script>
<script src="/Scripts/person.detail.js"></script>
<%--<script src="../../Scripts/events.list.js"></script>--%>
</html>

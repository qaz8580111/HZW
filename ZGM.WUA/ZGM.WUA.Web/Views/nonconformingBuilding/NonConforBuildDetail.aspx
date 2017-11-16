<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NonConforBuildDetail.aspx.cs" Inherits="ZGM.WUA.Web.Views.nonconformingBuilding.NonConforBuildDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>违建详细</title>
    <link href="/Styles/listdetail.css" rel="stylesheet" />
    <style>
        .ptitle_option .scroll {
            overflow-y: scroll;
            overflow-x: hidden;
            height: 100px;
            width: 99%;
        }

        .columm1 {
            width: 120px;
        }

        .columm2 {
            width: 140px;
        }

        .columm3 {
            margin-left: 20px;
            width: 60px;
            min-width: 60px;
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
                    违建详情
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
                        <div class="columm1" style="margin-left: 40px;">违建单位（个人）:</div>
                        <div class="columm2" id="xm"></div>

                        <div class="columm3" style="width: 60px;">所在片区:</div>
                        <div class="columm2 colorYellow" id="name2"></div>

                    </div>

                    <div class="personnum_BMD">

                        <div class="columm1" style="margin-left: 40px;">违建用途:</div>
                        <div class="columm2" id="Div1"></div>

                        <div class="columm3">当前状态:</div>
                        <div class="columm2 colorRed" id="Div2"></div>

                    </div>
                    <div class="personnum_BMD">
                        <div class="columm1" style="margin-left: 40px;">个人或法人代表身份:</div>
                        <div class="columm2 colorYellow" id="Div3"></div>

                        <div class="columm3">联系电话:</div>
                        <div class="columm2 colorYellow" id="Div4"></div>


                    </div>
                    <div class="personnum_BMD">
                        <div class="columm1" style="margin-left: 40px;">违建地点:</div>
                        <div class="columm2" id="Div5"></div>

                        <div class="columm3">拆除时间:</div>
                        <div class="columm2" id="Div6"></div>
                    </div>
                    <div class="personnum_BMD">
                        <div class="columm1" style="margin-left: 40px;">建筑面积:</div>
                        <div class="columm2" id="Div7"></div>

                        <div class="columm3">拆除面积:</div>
                        <div class="columm2" id="Div8"></div>
                    </div>
                    <%--<div class="ptitle_option">
                        <div class="personbase1 ">附件</div>
                        <div class="ppolice_line" style="width: 562px; height: 1px; display: block; margin-left: 4px;"></div>
                        <div id="Files" style="width: 100%">
                         
                        </div>
                    </div>--%>
                    <div class="ptitle_option">
                        <div class="personbase1 ">附件</div>
                        <div class="ppolice_line" style="width: 562px; height: 1px; display: block; margin-left: 4px;"></div>
                        <div class="scroll" id="scrollContent">
                            <table style='margin-left: 40px;' id="tableFile">
                                <%-- <tr> 
                                <td class="tbHide">汇报日期1</td>                              
                            </tr>
                             <tr > 
                                <td class="tbHide">汇报日期2</td>                              
                            </tr>
                             <tr> 
                                <td class="tbHide">汇报日期3</td>                              
                            </tr>
                             <tr> 
                                <td class="tbHide">汇报日期4</td>                              
                            </tr>
                             <tr> 
                                <td class="tbHide">汇报日期5</td>                              
                            </tr>--%>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/page/jquery.jqpagination.js"></script>
<script src="/Scripts/NonConforBuild.detail.js"></script>
<script src="../../Scripts/Communicate/jquery.nicescroll.js"></script>
<%--<script src="../../Scripts/events.list.js"></script>--%>
<script>
    $('#scrollContent').niceScroll({
        cursorcolor: "#12dffc",//#CC0071 光标颜色
        cursoropacitymax: 1, //改变不透明度非常光标处于活动状态（scrollabar“可见”状态），范围从1到0
        touchbehavior: false, //使光标拖动滚动像在台式电脑触摸设备
        cursorwidth: "5px", //像素光标的宽度
        cursorborder: "0", // 	游标边框css定义
        cursorborderradius: "5px",//以像素为光标边界半径
        autohidemode: false //是否隐藏滚动条
    });
</script>
</html>
<style>
    
    </style>

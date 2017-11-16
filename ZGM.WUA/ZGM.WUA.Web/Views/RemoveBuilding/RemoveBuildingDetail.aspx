<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RemoveBuildingDetail.aspx.cs" Inherits="ZGM.WUA.Web.Views.RemoveBuilding.RemoveBuildingDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>拆迁详细</title>
    <link href="/Styles/listdetail.css" rel="stylesheet" />
    <style>
        .ptitle_option .scroll {
            overflow-y: scroll;
            overflow-x: hidden;
            height: 100px;
            width: 99%;
        }
        #table1 td {        
        /*border:inset 1px #fff;*/
        }

        .columm1 {
            /*width: 60px;
            min-width: 60px;*/
        }

        .columm2 {
            /*width: 80px;
            min-width: 80px;*/
            /*margin-left:20px*/
        }

        .columm3 {
            /*width: 90px;
            min-width: 90px;*/
        }

        .personnum_BMD {
            float: none;
        }

            .personnum_BMD .columm1 {
                width: 100px;
            }
        .ptitle_option1 .option1 {
        float:left;
        width:80px;
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
                    拆迁详情
                </div>
                <div class="ptitle_space"></div>
                <div class="ptitle_option" id="personal">
                    <div class="option current" style="margin-left: 40px">丈量摸底阶段</div>
                    <div class="option">签协阶段</div>
                    <div class="option">过渡阶段</div>
                    <div class="option">抽签安置阶段</div>
                    <div class="option">结账阶段</div>
                    <div class="ppolice_line" style="width: 562px; height: 1px; display: block; margin-left: 4px;"></div>
                </div>
                <div id="personalDetail">
                    <%-- 丈量摸底阶段 --%>
                    <div class="personbasedetail">
                        <div class="personnum_BMD" style="height: 35px">
                            <div class="columm1" style="margin-left: 40px;">项目负责人:</div>
                            <div class="columm2" id="xm"></div>

                            <div class="columm3">项目状态:</div>
                            <div class="columm2 colorRed" id="name2"></div>

                        </div>

                        <div class="personnum_BMD" style="height: 35px">

                            <div class="columm1" style="margin-left: 40px;">项目名称:</div>
                            <div class="columm2 colorYellow" id="Div1"></div>

                            <div class="columm3">权证记载面积:</div>
                            <div class="columm2" id="Div2"></div>

                        </div>
                        <div class="personnum_BMD" style="height: 35px">
                            <div class="columm1" style="margin-left: 40px;">户主姓名:</div>
                            <div class="columm2 colorRed" id="Div3"></div>

                            <div class="columm3">联系方式:</div>
                            <div class="columm2 colorYellow" id="Div4"></div>


                        </div>
                        <div class="personnum_BMD" style="height: 35px">
                            <div class="columm1" style="margin-left: 40px;">丈量面积:</div>
                            <div class="columm2" id="Div5"></div>

                            <div class="columm3">无证面积:</div>
                            <div class="columm2" id="Div6"></div>
                        </div>
                      <%--  <div class="personnum_BMD" style="height: 35px">
                            <div class="columm1" style="margin-left: 40px;">总补偿金额:</div>
                            <div class="columm2" id="Div7"></div>

                            <div class="columm3">所得税补偿金额:</div>
                            <div class="columm2" id="Div8"></div>
                        </div>--%>
                        <%-- 附件 --%>
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
                    <%-- 签协阶段 --%>
                    <div class="personbasedetail" style="display: none"></div>
                    <%-- 过渡阶段 --%>
                    <div class="personbasedetail" style="display: none"></div>
                    <%-- 抽签安置阶段 --%>
                    <div class="personbasedetail" style="display: none"></div>
                    <%-- 结账阶段 --%>
                    <div class="personbasedetail" style="display: none"></div>
                </div>
                <div id="Files" style="width: 100%">
                    <div class="ptitle_option">
                        <div class="option" style="margin-left: 0px">基本信息</div>
                       <%-- <div class="option" style="margin-left: 20px">支付信息</div>--%>

                        <div class="ppolice_line" style="width: 562px; height: 1px; display: block; margin-left: 4px;"></div>
                    </div>
                      <div class="personbasedetail">
                        <div class="personnum_BMD" style="height: 30px">
                            <div class="columm1" style="margin-left: 40px;">法人代表:</div>
                            <div class="columm2" id="Div9"></div>

                            <div class="columm3" style="margin-left:0px">法人代表联系方式:</div>
                            <div class="columm2 colorRed" id="Div10"></div>

                        </div>

                        <div class="personnum_BMD" style="height: 30px">

                            <div class="columm1" style="margin-left: 40px;">土地面积:</div>
                            <div class="columm2 colorYellow" id="Div11"></div>

                            <div class="columm3" style="margin-left:0px">有证建筑面积:</div>
                            <div class="columm2" style="width:180px" id="Div12"></div>

                        </div>
                        <div class="personnum_BMD" style="height: 30px">
                            <div class="columm1" style="margin-left: 40px;">无证建筑面积 :</div>
                            <div class="columm2 colorRed" id="Div13"></div>

                            <div class="columm3" style="margin-left:0px">备注:</div>
                            <div class="columm2 colorYellow" id="Div14"></div>


                        </div>
                        <div class="personnum_BMD" style="height: 30px">
                            <div class="columm1" style="margin-left: 40px;">签约时间:</div>
                            <div class="columm2" id="Div15"></div>

                            <div class="columm3" style="margin-left:0px">腾空时间:</div>
                            <div class="columm2" id="Div16"></div>
                        </div>
                        <div class="personnum_BMD" style="height: 30px">
                            <div class="columm1" style="margin-left: 40px;">总补偿金额:</div>
                            <div class="columm2" id="Div17"></div>

                            <div class="columm3" style="margin-left:0px">所得税补偿金额:</div>
                            <div class="columm2" id="Div18"></div>
                        </div>
                           <div class="personnum_BMD" style="height: 20px">
                            <div class="columm1" style="margin-left: 40px;">附件:</div>
                            <div class="columm2" id="Div7"></div>
                           
                        </div>
                        <%-- 附件 --%>
                        <div class="ptitle_option">
                            <div class="personbase1 ">支付信息</div>
                            <div class="ppolice_line" style="width: 562px; height: 1px; display: block; margin-left: 4px;"></div>
                            <div class="scroll" id="Div19">
                                <table  id="table1" style="width:100%">                              
                                </table>
                            </div>
                        </div>
                    </div>
                  <%-- <div class="fileClass" style="width: 100%; margin-top: 10px;color:#619ae2">
                                <span class="columm2 textAlign" style="margin-left:20px; width:200px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">支付时间</span>
                                <span class="columm2 textAlign" style="margin-left:20px;width:150px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">支付金额</span>
                                <span class="columm2 textAlign" style="margin-left:20px;width:150px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">备注</span>
                     
                       </div>  
                    <div class="fileClass" style="width: 100%; margin-top: 10px;">
                                 <span class="columm2 textAlign" style="margin-left:20px;width:200px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;"> (data[i].PaypalTime == null ? "" : data[i].PaypalTime.substr(0,10)) + '</span>
                                 <span class="columm2 textAlign" style="margin-left:20px;width:150px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">data[i].ParpalMoney + '</span>
                                 <span class="columm2 textAlign" style="margin-left:20px;width:150px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;"> data[i].Remarks + '</span>
                           </div>--%>
                </div>
            </div>
        </div>

    </form>
</body>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/page/jquery.jqpagination.js"></script>
<script src="/Scripts/RemoveBuilding.detail.js"></script>
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

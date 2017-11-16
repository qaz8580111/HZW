<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConstrDetail.aspx.cs" Inherits="ZGM.WUA.Web.Views.Constr.ConstrDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>白名单详细</title>
    <link href="/Styles/listdetail.css" rel="stylesheet" />
    <style>
        .ptitle_option .scroll {
            overflow-y: scroll;
            overflow-x: hidden;
            height: 100px;
            width: 99%;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">

        <div class="main">
            <div class="closebtn" id="closebtn" onclick="detail.close();"></div>
            <div class="minbtn" style=""></div>
            <%--基本消息--%>
            <div class="personbase" style="display: block;">
                <div class="ptitle">
                    工程详情
                </div>
                <div class="ptitle_space"></div>
                <div class="ptitle_option">
                    <div class="option current" style="margin-left: 40px">工程信息</div>
                     <div class="option">工程招标</div>
                    <div class="option">工程施工</div>
                    <div class="option">工程竣工</div>
                    <div class="option">工程审计</div>
                    <div class="ppolice_line" style="width: 562px; height: 1px; display: block; margin-left: 4px;"></div>
                </div>
                <div>
                      <%-- 工程信息 --%>
                        <div class="personbasedetail">
                        <div class="personnum_BMD" style="height: 30px">
                            <div class="columm1 column6" style="margin-left: 40px;">工程名称:</div>
                            <div class="columm2" id="Div11"></div>

                            <div class="columm3">工程类型:</div>
                            <div class="columm2 colorYellow" id="Div12"></div>

                        </div>

                        <div class="personnum_BMD" style="height: 30px">

                            <div class="columm1 column6" style="margin-left: 40px;">立项批文号:</div>
                            <div class="columm2" id="Div13"></div>

                            <div class="columm3">立项批准机关:</div>
                            <div class="columm2 colorRed" id="Div14"></div>

                        </div>
                        <div class="personnum_BMD" style="height: 30px">
                            <div class="columm1 column6" style="margin-left: 40px;">建设内容::</div>
                            <div class="columm2" id="Div15"></div>

                            <div class="columm3">工程地址:</div>
                            <div class="columm2" id="Div16"></div>


                        </div>
                        <div class="personnum_BMD" style="height: 30px">
                            <div class="columm1 column6" style="margin-left: 40px;">工程性质:</div>
                            <div class="columm2" id="Div17"></div>

                            <div class="columm3">预算资金(元):</div>
                            <div class="columm2" id="Div18"></div>
                        </div>
                        <div class="personnum_BMD" style="height: 30px">
                            <div class="columm1 column6" style="margin-left: 40px;">计划开工日期:</div>
                            <div class="columm2" id="Div19"></div>
                            <div class="columm3">计划竣工日期:</div>
                            <div class="columm2" id="Div20"></div>
                        </div>
                             <div class="personnum_BMD" style="height: 20px">
                            <div class="columm1 column6" style="margin-left: 40px;">工程阶段:</div>
                            <div class="columm2" id="Div21"></div>
                        </div>
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

                    <%-- 工程招标 --%>
                    <div class="personbasedetail"  style="display: none">
                        <div class="personnum_BMD" style="height: 30px">
                            <div class="columm1" style="margin-left: 40px;">招标日期:</div>
                            <div class="columm2" id="xm"></div>

                            <div class="columm3">招标方式:</div>
                            <div class="columm2" id="name2"></div>

                        </div>

                        <div class="personnum_BMD" style="height: 30px">

                            <div class="columm1" style="margin-left: 40px;">招标负责人:</div>
                            <div class="columm2" id="Div1"></div>

                            <div class="columm3">中标金额:</div>
                            <div class="columm2 colorRed" id="Div2"></div>

                        </div>
                        <div class="personnum_BMD" style="height: 30px">
                            <div class="columm1" style="margin-left: 40px;">中标公司:</div>
                            <div class="columm2 colorRed" id="Div3"></div>

                            <div class="columm3">中标负责人:</div>
                            <div class="columm2" id="Div4"></div>


                        </div>
                        <div class="personnum_BMD" style="height: 30px">
                            <div class="columm1" style="margin-left: 40px;">监理公司:</div>
                            <div class="columm2 colorYellow" id="Div5"></div>

                            <div class="columm3">监理公司负责人:</div>
                            <div class="columm2" id="Div6"></div>
                        </div>
                        <div class="personnum_BMD" style="height: 30px">
                            <div class="columm1" style="margin-left: 40px;">设计公司:</div>
                            <div class="columm2 colorYellow" id="Div7"></div>

                            <div class="columm3">设计公司负责人:</div>
                            <div class="columm2" id="Div8"></div>
                        </div>

                        <div class="personnum_BMD" style="height: 30px">
                            <div class="columm1" style="margin-left: 40px;">招标代理人:</div>
                            <div class="columm2" id="Div9"></div>

                            <div class="columm3">招标联系人:</div>
                            <div class="columm2" id="Div10"></div>
                        </div>
                       <%-- <div class="ptitle_option">
                            <div class="personbase1 ">附件</div>
                            <div class="ppolice_line" style="width: 562px; height: 1px; display: block; margin-left: 4px;"></div>
                            <div class="scroll" id="scrollContent">
                                <table style='margin-left: 40px;' id="tableFile">
                                    <tr> 
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
                            </tr>
                                </table>
                            </div>
                        </div>--%>
                    </div>
                    <%-- 施工 --%>
                    <div class="personbasedetail " style="display: none; margin-top: 8px;">
                        <div class="ptitle_option noCss" style="margin-left: 130px; font-size: 12px; width: 480px;">
                            <div class="option current">工程施工问题</div>
                            <div class="option">施工进度</div>
                            <div class="option">工程资金拨付</div>
                            <div class="" style="width: 300px; height: 1px; display: block; margin-left: 0px; background-color: rgb( 23, 203, 252 )"></div>
                        </div>
                        <div id="sgList"></div>
                    </div>
                    <%-- 竣工 --%>
                    <div class="personbasedetail" style="display: none">
                        <div class="personnum_BMD" style="height: 45px">
                            <div class="columm1 column6" style="margin-left: 40px;">竣工日期:</div>
                            <div class="columm2" id="Div23"></div>

                            <div class="columm3">是否按期竣工:</div>
                            <div class="columm2 colorYellow" id="Div24"></div>

                        </div>

                        <div class="personnum_BMD" style="height: 45px">

                            <div class="columm1 column6" style="margin-left: 40px;">超期天数:</div>
                            <div class="columm2" id="Div25"></div>

                            <div class="columm3">质量结果:</div>
                            <div class="columm2 colorRed" id="Div26"></div>

                        </div>
                        <div class="personnum_BMD" style="height: 45px">
                            <div class="columm1 column6" style="margin-left: 40px;">竣工说明:</div>
                            <div class="columm2" id="Div27"></div>

                            <div class="columm3">填报时间:</div>
                            <div class="columm2" id="Div28"></div>


                        </div>
                        <div class="personnum_BMD" style="height: 45px">
                            <div class="columm1 column6" style="margin-left: 40px;">计划工期(天):</div>
                            <div class="columm2" id="Div29"></div>

                            <div class="columm3">实际工期(天):</div>
                            <div class="columm2" id="Div30"></div>
                        </div>
                        <div class="personnum_BMD" style="height: 40px">
                            <div class="columm1 column6" style="margin-left: 40px;">开工日期:</div>
                            <div class="columm2" id="Div31"></div>
                            <%-- <div class="columm1 column6" style="margin-left: 40px;">竣工日期:</div>
                            <div class="columm2" id="Div22"></div>--%>

                        </div>

                    </div>
                    <%-- 审计 --%>
                    <div class="personbasedetail" style="display: none">
                        <div class="personnum_BMD" style="height: 45px">
                            <div class="columm1" style="margin-left: 40px; width: 100px">审计开始时间:</div>
                            <div class="columm2" id="Div32"></div>

                            <div class="columm3">审计结束时间:</div>
                            <div class="columm2" id="Div33"></div>

                        </div>

                        <div class="personnum_BMD" style="height: 45px">

                            <div class="columm1" style="margin-left: 40px; width: 100px;">审计单位:</div>
                            <div class="columm2 colorYellow" id="Div34"></div>

                            <div class="columm3">审计工程金额:</div>
                            <div class="columm2 colorRed" id="Div35"></div>

                        </div>
                        <div class="personnum_BMD" style="height: 45px">
                            <div class="columm1" style="margin-left: 40px; width: 100px;">审计扣款金额:</div>
                            <div class="columm2 colorRed" id="Div36"></div>

                            <div class="columm3">审计说明:</div>
                            <div class="columm2" id="Div37"></div>


                        </div>
                        <div class="personnum_BMD" style="height: 45px">
                            <div class="columm1" style="margin-left: 40px; width: 100px;">填报日期:</div>
                            <div class="columm2" id="Div38"></div>

                            <div class="columm3">送审日期:</div>
                            <div class="columm2" id="Div39"></div>
                        </div>

                    </div>

                </div>
            </div>
    </form>
</body>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/page/jquery.jqpagination.js"></script>
<script src="/Scripts/Constr.detail.js"></script>
<script src="../../Scripts/Communicate/jquery.nicescroll.js"></script>
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

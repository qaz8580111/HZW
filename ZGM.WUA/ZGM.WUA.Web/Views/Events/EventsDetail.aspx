<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventsDetail.aspx.cs" Inherits="ZGM.WUA.Web.Views.Events.EventsDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>事件详情</title>
    <link href="/Styles/listdetail.css" rel="stylesheet" />
    <link href="/Scripts/myfocus/js/mf-pattern/mF_games_tb.css" rel="stylesheet" />
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
                    事件详情
                </div>
                <div class="eventstitle_option" style="height: 41px;">
                    <div class="eventsbase1 current">事件处理前消息</div>
                    <div class="eventsbase2">事件处理前照片</div>
                    <div class="eventsbase3">事件处理后消息</div>
                    <div class="eventsbase4">事件处理后照片</div>
                   
                    <div class="ptitle_space"></div>
                </div>
                <div class="eventsbaseinfo" style="display: block;">
                    <div class="eventsbasenum">
                        <span class="eventcode" style="text-align: left; margin-left: 20px; width: 360px;display:block;float:left"></span>
                        <span class="events_police" style="width: 200px;display:block;float:left"></span>
                         <div class="ppolice_line" style="width: 562px; height: 1px; display: block; margin-left: 4px;margin-top:60px"></div>
                    </div>
                    <div class="partsbaseinfo_left">
                        <div>
                            <span>事件标题:</span>
                            <span id="eventtitle" style="color:#fff;width: 100px;margin-left:10px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;"></span>
                        </div>
                        <div>
                            <span>事件大类:</span>
                            <span id="bclass" style="color:#fff;margin-left:10px;"></span>
                        </div>
                        <div>
                            <span>相关联系人:</span>
                            <span id="contact" style="color:#fff;margin-left:10px;"></span>
                        </div>
                        <div>
                            <span>发现时间:</span>
                            <span id="foundtime" style="color:#fff;margin-left:10px;"></span>
                        </div>
                        <div>
                            <span>超时时间:</span>
                            <span id="overtime" style="color:#fff;margin-left:10px;"></span>
                        </div>
                    </div>
                    <div class="partsbaseinfo_right">
                        <div>
                            <span>事件来源:</span>
                            <span id="sourcename" style="color:#fff;"></span>
                        </div>
                        <div>
                            <span>事件小类:</span>
                            <span id="sclass" style="color:#fff;">乱堆放</span>
                        </div>
                        <div>
                            <span>相关联系人电话:</span>
                            <span id="contactphone" style="color: #e2d116;"></span>
                        </div>
                        <div>
                            <span>上报人员:</span>
                            <span id="createuser" style="color: #ff3030;"></span>
                        </div>
                    </div>
                    <div class="eventsbaseinfo_bottom">
                        <div>
                            <span style="margin-left: 40px; width: 100px;">事件地址:</span>
                            <span id="address" style="color: #3eb43c;margin-left:10px;"></span>
                        </div>
                        <div>
                            <span style="margin-left: 40px; width: 100px;">事件内容:</span>
                            <span id="eventcontent" style="color: #3eb43c;margin-left:10px;"></span>
                        </div>
                    </div>
                </div>
                <div class="eventsbaseinfo02" style="display: none;">
                    <div id="myFocus" style="width: 522px; height: 254px; position: relative; z-index: 9; text-align: center; background-image: url('/images/ppolice/ppolicebg.png'); background-position: -6px; display: none;">
                        <!--焦点图盒子-->
                        <div class="pic">
                            <!--图片列表-->

                            <ul id="myfocus01piclist">
                                <li>
                                    <a href="javascript:void();">
                                        <img thumb="" src="/Images/eventslist/eventspicbig01.png" alt="" />
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="eventsbaseinfo03" style="display: none;">
                    <div class="eventsbasenum">
                        <span class="eventcode" style="text-align: left; margin-left: 20px; width: 360px;">173874621</span>
                        <span class="events_police" style="width: 200px;"></span>
                    </div>
                    <div style="display: block; width: 580px; height: 38px; float: left; line-height: 38px;" class="partsbaseinfo_left">
                        <div style="width: 350px; height: 38px; display: inline-block; float: left;">
                            <span style="margin-left:40px;width:126px">当前状态:</span>
                            <span style="color: #1eff43; width: 126px;margin-left:0px" id="ecurentstatus"></span>
                        </div>
                        <%--<div style="width: 290px; height: 38px; display: inline-block;"><span style="width: 170px;">处理方式:</span><span style="width: 60px;" id="etypechoies"></span></div>--%>
                        <%--<div style="width: 580px; height: 17px; background-image: url('../../Images/eventslist/events_midbg.png'); background-repeat: no-repeat; background-position: center; background-position-x: 100px;"></div>--%>
                        <div style="width: 580px; height: 38px; text-align:left">
                            <span style="width: 126px;">环节</span>
                            <span style="width: 126px;margin:0px;">上报人</span>
                            <span style="width: 180px;margin:0px;">时间</span>
                            <span style="width: 60px;margin:0px;">意见</span>
                        </div>

                        <div id="aftereventsdetail" style="width: 580px;height:180px; overflow-x: hidden;overflow-y:auto;">
                            <%-- <div style="width: 580px; height: 38px; display: inline-block; float: left;">
                            <span>事件上报</span>
                            <span style="width: 126px;">张瑞</span>
                            <span style="width: 180px">2016-4-21 10:40:28</span>
                            <span style="width: 60px; color: #34E66F;">没意见</span>
                            </div>

                        <div style="width: 580px; height: 38px; display: inline-block; float: left;">
                            <span>结束</span>
                            <span style="width: 126px;">张瑞</span>
                            <span style="width: 180px">2016-4-21 10:40:28</span>
                            <span style="width: 60px; color: #CC4A37;">有意见</span>
                        </div>
                        </div>--%>
                        </div>
                    </div>

                </div>
                <div class="eventsbaseinfo04" style="display: block;">
                    <div id="myFocus02" style="width: 522px; height: 254px; position: relative; z-index: 9; text-align: center; background-image: url('/images/ppolice/ppolicebg.png'); background-position: -6px; display: none;">
                        <!--焦点图盒子-->
                        <div class="pic">
                            <!--图片列表-->
                            <ul id="myfocus02piclist">
                                <li>
                                    <a href="javascript:void();">
                                        <img thumb="" src="/Images/eventslist/eventspicbig01.png" alt="" />
                                    </a>
                                </li>

                            </ul>
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
<script src="../../Scripts/base/jquery-1.8.2.min.js"></script>
<script src="../../Scripts/myfocus/js/myfocus-2.0.1.full.js"></script>
<script src="/Scripts/events.detail.js"></script>
<script type="text/javascript">
    //设置
    myFocus.set({
        id: 'myFocus', //ID
        pattern: 'mF_games_tb'//风格

    });
    myFocus.set({
        id: 'myFocus02', //ID
        pattern: 'mF_games_tb'//风格

    });
</script>
</html>


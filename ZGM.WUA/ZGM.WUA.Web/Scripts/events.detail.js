$(".eventsbase1").click(function () {
    $(".eventsbase1").attr('class', 'eventsbase1 current');
    $(".eventsbase2").removeClass('current');
    $(".eventsbase3").removeClass('current');
    $(".eventsbase4").removeClass('current');
    $(".eventsbaseinfo").css('display', 'block');
    $(".eventsbaseinfo02").css('display', 'none');
    $(".eventsbaseinfo03").css('display', 'none');
    $(".eventsbaseinfo04").css('display', 'none');

});


$(".eventsbase2").click(function () {
    $(".eventsbase2").attr('class', 'eventsbase2 current');
    $(".eventsbase1").removeClass('current');
    $(".eventsbase3").removeClass('current');
    $(".eventsbase4").removeClass('current');
    $("#myFocus").css('display', 'block');
    $("#myFocus02").css('display', 'none');
    $(".eventsbaseinfo").css('display', 'none');
    $(".eventsbaseinfo02").css('display', 'block');
    $(".eventsbaseinfo03").css('display', 'none');
    $(".eventsbaseinfo04").css('display', 'none');
});


$(".eventsbase3").click(function () {
    $(".eventsbase3").attr('class', 'eventsbase3 current');
    $(".eventsbase1").removeClass('current');
    $(".eventsbase2").removeClass('current');
    $(".eventsbase4").removeClass('current');
    $(".eventsbaseinfo").css('display', 'none');
    $(".eventsbaseinfo02").css('display', 'none');
    $(".eventsbaseinfo03").css('display', 'block');
    $(".eventsbaseinfo04").css('display', 'none');
});

$(".eventsbase4").click(function () {
    $(".eventsbase4").attr('class', 'eventsbase4 current');
    $(".eventsbase1").removeClass('current');
    $(".eventsbase2").removeClass('current');
    $(".eventsbase3").removeClass('current');
    $(".eventsbaseinfo").css('display', 'none');
    $(".eventsbaseinfo02").css('display', 'none');
    $(".eventsbaseinfo03").css('display', 'none');
    $(".eventsbaseinfo04").css('display', 'block');
    $("#myFocus").css('display', 'none');
    $("#myFocus02").css('display', 'block');
});

$(function () {
    detail.init(parent.event.Event);
    parent.mapSW.deepView();
});

var detail = {
    apiconfig: null,
    picurl: null,
    Event: null,
    init: function (event) {
        this.Event = event;
        this.apiconfig = parent.globalConfig.apiconfig;
        this.picurl = parent.globalConfig.picurl;
        this.initDetailBefore(this.Event);
        this.initDetailAfter(this.Event);
        $(".minbtn").toggle(function () {
            detail.expand();
        }, function () {
            detail.collapse();
        });
    },
    initDetailBefore: function (event) {
        if (event.LevelNum == 1) {
            $(".events_police").css("visibility", "hidden");
        }
        $(".eventcode").html(event.EventCode);
        $("#eventtitle").html(event.EventTitle);
        $("#eventtitle").attr("title", event.EventTitle);
        $("#bclass").html(event.BClassName);
        $("#sclass").html(event.SClassName);
        $("#sourcename").html(event.SourceName);
        $("#contact").html(event.Contact);
        $("#contactphone").html(event.ContactPhone);
        $("#foundtime").html(event.FoundTime == null ? "" : event.FoundTime.replace("T", " "));
        $("#foundtime").attr("title", event.FoundTime == null ? "" : event.FoundTime.replace("T", " "));
        $("#overtime").html(event.OverTime == null ? "" : event.OverTime.replace("T", " "));
        $("#overtime").attr("title", event.OverTime == null ? "" : event.OverTime.replace("T", " "));
        $("#createuser").html(event.CreateUserName);
        $("#address").html(event.EventAddress);
        $("#eventcontent").html(event.EventContent);
        this.getEventsListPicBefore(event.ZFSJId);
    },
    initDetailAfter: function (event) {
        this.getEventsList(event.ZFSJId);
        this.getEventsListPicAfter(event.ZFSJId);
    },
    getEventsList: function (taskId) { //事件处理后消息
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/Task/GetTaskDisposes",
            data: { taskId: taskId },
            dataType: "json",
            success: function (data) {
                $("#aftereventsdetail").html("");
                $("#ecurentstatus").html("");
                for (var i = 0; i < data.length; i++) {
                    var content = data[i].Content == null ? "无意见" : data[i].Content;
                    var str = '<div style="width: 580px; height: 34px;text-align:left;color:#fff">' +
                             '<span style="width: 126px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + data[i].WFDName + '</span>' +
                             '<span style="width: 126px; margin:0px;overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + data[i].UserName + '</span>' +
                             '<span style="width: 180px;margin:0px;">' + (data[i].dealTime == null ? "无" : data[i].dealTime.replace('T', ' ')) + '</span>' +
                             '<span style="width: 60px; margin:0px;color: #34E66F;" title="' + content + '">' + content + '</span>' +
                             '</div>';
                    if ((i + 1) == data.length) {
                        $("#ecurentstatus").append(data[i].WFDName);//当前状态
                    }
                    $("#aftereventsdetail").append(str);//填充div            
                }
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getEventsListPicBefore: function (taskId) { //事件处理前图片
        $.ajax({
            type: "GET",
            async: true,
            url: detail.apiconfig + "/api/Task/GetFoundFilesByTaskId",
            data: { taskId: taskId },
            dataType: "json",
            success: function (data) {
                $("#myfocus01piclist").html("");
                for (var i = 0; i < data.length; i++) {
                    var uri = detail.picurl + "/GetZFSJPicByPath.ashx?PicPath=" + data[i].FilePath;
                    var str = '<li><a href="javascript:void();">' +
                              '<img thumb="" src="' + uri + '" alt="" />' +
                              '</a></li>';
                    $("#myfocus01piclist").append(str);
                }
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getEventsListPicAfter: function (taskId) { //事件处理后图片
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/Task/GetDisposeFilesByTaskId",
            data: { taskId: taskId },
            dataType: "json",
            success: function (data) {
                $("#myfocus02piclist").html("");
                for (var i = 0; i < data.length; i++) {
                    var uri = detail.picurl + "/GetZFSJPicByPath.ashx?PicPath=" + data[i].FilePath;
                    var str = '<li><a href="javascript:void();">' +
                              '<img thumb="" src="' + uri + '" alt="" />' +
                              '</a></li>';
                    $("#myfocus02piclist").append(str);
                }
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    collapse: function () {
        parent.detail.collapse();
        setTimeout(function () {
            $("body").css("background-image", "url('/images/ppolice/ppolicebg1.png')");
        }, 300);
    },
    expand: function () {
        parent.detail.expand();
        $("body").css("background-image", "url('/images/ppolice/ppolicebg.png')");
    },
    close: function () {
        parent.detail.close();
    },
}
$(function () {
    list.init(parent.event.SourceId);
});
var list = {
    sourceId: null,
    sumCount: null,
    apiconfig: null,
    takeNum: null,
    skipNum: 0,
    isFirstInit:0,
    eventAddress: null,
    events: null,
    init: function (sourceId) {
        this.apiconfig = parent.globalConfig.apiconfig;
        this.takeNum = parent.globalConfig.listTakeNum;
        this.eventAddress = this.getSearchContent;
        this.sourceId = sourceId;
        this.searchEvents();
        $(".minbtn").toggle(function () {
            list.collapse();
        }, function () {
            list.expand();
        });
        list.positionAllEvent();
    },
    searchEvents: function () {
        this.eventAddress = this.getSearchContent;
        this.setLoading();
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/Task/GetTasksCount",
            data: {
                eventAddress: this.eventAddress, sourceId: list.sourceId, bClassId: null, sClassId: null
                , levelNum: null, createUserId: null
            },
            dataType: "json",
            success: function (result) {
                list.setPageCount(result);
                list.initPage();
                list.isFirstInit = 1;
                list.getEventsList();
              
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getEventsList: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/Task/GetTasksByPage",
            data: {
                eventAddress: this.eventAddress, sourceId: this.sourceId, bClassId: null, sClassId: null
                , levelNum: null, createUserId: null, skipNum: this.skipNum, takeNum: this.takeNum
            },
            dataType: "json",
            success: function (data) {
                list.events = data;
                $(".personlist").html("");
                if (data.length == 0) {
                    $(".personlist").html('<p style="margin:0px;font-size:14px;color:#fff;text-align: center;line-height:200px">暂无数据</p>');
                }
                for (var i = 0; i < data.length; i++) {
                    var str = '<div class="personlist_list">'
                        + '<a href="javascript:void(0)"  onclick="list.eventClicked(' + data[i].ZFSJId + ');">'
                        + '<div class="statusicon_events' + data[i].LevelNum + '" style="display:block; width:38px;height:30px; line-height:30px;"></div>'
                        + '<div style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:60px;" title=' + data[i].EventCode + '>' + data[i].EventCode + '</div>'
                        + '<div style="float: right; text-align: left; width: 95px;overflow: hidden; white-space: nowrap; text-overflow: ellipsis;">' + data[i].SClassName + '</div>'
                        + '<div class="topline" style="width:190px; height:1px; margin-left:8px;"></div>'
                        + '</a>'
                   + '</div>'
                    $(".personlist").append(str);
                }
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    initPage: function () {
        if (this.isFirstInit == 1) {
            //list.takeNum=8;
            list.skipNum = 0;
            $('.pagination').jqPagination('destroy');
        }
        $('.pagination').jqPagination({
            current_page: 1,
            link_string: '/?page={page_number}',
            max_page: Math.ceil(this.sumCount / this.takeNum),
            paged: function (page) {
                list.skipNum = (page - 1) * 8;

                list.setLoading();
                list.getEventsList();
            }
        });
    },
    getSearchContent: function () {
        return $("#search").val() == "按事件地址查询" ? "" : $("#search").val();
    },
    setLoading: function () {
        $(".personlist").html("");
        $(".personlist").html('<div id="allbg"><div id="showLoading" class="showLoading" style="top:60px;left:45px;"><img src="/images/list/loading2.gif" style="width: 20px; height: 20px" />' +
        '<br /><p style="font-size: 17px;">正在加载中...</></div></div>');
    },
    setPageCount: function (sumCount) {
        this.sumCount = sumCount;
        $("#pcounts").html("");
        $("#pcounts").html(sumCount);
    },
    eventClicked: function (eventId) {
        for (var i = 0; i < list.events.length; i++) {
            if (this.events[i].ZFSJId == eventId) {
                //显示概要面板
                this.showDetailMin(this.events[i]);
                //地图定位
                this.positionEvent(this.events[i]);
            }
        }
    },
    showDetailMin: function (event) {
        parent.event.initEventDetailMin(event);
    },
    positionEvent: function (event) {
        parent.event.positionEvent(event);
    },
    positionAllEvent: function () {
        $('.allLocate').click(function () {
            $.ajax({
                type: "GET",
                async: true,
                url: list.apiconfig + "/api/Task/GetTasksByPage",
                data: {
                    eventAddress: list.eventAddress, sourceId: list.sourceId, bClassId: null, sClassId: null
                    , levelNum: null, createUserId: null, skipNum: null, takeNum: null
                },
                dataType: "json",
                success: function (data) {
                    parent.event.positionAllEvent(list.events);
                },
                error: function (errorMsg) {
                    console.log(errorMsg);
                }
            });           
        });
    },
    collapse: function () {
        parent.list.collapse();
    },
    expand: function () {
        parent.list.expand();
    },
    close: function () {
        parent.list.close();
    },
}
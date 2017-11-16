$(".personbase1").click(function () {
    $(".personbase").css('display', 'block');
    $(".preport").css('display', 'none');
    $(".ppolice").css('display', 'none');
    $(".personbase1").attr('class', 'personbase1 current');
    $(".preport1").removeClass('current');
    $(".personbasedetail1").removeClass('current');
});


$(".preport1").click(function () {
    $(".personbase").css('display', 'none');
    $(".preport").css('display', 'block');
    $(".ppolice").css('display', 'none');
    $(".preport1").attr('class', 'preport1 current');
    $(".personbase1").removeClass('current');
    $(".personbasedetail1").removeClass('current');
});


$(".personbasedetail1").click(function () {

    $(".personbase").css('display', 'none');
    $(".preport").css('display', 'none');
    $(".ppolice").css('display', 'block');
    $(".personbasedetail1").attr('class', 'personbasedetail1 current');
    $(".preport1").removeClass('current');
    $(".personbase1").removeClass('current');
});
$(function () {
    detail.init(parent.person.User);
    parent.mapSW.deepView();
});

var detail =
    {
        User: null,
        sumCount: null, //人员上报数量
        sumCountPP: null,//人员报警数量
        apiconfig: null,
        takeNum2: null,
        skipNum2: 0,
        takeNum: null,
        skipNum: 0,
        events: null,
        init: function (user) {
            this.User = user;
            this.apiconfig = parent.globalConfig.apiconfig;
            this.takeNum = parent.globalConfig.listTakeNum;
            this.takeNum2 = parent.globalConfig.listTakeNum;
            this.initDetail(this.User);
            this.getEventsCount();
            this.getEventsCountPP();
            //
            $(".minbtn").toggle(function () {
                detail.expand();
            }, function () {
                detail.collapse();
            });
        },
        initDetail: function (user) {
            $(".personbasename").html(user.UserName);
            $("#personcode").html(user.ZFZBH);
            $("#unit").html(user.UnitName);
            $("#gender").html(user.Sex);
            $("#position").html(user.UserPositionName);
            $("#shotnum").html(user.SPhone);
            $("#phone").html(user.Phone);
        },
        getEventsCount: function () {
            $.ajax({
                type: "GET",
                async: true,
                url: detail.apiconfig + "/api/Task/GetTasksCount",
                data: {
                    eventAddress: null, sourceId: null, bClassId: null, sClassId: null
                    , levelNum: null, createUserId: this.User.UserId
                },
                dataType: "json",
                success: function (result) {
                    detail.setPageCount(result);
                    detail.getEventsList();
                    detail.initPage();
                },
                error: function (errorMsg) {

                    console.log(errorMsg);
                }
            });
        },
        getEventsCountPP: function () { //人员报警数量获取
            $.ajax({
                type: "GET",
                async: true,
                url: detail.apiconfig + "/api/User/GetAlarmsCount",//接口需修改
                data: {
                    userId: this.User.UserId
                },
                dataType: "json",
                success: function (result) {
                    detail.setPageCountPP(result);
                    detail.getPersonPoliceList();
                    detail.initPage();
                },
                error: function (errorMsg) {

                    console.log(errorMsg);
                }
            });
        },
        getEventsList: function () {//人员上报
            $.ajax({
                type: "GET",
                async: true,
                url: this.apiconfig + "/api/Task/GetTasksByPage",
                data: {
                    eventAddress: null, sourceId: null, bClassId: null, sClassId: null
                    , levelNum: null, createUserId: this.User.UserId, skipNum: this.skipNum, takeNum: this.takeNum
                },
                dataType: "json",
                success: function (data) {
                    detail.events = data;
                    $(".PersonReportinfo").html("");
                    for (var i = 0; i < data.length; i++) {
                        var img = data[i].LevelNum == 1 ? "preportsta02" : "preportsta03";
                        var imgTip = data[i].LevelNum == 1 ? "<img src='/images/personDetail/一般.png'>" : "<img src='/images/personDetail/紧急.png'>";
                        var colorS = data[i].LevelNum == 1 ? "#56e15c" : "#fe7917";
                        var str = '<div class="ppolist_list" style="color: ' + colorS + '; cursor:pointer">' +
                        '<div style="width: 50px; height: 30px; line-height: 30px;" class="preportsta">' + imgTip + '</div>' +
                        '<div style="width: 130px; height: 30px; line-height: 30px;">' + (data[i].FoundTime == null ? "" : data[i].FoundTime.substr(0, 10)) + '</div>' +
                        '<div style="width: 150px; height: 30px; line-height: 30px;">' + data[i].EventCode + '</div>' +
                        '<div style="width: 70px; height: 30px; line-height: 30px;">' + data[i].SClassName + '</div>' +
                        '<div style="width: 100px; height: 30px; line-height: 30px;">' + data[i].EventAddress + '</div>' +
                        '<div style="width: 70px; height: 30px; line-height: 30px;">' + data[i].WFName + '</div>' +
                        '<div class="ppolice_line02" style="width: 562px; height: 1px; margin-left: 10px;"></div>' +
                    '</div>';

                        //var str = '<div class="ppolist_list" style="color: #56e15c;cursor:pointer">' +
                        //             '<div style="width: 50px; height: 30px; line-height: 30px;" title="' + imgTip + '"  class="' + img + '"></div>' +
                        //             '<div style="width: 130px; height: 30px; line-height: 30px;">' + data[i].FoundTime == null ? "" : data[i].FoundTime.substr(0, 10) + '</div>' +
                        //             '<div style="width: 150px; height: 30px; line-height: 30px;">' + data[i].EventCode + '</div>' +
                        //             '<div style="width: 70px; height: 30px; line-height: 30px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:70px;">' + data[i].SClassName + '</div>' +
                        //             '<div style="width: 100px; height: 30px; line-height: 30px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:100px;">' + data[i].EventAddress + '</div>' +
                        //             '<div style="width: 70px; height: 30px; line-height: 30px;">' + data[i].Status + '</div>' +
                        //             '<div class="ppolice_line02" style="width: 562px; height: 1px; margin-left: 10px;"></div></div>';
                        $(".PersonReportinfo").append(str);
                    }
                    detail.eventClick();
                },
                error: function (errorMsg) {
                    console.log(errorMsg);
                }
            });
        },
        eventClick: function () { //人员上报事件的点击
            $('.ppolist_list').each(function (i) {
                $(this).click(function () {
                    var eventInfo = detail.events[detail.skipNum + i];
                    parent.person.positionEventSB(eventInfo);
                })
            })
        },
        getPersonPoliceList: function () {//人员报警
            $.ajax({
                type: "GET",
                async: true,
                url: this.apiconfig + "/api/User/GetAlarmsByPage",
                data: {
                    UserId: this.User.UserId, skipNum: this.skipNum2, takeNum: this.takeNum2
                },
                dataType: "json",
                success: function (data) {
                    detail.events = data;
                    $(".ppoliceinfo").html("");
                    for (var i = 0; i < data.length; i++) {
                        var imgTip = detail.changeImg(data[i].TypeId);
                        var colorS = detail.changeColor(data[i].State);
                        var startTime = data[i].StartTime.replace("T", " ");
                        var endTime = data[i].EndTime.replace("T", " ");
                        var str = '<div class="ppolist_list2" style="color: ' + colorS + '; cursor:pointer" onclick="detail.alarm(\'' + startTime + '\',\'' + endTime + '\')">' +
                            '<div style="width: 70px; height: 30px; line-height: 30px;" class="">' + imgTip + '</div>' +
                            '<div style="width: 100px; height: 30px; line-height: 30px;">' + detail.User.UserName + '</div>' +
                            '<div style="width: 140px; height: 30px; line-height: 30px;">' + (data[i].StartTime == null ? "" : data[i].StartTime.substr(0, 10)) + '</div>' +
                            '<div style="width: 160px; height: 30px; line-height: 30px;">' + data[i].TypeName + '</div>' +
                            '<div style="width: 110px; height: 30px; line-height: 30px;">' + data[i].StateName + '</div>' +
                            '<div class="ppolice_line02" style="width: 562px; height: 1px; margin-left: 10px;"></div>' +
                        '</div>';

                        $(".ppoliceinfo").append(str);

                    }

                },
                error: function (errorMsg) {
                    console.log(errorMsg);
                }
            });
        },
        alarm: function (startTime, endTime) {
            parent.person.getUserAlarmArea(detail.User, startTime, endTime);
        },
        changeColor: function (type) {
            //处理状态  0未处理 1生效 2作废
            switch (type) {
                case 0: return "#d7b630";
                case 1: return "#56e15c";
                case 2: return "#989898";
            }
        },
        changeImg: function (type) {
            ////1.停留 2.越界 3.离线
            switch (type) {
                case 1: return "<img src='/images/personDetail/停留报警.png'>";
                case 2: return "<img src='/images/personDetail/越界报警.png'>";
                case 3: return "<img src='/images/personDetail/离线报警.png'>";
            }
        },
        initPage: function () {
            $('.pagination').jqPagination({
                current_page: 1,
                link_string: '/?page={page_number}',
                max_page: Math.ceil(this.sumCount / this.takeNum),
                paged: function (page) {
                    detail.skipNum = (page - 1) * 8;
                    detail.getEventsList();
                }
            });

            $('.ppoliceinfopage').jqPagination({
                current_page: 1,
                link_string: '/?page={page_number}',
                max_page: Math.ceil(this.sumCountPP / this.takeNum2),
                paged: function (page) {
                    detail.skipNum2 = (page - 1) * 8;
                    detail.getPersonPoliceList();
                }
            });
        },
        setPageCount: function (sumCount) //设置人员上报人也数量
        {
            this.sumCount = sumCount;
        },
        setPageCountPP: function (sumCount) { //设置人员报警人也数量
            this.sumCountPP = sumCount;
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
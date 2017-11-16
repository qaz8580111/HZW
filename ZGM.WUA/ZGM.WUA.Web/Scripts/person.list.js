$(function () {
    list.init(parent.person.UnitId);
});

var list = {
    unitId: null,
    sumCount: null,
    apiconfig: null,
    takeNum: null,
    skipNum: 0,
    isFirstInit:0,
    userName: null,
    users: null,
    user:null,
    init: function (unitId) {
        this.apiconfig = parent.globalConfig.apiconfig;
        this.takeNum = parent.globalConfig.listTakeNum;
        this.unitId = unitId;
        this.searchUsers();
        $(".minbtn").toggle(function () {
            list.collapse();
        }, function () {
            list.expand();
        });
        list.positionAllUser();
    },
    searchUsers: function () {
        this.userName = this.getSearchContent();
        this.setLoading();
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/User/GetUsersCount",
            data: { userName: list.userName, unitId: list.unitId },
            dataType: "json",
            success: function (result) {
                list.setPageCount(result);
                list.initPage();
                list.getUserList();
               
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getUserList: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: list.apiconfig + "/api/User/GetUsersByPage",
            data: { userName: this.userName, unitId: this.unitId, takeNum: this.takeNum, skipNum: this.skipNum },
            dataType: "json",
            success: function (data) {
                list.users = data;
                var str = "";
                $(".personlist").html("");
                if (data.length == 0) {
                    $(".personlist").html('<p style="margin:0px;font-size:14px;color:#fff;text-align: center;line-height:200px">暂无数据</p>');
                }
                for (var i = 0; i < data.length; i++) {
                    var icon = data[i].IsOnline == 1 ? data[i].IsAlarm == 1 ? "Alarm" : "Online" : "Offline";
                    var msg = data[i].IsMessage == 1 ? "1" : "0";
                    var str = '<div class="personlist_list">'
                        + '<a href="javascript:void(0)" style="color:#87a7c8;" onclick="list.userClicked(' + data[i].UserId + ');">'
                        + '<div class="statusicon' + icon + '" style="display:block; width:38px;height:30px; line-height:30px;"></div>'
                        + '<div  style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:64px;">' + data[i].UserName + '</div>'
                        + '<div style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:71px;">' + data[i].ZFZBH + '</div>'
                        + '<div class="message message' + msg + '" style="float:right; display:block;width:33px; height:30px;  line-height:30px;cursor: pointer;"></div>'
                        + '<div class="topline" style="width:190px; height:1px; margin-left:8px;"></div>'
                        + '</a>'
                   + '</div>'
                    $(".personlist").append(str);
                }
                list.showMessageContent();
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
        list.isFirstInit = 1;
        $('.pagination').jqPagination({
            current_page: 1,
            link_string: '/?page={page_number}',
            max_page: Math.ceil(this.sumCount / this.takeNum),
           
            paged: function (page) {
                list.skipNum = (page - 1) * 8;
                list.setLoading();
                list.getUserList();
            }
        });
    },
    getSearchContent: function () {
        return $("#search").val() == "请输入搜索内容" ? "" : $("#search").val();
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
    userClicked: function (userId) {
        for (var i = 0; i < list.users.length; i++) {
            if (this.users[i].UserId == userId) {
                //显示概要面板
                this.showDetailMin(this.users[i]);
                //地图定位
                this.positionUser(this.users[i]);
                this.user = this.users[i];
            }
        }
    },
    showMessageContent: function () {
        $('.message').each(function (i) {
            $(this).click(function () {
                parent.person.showMessageContent(list.users[i]);
            });
        })       
    },
    positionUser: function (user) {
        this.user = user;
        parent.person.positionUser(user);
        //clearInterval(parent.globalConfig.refreshPosition);
        //list.getUserPositionById();
        //parent.globalConfig.refreshPosition = setInterval("list.getUserPositionById()", 15000);
    },
    positionAllUser: function () {
        $('.allLocate').click(function () {
            //clearInterval(parent.globalConfig.refreshPosition);
            list.getAllUserPosition();
            //parent.globalConfig.refreshPosition = setInterval("list.getAllUserPosition()", 15000);
        });      
    },
    getUserPositionById: function () {
        
        $.ajax({
            type: "GET",
            async: true,
            url: list.apiconfig + "/api/User/GetUserByUserId",
            data: { userId: this.user.UserId },
            dataType: "json",
            success: function (data) {
                parent.person.positionUser(data);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getAllUserPosition: function () {

        parent.person.positionAllUserInTime(list.userName, list.unitId);

        $.ajax({
            type: "GET",
            async: true,
            url: list.apiconfig + "/api/User/GetUsersByPage",
            data: { userName: list.userName, unitId: list.unitId, takeNum: null, skipNum: null },
            dataType: "json",
            success: function (data) {
                parent.person.positionAllUser(data);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    showDetailMin: function (user) {
        parent.person.initPersonDeatilMin(user);
    },
    collapse: function () {
        parent.list.collapse();
        setTimeout(function () {
            $("body").css("background-image", "url('/images/list/listdivbg5.png')");
        }, 300);
    },
    expand: function () {
        parent.list.expand();
        $("body").css("background-image", "url('/images/list/listdivbg4.png')");
    },
    close: function () {
        parent.list.close();
    },
}
$(function () {
    list.init();
});

var list = {
    sumCount: null,
    apiconfig: null,
    takeNum: null,
    skipNum: 0,
    cars: null,
    isFirstInit:0,
    carNumber: null,
    init: function () {
        this.apiconfig = parent.globalConfig.apiconfig;
        this.takeNum = parent.globalConfig.listTakeNum;
        this.searchCars();
       
        $(".minbtn").toggle(function () {
            list.collapse();
        }, function () {
            list.expand();
        });
        list.positionAllCar();
    },
    searchCars: function () {
        this.carNumber = this.getSearchContent();
        this.setLoading();
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/Car/GetCarsCount",
            data: { carNumber: list.carNumber, isOnline: null },
            dataType: "json",
            success: function (result) {
                list.setPageCount(result);
                
                list.initPage();
                list.isFirstInit = 1;
                list.getCarsList();
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    getCarsList: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: list.apiconfig + "/api/Car/GetCarsByPage",
            data: { carNumber: this.carNumber, isOnline: null, takeNum: this.takeNum, skipNum: this.skipNum },
            dataType: "json",
            success: function (data) {
                list.cars = data;
                var str = "";
                $(".personlist").html("");
                if (data.length == 0) {
                    $(".personlist").html('<p style="margin:0px;font-size:14px;color:#fff;text-align: center;line-height:200px">暂无数据</p>');
                }
                for (var i = 0; i < data.length; i++) {
                    var icon = data[i].isOnline == 1 ? data[i].IsOverArea == 1 ? "Alarm" : "Online" : "Offline";
                    var str = '<div class="personlist_list">'
                        + '<a href="javascript:void(0)" style="" onclick="list.carClicked(' + data[i].CarId + ');">'
                        + '<div class="statusicon_c' + icon + '" style="display:block; width:38px;height:30px; line-height:30px;"></div>'
                        + '<div  style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:64px;">' + data[i].CarNumber + '</div>'
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
            list.skipNum =0;
            $('.pagination').jqPagination('destroy');
        }
        $('.pagination').jqPagination({
            current_page: 1,
            link_string: '/?page={page_number}',
            max_page: Math.ceil(this.sumCount / this.takeNum),
           // page_string: '{current_page} / {max_page}页',
            paged: function (page) {
                list.skipNum = (page - 1) * 8;
                //alert($('.pagination').pagination('getCurrentPage'));
                list.setLoading();
                list.getCarsList();
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
    carClicked: function (carId) {
        for (var i = 0; i < list.cars.length; i++) {
            if (this.cars[i].CarId == carId) {
                //显示概要面板
                this.showDetailMin(this.cars[i]);
                //地图定位
                this.positionCar(this.cars[i]);
            }
        }
    },
    showDetailMin: function (car) {
        parent.car.initDetailMin(car);
    },
    positionCar: function (car) {
        parent.car.positionCar(car);
    },
    positionAllCar: function () {
        $('.allLocate').click(function () {
            $.ajax({
                type: "GET",
                async: true,
                url: list.apiconfig + "/api/Car/GetCarsByPage",
                data: { carNumber: list.carNumber, isOnline: null, takeNum: null, skipNum: null },
                dataType: "json",
                success: function (data) {
                    parent.car.positionAllCar(list.cars);
                },
                error: function (errorMsg) {
                    console.log(errorMsg);
                }
            });           
        });
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
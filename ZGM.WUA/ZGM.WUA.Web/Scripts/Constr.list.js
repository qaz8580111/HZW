$(function () {
    list.init();
});

var list = {    
    sumCount: null,
    apiconfig: null,
    takeNum: null,
    skipNum: 0,
    name: null,
    isFirstInit:0,
    ConstrList: null,
    init: function () {
        this.apiconfig = parent.globalConfig.apiconfig;
        this.takeNum = parent.globalConfig.listTakeNum;        
        this.searchConstr();
        $(".minbtn").toggle(function () {
            list.collapse();
        }, function () {
            list.expand();
        });
        this.positionAll();
    },
    searchConstr: function () {
        this.name = this.getSearchContent;
        this.setLoading();
        $.ajax({
            type: "GET",
            async: true,
            url: this.apiconfig + "/api/Constr/GetConstrsCount",
            data: { name: list.name },
            dataType: "json",
            success: function (result) {
                list.setPageCount(result);
                list.initPage();
                list.isFirstInit = 1;
                list.getConstrList();
              
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    positionAll: function () {
        var url = "/api/Constr/GetAllAreas";
        $('.allLocate').click(function () {
            $.ajax({
                type: "GET",
                async: true,
                url: list.apiconfig + url,
                data: { },
                dataType: "json",
                success: function (data) {
                    if (data != null && data.length != 0) {
                        parent.Constr.positionAll(data);
                    }
                },
                error: function (errorMsg) {
                    console.log(errorMsg);
                }
            });
        });
    },
    getConstrList: function () {
      
        $.ajax({
            type: "GET",
            async: true,
            url: list.apiconfig + "/api/Constr/GetConstrsByPage",
            data: { name: list.name, takeNum: list.takeNum, skipNum: list.skipNum },
            dataType: "json",
            success: function (data) {
                list.ConstrList = data;
                var str = "";
                $(".personlist").html("");
                if (data.length == 0) {
                    $(".personlist").html('<p style="margin:0px;font-size:14px;color:#fff;text-align: center;line-height:200px">暂无数据</p>');
                }
                for (var i = 0; i < data.length; i++) {
                    var str ='<div class="personlist_list">'+
                        '<a href="javascript:void(0)"  onclick="list.ConstrClicked(' + data[i].ConstrId + ');">' +
                         '<div class="statusiconConstr  icon  " style="display:block; width:38px;height:30px; line-height:30px;float:left"></div>'+
                         '<div  style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis;width:160px;;float:left"> ' + data[i].ConstrName + '  </div>' +
                         '<div class="topline" style="width:190px; height:1px; margin-left:8px;"></div>'
                    '</a>'+
                    '</div>';
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
                list.getConstrList();
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
    ConstrClicked: function (ConstrId) {
        for (var i = 0; i < list.ConstrList.length; i++) {
            if (this.ConstrList[i].ConstrId == ConstrId) {
                //显示概要面板
                this.showDetailMin(this.ConstrList[i]);
                //地图定位
                this.positionConstr(this.ConstrList[i]);
            }
        }
    },
    positionConstr: function (Constr) {
        parent.Constr.positionConstr(Constr);
    },
    showDetailMin: function (Constr) {
        parent.Constr.initConstrDeatilMin(Constr);
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
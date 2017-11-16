//列表
var list = {
    //显示列表
    show: function (uri) {
        
        map.clears();
        list.expand();
        $("#ifmList").attr("src", uri);
        $("#listDiv").css("visibility", "visible");
        $("#detailDiv").css("visibility", "hidden");
        $("#detail-minDiv").css("visibility", "hidden");
        clearInterval(globalConfig.refreshPosition);
    },
    //关闭列表
    close: function () {
        map.clears();
        $("#ifmList").attr("src", "");
        $("#listDiv").css("visibility", "hidden");
        detail.close();
        detailMin.close();
        clearInterval(globalConfig.refreshPosition);
    },
    //展开列表
    expand: function () {
        $("#listDiv").animate({ height: '360px' });
    },
    //收缩列表
    collapse: function () {
        $("#listDiv").animate({ height: '25px' });
    }
}
//详情
var detail = {
    //显示详情
    show: function (uri) {
        //this.expand();
        $("#ifmDetail").attr("src", uri);
        $("#detailDiv").css("visibility", "visible");
    },
    //关闭详情
    close: function () {
        this.collapse();
        $("#ifmDetail").attr("src", "");
        $("#detailDiv").css("visibility", "hidden");
    },
    //展开列表
    expand: function () {
        $("#detailDiv").animate({ height: '402px' });
    },
    //收缩列表
    collapse: function () {
        $("#detailDiv").animate({ height: '25px' });
    }
}

//概要面板
var detailMin = {
    //显示概要
    show: function (uri) {
        $("#ifmDetailMin").attr("src", uri);
        $("#detailDiv").css("visibility", "hidden");
        $("#detail-minDiv").css("visibility", "visible");
    },
    //关闭概要
    close: function () {
        $("#ifmDetailMin").attr("src", "");
        $("#detail-minDiv").css("visibility", "hidden");
        detail.close();
    }
}

var report = {
    show: function () {
        $("#ifmReport").attr("src", "Views/Report/Report.aspx");

        $("#reportDiv").css("visibility", "visible");
        $("#swDiv").css("visibility", "hidden");
        $("#ewDiv").css("visibility", "hidden");
        $("#menuDiv").css("visibility", "hidden");
        list.close();
    },
    close: function () {
        $("#ifmReport").attr("src", "");

        $("#reportDiv").css("visibility", "hidden");
        $("#swDiv").css("visibility", "visible");
        $("#menuDiv").css("visibility", "visible");
    }
}
//自定义弹窗 dialog
var dialog_self = {
    diag: null,
    //内容页为html代码的窗口
    htmlDialog: function (title, innerhtml) {
        this.diag = new Dialog();
        this.diag.Width = 300;
        this.diag.Height = 100;
        this.diag.Title = title;
        this.diag.InnerHtml = innerhtml;
        this.diag.OKEvent = function () { dialog_self.diag.close(); };//点击确定后调用的方法
        this.diag.show();
    }

}

//菜单栏
//切换显示菜单页
function changeMenu(src) {
    $("#ifmMenu").attr("src", src);
}
//清除全部面板
function ClearAll() {
    $("#listDiv").css("visibility", "hidden");
    $("#detail-minDiv").css("visibility", "hidden");
    $("#detailDiv").css("visibility", "hidden");
}